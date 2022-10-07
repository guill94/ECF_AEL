using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECF_AutoEcole.Models
{
    public class Lecon
    {
        public Lecon()
        {
            this.IdMoniteurNavigation = new Moniteur();
            this.ModeleVehiculeNavigation = new Modele();
            this.DateHeureNavigation = new Calendrier();
            this.IdEleveNavigation = new Eleve();
        }

        /// <summary>
        /// Le modèle du véhicule pour la leçon
        /// </summary>
        private string modeleVehicule;
        /// <summary>
        /// Obtient ou définit le modèle du véhicule
        /// </summary>
        public string ModeleVehicule
        {
            get { return modeleVehicule; }
            set { modeleVehicule = value; }
        }

        /// <summary>
        /// La date et heure de la leçon
        /// </summary>
        private DateTime dateHeure;
        /// <summary>
        /// Obtient ou définit la date et l'heure
        /// </summary>
        public DateTime DateHeure
        {
            get { return dateHeure; }
            set { dateHeure = value; }
        }

        /// <summary>
        /// L'identifiant de l'élève associée à la leçon
        /// </summary>
        private int idEleve;
        /// <summary>
        /// Obtient ou définint l'id de l'élève
        /// </summary>
        public int IdEleve
        {
            get { return idEleve; }
            set { idEleve = value; }
        }

        /// <summary>
        /// L'identifiant du moniteur associé à la leçon
        /// </summary>
        private int idMoniteur;
        /// <summary>
        /// Obtient ou définit l'id du moniteur
        /// </summary>
        public int IdMoniteur
        {
            get { return idMoniteur; }
            set { idMoniteur = value; }
        }

        /// <summary>
        /// La diurée de la leçon
        /// </summary>
        private int duree;
        /// <summary>
        /// Obtient ou définit la durée
        /// </summary>
        public int Duree
        {
            get { return duree; }
            set { duree = value; }
        }

        public virtual Calendrier DateHeureNavigation { get; set; } = null!;
        public virtual Moniteur IdMoniteurNavigation { get; set; } = null!;
        public virtual Eleve IdEleveNavigation { get; set; } = null!;
        public virtual Modele ModeleVehiculeNavigation { get; set; } = null!;
    }
}
