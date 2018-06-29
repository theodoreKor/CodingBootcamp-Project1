using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheodoreKoronaios_P1
{
    public class User
    {
        // properties
        public int UserId { get; set; } // primary Key
        public string Username { get; set; }
        public string Password { get; set; }

        public DateTime DateCreated { get; set; }


        public int UserType { get; set; }
        public bool IsUserActive { get; set; } // to check for deleted users


        //public ICollection<Message> Messages { get; set; } // navigation property

        [InverseProperty("Sender")]
        public ICollection<Message> SentMessages { get; set; } // navigation property

        [InverseProperty("Recipient")]
        public ICollection<Message> ReceivedMessages { get; set; } // navigation property

        //constructor
        public User(string username, string password, int userType)
        {
            Username = username;
            Password = password;
            DateCreated = DateTime.Now;
            UserType = userType;
            IsUserActive = true;

        }

        // default constructor
        public User()
        {

        }

    }
}
