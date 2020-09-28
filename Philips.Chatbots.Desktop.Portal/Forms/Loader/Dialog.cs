using Philips.Chatbots.Desktop.Portal.Forms.Loader;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Philips.Chatbots.Desktop.Portal
{
    public partial class Dialog : Form
    {
        private const int TickCount = 5;
        private Action action;
        private Timer timer;
        private FormLocation ownerLocation;
        private int curTick = 0;
        private string titleText;

        public Dialog(string title, string message, string buttonText, string hideButtonText, Action action, FormLocation ownerLocation = null)
        {
            InitializeComponent();
            this.Text = "";
            this.ownerLocation = ownerLocation;
            lblTitle.Text = titleText = title;
            lblMessage.Text = message;
            this.action = action;
            btnBackground.Text = buttonText;
            btnBackground.Text = hideButtonText;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void Dialog_Load(object sender, EventArgs e)
        {
            if (ownerLocation != null)
            {
                Location = new Point(ownerLocation.Location.X + ownerLocation.Width / 2 - Width / 2,
                    ownerLocation.Location.Y + ownerLocation.Height / 2 - Height / 2);
            }

            timer = new Timer();
            timer.Interval = 1000 / TickCount;
            timer.Tick -= new EventHandler(timer_Tick);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            await Task.Run(action).ConfigureAwait(true);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTitle.Text = $"{titleText}.{new string('.', curTick)}";
            if (++curTick == TickCount)
            {
                btnLoad.Text = ((int.Parse(btnLoad.Text)) + 1).ToString();
                curTick = 0;
            }
        }

        private void btnBackground_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
