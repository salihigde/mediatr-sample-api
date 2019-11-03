using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrSampleApi.Models
{
    /// <summary>
    /// </summary>
    public class Order
    {
        /// <summary>
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }
    }
}
