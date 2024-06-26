﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using PuppeteerSharp;
using System.Threading.Tasks;

namespace Cinemavazut_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;

        [TestInitialize]
        public void Initialize()
        {
        
            driver = new ChromeDriver();           
            driver.Navigate().GoToUrl("https://localhost:7231");
            driver.Manage().Window.Maximize();                    
      
        }




        [TestMethod]
        public void Test01_Run()
        {
            System.Threading.Thread.Sleep(2000);
            string titlu = driver.Title;
            
            if(titlu == "Cinemavazut")
            {
                Console.WriteLine("Tiltu este corect");
            }
            else { Console.WriteLine("NU e corect"); }
        }


        [TestMethod]
        public void Test02_Login()
        {
            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori/SignIn");
            WebElement username = (WebElement)driver.FindElement(By.Id("email"));
            WebElement password = (WebElement)driver.FindElement(By.Id("parola"));
            WebElement login = (WebElement)driver.FindElement(By.Id("SignIn"));
            username.SendKeys("iancumihaela@gmail.com");
            password.SendKeys("123456");
            login.Click();
            string actualUrl = "https://localhost:7231/";
            string expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
          
        }


        [TestMethod]
        public void Test03_Admin()
        {
            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori/SignIn");

            WebElement username = (WebElement)driver.FindElement(By.Id("email"));
            WebElement password = (WebElement)driver.FindElement(By.Id("parola"));
            WebElement login = (WebElement)driver.FindElement(By.Id("SignIn"));
            username.SendKeys("cioflancezar@gmail.com");
            password.SendKeys("567890");
            /*username.SendKeys("iancumihaela@gmail.com");
            password.SendKeys("123456");*/
            login.Click();

            string actualUrl = "https://localhost:7231/";
            string expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);

            try
            {
                driver.FindElement(By.Id("btn-admin"));
            }
            catch (Exception NoSuchElementException)
            {
                Console.WriteLine("Admin login didn't work. Administration not found!");
                Console.WriteLine(NoSuchElementException);
                Assert.Fail();
            }

            Console.WriteLine("Admin login worked.");
        }


        [TestMethod]
        public void Test04_SearchBar()
        {
            driver.Navigate().GoToUrl("https://localhost:7231");
            WebElement SearchString = (WebElement)driver.FindElement(By.Name("SearchString"));
            WebElement SearchBtn = (WebElement)driver.FindElement(By.Name("ButtonSearch"));
            SearchString.SendKeys("F");
            SearchBtn.Click();
            System.Threading.Thread.Sleep(2000);

            string actualUrl = "https://localhost:7231/Filme/Search";
            string expectedUrl = driver.Url;

            Assert.AreEqual(expectedUrl, actualUrl);

            string result = driver.FindElement(By.XPath("/ html / body / div[2] / main / div / div / h5")).Text;//ceva getText() de facut acasa
            Console.WriteLine(result);
            Assert.AreEqual("Filantropica", result);



        }


        [TestMethod]
        public void Test05_Register()
        {
            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori/Create");

            //Nume
            WebElement nume = (WebElement)driver.FindElement(By.Name("nume"));
            nume.SendKeys("Ion");

            //Prenume
            WebElement prenume = (WebElement)driver.FindElement(By.Name("prenume"));
            prenume.SendKeys("Popescu");

            //parola
            WebElement parola = (WebElement)driver.FindElement(By.Id("parola"));
            parola.SendKeys("Parola10@");

            //email
            WebElement email = (WebElement)driver.FindElement(By.Name("email"));
            email.SendKeys("ionpopescu@gmail.com");

            //numamr telefon
            WebElement telefon = (WebElement)driver.FindElement(By.Id("telefon"));
            string phone = "0733555777";
            telefon.SendKeys(phone);

            WebElement data_nastere = (WebElement)driver.FindElement(By.Name("data_nastere"));
            data_nastere.SendKeys("10092002");
            data_nastere.SendKeys(Keys.Tab);
            data_nastere.SendKeys("0230PM");


            //Creare cont
            WebElement create = (WebElement)driver.FindElement(By.Name("create_util"));
            new Actions(driver).Click(create).Perform();
            System.Threading.Thread.Sleep(2000);

            string actualUrl = "https://localhost:7231/";
            string expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }
        

        [TestMethod]
        public void Test06_EditUser()
        {
            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori/SignIn");

            WebElement username = (WebElement)driver.FindElement(By.Id("email"));
            WebElement password = (WebElement)driver.FindElement(By.Id("parola"));
            WebElement login = (WebElement)driver.FindElement(By.Id("SignIn"));
            username.SendKeys("cioflancezar@gmail.com");
            password.SendKeys("567890");
            login.Click();

            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori");

            IList<IWebElement> allElement = driver.FindElements(By.TagName("td"));
            int sw = 0;
            foreach (IWebElement element in allElement)
            {
                string cellText = element.Text;
                if (cellText == "ionpopescu@gmail.com")
                {
                    sw = 1;
                }

                if (sw == 1 && cellText == "Edit | Details | Delete")
                {
                    IWebElement btn = element.FindElement(By.Name("edit"));
                    btn.Click();
                    break;
                }
            }
            System.Threading.Thread.Sleep(2000);

            WebElement nume = (WebElement)driver.FindElement(By.Id("nume"));
            WebElement prenume = (WebElement)driver.FindElement(By.Id("prenume"));
            WebElement email = (WebElement)driver.FindElement(By.Id("email"));
            WebElement parola = (WebElement)driver.FindElement(By.Id("parola"));
            WebElement telefon = (WebElement)driver.FindElement(By.Id("telefon"));
            WebElement data_nastere = (WebElement)driver.FindElement(By.Id("data_nastere"));
            WebElement rol = (WebElement)driver.FindElement(By.Id("rol"));
            //discutabil aici ca nush cum sa trasmit ora inscrierii
            /*WebElement data_inscriere = (WebElement)driver.FindElement(By.Id("data_inscriere"));*/
            string nume_expected = "Ion";
            string prenume_expected = "Popescu";
            string email_expected = "ionpopescu@gmail.com";
            string parola_expected = "Parola10@";
            string telefon_expected = "0733555777";
            string data_nastere_expected = "2002-10-09T00:00";
            string rol_expected = "1";


            Assert.AreEqual(nume.GetAttribute("value"), nume_expected);
            Assert.AreEqual(prenume.GetAttribute("value"), prenume_expected);
            Assert.AreEqual(email.GetAttribute("value"), email_expected);
            Assert.AreEqual(parola.GetAttribute("value"), parola_expected);
            Assert.AreEqual(telefon.GetAttribute("value"), telefon_expected);
            Assert.AreEqual(data_nastere.GetAttribute("value"), data_nastere_expected);
            Assert.AreEqual(rol.GetAttribute("value"), rol_expected);

            //De continuat acasa sa vezi daca ii dai SendKeys pe nume completeaza sau scrie peste

        }


        [TestMethod]
        public void Test07_DeleteUser()
        {
            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori/SignIn");

            WebElement username = (WebElement)driver.FindElement(By.Id("email"));
            WebElement password = (WebElement)driver.FindElement(By.Id("parola"));
            WebElement login = (WebElement)driver.FindElement(By.Id("SignIn"));
            username.SendKeys("cioflancezar@gmail.com");
            password.SendKeys("567890");
            login.Click();

            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori");

            IList<IWebElement> allElement = driver.FindElements(By.TagName("td"));
            int sw = 0;
            foreach (IWebElement element in allElement)
            {
                string cellText = element.Text;
                if (cellText == "ionpopescu@gmail.com")
                {
                    sw = 1;
                }

                if (sw == 1 && cellText == "Edit | Details | Delete")
                {
                    IWebElement btn = element.FindElement(By.Name("delete"));
                    btn.Click();
                    break;
                }
            }
            System.Threading.Thread.Sleep(2000);

            WebElement deletebtn = (WebElement)driver.FindElement(By.Name("delete_btn"));
            deletebtn.Click();

            string actualUrl = "https://localhost:7231/Utilizatori";
            string expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }


        [TestMethod]
        public void Test08_WatchList()
        {
            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori/SignIn");
            WebElement username = (WebElement)driver.FindElement(By.Id("email"));
            WebElement password = (WebElement)driver.FindElement(By.Id("parola"));
            WebElement login = (WebElement)driver.FindElement(By.Id("SignIn"));
            username.SendKeys("iancumihaela@gmail.com");
            password.SendKeys("123456");
            login.Click();

            driver.Navigate().GoToUrl("https://localhost:7231/Filme/PaginaFilm/4");
            WebElement watchlist = (WebElement)driver.FindElement(By.Id("watchlist-1"));
            watchlist.Click();

            string actualUrl = "https://localhost:7231/Filme/PaginaFilm/4";
            string expectedUrl = driver.Url;
            WebElement watchlist2 = (WebElement)driver.FindElement(By.Id("watchlist-2"));
            watchlist2.Click();

            Assert.AreEqual(expectedUrl, actualUrl);
        }


        [TestMethod]
        public void Test09_Recenzi()
        {
            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori/SignIn");
            WebElement username = (WebElement)driver.FindElement(By.Id("email"));
            WebElement password = (WebElement)driver.FindElement(By.Id("parola"));
            WebElement login = (WebElement)driver.FindElement(By.Id("SignIn"));

            int recenzii_vechi = driver.FindElements(By.Id("grid_comment")).Count;
            username.SendKeys("iancumihaela@gmail.com");
            password.SendKeys("123456");
            login.Click();
            driver.Navigate().GoToUrl("https://localhost:7231/Filme/PaginaFilm/4");
            WebElement recenzie = (WebElement)driver.FindElement(By.Id("btn-recenzie"));
            recenzie.Click();
            WebElement titlu = (WebElement)driver.FindElement(By.Id("titlu"));
            WebElement comentariu = (WebElement)driver.FindElement(By.Id("comentariu"));
            WebElement rating = (WebElement)driver.FindElement(By.Id("rating"));
            WebElement create = (WebElement)driver.FindElement(By.Name("create_recenzie"));
            titlu.SendKeys("Recenzie Test");
            comentariu.SendKeys("Comentariu de test");
            rating.SendKeys("5");
            create.Click();
            int recenzii_noi = driver.FindElements(By.Id("grid_comment")).Count;
            Assert.AreNotEqual(recenzii_noi, recenzii_vechi);

        }


        [TestMethod]
        public void Test10_Achievement()
        {
            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori/SignIn");
            WebElement username = (WebElement)driver.FindElement(By.Id("email"));
            WebElement password = (WebElement)driver.FindElement(By.Id("parola"));
            WebElement login = (WebElement)driver.FindElement(By.Id("SignIn"));

            int recenzii_vechi = driver.FindElements(By.Id("grid_comment")).Count;
            username.SendKeys("iancumihaela@gmail.com");
            password.SendKeys("123456");
            login.Click();

            driver.Navigate().GoToUrl("https://localhost:7231/Achievements/Badges");
            string[] img = driver.FindElement(By.XPath("/html/body/div[2]/main/div/div[1]/div[2]/div[1]/img")).GetAttribute("src").Split('/');
           
            if (img.Last() == "badge.png")
            {
                Console.WriteLine("Achievement exists!");
            }
            else if (img.Last() == "badge_gray.png")
            {
                Console.WriteLine("Achievement doesn't exist yet!");
                Assert.Fail();
            }
        }


        [TestMethod]
        public void Test11_Quiz_Score()
        {
            int scor = 0;
            int scor2 = 0;

            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori/SignIn");

            WebElement username = (WebElement)driver.FindElement(By.Id("email"));
            WebElement password = (WebElement)driver.FindElement(By.Id("parola"));
            WebElement login = (WebElement)driver.FindElement(By.Id("SignIn"));
            username.SendKeys("cioflancezar@gmail.com");
            password.SendKeys("567890");
            login.Click();

            WebElement profil = (WebElement)driver.FindElement(By.ClassName("pfp"));
            profil.Click();

            IList<IWebElement> allElement = driver.FindElements(By.TagName("td"));
            int sw = 0;
            foreach (IWebElement element in allElement)
            {
                string cellText = element.Text;
                if (cellText == "Cioflan")
                {
                    sw = 1;
                    continue;
                }

                if (sw == 1)
                {
                    scor = Convert.ToInt32(cellText);
                    Console.WriteLine(scor);
                    break;
                }
            }

            driver.Navigate().GoToUrl("https://localhost:7231/Quizuri/Test4");
            WebElement aw1 = (WebElement)driver.FindElement(By.Id("pomana"));
            WebElement aw2 = (WebElement)driver.FindElement(By.Id("Nae Caranfil"));
            WebElement aw3 = (WebElement)driver.FindElement(By.Id("Mircea Diaconu"));

            aw1.Click(); aw2.Click() ; aw3.Click();

            System.Threading.Thread.Sleep(1000);

            WebElement btn = (WebElement)driver.FindElement(By.Id("submit"));
            btn.Click();

            System.Threading.Thread.Sleep(1000);

            profil = (WebElement)driver.FindElement(By.ClassName("pfp"));
            profil.Click();

            allElement = driver.FindElements(By.TagName("td"));
            sw = 0;
            foreach (IWebElement element in allElement)
            {
                string cellText = element.Text;
                if (cellText == "Cioflan")
                {
                    sw = 1;
                    continue;
                }

                if (sw == 1)
                {
                    scor2 = Convert.ToInt32(cellText);
                    Console.WriteLine(scor2);
                    break;
                }
            }

            Assert.AreNotEqual(scor, scor2);
        }


        [TestMethod]
        public void Test12_Logout()
        {
            driver.Navigate().GoToUrl("https://localhost:7231/Utilizatori/SignIn");

            WebElement username = (WebElement)driver.FindElement(By.Id("email"));
            WebElement password = (WebElement)driver.FindElement(By.Id("parola"));
            WebElement login = (WebElement)driver.FindElement(By.Id("SignIn"));
            username.SendKeys("cioflancezar@gmail.com");
            password.SendKeys("567890");
            login.Click();

            WebElement profil = (WebElement)driver.FindElement(By.ClassName("pfp"));
            profil.Click();

            WebElement logout = (WebElement)driver.FindElement(By.ClassName("logout"));
            logout.Click();

            profil = (WebElement)driver.FindElement(By.ClassName("pfp"));
            profil.Click();

            /*Verif: Butonul de profil te duce la SignIn, nu la pagina de Profil, fiindca nu exista utilizator inca (doar guest)*/

            string actualUrl = "https://localhost:7231/Utilizatori/SignIn";
            string expectedUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }


                // Comparatie Selenium cu ChatGPT
        private string baseUrl = "https://localhost:7231";
        
        [TestMethod]
        public void TestGPT_Login()
        {
            // Navigate to the login page
            driver.Navigate().GoToUrl(baseUrl + "/Utilizatori/SignIn");
        
            // Fill in the login form
            driver.FindElement(By.Id("email")).SendKeys("iancumihaela@gmail.com");
            driver.FindElement(By.Id("parola")).SendKeys("123456");
        
            // Submit the login form
            driver.FindElement(By.Id("SignIn")).Click();
        
            // Wait for the main page to load (custom wait condition for URL change)
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.Url.StartsWith(baseUrl + "/"));
        
            // Assert that the URL matches the main page URL
            Assert.AreEqual(baseUrl + "/", driver.Url);
        }
        
        
        [TestMethod]
        public void TestGPT_Search() //(string movieName) nu ii place parametrul
        {
            string movieName = "Filantropica";
        
            // Navigate to the movie site
            driver.Navigate().GoToUrl(baseUrl);
        
            // Locate the search bar and input the movie name
            IWebElement searchInput = driver.FindElement(By.Name("SearchString"));
            searchInput.SendKeys(movieName);
            searchInput.SendKeys(Keys.Enter);
        
            // Wait for search results to load
            // You may need to wait for search results to load here
        
            // Get the title of the first movie in the search results
            IWebElement firstResultTitle = driver.FindElement(By.XPath("/ html / body / div[2] / main / div / div / h5"));
        
            // Verify if the first movie title corresponds to the provided movie name
            if (firstResultTitle.Text.Equals(movieName, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Search bar is working. First movie found corresponds to the provided movie.");
            }
            else
            {
                Console.WriteLine("Search bar is not working properly. First movie found does not correspond to the provided movie.");
                // lipseste Assert.Fail(); in ver. GPT
            }
        }
        
        
        [TestMethod]
        public void TestGPT_Register()
        {
            // Navigate to the registration page
            driver.Navigate().GoToUrl(baseUrl + "/Utilizatori/Create");
        
            // Fill in the registration form
            driver.FindElement(By.Name("nume")).SendKeys("John");
            driver.FindElement(By.Name("prenume")).SendKeys("Doe");
            driver.FindElement(By.Id("parola")).SendKeys("Example12//");
            driver.FindElement(By.Name("email")).SendKeys("john.doe@example.com");
            driver.FindElement(By.Id("telefon")).SendKeys("0034567890");
        
            driver.FindElement(By.Name("data_nastere")).SendKeys("01-01-1990");
        
            driver.FindElement(By.Name("data_nastere")).SendKeys(Keys.Tab); // Move to the next field
            driver.FindElement(By.Name("data_nastere")).SendKeys("1200PM");
        
            // Scroll down to the submit button
            IWebElement submitButton = driver.FindElement(By.Name("create_util"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);
        
            // Submit the form
            // submitButton.Click(); // nu merge versiunea de la GPT
            new Actions(driver).Click(submitButton).Perform();
        
            // Wait for the main page to load (custom wait condition for URL change)
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5000));
            wait.Until(driver => driver.Url.StartsWith(baseUrl + "/"));
        
            // Assert that the URL matches the main page URL
            Assert.AreEqual(baseUrl + "/", driver.Url);
        }


        //-------------------------------------------------------------------------------
        //3 Teste Puppeteer

        /*PUPPETEER*/

        

        [TestMethod]
        public async Task Test13_Puppeteer_Login()
        {
            //await new BrowserFetcher().DownloadAsync();

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions() { Headless = false });
            //new tab
            var page = await browser.NewPageAsync();

            //login page
            await page.GoToAsync("https://localhost:7231/Utilizatori/SignIn");

            await page.TypeAsync("input[id=email]", "iancumihaela@gmail.com");
            await page.TypeAsync("input[id=parola]", "123456");
            await page.ClickAsync("input[id=SignIn]");
            System.Threading.Thread.Sleep(2000); // prea rapid, nu apucase sa treaca la pagina de home

            string actualURL = "https://localhost:7231/";
            string expectedURL =  page.Url.ToString();

            Assert.AreEqual(expectedURL, actualURL);

            await browser.CloseAsync();
        }


        [TestMethod]
        public async Task Test14_Puppeteer_Admin()
        {
            await new BrowserFetcher().DownloadAsync();

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions() { Headless = false, Args = new[] { "--start-maximized" } });
            //new tab
            var page = await browser.NewPageAsync();
            await page.SetViewportAsync(new ViewPortOptions { Width = 1920, Height = 1080 });
            //login page
            await page.GoToAsync("https://localhost:7231/Utilizatori/SignIn");

            await page.TypeAsync("input[id=email]", "cioflancezar@gmail.com");
            await page.TypeAsync("input[id=parola]", "567890");
            await page.ClickAsync("input[id=SignIn]");
            System.Threading.Thread.Sleep(2000);

            await page.ClickAsync("a#btn-admin.button");
            string actualURL = "https://localhost:7231/Home/Admin";
            System.Threading.Thread.Sleep(2000);
            string expectedURL = page.Url.ToString();

            Assert.AreEqual(expectedURL, actualURL);

            await browser.CloseAsync();
        }
        

        [TestMethod]
        public async Task Test15_Puppeteer_Logout()
        {
            await new BrowserFetcher().DownloadAsync();

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions() { Headless = false, Args = new[] { "--start-maximized" } });
            //new tab
            var page = await browser.NewPageAsync();

            //login page
            await page.SetViewportAsync(new ViewPortOptions { Width = 1920, Height = 1080 });
            await page.GoToAsync("https://localhost:7231/Utilizatori/SignIn");

            await page.TypeAsync("input[id=email]", "iancumihaela@gmail.com");
            await page.TypeAsync("input[id=parola]", "123456");
            await page.ClickAsync("input[id=SignIn]");
            System.Threading.Thread.Sleep(2000);
            await page.ClickAsync("a.pfp");
            System.Threading.Thread.Sleep(2000);
            await page.ClickAsync("a.logout");
            System.Threading.Thread.Sleep(2000);
            await page.ClickAsync("a.pfp");
            string actualURL = "https://localhost:7231/Utilizatori/SignIn";

            string expectedURL = page.Url.ToString();

            Assert.AreEqual(expectedURL, actualURL);

            await browser.CloseAsync();
        }


        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }

      
    }
    
}
