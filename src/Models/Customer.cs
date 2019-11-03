using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrSampleApi.Models
{
    /// <summary>
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        [MaxLength(120)]
        public string Email { get; set; }

        /// <summary>
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// </summary>
        public ICollection<Order> Orders { get; set; }
    }
}
