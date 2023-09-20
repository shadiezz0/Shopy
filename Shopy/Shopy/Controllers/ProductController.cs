using Microsoft.AspNetCore.Mvc;
using Shopy.Core;
using Shopy.Repository.Interfaces;

namespace Shopy.Controllers
{
    public class ProductController : Controller
    {
        private readonly I_CRUD_Repos<Product> _productRepo;

        public ProductController(I_CRUD_Repos<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        //Product/Index
        public async Task<IActionResult> Index()
        {
            return View(await _productRepo.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int id=0)
        {
            if (id == 0)
            {
                return View(new Product());
            }
            else
            {
                try
                {
                    Product product = await _productRepo.GetById(id);
                    if (product != null)
                    {
                        return View(product);
                    }
                }
                catch (Exception ex) 
                {
                    TempData["ErrorMassage"] = ex.Message;
                    return RedirectToAction("Index");
                }
                TempData["ErrorMassage"] = $"Product is not found with Id : {id}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ProductId == 0) //create action
                    {
                        await _productRepo.AddNewItem(model); 
                        TempData["SuccessMassage"] = "Product Created Successfully";
                    }
                    else // edit action
                    {
                        await _productRepo.UpdateItem(model);
                        TempData["SuccessMassage"] = "Product updated Successfully";
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMassage"] = "Model state is invalid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMassage"] = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Product product = await _productRepo.GetById(id);
                if (product != null)
                {
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMassage"] = ex.Message;
                return RedirectToAction("Index");
            }
            TempData["ErrorMassage"] = $"Product is not found with Id : {id}";
            return RedirectToAction("Index");
        }

        [HttpPost , ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productRepo.DeleteItem(id);
                TempData["SuccessMassage"] = "Product Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMassage"] = ex.Message;
                return View();
            }
        }
    }
}
