using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Hw5.Pages;
using Hw5.BuisnessLogic;
using Hw5.NewProduct;
using Hw5.ProductMaker;



namespace Hw5
{
    public class Tests
    {
        private IWebDriver driver;



        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "http://localhost:5000";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);



            LoginPage login = new LoginPage(driver);
            login.login("user", "user");
            Assert.AreNotEqual(login.GetNamePage(), "Login");
        }

        [Test]
        public void AddProduct()
        {
            Menu menuPage = new Menu(driver);
            ProductPage product = menuPage.GoToProducts();
            Logic productPage = product.AddNewProduct();

            NewProducts newProduct = ProductMakers.Make();
            productPage.CreateProduct(newProduct);

            Assert.AreEqual(product.GetNamePage(), "All Products");


        }


        [Test]
        public void CheckProduct()
        {
            Menu menuPage = new Menu(driver);
            ProductPage product = menuPage.GoToProducts();
            EditProduct edit = product.ViewProduct();
            edit.Check();
        }

        [Test]
        public void RemoveProduct()
        {
            Menu menuPage = new Menu(driver);
            ProductPage product = menuPage.GoToProducts();
            product.Remove();
        }

        [TearDown]
        public void TearDown()
        {
            Menu menu = new Menu(driver);
            menu.Logout();
            driver.Quit();
        }
    }
}