﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
    Liver Only Normal Tissue Complication Probability (NTCP) Calculator - ESAPI 15.6 version (3/16/2020).

    This is the source code for the Windows Forms GUI used in the Liver Only Normal Tissue Complication Probability (NTCP) Calculator program. It is kept as a separate file from the main file.
    This is specifically the Windows Forms Designer code automatically generated by Visual Studio, however I have edited some of it to get the program to work as intended.

    This program is expressely written as a plug-in script for use with Varian's Eclipse Treatment Planning System, and requires Varian's API files to run properly.
    This program also requires .NET Framework 4.5.0 to run properly, and the MathNet.Numerics class library package, which is freely availible on the NuGet Package manager in Visual Studio.
    The MathNet.Numerics package contains an Error function method which is used in the NTCP calculation.

    Copyright (C) 2020 Zackary Thomas Ricci Morelli
    
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
                               zackary.t.morelli@lahey.org


*/

namespace NTCPcalc
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.Directions = new System.Windows.Forms.TextBox();
            this.PlanList = new System.Windows.Forms.ListBox();
            this.ExecuteCalc = new System.Windows.Forms.Button();
            this.VeffOut = new System.Windows.Forms.TextBox();
            this.NTCPout = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Nlabel = new System.Windows.Forms.Label();
            this.Vlabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Directions
            // 
            this.Directions.Font = new System.Drawing.Font("Goudy Old Style", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Directions.Location = new System.Drawing.Point(12, 12);
            this.Directions.Multiline = true;
            this.Directions.Name = "Directions";
            this.Directions.ReadOnly = true;
            this.Directions.Size = new System.Drawing.Size(815, 117);
            this.Directions.TabIndex = 0;
            this.Directions.Text = resources.GetString("Directions.Text");
            // 
            // PlanList
            // 
            this.PlanList.Font = new System.Drawing.Font("Goudy Old Style", 16F);
            this.PlanList.ForeColor = System.Drawing.Color.Black;
            this.PlanList.FormattingEnabled = true;
            this.PlanList.ItemHeight = 25;
            this.PlanList.Location = new System.Drawing.Point(12, 141);
            this.PlanList.Name = "PlanList";
            this.PlanList.Size = new System.Drawing.Size(322, 204);
            this.PlanList.TabIndex = 2;
            this.PlanList.SelectedIndexChanged += new System.EventHandler(this.PlanList_SelectedIndexChanged);
            // 
            // ExecuteCalc
            // 
            this.ExecuteCalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.ExecuteCalc.Location = new System.Drawing.Point(530, 162);
            this.ExecuteCalc.Name = "ExecuteCalc";
            this.ExecuteCalc.Size = new System.Drawing.Size(151, 86);
            this.ExecuteCalc.TabIndex = 4;
            this.ExecuteCalc.Text = "Execute NTCP Calculation";
            this.ExecuteCalc.UseVisualStyleBackColor = true;
            this.ExecuteCalc.Click += new System.EventHandler(this.ExecuteCalc_Click);
            // 
            // VeffOut
            // 
            this.VeffOut.BackColor = System.Drawing.SystemColors.Window;
            this.VeffOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.VeffOut.Location = new System.Drawing.Point(435, 315);
            this.VeffOut.Name = "VeffOut";
            this.VeffOut.ReadOnly = true;
            this.VeffOut.Size = new System.Drawing.Size(113, 35);
            this.VeffOut.TabIndex = 5;
            // 
            // NTCPout
            // 
            this.NTCPout.BackColor = System.Drawing.SystemColors.Window;
            this.NTCPout.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.NTCPout.Location = new System.Drawing.Point(696, 316);
            this.NTCPout.Name = "NTCPout";
            this.NTCPout.ReadOnly = true;
            this.NTCPout.Size = new System.Drawing.Size(130, 35);
            this.NTCPout.TabIndex = 6;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Goudy Old Style", 16F);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Items.AddRange(new object[] {
            "HCC",
            "METS"});
            this.listBox1.Location = new System.Drawing.Point(385, 194);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(80, 54);
            this.listBox1.TabIndex = 9;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Nlabel
            // 
            this.Nlabel.AutoSize = true;
            this.Nlabel.Font = new System.Drawing.Font("Goudy Old Style", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nlabel.Location = new System.Drawing.Point(607, 320);
            this.Nlabel.Name = "Nlabel";
            this.Nlabel.Size = new System.Drawing.Size(83, 31);
            this.Nlabel.TabIndex = 10;
            this.Nlabel.Text = "NTCP";
            // 
            // Vlabel
            // 
            this.Vlabel.AutoSize = true;
            this.Vlabel.Font = new System.Drawing.Font("Goudy Old Style", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Vlabel.Location = new System.Drawing.Point(370, 321);
            this.Vlabel.Name = "Vlabel";
            this.Vlabel.Size = new System.Drawing.Size(59, 31);
            this.Vlabel.TabIndex = 11;
            this.Vlabel.Text = "Veff";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 363);
            this.Controls.Add(this.Vlabel);
            this.Controls.Add(this.Nlabel);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.NTCPout);
            this.Controls.Add(this.VeffOut);
            this.Controls.Add(this.ExecuteCalc);
            this.Controls.Add(this.PlanList);
            this.Controls.Add(this.Directions);
            this.Name = "GUI";
            this.Text = "Normal Tissue Complication Probability Calculator (Liver Only)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Directions;
        private System.Windows.Forms.ListBox PlanList;
        private System.Windows.Forms.Button ExecuteCalc;
        private System.Windows.Forms.TextBox VeffOut;
        private System.Windows.Forms.TextBox NTCPout;
        private ListBox listBox1;
        private Label Nlabel;
        private Label Vlabel;
    }
}