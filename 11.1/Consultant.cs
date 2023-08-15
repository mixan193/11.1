using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _11._1
{
    public class Consultant : IGetClient, IChangeClient
    {
        protected MainWindow mainWindow;
        protected ListView clientsListView;
        protected TextBlock[] textBlocks = new TextBlock[5];
        protected TextBox[] textBoxes = new TextBox[5];
        protected Button findButton;
        protected Button saveChangesButton;
        protected Button changeDataButton;
        protected Button deleteClientButton;
        protected TextBlock infoTextBlock;
        protected bool isChange;

        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public Consultant(MainWindow mainWindow)
        {
            Id = 1;
            Name = "Random name";
            this.mainWindow = mainWindow;
            clientsListView = mainWindow.ClientsListView;
            textBlocks[0] = mainWindow.SurnameTextBlock;
            textBlocks[1] = mainWindow.NameTextBlock;
            textBlocks[2] = mainWindow.PatronimicTextBlock;
            textBlocks[3] = mainWindow.PhoneNumberTextBlock;
            textBlocks[4] = mainWindow.SeriesAndNumberOfThePassportTextBlock;
            textBoxes[0] = mainWindow.SurnameTextBox;
            textBoxes[1] = mainWindow.NameTextBox;
            textBoxes[2] = mainWindow.PatronimicTextBox;
            textBoxes[3] = mainWindow.PhoneNumberTextBox;
            textBoxes[4] = mainWindow.SeriesAndNumberOfThePassportTextBox;
            findButton = mainWindow.FindButton;
            saveChangesButton = mainWindow.SaveChangesButton;
            changeDataButton = mainWindow.ChangeDataButton;
            deleteClientButton = mainWindow.DeleteClientButton;
            infoTextBlock = mainWindow.InfoTextBlock;
            mainWindow.MyStackPanel.Children.Clear();
            mainWindow.MyStackPanel.Visibility = System.Windows.Visibility.Visible;
        }
        public void ShowAddClientForm()
        {
            isChange = false;
            mainWindow.MyStackPanel.Children.Clear();

            mainWindow.MyStackPanel.Children.Add(textBlocks[0]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[0]);

            mainWindow.MyStackPanel.Children.Add(textBlocks[1]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[1]);

            mainWindow.MyStackPanel.Children.Add(textBlocks[2]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[2]);

            mainWindow.MyStackPanel.Children.Add(textBlocks[3]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[3]);

            mainWindow.MyStackPanel.Children.Add(textBlocks[4]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[4]);

            mainWindow.MyStackPanel.Children.Add(saveChangesButton);
        }
        public virtual void ShowModifyDataForm()
        {
            isChange = true;
            mainWindow.MyStackPanel.Children.Clear();
            mainWindow.MyStackPanel.Children.Add(textBlocks[0]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[0]);
            mainWindow.SurnameTextBox.Text = (clientsListView.SelectedItem as Client).surname;
            mainWindow.SurnameTextBox.IsEnabled = false;

            mainWindow.MyStackPanel.Children.Add(textBlocks[1]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[1]);
            mainWindow.NameTextBox.Text = (clientsListView.SelectedItem as Client).name;
            mainWindow.NameTextBox.IsEnabled = false;

            mainWindow.MyStackPanel.Children.Add(textBlocks[2]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[2]);
            mainWindow.PatronimicTextBox.Text = (clientsListView.SelectedItem as Client).patronimic;
            mainWindow.PatronimicTextBox.IsEnabled = false;

            mainWindow.MyStackPanel.Children.Add(textBlocks[3]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[3]);
            mainWindow.PhoneNumberTextBox.Text = (clientsListView.SelectedItem as Client).phoneNumber;

            mainWindow.MyStackPanel.Children.Add(textBlocks[4]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[4]);
            mainWindow.SeriesAndNumberOfThePassportTextBox.Text = (clientsListView.SelectedItem as Client).seriesAndNumberOfThePassport;
            mainWindow.SeriesAndNumberOfThePassportTextBox.IsEnabled = false;
            
            mainWindow.MyStackPanel.Children.Add(saveChangesButton);
        }
        protected virtual void HideModifyDataForm()
        {
            mainWindow.SurnameTextBox.IsEnabled = true;
            mainWindow.NameTextBox.IsEnabled = true;
            mainWindow.PatronimicTextBox.IsEnabled = true;
            mainWindow.SeriesAndNumberOfThePassportTextBox.IsEnabled = true;
            mainWindow.SurnameTextBox.Text = string.Empty;
            mainWindow.NameTextBox.Text = string.Empty;
            mainWindow.PatronimicTextBox.Text = string.Empty;
            mainWindow.PhoneNumberTextBox.Text= string.Empty;
            mainWindow.SeriesAndNumberOfThePassportTextBox.Text= string.Empty;
            mainWindow.MyStackPanel.Children.Clear();
        }

        public void ShowClientInfo()
        {
            HideModifyDataForm();
            mainWindow.MyStackPanel.Children.Clear();
            if(clientsListView.SelectedItem != null)
            {
                mainWindow.MyStackPanel.Children.Add(infoTextBlock);
                infoTextBlock.Text = GetClientInString(clientsListView.SelectedItem as Client);
                mainWindow.MyStackPanel.Children.Add(changeDataButton);
                mainWindow.MyStackPanel.Children.Add(deleteClientButton);
            }
        }
        public void SaveChanges()
        {
            if(isChange)
            {
                ChangeClient(clientsListView.SelectedItem as Client);
            }
            else
            {
                AddClient();
            }
            GetAllClients();
        }
        protected void ChooseAMethodToFind()
        {
            Client client = null;
            Console.Clear();
            Console.WriteLine("Выберте действие:\r\n" +
                "1. Поиск по ФИО;\r\n" +
                "2. Поиск по ИД\r\n" +
                "3. Поиск по номеру телефона\r\n");
            switch (Console.ReadKey(false).Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.WriteLine("Введите ФИО через пробел");
                    string[] strings = Console.ReadLine().Split(new char[] { ' ' });
                    client = ClientsDB.GetClient(strings[0], strings[1], strings[2]);
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.WriteLine("Введите ИД");
                    client = ClientsDB.GetClient(int.Parse(Console.ReadLine()));
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    Console.WriteLine("Введите номер телефона");
                    client = ClientsDB.GetClient(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Вы ничего не выбрали");
                    Console.ReadKey();
                    break;
            }
            if (client == null)
            {
                Console.WriteLine("Клиент не найден");
            }
            else
            {
                ChooseActionsWithClient(client);
            }
        }

        protected virtual void ChooseActionsWithClient(Client client)
        {
            Console.Clear();
            Console.WriteLine(GetClientInString(client));
            Console.WriteLine("Выберте действие:\r\n" +
                "1. Изменить номер;\r\n" +
                "2. Выход\r\n");
            switch (Console.ReadKey(false).Key)
            {
                case ConsoleKey.D1:
                    ChangeClient(client);
                    break;
                default:
                    break;
            }

        }

        public string GetClientInString(Client client)
        {
            if (client == null)
            {
                return "Такого клиента не существует";
            }
            else
            {
                string result = string.Empty;
                result += $"ИД: {client.id}\r\n";
                result += $"Фамилия: {client.surname}\r\n";
                result += $"Имя: {client.name}\r\n";
                result += $"Отчество: {client.patronimic}\r\n";
                result += $"Номер телефона: {client.phoneNumber}\r\n";
                if (client.seriesAndNumberOfThePassport != string.Empty)
                {
                    result += "Серия и номер паспорта: **********\r\n";
                }
                else
                {
                    result += "Серия и номер паспорта: отсутствует\r\n";
                }
                result += $"Дата и время изменения записи: {client.modificationTime}\r\n";
                result += $"Было изменено: {client.modificatedData}\r\n";
                result += $"Тип изменений: {client.typeOfModification}\r\n";
                result += $"Кто менял: {client.whoChangeData}\r\n";
                return result;
            }
        }
        public void AddClient()
        {
            int id;
            string surname;
            string name;
            string patronimic;
            string phoneNumber;
            string seriesAndNumberOfThePassport;
            if (ClientsDB.clients.Count > 0)
            {
                id = ClientsDB.clients.Last().id + 1;
            }
            else
            {
                id = 1;
            }
            surname = mainWindow.SurnameTextBox.Text;
            name = mainWindow.NameTextBox.Text;
            patronimic = mainWindow.PatronimicTextBox.Text;
            phoneNumber = mainWindow.PhoneNumberTextBox.Text;
            seriesAndNumberOfThePassport = mainWindow.SeriesAndNumberOfThePassportTextBox.Text;
            ClientsDB.AddClient(new Client(id, surname, name, patronimic, phoneNumber, seriesAndNumberOfThePassport, GetType().Name));
            HideModifyDataForm();
        }
        public virtual void ChangeClient(Client client)
        {
            client.phoneNumber = mainWindow.PhoneNumberTextBox.Text;
            client.whoChangeData = GetType().Name;
            client.modificationTime = DateTime.Now;
            client.modificatedData = "phone number";
            client.typeOfModification = "modification";
            HideModifyDataForm();
        }

        public void GetAllClients()
        {
            clientsListView.ItemsSource = new List<Client>();
            clientsListView.ItemsSource = ClientsDB.clients;
        }
    }
}
