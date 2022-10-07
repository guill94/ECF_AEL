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
    public class ModelesRepository
    {
        private SqlConnection activeCon;
        public ModelesRepository()
        {
            this.DbConnecter();
        }

        private void DbConnecter()
        {
            DbConnexion con = new DbConnexion();
            this.activeCon = con.GetConnexion();
        }

        /// <summary>
        /// Recupère les modèles par nom de modèle
        /// </summary>
        /// <param name="mod">modele</param>
        /// <returns>liste de modèles</returns>
        public List<Modele> CheckExists(string mod)
        {
            this.activeCon.Open();
            List<Modele> ListModeles = new List<Modele>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM MODELE WHERE [modèle véhicule] = @mod";

            SqlParameter modele = sqlCommand.Parameters.AddWithValue("@mod", System.Data.SqlDbType.VarChar);
            modele.Value = mod;


            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Modele unmodele = new Modele
                    {
                        ModeleVehicule = $"{reader[0]}",
                        Marque = $"{reader[1]}",
                        Annee = $"{reader[2]}",
                        DateAchat = Convert.ToDateTime($"{reader[3]}"),
                    };
                    ListModeles.Add(unmodele);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListModeles;
        }

        /// <summary>
        /// Permet de faire une recherche sur le modèle et la marque du véhicule
        /// </summary>
        /// <param name="search">La chaîne entrée par l'utilisateur dans l'input de recherche</param>
        /// <returns>Les résultats correspondant à la chaîne entrée</returns>
        public List<Modele> FilterModele(string search)
        {
            this.activeCon.Open();

            List<Modele> ListModeles = new List<Modele>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM MODELE WHERE [modèle véhicule] LIKE @search_string OR [marque] LIKE @search_string";

            SqlParameter SearchString = sqlCommand.Parameters.AddWithValue("@search_string", System.Data.SqlDbType.VarChar);
            SearchString.Value = "%"+search+"%";

            try
            {
                SqlDataReader modeles = sqlCommand.ExecuteReader();

                while (modeles.Read())
                {
                    Modele unModele = new Modele
                    {
                        ModeleVehicule = $"{modeles[0]}",
                        Marque = $"{modeles[1]}",
                        Annee = $"{modeles[2]}",
                        DateAchat = Convert.ToDateTime($"{modeles[3]}"),

                    };
                    ListModeles.Add(unModele);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListModeles;

        }

       
        public Modele GetModeleByModeleName(string id)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM MODELE WHERE [modèle véhicule] = @id";

            SqlParameter Id = sqlCommand.Parameters.AddWithValue("@id", System.Data.SqlDbType.VarChar);
            Id.Value = id;

            Modele unModele = new Modele();

            try
            {
                SqlDataReader modele = sqlCommand.ExecuteReader();

                while (modele.Read())
                {
                    unModele.ModeleVehicule = $"{modele[0]}";
                    unModele.Marque = $"{modele[1]}";
                    unModele.Annee = $"{modele[2]}";
                    unModele.DateAchat = Convert.ToDateTime($"{modele[3]}");

                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return unModele;
        }

        /// <summary>
        /// Permet de récupérer toutes les entrées dans la table
        /// </summary>
        /// <returns>Liste de modèles (Tous ceux présents dans la table)</returns>
        public List<Modele> GetAllModeles()
        {
            this.activeCon.Open();
            List<Modele> ListModeles = new List<Modele>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM MODELE";

            try
            {
                SqlDataReader modele = sqlCommand.ExecuteReader();

                while (modele.Read())
                {
                    Modele unModele = new Modele
                    {
                        ModeleVehicule = $"{modele[0]}",
                        Marque = $"{modele[1]}",
                        Annee = $"{modele[2]}",
                        DateAchat = Convert.ToDateTime($"{modele[3]}"),

                    };
                    ListModeles.Add(unModele);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListModeles;
        }

        /// <summary>
        /// Création d'un nouveau modèle
        /// </summary>
        /// <param name="modele">Objet de type modèle</param>
        public void CreateModele(Modele modele)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO MODELE ([modèle véhicule] ,marque, annee, [date achat]) VALUES (@id, @marque, @annee, @date)";

            SqlParameter id = sqlCommand.Parameters.Add("@id", System.Data.SqlDbType.VarChar);
            SqlParameter marque = sqlCommand.Parameters.Add("@marque", System.Data.SqlDbType.VarChar);
            SqlParameter annee = sqlCommand.Parameters.Add("@annee", System.Data.SqlDbType.NChar);
            SqlParameter date = sqlCommand.Parameters.Add("@date", System.Data.SqlDbType.Date);


            id.Value = modele.ModeleVehicule;
            marque.Value = modele.Marque;
            annee.Value = modele.Annee;
            date.Value = modele.DateAchat;


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
        /// Mise à jour des données d'un modèle
        /// </summary>
        /// <param name="modele">Objet de type modèle</param>
        public void UpdateModele(Modele modele)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "UPDATE MODELE SET marque = @marque, année = @annee, [date achat] = @date WHERE [modèle véhicule] = @iddd";

            SqlParameter id = sqlCommand.Parameters.Add("@iddd", System.Data.SqlDbType.VarChar);
            SqlParameter marque = sqlCommand.Parameters.Add("@marque", System.Data.SqlDbType.VarChar);
            SqlParameter annee = sqlCommand.Parameters.Add("@annee", System.Data.SqlDbType.NChar);
            SqlParameter date = sqlCommand.Parameters.Add("@date", System.Data.SqlDbType.Date);


            id.Value = modele.ModeleVehicule;
            marque.Value = modele.Marque;
            annee.Value = modele.Annee;
            date.Value = modele.DateAchat;

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
        /// Suppression d'un modèle
        /// </summary>
        /// <param name="mod">L'Id du modèle à supprimer (modèle véhicule)</param>
        public void DeleteModele(string mod)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand1 = this.activeCon.CreateCommand();
            sqlCommand1.CommandText = "DELETE FROM VEHICULE WHERE [modèle véhicule] = @id";
            SqlParameter id1 = sqlCommand1.Parameters.Add("@id", System.Data.SqlDbType.VarChar);
            id1.Value = mod;

            SqlCommand sqlCommand2 = this.activeCon.CreateCommand();
            sqlCommand2.CommandText = "DELETE FROM LECON WHERE [modèle véhicule] = @id";
            SqlParameter id2 = sqlCommand2.Parameters.Add("@id", System.Data.SqlDbType.VarChar);
            id2.Value = mod;

            SqlCommand sqlCommand3 = this.activeCon.CreateCommand();
            sqlCommand3.CommandText = "DELETE FROM MODELE WHERE [modèle véhicule] = @id";
            SqlParameter id3 = sqlCommand3.Parameters.Add("@id", System.Data.SqlDbType.VarChar);
            id3.Value = mod;

            try
            {
                int res = sqlCommand1.ExecuteNonQuery();
                int res2 = sqlCommand2.ExecuteNonQuery();
                int res3 = sqlCommand3.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();
        }
    }
}
