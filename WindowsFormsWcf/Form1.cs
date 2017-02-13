using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsWcf.ServiceReference1;

namespace WindowsFormsWcf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.Id = Convert.ToInt32(txtId.Text);
            u.Name = txtName.Text;
            u.Age = Convert.ToInt32(txtAge.Text);

            Service1Client service = new Service1Client();

            if (service.InsertUser(u) == 1)
            {
                MessageBox.Show("User inserted successfully");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            User u = new User()
            {
                Id = Convert.ToInt32(txtId.Text),
                Name = txtName.Text,
                Age = Convert.ToInt32(txtAge.Text)
            };

            Service1Client service = new Service1Client();

            if (service.UpdateUser(u) == 1)
            {
                MessageBox.Show("User updated successfully");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            User u = new User()
            {
                Id = Convert.ToInt32(txtId.Text)
            };

            Service1Client service = new Service1Client();

            if (service.UpdateUser(u) == 1)
            {
                MessageBox.Show("User is deleted successfully");
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            IList<User> userL = new List<User>();
            User u = new User()
            {
                Id = Convert.ToInt32(txtId.Text == "" ? "0" : txtId.Text)
            };

            Service1Client service = new Service1Client();
            userL.Add(service.GetUser(u));
            dgvUsers.DataSource = userL;
        }
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            List<User> userL = new List<User>();
            Service1Client service = new Service1Client();

            dgvUsers.DataSource = service.GetAllUsers();
        }
    }
}
