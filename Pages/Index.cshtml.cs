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
        // TODO: load from interneet
        Albums.Add(new Album(1, "Title 1"));
        Albums.Add(new Album(2, "Title 2"));
        Albums.Add(new Album(3, "Title 3"));
        Albums.Add(new Album(4, "Title 4"));
        Albums.Add(new Album(5, "Title 5"));
        Albums.Add(new Album(6, "Title 6"));
        Albums.Add(new Album(7, "Title 7"));
        Albums.Add(new Album(8, "Title 8"));
        Albums.Add(new Album(9, "Title 9"));
        Albums.Add(new Album(10, "Title 10"));
    }

    public void OnGet()
    {
        this.Albums.Add(new Album(999, "The number of the cat"));
    }
}

public class Album
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ThumbnailUrl { get; set; }

    public Album(int id, string title)
    {
        Id = id;
        Title = title;
        // TODO: get url from photo store.
        ThumbnailUrl = "https://via.placeholder.com/160";
    }   
}
