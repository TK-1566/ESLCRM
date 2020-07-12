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
    public partial class frmStuListMain : Form
    {
        /// <summary>
        /// List to be parallel to listbox of equipment containing id
        /// </summary>
        List<int> intStudentID = new List<int>();
        public frmStuListMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// loads student names from database
        /// </summary>
        private void loadStudents()
        {
            //clear listbox
            lstStudents.Items.Clear();
            //clear list
            intStudentID.Clear();
            //create instance of class
            clsData stuData = new clsData();
            stuData.SQL = "SELECT ID, LastName, GivenName FROM tblStuInfo ORDER BY LastName, GivenName";
            for (int i = 0; i < stuData.dt.Rows.Count; i++)
            {
                //add student name to listbox
                lstStudents.Items.Add(stuData.dt.Rows[i]["LastName"].ToString() + ", " + stuData.dt.Rows[i]["GivenName"].ToString());
                //add student id to list
                intStudentID.Add(int.Parse(stuData.dt.Rows[i]["ID"].ToString()));
            }
        }

        private void frmStuListMain_Load(object sender, EventArgs e)
        {
            loadStudents(); //load students to listbox on screen
        }
        /// <summary>
        /// brings up the selected student's record for viewing or editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //ensure something was selected
            if (lstStudents.SelectedIndex > -1)
            {
                //create instance of second form
                frmStuDetails StudentDetailForm = new frmStuDetails(intStudentID[lstStudents.SelectedIndex]);
                StudentDetailForm.ShowDialog(); //show equipment details
                //refresh listbox
                loadStudents();
            }
            else
            {
                MessageBox.Show("Select a student display record details.");
            }
        }
        /// <summary>
        /// brings up a blank details form to input a new student
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewStudent_Click(object sender, EventArgs e)
        {
            //create instance of second form
            frmStuDetails NewDetailForm = new frmStuDetails();
            NewDetailForm.ShowDialog(); //show form
            //refresh listbox
            loadStudents();
        }
        /// <summary>
        /// Prompts a message to confirm or abort record deletion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //verfy record was selected
            if (lstStudents.SelectedIndex == -1)
            {
                MessageBox.Show("Select a record to delete.");
            }
            else
            {
                //verify that user wants to delete record
                DialogResult res = MessageBox.Show(this, "Delete record: " + lstStudents.SelectedItem.ToString() + "?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    //delete record
                    //create instance of data class
                    clsData DeleteData = new clsData();
                    //delete record based on SQL
                    DeleteData.DeleteRecord("DELETE * FROM tblStuInfo WHERE ID = " + intStudentID[lstStudents.SelectedIndex]);
                    //refresh listbox
                    loadStudents();
                }
            }
        }
        /// <summary>
        /// Calls instance of Reminders form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCounsel_Click(object sender, EventArgs e)
        {
            //create instance of reminders form
            frmReminders NewRemindersForm = new frmReminders();
            NewRemindersForm.ShowDialog(); //show form
            //refresh listbox
            loadStudents();
        }
    }
}
