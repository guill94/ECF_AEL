using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECF_AutoEcole.Models
{
    public class Eleve
    {
        public Eleve()
        {
            Lecons = new List<Lecon>();
        }

        /// <summary>
        /// Identifiant élève
        /// </summary>
        private int idEleve;
        /// <summary>
        /// Obtient ou définit l'id élève
        /// </summary>
        public int IdEleve
        {
            get { return idEleve; }
            set { idEleve = value; }
        }

        /// <summary>
        /// Le nom de l'élève
        /// </summary>
        private string nomEleve;
        /// <summary>
        /// Obtient ou définit le nom de l'élève
        /// </summary>
        public string NomEleve
        {
            get { return nomEleve; }
            set { nomEleve = value; }
        }

        /// <summary>
        /// Le prénom élève
        /// </summary>
        private string prenomEleve;
        /// <summary>
        /// Obtient ou définit le prénom de l'élève
        /// </summary>
        public string PrenomEleve
        {
            get { return prenomEleve; }
            set { prenomEleve = value; }
        }

        /// <summary>
        /// Si l'élève a obtenu le code ou non
        /// </summary>
        private bool? code;
        /// <summary>
        /// Obtient ou définit si l'élève a le code ou non
        /// </summary>
        public bool? Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// Si l'élève a le permis ou non
        /// </summary>
        private bool? conduite;
        /// <summary>
        /// Obtient ou définit si l'élève a le permis ou non 
        /// </summary>
        public bool? Conduite
        {
            get { return conduite; }
            set { conduite = value; }
        }

        /// <summary>
        /// Date naissance de l'élève
        /// </summary>
        private DateTime dateNaissance;
        /// <summary>
        /// Obtient ou définit la date de naissance de l'élève
        /// </summary>
        public DateTime DateNaissance
        {
            get { return dateNaissance; }
            set { dateNaissance = value; }
        }

      /// <summary>
      /// Liste des leçons de l'élève
      /// </summary>
        public virtual ICollection<Lecon> Lecons { get; set; }
    }
}
