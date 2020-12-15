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

namespace BooksProject.Views
{
    public partial class BooksFormEditor : Form
    {
        private int _bookId = 0;
        public Book book 
        {
            get
            {
                return new Book
                {
                    Id = _bookId,
                    Name = nameTextBox.Text,
                    Author = authorTextBox.Text,
                    DateOfPublish = dateOfPublishPicker.Value
                };     
            }         
        }
        public BooksFormEditor(List<Book> books)
        {
            InitializeComponent();

        }

        public DialogResult ShowDialog(Book book)
        {
            if (book == null)
                return DialogResult.None;

            _bookId = book.Id;
            nameTextBox.Text = book.Name;
            authorTextBox.Text = book.Author;
            dateOfPublishPicker.Value = book.DateOfPublish;

            return ShowDialog();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("Error");
                return;
            }
            if (string.IsNullOrEmpty(authorTextBox.Text))
            {
                MessageBox.Show("Error");
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void rejectButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
