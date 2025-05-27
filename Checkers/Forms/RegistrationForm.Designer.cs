using Checkers.Properties;

namespace Checkers.Forms
{
    partial class RegistrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrationForm));
            lblTitle = new Label();
            txtLogin = new TextBox();
            txtPassword = new TextBox();
            txtRepeatpassword = new TextBox();
            btnRegister = new Button();
            btnBack = new Button();
            lightpanel = new Panel();
            btnTogglePassword2 = new Button();
            btnUploudphoto = new Button();
            btnTogglePassword1 = new Button();
            pctrBoxProfile = new PictureBox();
            lightpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctrBoxProfile).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.BackColor = Color.FromArgb(65, 35, 20);
            lblTitle.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblTitle.ForeColor = Color.FromArgb(226, 199, 153);
            lblTitle.Location = new Point(260, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(522, 55);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Шашки Онлайн";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtLogin
            // 
            txtLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtLogin.Font = new Font("Arial Rounded MT Bold", 14F);
            txtLogin.ForeColor = Color.Black;
            txtLogin.Location = new Point(65, 82);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(389, 40);
            txtLogin.TabIndex = 1;
            txtLogin.Text = "Логин";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Font = new Font("Arial", 14F, FontStyle.Bold);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Location = new Point(65, 155);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(389, 40);
            txtPassword.TabIndex = 2;
            txtPassword.Text = "Пароль";
            // 
            // txtRepeatpassword
            // 
            txtRepeatpassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRepeatpassword.Font = new Font("Arial", 14F, FontStyle.Bold);
            txtRepeatpassword.ForeColor = Color.Black;
            txtRepeatpassword.Location = new Point(65, 258);
            txtRepeatpassword.Name = "txtRepeatpassword";
            txtRepeatpassword.Size = new Size(389, 40);
            txtRepeatpassword.TabIndex = 3;
            txtRepeatpassword.Text = "Подтверждение пароля";
            // 
            // btnRegister
            // 
            btnRegister.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnRegister.BackColor = Color.FromArgb(0, 64, 0);
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Arial", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(363, 340);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(312, 46);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "Зарегистрироваться";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += BtnRegister_Click;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnBack.BackColor = Color.FromArgb(0, 0, 128);
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Arial", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(363, 414);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(312, 46);
            btnBack.TabIndex = 5;
            btnBack.Text = "Назад";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += BtnBack_Click;
            // 
            // lightpanel
            // 
            lightpanel.Anchor = AnchorStyles.None;
            lightpanel.BackColor = Color.FromArgb(65, 35, 20);
            lightpanel.Controls.Add(btnTogglePassword2);
            lightpanel.Controls.Add(btnUploudphoto);
            lightpanel.Controls.Add(btnTogglePassword1);
            lightpanel.Controls.Add(pctrBoxProfile);
            lightpanel.Controls.Add(txtPassword);
            lightpanel.Controls.Add(btnBack);
            lightpanel.Controls.Add(lblTitle);
            lightpanel.Controls.Add(txtLogin);
            lightpanel.Controls.Add(txtRepeatpassword);
            lightpanel.Controls.Add(btnRegister);
            lightpanel.Location = new Point(90, 68);
            lightpanel.Name = "lightpanel";
            lightpanel.Size = new Size(1021, 507);
            lightpanel.TabIndex = 6;
            // 
            // btnTogglePassword2
            // 
            btnTogglePassword2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTogglePassword2.BackColor = SystemColors.Window;
            btnTogglePassword2.BackgroundImage = (Image)resources.GetObject("btnTogglePassword2.BackgroundImage");
            btnTogglePassword2.BackgroundImageLayout = ImageLayout.Stretch;
            btnTogglePassword2.FlatAppearance.BorderSize = 0;
            btnTogglePassword2.FlatStyle = FlatStyle.Flat;
            btnTogglePassword2.Location = new Point(414, 258);
            btnTogglePassword2.Name = "btnTogglePassword2";
            btnTogglePassword2.Size = new Size(40, 40);
            btnTogglePassword2.TabIndex = 8;
            btnTogglePassword2.UseVisualStyleBackColor = true;
            btnTogglePassword2.Click += btnTogglePassword2_Click;
            // 
            // btnUploudphoto
            // 
            btnUploudphoto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnUploudphoto.Font = new Font("Arial", 9F);
            btnUploudphoto.Location = new Point(669, 181);
            btnUploudphoto.Name = "btnUploudphoto";
            btnUploudphoto.Size = new Size(261, 34);
            btnUploudphoto.TabIndex = 7;
            btnUploudphoto.Text = "Загрузите фото профиля";
            btnUploudphoto.UseVisualStyleBackColor = true;
            btnUploudphoto.Click += BtnUploudPhoto_Click;
            // 
            // btnTogglePassword1
            // 
            btnTogglePassword1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTogglePassword1.BackColor = SystemColors.Window;
            btnTogglePassword1.BackgroundImage = (Image)resources.GetObject("btnTogglePassword1.BackgroundImage");
            btnTogglePassword1.BackgroundImageLayout = ImageLayout.Stretch;
            btnTogglePassword1.FlatAppearance.BorderSize = 0;
            btnTogglePassword1.FlatStyle = FlatStyle.Flat;
            btnTogglePassword1.Location = new Point(416, 159);
            btnTogglePassword1.Name = "btnTogglePassword1";
            btnTogglePassword1.Size = new Size(34, 34);
            btnTogglePassword1.TabIndex = 7;
            btnTogglePassword1.UseVisualStyleBackColor = true;
            btnTogglePassword1.Click += btnTogglePassword1_Click;
            // 
            // pctrBoxProfile
            // 
            pctrBoxProfile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pctrBoxProfile.BackgroundImageLayout = ImageLayout.Zoom;
            pctrBoxProfile.Location = new Point(647, 82);
            pctrBoxProfile.Name = "pctrBoxProfile";
            pctrBoxProfile.Size = new Size(304, 216);
            pctrBoxProfile.TabIndex = 6;
            pctrBoxProfile.TabStop = false;
            // 
            // RegistrationForm
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(40, 20, 0);
            ClientSize = new Size(1206, 635);
            Controls.Add(lightpanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "RegistrationForm";
            Text = "RegistrationForm";
            lightpanel.ResumeLayout(false);
            lightpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pctrBoxProfile).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private TextBox txtRepeatpassword;
        private Button btnRegister;
        private Button btnBack;
        private Panel lightpanel;
        private Button btnUploudphoto;
        private PictureBox pctrBoxProfile;
        private Button btnTogglePassword1;
        private Button btnTogglePassword2;
    }
}