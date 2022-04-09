﻿using System.ComponentModel.DataAnnotations;

/*
 * Tout le crédit des idées utilisées dans cette classe doit être
 * porté au site Binary Intellect. Repéré à http://www.binaryintellect.net/articles/b957238b-e2dd-4401-bfd7-f0b8d984786d.aspx
 */

namespace Api
{
    public class Login
    {
        [Required(ErrorMessage = "This field is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
