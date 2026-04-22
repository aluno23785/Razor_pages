using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data;
using WebApplication1.Data.Model;

namespace WebApplication1.Pages.Degrees
{
   
    public class CreateModel : PageModel
    {
        /// <summary>
        /// db do projeto, injetada pelo controlador
        /// </summary>
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public CreateModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }
        /// <summary>
        /// mostra a pagina do create, quando o pedido é feito em http get
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Degree Degree { get; set; } = default!;
        [BindProperty]
        public  IFormFile ImagemLogo { get; set; }
        // For more information, see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// mostra a pagina do create, quando o pedido é feito em http post
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            /*A imagem como está a ser processada
             * a imagem existe?
             * se ´não, devolver controlo à página
             * se sim é uma imagem
             * 
             */
            if (ImagemLogo.ContentType  != "image/jpeg"
                && ImagemLogo.ContentType != "Image/png"){



                ModelState.AddModelError("ImagemLogo", "O ficheiro da imagem deve ser");
                    return Page();
            }

            //
            //
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagemLogo.FileName);
            Degree.Logotype = fileName;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Degree.Add(Degree);
            await _context.SaveChangesAsync();
            try
            {
                _context.Degree
            }

            return RedirectToPage("./Index");
        }
    }
}
