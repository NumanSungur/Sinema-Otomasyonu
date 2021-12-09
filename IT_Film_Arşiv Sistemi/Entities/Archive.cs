using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Film_Arşiv_Sistemi.Entities
{
    class Archive
    {
        public int FilmID { get; set; }      
        public string FilmName { get; set; }
        public string FilmCategory { get; set; }
        public string FilmTime { get; set; }
        public string Director { get; set; }
        public string Menşei { get; set; }
        public DateTime VisionDate { get; set; }
    }
}
