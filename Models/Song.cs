namespace MusicLibrary.Models
{
    public class Song
    {
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public bool IsLiked { get; set; }
        public bool IsBlacklisted { get; set; }
    }
}
