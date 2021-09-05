using Microsoft.AspNetCore.Mvc;
using TopKala.DataAccess.Data;
using TopKala.DataAccess.Repository.IRepository;
using TopKala.Models;

namespace TopKala.Controllers
{
    [Route("{controller}")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ApplicationDbContext _db;

        public ProductController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        [Route("{id:int:required}")]
        public IActionResult Index(int id)
        {
            Product product = _unitOfWork.Product.GetFirstOrDefault(
                p => p.Id == id,
                "Brand, Category, Images, Colors, Comments, ProductInfos, ProductSellers"
            );

            // Product product = null;
            // IQueryable<Product> query = _db.Products.Where(p => p.Id == id)
            //                             .Include(p => p.Comments);

            // product = query.FirstOrDefault();

            if (product != null)
            {
                return View(product);
            }

            return NotFound();
        }
    }
}