using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ucsm.Pages
{
	public class AlbumModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int AlbumId { get; set; }

        public string? Title { get; set; }


        public void OnGet()
        {
            Title = $"Album title {AlbumId}";
        }
    }
}
