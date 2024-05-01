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
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into Student values(@sid,@sname,@sage,@semail)";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@sid", txtId.Text);
                cmd.Parameters.AddWithValue("@sname", txtname.Text);
                cmd.Parameters.AddWithValue("@sage", txtage.Text);
                cmd.Parameters.AddWithValue("@semail", txtemail.Text);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res >= 1)
                {
                    MessageBox.Show("Student added successfully");
                }


            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "update Student set sid=@sid,sname=@sname,sage=@sage,semail=@semail where sid=@sid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@sid", txtId.Text);
                cmd.Parameters.AddWithValue("@sname", txtname.Text);
                cmd.Parameters.AddWithValue("@sage", txtage.Text);
                cmd.Parameters.AddWithValue("@semail", txtemail.Text);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res >= 1)
                {
                    MessageBox.Show("Student Updated successfully");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Student where sid=@sid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@sid", txtId.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                      
                        txtname.Text = dr["sname"].ToString();
                        txtage.Text = dr["sage"].ToString();
                        txtemail.Text = dr["semail"].ToString();

                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btndelete_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "delete from Student where sid=@sid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@sid", txtId.Text);
                
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res >= 1)
                {
                    MessageBox.Show("Student deleted successfully");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Student";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(dr);
                dataGridView1.DataSource = table;

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
