using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;


/*
    Normal Tissue Complication Probability (NPTC) Calculator
    Copyright (c) 2019 Radiation Oncology Department, Lahey Health

    This program is expressely written as a plug-in script for use with Varian's Eclipse Treatment Planning System, and requires Varian's API files to run properly.
    This program also requires .NET Framework 4.5.0 to run properly.

*/

namespace VMS.TPS
{
    public class Script  // creates a class called Script within the VMS.TPS Namesapce
    {

       public Script() { }  // instantiates a Script class

        // Global Variable Declaration

       public String pl = null;

        // Declaration space for all the functions which make up the program.
        // Execution begins with the "Execute" function.

 





        public void Execute(ScriptContext context)     // PROGRAM START - sending a return to Execute will end the program
        {
            //Variable declaration space

            int sumcnt = 0;
            int plancnt = 0;

            IEnumerable<PlanSum> Plansums = context.PlanSumsInScope;
            IEnumerable<PlanSetup> Plans = context.PlansInScope;
            List<string> p1 = new List<string>();
            List<string> p2 = new List<string>();
            List<string> p3 = new List<string>();
            List<string> p4 = new List<string>();
            List<string> p5 = new List<string>();
            List<string> p6 = new List<string>();
            List<string> p7 = new List<string>();
            List<string> p8 = new List<string>();

            // start of actual code

            MessageBox.Show("Trig 1");
            if (context.Patient == null)
            {
                MessageBox.Show("Please load a patient with a treatment plan before running this script!");
                return;
            }

            //this snippet generates lists of the structures for every plan ans plansum that might be loaded in. these lists sre then used to populate the Organ list dropdown in the GUI.

            foreach (PlanSum aplansum in Plansums)
            {
                if (aplansum.StructureSet.Structures == null)
                {
                    continue;
                }

                sumcnt++;
                MessageBox.Show("Trig 2");
                if (sumcnt == 1)
                {
                    foreach (Structure S in aplansum.StructureSet.Structures)
                    {
                        if (S.Id != null)
                        {
                            p1.Add(S.Id);
                        }
                        else if (S.Name != null)
                        {
                            p1.Add(S.Name);
                        }
                        else if (S.ToString() != null)
                        {
                            p1.Add(S.ToString());
                        }
                    }
                }
                else if (sumcnt == 2)
                {
                    foreach (Structure S in aplansum.StructureSet.Structures)
                    {
                        if (S.Id != null)
                        {
                            p2.Add(S.Id);
                        }
                        else if (S.Name != null)
                        {
                            p2.Add(S.Name);
                        }
                        else if (S.ToString() != null)
                        {
                            p2.Add(S.ToString());
                        }
                    }
                }
                else if (sumcnt == 3)
                {
                    foreach (Structure S in aplansum.StructureSet.Structures)
                    {
                        if (S.Id != null)
                        {
                            p3.Add(S.Id);
                        }
                        else if (S.Name != null)
                        {
                            p3.Add(S.Name);
                        }
                        else if (S.ToString() != null)
                        {
                            p3.Add(S.ToString());
                        }
                    }
                }
            }

            foreach (PlanSetup aplan in Plans)
            {
                if(aplan.StructureSet.Structures == null)
                {
                    continue;
                }

                plancnt++;
                MessageBox.Show("Trig 3");
                if (plancnt == 1)
                {
                    foreach (Structure S in aplan.StructureSet.Structures)
                    {
                        if (S.Id != null)
                        {
                            p4.Add(S.Id);
                        }
                        else if (S.Name != null)
                        {
                            p4.Add(S.Name);
                        }
                        else if(S.ToString() != null)
                        {
                            p4.Add(S.ToString());
                        }
                    }
                }
                else if (plancnt == 2)
                {
                    foreach (Structure S in aplan.StructureSet.Structures)
                    {
                        if (S.Id != null)
                        {
                            p5.Add(S.Id);
                        }
                        else if (S.Name != null)
                        {
                            p5.Add(S.Name);
                        }
                        else if (S.ToString() != null)
                        {
                            p5.Add(S.ToString());
                        }
                    }
                }
                else if (plancnt == 3)
                {
                    foreach (Structure S in aplan.StructureSet.Structures)
                    {
                        if (S.Id != null)
                        {
                            p6.Add(S.Id);
                        }
                        else if (S.Name != null)
                        {
                            p6.Add(S.Name);
                        }
                        else if (S.ToString() != null)
                        {
                            p6.Add(S.ToString());
                        }
                    }
                }
                else if (plancnt == 4)
                {
                    foreach (Structure S in aplan.StructureSet.Structures)
                    {
                        if (S.Id != null)
                        {
                            p7.Add(S.Id);
                        }
                        else if (S.Name != null)
                        {
                            p7.Add(S.Name);
                        }
                        else if (S.ToString() != null)
                        {
                            p7.Add(S.ToString());
                        }
                    }
                }
                else if (plancnt == 5)
                {
                    foreach (Structure S in aplan.StructureSet.Structures)
                    {
                        if (S.Id != null)
                        {
                            p8.Add(S.Id);
                        }
                        else if (S.Name != null)
                        {
                            p8.Add(S.Name);
                        }
                        else if (S.ToString() != null)
                        {
                            p8.Add(S.ToString());
                        }
                    }
                }

                MessageBox.Show("Trig 3.5");
            }






            




            //GUI starts here


            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            MessageBox.Show("Trig 4");

            //Starts GUI for Dose objective check in a separate thread
            System.Windows.Forms.Application.Run(new NTCPcalc.GUI(Plansums, Plans,p1,p2,p3,p4,p5,p6,p7,p8));

            MessageBox.Show("Trig End");


        }










    }


}