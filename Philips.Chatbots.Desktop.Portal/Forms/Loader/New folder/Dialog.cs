using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Philips.Chatbots.Desktop.Portal
{
    public class FormLocation
    {
        public Point Location { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public partial class Dialog : Form
    {
        CancellationToken cancellationToken = new CancellationToken();
        Action action;
        Timer timer;

        public Dialog(string title, string message, string buttonText, Action action, FormLocation ownerLocation = null)
        {
            InitializeComponent();
            if (ownerLocation != null)
            {
                Location = new Point(ownerLocation.Location.X + ownerLocation.Width / 2 - Width / 2,
                    ownerLocation.Location.Y + ownerLocation.Height / 2 - Height / 2);
            }
            else
            {
                StartPosition = FormStartPosition.CenterScreen;
            }
            lblTitle.Text = title;
            lblMessage.Text = message;
            this.action = action;
            btnCancel.Text = buttonText;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void Dialog_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick -= new EventHandler(timer_Tick);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            await Task.Run(action, cancellationToken).ConfigureAwait(true);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = ((int.Parse(lblTimer.Text)) + 1).ToString();
        }

        private void btnBackground_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
