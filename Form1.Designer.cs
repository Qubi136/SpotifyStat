using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SpotifyStat
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelUserName;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            labelUserName = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // labelUserName
            // 
            labelUserName.AutoSize = true;
            labelUserName.Font = new Font("Microsoft Uighur", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelUserName.Location = new Point(116, 27);
            labelUserName.Name = "labelUserName";
            labelUserName.Size = new Size(104, 61);
            labelUserName.TabIndex = 0;
            labelUserName.Text = "Login";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(140, 82);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.MouseEnter += pictureBox1_MouseEnter;
            pictureBox1.MouseLeave += pictureBox1_MouseLeave;
            // 
            // Form1
            // 
            ClientSize = new Size(385, 336);
            Controls.Add(pictureBox1);
            Controls.Add(labelUserName);
            Name = "Form1";
            Load += Form1_Load;
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void AllInCenter()
        {
            labelUserName.Location = new Point((this.ClientSize.Width - labelUserName.Width) / 2, labelUserName.Height - 4);
            pictureBox1.Location = new Point((this.ClientSize.Width - pictureBox1.Width) / 2, labelUserName.Height + pictureBox1.Height);
        }
        private PictureBox pictureBox1;
    }
}
