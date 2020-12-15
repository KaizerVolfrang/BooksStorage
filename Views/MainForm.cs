using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BooksProject.Models;
using BooksProject.Providers;

namespace BooksProject.Views
{
    public partial class MainForm : Form
    {
        public User _user { get; set; }
        public StorageContext _context { get; set; }
        public MainForm(User user, StorageContext context)
        {
            InitializeComponent();
            _user = user;
            _context = context;
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            if (_user.Role == "User")
            {
                Hide();
                var view = new BooksForm(_context);
                view.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Cyberpunk");
            }
        }

        private void AdminButton_Click(object sender, EventArgs e)
        {
            if (_user.Role == "Admin")
            {
                Hide();
                var view = new BooksForm(_context);
                view.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Cyberpunk 2020");
            }
        }
    }
}
