namespace DTOsB.Controllers
{ public class LanguageController : Controller
    {
    private readonly ILanguageService languageService;

public LanguageController(ILanguageService _languageService){
 languageService = _languageService;
}

 public async Task<IActionResult> Index()
        {

         var availableLanguages = await languageService.GetAllLanguagesAsync();

         if (availableLanguages == null)
            {
                // Handle null case for products
                return View("Error", "No Languages available.");
            }
            return View(availableLanguages);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

         [HttpPost]
          public async Task<IActionResult> Create(LanguageDto langDto){

            var result = await languageService.CreateProductAsync(langDto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Msg);
                return View(langDto);
            }

             return RedirectToAction(nameof(Index));
}

public async Task<IActionResult> Edit(int id)
        {

            var language = await languageService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(language);
        }
        
 [HttpPost]
          public async Task<IActionResult> Edit(int id, ResultView<LanguageDto> resultView){

        var langDto = resultView.Entity;

            if (id != langDto.Id)
            {
                return BadRequest();
            }

             var result = await languageService.UpdateProductAsync(langDto);
}

  public async Task<IActionResult> Delete(int id)
        {
            var lang = await languageService.GetProductByIdAsync(id);
            if (lang == null)
            {
                return NotFound();
            }
            return View(lang);
        }

        // POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await languageService.DeleteProductAsync(id);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest(result.Msg);
        }
        

    
    }}
