using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace AgendaDeContactos
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Contact> Contacts {  get; set; } = new ObservableCollection<Contact>();
        private Contact SelectedContact { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWin = new AddWindow();
            if (addWin.ShowDialog() == true)
            {
                Contact newContact = addWin.NewContact;
                Contacts.Add(newContact);
                contactList.ItemsSource = Contacts;
            }
        }


        private void contactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contactList.SelectedItem !=null)
            {
                SelectedContact = (Contact)contactList.SelectedItem;
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedContact != null)
            {
                EditWindow editWin = new EditWindow(SelectedContact,Contacts);
                if (editWin.ShowDialog() == true)
                {
                                                            
                    editWin.Close();
                }
            }
        }
    }
    public class Contact(string firstName, string lastName, string phoneNumber, string email)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string PhoneNumber { get; set; } = phoneNumber;
        public string Email { get; set; } = email;
        public override string ToString()
        {
            return $"{FirstName} {LastName} | {PhoneNumber} | {Email}";
        }
    }
}