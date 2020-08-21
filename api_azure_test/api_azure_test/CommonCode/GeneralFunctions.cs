using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_azure_test.Common
{
    public class GeneralFunctions
    {
        public static string GetConnectionString()
        {
            return @"Server=tcp:ephraim.database.windows.net,1433;Initial Catalog=ephraim-employee;Persist Security Info=False;User ID=ephraiminjamuri;Password=Sandalwood123$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Ephraim\Software\SnippetGenerator\SnippetGenerator\eDemoDatabase.mdf;Integrated Security=True";
        }
    }
}
