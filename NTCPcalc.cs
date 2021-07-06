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
    Liver Only Normal Tissue Complication Probability (NTCP) Calculator - ESAPI 16.0 version (3/16/2020)
    This is the start-up program for the NTCP calc plug-in script. It calls the Execute function to start the script and then starts a WinForms GUI where the rest fo the program takes place. 
    This program is expressely written as a plug-in script for use with Varian's Eclipse Treatment Planning System, and requires Varian's API files to run properly.
    This program requires .NET Framework 4.6.1, and the MathNet.Numerics class library package, which is freely availible on the NuGet Package manager in Visual Studio.
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

//The NTCP Calc script starts here 


namespace VMS.TPS
{
    public class Script  // creates a class called Script within the VMS.TPS Namesapce
    {

       public Script() { }  // instantiates a Script class


        // Global Variable Declaration

       public String pl = null;

      // Execution begins with the "Execute" function.

        public void Execute(ScriptContext context)     // PROGRAM START - sending a return to Execute will end the program
        {

          //  MessageBox.Show("Trig 1");

            IEnumerable<PlanSum> Plansums = context.PlanSumsInScope;
            IEnumerable<PlanSetup> Plans = context.PlansInScope;

            if (context.Patient == null)
            {
                MessageBox.Show("Please load a patient with a treatment plan before running this script!");
                return;
            }
<<<<<<< HEAD
            
=======

            //this snippet generates lists of the structures for every plan and plansum that might be loaded in. these lists are then used to populate the Organ list dropdown in the GUI.

            foreach (PlanSum aplansum in Plansums)
            {
                if (aplansum.Id == "motion assess" || aplansum.Id == "Motion Assess" || aplansum.Id == "Mot Assess" || aplansum.Id == "mot assess")
                { 
                   // MessageBox.Show("Trig Sum cHECK");
                    sumcnt++;
                    continue;
                }
                sumcnt++;
               // MessageBox.Show("Trig 2");
                if (sumcnt == 1)
                {
                    foreach (Structure S in aplansum.StructureSet.Structures)
                    {
                        if (S.Volume < 0.03)
                        {
                            continue;
                        }
                        else if (S.Id != null)
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
                        if (S.Volume < 0.03)
                        {
                            continue;
                        }
                        else if (S.Id != null)
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
                        if (S.Volume < 0.03)
                        {
                            continue;
                        }
                        else if (S.Id != null)
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
                if(aplan.Id == "motion assess" || aplan.Id == "Motion Assess" || aplan.Id == "Mot Assess" || aplan.Id == "mot assess")
                {
                   // MessageBox.Show("Trig Plan cHECK");
                    plancnt++;
                    continue;
                }

                plancnt++;
              //  MessageBox.Show("Trig 3");
                if (plancnt == 1)
                {
                    foreach (Structure S in aplan.StructureSet.Structures)
                    {
                        if (S.Volume < 0.03)
                        {
                            continue;
                        }
                        else if (S.Id != null)
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
                        if (S.Volume < 0.03)
                        {
                            continue;
                        }
                        else if (S.Id != null)
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
                        if (S.Volume < 0.03)
                        {
                            continue;
                        }
                        else if (S.Id != null)
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
                        if (S.Volume < 0.03)
                        {
                            continue;
                        }
                        else if (S.Id != null)
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
                        if (S.Volume < 0.03)
                        {
                            continue;
                        }
                        else if (S.Id != null)
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

               // MessageBox.Show("Trig 3.5");
            }                                                       

>>>>>>> master
            //GUI starts here
            //MessageBox.Show("Trig 4");

            System.Windows.Forms.Application.EnableVisualStyles();
<<<<<<< HEAD
           // System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);      //This method breaks the script when it runs multiple times, because it will throw an exception if a window has already been created.
            Start(Plansums, Plans );                                                           //The Windows .NET documentation specifically says this method should NOT be called in a Windows Forms program hosted in another application, like this, so it is ommitted
                                                                                            //It is a legacy method anyway from eary versions of .NET, there should be no need to call it.
=======

           // MessageBox.Show("Trig 4");

            //Starts GUI for Dose objective check in a separate thread
            System.Windows.Forms.Application.Run(new NTCPcalc.GUI(Plansums, Plans,p1,p2,p3,p4,p5,p6,p7,p8));
>>>>>>> master

            // MessageBox.Show("Trig End");
        }

        public static void Start(IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans )
        {
            System.Windows.Forms.Application.Run(new NTCPcalc.GUI(Plansums, Plans ));
        }

    }
}