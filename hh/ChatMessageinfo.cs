using App.EnglishBuddy.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("chatmessage")]
    public class ChatMessageinfo : BaseEntity
    {
        [Column("sender_name")]
        public string SenderName {  get; set; }

        [Column("receiver_name")]
        public string ReceiverName { get; set; }

        [Column("sender_id")]
        public Guid SenderId { get; set; }

        [Column("receiver_id")]
        public Guid ReceiverId { get; set; }

        [Column("message")]
        public string Message { get; set; }

        //[Column("time")]
        //public DateTime Time { get; set; } 

        [Column("connection_id")]
        public Guid ConnectionId { get; set; }
    }
}
