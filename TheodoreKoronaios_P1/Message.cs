using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheodoreKoronaios_P1
{
    public class Message
    {
        public int MessageId { get; set; } // primary Key
        public DateTime DateCreated { get; set; }

        [MaxLength(250), MinLength(1)]
        public string Content { get; set; }

        public string Subject { get; set; }

        public bool IsMessageActive { get; set; }

        //public int UserId { get; set; } // foreign key
        //public User User { get; set;  } // navigation property

        //public int SenderId { get; set; }
        //[ForeignKey("SenderId")]
        public User Sender { get; set; } // navigation property

        //public int RecipientId { get; set; }
        //[ForeignKey("RecipientId")]
        public User Recipient { get; set; }  // navigation property
    }
}
