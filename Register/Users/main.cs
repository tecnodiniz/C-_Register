using Register.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register.Users
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        public void loadUser()
        {
            using (var _context = new MyDbContext())
            {
                dataGridView1.DataSource = _context.Users.ToList();
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["UserRole"].Visible = false;
                dataGridView1.Columns["role"].HeaderText= "Nível";
                dataGridView1.Columns["Employees"].Visible = false;
            }

        }
        private void main_Load(object sender, EventArgs e)
        {
           loadUser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Users.Create window = new Users.Create(this);
            window.Show();
        }
    }
}
