using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECF_AutoEcole.Models
{
    public class Vehicule
    {
        public Vehicule()
        {
            
        }

        /// <summary>
        /// Numéro d'immatriculation du véhicule
        /// </summary>
        private string nImmatriculation;
        /// <summary>
        /// Obtient ou définit l'immat du véhicule
        /// </summary>
        public string NImmatriculation
        {
            get { return nImmatriculation; }
            set { nImmatriculation = value; }
        }

        /// <summary>
        /// Le modèle du véhicule
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
        /// Si le véhicule est en service ou non
        /// </summary>
        private bool? etat;
        /// <summary>
        /// Obtient ou définit si le véhicule est en ser vice ou non
        /// </summary>
        public bool? Etat
        {
            get { return etat; }
            set { etat = value; }
        }

        public Modele ModeleV { get; set; }

    }
}
