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

        public AlbumDTO(int id, string title)
		{
			Id = id;
			Title = title;
		}
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

        public AlbumPhotoDTO(int id, int albumId, string title, string thumbnailUrl, string url)
        {
            Id = id;
            AlbumId = albumId;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
            Url = url;
        }
    }

	public interface IDataStore
	{
        Task<AlbumDTO> GetAlbum(int id);
        Task<IEnumerable<AlbumDTO>> GetAlbums();
        Task<AlbumPhotoDTO> GetAlbumPhoto(int albumId);

    }

    public class DataStore : IDataStore
	{
        private readonly IHttpClientFactory httpClientFactory;

		private IEnumerable<AlbumDTO> AllAlbums { get; set; }
		private IEnumerable<AlbumPhotoDTO> AllAlbumPhotos { get; set; }

		public DataStore(IHttpClientFactory httpClientFactory)
		{
            this.httpClientFactory = httpClientFactory;
            AllAlbums = new List<AlbumDTO>();
            AllAlbumPhotos = new List<AlbumPhotoDTO>();   
        }

		public async Task<AlbumDTO> GetAlbum (int id)
		{
            var albums = await GetAlbums();
            return albums.SingleOrDefault(m => m.Id == id);
			
		}

        public async Task<AlbumPhotoDTO> GetAlbumPhoto (int albumId)
        {
            var photos = await GetAlbumPhotos();
            return photos.FirstOrDefault(m => m.AlbumId == albumId);
        }

		public async Task<IEnumerable<AlbumDTO>> GetAlbums()
		{
            if(!AllAlbums.Any())
                AllAlbums = await LoadAlbums();

            
            return AllAlbums;
		}

        public async Task<IEnumerable<AlbumPhotoDTO>> GetAlbumPhotos()
        {
            if (!AllAlbumPhotos.Any())
                AllAlbumPhotos = await LoadAlbumPhotos();

            System.Console.WriteLine($"All data images: {AllAlbumPhotos.Count()}");
            return AllAlbumPhotos;
        }

        private async Task<IEnumerable<T>> GetCollection<T>(string url)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                url
            );

            var client = this.httpClientFactory.CreateClient();
            var resonse = await client.SendAsync(httpRequestMessage);

            if (resonse.IsSuccessStatusCode)
            {
                using var contentStream =
                    await resonse.Content.ReadAsStreamAsync();

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

