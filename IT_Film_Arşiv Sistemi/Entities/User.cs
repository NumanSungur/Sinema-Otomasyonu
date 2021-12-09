using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Film_Arşiv_Sistemi.Entities
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public User()
        {
            UserName = "Ahmet Çiçek";
            Password = "123";
            Name = "Ahmet";
            SurName = "Çiçek";
        }
    }
}
