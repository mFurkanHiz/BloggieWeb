namespace Bloggie.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; } // property yi nullable bırakmak istersek türün sonuna (string in sonuna) soru işareti koyulur
        //public string? Heading { get; set; } // now this is nullable
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShorstDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandler { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set;}
        public bool Visible { get; set;}
        public ICollection<Tag> Tags { get; set;}

    }
}
