using DesktopContactApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopContactApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;

        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            ReadDatabase();
        }

        private void ReadDatabase()
        {
            
            using (SQLite.SQLiteConnection readingConn = new SQLite.SQLiteConnection(App.databasePath))
            {
                readingConn.CreateTable<Contact>();
                contacts = (readingConn.Table<Contact>().ToList()).OrderBy(c => c.Name).ToList();
            }

            if(contacts != null && contacts.Count != 0)
            {
                //foreach (var contact in contacts)
                //{
                //    contactsListView.Items.Add(new ListViewItem()
                //    {
                //        Content = contact
                //    });
                //}

                contactsListView.ItemsSource = contacts;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();
            ReadDatabase();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //bring here the text inside the texBOx
            TextBox searchTextBox = sender as TextBox;

            //Filter Research: consider only the contacts that contain the text in textBox
            //Because filteredList is Inumerable I need to transform it with .ToList()
            //Version using LINQ
            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();

            //Version without LINQ.... it is explicit query, longer but can allow me more operation
            // i.e. filtere, re-order, re-filter etc....
            var filteredList2 = (from c2 in contacts
                                where c2.Name.ToLower().Contains(searchTextBox.Text.ToLower())
                                orderby c2.PhoneNumber
                                select c2).ToList();

            //change the displayed list
            contactsListView.ItemsSource = filteredList;
        }
    }
}
