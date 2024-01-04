using Register.DataAccess;
using Register.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.FormClosing += MainForm_FormClosing;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    context.Database.Connection.Open();
                    MessageBox.Show("Conexão bem-sucedida!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro de conexão: {ex.Message}");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var _context = new MyDbContext())
            {
                var user = _context.Users.Where(obj => obj.UserLogin == textBox1.Text && obj.UserPassword == textBox2.Text).FirstOrDefault();
                if (user != null)
                {
                    Main main = new Main(user);
                    main.Show();
                    this.Hide();

                }
                else
                    MessageBox.Show("Usuário ou senha inválidos");
            }
           
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Deseja realmente sair?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Cancela o fechamento da janela
                }
                else
                {
                    Application.Exit(); // Encerra completamente a aplicação
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
