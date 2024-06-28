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
    public partial class Edit : Form
    {
        private readonly ambotEntities2 _c = new ambotEntities2();
        private readonly BindingSource _bSource;
        private int _id;
        public Edit()
        {
            InitializeComponent();
        }
        public Edit(int id, BindingSource bSource) : this()
        {
            _bSource = bSource;
            _id = id;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox2.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox3.Text.Trim()))
            {
                MessageBox.Show("ERROR");
            }
            else
            {
                var c = _c.CLIENTs.Where(q => q.Id == _id).FirstOrDefault();
                c.Firstname = guna2TextBox1.Text.Trim();
                c.Lastname = guna2TextBox2.Text.Trim();
                c.Residency = guna2TextBox3.Text.Trim();
                c.Birthday = gunaDateTimePicker1.Value;

                _c.SaveChanges();

                guna2TextBox1.Text = " ";
                guna2TextBox2.Text = " ";
                guna2TextBox3.Text = " ";

                _bSource.DataSource = _c.CLIENTs.ToList();
                this.Close();
            }
    }
    private void Edit_Load(object sender, EventArgs e)
        {
            var client = _c.CLIENTs.Where(q => q.Id == _id).FirstOrDefault();
            guna2TextBox1.Text = client.Firstname;
            guna2TextBox2.Text = client.Lastname;
            guna2TextBox3.Text = client.Residency;
            gunaDateTimePicker1.Value = client.Birthday;
        }
    }
}
