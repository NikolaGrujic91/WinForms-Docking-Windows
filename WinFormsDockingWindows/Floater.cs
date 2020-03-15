using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsDockingWindows
{
    public partial class Floater : Form
    {
        #region Fields

        private readonly Timer timerDock;
        private Point dockTestAt;
        private bool isDocked;

        #endregion

        #region Constructors

        public Floater()
        {
            this.InitializeComponent();

            this.components = new System.ComponentModel.Container();
            this.timerDock = new Timer(this.components) {Interval = 10};
            this.timerDock.Tick += this.TimerDockTick;

            this.isDocked = false;
        }

        #endregion

        #region Events

        private void TimerDockTick(object sender, EventArgs e)
        {
            if (this.dockTestAt.X == this.Owner.PointToClient(this.Location).X && 
                this.dockTestAt.Y == this.Owner.PointToClient(this.Location).Y)
            {
                if (MouseButtons != MouseButtons.None)
                {
                    return;
                }

                this.SizeChanged -= this.Floater_SizeChanged;
                this.timerDock.Enabled = false;
                this.isDocked = true;
                ((Form1)this.Owner).DockControl(this);
                this.SizeChanged += this.Floater_SizeChanged;
            }
            else
            {
                // Mouse has moved. Disable this dock attempt.
                this.timerDock.Enabled = false;
                ((Form1)this.Owner).DrawDockRectangle = false;

            }
        }

        private void Floater_Move(object sender, EventArgs e)
        {
            // Determine the current location in parent form coordinates.
            Point mouseAt = this.Owner.PointToClient(this.Location);

            // Determine if the floated is close enough to dock horizontaly.
            if (mouseAt.X >= ((Form1)this.Owner).DockPanelWidth || mouseAt.X <= -5)
            {
                return;
            }

            // Determine if the floated is close enough to dock vertically.
            if (mouseAt.Y >= ((Form1)this.Owner).DockPanelHeight || mouseAt.Y <= -5)
            {
                return;
            }

            if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left)
            {
                return;
            }

            this.dockTestAt = mouseAt;

            // Show the dock focus rectangle.
            ((Form1)this.Owner).DrawDockRectangle = true;

            // Reset the timer to poll for the MouseUp event.
            this.timerDock.Enabled = false;
            this.timerDock.Enabled = true;
        }

        private void Floater_SizeChanged(object sender, EventArgs e)
        {
            if (!this.isDocked)
            {
                return;
            }

            this.Move -= this.Floater_Move;
            this.timerDock.Enabled = false;
            this.isDocked = false;
            ((Form1)this.Owner).UndockControl(this);
            this.Move += this.Floater_Move;
        }

        #endregion
    }
}
