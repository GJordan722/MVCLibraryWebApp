namespace LibraryWebApp.Models
{
    public class SearchModel
    {
   
        public MediaModel Model = new MediaModel();
        public string? Category { get; set; }
        public List<MediaModel>? MediaList { get; set; } = new List<MediaModel>();

        public int? access { get; set; } = null;
    }
}
