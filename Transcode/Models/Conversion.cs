using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Transcode.Models
{
    public class Conversion : IEntity
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String UserId { get; set; }
        //public int PaymentId { get; set; }
        public DateTime Date { get; set; }
        public float Size { get; set; }
        public String Url { get; set; }
        
        public virtual string ProvidedMedia { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }
}