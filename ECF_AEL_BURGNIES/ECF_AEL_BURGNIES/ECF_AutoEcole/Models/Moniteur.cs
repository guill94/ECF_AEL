using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECF_AutoEcole.Models
{
    public class Moniteur
    {
        public Moniteur()
        {
            Lecons = new List<Lecon>();
        }

        /// <summary>
        /// Identifiant moniteur
        /// </summary>
        private int idMoniteur;
        /// <summary>
        /// Obtient ou définit l'id moniteur
        /// </summary>
        public int IdMoniteur
        {
            get { return idMoniteur; }
            set { idMoniteur = value; }
        }

        /// <summary>
        /// Nom du moniteur
        /// </summary>
        private string nomMoniteur;
        /// <summary>
        /// Obtient ou définit le nom du moniteur
        /// </summary>
        public string NomMoniteur
        {
            get { return nomMoniteur; }
            set { nomMoniteur = value; }
        }

        /// <summary>
        /// Prénom du moniteur
        /// </summary>
        private string prenomMoniteur;
        /// <summary>
        /// Obtient ou définit le prénom du moniteur
        /// </summary>
        public string PrenomMoniteur
        {
            get { return prenomMoniteur; }
            set { prenomMoniteur = value; }
        }

        /// <summary>
        /// Date naissance moniteur
        /// </summary>
        private DateTime dateNaissance;
        /// <summary>
        /// Obtient ou définit date naissance moniteur
        /// </summary>
        public DateTime DateNaissance
        {
            get { return dateNaissance; }
            set { dateNaissance = value; }
        }

        /// <summary>
        /// Date embauche moniteur
        /// </summary>
        private DateTime dateEmbauche;
        /// <summary>
        /// Obtient ou définit date embauche moniteur
        /// </summary>
        public DateTime DateEmbauche
        {
            get { return dateEmbauche; }
            set { dateEmbauche = value; }
        }

        /// <summary>
        /// Si le moniteur est en activité ou non
        /// </summary>
        private bool? activite;
        /// <summary>
        /// Obtient ou définit si le moniteur est en activité ou non
        /// </summary>
        public bool? Activite
        {
            get { return activite; }
            set { activite = value; }
        }

        /// <summary>
        /// Liste des leçons du moniteur
        /// </summary>
        public virtual ICollection<Lecon> Lecons { get; set; }
    }
}
