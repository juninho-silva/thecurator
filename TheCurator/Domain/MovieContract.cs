namespace Domain
{
    public class MovieContract
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }
        public string Platform { get; set; }

        public MovieContract(string title, string director, int releaseYear, string platform)
        {
            Title = title;
            Director = director;
            ReleaseYear = releaseYear;
            Platform = platform;
        }
    }
}
