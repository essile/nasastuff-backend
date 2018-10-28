namespace NasasivustonAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Key]
        public int User_id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nickname { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public byte[] Photo { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public int PasswordHash { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
