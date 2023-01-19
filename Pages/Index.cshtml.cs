using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace ucsm.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> logger;
    private readonly IDataStore dataStore;

    public IEnumerable<Album> Albums { get; set; } = new List<Album>();

    public IndexModel(ILogger<IndexModel> logger, IDataStore dataStore)
    {
        this.logger = logger;
        this.dataStore = dataStore;
    }

    public async Task OnGetAsync()
    {
        this.Albums = await dataStore.GetAlbums();
    }
}

