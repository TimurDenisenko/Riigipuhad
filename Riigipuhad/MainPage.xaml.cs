using Plugin.Media.Abstractions;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Resources;

namespace Riigipuhad
{
    public partial class MainPage : TabbedPage
    {
        string currentPage;
        public MainPage()
        {
            CreateTabbedPage(new List<LocalTabbedPage>
            {
                new LocalTabbedPage
                {
                    Title = "Talv",
                    Pages = new List<LocalContentPage> {
                        new LocalContentPage {
                            Title = "Detsember",
                            Content = "24. detsember Jõululaupäev\n25. detsember Esimene jõulupüha\n26. detsember Teine jõulupüha",
                            ImageByte = Properties.Resources.winter1
                        },
                        new LocalContentPage {
                            Title = "Jaanuar",
                            Content = "01. jaanuar Uusaasta",
                            ImageByte = Properties.Resources.winter2
                        },
                        new LocalContentPage {
                            Title = "Veebruar",
                            Content = " 24. veebruar Iseseisvuspäev, Eesti Vabariigi aastapäev",
                            ImageByte = Properties.Resources.winter3
                        }
                    }
                },
                new LocalTabbedPage
                {
                    Title = "Kevad",
                    Pages = new List<LocalContentPage> {
                        new LocalContentPage("Märts","29. märts Suur reede\n31. märts Ülestõusmispühade 1. püha",Properties.Resources.spring1),
                        new LocalContentPage("Aprill","23. aprill – veteranipäev",Properties.Resources.spring2),
                        new LocalContentPage("Mai","01. mai Kevadpüha\n19. mai Nelipühade 1. püha",Properties.Resources.spring3),
                    }
                },
                new LocalTabbedPage
                {
                    Title = "Suvel",
                    Pages = new List<LocalContentPage> {
                        new LocalContentPage(title: "Juuni",content: "23. juuni Võidupüha\n24. juuni Jaanipäev",image: Properties.Resources.summer1),
                        new LocalContentPage(title: "Juuli",content: "10. juuli Capybara armastuspäev",image: Properties.Resources.summer2),
                        new LocalContentPage(title: "August",content: "20. august Taasiseseisvumispäev",image: Properties.Resources.summer3),
                    }
                },
                new LocalTabbedPage
                {
                    Title = "Sügis",
                    Pages = new List<LocalContentPage> {
                        new LocalContentPage("September","1. september teadmistepäev\n8. september (septembri teine pühapäev) vanavanemate päev",Properties.Resources.autumn1),
                        new LocalContentPage("Oktoober","19. oktoober (oktoobri kolmas laupäev) hõimupäev",Properties.Resources.autumn2),
                        new LocalContentPage("November","10. november (novembri teine pühapäev) isadepäev",Properties.Resources.autumn3),
                    }
                }
            });
            //FileManage.ClearFiles();
            UpdateTabbedPage();
        }

        private void UpdateTabbedPage()
        {
            string[] files = FileManage.GetFilesFromFolder();
            List<LocalTabbedPage> pages = new List<LocalTabbedPage>();
            foreach (string file in files) 
            {
                pages.Add(FileManage.DeserializeFromFile<LocalTabbedPage>(FileManage.GetSolutionDirectory()+$"/{file}"));
            }
            CreateTabbedPage(pages);
        }
        private void CreateTabbedPage(List<LocalTabbedPage> listpage)
        {
            foreach (LocalTabbedPage page in listpage)
            {
                TabbedPage tp = new TabbedPage { Title = page.Title };
                foreach (LocalContentPage cp in page.Pages)
                {
                    if (cp.ImageByte != null)
                        tp.Children.Add(CreatePage(cp.Title, cp.Content, cp.Desc, cp.ImageByte));
                    else
                        tp.Children.Add(CreatePage(cp.Title, cp.Content, cp.Desc, image2: cp.ImageMedia));
                }
                Children.Add(tp);
            }
        }
        private ContentPage CreatePage(string title, string description, string content, byte[] image1 = null, MediaFile image2 = null)
        {
            ImageSource img;
            if (image1!=null)
                img = FileManage.ConvertToImageSource(image1);
            else
                img = FileManage.ConvertToImageSource(image2);
            Label lbl = new Label { Text = description, FontFamily = "Gl" };
            ImageButton image = new ImageButton { Source = img };
            image.Clicked += async (sender, e) => await DisplayAlert($"{title} {content}",lbl.Text,"OK");
            Button btn1 = new Button { Text = "Lisada uus leht", };
            Button btn2 = new Button { Text = "Lisada uus vahekaardi leht", };
            Button btn3 = new Button { Text= "Kustuta leht", };
            Button btn4 = new Button { Text= "Kustuta element", };
            Button btn5 = new Button { Text = "Muuta element", };
            btn1.Clicked+=Btn_Clicked;
            btn2.Clicked+=BtnTapped_Clicked;
            ContentPage contentPage = new ContentPage
            {
                Title = title,
                Content = new StackLayout { Children = { image, lbl,
                        new StackLayout { Children = { btn1, btn2 }, Orientation = StackOrientation.Horizontal,HorizontalOptions = LayoutOptions.CenterAndExpand },
                        new StackLayout { Children = { btn3, btn4 }, Orientation = StackOrientation.Horizontal,HorizontalOptions = LayoutOptions.CenterAndExpand },
                        new StackLayout { Children = { btn5 }, Orientation = StackOrientation.Horizontal,HorizontalOptions = LayoutOptions.CenterAndExpand },
                    }, VerticalOptions= LayoutOptions.Center
                }
            };
            async Task<string> _element()
            {
                string action = string.Empty;
                if (image?.IsVisible ?? false == true & lbl?.IsVisible ?? false == true)
                {
                    action = await DisplayActionSheet("Vali element", "Tühista", null, "Pilt", "Kirjaldus");
                }
                else if (image?.IsVisible ?? false == true & lbl?.IsVisible ?? false == false)
                {
                    action = await DisplayActionSheet("Vali element", "Tühista", null, "Pilt");
                }
                else if (image?.IsVisible ?? false == false & lbl?.IsVisible ?? false == true)
                {
                    action = await DisplayActionSheet("Vali element", "Tühista", null, "Kirjaldus");
                }
                return action;
            };
            btn3.Clicked+=async(sender, e) =>
            {
                string action = await DisplayActionSheet("Lihtsalt leht või vahekaardi leht?", "Tühista", null, "Leht", "Vahekaardi leht");
                if (action=="Leht")
                {
                    foreach (TabbedPage item in Children)
                    {
                        if (item.Children.Contains(contentPage))
                        {
                            item.Children.Remove(contentPage);
                            break;
                        }
                    }
                }
                else if (action=="Vahekaardi leht")
                {
                    foreach (TabbedPage item in Children)
                    {
                        if (item.Children.Contains(contentPage))
                        {
                            Children.Remove(item);
                            break;
                        }
                    }
                };
            };
            btn4.Clicked+=async (sender, e) =>
            {
                string action = await _element();
                if (action=="Pilt")
                {
                    image.IsVisible = false;
                    image = null;
                }
                if (action=="Kirjaldus")
                {
                    lbl.IsVisible = false;
                    lbl = null;
                }
            };
            btn5.Clicked += async (sender, e) =>
            {
                string action = await _element();
                if (action == "Pilt")
                {
                    await CrossMedia.Current.Initialize();
                    MediaFile new_image = await CrossMedia.Current.PickPhotoAsync();
                    image.Source = FileManage.ConvertToImageSource(new_image);
                }
                if (action == "Kirjaldus")
                {
                    string new_kirjaldus = await DisplayPromptAsync("Kirjaldus", "Kirjuta uus kirjeldus");
                    lbl.Text = new_kirjaldus;
                }
            };
            return contentPage;
        }

        private async void BtnTapped_Clicked(object sender, EventArgs e)
        {
            string new_title = await DisplayPromptAsync("Vahekaardi päiseleht", "Kirjuta vahekaardi päiseleht");
            LocalTabbedPage ltp = new LocalTabbedPage { Title = new_title };
            do
            {
                LocalContentPage lcp = new LocalContentPage(await LocalPage());
                lcp.ImageByte = await FileManage.ConvertToByteArray(FileManage.ConvertToImageSource(lcp.ImageMedia));
                ltp.Pages.Add(lcp);
            } while (await DisplayAlert("Leht", "Kas soovite lisada lehe?", "Jah", "Ei"));
            CreateTabbedPage(new List<LocalTabbedPage> { ltp });
            FileManage.SerializeToFile(ltp, FileManage.GetSolutionDirectory()+$"/{new_title}.json");
        }

        private async void Btn_Clicked(object sender, EventArgs e)
        {
            Tuple<string, string, string, MediaFile>  page = await LocalPage();
            Children.Add(CreatePage(page.Item1,page.Item2,page.Item3,image2:page.Item4));
        }
        private async Task<Tuple<string,string,string,MediaFile>> LocalPage()
        {
            string new_title = await DisplayPromptAsync("Päiseleht", "Kirjuta päiseleht");
            string new_desc = await DisplayPromptAsync("Lehe sisu", "Kirjuta lehe sisu");
            string new_content = await DisplayPromptAsync("Lehe kirjeldus", "Kirjuta lehe kirjeldus");
            await CrossMedia.Current.Initialize();
            MediaFile new_image = await CrossMedia.Current.PickPhotoAsync();
            return new Tuple<string, string, string, MediaFile>(new_title, new_desc, new_content, new_image);
        }
    }
}
