using System;
using Cabardo.Entities;
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
    public partial class Add : Form
    {
        private readonly ambotEntities2 _c = new ambotEntities2();
        private readonly BindingSource _bSource;
        public Add()
        {
            InitializeComponent();
        }
        public Add(BindingSource bSource) : this()
        {
            _bSource = bSource;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox2.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox3.Text.Trim()))
            {
                MessageBox.Show("ERROR");
            }
            else
            {
                CLIENT c = new CLIENT();
                c.Firstname = guna2TextBox1.Text.Trim();
                c.Lastname = guna2TextBox2.Text.Trim();
                c.Residency = guna2TextBox3.Text.Trim();
                c.Birthday = gunaDateTimePicker1.Value;

                _c.CLIENTs.Add(c);
                _c.SaveChanges();

                guna2TextBox1.Text = "";
                guna2TextBox2.Text = "";
                guna2TextBox3.Text = "";

                _bSource.DataSource = _c.CLIENTs.ToList();
                this.Close();
            }
        }

    }
}
