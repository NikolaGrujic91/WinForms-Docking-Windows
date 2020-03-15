using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsDockingWindows
{
    public partial class Form1 : Form
    {
        #region Constructors

        public Form1()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Events

        private void Form1_Load(object sender, EventArgs e)
        {
            var formFloat = new Floater {Owner = this};
            formFloat.Show();
        }

        private void dockPanel_Paint(object sender, PaintEventArgs e)
        {
            if (this.dockPanel.Controls.Count != 0)
            {
                return;
            }

            var dockCueBrush = new HatchBrush(HatchStyle.Cross,
                                              Color.White,
                                              Color.Gray);
            var dockCuePen = new Pen(dockCueBrush, 5);
            e.Graphics.DrawRectangle(dockCuePen, new Rectangle(0, 0, dockPanel.Width, dockPanel.Height));
        }

        #endregion

        #region Properties

        public bool DrawDockRectangle
        {
            get { return dockPanel.Visible; }
            set { dockPanel.Visible = value; }
        }

        public float DockPanelWidth
        {
            get { return this.dockPanel.Width; }
        }

        public float DockPanelHeight
        {
            get { return this.dockPanel.Height; }
        }

        #endregion

        #region Methods

        public void DockControl(Form form)
        {
            form.TopLevel = false;
            this.dockPanel.Controls.Add(form);
            form.WindowState = FormWindowState.Maximized;
        }

        public void UndockControl(Form form)
        {
            this.dockPanel.Controls.Remove(form);
            form.TopLevel = true;
            form.WindowState = FormWindowState.Normal;
        }

        #endregion
    }
}
