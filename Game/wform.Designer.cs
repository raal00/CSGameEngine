namespace Game
{
    partial class wform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wform));
            this.RunBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.LoadBtn = new System.Windows.Forms.Button();
            this.LevelsBox = new System.Windows.Forms.ListBox();
            this.LoadSavedGame_btn = new System.Windows.Forms.Button();
            this.SetLevel = new System.Windows.Forms.Button();
            this.LevelID = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LevelID_2 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.LevelID)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LevelID_2)).BeginInit();
            this.SuspendLayout();
            // 
            // RunBtn
            // 
            this.RunBtn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RunBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RunBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RunBtn.Location = new System.Drawing.Point(9, 25);
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(154, 65);
            this.RunBtn.TabIndex = 0;
            this.RunBtn.Text = "Запуск конструктора уровней";
            this.RunBtn.UseVisualStyleBackColor = false;
            this.RunBtn.Click += new System.EventHandler(this.RunBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SaveBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SaveBtn.Location = new System.Drawing.Point(6, 199);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(157, 65);
            this.SaveBtn.TabIndex = 1;
            this.SaveBtn.Text = "Сохранить игру";
            this.SaveBtn.UseVisualStyleBackColor = false;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // LoadBtn
            // 
            this.LoadBtn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LoadBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LoadBtn.Location = new System.Drawing.Point(6, 23);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(157, 65);
            this.LoadBtn.TabIndex = 2;
            this.LoadBtn.Text = "Выбор уровня";
            this.LoadBtn.UseVisualStyleBackColor = false;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // LevelsBox
            // 
            this.LevelsBox.FormattingEnabled = true;
            this.LevelsBox.Location = new System.Drawing.Point(169, 19);
            this.LevelsBox.Name = "LevelsBox";
            this.LevelsBox.Size = new System.Drawing.Size(175, 381);
            this.LevelsBox.TabIndex = 3;
            this.LevelsBox.SelectedIndexChanged += new System.EventHandler(this.LevelsBox_SelectedIndexChanged);
            // 
            // LoadSavedGame_btn
            // 
            this.LoadSavedGame_btn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LoadSavedGame_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadSavedGame_btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LoadSavedGame_btn.Location = new System.Drawing.Point(6, 94);
            this.LoadSavedGame_btn.Name = "LoadSavedGame_btn";
            this.LoadSavedGame_btn.Size = new System.Drawing.Size(157, 65);
            this.LoadSavedGame_btn.TabIndex = 4;
            this.LoadSavedGame_btn.Text = "Выбор сохраненной игры";
            this.LoadSavedGame_btn.UseVisualStyleBackColor = false;
            this.LoadSavedGame_btn.Click += new System.EventHandler(this.ShowSavedGame_btn_Click);
            // 
            // SetLevel
            // 
            this.SetLevel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SetLevel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SetLevel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SetLevel.Location = new System.Drawing.Point(6, 128);
            this.SetLevel.Name = "SetLevel";
            this.SetLevel.Size = new System.Drawing.Size(157, 65);
            this.SetLevel.TabIndex = 5;
            this.SetLevel.Text = "Сохранить уровень";
            this.SetLevel.UseVisualStyleBackColor = false;
            this.SetLevel.Click += new System.EventHandler(this.SetLevel_Click);
            // 
            // LevelID
            // 
            this.LevelID.Location = new System.Drawing.Point(91, 194);
            this.LevelID.Name = "LevelID";
            this.LevelID.Size = new System.Drawing.Size(72, 20);
            this.LevelID.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Номер уровня";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SetLevel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.RunBtn);
            this.groupBox1.Controls.Add(this.LevelID);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 220);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Генерация сцены";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.LoadBtn);
            this.groupBox2.Controls.Add(this.LevelID_2);
            this.groupBox2.Controls.Add(this.SaveBtn);
            this.groupBox2.Controls.Add(this.LevelsBox);
            this.groupBox2.Controls.Add(this.LoadSavedGame_btn);
            this.groupBox2.Location = new System.Drawing.Point(286, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(384, 420);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Запуск игры";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Номер сохранения";
            // 
            // LevelID_2
            // 
            this.LevelID_2.Location = new System.Drawing.Point(115, 265);
            this.LevelID_2.Name = "LevelID_2";
            this.LevelID_2.Size = new System.Drawing.Size(46, 20);
            this.LevelID_2.TabIndex = 9;
            // 
            // wform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(682, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "wform";
            this.Text = "Game launcher v1.1";
            this.Load += new System.EventHandler(this.wform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LevelID)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LevelID_2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RunBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button LoadBtn;
        private System.Windows.Forms.ListBox LevelsBox;
        private System.Windows.Forms.Button LoadSavedGame_btn;
        private System.Windows.Forms.Button SetLevel;
        private System.Windows.Forms.NumericUpDown LevelID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown LevelID_2;
    }
}