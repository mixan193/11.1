using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._1
{
    internal interface IGetClient
    {
        string GetClientInString(Client client);
        void GetAllClients();
    }
}
