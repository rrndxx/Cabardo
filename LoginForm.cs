﻿using System;
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
    public partial class LoginForm : Form
    {
        Panel parentpanel;
        public LoginForm()
        {
            InitializeComponent();
            parentpanel = panel1;
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
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
