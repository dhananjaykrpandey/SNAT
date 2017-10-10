namespace SNAT.Document
{
    partial class frmChangePassword
    {

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
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnsave = new Telerik.WinControls.UI.RadButton();
            this.txtoldPassword = new Telerik.WinControls.UI.RadTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtconfirmpassword = new Telerik.WinControls.UI.RadTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtpassword = new Telerik.WinControls.UI.RadTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserId = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnsave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtoldPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtconfirmpassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::SNAT.Properties.Resources.delete_logo_icon;
            this.btnCancel.Location = new System.Drawing.Point(226, 135);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 33);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnsave
            // 
            this.btnsave.Image = global::SNAT.Properties.Resources.Save_icon;
            this.btnsave.Location = new System.Drawing.Point(130, 135);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(90, 33);
            this.btnsave.TabIndex = 4;
            this.btnsave.Text = "Save";
            this.btnsave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // txtoldPassword
            // 
            this.txtoldPassword.Location = new System.Drawing.Point(130, 41);
            this.txtoldPassword.MaxLength = 20;
            this.txtoldPassword.Name = "txtoldPassword";
            this.txtoldPassword.NullText = "Enter user id old password";
            this.txtoldPassword.PasswordChar = '+';
            this.txtoldPassword.Size = new System.Drawing.Size(217, 20);
            this.txtoldPassword.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 67;
            this.label3.Text = "Old Password";
            // 
            // txtconfirmpassword
            // 
            this.txtconfirmpassword.Location = new System.Drawing.Point(130, 94);
            this.txtconfirmpassword.MaxLength = 20;
            this.txtconfirmpassword.Name = "txtconfirmpassword";
            this.txtconfirmpassword.NullText = "Re-enter user id  password";
            this.txtconfirmpassword.PasswordChar = '+';
            this.txtconfirmpassword.Size = new System.Drawing.Size(217, 20);
            this.txtconfirmpassword.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(21, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "Confirm Password";
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(130, 68);
            this.txtpassword.MaxLength = 20;
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.NullText = "Enter user id password";
            this.txtpassword.PasswordChar = '+';
            this.txtpassword.Size = new System.Drawing.Size(217, 20);
            this.txtpassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "New Password";
            // 
            // txtUserId
            // 
            this.txtUserId.Enabled = false;
            this.txtUserId.Location = new System.Drawing.Point(130, 15);
            this.txtUserId.MaxLength = 20;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.NullText = "Enter User Id";
            this.txtUserId.ReadOnly = true;
            this.txtUserId.Size = new System.Drawing.Size(217, 20);
            this.txtUserId.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 63;
            this.label1.Text = "User ID";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 182);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.txtoldPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtconfirmpassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.label1);
            this.Name = "frmChangePassword";
            this.Text = "frmChangePassword";
            this.Load += new System.EventHandler(this.frmChangePassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnsave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtoldPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtconfirmpassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadButton btnsave;
        private Telerik.WinControls.UI.RadTextBox txtoldPassword;
        public System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadTextBox txtconfirmpassword;
        public System.Windows.Forms.Label label6;
        private Telerik.WinControls.UI.RadTextBox txtpassword;
        public System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadTextBox txtUserId;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.ComponentModel.IContainer components;
    }
}