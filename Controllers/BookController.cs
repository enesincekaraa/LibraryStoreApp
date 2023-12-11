using LibraryStoreApp.Models;
using LibraryStoreApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryStoreApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;   
        private readonly IBookTypeRepository _bookTypeRepository;   
        public readonly IWebHostEnvironment _webHostEnvironment;    

        public BookController(IBookRepository bookRepository, IBookTypeRepository bookTypeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _bookTypeRepository = bookTypeRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin,Customer")]
        public IActionResult Index()
        {
            List<Book> booksDb = _bookRepository.GetAll(includeProps:"BookType").ToList();

            IEnumerable<SelectListItem> bookTypeDb = _bookTypeRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });
            return View(booksDb);
        }


        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> bookTypeList = _bookTypeRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });
            ViewBag.bookTypeList= bookTypeList;

            if(id==null || id == 0) 
            {
                return View();
            }
            else 
            {
                Book? bookdb = _bookRepository.Get(u => u.Id==id);
                if(bookdb==null) { return NotFound(); }
                return View(bookdb);
            }
        }


        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public IActionResult AddUpdate(Book book,IFormFile? file) 
        { 
            var errors = ModelState.Values.SelectMany(x => x.Errors); 
            
            if(ModelState.IsValid) 
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string bookPath = Path.Combine(wwwRootPath, @"img");

                if(file!=null) 
                {
                    using(var fileStream = new FileStream(Path.Combine(bookPath, file.FileName), FileMode.Create)) 
                    {
                        file.CopyTo(fileStream);
                    }
                    book.ImageUrl=@"\img\" + file.FileName;
                }
                if (book.Id == 0)
                {
                    _bookRepository.Add(book);
                    TempData["Success"] = "new book created successfully";
                }
                else 
                {
                    _bookRepository.Update(book);
                    TempData["Success"] = "book updated successfully";
                }
                _bookRepository.Save();
                return RedirectToAction("Index", "Book");
            }
            return View();
            

        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Delete(int? id)
        {

            Book? deletedBook = _bookRepository.Get(u => u.Id == id);

            if (deletedBook == null || id == 0)
            {
                return NotFound();
            }
            return View(deletedBook);
        }

        [Authorize(Roles = UserRoles.Role_Admin)]

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Book? kitap = _bookRepository.Get(u => u.Id == id);
            if (kitap == null) { return NotFound(); }
            _bookRepository.Delete(kitap);
            _bookRepository.Save();
            TempData["success"] = "Book Deleted Successfully";

            return RedirectToAction("Index", "Book");


        }
    }
}
