using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Transcode.Models
{
    public class Payment : IEntity
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ConversionId { get; set; }
        public float AmountPaid { get; set; }
        public DateTime Date { get; set; }
        public String Url { get; set; }
        [ForeignKey("conversionId")]
        public virtual Conversion conversion { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
