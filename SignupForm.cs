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
    public partial class SignupForm : Form
    {
        private readonly ambotEntities2 _c = new ambotEntities2();
        Panel parentpanel;
        public SignupForm()
        {
            InitializeComponent();
            parentpanel = panel1;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox2.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox3.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox4.Text.Trim()))
            {
                MessageBox.Show("ERROR");
            }
            else
            {
                string fname = guna2TextBox1.Text.Trim();
                string lname = guna2TextBox2.Text.Trim();
                string uname = guna2TextBox3.Text.Trim();
                string pass = guna2TextBox4.Text.Trim();
                string email = guna2TextBox5.Text.Trim();

                var a = _c.USERS.FirstOrDefault(q => q.Username == uname);
                if (a != null)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.");
                    return;
                }

                USER newUser = new USER
                {
                    FirstName = fname,
                    LastName = lname,
                    Username = uname,
                    Password = pass,
                    Email = email
                };

                _c.USERS.Add(newUser);
                _c.SaveChanges();
                MessageBox.Show("Sign-up successful");
                OpenChildFormInParentPanel(new Login());
                this.Close();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            OpenChildFormInParentPanel(new Login());
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
    }
}
