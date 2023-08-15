using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _11._1
{
    internal class Manager : Consultant
    {
        public Manager(MainWindow mainWindow) : base(mainWindow)
        {
            Id = 2;
            Name = "Random name";
        }


        protected override void ChooseActionsWithClient(Client client)
        {
            Console.Clear();
            Console.WriteLine(GetClientInString(client));
            Console.WriteLine("Выберте действие:\r\n" +
                "1. Изменить данные;\r\n" +
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

        public override void ShowModifyDataForm()
        {
            mainWindow.MyStackPanel.Children.Clear();
            mainWindow.MyStackPanel.Children.Add(textBlocks[0]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[0]);
            mainWindow.SurnameTextBox.Text = (clientsListView.SelectedItem as Client).surname;

            mainWindow.MyStackPanel.Children.Add(textBlocks[1]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[1]);
            mainWindow.NameTextBox.Text = (clientsListView.SelectedItem as Client).name;

            mainWindow.MyStackPanel.Children.Add(textBlocks[2]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[2]);
            mainWindow.PatronimicTextBox.Text = (clientsListView.SelectedItem as Client).patronimic;

            mainWindow.MyStackPanel.Children.Add(textBlocks[3]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[3]);
            mainWindow.PhoneNumberTextBox.Text = (clientsListView.SelectedItem as Client).phoneNumber;

            mainWindow.MyStackPanel.Children.Add(textBlocks[4]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[4]);
            mainWindow.SeriesAndNumberOfThePassportTextBox.Text = (clientsListView.SelectedItem as Client).seriesAndNumberOfThePassport;

            mainWindow.MyStackPanel.Children.Add(saveChangesButton);
        }

        public override void ChangeClient(Client client)
        {
            client.surname = mainWindow.SurnameTextBox.Text;
            client.name = mainWindow.NameTextBox.Text;
            client.patronimic = mainWindow.PatronimicTextBox.Text;
            client.phoneNumber = Client.PhoneNumberUniformization(mainWindow.PhoneNumberTextBox.Text);
            client.seriesAndNumberOfThePassport = mainWindow.SeriesAndNumberOfThePassportTextBox.Text;
            client.whoChangeData = GetType().Name;
            client.modificationTime = DateTime.Now;
            client.modificatedData = "All data";
            client.typeOfModification = "modification";
            HideModifyDataForm();
        }
    }
}
