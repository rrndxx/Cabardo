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
        
        private void CalculateDueDate()
        {
            if (int.TryParse(guna2TextBox4.Text.Trim(), out int termDuration))
            {
                string selectedTerm = gunaComboBox1.SelectedItem.ToString();
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
                        daysInTerm = termDuration * 30;
                        break;
                    default:
                        MessageBox.Show("Invalid term.");
                        return;
                }
                DateTime dueDate = DateTime.Today.AddDays(daysInTerm + 1);

                gunaDateTimePicker1.Value = dueDate;
            }
            else
            {
                MessageBox.Show("Invalid term duration.");
            }
        }
        private void CalculateInterest()
        {
            if (float.TryParse(guna2TextBox1.Text.Trim(), out float lamount) && float.TryParse(guna2TextBox2.Text.Trim(), out float interest))
            {
                float intamount = lamount * (interest / 100);

                guna2TextBox6.Text = intamount.ToString();
            }
        }
        private void CalculatePayable()
        {
            if (float.TryParse(guna2TextBox1.Text.Trim(), out float loanAmount) && float.TryParse(guna2TextBox2.Text.Trim(), out float interest) && int.TryParse(guna2TextBox4.Text.Trim(), out int termDuration))
            {
                float totalPayable = (loanAmount * (interest / 100) * termDuration) + loanAmount;

                guna2TextBox8.Text = totalPayable.ToString();
            }
        }
        private void CalculateReceivable()
        {
            if (float.TryParse(guna2TextBox1.Text.Trim(), out float loanAmount) && float.TryParse(guna2TextBox5.Text.Trim(), out float deduct))
            {
                float receivable = loanAmount - deduct;

                guna2TextBox7.Text = receivable.ToString();
            }
        }
        private void PaymentSched()
        {
            List<PaymentDetail> paymentDetails = new List<PaymentDetail>();
            DateTime currentDate = DateTime.Now;
            int interval = 0;

            switch (gunaComboBox1.SelectedItem)
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

            float paymentAmount = float.Parse(guna2TextBox8.Text.Trim()) / int.Parse(guna2TextBox4.Text.Trim());

            for (int i = 0; i < int.Parse(guna2TextBox4.Text.Trim()); i++)
            {
                currentDate = currentDate.AddDays(interval);
                PAYMENTSCHEDULE p = new PAYMENTSCHEDULE();
                p.PaymentID = ID;
                p.Payment = paymentAmount;
                p.Date = currentDate;
                p.Status = "UNPAID";
                p.Term = gunaComboBox1.SelectedItem.ToString();
                p.TotalPayable = float.Parse(guna2TextBox8.Text.Trim());
                p.Number = int.Parse(guna2TextBox4.Text.Trim());
                p.LoanDate = DateTime.Now;

                _c.PAYMENTSCHEDULEs.Add(p);
            }
            _c.SaveChanges();
        }
        public class PaymentDetail
        {
            public DateTime PaymentDate { get; set; }
            public float Amount { get; set; }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox1.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox6.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox4.Text.Trim())
                || string.IsNullOrEmpty(guna2TextBox5.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox7.Text.Trim()) || string.IsNullOrEmpty(guna2TextBox8.Text.Trim()) || string.IsNullOrEmpty(gunaComboBox1.Text.Trim())
                || string.IsNullOrEmpty(gunaDateTimePicker1.Text.Trim()))
            {
                MessageBox.Show("Please fill in all fields.");
            }

            LOAN l = new LOAN();

            l.LoanAmount = float.Parse(guna2TextBox1.Text.Trim());
            l.Interest = float.Parse(guna2TextBox2.Text.Trim());
            l.Term = gunaComboBox1.SelectedItem.ToString();
            l.Number = int.Parse(guna2TextBox4.Text.Trim());
            l.Deduction = float.Parse(guna2TextBox5.Text.Trim());
            l.InterestedAmount = float.Parse(guna2TextBox6.Text.Trim());
            l.ReceivedAmount = float.Parse(guna2TextBox7.Text.Trim());
            l.Status = "UNPAID";
            l.ClientId = _id;
            l.LoanDate = DateTime.Now;

            CalculateDueDate();
            l.DueDate = (DateTime)gunaDateTimePicker1.Value;

            CalculateInterest();
            l.InterestedAmount = float.Parse(guna2TextBox6.Text.Trim());

            CalculatePayable();
            l.TotalPayable = float.Parse(guna2TextBox8.Text.Trim());

            _c.LOANs.Add(l);
            _c.SaveChanges();

            _bsource.DataSource = _c.LOANs.Where(loan => loan.ClientId == _id).ToList();
            ID = l.Id;
            PaymentSched();
            this.Close();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            CalculatePayable();
            CalculateReceivable();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            CalculateInterest();
            CalculatePayable();
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            CalculateDueDate();
            CalculatePayable();
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            CalculateReceivable();
        }
    }
}
