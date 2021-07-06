using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;



/*
    Liver Only Normal Tissue Complication Probability (NTCP) Calculator - ESAPI 16.0 version (3/16/2020).

    This is the WinForms GUI for the NTCP calc program, where most of the program takes place. It is called by the NTCPcalc start-up program.

    This program is expressely written as a plug-in script for use with Varian's Eclipse Treatment Planning System, and requires Varian's API files to run properly.
    This program also requires .NET Framework 4.6.1 to run properly, and the MathNet.Numerics class library package, which is freely availible on the NuGet Package manager in Visual Studio.
    The MathNet.Numerics package contains an Error function method which is used in the NTCP calculation.

    Copyright (C) 2021 Zackary Thomas Ricci Morelli
    
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

        I can be contacted at: zackmorelli@gmail.com
       
*/



namespace NTCPcalc
{
    public partial class GUI : Form
    {
        public string pl = null;
        public string org = null;
        double TD50 = 0.0;
      //  double ab = 0.0;
        string ty;
        // double n = 0.0;
        // double m = 0.0;
        List<string> plannames = new List<string>();
        List<string> sumnames = new List<string>();

        public GUI(IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans)
        {
            // MessageBox.Show("Trig 5");
            InitializeComponent();
 
            // MessageBox.Show("Trig 6");

            foreach (PlanSetup aplan in Plans)
            {
                PlanList.Items.Add(aplan.Id);
                plannames.Add(aplan.Id);
                // MessageBox.Show("Trig 8");
            }

            ExecuteCalc.Click += (sender, EventArgs) => { buttonNext_Click(sender, EventArgs, Plansums, Plans); };

   
            //  MessageBox.Show("Trig GUI END");
        }  // end of actual GUI execution

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //associated functions

        private double Veff(DVHData sDVH, double RX, double Vol)
        {
            double VE = 0.0;
            double TotVol = 0.0;

            //  MessageBox.Show("sDVH.CurveData[50].DoseValue is: " + sDVH.CurveData[50].DoseValue);

            //This performs a volume sum of the Liver DVH curve. It is cut off once it gets to a very small volume; for some reason Eclipse attempts to figure out dose values at very small volumes but it is really meaningless and negligible.
            foreach (DVHPoint point in sDVH.CurveData)
            {
                TotVol += point.Volume;

                if (point.Volume < 0.00025)    // ends volume sum before very end to prevent sum of strange values at very end
                {
                    break;
                }
            }

           //  MessageBox.Show("Totvol is: " + TotVol);

            double Area = (TotVol * 0.1);  // Volume Sum multiplied by the Dose step size (always 0.1) gives the area under the DVH curve for the organ in question

            double AbsVeff = (Area / (RX * 100.0));  // this gives the Veff in cc

            // MessageBox.Show("ABSVeff is: " + AbsVeff);
           //  MessageBox.Show("Vol is: " + Vol);

            VE = (AbsVeff / Vol);  // divide the Veff by the total volume of the organ to express Veff as a percentage

            //  MessageBox.Show("VE is: " + VE);

            return VE;
        }

        private double NTCPCALC(double VE, double ab, double Dmax, int fracs, double n, double m)
        {
             // MessageBox.Show("Dmax is: " + Dmax.ToString());
            //  double Fracs = (double)fracs;
           //   MessageBox.Show("Fracs is: " + fracs.ToString());
          // MessageBox.Show("VE is: " + VE.ToString());
          //  MessageBox.Show("TD50 is: " + TD50.ToString());
           // MessageBox.Show("ab is: " + ab.ToString());
          //  MessageBox.Show("Rx is: " + RX.ToString());
          //  MessageBox.Show("Vol is: " + Vol.ToString());
         //  MessageBox.Show("n is: " + n.ToString());
         //   MessageBox.Show("m is: " + m.ToString());

            //This is a protection to make sure the user selected the specific diagnosis for the patient they are making a liver plan for. Otherwise we don't know what the TD50 is.
            if(TD50 == 0)
            {
                MessageBox.Show("Please select either METS or HCC for the calculation to work properly");
            }

            double NTCP = 0.0;

            // The NTCP calculation is broken into parts here to make it easier.

            double NTD2 = Dmax * (((Dmax / fracs) + ab) / (2 + ab));

            double TDVeff = TD50 * (Math.Pow(VE, -n));

            double t = ((NTD2 - TDVeff) / TDVeff) / m;  //  t is the parameter given to the Error function

            // NTCP = 1/2(ERF(t/sqrt(2)) + 1)

            //This is the core of what this program does. The NTCP formula is expressed as an Error Function (one of the special named integral functions), so the Math.Net Numerics package is used to numerically approximate it.
            double erf = MathNet.Numerics.SpecialFunctions.Erf((t / Math.Sqrt(2)));

            NTCP = (erf + 1) * 0.5;

            // MessageBox.Show("NTCP is: " + NTCP);    used to show the whole value and verify calculation
            return NTCP;
        }

        private void EXECUTE(IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans)
        {
            //All of the parameters of the LKB model and the other variables used are all declared here, that way they can all be changed in one place.
            bool livlock = false;
            double n = 1.0;         // Volume effect parameter of the Lyman-Kutcher-Burman model. Set here to one in accordance with the Lahey SBRT Liver Protocol
            double m = 0.12;        // Slope parameter of the Lyman-Kutcher-Burman model. set here to 0.12 in accordance with the Lahey SBRT Liver Protocol
            double ab = 3.0;        // alpha/beta radation sensitivity ratio of Livers. Originally comes from Linear-Quadratic model, used in LKB as well.
            double RX = 0.0;        //prescribed dose of the organ in cGy
            double Dmax = 0.0;     // max dose of the organ
            double Vol = 0.0;      // volume of the organ
            int fracs = 0;          // number of fractions to the organ

           // MessageBox.Show("Trig EXE - 1");


            //This bit of code below is just to find the plan the user selected from the list
            //it is the same anachronistic code from the Dose objective check program, obviously using LINQ would be better
            IEnumerator ER = Plans.GetEnumerator();
            ER.MoveNext();
            PlanSetup Plan = (PlanSetup)ER.Current;

            if (Plan.Id == pl)
            {
                // MessageBox.Show("Trig EXE - 2");
                Plan = (PlanSetup)ER.Current;
            }
            else
            {
                ER.MoveNext();
                Plan = (PlanSetup)ER.Current;
                if (Plan.Id == pl)
                {
                    Plan = (PlanSetup)ER.Current;
                   // MessageBox.Show("Trig EXE - 3");
                }
                else
                {
                    ER.MoveNext();
                    Plan = (PlanSetup)ER.Current;
                    if (Plan.Id == pl)
                    {
                        Plan = (PlanSetup)ER.Current;
                    }
                    else
                    {
                        ER.MoveNext();
                        Plan = (PlanSetup)ER.Current;
                        if (Plan.Id == pl)
                        {
                            Plan = (PlanSetup)ER.Current;
                        }
                        else
                        {
                            ER.MoveNext();
                            Plan = (PlanSetup)ER.Current;
                            if (Plan.Id == pl)
                            {
                                Plan = (PlanSetup)ER.Current;
                            }
                            else
                            {
                                ER.MoveNext();
                                Plan = (PlanSetup)ER.Current;
                                if (Plan.Id == pl)
                                {
                                    Plan = (PlanSetup)ER.Current;
                                }
                                else
                                {
                                    ER.MoveNext();
                                    Plan = (PlanSetup)ER.Current;
                                    if (Plan.Id == pl)
                                    {
                                        Plan = (PlanSetup)ER.Current;
                                    }
                                    else
                                    {
                                        ER.MoveNext();
                                        Plan = (PlanSetup)ER.Current;
                                        if (Plan.Id == pl)
                                        {
                                            Plan = (PlanSetup)ER.Current;
                                        }
                                        else
                                        {
                                            ER.MoveNext();
                                            Plan = (PlanSetup)ER.Current;
                                            if (Plan.Id == pl)
                                            {
                                                Plan = (PlanSetup)ER.Current;
                                            }
                                            else
                                            {
                                                ER.MoveNext();
                                                Plan = (PlanSetup)ER.Current;
                                                if (Plan.Id == pl)
                                                {
                                                    Plan = (PlanSetup)ER.Current;
                                                }
                                                else
                                                {
                                                    ER.MoveNext();
                                                    Plan = (PlanSetup)ER.Current;
                                                    if (Plan.Id == pl)
                                                    {
                                                        Plan = (PlanSetup)ER.Current;
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Could not find the selected plan!");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

           //now that we have identified the plan, we get the prescribed dose and number of fractions
          //  MessageBox.Show("Trig EXE - 10");

          RX = Plan.TotalDose.Dose;

          try
          {
               fracs = (int)Plan.NumberOfFractions;
          }
          catch
          {
                MessageBox.Show("An error ocurred when attempting to retrieve the Number of Fractions of this plan. A default value of 5 will be used.");
                fracs = 5;
          }
          
         //Then we identify the Liver structure, if it exists.
         //In order to expand this program to calculate NTCP for various organs, you would to identify other organs and then look up the parameters required from a static list of a custom class used to store the NTCP parameters 

          IEnumerator ZK = Plan.StructureSet.Structures.GetEnumerator();      // all of this stuff from line 286 to 291 is just so we can get an initialized structure variable that we can use to instantiate a DVHdata variable, all because we don't have write access to Varian's API                                                                 
          ZK.MoveNext();
          // MessageBox.Show("Trig EXE - 11");

          Structure STR = (Structure)ZK.Current;

          foreach (Structure S in Plan.StructureSet.Structures)
          {
              if (S.Id == "Liver")
              {
                  //  MessageBox.Show("S.Id is: " + S.Id.ToString());
                  STR = S;
                  livlock = true;
                  // MessageBox.Show("Trig EXE - f3.4");
              }
          }

          if(livlock == false)
          {
                MessageBox.Show("Warning: No liver structure was found in this plan! Unable to perform calculation.");
                return;
          }

          //Once we have the Liver structure we pull it's DVH info to get the max dose to the liver
          DVHData sDVH = Plan.GetDVHCumulativeData(STR, DoseValuePresentation.Absolute, VolumePresentation.AbsoluteCm3, 0.1);

          // MessageBox.Show("DVH fetched successfully!");

          Vol = STR.Volume;
          Dmax = 0.0;

            //  DoseValue maxdose = kDVH.MaxDose;

            foreach (DVHPoint point in sDVH.CurveData)
            {
                if (point.Volume < 0.1 && point.Volume > 0.03)
                {
                    // Console.WriteLine("\n  DVH Point VOLUME: {0}", point.Volume);

                    if (Dmax == 0.0)
                    {
                        Dmax = point.DoseValue.Dose;
                    }

                    if (point.DoseValue.Dose > Dmax)
                    {
                        Dmax = point.DoseValue.Dose;
                    }
                }
            }

          RX = (RX / 100.0);       // converting cGy to Gy
          Dmax = (Dmax / 100.0);
            
            //The Liver's DVH object, the prescribed dose of the plan, and the volume of the Liver are passed to a method which calculates the effective volume.
          double VE = Veff(sDVH, RX, Vol);

            // The effective volume returned by the Veff method is displayed on the GUI
          VeffOut.Text = Math.Round((VE * 100.0), 1, MidpointRounding.AwayFromZero) + "%";

            //The effective volume, alpha/beta ratio, max dose, number of fractions, the volume parameter, and the slope parameter are passed to a mehtod which calculates the NTCP.
          double NTCP = NTCPCALC(VE, ab, Dmax, fracs, n, m);

            //The NTCP value returned by the NTCPCALC method is displayed on the GUI.
          NTCPout.Text = Math.Round((NTCP * 100.0), 1, MidpointRounding.AwayFromZero) + "%";
        }

        private void ExecuteCalc_Click(object sender, EventArgs args)
        {
           
            //  MessageBox.Show("Organ: " + org.ToString());
            //  MessageBox.Show("Trig 12 - First Click");
        }

        void buttonNext_Click(object sender, EventArgs e, IEnumerable<PlanSum> PLS, IEnumerable<PlanSetup> PLN)
        {
            // MessageBox.Show("Trig MORTY");
            EXECUTE(PLS, PLN);
        }

        void PlanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            pl = PlanList.SelectedItem.ToString();

            // Liver plans usually have a motion assesment plan. This makes sure the program doesn't attempt to run if the user chooses it by accident, as it will cause an error.
            if (pl == "Motion Assess" || pl == "motion assess" || pl == "Mot Assess" || pl == "mot assess")
            {
                MessageBox.Show("This script is not compatible with Motion Assess plans!");
                return;
            }
            //  MessageBox.Show("Trig 10");
           
        }
        
        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show("checkedlistboxfire");
            if (listBox1.SelectedItem.ToString() == "HCC")
            {
                ty = "HCC";
                TD50 = 39.8;
            }
            else if (listBox1.SelectedItem.ToString() == "METS")
            {
                ty = "METS";
                TD50 = 45.8;
            }
        }



    }
}
