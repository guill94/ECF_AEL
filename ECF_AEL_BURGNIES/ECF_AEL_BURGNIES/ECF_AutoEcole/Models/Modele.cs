using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECF_AutoEcole.Models
{
    public class Modele
    {
        public Modele()
        {
            Lecons = new List<Lecon>();
            Vehicules = new List<Vehicule>();
        }
        /// <summary>
        /// Le modèle de véhicule
        /// </summary>
        private string modeleVehicule;
        /// <summary>
        /// Obtient ou définit le modèle de véhicule
        /// </summary>
        public string ModeleVehicule
        {
            get { return modeleVehicule; }
            set { modeleVehicule = value; }
        }

        /// <summary>
        /// La marque associée au modèle
        /// </summary>
        private string marque;
        /// <summary>
        /// Obtient ou définit la marque
        /// </summary>
        public string Marque
        {
            get { return marque; }
            set { marque = value; }
        }

        /// <summary>
        /// L'année de construction du modèle
        /// </summary>
        private string annee;
        /// <summary>
        /// Obtient ou définit l'année
        /// </summary>
        public string Annee
        {
            get { return annee; }
            set { annee = value; }
        }

        /// <summary>
        /// Date achat du modèle
        /// </summary>
        private DateTime dateAchat;
        /// <summary>
        /// Obtient ou définit la date d'achat
        /// </summary>
        public DateTime DateAchat
        {
            get { return dateAchat; }
            set { dateAchat = value; }
        }

        /// <summary>
        /// Les leçons associées à ce modèle
        /// </summary>
        public virtual ICollection<Lecon> Lecons { get; set; }
        /// <summary>
        /// Les véhicules associés à ce modèle
        /// </summary>
        public virtual ICollection<Vehicule> Vehicules { get; set; }
    }
}
