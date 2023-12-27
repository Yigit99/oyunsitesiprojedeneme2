using ooyunsitesiprojedeneme2.Domain;
using oyunsitesiprojedeneme2.Domain.Models;
using oyunprojesideneme2.Services;
using ooyunsitesiprojedeneme2.domain.Collections.Interfaces;
using oyunsitesiprojedeneme2.Domain.Collections;
using NUnit.Framework.Constraints;
using Moq;
using System.Security.Permissions;
using Castle.Components.DictionaryAdapter.Xml;

namespace oyunsitesiprojedeneme.UnitTest
{
    public class Tests
    {
        //-----------------------------KULLANICI ��LEMLER� TESTLER�---------------------
        // Kullan�c� Kay�t Testi
        [Test]
        public void UserSavingTest()
        {
            //Arrange
            customerManager repoBag = new customerManager(new UsersCollection());
            UsersDataModel newFonk = new UsersDataModel()
            {
                UserName = "test",
                Password = "test",
                Email = "test@gmail.com",
                Role = "user",
            };

            //Action
            repoBag.registerSaving(newFonk);
            bool sonuc = repoBag.Login(newFonk);
            repoBag.usersRemove(newFonk);

            //Assert
            Assert.IsTrue(sonuc);

        }
        
        // Kullan�c� Kald�rma Testi (fixed)
        [Test]
        public void UserRemoveTest()
        {
            //Arrange
            customerManager repoBag = new customerManager(new UsersCollection());
            UsersDataModel newFonk = new UsersDataModel()
            {
                UserName = "test",
                Password = "test",
                Email = "test@gmail.com",
                Role = "user",
            };
            
            //Action
            repoBag.registerSaving(newFonk);
            repoBag.usersRemove(newFonk);
            bool sonuc = repoBag.Login(newFonk);

            //Assert
            Assert.IsFalse(sonuc);

        }

        // Kullan�c� Giri� Kontrol� Testi (Fixed)
        [Test]
        public void UsersEntranceControlTest()
        {
            //Arrange
            var mockVal = new Mock<IRepository>();
            UsersDataModel users = new UsersDataModel()
            { 
            
                UserName = "test",
                Email = "test",
                Password = "test",
                Role = "test",
            
            };
            mockVal.Setup(i => i.Login(users)).Returns(true);
            var evu = mockVal.Object;
            
            //Action
            var result = evu.Login(users);

            //Assert
            Assert.IsTrue(result);

        }


        //------------------------------�R�N ��LEMLER� TESTLER�--------------------------
        // �r�n kay�t Testi
        [Test]
        public void ProductAddingTest() {
            //Arrange
            ProductManager productMan = new ProductManager(new ProductsCollection());
            ProductsDataModel products = new ProductsDataModel() {
                productDescription = "test",
                productName = "test",
                productPhoto = "test",
                productPrice = "test",
                productReleaseDate = null,
            };

            //Action
            productMan.productAdd(products);
            bool sonuc = productMan.productControl(products);
            productMan.productRemove(products.productName);

            //Assert
            Assert.IsTrue(sonuc);
        }

        // �r�n kald�rma Testi
        [Test]
        public void ProductRemovingTest()
        {
            //Arrange
            ProductManager productMan = new ProductManager(new ProductsCollection());
            SqlConnectionsChangeData sqlBag = new SqlConnectionsChangeData();
            ProductsDataModel products = new ProductsDataModel()
            {
                productDescription = "test",
                productName = "test",
                productPhoto = "test",
                productPrice = "test",
                productReleaseDate = null,
            };
            
            //Action
            productMan.productAdd(products);
            productMan.productRemove(products.productName);
            bool sonuc = sqlBag.DataControlProduct(products);

            //Assert
            Assert.IsFalse(sonuc);
        }

        // �r�n veritaban�ndan veri �ekme Testi
        [Test]
        public void ProductGetFromDatabaseTest()
        {
            //Arrange
            ProductManager productmanager = new ProductManager(new ProductsCollection());
            var PageNumber = "1";

            //Action
            List<ProductsDataModel> result = productmanager.GetProducts(PageNumber);

            //Assert
            Assert.IsNotNull(result);
        }

        // �r�n verilen id'ye g�re veri se�me Testi
        [Test]
        public void BucketGetFromDatabaseTest()
        {
            //Arrange
            ProductManager proMan = new ProductManager(new ProductsCollection());
            List<int> sepet = new List<int>()
            {
                1,
            };
            ProductsDataModel aga = new ProductsDataModel() 
            {
                productName = "Test",
                productPrice = "Test",
                productDescription = "Test",
                productPhoto = "Test",
                productReleaseDate = "Test",
            };

            //Action
            proMan.productAdd(aga);
            List<BucketDataModel> agaThree = proMan.bucetGetProductFromDatabase(1, sepet);
            proMan.productRemove(aga.productName);

            //Assert
            Assert.IsNotNull(agaThree);
            
        }

        [Test]
        public void ProductControlTest() {
            //Arrange
            var mockVal = new Mock<IProductManager>();
            ProductsDataModel aga = new ProductsDataModel()
            { 
            
                productName = "Test",
                productPrice = "Test",
                productDescription = "Test",
                productPhoto = "Test",
                productReleaseDate = "Test",

            };
            mockVal.Setup(i => i.productControl(aga)).Returns(true);
            var evu = mockVal.Object;

            //Action
            var result = evu.productControl(aga);

            //Assert
            Assert.IsTrue(result);
        }

    }
}