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
    public partial class PaymentForm : Form
    {
        private readonly ambotEntities2 _c = new ambotEntities2();
        private readonly BindingSource _bSource;
        private int _id;
        private double _payment;
        private double newp;
        public PaymentForm()
        {
            InitializeComponent();
        }
        public PaymentForm(int id, BindingSource bsource, double payment): this()
        {
            _id = id;
            _bSource = bsource;
            _payment = payment;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text.Trim()) || !double.TryParse(guna2TextBox1.Text.Trim(), out double amountPaid))
            {
                MessageBox.Show("Please enter a valid amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            newp = _payment - amountPaid;
            var p = _c.PAYMENTSCHEDULEs.Where(q => q.Id == _id).FirstOrDefault();
            p.Payment = newp;
            if(newp <= 0)
            {
                p.Status = "PAID";
            }

            _c.SaveChanges();
            this.Close();
        }
        
        private void PaymentForm_Load(object sender, EventArgs e)
        {
            gunaLabel2.Text = _payment.ToString();
        }
    }
}
