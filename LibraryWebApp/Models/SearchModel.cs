namespace LibraryWebApp.Models
{
    public class SearchModel
    {
   
        public MediaModel Model = new MediaModel();
        public string? Category { get; set; }
        public List<MediaModel>? MediaList { get; set; } = new List<MediaModel>();
        public UserModel User { get; set; } = new UserModel();
        public List<CheckIOModel> checkiolist { get; set; } = new List<CheckIOModel>();
        public List<HoldIOModel> holdiolist { get; set; } = new List<HoldIOModel>();
        public int? access { get; set; } = null;
    }
}
