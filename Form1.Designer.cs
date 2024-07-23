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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            labelUserName = new Label();
            pictureBox1 = new PictureBox();
            OpenNewForm = new Label();
            Settings = new PictureBox();
            LanguageSettings = new ContextMenuStrip(components);
            toolStripTextBox1 = new ToolStripTextBox();
            toolStripSeparator1 = new ToolStripSeparator();
            UkrSet = new ToolStripMenuItem();
            EngSet = new ToolStripMenuItem();
            RusSet = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            CloseMenu = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Settings).BeginInit();
            LanguageSettings.SuspendLayout();
            SuspendLayout();
            // 
            // labelUserName
            // 
            resources.ApplyResources(labelUserName, "labelUserName");
            labelUserName.ForeColor = Color.FromArgb(82, 185, 104);
            labelUserName.Name = "labelUserName";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.ae844e959e313cd5fc36f2ba77268661_removebg_preview;
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.MouseMove += PictureBox_MouseMove;
            // 
            // OpenNewForm
            // 
            resources.ApplyResources(OpenNewForm, "OpenNewForm");
            OpenNewForm.ForeColor = Color.FromArgb(82, 185, 104);
            OpenNewForm.Name = "OpenNewForm";
            OpenNewForm.Click += OpenNewForm_Click_1;
            OpenNewForm.MouseEnter += OpenNewForm_MouseEnter;
            OpenNewForm.MouseLeave += OpenNewForm_MouseLeave;
            // 
            // Settings
            // 
            Settings.BackColor = Color.Transparent;
            Settings.Image = Properties.Resources._;
            resources.ApplyResources(Settings, "Settings");
            Settings.Name = "Settings";
            Settings.TabStop = false;
            Settings.Click += pictureBox2_Click;
            Settings.MouseMove += PictureBox_MouseMove;
            // 
            // LanguageSettings
            // 
            LanguageSettings.AutoClose = false;
            LanguageSettings.BackColor = Color.FromArgb(18, 18, 18);
            LanguageSettings.ImageScalingSize = new Size(0, 0);
            LanguageSettings.Items.AddRange(new ToolStripItem[] { toolStripTextBox1, toolStripSeparator1, UkrSet, EngSet, RusSet, toolStripSeparator2, CloseMenu });
            LanguageSettings.Name = "contextMenuStrip1";
            LanguageSettings.RenderMode = ToolStripRenderMode.System;
            LanguageSettings.ShowImageMargin = false;
            resources.ApplyResources(LanguageSettings, "LanguageSettings");
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.BackColor = Color.FromArgb(18, 18, 18);
            toolStripTextBox1.BorderStyle = BorderStyle.None;
            resources.ApplyResources(toolStripTextBox1, "toolStripTextBox1");
            toolStripTextBox1.ForeColor = Color.FromArgb(82, 185, 104);
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.ReadOnly = true;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // UkrSet
            // 
            UkrSet.BackColor = Color.FromArgb(18, 18, 18);
            resources.ApplyResources(UkrSet, "UkrSet");
            UkrSet.ForeColor = Color.FromArgb(82, 185, 104);
            UkrSet.Name = "UkrSet";
            UkrSet.Click += UkrSet_Click;
            // 
            // EngSet
            // 
            resources.ApplyResources(EngSet, "EngSet");
            EngSet.ForeColor = Color.FromArgb(82, 185, 104);
            EngSet.Name = "EngSet";
            EngSet.Click += EngSet_Click;
            // 
            // RusSet
            // 
            resources.ApplyResources(RusSet, "RusSet");
            RusSet.ForeColor = Color.FromArgb(82, 185, 104);
            RusSet.Name = "RusSet";
            RusSet.Click += RusSet_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // CloseMenu
            // 
            resources.ApplyResources(CloseMenu, "CloseMenu");
            CloseMenu.ForeColor = Color.Red;
            CloseMenu.Name = "CloseMenu";
            CloseMenu.Click += CloseMenu_Click;
            // 
            // Form1
            // 
            BackColor = Color.FromArgb(18, 18, 18);
            resources.ApplyResources(this, "$this");
            Controls.Add(Settings);
            Controls.Add(OpenNewForm);
            Controls.Add(pictureBox1);
            Controls.Add(labelUserName);
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Settings).EndInit();
            LanguageSettings.ResumeLayout(false);
            LanguageSettings.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox == null || pictureBox.Image == null)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            Bitmap bitmap = (Bitmap)pictureBox.Image;

            // Получаем размер изображения и PictureBox
            Size imageSize = bitmap.Size;
            Size pictureBoxSize = pictureBox.ClientSize;

            // Рассчитываем коэффициенты масштабирования по горизонтали и вертикали
            float scaleX = (float)pictureBoxSize.Width / imageSize.Width;
            float scaleY = (float)pictureBoxSize.Height / imageSize.Height;

            // Используем меньший коэффициент для сохранения пропорций изображения
            float scale = Math.Min(scaleX, scaleY);

            // Рассчитываем размеры отображаемого изображения
            Size scaledImageSize = new Size((int)(imageSize.Width * scale), (int)(imageSize.Height * scale));

            // Рассчитываем отступы по горизонтали и вертикали
            int offsetX = (pictureBoxSize.Width - scaledImageSize.Width) / 2;
            int offsetY = (pictureBoxSize.Height - scaledImageSize.Height) / 2;

            // Рассчитываем координаты курсора относительно изображения
            int x = (int)((e.X - offsetX) / scale);
            int y = (int)((e.Y - offsetY) / scale);

            // Проверяем, находится ли курсор в пределах изображения
            if (x < 0 || x >= imageSize.Width || y < 0 || y >= imageSize.Height)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            Color pixelColor = bitmap.GetPixel(x, y);

            // Проверяем, является ли пиксель прозрачным
            if (pixelColor.A == 0)
            {
                this.Cursor = Cursors.Default;
            }
            else
            {
                this.Cursor = Cursors.Hand; // Замените на нужный вам курсор
            }
        }
        private void AllInCenter()
        {
            labelUserName.Location = new Point((ClientSize.Width - labelUserName.Width) / 2, (ClientSize.Height - labelUserName.Height) / 4);
            pictureBox1.Location = new Point((ClientSize.Width - pictureBox1.Width) / 2, (ClientSize.Height - pictureBox1.Height) / 4 + labelUserName.Height);
            OpenNewForm.Location = new Point(ClientSize.Width - OpenNewForm.Width - 4, ClientSize.Height - OpenNewForm.Height - 4);
        }
        private PictureBox pictureBox1;
        private Label OpenNewForm;
        private PictureBox Settings;
        private ContextMenuStrip LanguageSettings;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripMenuItem UkrSet;
        private ToolStripMenuItem EngSet;
        private ToolStripMenuItem RusSet;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem CloseMenu;
        private ToolStripSeparator toolStripSeparator2;
    }
}
