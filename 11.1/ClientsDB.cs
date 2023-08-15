using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace _11._1
{
    public static class ClientsDB
    {
        public static List<Client> clients = new List<Client>();
        public static int GetCount { get { return clients.Count; } }
        static ClientsDB()
        {
            if (File.Exists("DB.dat"))
            {
                FileStream fs = new FileStream("DB.dat", FileMode.Open, FileAccess.Read);
                clients = (List<Client>)new BinaryFormatter().Deserialize(fs);
                fs.Close();
            }
        }
        public static int AddClient(Client client)
        {
            clients.Add(client);
            SaveDB();
            return clients.Count;
        }

        public static int RemoveClient(Client client)
        {
            clients.Remove(client);
            SaveDB();
            return clients.Count;
        }

        private static void SaveDB()
        {
            FileStream fs = new FileStream("DB.dat", FileMode.OpenOrCreate, FileAccess.Write);
            new BinaryFormatter().Serialize(fs, clients);
            fs.Close();
        }
        /// <summary>
        /// Возвращает экземпляр класса Client по имени, фамилии и отчеству
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        public static Client GetClient(string surname, string name, string patronimic)
        {
            foreach (Client client in clients)
            {
                if (client.surname == surname)
                {
                    if (client.name == name)
                    {
                        if (client.patronimic == patronimic)
                        {
                            return client;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Возвращает экземпляр класса Client по номеру телефона
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static Client GetClient(string phoneNumber)
        {
            foreach (Client client in clients)
            {
                if (client.phoneNumber == Client.PhoneNumberUniformization(phoneNumber))
                {
                    return client;
                }
            }
            return null;
        }
        /// <summary>
        /// Возвращает экземпляр класса Client по номеру ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Client GetClient(int id)
        {
            foreach (Client client in clients)
            {
                if (client.id == id)
                {
                    return client;
                }
            }
            return null;
        }

        public static void Sort(string sortBy)
        {
            switch (sortBy)
            {
                case "Фамилия":
                    clients = clients.OrderBy(x => x.surname).ToList();
                    break;
                case "Имя":
                    clients = clients.OrderBy(x => x.name).ToList();
                    break;
                case "Отчество":
                    clients = clients.OrderBy(x => x.patronimic).ToList();
                    break;
                case "Номер телефона":
                    clients = clients.OrderBy(x => x.phoneNumber).ToList();
                    break;
                case "Серия и номер паспорта":
                    clients = clients.OrderBy(x => x.seriesAndNumberOfThePassport).ToList();
                    break;
            }
        }
    }
}
