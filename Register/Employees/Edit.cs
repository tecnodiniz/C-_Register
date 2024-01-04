using Register.DataAccess;
using Register.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register.Employees
{
    public partial class Edit : Form
    {

        private Employee employee;
        private Main main;
        public Edit(Employee employee, Main main)
        {
            InitializeComponent();
            this.employee = employee;
            txtName.Text = employee.Name;  
            richTextDesc.Text = employee.Description;
            this.main = main;
            
        }

        private void Edit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var _context = new MyDbContext())
            {
                User current_user = _context.Users.Find(this.main.user.Id);
                Employee employee = _context.Employees.Find(this.employee.Id);
                employee.Name = txtName.Text;
                employee.Description = richTextDesc.Text;
                employee.Last_Altered = current_user.UserName;

                try
                {
                    _context.SaveChanges();

                    MessageBox.Show("Registro Atualizado");


                    this.Close();
                    main.LoadData();

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
                    
                }catch (Exception ex)
                {
                    MessageBox.Show($"Erro de conexão: {ex.Message}");
                }



            }


        }
    }
}
