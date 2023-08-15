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

namespace _11._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Consultant employee;
        public MainWindow()
        {
            InitializeComponent();
            ClientsListView.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ListView_OnColumnClick));

        }

        private void ConsultantRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            employee = new Consultant(this);
            EnableUI();
        }

        private void ManagerRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            employee = new Manager(this);
            EnableUI();
        }

        private void EnableUI()
        {
            ConsultantRadioButton.IsEnabled = false;
            ManagerRadioButton.IsEnabled = false;
            ClientsListView.IsEnabled = true;
            AddClientButton.IsEnabled = true;
            employee.GetAllClients();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            employee.SaveChanges();
        }

        private void ChangeData_Click(object sender, RoutedEventArgs e)
        {
            employee.ShowModifyDataForm();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            ClientsDB.RemoveClient(ClientsListView.SelectedItem as Client);
            employee.GetAllClients();
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            employee.ShowAddClientForm();
        }

        private void ClientsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employee.ShowClientInfo();
        }

        private void ListView_OnColumnClick(object sender, RoutedEventArgs e)
        {
            ClientsDB.Sort((e.OriginalSource as GridViewColumnHeader).Content.ToString());
            employee.GetAllClients();
        }
    }
}
