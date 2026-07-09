using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Persona.Models
{
    public class Persona : IValidatableObject
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string Direccion { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FechaDeNacimiento > DateTime.Today.AddYears(-18))
            {
                yield return new ValidationResult("Age must be above 18.", new[] { nameof(FechaDeNacimiento) });
            }
        }
    }
}
