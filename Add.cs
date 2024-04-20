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
        private readonly ambotEntities1 _c = new ambotEntities1();
        private readonly BindingSource _bSource;

        public Add()
        {
            InitializeComponent();
        }
        public Add(BindingSource bSource) : this()
        {
            _bSource = bSource;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) || string.IsNullOrEmpty(textBox2.Text.Trim()) || string.IsNullOrEmpty(textBox3.Text.Trim()))
            {
                MessageBox.Show("ERROR");
            }
            else
            {


                CLIENT c = new CLIENT();
                c.Firstname = textBox1.Text.Trim();
                c.Lastname = textBox2.Text.Trim();
                c.Residency = textBox3.Text.Trim();
                c.Birthday = dateTimePicker1.Value;

                _c.CLIENTs.Add(c);
                _c.SaveChanges();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

                _bSource.DataSource = _c.CLIENTs.ToList();
                this.Close();
            }
        }
    }
}
