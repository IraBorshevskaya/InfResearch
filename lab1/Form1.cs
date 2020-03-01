using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DB1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=Ira1234567890;Database=InfResearch;");
            conn.Open();
            string name = textBox1.Text;
            string name1 = "%" + name + "%";
            var input = name1.Replace(" ", "% %");
            NpgsqlParameter nameParam = new NpgsqlParameter("@input", input);
            NpgsqlCommand command = new NpgsqlCommand("select * from movies " +
                                                        "WHERE CAST(year as TEXT) LIKE @input " +
                                                        "or name like @input " +
                                                        "LIMIT 10", conn);
            command.Parameters.Add(nameParam);
            try
            {
                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    int rowNumber = dataGridView1.Rows.Add();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        dataGridView1.Rows[rowNumber].Cells["ID"].Value = dr[0];
                        dataGridView1.Rows[rowNumber].Cells["name"].Value = dr[1];
                        dataGridView1.Rows[rowNumber].Cells["year"].Value = dr[2];
                    }
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
