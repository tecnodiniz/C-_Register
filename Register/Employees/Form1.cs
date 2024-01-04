using Register.DataAccess;
using Register.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register.Employees
{
    public partial class Form1 : Form
    {
        private MyDbContext _context;
        private Main main1;
        public Form1(Main main1)
        {
            InitializeComponent();
            _context = new MyDbContext();
            this.main1 = main1;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            User current_user = _context.Users.Find(this.main1.user.Id);
            Employee employee = new Employee
            {
                Name = textBox1.Text,
                Description = richTextBox1.Text,
                User = current_user,
                Last_Altered = "Sem alterações"
             
             };
            
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();

                MessageBox.Show("Registro feito com sucesso!");
                this.Close();
                main1.LoadData();
                
            }
            catch (DbEntityValidationException ex)
            {
                // Handle exceptions related to entity validation
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Erro de validação: {validationError.ErrorMessage}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                _context.Dispose();
                _context = new MyDbContext();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro de conexão: {ex.Message}");
            }

        }
    }
}
