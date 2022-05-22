using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WTW.Core.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName{ get; set; }
        public string DocumentType { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Identification
        {
            get { return $"{this.Document}{this.DocumentType}"; }
            private set { }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName
        {
            get { return $"{this.Name} {this.LastName}"; }
            private set { }
        }
    }
}
