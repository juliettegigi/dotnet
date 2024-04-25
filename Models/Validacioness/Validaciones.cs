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
            
            // existe el inmueble pero    
            // el nnuevo contrato no puede tener un inmueble con la fecha superpuesta
            
            
            Contrato contrato=(Contrato)validationContext.ObjectInstance;
            
            RepositorioContrato rc = new RepositorioContrato();
            var lista=rc.GetContratoByInmueble(inmueble.Id);
            foreach (Contrato c in lista)
            {
                if(contrato.Id!=0){
                       if(( (c.FechaInicio<=contrato.FechaInicio && contrato.FechaInicio<=c.FechaFin) ||
                    (c.FechaInicio<=contrato.FechaFin && contrato.FechaFin<=c.FechaFin))&&
                    c.Id!=contrato.Id)
                    return new ValidationResult("El inmueble ya tiene contrato en la fecha ingresada.");
                }
                else{
                if( (c.FechaInicio<=contrato.FechaInicio && contrato.FechaInicio<=c.FechaFin) ||
                    (c.FechaInicio<=contrato.FechaFin && contrato.FechaFin<=c.FechaFin))
                              {
                                return new ValidationResult("El inmueble ya tiene contrato en la fecha ingresada.");
                              } 
            }}

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

     public class UniqueEmailDni : ValidationAttribute
        {
              protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null){
                return new ValidationResult("Debe ingresar un propietario.");
            }   
            
            
            

            if (validationContext.ObjectInstance is Propietario)
            {
                var propietario=(Propietario)validationContext.ObjectInstance ;
               RepositorioPropietario rp=new RepositorioPropietario();
               if(rp.GetPropietarioByEmailDni(propietario.Email,propietario.DNI)!=null)
                  return new ValidationResult("Ya existe un propietario con el dni o email ingresado.");
                
            }

            return ValidationResult.Success;
            

    }
    }


}