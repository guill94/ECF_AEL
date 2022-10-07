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
    public class MoniteursRepository
    {
        private SqlConnection activeCon;
        public MoniteursRepository()
        {
            this.DbConnecter();
        }

        private void DbConnecter()
        {
            DbConnexion con = new DbConnexion();
            this.activeCon = con.GetConnexion();
        }

        /// <summary>
        /// Permet de récupérer le dernier Id (max) dans la table 
        /// </summary>
        /// <returns>L'Id le plus grand existant dans la table</returns>
        public int GetLastId()
        {
            int lastId = 0;
            this.activeCon.Open();
            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT max([id moniteur]) FROM MONITEUR";

            string id = sqlCommand.ExecuteScalar().ToString();

            if (id == "")
            {
                lastId = 0;
            }
            else
            {
                lastId = Convert.ToInt32(id);
            }

            lastId++;

            return lastId;
        }

        /// <summary>
        /// Permet de faire une recherche sur le nom et prénom du moniteur
        /// </summary>
        /// <param name="search">La chaîne entrée par l'utilisateur dans l'input de recherche</param>
        /// <returns>Les résultats correspondant à la chaîne entrée</returns>
        public List<Moniteur> FilterMoniteur(string search)
        {
            this.activeCon.Open();

            List<Moniteur> ListMoniteurs = new List<Moniteur>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM Moniteur WHERE [nom moniteur] LIKE @search_string OR [prénom moniteur] LIKE @search_string";

            SqlParameter SearchString = sqlCommand.Parameters.AddWithValue("@search_string", System.Data.SqlDbType.VarChar);
            SearchString.Value = "%" + search + "%";

            try
            {
                SqlDataReader moniteurs = sqlCommand.ExecuteReader();

                while (moniteurs.Read())
                {
                    Moniteur moniteur = new Moniteur
                    {
                        IdMoniteur = Convert.ToInt32($"{moniteurs[0]}"),
                        NomMoniteur = $"{moniteurs[1]}",
                        PrenomMoniteur = $"{moniteurs[2]}",
                        DateNaissance = Convert.ToDateTime($"{moniteurs[3]}"),
                        DateEmbauche = Convert.ToDateTime($"{moniteurs[4]}"),
                        Activite = Convert.ToBoolean($"{moniteurs[5]}")
                    };
                    ListMoniteurs.Add(moniteur);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListMoniteurs;

        }

        /// <summary>
        /// Permet de récupérer toutes les entrées dans la table
        /// </summary>
        /// <returns>Liste de moniteurs (Tous ceux présents dans la table)</returns>
        public List<Moniteur> GetAllMoniteurs()
        {
            this.activeCon.Open();
            List<Moniteur> ListMoniteurs = new List<Moniteur>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM MONITEUR";

            try
            {
                SqlDataReader moniteurs = sqlCommand.ExecuteReader();

                while (moniteurs.Read())
                {
                    Moniteur moniteur = new Moniteur
                    {
                        IdMoniteur = Convert.ToInt32($"{moniteurs[0]}"),
                        NomMoniteur = $"{moniteurs[1]}",
                        PrenomMoniteur = $"{moniteurs[2]}",
                        DateNaissance = Convert.ToDateTime($"{moniteurs[3]}"),
                        DateEmbauche = Convert.ToDateTime($"{moniteurs[4]}"),
                        Activite = Convert.ToBoolean($"{moniteurs[5]}")
                    };
                    ListMoniteurs.Add(moniteur);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListMoniteurs;
        }

        public List<Moniteur> GetAllAvailableMoniteurs()
        {
            this.activeCon.Open();
            List<Moniteur> ListMoniteurs = new List<Moniteur>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM MONITEUR WHERE [activité] = 1";

            try
            {
                SqlDataReader moniteurs = sqlCommand.ExecuteReader();

                while (moniteurs.Read())
                {
                    Moniteur moniteur = new Moniteur
                    {
                        IdMoniteur = Convert.ToInt32($"{moniteurs[0]}"),
                        NomMoniteur = $"{moniteurs[1]}",
                        PrenomMoniteur = $"{moniteurs[2]}",
                        DateNaissance = Convert.ToDateTime($"{moniteurs[3]}"),
                        DateEmbauche = Convert.ToDateTime($"{moniteurs[4]}"),
                        Activite = Convert.ToBoolean($"{moniteurs[5]}")
                    };
                    ListMoniteurs.Add(moniteur);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListMoniteurs;
        }



        /// <summary>
        /// Création d'un nouveau moniteur
        /// </summary>
        /// <param name="mono">Objet de type moniteur</param>
        public void CreateMoniteur(Moniteur mono)
        {
            //this.activeCon.Open();
            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO MONITEUR ([id moniteur] ,[nom moniteur], [prénom moniteur], [date naissance], [date embauche], activité) VALUES (@id, @nom, @prenom, @dateNaissance, @dateEmbauche, @act) ";

            SqlParameter id = sqlCommand.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SqlParameter nom = sqlCommand.Parameters.Add("@nom", System.Data.SqlDbType.VarChar);
            SqlParameter prenom = sqlCommand.Parameters.Add("@prenom", System.Data.SqlDbType.VarChar);
            SqlParameter dateNaissance = sqlCommand.Parameters.Add("@dateNaissance", System.Data.SqlDbType.DateTime);
            SqlParameter dateEmbauche = sqlCommand.Parameters.Add("@dateEmbauche", System.Data.SqlDbType.DateTime);
            SqlParameter act = sqlCommand.Parameters.Add("@act", System.Data.SqlDbType.Bit);

            id.Value = mono.IdMoniteur;
            nom.Value = mono.NomMoniteur;
            prenom.Value = mono.PrenomMoniteur;
            dateNaissance.Value = mono.DateNaissance;
            dateEmbauche.Value = mono.DateEmbauche;
            act.Value = mono.Activite;

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
        /// Mise à jour des données d'un moniteur
        /// </summary>
        /// <param name="mono">Objet de type moniteur</param>
        public void UpdateMoniteur(Moniteur mono)
        {
            this.activeCon.Open();

            SqlCommand sqlCommand1 = this.activeCon.CreateCommand();

            if (mono.Activite == false)
            {
                sqlCommand1.CommandText = "DELETE FROM LECON WHERE [id moniteur] = @mono";

                SqlParameter idMoniteur = sqlCommand1.Parameters.Add("@mono", System.Data.SqlDbType.Int);
                idMoniteur.Value = mono.IdMoniteur;

            }



            SqlCommand sqlCommand2 = this.activeCon.CreateCommand();
            sqlCommand2.CommandText = "UPDATE MONITEUR SET [nom moniteur] = @nom, [prénom moniteur] = @prenom, [date naissance] = @dateNaissance, [date embauche] = @dateEmbauche, activité = @act WHERE [id moniteur] = @id";

            SqlParameter id = sqlCommand2.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SqlParameter nom = sqlCommand2.Parameters.Add("@nom", System.Data.SqlDbType.VarChar);
            SqlParameter prenom = sqlCommand2.Parameters.Add("@prenom", System.Data.SqlDbType.VarChar);
            SqlParameter dateNaissance = sqlCommand2.Parameters.Add("@dateNaissance", System.Data.SqlDbType.DateTime);
            SqlParameter dateEmbauche = sqlCommand2.Parameters.Add("@dateEmbauche", System.Data.SqlDbType.DateTime);
            SqlParameter act = sqlCommand2.Parameters.Add("@act", System.Data.SqlDbType.Bit);

            id.Value = mono.IdMoniteur;
            nom.Value = mono.NomMoniteur;
            prenom.Value = mono.PrenomMoniteur;
            dateNaissance.Value = mono.DateNaissance;
            dateEmbauche.Value = mono.DateEmbauche;
            act.Value = mono.Activite;


            try
            {
                if (mono.Activite == false)
                {
                    int res1 = sqlCommand1.ExecuteNonQuery();
                }

                int res2 = sqlCommand2.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();
        }

        /// <summary>
        /// Suppression d'un moniteur
        /// </summary>
        /// <param name="Id">L'Id du moniteur à supprimer</param>
        public void DeleteMoniteur(int Id)
        {
            this.activeCon.Open();

            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM LECON WHERE [id moniteur] = @id";
            SqlParameter id = sqlCommand.Parameters.Add("@id", System.Data.SqlDbType.Int);
            id.Value = Id;


            SqlCommand sqlCommand2 = this.activeCon.CreateCommand();
            sqlCommand2.CommandText = "DELETE FROM MONITEUR WHERE [id moniteur] = @id";
            SqlParameter id2 = sqlCommand2.Parameters.Add("@id", System.Data.SqlDbType.Int);
            id2.Value = Id;

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
