using SpotifyAPI.Web;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;

namespace SpotifyStat
{

partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            labelUserName = new Label();
            ComboBox2 = new RJComboBox();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            listView1 = new ListView();
            button2 = new Button();
            label1 = new Label();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            ComboBox1 = new RJComboBox();
            trackState = new RJProgressBar();
            panel1 = new Panel();
            label2 = new Label();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            PlayResume = new PictureBox();
            PictureOfSong = new PictureBox();
            ListeningTime = new Label();
            pictureBox2 = new PictureBox();
            button3 = new Button();
            Settings = new ContextMenuStrip(components);
            LanguageSettings = new ToolStripMenuItem();
            toolStripTextBox1 = new ToolStripTextBox();
            toolStripSeparator2 = new ToolStripSeparator();
            SetUkr = new ToolStripMenuItem();
            SetEng = new ToolStripMenuItem();
            SetRus = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            CloseContextMenu = new ToolStripMenuItem();
            pictureBox5 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PlayResume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureOfSong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            Settings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // labelUserName
            // 
            resources.ApplyResources(labelUserName, "labelUserName");
            labelUserName.ForeColor = Color.FromArgb(82, 185, 104);
            labelUserName.Name = "labelUserName";
            // 
            // ComboBox2
            // 
            resources.ApplyResources(ComboBox2, "ComboBox2");
            ComboBox2.BackColor = Color.FromArgb(41, 41, 41);
            ComboBox2.BorderColor = Color.FromArgb(82, 185, 104);
            ComboBox2.BorderSize = 1;
            ComboBox2.DropDownStyle = ComboBoxStyle.DropDown;
            ComboBox2.ForeColor = Color.White;
            ComboBox2.IconColor = Color.FromArgb(82, 185, 104);
            ComboBox2.Items.AddRange(new object[] { resources.GetString("ComboBox2.Items"), resources.GetString("ComboBox2.Items1"), resources.GetString("ComboBox2.Items2") });
            ComboBox2.ListBackColor = Color.FromArgb(41, 41, 41);
            ComboBox2.ListTextColor = Color.White;
            ComboBox2.Name = "ComboBox2";
            ComboBox2.Texts = "Select period";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.BackColor = Color.White;
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.BackColor = Color.FromArgb(82, 185, 104);
            button1.ForeColor = Color.FromArgb(18, 18, 18);
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // listView1
            // 
            resources.ApplyResources(listView1, "listView1");
            listView1.BackColor = Color.FromArgb(41, 41, 41);
            listView1.ForeColor = Color.White;
            listView1.Name = "listView1";
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.MouseClick += listView1_MouseClick;
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.BackColor = Color.FromArgb(82, 185, 104);
            button2.ForeColor = Color.FromArgb(18, 18, 18);
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.AutoEllipsis = true;
            label1.ForeColor = Color.White;
            label1.Name = "label1";
            // 
            // button4
            // 
            resources.ApplyResources(button4, "button4");
            button4.BackColor = Color.FromArgb(82, 185, 104);
            button4.ForeColor = Color.FromArgb(18, 18, 18);
            button4.Name = "button4";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            resources.ApplyResources(button5, "button5");
            button5.BackColor = Color.FromArgb(82, 185, 104);
            button5.ForeColor = Color.FromArgb(18, 18, 18);
            button5.Name = "button5";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            resources.ApplyResources(button6, "button6");
            button6.AutoEllipsis = true;
            button6.BackColor = Color.FromArgb(82, 185, 104);
            button6.ForeColor = Color.FromArgb(18, 18, 18);
            button6.Name = "button6";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // ComboBox1
            // 
            resources.ApplyResources(ComboBox1, "ComboBox1");
            ComboBox1.BackColor = Color.FromArgb(41, 41, 41);
            ComboBox1.BorderColor = Color.FromArgb(82, 185, 104);
            ComboBox1.BorderSize = 1;
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            ComboBox1.ForeColor = Color.White;
            ComboBox1.IconColor = Color.FromArgb(82, 185, 104);
            ComboBox1.Items.AddRange(new object[] { resources.GetString("ComboBox1.Items"), resources.GetString("ComboBox1.Items1"), resources.GetString("ComboBox1.Items2") });
            ComboBox1.ListBackColor = Color.FromArgb(41, 41, 41);
            ComboBox1.ListTextColor = Color.White;
            ComboBox1.Name = "ComboBox1";
            ComboBox1.Texts = "Select period";
            // 
            // trackState
            // 
            resources.ApplyResources(trackState, "trackState");
            trackState.BackColor = Color.FromArgb(41, 41, 41);
            trackState.ChannelColor = Color.FromArgb(18, 18, 18);
            trackState.ChannelHeight = 9;
            trackState.ForeBackColor = Color.OliveDrab;
            trackState.ForeColor = Color.FromArgb(82, 185, 104);
            trackState.Name = "trackState";
            trackState.ShowMaximun = false;
            trackState.ShowValue = TextPosition.None;
            trackState.SliderColor = Color.FromArgb(82, 185, 104);
            trackState.SliderHeight = 6;
            trackState.Step = 1;
            trackState.Style = ProgressBarStyle.Continuous;
            trackState.SymbolAfter = "";
            trackState.SymbolBefore = "";
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.BackColor = Color.FromArgb(41, 41, 41);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(PlayResume);
            panel1.Controls.Add(PictureOfSong);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(trackState);
            panel1.Name = "panel1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.ForeColor = Color.FromArgb(82, 185, 104);
            label2.Name = "label2";
            // 
            // pictureBox4
            // 
            resources.ApplyResources(pictureBox4, "pictureBox4");
            pictureBox4.BackColor = Color.FromArgb(41, 41, 41);
            pictureBox4.Image = Properties.Resources.Перемотка_вперед_;
            pictureBox4.Name = "pictureBox4";
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // pictureBox3
            // 
            resources.ApplyResources(pictureBox3, "pictureBox3");
            pictureBox3.BackColor = Color.FromArgb(41, 41, 41);
            pictureBox3.Image = Properties.Resources.Перемотка_назад;
            pictureBox3.Name = "pictureBox3";
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // PlayResume
            // 
            resources.ApplyResources(PlayResume, "PlayResume");
            PlayResume.BackColor = Color.FromArgb(41, 41, 41);
            PlayResume.Image = Properties.Resources.Кнопка_играть;
            PlayResume.Name = "PlayResume";
            PlayResume.TabStop = false;
            PlayResume.Click += PlayResume_Click;
            // 
            // PictureOfSong
            // 
            resources.ApplyResources(PictureOfSong, "PictureOfSong");
            PictureOfSong.Name = "PictureOfSong";
            PictureOfSong.TabStop = false;
            // 
            // ListeningTime
            // 
            resources.ApplyResources(ListeningTime, "ListeningTime");
            ListeningTime.ForeColor = Color.FromArgb(82, 185, 104);
            ListeningTime.Name = "ListeningTime";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(pictureBox2, "pictureBox2");
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = Properties.Resources._;
            pictureBox2.Name = "pictureBox2";
            pictureBox2.TabStop = false;
            pictureBox2.MouseClick += pictureBox2_Click;
            pictureBox2.MouseMove += PictureBox_MouseMove;
            // 
            // button3
            // 
            resources.ApplyResources(button3, "button3");
            button3.BackColor = Color.FromArgb(82, 185, 104);
            button3.ForeColor = Color.FromArgb(18, 18, 18);
            button3.Name = "button3";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // Settings
            // 
            resources.ApplyResources(Settings, "Settings");
            Settings.BackColor = Color.FromArgb(18, 18, 18);
            Settings.ImageScalingSize = new Size(20, 20);
            Settings.Items.AddRange(new ToolStripItem[] { LanguageSettings, toolStripSeparator1, CloseContextMenu });
            Settings.Name = "Settings";
            Settings.RenderMode = ToolStripRenderMode.System;
            Settings.ShowImageMargin = false;
            // 
            // LanguageSettings
            // 
            resources.ApplyResources(LanguageSettings, "LanguageSettings");
            LanguageSettings.BackColor = Color.FromArgb(18, 18, 18);
            LanguageSettings.DropDownItems.AddRange(new ToolStripItem[] { toolStripTextBox1, toolStripSeparator2, SetUkr, SetEng, SetRus });
            LanguageSettings.ForeColor = Color.FromArgb(82, 185, 104);
            LanguageSettings.Name = "LanguageSettings";
            // 
            // toolStripTextBox1
            // 
            resources.ApplyResources(toolStripTextBox1, "toolStripTextBox1");
            toolStripTextBox1.BackColor = Color.FromArgb(18, 18, 18);
            toolStripTextBox1.BorderStyle = BorderStyle.None;
            toolStripTextBox1.ForeColor = Color.FromArgb(82, 185, 104);
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.ReadOnly = true;
            toolStripTextBox1.ShortcutsEnabled = false;
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // SetUkr
            // 
            resources.ApplyResources(SetUkr, "SetUkr");
            SetUkr.BackColor = Color.FromArgb(18, 18, 18);
            SetUkr.ForeColor = Color.FromArgb(82, 185, 104);
            SetUkr.Name = "SetUkr";
            SetUkr.Click += SetUkr_Click;
            // 
            // SetEng
            // 
            resources.ApplyResources(SetEng, "SetEng");
            SetEng.BackColor = Color.FromArgb(18, 18, 18);
            SetEng.ForeColor = Color.FromArgb(82, 185, 104);
            SetEng.Name = "SetEng";
            SetEng.Click += SetEng_Click;
            // 
            // SetRus
            // 
            resources.ApplyResources(SetRus, "SetRus");
            SetRus.BackColor = Color.FromArgb(18, 18, 18);
            SetRus.ForeColor = Color.FromArgb(82, 185, 104);
            SetRus.Name = "SetRus";
            SetRus.Click += SetRus_Click;
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // CloseContextMenu
            // 
            resources.ApplyResources(CloseContextMenu, "CloseContextMenu");
            CloseContextMenu.ForeColor = Color.Red;
            CloseContextMenu.Name = "CloseContextMenu";
            // 
            // pictureBox5
            // 
            resources.ApplyResources(pictureBox5, "pictureBox5");
            pictureBox5.Image = Properties.Resources.Кнопка_включения_;
            pictureBox5.Name = "pictureBox5";
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox5_Click;
            pictureBox5.MouseMove += PictureBox_MouseMove;
            // 
            // Form3
            // 
            resources.ApplyResources(this, "$this");
            BackColor = Color.FromArgb(18, 18, 18);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox2);
            Controls.Add(ListeningTime);
            Controls.Add(panel1);
            Controls.Add(ComboBox2);
            Controls.Add(ComboBox1);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(listView1);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(labelUserName);
            Name = "Form3";
            Load += Form3_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PlayResume).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureOfSong).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            Settings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        public void ElementsOrder()
        {
            pictureBox1.Location = new Point(5,5);
            labelUserName.Location = new Point(pictureBox1.Width, 0);
            pictureBox2.Size = new Size(pictureBox1.Width, labelUserName.Height);
            pictureBox2.Location = new Point(labelUserName.Location.X + labelUserName.Width, 0);
            PictureOfSong.Location = new Point((panel1.Width - PictureOfSong.Width) / 2,
                                   panel1.Height - PictureOfSong.Height - 135);

            // Выравнивание label1 по центру PictureOfSong
            int label1X = PictureOfSong.Location.X + (PictureOfSong.Width - label1.Width) / 2;
            int label1Y = PictureOfSong.Location.Y + PictureOfSong.Height + 10; // Подбирайте нужное смещение по вертикали
            label1.Location = new Point(label1X, label1Y);

            // Выравнивание ListeningTime по центру PictureOfSong с учетом высоты label1
            int listeningTimeX = panel1.Location.X + (panel1.Width - ListeningTime.Width) / 2;
            int listeningTimeY = panel1.Location.Y - ListeningTime.Height ; // Смещение ниже label1
            ListeningTime.Location = new Point(listeningTimeX, listeningTimeY);
            int playResumeX = (panel1.Width - PlayResume.Width) / 2;
            int playResumeY = label1.Location.Y + 50; 
            PlayResume.Location = new Point(playResumeX, playResumeY);
            pictureBox3.Location = new Point(playResumeX - 60, playResumeY);
            pictureBox4.Location = new Point(playResumeX + 60, playResumeY);
            pictureBox5.Location = new Point(ClientSize.Width - pictureBox5.Width-5, 5);
            trackState.Location = new Point((panel1.Width - trackState.Width) /2, playResumeY+35);
            label2.Location = new Point((panel1.Width - label2.Width) / 2, trackState.Location.Y + 16);
        }

        public void newText()
        { 
            label1.AutoEllipsis = true;
            //label1.AutoSize = true;
            ElementsOrder();
            label1.MaximumSize = new Size(label1.Width, label1.Height+label1.Height);
            label1.AutoEllipsis = false;
            label1.AutoSize = false;

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

        private PictureBox pictureBox1;
        private Button button1;
        private ListView listView1;
        private Button button2;
        private RJComboBox ComboBox2;
        private Label label1;
        private Button button4;
        private Button button5;
        private Button button6;
        private RJComboBox ComboBox1;
        private Panel panel1;
        private PictureBox PictureOfSong;
        private Label ListeningTime;
        private PictureBox pictureBox2;
        private PictureBox PlayResume;
        private Button button3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private ContextMenuStrip Settings;
        private ToolStripMenuItem LanguageSettings;
        private ToolStripMenuItem CloseContextMenu;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem SetUkr;
        private ToolStripMenuItem SetEng;
        private ToolStripMenuItem SetRus;
        private PictureBox pictureBox5;
        private Label label2;
        private RJProgressBar trackState;
    }

    
}
