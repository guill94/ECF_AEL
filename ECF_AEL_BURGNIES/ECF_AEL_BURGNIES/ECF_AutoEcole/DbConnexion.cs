using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECF_AutoEcole
{
    public class DbConnexion
    {
        public SqlConnection GetConnexion()
        {
            SqlConnection connexion = new SqlConnection(@"Data Source=PC_Guill\SQL2019;Initial Catalog=ECF_auto_ecole;Persist Security Info=True;User ID=sa;Password=");

            //connexion.Open();
            return connexion;
        }
    }
}
