namespace SNAT.Comman_Form
{
    partial class frmImportExcel
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rbMemberID = new System.Windows.Forms.RadioButton();
            this.rbEmployeeNo = new System.Windows.Forms.RadioButton();
            this.lbltext = new System.Windows.Forms.Label();
            this.txtReadRow = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.rbAdvance = new System.Windows.Forms.RadioButton();
            this.rbStandread = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SlateGray;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Import Excel Data . . . ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.lbltext);
            this.panel1.Controls.Add(this.txtReadRow);
            this.panel1.Controls.Add(this.lblMsg);
            this.panel1.Controls.Add(this.rbAdvance);
            this.panel1.Controls.Add(this.rbStandread);
            this.panel1.Location = new System.Drawing.Point(1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 201);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.rbMemberID);
            this.panel3.Controls.Add(this.rbEmployeeNo);
            this.panel3.Location = new System.Drawing.Point(47, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(228, 67);
            this.panel3.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DimGray;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Link On . . .";
            // 
            // rbMemberID
            // 
            this.rbMemberID.AutoSize = true;
            this.rbMemberID.Location = new System.Drawing.Point(8, 41);
            this.rbMemberID.Name = "rbMemberID";
            this.rbMemberID.Size = new System.Drawing.Size(80, 17);
            this.rbMemberID.TabIndex = 6;
            this.rbMemberID.Text = "Member ID.";
            this.rbMemberID.UseVisualStyleBackColor = true;
            // 
            // rbEmployeeNo
            // 
            this.rbEmployeeNo.AutoSize = true;
            this.rbEmployeeNo.Checked = true;
            this.rbEmployeeNo.Location = new System.Drawing.Point(8, 22);
            this.rbEmployeeNo.Name = "rbEmployeeNo";
            this.rbEmployeeNo.Size = new System.Drawing.Size(91, 17);
            this.rbEmployeeNo.TabIndex = 5;
            this.rbEmployeeNo.TabStop = true;
            this.rbEmployeeNo.Text = "Employee No.";
            this.rbEmployeeNo.UseVisualStyleBackColor = true;
            // 
            // lbltext
            // 
            this.lbltext.AutoSize = true;
            this.lbltext.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltext.ForeColor = System.Drawing.Color.Blue;
            this.lbltext.Location = new System.Drawing.Point(58, 36);
            this.lbltext.Name = "lbltext";
            this.lbltext.Size = new System.Drawing.Size(208, 14);
            this.lbltext.TabIndex = 4;
            this.lbltext.Text = "Please enter data read row no in textbox.";
            this.lbltext.Visible = false;
            // 
            // txtReadRow
            // 
            this.txtReadRow.Location = new System.Drawing.Point(181, 13);
            this.txtReadRow.Name = "txtReadRow";
            this.txtReadRow.Size = new System.Drawing.Size(94, 20);
            this.txtReadRow.TabIndex = 3;
            this.txtReadRow.Visible = false;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(10, 151);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(38, 14);
            this.lblMsg.TabIndex = 2;
            this.lblMsg.Text = "label2";
            this.lblMsg.Visible = false;
            // 
            // rbAdvance
            // 
            this.rbAdvance.AutoSize = true;
            this.rbAdvance.Location = new System.Drawing.Point(47, 49);
            this.rbAdvance.Name = "rbAdvance";
            this.rbAdvance.Size = new System.Drawing.Size(121, 17);
            this.rbAdvance.TabIndex = 1;
            this.rbAdvance.TabStop = true;
            this.rbAdvance.Text = "Advance Process ...";
            this.rbAdvance.UseVisualStyleBackColor = true;
            // 
            // rbStandread
            // 
            this.rbStandread.AutoSize = true;
            this.rbStandread.Location = new System.Drawing.Point(47, 12);
            this.rbStandread.Name = "rbStandread";
            this.rbStandread.Size = new System.Drawing.Size(127, 17);
            this.rbStandread.TabIndex = 0;
            this.rbStandread.TabStop = true;
            this.rbStandread.Text = "Standread Process ...";
            this.rbStandread.UseVisualStyleBackColor = true;
            this.rbStandread.CheckedChanged += new System.EventHandler(this.rbStandread_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SlateGray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 228);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(338, 40);
            this.panel2.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::SNAT.Properties.Resources.delete_logo_icon;
            this.btnCancel.Location = new System.Drawing.Point(240, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 33);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Image = global::SNAT.Properties.Resources.button_ok;
            this.btnOk.Location = new System.Drawing.Point(140, 1);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(94, 33);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmImportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 268);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportExcel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.RadioButton rbAdvance;
        private System.Windows.Forms.RadioButton rbStandread;
        private System.Windows.Forms.TextBox txtReadRow;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbltext;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbMemberID;
        private System.Windows.Forms.RadioButton rbEmployeeNo;
    }
}