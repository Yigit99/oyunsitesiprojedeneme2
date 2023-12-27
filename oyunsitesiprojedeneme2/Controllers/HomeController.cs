using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ooyunsitesiprojedeneme2.Domain;
using oyunsitesiprojedeneme2.Domain.Models;
using X.PagedList;

namespace oyunsitesiprojedeneme2.Controllers
{
    
    public class HomeController : Controller
    {

        //--------------------------------INDEX SAYFASI YÖNLENDİRME------------------------------------

        public IActionResult Index()
        {

            if (Request.Cookies["Rol"]=="admin")
            return RedirectToAction("adminIndexPage");

            else
            return RedirectToAction("UserIndexPage");

        }
        
        //--------------------------------KAYIT SAYFASI YÖNLENDİRME-------------------------------------
        public IActionResult register()
        {
            return View("../Authentication/register");
        }
       


        //-------------------------GİRİŞ YAPMA SAYFASI VE COOKİE CONTROL OTOMATİK GİRİŞ------------------
        public IActionResult Login()
        {

            return View("../Authentication/Login");
        }

        //-----------------------------------------ADMİN SAYFASI-----------------------------------------

        [Authorize (Roles = "admin")]
        public IActionResult adminIndexPage()
        {
            return View("../Admin/adminIndexPage");
        }




        public IActionResult AdminPage(string submitButton1 = "adminPagePro")
        {

            var sqlbaglanti = new SqlConnectionsChangeData();
            List<ProductsDataModel> products = sqlbaglanti.GetProductsFromDatabase("1");
            if (submitButton1 != "before" && submitButton1 != "after" && submitButton1 != null)
            {
                products = sqlbaglanti.GetProductsFromDatabase(submitButton1);
            }
            else
            {

            }


            return View("../Admin/AdminPage" , products);

        }

        //-------------------------------------------ÜRÜN SAYFASI-------------------------------------------

        public IActionResult Products(string submitButton1)
        {
            var sqlbaglanti = new SqlConnectionsChangeData();
            List<ProductsDataModel> products = sqlbaglanti.GetProductsFromDatabase("1");
            if (submitButton1 != "before" && submitButton1 != "after" && submitButton1 != null)
            {
                products = sqlbaglanti.GetProductsFromDatabase(submitButton1);
            }
            else
            {

            }

            return View(products);

        }

       
        //--------------------------------------------SEPET SAYFASI----------------------------------------
        public IActionResult bucketPage(int pageNumber = 1)
        {
            var sqlbaglanti = new SqlConnectionsChangeData();

            List<int> cartItems;
            string cartItemsString = Request.Cookies["cartItems"];

            if (!string.IsNullOrEmpty(cartItemsString))
            {
                cartItems = cartItemsString.Split(',').Select(int.Parse).ToList();
            }
            else
            {
                cartItems = new List<int>();
            }



            List<BucketDataModel> buckets = sqlbaglanti.bucketGetFromDataBase(pageNumber, cartItems);
            return View(buckets);

        }

        //--------------------------------------------KULLANICI GİRİŞİ---------------------------------------

        [Authorize (Roles = "user")]
        public IActionResult UserIndexPage()
        {

            return View();
        }

    }
}