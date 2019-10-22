using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VMS.TPS;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

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

            foreach (DVHPoint point in sDVH.CurveData)
            {
                TotVol += point.Volume;

                if (point.Volume < 0.00025)    // ends volume sum before very end to prevent sum of strange values at very end
                {
                    break;
                }
            }

           //  MessageBox.Show("Totvol is: " + TotVol);

            double Area = (TotVol * 0.1);  // Volume Sum multiplied by the Dose step size gives the area under the DVH curve for the organ in question

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

            if(TD50 == 0)
            {
                MessageBox.Show("Please select either METS or HCC for the calculation to work properly");
            }

            double NTCP = 0.0;
            //  double n = 0.97;  // Volume effect paramter of the Lyman-Kutcher-Burman model. hard coded to 0.97 for Liver for now
            //  double m = 0.12;  // The Slope parameter of the Lyman-Kutcher-Burman model. hard coded to 0.12 for Liver for Now
            // double TD50 = 45.8; // The 50% whole organ irradiation tolerance dose, in Gy. hard coded to 45.8 Gy for Liver for now.
            // double ab = 3.0; // alpha/beta ratio. hard coded as 3 for liver for now

            double NTD2 = Dmax * (((Dmax / fracs) + ab) / (2 + ab));

            double TDVeff = TD50 * (Math.Pow(VE, -n));

            double t = ((NTD2 - TDVeff) / TDVeff) / m;  //  t is the parameter given to the ERF function

            // NTCP = 1/2(ERF(t/sqrt(2)) + 1)

            double erf = MathNet.Numerics.SpecialFunctions.Erf((t / Math.Sqrt(2)));

            NTCP = (erf + 1) * 0.5;

             MessageBox.Show("NTCP is: " + NTCP);
            return NTCP;
        }

        private void EXECUTE(IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans)
        {
            
            double n = 1.0;         // Volume effect parameter of the Lyman-Kutcher-Burman model. Set here to one in accordance with the Lahey SBRT Liver Protocol
            double m = 0.12;        // Slope parameter of the Lyman-Kutcher-Burman model. set here to 0.12 in accordance with the Lahey SBRT Liver Protocol
            double ab = 3.0;        // alpha/beta radation sensitivity ratio of Livers. Originally comes from Linear-Quadratic model, used in LKB as well.
            double RX = 0.0;        //prescribed dose of the organ in cGy
            double Dmax = 0.0;     // max dose of the organ
            double Vol = 0.0;      // volume of the organ
            int fracs = 0;          // number of fractions to the organ

           // MessageBox.Show("Trig EXE - 1");

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
                        }
                    }
                }
            }

          //  MessageBox.Show("Trig EXE - 10");

          RX = Plan.TotalPrescribedDose.Dose;

          try
          {
               fracs = (int)Plan.UniqueFractionation.NumberOfFractions;
          }
          catch
          {
                MessageBox.Show("An error ocurred when attempting to retrieve the Number of Fractions of this plan. A default value of 5 will be used.");
                fracs = 5;
          }
                
          IEnumerator ZK = Plan.StructureSet.Structures.GetEnumerator();      // all of this stuff from line 157 to 168 is just so we can get an initialized structure variable that we can use to instaiatiate a DVHdata variable, all because we don't have write access to Varian's API
                                                                                    //  MessageBox.Show("Trig EXE - f3.1");
          ZK.MoveNext();
          // MessageBox.Show("Trig EXE - 11");

          Structure STR = (Structure)ZK.Current;
         // MessageBox.Show("Trig EXE - f3.3");

          foreach (Structure S in Plan.StructureSet.Structures)
          {
              if (S.Id == "Liver")
              {
                  //  MessageBox.Show("S.Id is: " + S.Id.ToString());
                  STR = S;
                  // MessageBox.Show("Trig EXE - f3.4");
              }
          }

          DVHData sDVH = Plan.GetDVHCumulativeData(STR, DoseValuePresentation.Absolute, VolumePresentation.AbsoluteCm3, 0.1);

          MessageBox.Show("DVH fetched successfully!");

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

          double VE = Veff(sDVH, RX, Vol);

          VeffOut.Text = "Effective Volume: " + Math.Round((VE * 100.0), 1, MidpointRounding.AwayFromZero) + "%";

          double NTCP = NTCPCALC(VE, ab, Dmax, fracs, n, m);

          NTCPout.Text = "Normal Tissue Complication Probability: " + Math.Round((NTCP * 100.0), 1, MidpointRounding.AwayFromZero) + "%";
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

            //  MessageBox.Show("Trig 10");
           
        }
        
        void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show("checkedlistboxfire");
            if (checkedListBox1.GetItemChecked(0))
            {
                ty = "HCC";
                TD50 = 39.8;
            }
            else if (checkedListBox1.GetItemChecked(1))
            {
                ty = "METS";
                TD50 = 45.8;
            }
        }

        void checkedListBox1_ItemCheck(object sender, EventArgs e)
        {
            if (checkedListBox1.GetItemChecked(0))
            {
                ty = "HCC";
                TD50 = 39.8;
            }
            else if (checkedListBox1.GetItemChecked(1))
            {
                ty = "METS";
                TD50 = 45.8;
            }
        }
    }
}
