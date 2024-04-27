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
    public partial class AddLoan : Form
    {
        private readonly int _id;
        private readonly ambotEntities2 _c = new ambotEntities2();
        public AddLoan(int id)
        {
            InitializeComponent();
            _id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LOAN l = new LOAN();
            l.LoanAmount = float.Parse(textBox1.Text.Trim());
            l.Interest = float.Parse(textBox2.Text.Trim());
            l.Term = (string)comboBox1.SelectedValue;
            l.Number = int.Parse(textBox3.Text.Trim());
            l.Deduction = float.Parse(textBox4.Text.Trim());
            l.InterestedAmount = float.Parse(textBox6.Text.Trim());
            l.ReceivedAmount = float.Parse(textBox5.Text.Trim());
            l.TotalPayable = float.Parse(textBox7.Text.Trim());
            l.DueDate = dateTimePicker1.Value;
            l.Status = "UNPAID";
            l.Id = _id;
            
            _c.LOANs.Add(l);
            _c.SaveChanges();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
