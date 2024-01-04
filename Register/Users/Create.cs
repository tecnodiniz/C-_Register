using Register.DataAccess;
using Register.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register.Users
{
    public partial class Create : Form
    {
        private main main;
        public Create(main main)
        {
            this.main = main;
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(var _context = new MyDbContext()) 
            {
                User user = new User
                {
                    UserLogin = textBox1.Text,
                    UserPassword = textBox2.Text,
                    UserRole = (int)comboBox1.SelectedValue,
                    UserName = textBox3.Text
                };
                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    MessageBox.Show("Usuário criado");
                    this.Close();
                    main.loadUser();

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro de conexão: {ex.Message}");
                }
            }
        }

        private void Create_Load(object sender, EventArgs e)
        {
            
            comboBox1.DataSource = Enum.GetValues(typeof(role));
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
