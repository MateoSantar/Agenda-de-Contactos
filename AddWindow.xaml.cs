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
    public partial class AddWindow : Window
    {
        public Contact NewContact { get; private set; }
        public AddWindow()
        {
            InitializeComponent();
        }
        private string nombre, apellido, numero, mail;
        private void readyBtn_Click(object sender, RoutedEventArgs e)
        {
            int flag = 1;
                if (firstNameTxT.Text == "Nombre" || firstNameTxT.Text == "")
                {
                    MessageBox.Show("Falta un nombre");
                    flag = 0;
                }
                if (lastNameTxT.Text == "Apellido" || lastNameTxT.Text == "")
                {
                    MessageBox.Show("Falta un Apellido");
                    flag = 0;
                }
                if (phoneNumberTxT.Text == "Número" || phoneNumberTxT.Text == "")
                {
                    MessageBox.Show("Falta un número");
                    flag = 0;
                }
                if (emailTxT.Text == "Mail" || emailTxT.Text == "")
                {
                    MessageBox.Show("Falta un Mail");
                    flag = 0;
                }
                if (flag == 1)
                {
                    NewContact = new Contact(firstNameTxT.Text, lastNameTxT.Text, phoneNumberTxT.Text, emailTxT.Text);
                    MessageBox.Show("Guardado");
                    this.DialogResult = true;
                    this.Close();
                }
            } 
        }
    }
