using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AgendaDeContactos
{
    public partial class EditWindow : Window
    {
        public Contact EditContact {  get; set; }
        private ObservableCollection<Contact> Contacts;
        public EditWindow(Contact editContact, ObservableCollection<Contact> contacts)
        {
            InitializeComponent();
            EditContact = editContact;
            firstNameTxT.Text = EditContact.FirstName;
            lastNameTxT.Text=EditContact.LastName;
            phoneNumberTxT.Text =EditContact.PhoneNumber;
            emailTxT.Text =EditContact.Email;
            Contacts = contacts;
        }

        private void saveEditBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = Contacts.IndexOf(EditContact);
            string firstName, lastName, phoneNumber, email;
            firstName = firstNameTxT.Text;
            lastName = lastNameTxT.Text;
            phoneNumber = phoneNumberTxT.Text;
            email = emailTxT.Text;
            Contact c = new Contact(firstName, lastName, phoneNumber,email);
            Contacts[index] = c;
            this.DialogResult = true;
        }
    }
}
