using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Studentcrudbywindowsform
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;
        public Form2()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        private DataSet GetAllStudents()
        {
            string qry = "select * from Student";
            da = new SqlDataAdapter(qry, con);

            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            scb = new SqlCommandBuilder(da);
            ds = new DataSet();

            da.Fill(ds, "stud");
            return ds;

        }
        private void ClearFileds()
        {
            txtId.Clear();
            txtname.Clear();
            txtage.Clear();
            txtemail.Clear();


        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudents();
                DataRow row = ds.Tables["stud"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    txtname.Text = row["sname"].ToString();
                    txtage.Text = row["sage"].ToString();
                    txtemail.Text = row["semail"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudents();

                DataRow row = ds.Tables["stud"].NewRow();
                row["sid"] = txtId.Text;
                row["sname"] = txtname.Text;
                row["sage"] = txtage.Text;
                row["semail"] = txtemail.Text;


                ds.Tables["stud"].Rows.Add(row);
                int result = da.Update(ds.Tables["stud"]);
                if (result >= 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudents();
                DataRow row = ds.Tables["stud"].Rows.Find(txtId.Text);
                if (row != null)
                {

                    row["sname"] = txtname.Text;
                    row["sage"] = txtage.Text;
                    row["semail"] = txtemail.Text;

                    int result = da.Update(ds.Tables["stud"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record updated");
                        ClearFileds();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudents();
                DataRow row = ds.Tables["stud"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["stud"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record deleted");
                       ClearFileds();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnshow_Click(object sender, EventArgs e)
        {

            ds = GetAllStudents();
            dataGridView1.DataSource = ds.Tables["stud"];

        }
    }
}
