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
		return View();
	}


	//***************************************************************************************************************************************************
		public ActionResult Create()
		{
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View();
		} 




//********************************************************************************************************************************************
        [HttpPost]
		public ActionResult Create(Usuario u)
		{
			if (!ModelState.IsValid)
				return View();
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
				return RedirectToAction(nameof(Index));
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
		{
			try
			{
				// url de retorno= esto redirecciona a donnde se quiso acceder sin estar logueado
				var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string) ? "/Home" : TempData["returnUrl"].ToString();
				if (ModelState.IsValid)
				{
					// hasheo la clave
					string passIngresada = Convert.ToBase64String(KeyDerivation.Pbkdf2(
						password: login.Pass,
						salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
						prf: KeyDerivationPrf.HMACSHA1,
						iterationCount: 1000,
						numBytesRequested: 256 / 8));

					// lo busco al user por email
					var u = repositorio.ObtenerPorEmail(login.Usuario);
					
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
						new Claim("id", $"{u.Id}")
					};

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

}