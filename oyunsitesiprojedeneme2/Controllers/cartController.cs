using Microsoft.AspNetCore.Mvc;

namespace oyunsitesiprojedeneme2.Controllers
{
    public class cartController : Controller
    {

        public IActionResult bucketPageAct() {
            return View();
        }
       
        public IActionResult AddToCart(int productId)
        {
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

            cartItems.Add(productId);

            // Çerezleri güncelle
            Response.Cookies.Append("cartItems", string.Join(",", cartItems));

            return RedirectToAction("Products" , "Home");
        }

        public IActionResult bucketExit(string bucketProductId)
        {
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

            // Ürünü çıkar
            cartItems.Remove(int.Parse(bucketProductId));

            // Çerezleri güncelle
            Response.Cookies.Append("cartItems", string.Join(",", cartItems));

            return RedirectToAction("bucketPage" , "Home");

        }
    }
}
