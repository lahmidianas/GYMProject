using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYMProject.Models
{
    [Table("SESSION", Schema = "dbo")]
    public class Session
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("HDEBUT")]
        public int HeureDebut { get; set; }

        [Required]
        [Column("HFIN")]
        public int HeureFin { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("SessionType")]
        public string SessionType { get; set; }

        [ForeignKey("Staff")]
        [Column("StaffId")]
        public int StaffId { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
