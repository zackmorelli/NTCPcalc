﻿using System;
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
        // int sumcnt = 0;
        //int plancnt = 0;
        public int c = 0;
        public int k = 0;
        public int c1 = 0;
        public int k1 = 0;
        List<string> plannames = new List<string>();
        List<string> sumnames = new List<string>();

        List<string> L1 = new List<string>();
        List<string> L2 = new List<string>();
        List<string> L3 = new List<string>();
        List<string> L4 = new List<string>();
        List<string> L5 = new List<string>();
        List<string> L6 = new List<string>();
        List<string> L7 = new List<string>();
        List<string> L8 = new List<string>();

        public GUI(IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans)
        {
            // MessageBox.Show("Trig 5");
            InitializeComponent();

 
            // MessageBox.Show("Trig 6");

            foreach (PlanSum aplansum in Plansums)
            {
                PlanList.Items.Add(aplansum.Id);
                sumnames.Add(aplansum.Id);
                // MessageBox.Show("Trig 7");
            }

            foreach (PlanSetup aplan in Plans)
            {
                PlanList.Items.Add(aplan.Id);
                plannames.Add(aplan.Id);
                // MessageBox.Show("Trig 8");
            }

            ExecuteCalc.Click += (sender, EventArgs) => { buttonNext_Click(sender, EventArgs, Plansums, Plans, TD50); };

   


            //  MessageBox.Show("Trig GUI END");
        }  // end of actual GUI execution

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //associated functions

        private double Veff(DVHData sDVH, double RX, double Dmax, double Vol)
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

            // MessageBox.Show("Totvol is: " + TotVol);

            double Area = (TotVol * 0.1);  // Volume Sum multiplied by the Dose step size gives the area under the DVH curve for the organ in question

            double AbsVeff = (Area / (RX * 100.0));  // this gives the Veff in cc

            // MessageBox.Show("ABSVeff is: " + AbsVeff);
            // MessageBox.Show("Vol is: " + Vol);

            VE = (AbsVeff / Vol);  // divide the Veff by the total volume of the organ to express Veff as a percentage

            //  MessageBox.Show("VE is: " + VE);

            return VE;
        }

        private double NTCPCALC(double VE, double TD50, double ab, double RX, double Dmax, double Vol, int fracs, double n, double m)
        {
            //  MessageBox.Show("Dmax is: " + Dmax.ToString());
            //  double Fracs = (double)fracs;
            //  MessageBox.Show("Fracs is: " + fracs.ToString());
            double NTCP = 0.0;
            ///  double n = 0.97;  // Volume effect paramter of the Lyman-Kutcher-Burman model. hard coded to 0.97 for Liver for now
            //  double m = 0.12;  // The Slope parameter of the Lyman-Kutcher-Burman model. hard coded to 0.12 for Liver for Now
            // double TD50 = 45.8; // The 50% whole organ irradiation tolerance dose, in Gy. hard coded to 45.8 Gy for Liver for now.
            // double ab = 3.0; // alpha/beta ratio. hard coded as 3 for liver for now

            double NTD2 = Dmax * (((Dmax / fracs) + ab) / (2 + ab));

            double TDVeff = TD50 * (Math.Pow(VE, -n));

            double t = ((NTD2 - TDVeff) / TDVeff) / m;  //  t is the parameter given to the ERF function

            // NTCP = 1/2(ERF(t/sqrt(2)) + 1)

            double erf = MathNet.Numerics.SpecialFunctions.Erf((t / Math.Sqrt(2)));

            NTCP = (erf + 1) * 0.5;

            // MessageBox.Show("NTCP is: " + NTCP);
            return NTCP;
        }

        private void EXECUTE(string org, double TD50, IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans)
        {
            double n = 1.0;  // 
            double m = 0.12;
            double ab = 3.0;

              MessageBox.Show("Trig EXE");
            IEnumerator ER = Plans.GetEnumerator();
             MessageBox.Show("Trig EXE - f1");
            ER.MoveNext();
              MessageBox.Show("Trig EXE - f2");
            PlanSetup Plan = (PlanSetup)ER.Current;
              MessageBox.Show("Trig EXE - f3");
            DVHData sDVH = Plan.GetDVHCumulativeData(Plan.StructureSet.Structures.ElementAt(14), DoseValuePresentation.Absolute, VolumePresentation.AbsoluteCm3, 0.1);

              MessageBox.Show("Trig EXE - f4");
            double RX = 0.0;        //prescribed dose of the organ in cGy
            double Dmax = 0.0;     // max dose ot the organ
            double Vol = 0.0;      // volume of the organ
            int fracs = 0;          // number of fractions to the organ

             MessageBox.Show("Trig EXE - f5");

            if (c1 > 0)
            {
                   MessageBox.Show("Trig EXE - 2-1");
                IEnumerator TR = Plansums.GetEnumerator();
                TR.MoveNext();
                PlanSum Plansum = (PlanSum)TR.Current;

                  MessageBox.Show("Trig EXE - 3");
                if (c1 == 1)
                {
                    Plansum = (PlanSum)TR.Current;
                }
                else if (c1 == 2)
                {
                    TR.MoveNext();
                    Plansum = (PlanSum)TR.Current;
                }
                else if (c1 == 3)
                {
                    TR.MoveNext();
                    TR.MoveNext();
                    Plansum = (PlanSum)TR.Current;
                }

                RX = (Plansum.PlanSetups.ElementAt(1).TotalPrescribedDose.Dose + Plansum.PlanSetups.ElementAt(2).TotalPrescribedDose.Dose);
                fracs = (int)Plansum.PlanSetups.ElementAt(1).UniqueFractionation.NumberOfFractions;
                 MessageBox.Show("Trig EXE - 4");
                foreach (Structure S in Plansum.StructureSet.Structures)
                {
                    if (S.Id == org)
                    {
                          MessageBox.Show("Trig EXE - 5");
                        sDVH = Plansum.GetDVHCumulativeData(S, DoseValuePresentation.Absolute, VolumePresentation.AbsoluteCm3, 0.1);
                        Vol = S.Volume;
                    }
                }
                  MessageBox.Show("Trig EXE - 6");
            }
            else if (k1 > 0)
            {

                  MessageBox.Show("K is: " + k1.ToString());
                if (k1 == 1)
                {
                      MessageBox.Show("Trig EXE - 7");
                    Plan = (PlanSetup)ER.Current;
                }
                else if (k1 == 2)
                {
                    ER.MoveNext();
                    Plan = (PlanSetup)ER.Current;
                }
                else if (k1 == 3)
                {
                    ER.MoveNext();
                    ER.MoveNext();
                    Plan = (PlanSetup)ER.Current;
                }
                else if (k1 == 4)
                {
                    ER.MoveNext();
                    ER.MoveNext();
                    ER.MoveNext();
                    Plan = (PlanSetup)ER.Current;
                }
                else if (k1 == 5)
                {
                    ER.MoveNext();
                    ER.MoveNext();
                    ER.MoveNext();
                    ER.MoveNext();
                    Plan = (PlanSetup)ER.Current;
                }
                 MessageBox.Show("Trig EXE - 8");
                RX = Plan.TotalPrescribedDose.Dose;
                fracs = (int)Plan.UniqueFractionation.NumberOfFractions;

                foreach (Structure S in Plan.StructureSet.Structures)
                {
                    if (S.Id == org)
                    {
                         MessageBox.Show("Trig EXE - 9");
                        sDVH = Plan.GetDVHCumulativeData(S, DoseValuePresentation.Absolute, VolumePresentation.AbsoluteCm3, 0.1);
                        Vol = S.Volume;
                    }
                }
            }

             MessageBox.Show("Trig 13");

            Dmax = sDVH.MaxDose.Dose;


            /* foreach (DVHPoint point in sDVH.CurveData)
             {
                  if (point.Volume < 0.1 && point.Volume > 0.03)
                  {
                       // Console.WriteLine("\n  DVH Point VOLUME: {0}", point.Volume);
                       // This loop finds the Max Dose for a volume of at least 0.03 cc
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
             */
            //  MessageBox.Show("MaxDose 2 is: " + Dmax.ToString());

            RX = RX / 100.0;       // converting cGy to Gy
            Dmax = Dmax / 100.0;
            //  MessageBox.Show("MaxDose 1 is: " + Dmax.ToString());
            //  MessageBox.Show("RXDose 1 is: " + RX.ToString());
              MessageBox.Show("Fracs is: " + fracs.ToString());

            double VE = Veff(sDVH, RX, Dmax, Vol);

            VeffOut.Text = "Effective Volume: " + Math.Round((VE * 100.0), 3, MidpointRounding.AwayFromZero) + "%";

            double NTCP = NTCPCALC(VE, TD50, ab, RX, Dmax, Vol, fracs, n, m);

            NTCPout.Text = "Normal Tissue Complication Probability: " + Math.Round((NTCP * 100.0), 3, MidpointRounding.AwayFromZero) + "%";
        }

        private void ExecuteCalc_Click(object sender, EventArgs args)
        {
           
            //  MessageBox.Show("Organ: " + org.ToString());
            //  MessageBox.Show("Trig 12 - First Click");
        }

        void buttonNext_Click(object sender, EventArgs e, IEnumerable<PlanSum> PLS, IEnumerable<PlanSetup> PLN, double TD50)
        {
            // MessageBox.Show("Trig MORTY");
            EXECUTE(org, TD50, PLS, PLN);
        }

        void PlanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            pl = PlanList.SelectedItem.ToString();

            //  MessageBox.Show("Trig 10");
            foreach (string str in sumnames)
            {
                c++;
                if (pl == str)
                {
                    if (c == 1)
                    {
                        // MessageBox.Show("Trig L1");
                       
                        break;
                    }
                    else if (c == 2)
                    {
                        //  MessageBox.Show("Trig L2");
                        
                        break;
                    }
                    else if (c == 3)
                    {
                        // MessageBox.Show("Trig L3");
                       
                        break;
                    }
                }
            }
            foreach (string str in plannames)
            {
                k++;
                if (pl == str)
                {
                    if (k == 1)
                    {
                        // MessageBox.Show("Trig L4");
                        
                        break;
                    }
                    else if (k == 2)
                    {
                        //  MessageBox.Show("Trig L5");
                        
                        break;
                    }
                    else if (k == 3)
                    {
                        // MessageBox.Show("Trig L6");
                       ;
                        break;
                    }
                    else if (k == 4)
                    {
                        // MessageBox.Show("Trig L7");
                        
                        break;
                    }
                    else if (k == 5)
                    {
                        // MessageBox.Show("Trig L8");
                       
                        break;
                    }
                }
            }
            // MessageBox.Show("Trig 11");
            c1 = c;
            k1 = k;
            c = 0;
            k = 0;
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
    }
}
