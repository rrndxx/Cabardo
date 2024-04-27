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
    public partial class LoanForm : Form
    {
        private readonly ambotEntities2 _c = new ambotEntities2();
        private int _id;
        public LoanForm()
        {
            InitializeComponent();
        }
        public LoanForm(int id) : this()
        { 
            _id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddLoan a = new AddLoan(_id);
            a.ShowDialog();
        }

        private void LoanForm_Load(object sender, EventArgs e)
        {
            var clientLoans = _c.LOANs.Where(loan => loan.Id == _id).ToList();
            dataGridView1.DataSource = clientLoans;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main m = new Main();
            m.ShowDialog();
        }
    }
}
