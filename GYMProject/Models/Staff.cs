namespace GYMProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Staff", Schema = "dbo")]
    public class Staff
    {
        [Key]
        [Column("ID")]
        public int StaffId { get; set; }

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
        [Column("JOB")]
        public string Job { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("phone")]
        public string Phone { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}