using Microsoft.AspNetCore.Authentication.Cookies; // para usar CookieAuthenticationDefaults


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// añadimos auth a los servicios. indicamos que las cookies es el método de auth de la app.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>//el sitio web valida con cookie
	{
		options.LoginPath = "/Usuarios/Login";  //  ruta a la que se redirigirá a los usuarios no autenticados cuando intenten acceder a una página protegida.
		options.LogoutPath = "/Usuarios/Logout"; //ruta a la que se redirigirá a los usuarios para cerrar sesión.
		options.AccessDeniedPath = "/Home/Restringido"; // ruta a la que se redirigirá a los usuarios que no tienen permiso para acceder a un recurso protegido
		//options.ExpireTimeSpan = TimeSpan.FromMinutes(5);//Tiempo de expiración
	});


builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador", "Empleado"));
});









var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");





    app.MapControllerRoute("rutaFija", "nn/{tipo}", new { controller = "Inmueble", action = "Uso", tipo = "defecto" });
//app.MapControllerRoute("rutaFija", "ruteo/{valor}", new { controller = "Home", action = "Ruta", valor = "defecto" });
app.Run();