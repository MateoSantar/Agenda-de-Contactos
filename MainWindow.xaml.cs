using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
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

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            string jsonString = JsonConvert.SerializeObject(Contacts,Formatting.Indented);
            File.WriteAllText("contacts.json", jsonString);
            MessageBox.Show("Guardado!");
        }

        private void importBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Seleccione el archivo de contactos"

            };
            string filePath = string.Empty;

            // Mostrar el cuadro de diálogo y obtener la ruta del archivo seleccionado
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
            }
            else
            {
                Console.WriteLine("No file selected.");
            }

            try
            {
                string json = File.ReadAllText(filePath);
                ObservableCollection<Contact> contacts = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(json);
                contactList.ItemsSource = contacts;
                MessageBox.Show("Importado!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing file: {ex.Message}");
            }
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (searchTxT.Text != "" && contactList.Items.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                switch (searchComboBox.Text)
                {
                    case "Nombre":
                        foreach (Contact c in contactList.ItemsSource)
                        {
                            if (searchTxT.Text == c.FirstName)
                            {
                                sb.Append(c.ToString()+"\n");
                            }
                        }
                        break;
                    case "Apellido":
                        foreach (Contact c in contactList.ItemsSource)
                        {
                            if (searchTxT.Text == c.LastName)
                            {
                                sb.Append(c.ToString() + "\n");
                            }
                        }
                        break;
                    case "Teléfono":
                        foreach (Contact c in contactList.ItemsSource)
                        {
                            if (searchTxT.Text == c.PhoneNumber)
                            {
                                sb.Append(c.ToString() + "\n");
                            }
                        }
                        break;
                    default:
                        break;
                }
                MessageBox.Show(sb.ToString());
            }
            else
            {
                MessageBox.Show("No hay contactos");
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