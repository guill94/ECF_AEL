using ECF_AutoEcole.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ECF_AutoEcole.Data
{
    public class VehiculesRepository
    {
        private SqlConnection activeCon;
        public VehiculesRepository()
        {
            this.DbConnecter();
        }

        private void DbConnecter()
        {
            DbConnexion con = new DbConnexion();
            this.activeCon = con.GetConnexion();
        }

        public List<Vehicule> CheckExists(string immat)
        {

            this.activeCon.Open();
            List<Vehicule> ListVehicules = new List<Vehicule>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM VEHICULE WHERE [n°immatriculation] = @immat";

            SqlParameter Immat = sqlCommand.Parameters.AddWithValue("@immat", System.Data.SqlDbType.VarChar);
            Immat.Value = immat;


            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Vehicule unVehicule = new Vehicule
                    {
                        NImmatriculation = $"{reader[0]}",
                        ModeleVehicule = $"{reader[1]}",
                        Etat = Convert.ToBoolean($"{reader[2]}"),
                    };
                    ListVehicules.Add(unVehicule);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListVehicules;
        }

        /// <summary>
        /// Permet de faire une recherche sur le modèle et l'immatriculation du véhicule
        /// </summary>
        /// <param name="search">La chaîne entrée par l'utilisateur dans l'input de recherche</param>
        /// <returns>Les résultats correspondant à la chaîne entrée</returns>
        public List<Vehicule> FilterVehicule(string search)
        {
            this.activeCon.Open();

            List<Vehicule> ListVehicules = new List<Vehicule>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM VEHICULE WHERE [n°immatriculation] LIKE @search_string OR [modèle véhicule] LIKE @search_string";

            SqlParameter SearchString = sqlCommand.Parameters.AddWithValue("@search_string", System.Data.SqlDbType.VarChar);
            SearchString.Value = "%" + search + "%";

            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Vehicule unVehicule = new Vehicule
                    {
                        NImmatriculation = $"{reader[0]}",
                        ModeleVehicule = $"{reader[1]}",
                        Etat = Convert.ToBoolean($"{reader[2]}"),

                    };
                    ListVehicules.Add(unVehicule);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListVehicules;

        }

        public List<Vehicule> GetVehiculesByModele(string modele)
        {
            this.activeCon.Open();

            List<Vehicule> ListVehicules = new List<Vehicule>();


            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM VEHICULE WHERE VEHICULE.[modèle véhicule] = @mod AND [état] = 1";

            SqlParameter mod = sqlCommand.Parameters.AddWithValue("@mod", System.Data.SqlDbType.VarChar);
            mod.Value = modele;

            Vehicule unVehicule = new Vehicule();

            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();


                while (reader.Read())
                {
                    unVehicule.NImmatriculation = $"{reader[0]}";
                    unVehicule.ModeleVehicule = $"{reader[1]}";
                    unVehicule.Etat = Convert.ToBoolean($"{reader[2]}");

                    ListVehicules.Add(unVehicule);
                }
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListVehicules;
        }

        /// <summary>
        /// Permet de récupérer toutes les entrées dans la table
        /// </summary>
        /// <returns>Liste de véhicules (Tous ceux présents dans la table)</returns>
        public List<Vehicule> GetAllVehicules()
        {
            this.activeCon.Open();
            List<Vehicule> ListVehicules = new List<Vehicule>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM VEHICULE";

            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Vehicule unVehicule = new Vehicule
                    {
                        NImmatriculation = $"{reader[0]}",
                        ModeleVehicule = $"{reader[1]}",
                        Etat = Convert.ToBoolean($"{reader[2]}"),

                    };
                    ListVehicules.Add(unVehicule);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListVehicules;
        }

        /// <summary>
        /// Création d'un nouveau véhicule
        /// </summary>
        /// <param name="vehicule">Objet de type véhicule</param>
        public void CreateVehicule(Vehicule vehicule)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO VEHICULE ([n°immatriculation] ,[modèle véhicule], état) VALUES (@immat, @modele, @etat)";

            SqlParameter immat = sqlCommand.Parameters.Add("@immat", System.Data.SqlDbType.VarChar);
            SqlParameter modele = sqlCommand.Parameters.Add("@modele", System.Data.SqlDbType.VarChar);
            SqlParameter etat = sqlCommand.Parameters.Add("@etat", System.Data.SqlDbType.Bit);


            immat.Value = vehicule.NImmatriculation;
            modele.Value = vehicule.ModeleVehicule;
            etat.Value = vehicule.Etat;

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
        /// Mise à jour des données d'un véhicule
        /// </summary>
        /// <param name="vehicule">Objet de type véhicule</param>
        public void UpdateVehicule(Vehicule vehicule)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "UPDATE VEHICULE SET [n°immatriculation] = @immat, [modèle véhicule] = @modele, état = @etat WHERE [n°immatriculation] = @immat";

            SqlParameter immat = sqlCommand.Parameters.Add("@immat", System.Data.SqlDbType.VarChar);
            SqlParameter modele = sqlCommand.Parameters.Add("@modele", System.Data.SqlDbType.VarChar);
            SqlParameter etat = sqlCommand.Parameters.Add("@etat", System.Data.SqlDbType.Bit);


            immat.Value = vehicule.NImmatriculation;
            modele.Value = vehicule.ModeleVehicule;
            etat.Value = vehicule.Etat;

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
        /// Suppression d'un véhicule
        /// </summary>
        /// <param name="immat">L'Id du véhicule à supprimer (immatriculation)</param>
        public void DeleteVehicule(string immat)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM VEHICULE WHERE [n°immatriculation] = @immat";

            SqlParameter Immat = sqlCommand.Parameters.Add("@immat", System.Data.SqlDbType.VarChar);
            Immat.Value = immat;

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

    }
}
