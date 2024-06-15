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
            AddLoan a = new AddLoan(_id, lOANBindingSource);
            a.ShowDialog();
        }

        private void LoanForm_Load(object sender, EventArgs e)
        {
            var clientLoans = _c.LOANs.Where(loan => loan.ClientId == _id).ToList();
            lOANBindingSource.DataSource = clientLoans;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var clientLoans = _c.LOANs.Where(loan => loan.ClientId == _id).ToList();
            dataGridView1.DataSource = clientLoans;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            _id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
        }
    }
}
