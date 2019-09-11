using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
       // public string pl = null;
        public string org = null;
       // int sumcnt = 0;
        //int plancnt = 0;
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



        public GUI(IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans, List<string> p1, List<string> p2, List<string> p3, List<string> p4, List<string> p5, List<string> p6, List<string> p7, List<string> p8)
        {

            MessageBox.Show("Trig 5");
            InitializeComponent();
            L1 = p1;
            L2 = p2;
            L3 = p3;
            L4 = p4;
            L5 = p5;
            L6 = p6;
            L7 = p7;
            L8 = p8;

            // CustomSelectedIndexChange.CEvent += CustomSelectedIndexChange_CEvent;

            MessageBox.Show("Trig 6");

            foreach (PlanSum aplansum in Plansums)
            {
                PlanList.Items.Add(aplansum.Id);
                sumnames.Add(aplansum.Id);

                MessageBox.Show("Trig 7");
            }

            foreach (PlanSetup aplan in Plans)
            {
                PlanList.Items.Add(aplan.Id);
                plannames.Add(aplan.Id);

                MessageBox.Show("Trig 8");
            }







            MessageBox.Show("Trig 13");
        }  // end of actual GUI execution


        //GUI related functions

        private int Veff ()
        {
            int V = 0;

            // calculate Veff

            return V;
        }

        private int NTCPCALC ()
        {
            int NTCP = 0;
            int V = Veff();


            // calculate NTCP

            return NTCP;
        }    

        private void ExecuteCalc_Click(object sender, EventArgs e)
        {
            org = OrganList.SelectedItem.ToString();

            MessageBox.Show("Trig 12");
            int V = Veff();

            VeffOut.Text = "Effective Volume: " + V;

            int NTCP = NTCPCALC();

            NTCPout.Text = "Normal Tissue Complication Probability: " + NTCP + "%";

        }

        void PlanList_SelectedIndexChanged(object sender, EventArgs e)
        {
           string pl = PlanList.SelectedItem.ToString();

            int c = 0;
            int k = 0;
            MessageBox.Show("Trig 10");
            foreach (string str in sumnames)
            {
                c++;
                if(pl == str)
                {
                    if(c == 1)
                    {
                        OrganList.DataSource = L1;
                    }
                    else if(c == 2)
                    {
                        OrganList.DataSource = L2;
                    }
                    else if(c == 3)
                    {
                        OrganList.DataSource = L3;
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
                        OrganList.DataSource = L4;
                    }
                    else if (k == 2)
                    {
                        OrganList.DataSource = L5;
                    }
                    else if (k == 3)
                    {
                        OrganList.DataSource = L6;
                    }
                    else if (k == 4)
                    {
                        OrganList.DataSource = L7;
                    }
                    else if (k == 5)
                    {
                        OrganList.DataSource = L8;
                    }

                }
            }
            MessageBox.Show("Trig 11");
        }
    }
}
