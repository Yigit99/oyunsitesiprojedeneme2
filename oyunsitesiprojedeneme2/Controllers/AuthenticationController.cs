using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ooyunsitesiprojedeneme2.Domain;
using oyunprojesideneme2.Services;
using oyunsitesiprojedeneme2.Domain.Collections;
using oyunsitesiprojedeneme2.Domain.Models;
using System.Data;
using System.Security.Claims;

namespace oyunsitesiprojedeneme2.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Saving(string KullaniciAdi, string Eposta, string sifre, string sifreCheck)
        {
            UsersDataModel data = new UsersDataModel()
            { 
            
                UserName = KullaniciAdi,
                Email = Eposta,
                Password = sifre,
            
            };
            if (sifre == sifreCheck)
            {
                customerManager repoBaglanti = new customerManager(new UsersCollection());
                repoBaglanti.registerSaving(data);

                return RedirectToAction("UserIndexPage", "Home");
            }
            else
            {
                return RedirectToAction("kayitPage" , "Home");
            }
        }

        
        public async Task<IActionResult> Login(string UsersName, string Password)
        {

            if (UsersName != null && Password != null)
            {
                UsersDataModel data = new UsersDataModel() 
                {
                 UserName = UsersName, Password = Password,
                };
                customerManager sqlbaglanti = new customerManager(new UsersCollection());
                bool isLoginSuccessful = sqlbaglanti.Login(data);
                if (isLoginSuccessful)
                {
                    SqlConnectionsChangeData wantRole = new SqlConnectionsChangeData();
                    string rolDegeri = wantRole.getRole(UsersName, Password);
                    Response.Cookies.Append("Rol", rolDegeri);
                    var claims = new List<Claim>
                {


                new Claim(ClaimTypes.Name, UsersName),
                new Claim(ClaimTypes.Authentication, Password),
                new Claim(ClaimTypes.Role,rolDegeri),

                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Index", "Home");

                }
            }

            else { return RedirectToAction("Login", "Home"); }
            return RedirectToAction("Login", "Home");

        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("kullaniciAdi");
            Response.Cookies.Delete("sifre");
            Response.Cookies.Delete("Rol");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index" , "Home");
        }

    }
}
