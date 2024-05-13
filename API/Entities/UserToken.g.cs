using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace newUserdefine1.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a usertoken entity with essential details
    /// </summary>
    public class UserToken
    {
        /// <summary>
        /// Foreign key referencing the Tenant to which the UserToken belongs 
        /// </summary>
        [Required]
        public Guid TenantId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Tenant
        /// </summary>
        [ForeignKey("TenantId")]
        public Tenant? TenantId_Tenant { get; set; }

        /// <summary>
        /// Primary key for the UserToken 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Foreign key referencing the User to which the UserToken belongs 
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Navigation property representing the associated User
        /// </summary>
        [ForeignKey("UserId")]
        public User? UserId_User { get; set; }
        /// <summary>
        /// RefershToken of the UserToken 
        /// </summary>
        public string? RefershToken { get; set; }

        /// <summary>
        /// CreatedOn of the UserToken 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
    }
}