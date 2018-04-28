namespace ChessKing
{
    partial class frmChessKing
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
            this.bntQuit = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.veryHardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneplayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twoplayerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // bntQuit
            // 
            this.bntQuit.BackColor = System.Drawing.Color.Maroon;
            this.bntQuit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bntQuit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bntQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntQuit.ForeColor = System.Drawing.Color.LavenderBlush;
            this.bntQuit.Location = new System.Drawing.Point(646, 332);
            this.bntQuit.Name = "bntQuit";
            this.bntQuit.Size = new System.Drawing.Size(111, 23);
            this.bntQuit.TabIndex = 2;
            this.bntQuit.Text = "Quit";
            this.bntQuit.UseVisualStyleBackColor = false;
            this.bntQuit.Click += new System.EventHandler(this.bntQuit_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem,
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 26);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.depthToolStripMenuItem,
            this.soundToolStripMenuItem,
            this.gameTypeToolStripMenuItem});
            this.optionToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionToolStripMenuItem.ForeColor = System.Drawing.Color.Honeydew;
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(70, 22);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // depthToolStripMenuItem
            // 
            this.depthToolStripMenuItem.CheckOnClick = true;
            this.depthToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.veryHardToolStripMenuItem});
            this.depthToolStripMenuItem.Name = "depthToolStripMenuItem";
            this.depthToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.depthToolStripMenuItem.Text = "Level";
            this.depthToolStripMenuItem.Click += new System.EventHandler(this.subietm2ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.CheckOnClick = true;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem2.Text = "Easy";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.CheckOnClick = true;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItem3.Text = "Nomal";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Checked = true;
            this.toolStripMenuItem4.CheckOnClick = true;
            this.toolStripMenuItem4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem4.Text = "Hard ";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // veryHardToolStripMenuItem
            // 
            this.veryHardToolStripMenuItem.CheckOnClick = true;
            this.veryHardToolStripMenuItem.Name = "veryHardToolStripMenuItem";
            this.veryHardToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.veryHardToolStripMenuItem.Text = "Very Hard";
            this.veryHardToolStripMenuItem.Click += new System.EventHandler(this.veryHardToolStripMenuItem_Click);
            // 
            // soundToolStripMenuItem
            // 
            this.soundToolStripMenuItem.CheckOnClick = true;
            this.soundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem,
            this.offToolStripMenuItem});
            this.soundToolStripMenuItem.Name = "soundToolStripMenuItem";
            this.soundToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.soundToolStripMenuItem.Text = "Sound";
            this.soundToolStripMenuItem.Click += new System.EventHandler(this.subietm2ToolStripMenuItem_Click);
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Checked = true;
            this.onToolStripMenuItem.CheckOnClick = true;
            this.onToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.onToolStripMenuItem.Text = "On";
            this.onToolStripMenuItem.Click += new System.EventHandler(this.onToolStripMenuItem_Click);
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.offToolStripMenuItem_Click);
            // 
            // gameTypeToolStripMenuItem
            // 
            this.gameTypeToolStripMenuItem.CheckOnClick = true;
            this.gameTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneplayerToolStripMenuItem,
            this.twoplayerToolStripMenuItem1});
            this.gameTypeToolStripMenuItem.Name = "gameTypeToolStripMenuItem";
            this.gameTypeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gameTypeToolStripMenuItem.Text = "Game Mode";
            this.gameTypeToolStripMenuItem.Click += new System.EventHandler(this.subietm2ToolStripMenuItem_Click);
            // 
            // oneplayerToolStripMenuItem
            // 
            this.oneplayerToolStripMenuItem.Checked = true;
            this.oneplayerToolStripMenuItem.CheckOnClick = true;
            this.oneplayerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.oneplayerToolStripMenuItem.Name = "oneplayerToolStripMenuItem";
            this.oneplayerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.oneplayerToolStripMenuItem.Text = "1 Player";
            this.oneplayerToolStripMenuItem.Click += new System.EventHandler(this.oneplayerToolStripMenuItem_Click);
            // 
            // twoplayerToolStripMenuItem1
            // 
            this.twoplayerToolStripMenuItem1.CheckOnClick = true;
            this.twoplayerToolStripMenuItem1.Name = "twoplayerToolStripMenuItem1";
            this.twoplayerToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.twoplayerToolStripMenuItem1.Text = "2 Player";
            this.twoplayerToolStripMenuItem1.Click += new System.EventHandler(this.twoplayerToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.offlineToolStripMenuItem});
            this.helpToolStripMenuItem1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem1.ForeColor = System.Drawing.Color.Honeydew;
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(54, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // onlineToolStripMenuItem
            // 
            this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
            this.onlineToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.onlineToolStripMenuItem.Text = "Online";
            this.onlineToolStripMenuItem.Click += new System.EventHandler(this.onlineToolStripMenuItem_Click);
            // 
            // offlineToolStripMenuItem
            // 
            this.offlineToolStripMenuItem.Name = "offlineToolStripMenuItem";
            this.offlineToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.offlineToolStripMenuItem.Text = "Offline";
            this.offlineToolStripMenuItem.Click += new System.EventHandler(this.offlineToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.teamToolStripMenuItem,
            this.gameToolStripMenuItem});
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.Honeydew;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(63, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // teamToolStripMenuItem
            // 
            this.teamToolStripMenuItem.Name = "teamToolStripMenuItem";
            this.teamToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.teamToolStripMenuItem.Text = "Team";
            this.teamToolStripMenuItem.Click += new System.EventHandler(this.teamToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.gameToolStripMenuItem.Text = "Game";
            this.gameToolStripMenuItem.Click += new System.EventHandler(this.gameToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ChessKing.Properties.Resources.back;
            this.pictureBox1.Location = new System.Drawing.Point(546, 74);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ChessKing.Properties.Resources.back2;
            this.pictureBox2.Location = new System.Drawing.Point(546, 499);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // btnNewGame
            // 
            this.btnNewGame.BackColor = System.Drawing.Color.OliveDrab;
            this.btnNewGame.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGame.ForeColor = System.Drawing.Color.LavenderBlush;
            this.btnNewGame.Location = new System.Drawing.Point(646, 225);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(111, 23);
            this.btnNewGame.TabIndex = 6;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.BackColor = System.Drawing.Color.Orange;
            this.buttonMinimize.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMinimize.ForeColor = System.Drawing.Color.Black;
            this.buttonMinimize.Location = new System.Drawing.Point(646, 280);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(111, 23);
            this.buttonMinimize.TabIndex = 7;
            this.buttonMinimize.Text = "Minimize";
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            // 
            // frmChessKing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::ChessKing.Properties.Resources.B1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 610);
            this.Controls.Add(this.buttonMinimize);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bntQuit);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChessKing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess King";
            this.Load += new System.EventHandler(this.frmChessKing_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bntQuit;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem depthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripMenuItem soundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem veryHardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem teamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.ToolStripMenuItem gameTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneplayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twoplayerToolStripMenuItem1;
    }
}

