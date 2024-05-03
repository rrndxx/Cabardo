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
        private readonly BindingSource _bsource;
        private readonly ambotEntities2 _c = new ambotEntities2();
        public AddLoan(int id, BindingSource bsource)
        {
            InitializeComponent();
            _id = id;
            _bsource = bsource;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var loan = _c.LOANs.FirstOrDefault(q => q.ClientId == _id);

            if (loan == null)
            {
                loan = new LOAN();
                loan.ClientId = _id;
                _c.LOANs.Add(loan);
            }

            loan.LoanAmount = int.Parse(textBox1.Text.Trim());
            loan.Interest = int.Parse(textBox2.Text.Trim());
            loan.Term = (string)comboBox1.SelectedValue;
            loan.Number = int.Parse(textBox3.Text.Trim());
            loan.Deduction = int.Parse(textBox4.Text.Trim());
            loan.InterestedAmount = int.Parse(textBox5.Text.Trim());
            loan.ReceivedAmount = int.Parse(textBox5.Text.Trim());
            loan.TotalPayable = int.Parse(textBox7.Text.Trim());
            loan.Status = "UNPAID";

            _c.SaveChanges();
            this.Close();
        }
        private void CalculateDueDate()
        {
            string selectedTerm = (string)comboBox1.SelectedValue;
            int termDuration;

            switch (selectedTerm)
            {
                case "Daily":
                    termDuration = int.Parse(textBox3.Text.Trim());
                    break;
                case "Weekly":
                    termDuration = int.Parse(textBox3.Text.Trim()) * 7;
                    break;
                case "Monthly":
                    termDuration = int.Parse(textBox3.Text.Trim()) * 30;
                    break;
                default:
                    termDuration = 0;
                    break;
            }

            DateTime dueDate = DateTime.Today.AddDays(termDuration);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            CalculateDueDate(); 
        }
    }
}
