
namespace Riigipuhad
{
    public class LocalContentPage
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }
        public LocalContentPage() { }
        public LocalContentPage(string title, string content, byte[] image) 
        {
            Title = title;
            Content = content;
            Image = image;
        }

    }
}
