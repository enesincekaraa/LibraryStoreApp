using LibraryStoreApp.Models;
using LibraryStoreApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryStoreApp.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]

    public class BookTypeController : Controller
    {
        private readonly IBookTypeRepository _bookTypeRepository;

        public BookTypeController(IBookTypeRepository bookTypeRepository)
        {
            _bookTypeRepository = bookTypeRepository;
        }

        public IActionResult Index()
        {
            List<BookType> bookTypes=_bookTypeRepository.GetAll().ToList();

            return View(bookTypes); 

        }

        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookType bookType)
        {
            if(ModelState.IsValid) 
            {
                _bookTypeRepository.Add(bookType);
                _bookTypeRepository.Save();
                TempData["Success"] = "book added successfully";
                return RedirectToAction("Index", "BookType");
            }
            else { return View(); }
          
        }

        public IActionResult Update(int? id) 
        {
            if(id==null || id == 0) 
            {
                return NotFound();
            }
            BookType? bookTypeDb=_bookTypeRepository.Get(u=>u.Id==id);
            if(bookTypeDb==null)
            {
                return NotFound();
            }
            return View(bookTypeDb);    
        }

        [HttpPost]
        public IActionResult Update(BookType bookType) 
        {
            if (ModelState.IsValid) 
            {
                _bookTypeRepository.Update(bookType);
                _bookTypeRepository.Save();
                TempData["Success"] = "book updated successfully";

                return RedirectToAction("Index", "BookType");
            }
            else { return View(); }
        }


        public IActionResult Delete(int id) 
        {
            BookType? deleteBook=_bookTypeRepository.Get(u=>u.Id== id);
            if (deleteBook==null||id==0)
            {
                return NotFound();
            }
            return View(deleteBook);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id) 
        {
            BookType? bookType=_bookTypeRepository.Get(u=>u.Id== id);
            if( bookType==null) { return NotFound(); }
            _bookTypeRepository.Delete(bookType);
            _bookTypeRepository.Save();
            TempData["success"] = "book deleted successfully";
            return RedirectToAction("Index", "BookType");
        }


    }
}
