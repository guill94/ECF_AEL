using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECF_AutoEcole.Models
{
    public class Calendrier
    {
        public Calendrier()
        {
            Lecons = new List<Lecon>();
        }

        private DateTime dateHeure;
        public DateTime DateHeure
        {
            get { return dateHeure; }
            set { dateHeure = value; }
        }


        public virtual ICollection<Lecon> Lecons { get; set; }
    }
}
