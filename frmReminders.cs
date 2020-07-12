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
    public partial class frmReminders : Form
    {
        List<int> intStudentID = new List<int>();
        public frmReminders()
        {
            InitializeComponent();
        }

        private void frmReminders_Load(object sender, EventArgs e)
        {
            loadReminderLists();
        }
        /// <summary>
        /// Call functions to fill list boxes
        /// </summary>
        private void loadReminderLists()
        {
            loadBirthdays();
            loadRenewals();
            loadInitials();
        }
        /// <summary>
        /// Load the Birthday List
        /// </summary>
        private void loadBirthdays()
        {
            {
                //clear listbox
                lstBirthDays.Items.Clear();
                //clear list
                intStudentID.Clear();
                //create instance of class
                clsData stuData = new clsData();
                stuData.SQL = "SELECT ID, LastName, GivenName, BirthDate FROM tblStuInfo ORDER BY LastName, GivenName";
                for (int i = 0; i < stuData.dt.Rows.Count; i++)
                {
                    //compare dates to test for birthday
                    if (DateTime.Parse(stuData.dt.Rows[i]["BirthDate"].ToString()).DayOfYear == DateTime.Now.DayOfYear)
                    {
                        //add student name to listbox
                        lstBirthDays.Items.Add(stuData.dt.Rows[i]["LastName"].ToString() + ", " + stuData.dt.Rows[i]["GivenName"].ToString());
                        //add student id to list
                        intStudentID.Add(int.Parse(stuData.dt.Rows[i]["ID"].ToString()));
                    }
                }                
            }
        }
        /// <summary>
        /// Load the Renewal Counseling List
        /// </summary>
        private void loadRenewals()
        {
            //clear listbox
            lstRenew.Items.Clear();
            //clear list
            intStudentID.Clear();
            //create instance of class
            clsData stuData = new clsData();
            stuData.SQL = "SELECT ID, LastName, GivenName, EndDate FROM tblStuInfo ORDER BY LastName, GivenName";
            for (int i = 0; i < stuData.dt.Rows.Count; i++)
            {
                //create variable to hold difference in dates in days
                double result = (DateTime.Parse(stuData.dt.Rows[i]["EndDate"].ToString()) - DateTime.Now).TotalDays;
                //compare dates to test for birthday
                if (result <= 90 && result > 0)
                {
                    //add student name to listbox
                    lstRenew.Items.Add(stuData.dt.Rows[i]["LastName"].ToString() + ", " + stuData.dt.Rows[i]["GivenName"].ToString() + "   " + DateTime.Parse(stuData.dt.Rows[i]["EndDate"].ToString()).ToShortDateString());
                    //add student id to list
                    intStudentID.Add(int.Parse(stuData.dt.Rows[i]["ID"].ToString()));
                }
            }
        }
        /// <summary>
        /// Load the Initial Counseling list
        /// </summary>
        private void loadInitials()
        {
            //clear listbox
            lstInitial.Items.Clear();
            //clear list
            intStudentID.Clear();
            //create instance of class
            clsData stuData = new clsData();
            stuData.SQL = "SELECT ID, LastName, GivenName, StartDate FROM tblStuInfo ORDER BY LastName, GivenName";
            for (int i = 0; i < stuData.dt.Rows.Count; i++)
            {
                //create variable to hold difference in dates in days
                double result = (DateTime.Now - DateTime.Parse(stuData.dt.Rows[i]["StartDate"].ToString())).TotalDays;
                //compare dates to test for birthday
                if (result <= 30 && result > 0)
                {
                    //add student name to listbox
                    lstInitial.Items.Add(stuData.dt.Rows[i]["LastName"].ToString() + ", " + stuData.dt.Rows[i]["GivenName"].ToString() + "   " + DateTime.Parse(stuData.dt.Rows[i]["StartDate"].ToString()).ToShortDateString());
                    //add student id to list
                    intStudentID.Add(int.Parse(stuData.dt.Rows[i]["ID"].ToString()));
                }
            }
        }
        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
