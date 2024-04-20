﻿using Cabardo.Entities;
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
    public partial class Edit : Form
    {
        private readonly ambotEntities1 _c = new ambotEntities1();
        private readonly BindingSource _bSource;
        private int _id;
        public Edit()
        {
            InitializeComponent();
        }
        public Edit(int id, BindingSource bSource) : this()
        {
            _bSource = bSource;
            _id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) || string.IsNullOrEmpty(textBox2.Text.Trim()) || string.IsNullOrEmpty(textBox3.Text.Trim()))
            {
                MessageBox.Show("ERROR");
            }
            else
            {
                var c = _c.CLIENTs.Where(q => q.Id == _id).FirstOrDefault();
                c.Firstname = textBox1.Text.Trim();
                c.Lastname = textBox2.Text.Trim();
                c.Residency = textBox3.Text.Trim();
                c.Birthday = dateTimePicker1.Value;

                _c.SaveChanges();

                textBox1.Text = " ";
                textBox2.Text = " ";
                textBox3.Text = " ";

                _bSource.DataSource = _c.CLIENTs.ToList();
                this.Close();
            }
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            var client = _c.CLIENTs.Where(q => q.Id == _id).FirstOrDefault();
            textBox1.Text = client.Firstname;
            textBox2.Text = client.Lastname;
            textBox3.Text = client.Residency;
            dateTimePicker1.Value = client.Birthday;
        }
    }
}
