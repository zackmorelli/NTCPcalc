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
            this.OrganList = new System.Windows.Forms.ListBox();
            this.ExecuteCalc = new System.Windows.Forms.Button();
            this.VeffOut = new System.Windows.Forms.TextBox();
            this.NTCPout = new System.Windows.Forms.TextBox();
            this.nbox = new System.Windows.Forms.TextBox();
            this.mbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Directions
            // 
            this.Directions.Font = new System.Drawing.Font("Goudy Old Style", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Directions.Location = new System.Drawing.Point(12, 12);
            this.Directions.Multiline = true;
            this.Directions.Name = "Directions";
            this.Directions.ReadOnly = true;
            this.Directions.Size = new System.Drawing.Size(744, 125);
            this.Directions.TabIndex = 0;
            this.Directions.Text = resources.GetString("Directions.Text");
            // 
            // Directions2
            // 
            this.Directions2.Font = new System.Drawing.Font("Goudy Old Style", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Directions2.Location = new System.Drawing.Point(12, 156);
            this.Directions2.Multiline = true;
            this.Directions2.Name = "Directions2";
            this.Directions2.ReadOnly = true;
            this.Directions2.Size = new System.Drawing.Size(744, 121);
            this.Directions2.TabIndex = 1;
            this.Directions2.Text = resources.GetString("Directions2.Text");
            // 
            // PlanList
            // 
            this.PlanList.Font = new System.Drawing.Font("Goudy Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlanList.ForeColor = System.Drawing.Color.Black;
            this.PlanList.FormattingEnabled = true;
            this.PlanList.ItemHeight = 21;
            this.PlanList.Location = new System.Drawing.Point(12, 295);
            this.PlanList.Name = "PlanList";
            this.PlanList.Size = new System.Drawing.Size(322, 88);
            this.PlanList.TabIndex = 2;
            this.PlanList.SelectedIndexChanged += new System.EventHandler(this.PlanList_SelectedIndexChanged);
            // 
            // OrganList
            // 
            this.OrganList.Font = new System.Drawing.Font("Goudy Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrganList.ForeColor = System.Drawing.Color.Black;
            this.OrganList.FormattingEnabled = true;
            this.OrganList.ItemHeight = 21;
            this.OrganList.Location = new System.Drawing.Point(12, 400);
            this.OrganList.Name = "OrganList";
            this.OrganList.Size = new System.Drawing.Size(714, 193);
            this.OrganList.TabIndex = 3;
            this.OrganList.SelectedIndexChanged += new System.EventHandler(this.OrganList_SelectedIndexChanged);
            // 
            // ExecuteCalc
            // 
            this.ExecuteCalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecuteCalc.Location = new System.Drawing.Point(612, 295);
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
            this.VeffOut.Location = new System.Drawing.Point(12, 620);
            this.VeffOut.Multiline = true;
            this.VeffOut.Name = "VeffOut";
            this.VeffOut.ReadOnly = true;
            this.VeffOut.Size = new System.Drawing.Size(302, 49);
            this.VeffOut.TabIndex = 5;
            // 
            // NTCPout
            // 
            this.NTCPout.BackColor = System.Drawing.SystemColors.Window;
            this.NTCPout.Font = new System.Drawing.Font("Goudy Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NTCPout.Location = new System.Drawing.Point(331, 620);
            this.NTCPout.Multiline = true;
            this.NTCPout.Name = "NTCPout";
            this.NTCPout.ReadOnly = true;
            this.NTCPout.Size = new System.Drawing.Size(414, 49);
            this.NTCPout.TabIndex = 6;
            // 
            // nbox
            // 
            this.nbox.Font = new System.Drawing.Font("Goudy Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbox.Location = new System.Drawing.Point(351, 295);
            this.nbox.Name = "nbox";
            this.nbox.Size = new System.Drawing.Size(246, 27);
            this.nbox.TabIndex = 7;
            this.nbox.Text = "Volume Effect Parameter n: ";
            // 
            // mbox
            // 
            this.mbox.Font = new System.Drawing.Font("Goudy Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbox.Location = new System.Drawing.Point(351, 335);
            this.mbox.Name = "mbox";
            this.mbox.Size = new System.Drawing.Size(246, 27);
            this.mbox.TabIndex = 8;
            this.mbox.Text = "Slope Parameter m: ";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 681);
            this.Controls.Add(this.mbox);
            this.Controls.Add(this.nbox);
            this.Controls.Add(this.NTCPout);
            this.Controls.Add(this.VeffOut);
            this.Controls.Add(this.ExecuteCalc);
            this.Controls.Add(this.OrganList);
            this.Controls.Add(this.PlanList);
            this.Controls.Add(this.Directions2);
            this.Controls.Add(this.Directions);
            this.Name = "GUI";
            this.Text = "Normal Tissue Complication Probability Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Directions;
        private System.Windows.Forms.TextBox Directions2;
        private System.Windows.Forms.ListBox PlanList;
        private System.Windows.Forms.ListBox OrganList;
        private System.Windows.Forms.Button ExecuteCalc;
        private System.Windows.Forms.TextBox VeffOut;
        private System.Windows.Forms.TextBox NTCPout;
        private TextBox nbox;
        private TextBox mbox;
    }
}