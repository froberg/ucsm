using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace ucsm.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;
    private readonly IDataStore dataStore;
    
    public string? SearchTerm { get; set; }
    public IEnumerable<Album> Albums { get; set; } = new List<Album>();

    public IndexModel(ILogger<IndexModel> logger, IDataStore dataStore)
    {
        this.logger = logger;
        this.dataStore = dataStore;
    }
    public IActionResult OnPostAsync(string searchTerm)
    {
        return Redirect($"?SearchTerm={searchTerm}");
    }

    public async Task OnGetAsync(string? searchTerm)
    {
        this.SearchTerm = searchTerm;
        var albums = await dataStore.GetAlbums();
        this.Albums = albums.Where(m => m.Title.Contains(searchTerm ?? ""));
    }
}

