namespace Checkers.Forms
{
    partial class ProfileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileForm));
            paneldark = new Panel();
            btnChangeLanguage = new Button();
            lblLogin = new Label();
            pnlLight = new Panel();
            pctruser = new PictureBox();
            btnback = new Button();
            lblprofile = new Label();
            paneldark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctruser).BeginInit();
            SuspendLayout();
            // 
            // paneldark
            // 
            paneldark.BackColor = Color.FromArgb(40, 20, 0);
            paneldark.Controls.Add(btnChangeLanguage);
            paneldark.Controls.Add(lblLogin);
            paneldark.Controls.Add(pnlLight);
            paneldark.Controls.Add(pctruser);
            paneldark.Controls.Add(btnback);
            paneldark.Controls.Add(lblprofile);
            paneldark.Location = new Point(342, 36);
            paneldark.Name = "paneldark";
            paneldark.Size = new Size(506, 571);
            paneldark.TabIndex = 0;
            // 
            // btnChangeLanguage
            // 
            btnChangeLanguage.BackColor = Color.FromArgb(0, 64, 0);
            btnChangeLanguage.FlatStyle = FlatStyle.Popup;
            btnChangeLanguage.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnChangeLanguage.ForeColor = Color.White;
            btnChangeLanguage.Location = new Point(134, 451);
            btnChangeLanguage.Name = "btnChangeLanguage";
            btnChangeLanguage.Size = new Size(229, 42);
            btnChangeLanguage.TabIndex = 3;
            btnChangeLanguage.Text = "Сменить язык ";
            btnChangeLanguage.UseVisualStyleBackColor = false;
            btnChangeLanguage.Click += btnChangeLanguage_Click;
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Font = new Font("Segoe UI", 12F);
            lblLogin.ForeColor = Color.White;
            lblLogin.Location = new Point(211, 185);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(81, 32);
            lblLogin.TabIndex = 0;
            lblLogin.Text = "Логин";
            // 
            // pnlLight
            // 
            pnlLight.BackColor = Color.FromArgb(100, 64, 44);
            pnlLight.Location = new Point(89, 232);
            pnlLight.Name = "pnlLight";
            pnlLight.Size = new Size(329, 195);
            pnlLight.TabIndex = 1;
            // 
            // pctruser
            // 
            pctruser.BackgroundImage = (Image)resources.GetObject("pctruser.BackgroundImage");
            pctruser.BackgroundImageLayout = ImageLayout.Stretch;
            pctruser.Location = new Point(188, 57);
            pctruser.Name = "pctruser";
            pctruser.Size = new Size(129, 116);
            pctruser.TabIndex = 2;
            pctruser.TabStop = false;
            // 
            // btnback
            // 
            btnback.BackColor = Color.Navy;
            btnback.FlatStyle = FlatStyle.Popup;
            btnback.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnback.ForeColor = Color.White;
            btnback.Location = new Point(188, 509);
            btnback.Name = "btnback";
            btnback.Size = new Size(129, 42);
            btnback.TabIndex = 1;
            btnback.Text = "Назад";
            btnback.UseVisualStyleBackColor = false;
            btnback.Click += btnback_Click;
            // 
            // lblprofile
            // 
            lblprofile.AutoSize = true;
            lblprofile.Font = new Font("Arial Rounded MT Bold", 19F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblprofile.ForeColor = Color.FromArgb(226, 199, 153);
            lblprofile.Location = new Point(163, 0);
            lblprofile.Name = "lblprofile";
            lblprofile.Size = new Size(182, 44);
            lblprofile.TabIndex = 0;
            lblprofile.Text = "Профиль";
            // 
            // ProfileForm
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Tan;
            ClientSize = new Size(1206, 635);
            Controls.Add(paneldark);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ProfileForm";
            Text = "ProfileForm";
            paneldark.ResumeLayout(false);
            paneldark.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pctruser).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel paneldark;
        private PictureBox pctruser;
        private Button btnback;
        private Label lblprofile;
        private Label lblLogin;
        private Panel pnlLight;
        private Button btnChangeLanguage;
    }
}