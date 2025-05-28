namespace Checkers.Forms
{
    partial class MainForm
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
            panel1 = new Panel();
            btnExit = new Button();
            btnProfile = new Button();
            panel2 = new Panel();
            lblTitle = new Label();
            btnPlay = new Button();
            pctrBoxPlayers = new PictureBox();
            tblLayoutPnlHistory = new TableLayoutPanel();
            lblLogin = new Label();
            lblWins = new Label();
            lblLosses = new Label();
            lblPlace = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctrBoxPlayers).BeginInit();
            tblLayoutPnlHistory.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(40, 20, 0);
            panel1.Controls.Add(btnExit);
            panel1.Controls.Add(btnProfile);
            panel1.Location = new Point(33, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(429, 412);
            panel1.TabIndex = 0;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(192, 64, 0);
            btnExit.FlatStyle = FlatStyle.Popup;
            btnExit.Font = new Font("Arial", 11F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(108, 230);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(201, 52);
            btnExit.TabIndex = 0;
            btnExit.Text = "Выход";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += BtnExit_Click;
            // 
            // btnProfile
            // 
            btnProfile.BackColor = Color.FromArgb(0, 64, 0);
            btnProfile.FlatStyle = FlatStyle.Popup;
            btnProfile.Font = new Font("Arial", 11F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnProfile.ForeColor = Color.White;
            btnProfile.Location = new Point(108, 156);
            btnProfile.Name = "btnProfile";
            btnProfile.Size = new Size(201, 52);
            btnProfile.TabIndex = 0;
            btnProfile.Text = "Профиль";
            btnProfile.UseVisualStyleBackColor = false;
            btnProfile.Click += BtnProfile_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(40, 20, 0);
            panel2.Controls.Add(lblTitle);
            panel2.Controls.Add(btnPlay);
            panel2.Controls.Add(pctrBoxPlayers);
            panel2.Location = new Point(548, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(626, 412);
            panel2.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.BackColor = Color.FromArgb(40, 20, 0);
            lblTitle.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblTitle.ForeColor = Color.FromArgb(226, 199, 153);
            lblTitle.Location = new Point(60, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(523, 55);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Шашки Онлайн";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.FromArgb(0, 64, 0);
            btnPlay.FlatStyle = FlatStyle.Popup;
            btnPlay.Font = new Font("Arial", 11F, FontStyle.Bold);
            btnPlay.ForeColor = Color.White;
            btnPlay.Location = new Point(235, 307);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(171, 52);
            btnPlay.TabIndex = 1;
            btnPlay.Text = "Играть";
            btnPlay.UseVisualStyleBackColor = false;
            btnPlay.Click += BtnPlay_Click;
            // 
            // pctrBoxPlayers
            // 
            pctrBoxPlayers.BackColor = Color.FromArgb(65, 35, 20);
            pctrBoxPlayers.Location = new Point(113, 68);
            pctrBoxPlayers.Name = "pctrBoxPlayers";
            pctrBoxPlayers.Size = new Size(407, 315);
            pctrBoxPlayers.TabIndex = 0;
            pctrBoxPlayers.TabStop = false;
            // 
            // tblLayoutPnlHistory
            // 
            tblLayoutPnlHistory.BackColor = Color.FromArgb(40, 20, 0);
            tblLayoutPnlHistory.ColumnCount = 4;
            tblLayoutPnlHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.2352943F));
            tblLayoutPnlHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.76471F));
            tblLayoutPnlHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 338F));
            tblLayoutPnlHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 326F));
            tblLayoutPnlHistory.Controls.Add(lblLogin, 1, 0);
            tblLayoutPnlHistory.Controls.Add(lblWins, 2, 0);
            tblLayoutPnlHistory.Controls.Add(lblLosses, 3, 0);
            tblLayoutPnlHistory.Controls.Add(lblPlace, 0, 0);
            tblLayoutPnlHistory.Location = new Point(33, 446);
            tblLayoutPnlHistory.Name = "tblLayoutPnlHistory";
            tblLayoutPnlHistory.RowCount = 1;
            tblLayoutPnlHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblLayoutPnlHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblLayoutPnlHistory.Size = new Size(1144, 177);
            tblLayoutPnlHistory.TabIndex = 2;
            // 
            // lblLogin
            // 
            lblLogin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblLogin.AutoSize = true;
            lblLogin.BackColor = Color.FromArgb(65, 35, 20);
            lblLogin.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblLogin.ForeColor = Color.White;
            lblLogin.Location = new Point(162, 0);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(314, 27);
            lblLogin.TabIndex = 1;
            lblLogin.Text = "Логин";
            lblLogin.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWins
            // 
            lblWins.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblWins.AutoSize = true;
            lblWins.BackColor = Color.FromArgb(65, 35, 20);
            lblWins.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblWins.ForeColor = Color.White;
            lblWins.Location = new Point(482, 0);
            lblWins.Name = "lblWins";
            lblWins.Size = new Size(332, 27);
            lblWins.TabIndex = 2;
            lblWins.Text = "Выиграл";
            lblWins.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLosses
            // 
            lblLosses.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblLosses.AutoSize = true;
            lblLosses.BackColor = Color.FromArgb(65, 35, 20);
            lblLosses.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblLosses.ForeColor = Color.White;
            lblLosses.Location = new Point(820, 0);
            lblLosses.Name = "lblLosses";
            lblLosses.Size = new Size(321, 27);
            lblLosses.TabIndex = 3;
            lblLosses.Text = "Проиграл";
            lblLosses.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPlace
            // 
            lblPlace.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPlace.AutoSize = true;
            lblPlace.BackColor = Color.FromArgb(65, 35, 20);
            lblPlace.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblPlace.ForeColor = Color.White;
            lblPlace.Location = new Point(3, 0);
            lblPlace.Name = "lblPlace";
            lblPlace.Size = new Size(153, 27);
            lblPlace.TabIndex = 0;
            lblPlace.Text = "Mесто";
            lblPlace.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Tan;
            ClientSize = new Size(1206, 635);
            Controls.Add(tblLayoutPnlHistory);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "MainForm";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pctrBoxPlayers).EndInit();
            tblLayoutPnlHistory.ResumeLayout(false);
            tblLayoutPnlHistory.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Button btnProfile;
        private Button btnExit;
        private PictureBox pctrBoxPlayers;
        private Button btnPlay;
        private TableLayoutPanel tblLayoutPnlHistory;
        private Label lblTitle;
        private Label lblPlace;
        private Label lblLogin;
        private Label lblWins;
        private Label lblLosses;
    }
}