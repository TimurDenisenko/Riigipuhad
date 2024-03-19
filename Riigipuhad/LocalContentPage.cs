
using Plugin.Media.Abstractions;
using System;

namespace Riigipuhad
{
    public class LocalContentPage
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Desc { get; set; }
        public byte[] ImageByte { get; set; }
        public MediaFile ImageMedia { get; set; }
        public LocalContentPage() { }
        public LocalContentPage(string title, string content, byte[] image) 
        {
            Title = title;
            Content = content;
            Desc = "pühad";
            ImageByte = image;
            ImageMedia = null;
        }
        public LocalContentPage(Tuple<string, string, string, MediaFile> page)
        {
            Title = page.Item1;
            Content = page.Item2;
            Desc = page.Item3;
            ImageMedia = page.Item4;
            ImageByte = null;
        }

    }
}
