using System;
using System.Collections.Generic;
using System.Text;

namespace Riigipuhad
{
    public class LocalTabbedPage
    {
        public string Title { get; set; }
        public List<LocalContentPage> Pages { get; set; }
        public LocalTabbedPage() 
        {
            Pages = new List<LocalContentPage>();
        }
    }
}
