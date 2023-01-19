using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace ucsm.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;
    private readonly IDataStore dataStore;
    
    [BindProperty]
    public string? SearchTerm { get; set; }
    public IEnumerable<Album> Albums { get; set; } = new List<Album>();

    public IndexModel(ILogger<IndexModel> logger, IDataStore dataStore)
    {
        this.logger = logger;
        this.dataStore = dataStore;
    }
    public async Task OnPostAsync()
    {
        System.Console.WriteLine("here post");
        System.Console.WriteLine($"The SearchTerm: {SearchTerm}");
        var albums = await dataStore.GetAlbums();
        var searchResult = albums.Where(m => m.Title.Contains(SearchTerm ?? ""));
        this.Albums = searchResult;
    }

    public async Task OnGetAsync()
    {
        System.Console.WriteLine("here");
        System.Console.WriteLine($"The SearchTerm: {SearchTerm}");
        this.Albums = await dataStore.GetAlbums();
    }
}

