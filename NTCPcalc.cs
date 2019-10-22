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

       // Thread Prog = new Thread(Script());

        public void Execute(ScriptContext context)     // PROGRAM START - sending a return to Execute will end the program
        {

          //  MessageBox.Show("Trig 1");
            //Variable declaration space

            IEnumerable<PlanSum> Plansums = context.PlanSumsInScope;
            IEnumerable<PlanSetup> Plans = context.PlansInScope;

            // start of actual code

            //  MessageBox.Show("Trig 1");
            if (context.Patient == null)
            {
                MessageBox.Show("Please load a patient with a treatment plan before running this script!");
                return;
            }
            

            //GUI starts here

           //  MessageBox.Show("Trig 4");

            System.Windows.Forms.Application.EnableVisualStyles();
           // System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);      //This method breaks the script when it runs multiple times, because it will throw an exception if a window has already been created.
            Start(Plansums, Plans );                            //The Windows .NET documentation specifically says this method should NOT be called in a Windows Forms program hosted in another application, like this, so it is ommitted
                                                                                            //It is a legacy method anyway from eary versions of .NET, there should be no need to call it.

            //Starts GUI for Dose objective check in a separate thread
            //  Thread GUI = new Thread(new ThreadStart(Go));

            // MessageBox.Show("Trig End");


        }

        public static void Start(IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans )
        {
            System.Windows.Forms.Application.Run(new NTCPcalc.GUI(Plansums, Plans ));
        }

        //  static void Go (IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans, List<string> p1, List<string> p2, List<string> p3, List<string> p4, List<string> p5, List<string> p6, List<string> p7, List<string> p8)
        //  {
        //      System.Windows.Forms.Application.Run(new NTCPcalc.GUI(Plansums, Plans, p1, p2, p3, p4, p5, p6, p7, p8));
        //   }
                     


    }
}