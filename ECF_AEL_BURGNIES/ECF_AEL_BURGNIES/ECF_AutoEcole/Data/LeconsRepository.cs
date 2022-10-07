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
    public class LeconsRepository
    {
        private SqlConnection activeCon;
        public LeconsRepository()
        {
            this.DbConnecter();
        }

        private void DbConnecter()
        {
            DbConnexion con = new DbConnexion();
            this.activeCon = con.GetConnexion();
        }


        /// <summary>
        /// Vérifie si la leçon existe déjà
        /// </summary>
        /// <param name="lecon">objet leçon</param>
        /// <returns>Liste leçons</returns>
        public List<Lecon> CheckExists(Lecon lecon)
        {

            this.activeCon.Open();
            List<Lecon> ListLecons = new List<Lecon>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM LECON WHERE [modèle véhicule] = @mod AND [date heure] = @date AND [id élève] = @eleve AND [id moniteur] = @mono";

            SqlParameter mod = sqlCommand.Parameters.Add("@mod", System.Data.SqlDbType.VarChar);
            SqlParameter date = sqlCommand.Parameters.Add("@date", System.Data.SqlDbType.DateTime);
            SqlParameter eleve = sqlCommand.Parameters.Add("@eleve", System.Data.SqlDbType.Int);
            SqlParameter mono = sqlCommand.Parameters.Add("@mono", System.Data.SqlDbType.Int);


            mod.Value = lecon.ModeleVehicule;
            date.Value = lecon.DateHeure;
            eleve.Value = lecon.IdEleve;
            mono.Value = lecon.IdMoniteur;


            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Lecon uneLecon = new Lecon
                {
                    ModeleVehicule = $"{reader[0]}",
                    DateHeure = Convert.ToDateTime($"{reader[1]}"),
                    IdEleve = Convert.ToInt32($"{reader[2]}"),
                    IdMoniteur = Convert.ToInt32($"{reader[3]}"),
                };
                ListLecons.Add(uneLecon);
            }

            this.activeCon.Close();

            return ListLecons;
        }



        /// <summary>
        /// Permet de faire une recherche sur le nom du moniteur et le nom de l'élève
        /// </summary>
        /// <param name="search">La chaîne entrée par l'utilisateur dans l'input de recherche</param>
        /// <returns>Les résultats correspondant à la chaîne entrée</returns>
        public List<Lecon> FilterLecon(string search)
        {
            this.activeCon.Open();

            List<Lecon> ListLecons = new List<Lecon>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM LECON INNER JOIN MODELE ON LECON.[modèle véhicule] = MODELE.[modèle véhicule] INNER JOIN MONITEUR ON LECON.[id moniteur] = MONITEUR.[id moniteur] INNER JOIN ELEVE ON LECON.[id élève] = ELEVE.[id élève] INNER JOIN CALENDRIER ON LECON.[date heure] = CALENDRIER.[date heure] WHERE ELEVE.[nom élève] LIKE @search OR MONITEUR.[nom moniteur] LIKE @search OR LECON.[modèle véhicule] LIKE @search";

            SqlParameter searchString = sqlCommand.Parameters.Add("@search", System.Data.SqlDbType.VarChar);
            searchString.Value = "%"+search+"%";

            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Lecon uneLecon = new Lecon();

                    uneLecon.ModeleVehicule = $"{reader[0]}";
                    uneLecon.DateHeure = Convert.ToDateTime($"{reader[1]}");
                    uneLecon.IdEleve = Convert.ToInt32($"{reader[2]}");
                    uneLecon.IdMoniteur = Convert.ToInt32($"{reader[3]}");
                    uneLecon.Duree = Convert.ToInt32($"{reader[4]}");

                    uneLecon.ModeleVehiculeNavigation.ModeleVehicule = $"{reader[5]}";
                    uneLecon.ModeleVehiculeNavigation.Marque = $"{reader[6]}";
                    uneLecon.ModeleVehiculeNavigation.Annee = $"{reader[7]}";
                    uneLecon.ModeleVehiculeNavigation.DateAchat = Convert.ToDateTime($"{reader[8]}");

                    uneLecon.IdMoniteurNavigation.IdMoniteur = Convert.ToInt32($"{reader[9]}");
                    uneLecon.IdMoniteurNavigation.NomMoniteur = $"{reader[10]}";
                    uneLecon.IdMoniteurNavigation.PrenomMoniteur = $"{reader[11]}";
                    uneLecon.IdMoniteurNavigation.DateNaissance = Convert.ToDateTime($"{reader[12]}");
                    uneLecon.IdMoniteurNavigation.DateEmbauche = Convert.ToDateTime($"{reader[13]}");
                    uneLecon.IdMoniteurNavigation.Activite = Convert.ToBoolean($"{reader[14]}");

                    uneLecon.IdEleveNavigation.IdEleve = Convert.ToInt32($"{reader[15]}");
                    uneLecon.IdEleveNavigation.NomEleve = $"{reader[16]}";
                    uneLecon.IdEleveNavigation.PrenomEleve = $"{reader[17]}";
                    uneLecon.IdEleveNavigation.Code = Convert.ToBoolean($"{reader[18]}");
                    uneLecon.IdEleveNavigation.Conduite = Convert.ToBoolean($"{reader[19]}");
                    uneLecon.IdEleveNavigation.DateNaissance = Convert.ToDateTime($"{reader[20]}");

                    uneLecon.DateHeureNavigation.DateHeure = Convert.ToDateTime($"{reader[21]}");



                    ListLecons.Add(uneLecon);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListLecons;

        }


        /// <summary>
        /// Permet de récupérer toutes les entrées dans la table
        /// </summary>
        /// <returns>Liste de leçons (Toutes celles présentes dans la table)</returns>
        public List<Lecon> GetAllLecons()
        {
            this.activeCon.Open();
            List<Lecon> ListLecons = new List<Lecon>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM LECON INNER JOIN MODELE ON LECON.[modèle véhicule] = MODELE.[modèle véhicule] INNER JOIN MONITEUR ON LECON.[id moniteur] = MONITEUR.[id moniteur] INNER JOIN ELEVE ON LECON.[id élève] = ELEVE.[id élève] INNER JOIN CALENDRIER ON LECON.[date heure] = CALENDRIER.[date heure]";

            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Lecon uneLecon = new Lecon();

                    uneLecon.ModeleVehicule = $"{reader[0]}";
                    uneLecon.DateHeure = Convert.ToDateTime($"{reader[1]}");
                    uneLecon.IdEleve = Convert.ToInt32($"{reader[2]}");
                    uneLecon.IdMoniteur = Convert.ToInt32($"{reader[3]}");
                    uneLecon.Duree = Convert.ToInt32($"{reader[4]}");

                    uneLecon.ModeleVehiculeNavigation.ModeleVehicule = $"{reader[5]}";
                    uneLecon.ModeleVehiculeNavigation.Marque = $"{reader[6]}";
                    uneLecon.ModeleVehiculeNavigation.Annee = $"{reader[7]}";
                    uneLecon.ModeleVehiculeNavigation.DateAchat = Convert.ToDateTime($"{reader[8]}");

                    uneLecon.IdMoniteurNavigation.IdMoniteur = Convert.ToInt32($"{reader[9]}");
                    uneLecon.IdMoniteurNavigation.NomMoniteur = $"{reader[10]}";
                    uneLecon.IdMoniteurNavigation.PrenomMoniteur = $"{reader[11]}";
                    uneLecon.IdMoniteurNavigation.DateNaissance = Convert.ToDateTime($"{reader[12]}");
                    uneLecon.IdMoniteurNavigation.DateEmbauche = Convert.ToDateTime($"{reader[13]}");
                    uneLecon.IdMoniteurNavigation.Activite = Convert.ToBoolean($"{reader[14]}");

                    uneLecon.IdEleveNavigation.IdEleve = Convert.ToInt32($"{reader[15]}");
                    uneLecon.IdEleveNavigation.NomEleve = $"{reader[16]}";
                    uneLecon.IdEleveNavigation.PrenomEleve = $"{reader[17]}";
                    uneLecon.IdEleveNavigation.Code = Convert.ToBoolean($"{reader[18]}");
                    uneLecon.IdEleveNavigation.Conduite = Convert.ToBoolean($"{reader[19]}");
                    uneLecon.IdEleveNavigation.DateNaissance = Convert.ToDateTime($"{reader[20]}");

                    uneLecon.DateHeureNavigation.DateHeure = Convert.ToDateTime($"{reader[21]}");



                    ListLecons.Add(uneLecon);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListLecons;
        }

        /// <summary>
        /// Création d'une nouvelle leçon
        /// </summary>
        /// <param name="lecon">Objet de type leçon</param>
        public void CreateLecon(Lecon lecon)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO LECON ([modèle véhicule] ,[date heure], [id élève], [id moniteur], durée) VALUES (@mod, @date, @eleve, @mono, @duree)";

            SqlParameter mod = sqlCommand.Parameters.Add("@mod", System.Data.SqlDbType.VarChar);
            SqlParameter date = sqlCommand.Parameters.Add("@date", System.Data.SqlDbType.DateTime);
            SqlParameter eleve = sqlCommand.Parameters.Add("@eleve", System.Data.SqlDbType.Int);
            SqlParameter mono = sqlCommand.Parameters.Add("@mono", System.Data.SqlDbType.Int);
            SqlParameter duree = sqlCommand.Parameters.Add("@duree", System.Data.SqlDbType.Int);


            mod.Value = lecon.ModeleVehicule;
            date.Value = lecon.DateHeure;
            eleve.Value = lecon.IdEleve;
            mono.Value = lecon.IdMoniteur;
            duree.Value = lecon.Duree;


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
        /// Mise à jour des données d'une leçon
        /// </summary>
        /// <param name="lecon">Objet de type leçon</param>
        public void UpdateLecon(Lecon lecon)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "UPDATE LECON SET [durée] = @duree WHERE [modèle véhicule] = @mod AND [date heure] = @date AND [id élève] = @eleve AND [id moniteur] = @mono";

            SqlParameter mod = sqlCommand.Parameters.Add("@mod", System.Data.SqlDbType.VarChar);
            SqlParameter date = sqlCommand.Parameters.Add("@date", System.Data.SqlDbType.DateTime);
            SqlParameter eleve = sqlCommand.Parameters.Add("@eleve", System.Data.SqlDbType.Int);
            SqlParameter mono = sqlCommand.Parameters.Add("@mono", System.Data.SqlDbType.Int);
            SqlParameter duree = sqlCommand.Parameters.Add("@duree", System.Data.SqlDbType.Int);


            mod.Value = lecon.ModeleVehicule;
            date.Value = lecon.DateHeure;
            eleve.Value = lecon.IdEleve;
            mono.Value = lecon.IdMoniteur;
            duree.Value = lecon.Duree;


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
        /// Suppression d'une leçon
        /// </summary>
        /// <param name="lecon">Objet leçon à supprimer</param>
        public void DeleteLecon(Lecon lecon)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM LECON WHERE [modèle véhicule] = @mod AND [date heure] = @date AND [id élève] = @eleve AND [id moniteur] = @mono";

            SqlParameter mod = sqlCommand.Parameters.Add("@mod", System.Data.SqlDbType.VarChar);
            SqlParameter date = sqlCommand.Parameters.Add("@date", System.Data.SqlDbType.DateTime);
            SqlParameter eleve = sqlCommand.Parameters.Add("@eleve", System.Data.SqlDbType.Int);
            SqlParameter mono = sqlCommand.Parameters.Add("@mono", System.Data.SqlDbType.Int);


            mod.Value = lecon.ModeleVehicule;
            date.Value = lecon.DateHeure;
            eleve.Value = lecon.IdEleve;
            mono.Value = lecon.IdMoniteur;

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


        //REQUETES POUR VERIFICATIONS

        /// <summary>
        /// Récupérer les leçon pour un moniteur donné et date donnée
        /// </summary>
        /// <param name="mono">l'id du moniteur</param>
        /// <param name="date">la date</param>
        /// <returns>Liste de leçons</returns>
        public List<Lecon> GetLeconsByMoniteurAndDate(int mono, string date)
        {

            this.activeCon.Open();
            List<Lecon> ListLecons = new List<Lecon>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM LECON WHERE [id moniteur] = @mono AND [date heure] BETWEEN @date1 AND @date2";

            SqlParameter moniteur = sqlCommand.Parameters.Add("@mono", System.Data.SqlDbType.Int);
            SqlParameter date1 = sqlCommand.Parameters.Add("@date1", System.Data.SqlDbType.DateTime);
            SqlParameter date2 = sqlCommand.Parameters.Add("@date2", System.Data.SqlDbType.DateTime);


            moniteur.Value = mono;
            date1.Value = date+" 00:00:00";
            date2.Value = date+" 23:59:59";


            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Lecon uneLecon = new Lecon
                    {
                        ModeleVehicule = $"{reader[0]}",
                        DateHeure = Convert.ToDateTime($"{reader[1]}"),
                        IdEleve = Convert.ToInt32($"{reader[2]}"),
                        IdMoniteur = Convert.ToInt32($"{reader[3]}"),
                        Duree = Convert.ToInt32($"{reader[4]}"),
                    };
                    ListLecons.Add(uneLecon);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListLecons;
        }

        /// <summary>
        /// Récupérer des leçons pour un élève donné et une date donnée
        /// </summary>
        /// <param name="idEleve">is de l'élève</param>
        /// <param name="date">date</param>
        /// <returns>Liste de leçons</returns>
        public List<Lecon> GetLeconsByEleveAndDate(int idEleve, string date)
        {

            this.activeCon.Open();
            List<Lecon> ListLecons = new List<Lecon>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM LECON WHERE [id élève] = @eleve AND [date heure] BETWEEN @date1 AND @date2";

            SqlParameter eleve = sqlCommand.Parameters.Add("@eleve", System.Data.SqlDbType.Int);
            SqlParameter date1 = sqlCommand.Parameters.Add("@date1", System.Data.SqlDbType.DateTime);
            SqlParameter date2 = sqlCommand.Parameters.Add("@date2", System.Data.SqlDbType.DateTime);


            eleve.Value = idEleve;
            date1.Value = date + " 00:00:00";
            date2.Value = date + " 23:59:59";


            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Lecon uneLecon = new Lecon
                    {
                        ModeleVehicule = $"{reader[0]}",
                        DateHeure = Convert.ToDateTime($"{reader[1]}"),
                        IdEleve = Convert.ToInt32($"{reader[2]}"),
                        IdMoniteur = Convert.ToInt32($"{reader[3]}"),
                        Duree = Convert.ToInt32($"{reader[4]}"),
                    };
                    ListLecons.Add(uneLecon);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListLecons;
        }

        /// <summary>
        /// Récupérer des leçon pour un modèle de véhicule donné
        /// </summary>
        /// <param name="mod">modèle de véhicule</param>
        /// <returns>Liste de leçons</returns>
        public List<Lecon> GetLeconsByModele(string mod)
        {
            this.activeCon.Open();

            List<Lecon> ListLecons = new List<Lecon>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM LECON INNER JOIN MODELE ON LECON.[modèle véhicule] = MODELE.[modèle véhicule] INNER JOIN MONITEUR ON LECON.[id moniteur] = MONITEUR.[id moniteur] INNER JOIN ELEVE ON LECON.[id élève] = ELEVE.[id élève] INNER JOIN CALENDRIER ON LECON.[date heure] = CALENDRIER.[date heure] WHERE LECON.[modèle véhicule] = @mod";

            SqlParameter modele = sqlCommand.Parameters.Add("@mod", System.Data.SqlDbType.VarChar);
            modele.Value = mod;

            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Lecon uneLecon = new Lecon();

                    uneLecon.ModeleVehicule = $"{reader[0]}";
                    uneLecon.DateHeure = Convert.ToDateTime($"{reader[1]}");
                    uneLecon.IdEleve = Convert.ToInt32($"{reader[2]}");
                    uneLecon.IdMoniteur = Convert.ToInt32($"{reader[3]}");
                    uneLecon.Duree = Convert.ToInt32($"{reader[4]}");

                    uneLecon.ModeleVehiculeNavigation.ModeleVehicule = $"{reader[5]}";
                    uneLecon.ModeleVehiculeNavigation.Marque = $"{reader[6]}";
                    uneLecon.ModeleVehiculeNavigation.Annee = $"{reader[7]}";
                    uneLecon.ModeleVehiculeNavigation.DateAchat = Convert.ToDateTime($"{reader[8]}");

                    uneLecon.IdMoniteurNavigation.IdMoniteur = Convert.ToInt32($"{reader[9]}");
                    uneLecon.IdMoniteurNavigation.NomMoniteur = $"{reader[10]}";
                    uneLecon.IdMoniteurNavigation.PrenomMoniteur = $"{reader[11]}";
                    uneLecon.IdMoniteurNavigation.DateNaissance = Convert.ToDateTime($"{reader[12]}");
                    uneLecon.IdMoniteurNavigation.DateEmbauche = Convert.ToDateTime($"{reader[13]}");
                    uneLecon.IdMoniteurNavigation.Activite = Convert.ToBoolean($"{reader[14]}");

                    uneLecon.IdEleveNavigation.IdEleve = Convert.ToInt32($"{reader[15]}");
                    uneLecon.IdEleveNavigation.NomEleve = $"{reader[16]}";
                    uneLecon.IdEleveNavigation.PrenomEleve = $"{reader[17]}";
                    uneLecon.IdEleveNavigation.Code = Convert.ToBoolean($"{reader[18]}");
                    uneLecon.IdEleveNavigation.Conduite = Convert.ToBoolean($"{reader[19]}");
                    uneLecon.IdEleveNavigation.DateNaissance = Convert.ToDateTime($"{reader[20]}");

                    uneLecon.DateHeureNavigation.DateHeure = Convert.ToDateTime($"{reader[21]}");



                    ListLecons.Add(uneLecon);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListLecons;

        }

    }
}
