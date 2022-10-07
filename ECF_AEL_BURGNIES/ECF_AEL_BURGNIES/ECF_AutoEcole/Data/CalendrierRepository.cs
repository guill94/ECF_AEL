using ECF_AutoEcole.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ECF_AutoEcole.Data
{
    internal class CalendrierRepository
    {
        private SqlConnection activeCon;
        public CalendrierRepository()
        {
            this.DbConnecter();
        }

        private void DbConnecter()
        {
            DbConnexion con = new DbConnexion();
            this.activeCon = con.GetConnexion();
        }

        public List<Calendrier> CheckExists(string oneDate)
        {
            this.activeCon.Open();
            List<Calendrier> ListCalendrier = new List<Calendrier>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM CALENDRIER WHERE [date heure] = @date";

            SqlParameter date = sqlCommand.Parameters.Add("@date", System.Data.SqlDbType.Date);
            date.Value = oneDate;


            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Calendrier uneDate = new Calendrier
                    {
                        DateHeure = Convert.ToDateTime($"{reader[0]}"),

                    };
                    ListCalendrier.Add(uneDate);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListCalendrier;
        }


        /// <summary>
        /// Permet de faire une recherche sur les dates
        /// </summary>
        /// <param name="search">La chaîne entrée par l'utilisateur dans l'input de recherche</param>
        /// <returns>Les résultats correspondant à la chaîne entrée</returns>
        public List<Calendrier> FilterCalendrier(string search)
        {
            this.activeCon.Open();

            List<Calendrier> ListCalendrier = new List<Calendrier>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM CALENDRIER WHERE [date heure] BETWEEN @search_string1 AND @search_string2";

            SqlParameter SearchString1 = sqlCommand.Parameters.AddWithValue("@search_string1", System.Data.SqlDbType.DateTime);
            SearchString1.Value = Convert.ToDateTime(search);

            SqlParameter SearchString2 = sqlCommand.Parameters.AddWithValue("@search_string2", System.Data.SqlDbType.DateTime);
            SearchString2.Value = Convert.ToDateTime(search).AddMinutes(1439.00);


            try
            {
                SqlDataReader modeles = sqlCommand.ExecuteReader();

                while (modeles.Read())
                {
                    Calendrier uneDate = new Calendrier
                    {
                        DateHeure = Convert.ToDateTime($"{modeles[0]}"),

                    };
                    ListCalendrier.Add(uneDate);
                }
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListCalendrier;

        }

        /// <summary>
        /// Permet de récupérer toutes les entrées dans la table
        /// </summary>
        /// <returns>Liste de calendrier (Tous ceux présents dans la table)</returns>
        public List<Calendrier> GetAllCalendrier()
        {
            this.activeCon.Open();
            List<Calendrier> ListCalendrier = new List<Calendrier>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM CALENDRIER";

            try
            {
                SqlDataReader modeles = sqlCommand.ExecuteReader();

                while (modeles.Read())
                {
                    Calendrier uneDate = new Calendrier
                    {
                        DateHeure = Convert.ToDateTime($"{modeles[0]}"),

                    };
                    ListCalendrier.Add(uneDate);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            

            this.activeCon.Close();

            return ListCalendrier;
        }

        /// <summary>
        /// Création d'un nouveau calendrier
        /// </summary>
        /// <param name="cal">Objet de type calendrier</param>
        public void CreateCalendrier(Calendrier cal)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO CALENDRIER ([date heure]) VALUES (@date)";

            SqlParameter date = sqlCommand.Parameters.Add("@date", System.Data.SqlDbType.DateTime);
            date.Value = cal.DateHeure;

            try
            {
                int res = sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();
        }


        /// <summary>
        /// Suppression d'un calendrier
        /// </summary>
        /// <param name="cal">Objet de type calendrier</param>
        public void DeleteCalendrier(Calendrier cal)
        {
            this.activeCon.Open();

            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM LECON WHERE [date heure] = @date";
            SqlParameter date = sqlCommand.Parameters.Add("@date", System.Data.SqlDbType.DateTime);
            date.Value = cal.DateHeure;


            SqlCommand sqlCommand2 = this.activeCon.CreateCommand();
            sqlCommand2.CommandText = "DELETE FROM CALENDRIER WHERE [date heure] = @date";
            SqlParameter date2 = sqlCommand2.Parameters.Add("@date", System.Data.SqlDbType.DateTime);
            date2.Value = cal.DateHeure;

            try
            {
                int res = sqlCommand.ExecuteNonQuery();
                int res2 = sqlCommand2.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();
        }
    }
}
