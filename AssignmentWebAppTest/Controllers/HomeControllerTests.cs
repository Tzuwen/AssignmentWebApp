using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace AssignmentWebApp.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Total products count expected
            int count = 294;

            // Act
            ViewResult result = controller.Index(null, null, null) as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
            Assert.AreEqual(count, result.ViewBag.totalProductsCount);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            // Arrange
            HomeController controller = new HomeController();
            int productID = 680;

            // Act
            ViewResult result = controller.Details(productID) as ViewResult;

            // Assert
            Assert.AreEqual(productID, result.ViewBag.productID);
        }

        [TestMethod()]
        public void EditTest_Page()
        {
            // Arrange
            HomeController controller = new HomeController();
            int productID = 680;

            // Act
            ViewResult result = controller.Edit(productID) as ViewResult;

            // Assert
            Assert.AreEqual(productID, result.ViewBag.productID);
        }

        [TestMethod()]
        public void EditTest_UpdateSuccess()
        {
            // Arrange
            HomeController controller = new HomeController();
            int productID = 680;
            string name = "aaa"; // orignal name "HL Road Frame - Black, 58"
            // Expected message if success
            string msg = "Update success.";

            // Act
            ViewResult result = controller.Edit(productID, name) as ViewResult;

            // Assert
            Assert.AreEqual(msg, result.ViewBag.msg);
        }

        [TestMethod()]
        public void EditTest_UpdateFail()
        {
            // Arrange
            HomeController controller = new HomeController();
            int productID = 680;
            string name = "HL Road Frame - Red, 58"; // produtid 706 is already named "HL Road Frame - Red, 58"
            // Expected message if fail
            string msg = "There is a product with the same title, update fail.";

            // Act
            ViewResult result = controller.Edit(productID, name) as ViewResult;

            // Assert
            Assert.AreEqual(msg, result.ViewBag.msg);
        }
    }
}