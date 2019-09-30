using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



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
            this.Directions2 = new System.Windows.Forms.TextBox();
            this.PlanList = new System.Windows.Forms.ListBox();
            this.ExecuteCalc = new System.Windows.Forms.Button();
            this.VeffOut = new System.Windows.Forms.TextBox();
            this.NTCPout = new System.Windows.Forms.TextBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // Directions
            // 
            this.Directions.Font = new System.Drawing.Font("Goudy Old Style", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Directions.Location = new System.Drawing.Point(12, 12);
            this.Directions.Multiline = true;
            this.Directions.Name = "Directions";
            this.Directions.ReadOnly = true;
            this.Directions.Size = new System.Drawing.Size(815, 93);
            this.Directions.TabIndex = 0;
            this.Directions.Text = resources.GetString("Directions.Text");
            // 
            // Directions2
            // 
            this.Directions2.Font = new System.Drawing.Font("Goudy Old Style", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Directions2.Location = new System.Drawing.Point(12, 120);
            this.Directions2.Multiline = true;
            this.Directions2.Name = "Directions2";
            this.Directions2.ReadOnly = true;
            this.Directions2.Size = new System.Drawing.Size(815, 95);
            this.Directions2.TabIndex = 1;
            this.Directions2.Text = resources.GetString("Directions2.Text");
            // 
            // PlanList
            // 
            this.PlanList.Font = new System.Drawing.Font("Goudy Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlanList.ForeColor = System.Drawing.Color.Black;
            this.PlanList.FormattingEnabled = true;
            this.PlanList.ItemHeight = 21;
            this.PlanList.Location = new System.Drawing.Point(12, 233);
            this.PlanList.Name = "PlanList";
            this.PlanList.Size = new System.Drawing.Size(322, 88);
            this.PlanList.TabIndex = 2;
            this.PlanList.SelectedIndexChanged += new System.EventHandler(this.PlanList_SelectedIndexChanged);
            // 
            // ExecuteCalc
            // 
            this.ExecuteCalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecuteCalc.Location = new System.Drawing.Point(524, 233);
            this.ExecuteCalc.Name = "ExecuteCalc";
            this.ExecuteCalc.Size = new System.Drawing.Size(133, 70);
            this.ExecuteCalc.TabIndex = 4;
            this.ExecuteCalc.Text = "Execute NTCP Calculation";
            this.ExecuteCalc.UseVisualStyleBackColor = true;
            this.ExecuteCalc.Click += new System.EventHandler(this.ExecuteCalc_Click);
            // 
            // VeffOut
            // 
            this.VeffOut.BackColor = System.Drawing.SystemColors.Window;
            this.VeffOut.Font = new System.Drawing.Font("Goudy Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VeffOut.Location = new System.Drawing.Point(12, 349);
            this.VeffOut.Multiline = true;
            this.VeffOut.Name = "VeffOut";
            this.VeffOut.ReadOnly = true;
            this.VeffOut.Size = new System.Drawing.Size(322, 35);
            this.VeffOut.TabIndex = 5;
            // 
            // NTCPout
            // 
            this.NTCPout.BackColor = System.Drawing.SystemColors.Window;
            this.NTCPout.Font = new System.Drawing.Font("Goudy Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NTCPout.Location = new System.Drawing.Point(340, 349);
            this.NTCPout.Multiline = true;
            this.NTCPout.Name = "NTCPout";
            this.NTCPout.ReadOnly = true;
            this.NTCPout.Size = new System.Drawing.Size(490, 35);
            this.NTCPout.TabIndex = 6;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Font = new System.Drawing.Font("Goudy Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "HCC",
            "METS"});
            this.checkedListBox1.Location = new System.Drawing.Point(365, 233);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(80, 48);
            this.checkedListBox1.TabIndex = 9;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 402);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.NTCPout);
            this.Controls.Add(this.VeffOut);
            this.Controls.Add(this.ExecuteCalc);
            this.Controls.Add(this.PlanList);
            this.Controls.Add(this.Directions2);
            this.Controls.Add(this.Directions);
            this.Name = "GUI";
            this.Text = "Normal Tissue Complication Probability Calculator (Liver Only)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Directions;
        private System.Windows.Forms.TextBox Directions2;
        private System.Windows.Forms.ListBox PlanList;
        private System.Windows.Forms.Button ExecuteCalc;
        private System.Windows.Forms.TextBox VeffOut;
        private System.Windows.Forms.TextBox NTCPout;
        private CheckedListBox checkedListBox1;
    }
}