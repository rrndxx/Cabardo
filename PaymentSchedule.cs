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
    }
}
