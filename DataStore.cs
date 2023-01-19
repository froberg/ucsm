using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ucsm
{
	public struct AlbumDTO
	{
        [JsonInclude]
        public int Id;
        [JsonInclude]
        public string Title;
    }

    public struct AlbumPhotoDTO
    {
        [JsonInclude]
        public int Id;
        [JsonInclude]
        public int AlbumId;
        [JsonInclude]
        public string Title;
        [JsonInclude]
        public string ThumbnailUrl;
        [JsonInclude]
        public string Url;
    }

	public interface IDataStore
	{
        Task<Album> GetAlbum(int id);
        Task<IEnumerable<Album>> GetAlbums();
    }

    public class DataStore : IDataStore
	{
        private readonly IHttpClientFactory httpClientFactory;
        private IEnumerable<Album> AllAlbumsCons { get; set; } = new List<Album>();

		public DataStore(IHttpClientFactory httpClientFactory)
		{
            this.httpClientFactory = httpClientFactory;
        }

		public async Task<Album> GetAlbum (int id)
		{
            var albums = await GetAlbums();
            return albums.SingleOrDefault(m => m.Id == id);
		}

		public async Task<IEnumerable<Album>> GetAlbums()
		{
            if (!AllAlbumsCons.Any()) {
                var allAlbumsWithPhotos = await LoadAlbumPhotos();
                var allAlbums = await LoadAlbums();

                AllAlbumsCons = allAlbums.Select(m =>
                {
                    var awp = allAlbumsWithPhotos.FirstOrDefault(f => f.AlbumId == m.Id);
                    return new Album(m.Id, m.Title, awp.ThumbnailUrl, awp.Url);
                });
            }

            return AllAlbumsCons;
        }

        private async Task<IEnumerable<T>> GetCollection<T>(string url)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                url
            );

            var client = this.httpClientFactory.CreateClient();
            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                using var contentStream =
                    await response.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var collection = await JsonSerializer.DeserializeAsync
                    <IEnumerable<T>>(contentStream, options);

                return collection;
            }

            return null;
        }

        private Task<IEnumerable<AlbumDTO>> LoadAlbums ()
        {
            return GetCollection<AlbumDTO>("https://jsonplaceholder.typicode.com/albums");
        }

        private Task<IEnumerable<AlbumPhotoDTO>> LoadAlbumPhotos()
        {
            return GetCollection<AlbumPhotoDTO>("https://jsonplaceholder.typicode.com/photos");
        }
	}
}

