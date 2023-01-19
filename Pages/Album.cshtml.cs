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
        private readonly ILogger<AlbumModel> logger;
        private readonly IDataStore dataStore;

        [BindProperty(SupportsGet = true)]
        public int AlbumId { get; set; }

        public string? Title { get; set; }

        public AlbumModel (ILogger<AlbumModel> logger, IDataStore dataStore)
        {
            this.logger = logger;
            this.dataStore = dataStore;
        }

        public void OnGet()
        {
            var album = dataStore.GetAlbum(AlbumId).Result;

            Title = album.Title;

        }
    }
}
