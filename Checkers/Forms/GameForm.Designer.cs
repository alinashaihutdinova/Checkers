namespace Checkers.Forms
{
    partial class GameForm
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
                _timer?.Stop();
                _timer?.Dispose();
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
            btngiveup = new Button();
            btnendmove = new Button();
            checkersboardpnl = new Panel();
            pnlplayer1 = new Panel();
            lblstatus1 = new Label();
            lblplayer1 = new Label();
            pnlplayer2 = new Panel();
            lblstatus2 = new Label();
            lblplayer2 = new Label();
            lbltimer = new Label();
            pnlplayer1.SuspendLayout();
            pnlplayer2.SuspendLayout();
            SuspendLayout();
            // 
            // btngiveup
            // 
            btngiveup.BackColor = Color.Navy;
            btngiveup.FlatStyle = FlatStyle.Popup;
            btngiveup.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btngiveup.ForeColor = SystemColors.Window;
            btngiveup.Location = new Point(105, 460);
            btngiveup.Name = "btngiveup";
            btngiveup.Size = new Size(222, 50);
            btngiveup.TabIndex = 0;
            btngiveup.Text = "Сдаться";
            btngiveup.UseVisualStyleBackColor = false;
            btngiveup.Click += btngiveup_Click;
            // 
            // btnendmove
            // 
            btnendmove.BackColor = Color.FromArgb(0, 64, 0);
            btnendmove.FlatStyle = FlatStyle.Popup;
            btnendmove.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnendmove.ForeColor = SystemColors.Window;
            btnendmove.Location = new Point(105, 536);
            btnendmove.Name = "btnendmove";
            btnendmove.Size = new Size(222, 50);
            btnendmove.TabIndex = 1;
            btnendmove.Text = "Завершить ход";
            btnendmove.UseVisualStyleBackColor = false;
            btnendmove.Click += btnendmove_Click;
            // 
            // checkersboardpnl
            // 
            checkersboardpnl.Location = new Point(509, 25);
            checkersboardpnl.Name = "checkersboardpnl";
            checkersboardpnl.Size = new Size(620, 620);
            checkersboardpnl.TabIndex = 2;
            checkersboardpnl.Paint += checkersboardpnl_Paint;
            checkersboardpnl.MouseClick += сheckersboardpnl_MouseClick;
            // 
            // pnlplayer1
            // 
            pnlplayer1.BackColor = Color.Tan;
            pnlplayer1.Controls.Add(lblstatus1);
            pnlplayer1.Controls.Add(lblplayer1);
            pnlplayer1.Location = new Point(69, 58);
            pnlplayer1.Name = "pnlplayer1";
            pnlplayer1.Size = new Size(300, 180);
            pnlplayer1.TabIndex = 0;
            // 
            // lblstatus1
            // 
            lblstatus1.AutoSize = true;
            lblstatus1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblstatus1.Location = new Point(102, 93);
            lblstatus1.Name = "lblstatus1";
            lblstatus1.Size = new Size(96, 30);
            lblstatus1.TabIndex = 1;
            lblstatus1.Text = "Ваш ход";
            // 
            // lblplayer1
            // 
            lblplayer1.AutoSize = true;
            lblplayer1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblplayer1.Location = new Point(102, 24);
            lblplayer1.Name = "lblplayer1";
            lblplayer1.Size = new Size(107, 32);
            lblplayer1.TabIndex = 0;
            lblplayer1.Text = "Игрок 1";
            // 
            // pnlplayer2
            // 
            pnlplayer2.BackColor = Color.Tan;
            pnlplayer2.Controls.Add(lblstatus2);
            pnlplayer2.Controls.Add(lblplayer2);
            pnlplayer2.Location = new Point(69, 244);
            pnlplayer2.Name = "pnlplayer2";
            pnlplayer2.Size = new Size(300, 180);
            pnlplayer2.TabIndex = 3;
            // 
            // lblstatus2
            // 
            lblstatus2.AutoSize = true;
            lblstatus2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblstatus2.Location = new Point(89, 90);
            lblstatus2.Name = "lblstatus2";
            lblstatus2.Size = new Size(120, 30);
            lblstatus2.TabIndex = 2;
            lblstatus2.Text = "Ожидание";
            // 
            // lblplayer2
            // 
            lblplayer2.AutoSize = true;
            lblplayer2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblplayer2.Location = new Point(102, 27);
            lblplayer2.Name = "lblplayer2";
            lblplayer2.Size = new Size(107, 32);
            lblplayer2.TabIndex = 0;
            lblplayer2.Text = "Игрок 2";
            // 
            // lbltimer
            // 
            lbltimer.AutoSize = true;
            lbltimer.Font = new Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lbltimer.ForeColor = SystemColors.Window;
            lbltimer.Location = new Point(307, 9);
            lbltimer.Name = "lbltimer";
            lbltimer.Size = new Size(196, 33);
            lbltimer.TabIndex = 4;
            lbltimer.Text = "Время: 00:30";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(65, 35, 20);
            ClientSize = new Size(1206, 675);
            Controls.Add(lbltimer);
            Controls.Add(pnlplayer1);
            Controls.Add(pnlplayer2);
            Controls.Add(checkersboardpnl);
            Controls.Add(btnendmove);
            Controls.Add(btngiveup);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GameForm";
            Text = "GameForm";
            pnlplayer1.ResumeLayout(false);
            pnlplayer1.PerformLayout();
            pnlplayer2.ResumeLayout(false);
            pnlplayer2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Button btngiveup;
        private Button btnendmove;
        private Panel checkersboardpnl;
        private Panel pnlplayer1;
        private Label lblplayer1;
        private Panel pnlplayer2;
        private Label lblplayer2;
        private Label lblstatus1;
        private Label lblstatus2;
        private Label lbltimer;
    }
}