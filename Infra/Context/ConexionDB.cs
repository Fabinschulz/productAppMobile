using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAppMAUI.Infra.Context
{
    internal class ConexionDB
    {
        public static string ReturnRoute(string nameDatabase)
        {
            string routeDatabase = string.Empty;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                routeDatabase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                routeDatabase = Path.Combine(routeDatabase, nameDatabase);
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                routeDatabase = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                routeDatabase = Path.Combine(routeDatabase, "..", "Library", nameDatabase);
            }

            return routeDatabase;
        }
    }
}
