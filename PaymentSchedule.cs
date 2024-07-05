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
    public partial class PaymentSchedule : Form
    {
        private readonly ambotEntities2 _c = new ambotEntities2();
        private int _id;
        private double _payment;
        public PaymentSchedule(int ID): this()
        {
            _id = ID;
        }
        public PaymentSchedule()
        {
            InitializeComponent();
        }
        private void PaymentSchedule_Load(object sender, EventArgs e)
        {
            pAYMENTSCHEDULEBindingSource.DataSource = _c.PAYMENTSCHEDULEs.Where(PAYMENTSCHEDULE => PAYMENTSCHEDULE.PaymentID == _id).ToList();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (gunaDataGridView1.SelectedRows.Count == 0)
                return;

            _id = (int)gunaDataGridView1.SelectedRows[0].Cells[0].Value;
            _payment = Convert.ToDouble(gunaDataGridView1.SelectedRows[0].Cells[1].Value);

            PaymentForm p = new PaymentForm(_id, pAYMENTSCHEDULEBindingSource, _payment);
            p.ShowDialog();
        }
    }
}
