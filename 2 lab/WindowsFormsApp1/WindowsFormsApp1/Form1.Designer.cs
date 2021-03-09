namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ClearButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.TopsLabel = new System.Windows.Forms.Label();
            this.PreDrawTimer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PenWidthBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.PenColorButton = new System.Windows.Forms.Button();
            this.PenColorPanel = new System.Windows.Forms.Panel();
            this.FillColorButton = new System.Windows.Forms.Button();
            this.FillColorPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PenWidthBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(844, 474);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(106, 23);
            this.ClearButton.TabIndex = 1;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(844, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(125, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(917, 64);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(52, 20);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // TopsLabel
            // 
            this.TopsLabel.Location = new System.Drawing.Point(841, 64);
            this.TopsLabel.Name = "TopsLabel";
            this.TopsLabel.Size = new System.Drawing.Size(70, 29);
            this.TopsLabel.TabIndex = 5;
            this.TopsLabel.Text = "Количество вершин";
            // 
            // PreDrawTimer
            // 
            this.PreDrawTimer.Interval = 10;
            this.PreDrawTimer.Tick += new System.EventHandler(this.PreDrawTimer_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(811, 494);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // PenWidthBar
            // 
            this.PenWidthBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.PenWidthBar.Location = new System.Drawing.Point(844, 96);
            this.PenWidthBar.Maximum = 20;
            this.PenWidthBar.Name = "PenWidthBar";
            this.PenWidthBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.PenWidthBar.Size = new System.Drawing.Size(45, 170);
            this.PenWidthBar.TabIndex = 6;
            this.PenWidthBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.PenWidthBar.Value = 6;
            this.PenWidthBar.Scroll += new System.EventHandler(this.PenWidthBar_Scroll);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(841, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Толщина";
            // 
            // PenColorButton
            // 
            this.PenColorButton.Location = new System.Drawing.Point(829, 298);
            this.PenColorButton.Name = "PenColorButton";
            this.PenColorButton.Size = new System.Drawing.Size(60, 40);
            this.PenColorButton.TabIndex = 8;
            this.PenColorButton.Text = "Цвет линии";
            this.PenColorButton.UseVisualStyleBackColor = true;
            this.PenColorButton.Click += new System.EventHandler(this.PenColorButton_Click);
            // 
            // PenColorPanel
            // 
            this.PenColorPanel.BackColor = System.Drawing.Color.Black;
            this.PenColorPanel.Location = new System.Drawing.Point(910, 297);
            this.PenColorPanel.Name = "PenColorPanel";
            this.PenColorPanel.Size = new System.Drawing.Size(40, 41);
            this.PenColorPanel.TabIndex = 9;
            this.PenColorPanel.Click += new System.EventHandler(this.PenColorButton_Click);
            // 
            // FillColorButton
            // 
            this.FillColorButton.Location = new System.Drawing.Point(829, 344);
            this.FillColorButton.Name = "FillColorButton";
            this.FillColorButton.Size = new System.Drawing.Size(60, 40);
            this.FillColorButton.TabIndex = 10;
            this.FillColorButton.Text = "Цвет заливки";
            this.FillColorButton.UseVisualStyleBackColor = true;
            this.FillColorButton.Click += new System.EventHandler(this.FillColorButton_Click);
            // 
            // FillColorPanel
            // 
            this.FillColorPanel.BackColor = System.Drawing.Color.White;
            this.FillColorPanel.Location = new System.Drawing.Point(910, 343);
            this.FillColorPanel.Name = "FillColorPanel";
            this.FillColorPanel.Size = new System.Drawing.Size(40, 41);
            this.FillColorPanel.TabIndex = 10;
            this.FillColorPanel.Click += new System.EventHandler(this.FillColorButton_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(840, 398);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 73);
            this.label2.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(854, 522);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "label3";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 544);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FillColorPanel);
            this.Controls.Add(this.FillColorButton);
            this.Controls.Add(this.PenColorPanel);
            this.Controls.Add(this.PenColorButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PenWidthBar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TopsLabel);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.ClearButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PenWidthBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label TopsLabel;
        private System.Windows.Forms.Timer PreDrawTimer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar PenWidthBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button PenColorButton;
        private System.Windows.Forms.Panel PenColorPanel;
        private System.Windows.Forms.Button FillColorButton;
        private System.Windows.Forms.Panel FillColorPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}

