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
            
            //GUI starts here
            //MessageBox.Show("Trig 4");

            System.Windows.Forms.Application.EnableVisualStyles();
           // System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);      //This method breaks the script when it runs multiple times, because it will throw an exception if a window has already been created.
            Start(Plansums, Plans );                                                           //The Windows .NET documentation specifically says this method should NOT be called in a Windows Forms program hosted in another application, like this, so it is ommitted
                                                                                            //It is a legacy method anyway from eary versions of .NET, there should be no need to call it.

            // MessageBox.Show("Trig End");
        }

        public static void Start(IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans )
        {
            System.Windows.Forms.Application.Run(new NTCPcalc.GUI(Plansums, Plans ));
        }

    }
}