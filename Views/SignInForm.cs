using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BooksProject.Providers;

namespace BooksProject.Views
{
    public partial class SignInForm : Form
    {
        public StorageContext _context { get; set; }
        public SignInForm(StorageContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void signInButton_Click(object sender, EventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordTextBox.Text;
            var result = _context.AutorisationService.SignIn(login, password);
            if (result == null)
            {
                MessageBox.Show("Введите правильный логин и пароль");
            }
            else
            {
                Hide();
                var view = new MainForm(result, _context);
                view.ShowDialog();
                Close();
            }
        }
    }
}
