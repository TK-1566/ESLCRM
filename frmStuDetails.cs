using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace COMS_276_Final_Project
{
    public partial class frmStuDetails : Form
    {
        DataTable dtUpdate;
        public frmStuDetails()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public frmStuDetails(int StudentID)
        {
            InitializeComponent();
            loadStudents(StudentID);
        }
        /// <summary>
        /// loads student information into corresponding fields
        /// </summary>
        /// <param name="ID"></param>
        private void loadStudents(int ID)
        {
            //connect to class object
            clsData Students = new clsData();
            //pass SQL to equipment
            Students.SQL = "SELECT ID, LastName, GivenName, Address, Phone, Email, CurrentClass, StartDate, EndDate, BirthDate, Notes FROM tblStuInfo WHERE ID = " + ID;
            //get values from datatable and display to screen
            txtLastName.Tag = ID.ToString(); //capture unique id of equipment
            txtLastName.Text = Students.dt.Rows[0]["LastName"].ToString();
            txtGivenName.Text = Students.dt.Rows[0]["GivenName"].ToString();
            txtAddress.Text = Students.dt.Rows[0]["Address"].ToString();
            txtPhone.Text = Students.dt.Rows[0]["Phone"].ToString();
            txtEmail.Text = Students.dt.Rows[0]["Email"].ToString();
            txtCurrentClass.Text = Students.dt.Rows[0]["CurrentClass"].ToString();

            dtpStart.Text = Students.dt.Rows[0]["StartDate"].ToString();
            dtpEnd.Text = Students.dt.Rows[0]["EndDate"].ToString();
            dtpBirth.Text = Students.dt.Rows[0]["BirthDate"].ToString();

            txtNotes.Text = Students.dt.Rows[0]["Notes"].ToString();
            //get information update
            dtUpdate = Students.dt;
        }
        /// <summary>
        /// update changes to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //validate information
            if (txtLastName.Text == "")
            {
                MessageBox.Show("Error: Enter Last Name: ");
                txtLastName.Focus();
            }
            else if (txtGivenName.Text == "")
            {
                MessageBox.Show("Error: Enter Given Name: ");
                txtGivenName.Focus();
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Error: Enter E-mail: ");
                txtEmail.Focus();
            }
            else
            {
                //verify existing customer
                if (txtLastName.Tag.ToString() != "N")
                {
                    clsData myUpdateData = new clsData();
                    //update local datatable
                    dtUpdate.Rows[0]["LastName"] = txtLastName.Text;
                    dtUpdate.Rows[0]["GivenName"] = txtGivenName.Text;
                    dtUpdate.Rows[0]["Address"] = txtAddress.Text;
                    dtUpdate.Rows[0]["Phone"] = txtPhone.Text;
                    dtUpdate.Rows[0]["Email"] = txtEmail.Text;
                    dtUpdate.Rows[0]["CurrentClass"] = txtCurrentClass.Text;

                    dtUpdate.Rows[0]["StartDate"] = dtpStart.Text;
                    dtUpdate.Rows[0]["EndDate"] = dtpEnd.Text;
                    dtUpdate.Rows[0]["BirthDate"] = dtpBirth.Text;           

                    dtUpdate.Rows[0]["Notes"] = txtNotes.Text;
                    //send update to database
                    myUpdateData.UpdateData(dtUpdate, "SELECT ID, LastName, GivenName, Address, Phone, Email, CurrentClass, StartDate, EndDate, BirthDate, Notes FROM tblStuInfo WHERE ID = " + int.Parse(txtLastName.Tag.ToString()));
                }
                //assume new student
                else
                {
                    //create new instance of clsData
                    clsData myNewData = new clsData();
                    //call update method, passing values
                    myNewData.CreateRecord(txtLastName.Text, txtGivenName.Text, txtAddress.Text, txtPhone.Text, txtEmail.Text, txtCurrentClass.Text, dtpStart.Value, dtpEnd.Value, dtpBirth.Value, txtNotes.Text);
                }
            }
            //data update. close form
            this.Close();
        }
    }
}
