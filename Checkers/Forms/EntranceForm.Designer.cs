namespace Checkers
{
    partial class EntranceForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            txtLogin = new TextBox();
            txtPassword = new TextBox();
            btnTogglePassword = new Button();
            btnLogin = new Button();
            btnRegister = new Button();
            lightpanel = new Panel();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.BackColor = Color.FromArgb(65, 35, 20);
            lblTitle.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblTitle.ForeColor = Color.FromArgb(226, 199, 153);
            lblTitle.Location = new Point(350, 90);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(515, 55);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Шашки Онлайн";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtLogin
            // 
            txtLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtLogin.Font = new Font("Arial Rounded MT Bold", 14F);
            txtLogin.ForeColor = Color.Black;
            txtLogin.Location = new Point(330, 200);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(547, 40);
            txtLogin.TabIndex = 1;
            txtLogin.Text = "Логин";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Font = new Font("Arial", 14F, FontStyle.Bold);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Location = new Point(330, 260);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(517, 40);
            txtPassword.TabIndex = 2;
            txtPassword.Text = "Пароль";
            txtPassword.Enter += txtPassword_Enter;
            txtPassword.Leave += txtPassword_Leave;
            // 
            // btnTogglePassword
            // 
            btnTogglePassword.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTogglePassword.BackColor = SystemColors.Window;
            btnTogglePassword.FlatAppearance.BorderSize = 0;
            btnTogglePassword.FlatStyle = FlatStyle.Flat;
            btnTogglePassword.Location = new Point(829, 260);
            btnTogglePassword.Name = "btnTogglePassword";
            btnTogglePassword.Size = new Size(48, 40);
            btnTogglePassword.TabIndex = 3;
            btnTogglePassword.UseVisualStyleBackColor = false;
            btnTogglePassword.Click += btnTogglePassword_Click;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnLogin.BackColor = Color.FromArgb(0, 64, 0);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Arial", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(450, 370);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(300, 50);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Войти";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnRegister.BackColor = Color.FromArgb(0, 64, 0);
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Arial", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(450, 450);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(300, 50);
            btnRegister.TabIndex = 5;
            btnRegister.Text = "Зарегистрироваться";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            // 
            // lightpanel
            // 
            lightpanel.Anchor = AnchorStyles.None;
            lightpanel.BackColor = Color.FromArgb(65, 35, 20);
            lightpanel.Location = new Point(221, 56);
            lightpanel.Name = "lightpanel";
            lightpanel.Size = new Size(770, 493);
            lightpanel.TabIndex = 6;
            // 
            // EntranceForm
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(40, 20, 0);
            ClientSize = new Size(1206, 635);
            Controls.Add(btnRegister);
            Controls.Add(btnLogin);
            Controls.Add(btnTogglePassword);
            Controls.Add(txtPassword);
            Controls.Add(txtLogin);
            Controls.Add(lblTitle);
            Controls.Add(lightpanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "EntranceForm";
            Text = "EntranceForm";
            Load += EntranceForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnTogglePassword;
        private Button btnLogin;
        private Button btnRegister;
        private Panel lightpanel;
    }
}
