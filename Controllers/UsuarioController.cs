using Microsoft.AspNetCore.Mvc;
using InmobiliariaGutierrez.Models;
using InmobiliariaGutierrez.Models.VO;
using Microsoft.AspNetCore.Hosting;  // para el IWebHostEnviroment
using System.Security.Claims; // para poner la info del user que se loggueo 
using Microsoft.AspNetCore.Authentication;// para usar HttpContext.SignOutAsync
using Microsoft.AspNetCore.Authentication.Cookies;//para indicar q uso las cookies
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using InmobiliariaGutierrez.Models.DAO;
using System;
using Microsoft.Extensions.Logging.Console;

namespace InmobiliariaGutierrez.Controllers;

public class UsuarioController : Controller
	{  
        private readonly IConfiguration configuration;
		private readonly IWebHostEnvironment environment;
        private readonly ILogger<UsuarioController> _logger;
         private readonly RepositorioUsuario repositorio;
        public UsuarioController(IConfiguration configuration,ILogger<UsuarioController> logger,IWebHostEnvironment env )
        {
            _logger = logger;
            this.configuration = configuration;
            this.repositorio = new RepositorioUsuario();
			environment=env;
            }
	
      public IActionResult Index(){
		IList<Usuario> usuarios=new List<Usuario>();
		RepositorioUsuario ru=new RepositorioUsuario();
		usuarios=ru.ObtenerTodos();
		
		
		return View(usuarios);
	}


	//***************************************************************************************************************************************************
		public ActionResult Create()
		{
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View();
		} 




	//***************************************************************************************************************************************************
		public ActionResult Editar(int id)
		{   Console.WriteLine("********************************************* editaaaaaaaaaaar");
		    Console.WriteLine(id);
			RepositorioUsuario ru = new RepositorioUsuario();
			Usuario usuario = ru.ObtenerPorId(id);
			Console.WriteLine(usuario.Rol);
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View(usuario);
			} 




//*************************************************************************************************************
//********************************************************************************************************************************************
	//***************************************************************************************************************************************************
		public ActionResult Baja(int id)
		{ repositorio.Baja(id);
			 return RedirectToAction("Index");
			} 




//********************************************************************************************************************************************
        [HttpPost]
		public ActionResult Create(Usuario u)

		{
			
			try
			{
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
								password: u.Pass,
								salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 1000,
								numBytesRequested: 256 / 8));
				u.Pass = hashed;
			
			
				
				
				
				int res = repositorio.AltaUsuario(u);
				if (u.AvatarFile != null && u.Id > 0)
				{
					string wwwPath = environment.WebRootPath;
					string path = Path.Combine(wwwPath, "Uploads");
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
					string fileName = "avatar_" + u.Id + Path.GetExtension(u.AvatarFile.FileName);
					string pathCompleto = Path.Combine(path, fileName);
					u.Avatar = Path.Combine("/Uploads", fileName);
					using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
					{
						u.AvatarFile.CopyTo(stream);
					}
					repositorio.Modificacion(u);
				}
				
				TempData["msg"]="✔ Ya estás registrado, ahora podés loggearte.";
				return RedirectToAction("Index", "Home");
			}
			catch (Exception ex)
			{
				ViewBag.Roles = Usuario.ObtenerRoles();
				return View();
			}
		}
//*****************************************************************************************************************************
[HttpGet]
public ActionResult Login(string? returnUrl)
		{
			TempData["returnUrl"] = returnUrl;
			return View();
		}

//*********************************************************************************************************************		
[HttpPost]
public async Task<IActionResult> Login(ViewLogin login)
		{ RepositorioUsuario ru=new RepositorioUsuario();
			try
			{
				// url de retorno= esto redirecciona a donnde se quiso acceder sin estar logueado
				var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string) ? "/Home" : TempData["returnUrl"].ToString();
				if (ModelState.IsValid)
				{  Console.WriteLine("hola");
					// hasheo la clave
					string passIngresada = Convert.ToBase64String(KeyDerivation.Pbkdf2(
						password: login.Pass,
						salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
						prf: KeyDerivationPrf.HMACSHA1,
						iterationCount: 1000,
						numBytesRequested: 256 / 8));
                     Console.WriteLine(login.Usuario);
					// lo busco al user por email
				 Usuario u = ru.ObtenerPorEmail(login.Usuario);

					Console.WriteLine(u.Apellido);
					//  si puso cualquier email o la pass es culquira
					if (u == null || u.Pass != passIngresada)
					{
						ModelState.AddModelError("", "El email o la Contraseña no son correctos");
						TempData["returnUrl"] = returnUrl;
						return View();
					}
                    // si todo bien, si se loguea...creo la lista de Claims, guardo info del user que está loggueado para después poder accederla en cualquier lado siempre y cuando esté logged
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, u.Email),
						new Claim("FullName", u.Nombre + " " + u.Apellido),
						new Claim(ClaimTypes.Role, u.RolNombre),
						new Claim("id", $"{u.Id}"),
						new Claim("img", $"{u.Avatar}"),

					};
					Console.WriteLine(u.Avatar);

                    // al objeto le paso la lista, los datos del user, y lo 2do me crea la identidad conn cookies  
					var claimsIdentity = new ClaimsIdentity(
							claims, CookieAuthenticationDefaults.AuthenticationScheme);


                    /*se utiliza el método SignInAsync del contexto HTTP (HttpContext) para iniciar sesión en la aplicación. 
					Se pasa el esquema de autenticación por cookies y se crea un nuevo ClaimsPrincipal utilizando la ClaimsIdentity que se creó anteriormente. 
					Esto significa que el usuario se autentica correctamente y se le asigna una identidad basada en la lista de claims.*/
					await HttpContext.SignInAsync(
							CookieAuthenticationDefaults.AuthenticationScheme,
							new ClaimsPrincipal(claimsIdentity));  // ClaimsPrincipal se utiliza para llevar a cabo la autorización y el acceso a recursos protegidos.
					TempData.Remove("returnUrl");
					return Redirect(returnUrl);
				}
				TempData["returnUrl"] = returnUrl;
				return View();
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
		}

//**************************************************************************************************************************************************************
public async Task<ActionResult> Logout()
		{
			await HttpContext.SignOutAsync(
					CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}






[HttpPost]
public ActionResult Update(Usuario u)
{  


    try
    {
        if (u.Pass != null)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: u.Pass,
                                salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                                prf: KeyDerivationPrf.HMACSHA1,
                                iterationCount: 1000,
                                numBytesRequested: 256 / 8));
            u.Pass = hashed;
        }

		 repositorio.ActualizarUsuario(u);

        if (u.AvatarFile != null && u.Id > 0)
        {
            string wwwPath = environment.WebRootPath;
            string path = Path.Combine(wwwPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = "avatar_" + u.Id + Path.GetExtension(u.AvatarFile.FileName);
            string pathCompleto = Path.Combine(path, fileName);
            u.Avatar = Path.Combine("/Uploads", fileName);
            using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
            {
                u.AvatarFile.CopyTo(stream);
            }
        }

       

        return RedirectToAction(nameof(Index));
    }
    catch (Exception ex)
    {
        ViewBag.Roles = Usuario.ObtenerRoles();
        return View("Edit", u); 
    }

	
}
public ActionResult Updatese(Usuario u)
{  Console.WriteLine(u.Avatar);
    try
    {
        // Verificar si se proporciona una nueva contraseña y hashearla
        if (!string.IsNullOrEmpty(u.Pass))
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: u.Pass,
                                salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                                prf: KeyDerivationPrf.HMACSHA1,
                                iterationCount: 1000,
                                numBytesRequested: 256 / 8));
            u.Pass = hashed;
        }

        // Actualizar la información del usuario en la base de datos
if (u.EliminarFoto)
{   
    // Obtener la ruta completa del archivo de la foto
    u.Avatar = repositorio.ObtenerPorId(u.Id).Avatar;
    string wwwPath = environment.WebRootPath;
    string path = Path.Combine(wwwPath, "Uploads");

    // Eliminar la parte "/Uploads" de la ruta
    string fileName = u.Avatar.Replace("/Uploads", "");
	 Console.WriteLine("-----------");
   Console.WriteLine(path);
    Console.WriteLine("-------------");

    // Reemplazar las barras inclinadas inversas por barras inclinadas
    fileName = fileName.Replace("/", "\\");

    // Obtener la ruta completa del archivo
    string fullPath =path+fileName;
	 Console.WriteLine("-----------");
   Console.WriteLine(fullPath);
    Console.WriteLine("-------------");
    // Verificar si el archivo existe y eliminarlo
    if (System.IO.File.Exists(fullPath))
    {
        System.IO.File.Delete(fullPath);
    }

    // También puedes eliminar la referencia de la foto en el modelo del usuario si es necesario
    u.Avatar = null; 
    repositorio.ActualizarUsuario(u);

    return RedirectToAction(nameof(Index));
}


        // Manejar la carga de una nueva foto (avatar) si se proporciona
        if (u.AvatarFile != null && u.AvatarFile.Length > 0)
        {
            string wwwPath = environment.WebRootPath;
            string path = Path.Combine(wwwPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = "avatar_" + u.Id + Path.GetExtension(u.AvatarFile.FileName);
            string pathCompleto = Path.Combine(path, fileName);

            // Guardar la nueva ruta del avatar en el modelo del usuario
            u.Avatar = Path.Combine("/Uploads", fileName);

            // Guardar el archivo en el servidor
            using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
            {
                u.AvatarFile.CopyTo(stream);
            }
        }
        else if (string.IsNullOrEmpty(u.Avatar))
        {Console.WriteLine("Entrando aca");
          
            u.Avatar = repositorio.ObtenerPorId(u.Id).Avatar;
        }


        repositorio.ActualizarUsuario(u);

        return RedirectToAction(nameof(Index));
    }
    catch (Exception ex)
    {
        ViewBag.Roles = Usuario.ObtenerRoles();
        return View("Edit", u); 
    }   
}


}

//**********************************************************************************************
//**********************************************************************************************
