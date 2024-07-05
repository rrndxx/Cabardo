using Cabardo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cabardo
{
    public partial class Login : Form
    {
        private ambotEntities2 _c = new ambotEntities2();
        Panel parentpanel;
        public Login()
        {
            InitializeComponent();
            parentpanel = panel1;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string username = guna2TextBox10.Text;
            string password = guna2TextBox9.Text;

            var user = _c.USERS.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                MainDashboard m = new MainDashboard();
                m.ShowDialog();
            }
            else
            {
                MessageBox.Show("Username or password is incorrect");
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            OpenChildFormInParentPanel(new SignupForm());
        }
        private void OpenChildFormInParentPanel(Form childForm)
        {
            if (parentpanel.Controls.Count > 0)
            {
                foreach (Control ctrl in parentpanel.Controls)
                {
                    if (ctrl is Form form)
                    {
                        form.Close();
                        form.Dispose();
                    }
                }
                parentpanel.Controls.Clear();
            }

            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            childForm.BringToFront();
            parentpanel.Controls.Add(childForm);
            childForm.Show();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            guna2TextBox10.Text = "";
            guna2TextBox9.Text = "";
        }
    }
}
