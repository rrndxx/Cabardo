using Cabardo.Entities;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Cabardo
{
    public partial class Main : Form
    {
        private ambotEntities2 _c = new ambotEntities2();
        private int id = -1;
        private Panel _parentPanel;

        public Main(Panel parentPanel)
        {
            InitializeComponent();
            _parentPanel = parentPanel;
            gunaDataGridView1.SelectionChanged += new EventHandler(gunaDataGridView1_SelectionChanged);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cLIENTBindingSource.DataSource = _c.CLIENTs.ToList();
        }

        private void gunaDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (gunaDataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = gunaDataGridView1.SelectedRows[0];
                if (selectedRow.Cells[0].Value != null)
                {
                    id = Convert.ToInt32(selectedRow.Cells[0].Value);
                }
            }
            else
            {
                id = -1;
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Add a = new Add(cLIENTBindingSource);
            a.ShowDialog();
            RefreshClientData();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (id <= 0)
            {
                MessageBox.Show("Please select a client to edit.", "Edit Client", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Edit ed = new Edit(id, cLIENTBindingSource);
            ed.ShowDialog();
            RefreshClientData();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (id <= 0)
            {
                MessageBox.Show("Please select a client to delete.", "Delete Client", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var client = _c.CLIENTs.FirstOrDefault(q => q.Id == id);
            if (client == null)
            {
                MessageBox.Show("Client not found.", "Delete Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this client?", "Delete Client", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                _c.CLIENTs.Remove(client);
                _c.SaveChanges();
                RefreshClientData();
                id = -1;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var stext = guna2TextBox1.Text.Trim();
            if (!string.IsNullOrEmpty(stext))
            {
                cLIENTBindingSource.DataSource = _c.CLIENTs.Where(q => q.Firstname.Contains(stext)).ToList();
            }
            else
            {
                RefreshClientData();
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            guna2Button4_Click(sender, e);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (_parentPanel != null && id > 0)
            {
                OpenChildFormInParentPanel(new LoanForm(id));
            }
            else
            {
                MessageBox.Show("Please select a client to view loans.", "View Loan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OpenChildFormInParentPanel(Form childForm)
        {
            if (_parentPanel.Controls.Count > 0)
            {
                foreach (Control ctrl in _parentPanel.Controls)
                {
                    if (ctrl is Form form)
                    {
                        form.Close();
                        form.Dispose();
                    }
                }
                _parentPanel.Controls.Clear();
            }

            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            childForm.BringToFront();
            _parentPanel.Controls.Add(childForm);
            childForm.Show();
        }

        private void RefreshClientData()
        {
            cLIENTBindingSource.DataSource = _c.CLIENTs.ToList();
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gunaDataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = gunaDataGridView1.SelectedRows[0];
                if (selectedRow.Cells[0].Value != null)
                {
                    id = Convert.ToInt32(selectedRow.Cells[0].Value);
                }
            }
            else
            {
                id = -1;
            }
        }
    }
}
