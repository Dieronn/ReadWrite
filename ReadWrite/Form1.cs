using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadWrite
{
    public partial class Form1 : Form
    {
        DataTable table1 = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text files (.txt)|*txt|All files(*.*)|*.*";
            open.Title = "Open txt";
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader streamR = new StreamReader(open.FileName);
                    string line;
                    string[] values;

                    while (!streamR.EndOfStream)
                    {
                        line = streamR.ReadLine();
                        values = line.Split(',');
                        string[] row = new string[values.Length];

                        for (int j = 0; j < values.Length; j++)
                        {
                            row[j] = values[j].Trim();
                        }

                        table1.Rows.Add(row);                                              
                    }
                    streamR.Close();

                }
                catch
                {
                    MessageBox.Show("Unexpected error while opening");
                }
                lblCount.Text = "Count of rows " + dataGridView1.Rows.Count;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text files (.txt)|*.txt|All files|*.*";
            save.Title = "Table";
            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter streamW = new StreamWriter(save.FileName);
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {   
                            if(j!= dataGridView1.Columns.Count-1)
                            streamW.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + ",");
                            else streamW.Write(dataGridView1.Rows[i].Cells[j].Value.ToString());
                        }
                        streamW.WriteLine("");
                    }
                    streamW.Close();

                    MessageBox.Show("Saved");
                }
                catch
                {
                    MessageBox.Show("Unexpected error while saving");
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            table1.Rows.Add(txtHeader.Text, txtText.Text);
            txtHeader.Clear();
            txtText.Clear();
            lblCount.Text = "Count of rows " + dataGridView1.Rows.Count;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            table1.Rows[index].Delete();
            lblCount.Text = "Count of rows " + dataGridView1.Rows.Count;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtHeader.Clear();
            txtText.Clear();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            if (index >= 0)
            {
                txtHeader.Text = table1.Rows[index].ItemArray[0].ToString();
                txtText.Text = table1.Rows[index].ItemArray[1].ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            table1.Columns.Add("Header", typeof(string));
            table1.Columns.Add("Message", typeof(string));
            dataGridView1.DataSource = table1;
            dataGridView1.Columns["Header"].Width = 140;
            dataGridView1.Columns["Message"].Width = 140;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {   
            while (dataGridView1.Rows.Count > 0)
            {
                table1.Rows[0].Delete();
            }            
            lblCount.Text = "Count of rows " + dataGridView1.Rows.Count;
        }
    }
}
