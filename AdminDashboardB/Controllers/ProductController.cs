using ApplicationB.Contracts_B.General;
using ApplicationB.Services_B;
using ApplicationB.Services_B.General;
using ApplicationB.Services_B.Product;
using DTOsB.Product;
using DTOsB.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DTOsB.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IImageService _imageService;
        private readonly ILanguageService languageService;
        private readonly IUserService _userService;
        private readonly IProductService productService;
        private readonly IProductImageService _productImageService;
        private readonly IProductSpecificationService _productSpecificationService;
        private readonly IProductTranslationService _productTranslationService;
        private readonly IProductSpecificationTransService _productSpecificationTransService;


        public ProductController(IProductService _productService, IUserService userService,ILanguageService _languageService, IProductImageService productImageService,IProductSpecificationService productSpecificationService,
            IProductTranslationService productTranslationService,IProductSpecificationTransService productSpecificationTransService)
        {
            productService = _productService;
           
            _userService = userService;
            languageService = _languageService;
            _productImageService = productImageService;
            _productSpecificationService = productSpecificationService;
            _productTranslationService = productTranslationService;
            _productSpecificationTransService = productSpecificationTransService;
        }


        public async Task<IActionResult> Index(int selectedLanguageId=2)
        {
           
            var availableLanguages = await languageService.GetAllLanguagesAsync();
            ViewBag.AvailableLanguages = new SelectList(availableLanguages, "Id", "Code");

       
           await languageService.SetUserSelectedLanguageAsync(selectedLanguageId);
            ViewBag.SelectedLanguageId = selectedLanguageId;


            var products = await productService.GetAllProductsAsync();
            if (products == null)
            {
                // Handle null case for products
                return View("Error", "Products not available.");
            }
            return View(products);
        }


        //public async Task<IActionResult> Index(int pageNumber = 1, int count = 10, int selectedLanguageId = 2)
        //{
        //    var products = await productService.GetAllPaginatedByLanguageAsync(pageNumber, count, selectedLanguageId);
        //    var availableLanguages = await languageService.GetAllLanguagesAsync();

        //    var viewModel = new ProductViewModelWithLang
        //    {
        //        Products = products.Data,
        //        AvailableLanguages = availableLanguages,
        //        SelectedLanguageId = selectedLanguageId
        //    };

        //    return View(viewModel);
        //}

        //public async Task<IActionResult> Index()
        //{
        //    var products = await productService.GetAllProductsAsync();
        //    return View(products); // Assuming Data contains IEnumerable<ProductViewModel>
        //}

        // GET: Products/Create
        public  IActionResult Create(/*int selectedLanguageId = 2*/)
        {
            //var availableLanguages = await languageService.GetAllLanguagesAsync();
            //ViewBag.AvailableLanguages = new SelectList(availableLanguages, "Id", "Code");


            //languageService.SetUserSelectedLanguageAsync(selectedLanguageId);
            //ViewBag.SelectedLanguageId = selectedLanguageId;
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateOrUpdateDto productDto)
        {
            //if (ModelState.IsValid)
            //{

            productDto.CreatedBy = _userService.GetCurrentUserId();
            productDto.UpdatedBy = _userService.GetCurrentUserId();

            var result = await productService.CreateProductAsync(productDto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Msg);
                return View(productDto);
            }


            //Images
            if (productDto.ImageFiles != null && productDto.ImageFiles.Count > 0)
            {
                productDto.Images = productDto.Images ?? new List<ProductImageCreateOrUpdateDto>();
                var errorMessages = new List<string>();

                foreach (var imageFile in productDto.ImageFiles)
                {
                    var imageUrl = await _imageService.SaveImageAsync(imageFile, "ImageUrls");
                    ProductImageCreateOrUpdateDto imageDto = new ProductImageCreateOrUpdateDto
                    {
                        Url = imageUrl,
                    };

                    productDto.Images.Add(imageDto);

                    var imageResult = await _productImageService.AddImageAsync(imageDto);
                    if (!imageResult.IsSuccess)
                    {
                        errorMessages.Add(imageResult.Msg);
                    }
                }

                if (errorMessages.Any())
                {
                    ModelState.AddModelError("", string.Join(";", errorMessages));
                    return View(productDto);
                }
            }

            //Translations
            foreach (var trans in productDto.Translations)
            {
                var transResult = await _productTranslationService.AddTranslationAsync(trans);
                if (!transResult.IsSuccess)
                {
                    ModelState.AddModelError("", transResult.Msg);
                    return View(productDto);
                }
            }


            //Specifications
            foreach (var spec in productDto.Specifications)
            {
                var specResult = await _productSpecificationService.AddSpecificationAsync(spec);
                if (!specResult.IsSuccess)
                {
                    ModelState.AddModelError("", specResult.Msg);
                    return View(productDto);
                }

                foreach (var specTrans in spec.Translations)
                {
                    var specTransResult = await _productSpecificationTransService.AddTranslationAsync(specTrans);
                    if (!specTransResult.IsSuccess)
                    {
                        ModelState.AddModelError("", specTransResult.Msg);
                        return View(productDto);
                    }
                }
            }

            //}
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await productService.UpdateProductAsync(productDto);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Msg);
            }
            return View(productDto);
        }

        // GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var product = await productService.GetProductByIdAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        // POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await productService.DeleteProductAsync(id);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest(result.Msg);
        }









        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productB = await productService.GetProductByIdAsync(id);

            return View(productB);
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var products = await productService.SearchProductsByNameAsync(searchString);
            ViewBag.SearchString = searchString;

            return View("Index", products);
        }
    }
}
