using LibraryStoreApp.Models;
using LibraryStoreApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryStoreApp.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]

    public class LeasingController : Controller
    {

        private readonly ILeasingRepository _leasingRepository;

        private readonly IBookRepository _bookRepository;

        public readonly IWebHostEnvironment _webHostEnvironment;

        public LeasingController(ILeasingRepository leasingRepository, IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _leasingRepository = leasingRepository;
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;   
        }

        public IActionResult Index()
        {
            List<Leasing> leasings = _leasingRepository.GetAll(includeProps:"Book").ToList();
            return View(leasings);
        }

        public IActionResult AddUpdate(int? id) 
        {
            IEnumerable<SelectListItem> BookList = _bookRepository.GetAll()
                .Select(b => new SelectListItem
                {
                    Text=b.BookName,
                    Value=b.Id.ToString()
                });
            ViewBag.BookList=BookList;

            if(id == 0 || id == null) 
            {
                return View();
            }
            else 
            {
                Leasing? leasingDb = _leasingRepository.Get(u => u.Id == id);
                if(leasingDb == null) 
                {
                    return NotFound();
                }
                else 
                {
                    return View(leasingDb);
                }

            }
        }

        [HttpPost]
        public IActionResult AddUpdate(Leasing leasing)
        {
            if (ModelState.IsValid) 
            {
                if (leasing.Id == 0) 
                {
                    _leasingRepository.Add(leasing);
                    TempData["Success"] = "Success";

                }
                else 
                {
                    _leasingRepository.Update(leasing);
                    TempData["Success"] = "Success";
                }
                _leasingRepository.Save();
                return RedirectToAction("Index", "Leasing");
            }
            return View();
        }


        public IActionResult Delete(int? id)
        {
            IEnumerable<SelectListItem> BookList = _bookRepository.GetAll()
             .Select(b => new SelectListItem
             {
                 Text = b.BookName,
                 Value = b.Id.ToString()
             });
            ViewBag.BookList = BookList;

            if (id == 0) 
            {
                return NotFound();
            }
            
            Leasing? leasingDb=_leasingRepository.Get(u=>u.Id == id);
            
            if (leasingDb == null) 
            {
                return NotFound();
            }
            return View(leasingDb);
        }

        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePost(int? id) 
        {

            Leasing? leasingDb = _leasingRepository.Get(u => u.Id == id);

            if (leasingDb == null)
            {
                return NotFound();
            }
            _leasingRepository.Delete(leasingDb);
            _leasingRepository.Save();
            TempData["Success"] = "Success";
            return RedirectToAction("Index", "Leasing");
        }
    }
}
