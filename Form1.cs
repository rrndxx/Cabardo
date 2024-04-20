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
    public partial class Form1 : Form
    {
        private ambotEntities1 _c = new ambotEntities1();
        private int id;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Add a = new Add (cLIENTBindingSource);
            a.ShowDialog();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cLIENTBindingSource.DataSource = _c.CLIENTs.ToList();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Edit ed = new Edit(id, cLIENTBindingSource);
            ed.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var c = _c.CLIENTs.Where(q => q.Id == id).FirstOrDefault();
            _c.CLIENTs.Remove(c);
            _c.SaveChanges();

            cLIENTBindingSource.DataSource = _c.CLIENTs.ToList();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var stext = textBox1.Text.Trim();
            cLIENTBindingSource.DataSource = _c.CLIENTs.Where(q => q.Firstname.Contains(stext)).ToList();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cLIENTBindingSource.DataSource = _c.CLIENTs.ToList();
        }
    }
}
