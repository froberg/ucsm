public readonly struct Album
    {
        public readonly int Id;
        public readonly string Title;
        public readonly string ThumbnailUrl;
        public readonly string ImageUrl;

        public Album(int id, string title, string thumbnailUrl, string url)
        {
            Id = id;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
            ImageUrl = url;
        }
    }