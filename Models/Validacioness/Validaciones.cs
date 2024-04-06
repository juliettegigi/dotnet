using System;
using System.ComponentModel.DataAnnotations;

namespace InmobiliariaGutierrez.Models.Validacioness
{
    public class FechaNoMenorQueLaActualAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime fecha = (DateTime)value;
                if (fecha >= DateTime.Today)
                {Console.WriteLine("ww");
                    return ValidationResult.Success;
                }
                else
                {Console.WriteLine("ww");
                    return new ValidationResult(ErrorMessage ?? "La fecha debe ser menor que la fecha actual.");
                }
            }
            return ValidationResult.Success; // Permitir valores nulos
        }
    }
}