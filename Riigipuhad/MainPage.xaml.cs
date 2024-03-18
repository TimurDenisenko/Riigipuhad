using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Riigipuhad
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            CreateTabbedPage(new List<Tuple<string, List<Tuple<string, string, byte[]>>>> {
                new Tuple<string, List<Tuple<string, string, byte[]>>>(
                "Talv",new List<Tuple<string, string, byte[]>>{
                    new Tuple<string, string, byte[]>("Detsember","24. detsember Jõululaupäev\n25. detsember Esimene jõulupüha\n26. detsember Teine jõulupüha",Properties.Resources.winter1),
                    new Tuple<string, string, byte[]>("Jaanuar","01. jaanuar Uusaasta",Properties.Resources.winter2),
                    new Tuple<string, string, byte[]>("Veebruar"," 24. veebruar Iseseisvuspäev, Eesti Vabariigi aastapäev",Properties.Resources.winter3)
                    }),
                new Tuple<string, List<Tuple<string, string, byte[]>>>(
                "Kevad",new List<Tuple<string, string, byte[]>>{
                    new Tuple<string, string, byte[]>("Märts","29. märts Suur reede\n31. märts Ülestõusmispühade 1. püha",Properties.Resources.spring1),
                    new Tuple<string, string, byte[]>("Aprill","23. aprill – veteranipäev",Properties.Resources.spring2),
                    new Tuple<string, string, byte[]>("Mai"," 01. mai Kevadpüha\n19. mai Nelipühade 1. püha",Properties.Resources.spring3)
                    }),
                new Tuple<string, List<Tuple<string, string, byte[]>>>(
                "Suvel",new List<Tuple<string, string, byte[]>>{
                    new Tuple<string, string, byte[]>("Juuni","23. juuni Võidupüha\n24. juuni Jaanipäev",Properties.Resources.summer1),
                    new Tuple<string, string, byte[]>("Juuli","10. juuli Capybara armastuspäev",Properties.Resources.summer2),
                    new Tuple<string, string, byte[]>("August","20. august Taasiseseisvumispäev",Properties.Resources.summer3)
                    }),
                new Tuple<string, List<Tuple<string, string, byte[]>>>(
                "Sügis",new List<Tuple<string, string, byte[]>>{
                    new Tuple<string, string, byte[]>("September","1. september teadmistepäev\n8. september (septembri teine pühapäev) vanavanemate päev",Properties.Resources.autumn1),
                    new Tuple<string, string, byte[]>("Oktoober","19. oktoober (oktoobri kolmas laupäev) hõimupäev",Properties.Resources.autumn2),
                    new Tuple<string, string, byte[]>("November","10. november (novembri teine pühapäev) isadepäev",Properties.Resources.autumn3)
                    })
            });
        }

        private void CreateTabbedPage(List<Tuple<string, List<Tuple<string, string, byte[]>>>> listpage)
        {
            foreach (Tuple<string, List<Tuple<string, string, byte[]>>> page in listpage)
            {
                TabbedPage tp = new TabbedPage { Title = page.Item1 };
                foreach (Tuple<string, string, byte[]> cp in page.Item2)
                {
                    tp.Children.Add(CreatePage(cp.Item1, cp.Item2, cp.Item3));
                }
                Children.Add(tp);
            }
        }
        private ContentPage CreatePage(string title, string description, byte[] img)
        {
            ImageButton image = new ImageButton { Source = ImageSource.FromStream(() => new MemoryStream(img)) };
            Label lbl = new Label { Text = description};
            ContentPage page = new ContentPage
            {
                Title = title,
                Content = new StackLayout { Children = { image,lbl }, VerticalOptions = LayoutOptions.Center },
            };
            return page;
        }
    }
}
