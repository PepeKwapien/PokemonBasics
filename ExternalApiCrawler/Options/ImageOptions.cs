namespace ExternalApiCrawler.Options
{
    public class ImageOptions
    {
        public bool DownloadImages { get; set; }
        public bool UploadImages { get; set; }
        public string ImagesLocation { get; set; }
        public string PokemonImagesLocation { get; set; }
        public string PokeballImagesLocation { get; set; }
        public string ImgurUrl { get; set; }
        public string AccessToken { get; set; }
        public string ClientId { get; set; }
    }
}
