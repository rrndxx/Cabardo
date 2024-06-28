using System;
using System.Linq;
using System.Windows.Forms;

namespace Cabardo
{
    public partial class MainDashboard : Form
    {
        private Form activeForm = null;

        public MainDashboard()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Main(guna2Panel2));
        }

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm.Dispose();
            }

            activeForm = childForm;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            childForm.BringToFront();
            guna2Panel2.Controls.Clear();
            guna2Panel2.Controls.Add(childForm);
            childForm.Show();
        }
    }
}
