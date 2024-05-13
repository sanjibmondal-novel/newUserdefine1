using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace newUserdefine1.Models
{
#pragma warning disable
    /// <summary> 
    /// Represents a loginmodel entity with essential details
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Required field UserName of the LoginModel 
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Required field Password of the LoginModel 
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}