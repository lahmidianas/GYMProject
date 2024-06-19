using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYMProject.Models
{
    [Table("Member", Schema = "dbo")]
    public class Member
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("NOM")]
        public string Nom { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("PRENOM")]
        public string Prenom { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("phone")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("TYPE")]
        public string MembershipType { get; set; }

        [Required]
        [Column("StartMembershipDate")]
        public DateTime StartMembershipDate { get; set; }

        [Column("EndMembershipDate")]
        public DateTime? EndMembershipDate { get; set; }
    }
}
