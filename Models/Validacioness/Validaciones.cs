using System;
using System.ComponentModel.DataAnnotations;
using InmobiliariaGutierrez.Models.DAO;
using InmobiliariaGutierrez.Models.VO;


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
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage ?? "La fecha no debe ser menor que la fecha actual.");
                }
            }
            return ValidationResult.Success; // Permitir valores nulos
        }


}


public class InquilinoGuardarValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var inquilino = (Inquilino)value;
        
        // Si el contexto es guardar, se requiere DNI y Nombre.
        if (validationContext.Items.ContainsKey("Contexto") && 
            validationContext.Items["Contexto"].ToString() == "Guardar")
        {
            if (string.IsNullOrEmpty(inquilino.DNI) || string.IsNullOrEmpty(inquilino.Nombre))
            {
                return new ValidationResult("DNI y Nombre son obligatorios.");
            }
        }
        // En otro contexto, no se requiere DNI y Nombre.
        else
        {
            return ValidationResult.Success;
        }

        return ValidationResult.Success;
    }
}


        public class ExisteInmueble : ValidationAttribute
        {
              protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null){
                return new ValidationResult("Debe ingresar un inmueble.");
            }   
            
            Inmueble inmueble=(Inmueble)value;
            RepositorioInmueble ri=new RepositorioInmueble();
            if(ri.GetInmueble(inmueble.Id)==null)
                return new ValidationResult("El inmueble no pertenece a nuestro registro.");
            
                
            return ValidationResult.Success;
            

    }
    }


    
        public class ExisteInquilino : ValidationAttribute
        {
              protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null){
                return new ValidationResult("Debe ingresar un inquilino.");
            }   
            
            Inquilino Inquilino=(Inquilino)value;
            RepositorioInquilino ri=new RepositorioInquilino();
            if(ri.GetInquilino(Inquilino.Id)==null)
                return new ValidationResult("El Inquilino no pertenece a nuestro registro.");
            

            if (validationContext.ObjectInstance is Contrato)
            {
                if (Inquilino.Id == 0)
                {
                    return new ValidationResult("El campo Id es requerido.");
                }
            }

            return ValidationResult.Success;
            

    }
    }

}