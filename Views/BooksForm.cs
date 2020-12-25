using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BooksProject.Providers;
using BooksProject.Models;

namespace BooksProject.Views
{
    public partial class BooksForm : Form
    {
        public StorageContext _context { get; set; }
        public BooksForm(StorageContext context)
        {
            InitializeComponent();
            _context = context;
            if (_context.AutorisationService.Me.Role == "User")
            {
                addButton.Visible = false;
                deleteButton.Visible = false;
                editButton.Visible = false;
            }
        }

        public void RefreshData()
        {
            List<Book> books = _context.booksProvider.GetAll();
            booksGridView.DataSource = books;
        }

        private void BooksForm_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var books = _context.booksProvider.GetAll();
            var editor = new BooksFormEditor(books);
            var res = editor.ShowDialog();
            if (res != DialogResult.OK)
                return;

            _context.booksProvider.Add(editor.book);

            RefreshData();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (booksGridView.SelectedRows.Count == 0)
                return;
            var selectedBook = booksGridView.SelectedRows[0].DataBoundItem as Book;
            _context.booksProvider.Delete(selectedBook.Id);
            RefreshData();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (booksGridView.SelectedRows == null)
                return;
            var selectedBook = booksGridView.SelectedRows[0].DataBoundItem as Book;
            var books = _context.booksProvider.GetAll();
            var booksFormEditor = new BooksFormEditor(books);
            var res = booksFormEditor.ShowDialog(selectedBook);
            if (res != DialogResult.OK)           
                return;  
            _context.booksProvider.Update(booksFormEditor.book);
            RefreshData();
        }
    }
}
