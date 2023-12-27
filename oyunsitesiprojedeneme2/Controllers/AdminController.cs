using Microsoft.AspNetCore.Mvc;
using ooyunsitesiprojedeneme2.domain.Collections.Interfaces;
using ooyunsitesiprojedeneme2.Domain;
using oyunprojesideneme2.Services;
using oyunsitesiprojedeneme2.Domain.Collections;
using oyunsitesiprojedeneme2.Domain.Models;

namespace oyunsitesiprojedeneme2.Controllers
{
    public class AdminController : Controller
    {
        //--------------------------------------ÜRÜN SAYFASIA ÜRÜN EKLEME--------------------------------
        public IActionResult SaveProductAdd(string productName, string productPrice, string productDate, string productPhoto, string productDesc)
        {
            ProductsDataModel data = new ProductsDataModel()
            { 
             productName = productName,
             productPrice = productPrice,
             productReleaseDate = productDate,
             productPhoto = productPhoto,
             productDescription = productDesc
            };
            ProductManager repoBaglanti = new ProductManager(new ProductsCollection());
            repoBaglanti.productAdd(data);

            return RedirectToAction("Index", "Home");

        }
        //---------------------------------------ÜRÜN SAYFASINDAN KALDIRMA-----------------------------
        public IActionResult removeProduct(string productRemoveName)
        {
            ProductManager repoBaglanti = new ProductManager(new ProductsCollection());
            repoBaglanti.productRemove(productRemoveName);
            return RedirectToAction("AdminPage" , "Home");
        }
    }
}
