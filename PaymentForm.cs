using Cabardo.Entities;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Cabardo
{
    public partial class PaymentForm : Form
    {
        private readonly ambotEntities2 _c = new ambotEntities2();
        private readonly BindingSource _bSource;
        private int _id;
        private PAYMENTSCHEDULE _paymentSchedule;

        public PaymentForm()
        {
            InitializeComponent();
        }

        public PaymentForm(int ID, BindingSource bsource) : this()
        {
            _id = ID;
            _bSource = bsource;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            _paymentSchedule = _c.PAYMENTSCHEDULEs.FirstOrDefault(p => p.Id == _id);
            if (_paymentSchedule == null)
            {
                MessageBox.Show("Payment schedule not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (_paymentSchedule == null)
            {
                MessageBox.Show("Payment schedule not loaded properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(guna2TextBox1.Text.Trim()) || !double.TryParse(guna2TextBox1.Text.Trim(), out double amountPaid))
            {
                MessageBox.Show("Please enter a valid amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double newPaymentAmount = (double)(_paymentSchedule.Payment - amountPaid);

            if (newPaymentAmount <= 0)
            {
                _paymentSchedule.Payment = 0;
                _paymentSchedule.Status = "PAID";
            }
            else
            {
                _paymentSchedule.Payment = newPaymentAmount;
            }

            _c.SaveChanges();

            MessageBox.Show("Payment successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _bSource.DataSource = _c.PAYMENTSCHEDULEs.ToList();
            this.Close();
        }
    }
}
