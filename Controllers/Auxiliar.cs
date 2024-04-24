/*

[Authorize(Policy ="Administrador")]
    public IActionResult Eliminar(int id)
	{   
      
        DateTime FechaFinparametro=DateTime.Now;;// tiene wue venir porparametro
		RepositorioContrato rc= new RepositorioContrato();
        RepositorioPago rp=new RepositorioPago();
        
        IList<Pago> pago ;
         pago=rp.GetPago(id);
        Contrato contrato=rc.GetContrato(id);
         DateTime fechaActual = DateTime.Now;
         
        
		rc.EliminarContrato(id);
		return RedirectToAction(nameof(Index),new { page = 1});
        
	}

[HttpGet]
    public IActionResult Editar(int id=0, Contrato? contrato=null)
	{    RepositorioInmuebleTipo rit=new RepositorioInmuebleTipo();
             ViewBag.ListaTipos=rit.GetInmuebleTipos();


        if(TempData.ContainsKey("msg"))
           ViewBag.msg=TempData["msg"];
        if(TempData.ContainsKey("errores"))
           ViewBag.errores=TempData["errores"];  
		RepositorioContrato rc = new RepositorioContrato();
        RepositorioInquilino ri=new RepositorioInquilino();
        if(id!=0){
         Contrato c=rc.GetContrato(id);
         return View(c);
        }

		//ri.BuscarPorTodosLosCampos();
		return View();
	}








    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


[HttpGet]
[HttpPost]
    public IActionResult FormContrato(Contrato? contrato,int idInq=0,int idInm=0){
            var rc=new RepositorioContrato();

            
            if(idInq!=0){

            ViewBag.idInq=idInq;
            ViewBag.idInm=idInm;
            }
            
          if(contrato.InquilinoId!=null){
            rc.AltaContrato(contrato);
           } 
            return View();
		
	}
    

[HttpPost]
	public IActionResult Guardar(Contrato contrato)
	{  int alta=0;

Console.WriteLine("*****************************************************contrato");
        Console.WriteLine(contrato);
        if (!ModelState.IsValid) {
            Console.WriteLine("***************************************************** Entro");
            foreach (var kvp in ModelState)
    {
        foreach (var error in kvp.Value.Errors)
        {
            Console.WriteLine(error.ErrorMessage);
        }
    }

             RepositorioInmuebleTipo rit=new RepositorioInmuebleTipo();
             ViewBag.ListaTipos=rit.GetInmuebleTipos();
              ViewBag.errores=true;
            return View("Editar", contrato);
            
            }

        
        string msg="";
		RepositorioContrato rc = new RepositorioContrato();
		RepositorioPago rp = new RepositorioPago();
        
		
		if(contrato.Id > 0){
			rc.ModificaContrato(contrato);
            msg="Contrato Actualizado.";
        }
		else{
			int id =rc.AltaContrato(contrato);
            Inquilino inquilino;
            RepositorioInquilino ri=new RepositorioInquilino();
            Contrato contrato1=rc.GetContrato(id);
            string idUsuario = HttpContext.User.FindFirst("id")?.Value;
            
            if (string.IsNullOrEmpty(idUsuario))
            {
                
                return RedirectToAction("Error");
            }
              else{

                string idUsuarioStr=idUsuario ;
                int idsuario;
                if (int.TryParse(idUsuarioStr, out idsuario))
                {
                    RepositorioAuditoriac rac=new RepositorioAuditoriac();
                    Usuario usuario=new Usuario();
                    Auditoriacontrato auditoriac=new Auditoriacontrato();
                    auditoriac.UsuarioId=new Usuario();
                    auditoriac.UsuarioId.Id=idsuario;
                    auditoriac.FechaInicio=DateTime.Now;
                    auditoriac.FechaCancelacion=null;
                     auditoriac.ContratoId=new Contrato();
                    auditoriac.ContratoId.Id=id;
                    rac.InsertAucitoriac(auditoriac);

                    
                }

              


               
              }

                msg="Contrato registrado.";
          return RedirectToAction("Prepago", "Pago", new { idContrato = id, idInquilino = contrato1.InquilinoId.Id });




            /*var pago=new Pago{
                NumeroPago=1,
                ContratoId=contrato.Id,
                Fecha=contrato.FechaInicio,
                FechaPago=contrato.FechaInicio
            };
            rp.InsertPago(pago);
       
           
            alta=1;
          
        }
        TempData["msg"]=msg;
        if (alta==1){

            
        }

		return RedirectToAction(nameof(Editar));
	
	}


    public IActionResult GetListaInmueblePag(int page,int limit=5,ViewInquilinoFiltrarInmueble filtro=null)
    {   

        
        int offset=(page-1)*limit;
		var ri=new RepositorioInmueble();
        IList<Inmueble>lista =ri.GetInmueblesPaginadoFiltrado(limit,offset,filtro);
		int totalReg=ri.getCantidadRegistrosFiltrado(filtro);
		int cantidadPaginas=totalReg/limit;
		cantidadPaginas=totalReg%limit!=0?++cantidadPaginas:cantidadPaginas;
		
		var objetoView=new IndexView{ListaInmuebles=lista};
		objetoView.PrimerNumero=page%limit==0?page-4:((page/limit)*limit+1);
		objetoView.Page=page;
		objetoView.CantidadPaginas=cantidadPaginas;

		return Json(objetoView);
        
    }
   
   public IActionResult Finalizar(int Id){
    RepositorioContrato rc=new RepositorioContrato();
    
    Contrato contrato=rc.GetContrato(Id);
    if (contrato.FechaFinAnticipada > new DateTime(1, 1, 1, 0, 0, 0))
{
  contrato.tienefechapactada=true;
   RepositorioPago rp=new RepositorioPago();
    IList<Pago> pagos;
    pagos=rp.GetPago(Id);
    int cont=pagos.Count();
    contrato.preciototal=pagos[cont-1].Importe;
    pagosstatic=contrato.preciototal;

}
else{
    contrato.tienefechapactada=false;
   
}
    return View(contrato);
   }
 public IActionResult Calcular([FromBody] List<Arreglo> arreglo)
    {
            RepositorioContrato rc=new RepositorioContrato();
            RepositorioPago rp=new RepositorioPago();
            IList<Pago> pagos;
            Contrato contrato = new Contrato();
            
            string[] partesId = arreglo[0].Id.Split('/');
            int Id = Convert.ToInt32(partesId[0]);
            DateTime FechaFinparametro = DateTime.Parse(partesId[1]);
            contrato=rc.GetContrato(Id);
            pagos=rp.GetPago(Id);
            
          

            
     int diferenciaMeses = (FechaFinparametro.Year-contrato.FechaInicio.Year  ) * 12 + FechaFinparametro.Month-contrato.FechaInicio.Month ;
     Console.WriteLine( "diferenciaMeses");
      Console.WriteLine( diferenciaMeses);
      Console.WriteLine( "diferenciaMeses");
             int  mitad=(contrato.FechaFin.Year-contrato.    FechaInicio.Year  ) * 12 + contrato.FechaFin.Month-contrato.FechaInicio.Month ;
     mitad=mitad/2 ;

  int fechamulta = (contrato.FechaFin.Year-FechaFinparametro.Year  ) * 12 + contrato.FechaFin.Month-FechaFinparametro.Month ;
           
            int deudas=0;
            int cantidadpagos=0;
            decimal multa=0;
           for(int i = 0; i<pagos.Count; i++){
            if(pagos[i].FechaPago>DateTime.MinValue)
            {
                cantidadpagos++;

            }else{
                deudas++;
            }
       
           }

            mesestotaldeuda=diferenciaMeses-cantidadpagos;
                
      
      int mesesadeudado=mesestotaldeuda;
      int mesesmulta=0;
           

            
            if (fechamulta>mitad){
                mesestotaldeuda=mesestotaldeuda+2;
                mesesmulta=mesesmulta+2;
            }
            else{
                mesestotaldeuda++;
                mesesmulta=mesesmulta+1;
            }
            multa=mesestotaldeuda*pagos[0].Importe;
           


       var resultado = new
    {
        Mensaje = "Tiene un total de meses adeudado: " + mesesadeudado + ", y una multa de " + mesesmulta+" meses de alquiler"+ "total a pagar es " + multa, 
        mesesadeudado = mesesadeudado,
        mesesmulta=mesesmulta,
        multa=multa
    };
pagosstatic=multa;

   
        return Json(resultado);
    }


     public IActionResult PagarCuota(int Id){
        RepositorioContrato rc=new RepositorioContrato();
        RepositorioPago rp=new RepositorioPago();
        Contrato contrato=new Contrato();
        IList<Pago>pagos;
        pagos=rp.GetPago(Id);
        Pago pago=new Pago();
          pagos= rp.GetPago(Id);
        pago.NumeroPago=pagos.Count()+1;
       
         


 Console.WriteLine("go");


        try{
        DateTime fechaactual=DateTime.Now;
        contrato=rc.GetContrato(Id);
        contrato.FechaFinAnticipada=fechaactual;
        contrato.Estado=false;
        rc.ModificaContratoparmi(contrato);
       
        pago.Fecha=contrato.FechaFin;
        pago.FechaPago=DateTime.Now;
        pago.Importe=pagosstatic;
        pago.ContratoId=Id;
        Console.WriteLine(pago);
        rp.InsertPago(pago);
        //-------------------------------Auditoria------------------------------------------
         string idUsuario = HttpContext.User.FindFirst("id")?.Value;
            
            if (string.IsNullOrEmpty(idUsuario))
            {
                
                return RedirectToAction("Error");
            }
              else{

                string idUsuarioStr=idUsuario ;
                int idsuario;
                if (int.TryParse(idUsuarioStr, out idsuario))
                {
                    RepositorioAuditoriac rac=new RepositorioAuditoriac();
                    Usuario usuario=new Usuario();
                    Auditoriacontrato auditoriac=new Auditoriacontrato();
                    auditoriac.UsuarioId=new Usuario();
                    auditoriac.UsuarioId.Id=idsuario;
                    auditoriac.FechaInicio=null;
                    auditoriac.FechaCancelacion=DateTime.Now;
                    auditoriac.ContratoId=new Contrato();
                    auditoriac.ContratoId.Id=Id;
                    rac.InsertAucitoriac(auditoriac);

                    
                }

              


               
              }





        //-------------------------------------------------------------------------
        


        return RedirectToAction(nameof(Index),new{page=1});
        }catch (Exception e){
           return Content("Ocurrió un error al intentar modificar el contrato: " + e.Message);
        }
   }

 public IActionResult Pactarfecha(int Id){
        RepositorioContrato rc=new RepositorioContrato();
        RepositorioPago rp=new RepositorioPago();
        Contrato contrato=new Contrato();
        IList<Pago>pagos;
        pagos=rp.GetPago(Id);
        Pago pago=new Pago();
          pagos= rp.GetPago(Id);
        pago.NumeroPago=pagos.Count()+1;
       
         




        try{
        DateTime fechaactual=DateTime.Now;
        contrato=rc.GetContrato(Id);
        contrato.FechaFinAnticipada=fechaactual;
        contrato.Estado=true;
        rc.ModificaContratoparmi(contrato);
       
        pago.Fecha=contrato.FechaFin;
        pago.FechaPago=null;
        pago.Importe=pagosstatic;
        pago.ContratoId=Id;
        Console.WriteLine(pago);
        rp.InsertPago(pago);
        


        return RedirectToAction(nameof(Index),new{page=1});
        }catch (Exception e){
           return Content("Ocurrió un error al intentar modificar el contrato: " + e.Message);
        }
   }


  
}

}
*/