using Register.DataAccess;
using Register.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Xsl;
using System.Data.Entity;

namespace Register
{
    public partial class Main : Form
    {
        public User user = new User();
     
        public Main(User user)
        {
            this.user = user;
            
            InitializeComponent();
            label1.Text = $"Bem vindo {user.UserName}";
            label2.Text = DateTime.Now.ToString("HH:hh:ss");

            dataGridViewEmployeeConfigure();

        }
        private void dataGridViewEmployeeConfigure()
        {
            dataGridViewEmployee.CellContentClick += DataGridViewEmployee_CellContentClick;
            dataGridViewEmployee.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
   
        }

        public void LoadData()
        {
            try
            {
                using(var _context = new MyDbContext())
                {
                    var employeesWithDepartments = _context.Employees.Include(u => u.User).ToList();

                    dataGridViewEmployee.DataSource = null;
                    dataGridViewEmployee.Columns.Clear();
                    dataGridViewEmployee.Rows.Clear();

                  /*  DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("Nome", typeof(string));
                    dataTable.Columns.Add("Descrição", typeof(string));
                    dataTable.Columns.Add("Usuário que registrou", typeof(string));
                    dataTable.Columns.Add("Última alteração", typeof(string));
                   */
                

                    List<Employee> employees = _context.Employees.ToList();

                 
                    dataGridViewEmployee.DataSource = employees;
                    dataGridViewEmployee.Columns["Id"].Visible = false;
                    dataGridViewEmployee.Columns["UserId"].Visible = false;

                    dataGridViewEmployee.Columns["Last_Altered"].HeaderText = "Ultima alteração";
                    dataGridViewEmployee.Columns["Name"].HeaderText = "Nome";
                    dataGridViewEmployee.Columns["Description"].HeaderText = "Descrição";
                    dataGridViewEmployee.Columns["User"].HeaderText = "Registrado por";

                    // Formatação de Células
                    dataGridViewEmployee.CellFormatting += (sender, e) =>
                    {
                       
                        if (e.ColumnIndex == dataGridViewEmployee.Columns["User"].Index && e.RowIndex >= 0 && e.RowIndex < employees.Count)
                        {
                            Employee employee = employees[e.RowIndex];


                            if (employee.User is Register.Model.User) 
                            { 
                                e.Value = ((Register.Model.User)employee.User).UserName;
                            }
                        } 
                    };


                    DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
                    {
                        HeaderText = "Editar Item",
                        Text = "Editar",
                        Name = "btnEditar",
                        UseColumnTextForButtonValue = true
                    };

                    DataGridViewButtonColumn btnRemover = new DataGridViewButtonColumn
                    {
                        HeaderText = "Remover Item",
                        Text = "Remover",
                        Name = "btnRemover",
                        UseColumnTextForButtonValue = true
                    };
                    dataGridViewEmployee.Columns.Add(btnEditar);
                    dataGridViewEmployee.Columns.Add(btnRemover);


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro de conexão: {ex.Message}");
            }

        }

        private void DataGridViewEmployee_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DataGridViewEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewEmployee.Columns["btnEditar"].Index)
            {
                DataGridViewRow selectedRow = dataGridViewEmployee.Rows[e.RowIndex];

                Employee obj = (Employee)selectedRow.DataBoundItem as Employee;
             
               

                Employees.Edit window = new Employees.Edit(obj,this);
                window.Show();

            }
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewEmployee.Columns["btnRemover"].Index)
            {
                DataGridViewRow selectedRow = dataGridViewEmployee.Rows[e.RowIndex];

                Employee obj = (Employee)selectedRow.DataBoundItem;

                var result = MessageBox.Show("Tem certeza de que deseja remover o registro?",
                                                "Deletar Registro",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    using (var _context = new MyDbContext()) 
                    {
                        Employee employee = _context.Employees.Find(obj.Id);
                        if(employee != null)
                        {
                            try
                            {
                                _context.Employees.Remove(employee);
                                _context.SaveChanges();

                                MessageBox.Show("Item removido");
                                LoadData();

                            }catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                          

                        }
                       

                    }
                }

    
            }
        }

        private void atualizarHora(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            label2.Text = date.ToString("HH:mm:ss");
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login window = new Login();
            this.Close();
            window.Show();

        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Enabled = true;
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += atualizarHora;
            timer.Start();

            if (this.user.role != role.Administrator) btnUsers.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Employees.Form1 form = new Employees.Form1(this);
            
            form.Show();


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewEmployee_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (var _context = new MyDbContext()) 
            {
                List<Employee> employees = _context.Employees.Where(em => em.Name.Contains(searchBox.Text)).ToList();
                dataGridViewEmployee.DataSource = employees;
            }
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Users.main main = new Users.main();
            main.Show();
        }
    }
}
