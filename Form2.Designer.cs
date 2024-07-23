namespace SpotifyStat
{
    partial class Form2
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
            label1 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(184, 105);
            label1.TabIndex = 0;
            label1.Text = "Devs\r\nCharkin Serhii\r\nMyroshnychenko Anrdii \r\nKovalchuk Dmytro \r\nBarladian Valeria\r\n";
            label1.Click += label1_Click_1;
            // 
            // pictureBox1
            // 
            pictureBox1.ImageLocation = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRYYRSg7kojWoYDDqgLD3N-1PfEew_tDwtW-g&s";
            pictureBox1.Location = new Point(-3, 94);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(478, 313);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 198, 65);
            ClientSize = new Size(472, 407);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            ForeColor = SystemColors.ButtonFace;
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
    }
}