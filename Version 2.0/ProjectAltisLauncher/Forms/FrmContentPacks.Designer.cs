﻿namespace ProjectAltisLauncher.Forms
{
    partial class FrmContentPacks
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
            this.grpContentPacks = new System.Windows.Forms.GroupBox();
            this.lstPacks = new System.Windows.Forms.ListBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMoveItemDown = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnMoveItemUp = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.grpContentPacks.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpContentPacks
            // 
            this.grpContentPacks.Controls.Add(this.lstPacks);
            this.grpContentPacks.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpContentPacks.ForeColor = System.Drawing.Color.White;
            this.grpContentPacks.Location = new System.Drawing.Point(12, 14);
            this.grpContentPacks.Name = "grpContentPacks";
            this.grpContentPacks.Size = new System.Drawing.Size(394, 185);
            this.grpContentPacks.TabIndex = 7;
            this.grpContentPacks.TabStop = false;
            this.grpContentPacks.Text = "Content Packs";
            // 
            // lstPacks
            // 
            this.lstPacks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPacks.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPacks.FormattingEnabled = true;
            this.lstPacks.ItemHeight = 17;
            this.lstPacks.Location = new System.Drawing.Point(3, 18);
            this.lstPacks.Name = "lstPacks";
            this.lstPacks.Size = new System.Drawing.Size(388, 164);
            this.lstPacks.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(287, 234);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(119, 23);
            this.btnApply.TabIndex = 13;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(13, 234);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMoveItemDown
            // 
            this.btnMoveItemDown.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveItemDown.Location = new System.Drawing.Point(150, 234);
            this.btnMoveItemDown.Name = "btnMoveItemDown";
            this.btnMoveItemDown.Size = new System.Drawing.Size(119, 23);
            this.btnMoveItemDown.TabIndex = 11;
            this.btnMoveItemDown.Text = "Move Item Down";
            this.btnMoveItemDown.UseVisualStyleBackColor = true;
            this.btnMoveItemDown.Click += new System.EventHandler(this.btnMoveItemDown_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(287, 205);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(119, 23);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnMoveItemUp
            // 
            this.btnMoveItemUp.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveItemUp.Location = new System.Drawing.Point(150, 205);
            this.btnMoveItemUp.Name = "btnMoveItemUp";
            this.btnMoveItemUp.Size = new System.Drawing.Size(119, 23);
            this.btnMoveItemUp.TabIndex = 9;
            this.btnMoveItemUp.Text = "Move Item Up";
            this.btnMoveItemUp.UseVisualStyleBackColor = true;
            this.btnMoveItemUp.Click += new System.EventHandler(this.btnMoveItemUp_Click);
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(13, 205);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(119, 23);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // FrmContentPacks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(418, 278);
            this.Controls.Add(this.grpContentPacks);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnMoveItemDown);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnMoveItemUp);
            this.Controls.Add(this.btnImport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmContentPacks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Content Packs";
            this.Load += new System.EventHandler(this.FrmContentPacks_Load);
            this.grpContentPacks.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpContentPacks;
        private System.Windows.Forms.ListBox lstPacks;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMoveItemDown;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnMoveItemUp;
        private System.Windows.Forms.Button btnImport;
    }
}