namespace Capture_image.Models
{
    public class ImageModel
    {
        public IList<IFormFile> File { get; set; }

        public int Id {  get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

    }
}
