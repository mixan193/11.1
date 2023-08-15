using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _11._1
{
    [Serializable]
    public class Client
    {
        public int id;
        public string surname { get; set; }
        public string name { get; set; }
        public string patronimic { get; set; }
        public string phoneNumber { get; set; }
        public string seriesAndNumberOfThePassport { get; set; }
        public DateTime modificationTime;
        public string modificatedData;
        public string typeOfModification;
        public string whoChangeData;
        public Client(int id, string surname, string name, string patronimic, string phoneNumber, string seriesAndNumberOfThePassport, string whoChangeData)
        {
            this.id = id;
            this.surname = surname;
            this.name = name;
            this.patronimic = patronimic;
            this.phoneNumber = PhoneNumberUniformization(phoneNumber);
            this.seriesAndNumberOfThePassport = seriesAndNumberOfThePassport;
            this.whoChangeData = whoChangeData;
            modificationTime = DateTime.Now;
            modificatedData = "All data";
            typeOfModification = "Create";
        }

        public static string PhoneNumberUniformization(string phoneNumber)
        {
            return Regex.Replace(phoneNumber, @"[^\d]", "");
        }
    }
}
