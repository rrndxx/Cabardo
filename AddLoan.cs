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
        int ID;
        private readonly BindingSource _bsource;
        private readonly ambotEntities2 _c = new ambotEntities2();
        public AddLoan()
        {
            InitializeComponent();
        }
        public AddLoan(int id, BindingSource bsource): this()
        {
            _id = id;
            _bsource = bsource;
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text.Trim()) || string.IsNullOrEmpty(textBox2.Text.Trim()) || string.IsNullOrEmpty(textBox3.Text.Trim()) || string.IsNullOrEmpty(textBox4.Text.Trim())
                || string.IsNullOrEmpty(textBox5.Text.Trim()) || string.IsNullOrEmpty(textBox6.Text.Trim()) || string.IsNullOrEmpty(textBox7.Text.Trim()) || string.IsNullOrEmpty(comboBox1.Text.Trim())
                || string.IsNullOrEmpty(dateTimePicker1.Text.Trim()))
            {
                MessageBox.Show("Please fill in all fields.");
            }

            LOAN l = new LOAN();

            l.LoanAmount = float.Parse(textBox1.Text.Trim());
            l.Interest = float.Parse(textBox2.Text.Trim());
            l.Term = comboBox1.SelectedItem.ToString();
            l.Number = int.Parse(textBox3.Text.Trim());
            l.Deduction = float.Parse(textBox4.Text.Trim());
            l.InterestedAmount = float.Parse(textBox5.Text.Trim());
            l.ReceivedAmount = float.Parse(textBox5.Text.Trim());
            l.Status = "UNPAID";
            l.ClientId = _id;
            l.LoanDate = DateTime.Now;

            CalculateDueDate();
            l.DueDate = (DateTime)dateTimePicker1.Value;

            CalculateInterest();
            l.InterestedAmount = float.Parse(textBox6.Text.Trim());

            CalculatePayable();
            l.TotalPayable = float.Parse(textBox7.Text.Trim());

            _c.LOANs.Add(l);
            _c.SaveChanges();

            _bsource.DataSource = _c.LOANs.Where(loan => loan.ClientId == _id).ToList();
            ID = l.Id;
            PaymentSched();
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CalculateDueDate()
        {
            if (int.TryParse(textBox3.Text.Trim(), out int termDuration))
            {
                string selectedTerm = comboBox1.SelectedItem.ToString();
                int daysInTerm = 0;

                switch (selectedTerm)
                {
                    case "Daily":
                        daysInTerm = termDuration;
                        break;
                    case "Weekly":
                        daysInTerm = termDuration * 7;
                        break;
                    case "Monthly":
                        daysInTerm = termDuration * 30; // Assuming 30 days per month
                        break;
                    default:
                        MessageBox.Show("Invalid term.");
                        return;
                }
                DateTime dueDate = DateTime.Today.AddDays(daysInTerm + 1);

                dateTimePicker1.Value = dueDate;
            }
            else
            {
                MessageBox.Show("Invalid term duration.");
            }
        }
        private void CalculateInterest()
        {
            if (float.TryParse(textBox1.Text.Trim(), out float lamount) && float.TryParse(textBox2.Text.Trim(), out float interest))
            {
                float intamount = lamount * (interest / 100);

                textBox6.Text = intamount.ToString();
            }
        }
        private void CalculatePayable()
        {
            if (float.TryParse(textBox1.Text.Trim(), out float loanAmount) && float.TryParse(textBox2.Text.Trim(), out float interest) && int.TryParse(textBox3.Text.Trim(), out int termDuration))
            {
                float totalPayable = (loanAmount * (interest / 100) * termDuration) + loanAmount;

                textBox7.Text = totalPayable.ToString();
            }
        }
        private void CalculateReceivable()
        {
            if (float.TryParse(textBox1.Text.Trim(), out float loanAmount) && float.TryParse(textBox4.Text.Trim(), out float deduct))
            {
                float receivable = loanAmount - deduct;

                textBox5.Text = receivable.ToString();
            }
        }
        private void PaymentSched()
        {
            List<PaymentDetail> paymentDetails = new List<PaymentDetail>();
            DateTime currentDate = DateTime.Now;
            int interval = 0;

            switch (comboBox1.SelectedItem)
            {
                case "Daily":
                    interval = 1;
                    break;
                case "Weekly":
                    interval = 7;
                    break;
                case "Monthly":
                    interval = 30;
                    break;
                default:
                    return;
            }

            float paymentAmount = float.Parse(textBox7.Text.Trim()) / int.Parse(textBox3.Text.Trim());

            for (int i = 0; i < int.Parse(textBox3.Text.Trim()); i++)
            {
                currentDate = currentDate.AddDays(interval);
                PAYMENTSCHEDULE p = new PAYMENTSCHEDULE();
                p.PaymentID = ID;
                p.Payment = paymentAmount;
                p.Date = currentDate;
                p.Status = "UNPAID";
                p.Term = comboBox1.SelectedItem.ToString();
                p.TotalPayable = float.Parse(textBox7.Text.Trim());
                p.Number = int.Parse(textBox3.Text.Trim());
                p.LoanDate = DateTime.Now;

                _c.PAYMENTSCHEDULEs.Add(p);
            }
            _c.SaveChanges();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CalculatePayable();
            CalculateReceivable();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CalculateInterest();
            CalculatePayable();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            CalculateDueDate();
            CalculatePayable();
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            CalculateReceivable();
        }
        public class PaymentDetail
        {
            public DateTime PaymentDate { get; set; }
            public float Amount { get; set; }
        }   
    }
}
