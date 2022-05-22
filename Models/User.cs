using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WTW.Core.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Data { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
