using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace ucsm.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IDataStore dataStore;

    public List<Album> Albums { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IDataStore dataStore)
    {
        _logger = logger;
        this.dataStore = dataStore;

        Albums = new();
    }

    public void OnGet()
    {

        // .Result a bit iffy?
        var albums = dataStore.GetAlbums().Result;
        foreach (var album in albums)
        {
            var albumPhoto = dataStore.GetAlbumPhoto(album.Id).Result;
            this.Albums.Add(new Album(album.Id, album.Title, albumPhoto.ThumbnailUrl));
        }
    }
}

public class Album
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ThumbnailUrl { get; set; }

    public Album(int id, string title, string thumbnail)
    {
        Id = id;
        Title = title;
        ThumbnailUrl = thumbnail;
    }   
}
