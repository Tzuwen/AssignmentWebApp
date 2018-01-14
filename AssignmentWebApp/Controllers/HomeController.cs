using AssignmentWebApp.Models;
using AssignmentWebApp.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace AssignmentWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string currentFilter, string searchKeyword, int? page)
        {
            if (searchKeyword != null)
            {
                page = 1;
            }
            else
            {
                searchKeyword = currentFilter;
            }

            ViewBag.CurrentFilter = searchKeyword;

            ProductRepository productRepository = new ProductRepository();
            List<Product> productList = productRepository.GetAllProducts();
                        
            ViewBag.totalProductsCount = productList.Count;// For unit test

            // Search filter, search for keyword in Title or Description
            if (!string.IsNullOrWhiteSpace(searchKeyword))
                productList = productList.Where(p => p.Name.ToUpper().Contains(searchKeyword.ToUpper()) || p.Description.ToUpper().Contains(searchKeyword.ToUpper())).ToList();
                        
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var result = productList.ToPagedList(pageNumber, pageSize);

            return View(result);
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            Product result = productRepository.GetProduct(id);
            ViewBag.productID = result.ProductID;// For unit test
            return View(result);
        }

        // GET: Home/Edit/5       
        public ActionResult Edit(int id)
        {
            ViewBag.msg = "";
            ProductRepository productRepository = new ProductRepository();
            Product result = productRepository.GetProduct(id);
            ViewBag.productID = result.ProductID;// For unit test
            return View(result);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string name)
        {            
            int result = 0 ;
            ProductRepository productRepository = new ProductRepository();
            result = productRepository.UpdateProduct(id, name);

            if (result == 1) // Update success
            {
                ViewBag.msg = "Update success.";                
            }
            else // Title duplicated, update faile
            {
                ViewBag.msg = "There is a product with the same title, update fail.";
            }
            return View(productRepository.GetProduct(id));
        }
    }
}
