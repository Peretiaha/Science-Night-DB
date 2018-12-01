using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Курсач
{
    public partial class Form1 : Form
    {
        string SqlCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Maksy\Documents\CourseDB.mdf;Integrated Security=True;Connect Timeout=30";

        int Id = 0, UId = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_add_Click(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(SqlCon))
            {
                try
                {
                    con.Open();
                    if (button_add.Text == "Add")
                    {
                        SqlCommand sqlCmd = new SqlCommand("ContactAddOrEditStudents", con);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@mode", "Add");
                        sqlCmd.Parameters.AddWithValue("@Id", 0);
                        sqlCmd.Parameters.AddWithValue("@Surname", textBox_surname.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Name", textBox_name.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Birthday", dateTimePicker.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Gender", comboBox_gender.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@University", comboBox_univer.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Email", textBox_email.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Teacher", textBox_teacher.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Tent_num", textBox_tent.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Experiment", textBox_exp.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Points", textBox_points.Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Saved Successful!!!!");
                    }
                    else
                    {
                        SqlCommand sqlCmd = new SqlCommand("ContactAddOrEditStudents", con);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@mode", "Edit");
                        sqlCmd.Parameters.AddWithValue("@Id", Id);
                        sqlCmd.Parameters.AddWithValue("@Surname", textBox_surname.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Name", textBox_name.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Birthday", dateTimePicker.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Gender", comboBox_gender.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@University", comboBox_univer.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Email", textBox_email.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Teacher", textBox_teacher.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Tent_num", textBox_tent.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Experiment", textBox_exp.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Points", textBox_points.Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Update Successful!!!!");
                    }

                    Reset();
                    FillDataGridView();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
            void FillDataGridView()
            {
            using (SqlConnection con = new SqlConnection(SqlCon))
            {
                if (label_title.Text == "Students")
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("ContactViewOrSearch", SqlCon);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("@Surname", textBox_search.Text.Trim());
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;
                    dataGridView1.Columns[0].Visible = false;
                }
                else if (label_title.Text == "University")
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("ContactViewOrSearchUniver", SqlCon);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("@University_name", textBox_search.Text.Trim());
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;
                    dataGridView1.Columns[0].Visible = false;

                }
            }

            }

        

        private void button_search_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                if (label_title.Text == "Student")
                {
                    Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    textBox_surname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textBox_name.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    dateTimePicker.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    comboBox_gender.SelectedItem = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    comboBox_univer.SelectedItem = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    textBox_email.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    textBox_teacher.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    textBox_tent.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    textBox_exp.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                    textBox_points.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                    button_add.Text = "Update";
                    button_delete.Enabled = true;
                }
                else if (label_title.Text=="University")
                {
                    //UId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());


                }
            }


        }

        void Reset()
        {
            textBox_surname.Text = textBox_name.Text = textBox_email.Text = textBox_teacher.Text = textBox_tent.Text = textBox_exp.Text = textBox_points.Text = "";
            comboBox_gender.SelectedItem = comboBox_univer.SelectedItem ="";
            dateTimePicker.Text = "";
            button_add.Text = "Add";
            Id = 0;
            button_delete.Enabled = false;
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            Reset();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Reset();
            FillDataGridView();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SqlCon))
                {
                    con.Open();
                    SqlCommand sqlCmd = new SqlCommand("ContactDelete", con);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@Id", Id);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Delete Successful!!!!");
                    Reset();
                    FillDataGridView();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_students_Click(object sender, EventArgs e)
        {
            label_title.Text = "Students";
            panel_univer.Visible = false;
            panel_students.Visible = true;
            
            
        }

        private void button_univer_Click(object sender, EventArgs e)
        {
            label_title.Text = "University";
            panel_students.Visible = false;
            panel_univer.Visible = true;
            
           
        }

        private void button_add_univer_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(SqlCon))
            {
                try
                {
                    con.Open();
                    SqlCommand sqlCmd = new SqlCommand("ContactAddOrEditUniver", con);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Add");
                    sqlCmd.Parameters.AddWithValue("@UId", 0);
                    sqlCmd.Parameters.AddWithValue("@University_name", textBox_univer_name.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Location", textBox_location.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Add  Successful");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
