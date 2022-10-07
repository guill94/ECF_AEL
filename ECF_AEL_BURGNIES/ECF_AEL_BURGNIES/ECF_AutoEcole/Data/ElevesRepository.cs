using ECF_AutoEcole.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ECF_AutoEcole.Data
{
    public class ElevesRepository
    {
        private SqlConnection activeCon;

        public ElevesRepository()
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
            sqlCommand.CommandText = "SELECT max([id élève]) FROM ELEVE";

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
        /// Permet de faire une recherche sur le nom et prénom de l'élève
        /// </summary>
        /// <param name="search">La chaîne entrée par l'utilisateur dans l'input de recherche</param>
        /// <returns>Les résultats correspondant à la chaîne entrée</returns>
        public List<Eleve> FilterEleve(string search)
        {
            this.activeCon.Open();

            List<Eleve> ListEleves = new List<Eleve>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM ELEVE WHERE [nom élève] LIKE @search_string OR [prénom élève] LIKE @search_string";

            SqlParameter SearchString = sqlCommand.Parameters.AddWithValue("@search_string", System.Data.SqlDbType.VarChar);
            SearchString.Value = "%" + search + "%";

            try
            {
                SqlDataReader eleves = sqlCommand.ExecuteReader();

                while (eleves.Read())
                {
                    Eleve unEleve = new Eleve
                    {
                        IdEleve = Convert.ToInt32($"{eleves[0]}"),
                        NomEleve = $"{eleves[1]}",
                        PrenomEleve = $"{eleves[2]}",
                        Code = Convert.ToBoolean($"{eleves[3]}"),
                        Conduite = Convert.ToBoolean($"{eleves[4]}"),
                        DateNaissance = Convert.ToDateTime($"{eleves[5]}"),
                    };
                    ListEleves.Add(unEleve);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListEleves;

        }


        /// <summary>
        /// Permet de récupérer toutes les entrées dans la table
        /// </summary>
        /// <returns>Liste d'élèves (Tous ceux présents dans la table)</returns>
        public List<Eleve> GetAllEleves()
        {
            this.activeCon.Open();
            List<Eleve> ListEleves = new List<Eleve>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM ELEVE";

            try
            {
                SqlDataReader eleve = sqlCommand.ExecuteReader();

                while (eleve.Read())
                {
                    Eleve unEleve = new Eleve
                    {
                        IdEleve = Convert.ToInt32($"{eleve[0]}"),
                        NomEleve = $"{eleve[1]}",
                        PrenomEleve = $"{eleve[2]}",
                        Code = Convert.ToBoolean($"{eleve[3]}"),
                        Conduite = Convert.ToBoolean($"{eleve[4]}"),
                        DateNaissance = Convert.ToDateTime($"{eleve[5]}"),
                    };
                    ListEleves.Add(unEleve);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListEleves;
        }

        public List<Eleve> GetAllElevesWithoutConduite()
        {
            this.activeCon.Open();
            List<Eleve> ListEleves = new List<Eleve>();

            SqlCommand sqlCommand = activeCon.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM ELEVE WHERE [conduite] = 0";

            try
            {
                SqlDataReader eleve = sqlCommand.ExecuteReader();

                while (eleve.Read())
                {
                    Eleve unEleve = new Eleve
                    {
                        IdEleve = Convert.ToInt32($"{eleve[0]}"),
                        NomEleve = $"{eleve[1]}",
                        PrenomEleve = $"{eleve[2]}",
                        Code = Convert.ToBoolean($"{eleve[3]}"),
                        Conduite = Convert.ToBoolean($"{eleve[4]}"),
                        DateNaissance = Convert.ToDateTime($"{eleve[5]}"),
                    };
                    ListEleves.Add(unEleve);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.activeCon.Close();

            return ListEleves;
        }



        /// <summary>
        /// Création d'un nouvel élève
        /// </summary>
        /// <param name="eleve">Objet de type élève</param>
        public void CreateEleve(Eleve eleve)
        {
            //this.activeCon.Open();
            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO ELEVE ([id élève] ,[nom élève], [prénom élève], code, conduite, [date naissance]) VALUES (@id, @nom, @prenom, @code, @conduite, @dateNaissance)";

            SqlParameter id = sqlCommand.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SqlParameter nom = sqlCommand.Parameters.Add("@nom", System.Data.SqlDbType.VarChar);
            SqlParameter prenom = sqlCommand.Parameters.Add("@prenom", System.Data.SqlDbType.VarChar);
            SqlParameter code = sqlCommand.Parameters.Add("@code", System.Data.SqlDbType.Bit);
            SqlParameter conduite = sqlCommand.Parameters.Add("@conduite", System.Data.SqlDbType.Bit);
            SqlParameter dateNaissance = sqlCommand.Parameters.Add("@dateNaissance", System.Data.SqlDbType.DateTime);

            id.Value = eleve.IdEleve;
            nom.Value = eleve.NomEleve;
            prenom.Value = eleve.PrenomEleve;
            code.Value = eleve.Code;
            conduite.Value = eleve.Conduite;
            dateNaissance.Value = eleve.DateNaissance;

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
        /// Mise à jour des données d'un élève
        /// </summary>
        /// <param name="eleve">Objet de type élève</param>
        public void UpdateEleve(Eleve eleve)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand1 = this.activeCon.CreateCommand();

            if (eleve.Conduite == true)
            {    
                sqlCommand1.CommandText = "DELETE FROM LECON WHERE [id élève] = @eleve";
                
                SqlParameter idEleve = sqlCommand1.Parameters.Add("@eleve", System.Data.SqlDbType.Int);
                idEleve.Value = eleve.IdEleve;

            }


            SqlCommand sqlCommand2 = this.activeCon.CreateCommand();
            sqlCommand2.CommandText = "UPDATE ELEVE SET [nom élève] = @nom, [prénom élève] = @prenom, code = @code, conduite = @conduite, [date naissance] = @dateNaissance WHERE [id élève] = @id";

            SqlParameter id = sqlCommand2.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SqlParameter nom = sqlCommand2.Parameters.Add("@nom", System.Data.SqlDbType.VarChar);
            SqlParameter prenom = sqlCommand2.Parameters.Add("@prenom", System.Data.SqlDbType.VarChar);
            SqlParameter code = sqlCommand2.Parameters.Add("@code", System.Data.SqlDbType.Bit);
            SqlParameter conduite = sqlCommand2.Parameters.Add("@conduite", System.Data.SqlDbType.Bit);
            SqlParameter dateNaissance = sqlCommand2.Parameters.Add("@dateNaissance", System.Data.SqlDbType.DateTime);

            id.Value = eleve.IdEleve;
            nom.Value = eleve.NomEleve;
            prenom.Value = eleve.PrenomEleve;
            code.Value = eleve.Code;
            conduite.Value = eleve.Conduite;
            dateNaissance.Value = eleve.DateNaissance;

            try
            {
                if (eleve.Conduite == true)
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
        /// Suppression d'un élève
        /// </summary>
        /// <param name="Id">L'Id de l'élève à supprimer</param>
        public void DeleteEleve(int Id)
        {
            this.activeCon.Open();
            SqlCommand sqlCommand = this.activeCon.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM LECON WHERE [id élève] = @id";
            SqlParameter id = sqlCommand.Parameters.Add("@id", System.Data.SqlDbType.Int);
            id.Value = Id;

            SqlCommand sqlCommand2 = this.activeCon.CreateCommand();
            sqlCommand2.CommandText = "DELETE FROM ELEVE WHERE [id élève] = @id";
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

