using DesktopContactApp.Classes;
using SQLite;
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
using System.Windows.Shapes;

namespace DesktopContactApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Save Contact
            Contact contact = new Contact()
            {
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                PhoneNumber = phoneNumberTextBox.Text
            };

            /*
            //Connecting to the database
            SQLiteConnection connection = new SQLiteConnection(databasePath);
            //Create the Table
            connection.CreateTable<Contact>();
            //insert the objects in the table
            connection.Insert(contact);

            connection.Close();
            */

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                //Create the Table
                connection.CreateTable<Contact>();
                //insert the objects in the table
                connection.Insert(contact);
            }

            this.Close();
        }
    }
}
