using SharpCompress.Archives;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

using longbox.Controllers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace longbox
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        private string savedFolder;
        private IReader comicReader;
        private Stream comicStream;
        private int pageNumber = 0;

        private int lastFlipIndex = 0;

        private ObservableCollection<BitmapImage> pages = new ObservableCollection<BitmapImage>();

        public MainPage()
        {
            this.InitializeComponent();

            new Navigation(this.NavView, this.ContentFrame);
        }

        private async void Choose_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var picker = new Windows.Storage.Pickers.FolderPicker();
                picker.FileTypeFilter.Add("*");
                StorageFolder folder = await picker.PickSingleFolderAsync();

                if (folder != null)
                {
                    this.savedFolder = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(folder);
                    this.txtBlock.Text += $"\n{this.savedFolder}";
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private async void Load_Comic_Button_Click(object sender, RoutedEventArgs e)
        {
            var folder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(this.savedFolder);

            IReadOnlyList<IStorageItem> itemsList = await folder.GetItemsAsync();
            foreach (var item in itemsList)
            {
                if (item is StorageFolder)
                {
                    this.txtBlock.Text += $"Folder {item.Name}\n";
                }
                else
                {
                    this.txtBlock.Text += $"\nFile {item.Name}\n";
                    //var newPath = Path.Combine(Path); //, "cache", this.savedFolder, item.Name


                    this.comicStream = await (item as StorageFile).OpenStreamForReadAsync();
                    this.comicReader = ReaderFactory.Open(this.comicStream, new ReaderOptions() { LeaveStreamOpen = true });
                    

                    while(this.comicReader.MoveToNextEntry() && pageNumber < 10)
                    {
                        if (!comicReader.Entry.IsDirectory)
                        {
                            this.txtBlock.Text += $"\nFile {comicReader.Entry.Key}\n";
                            var sampleFile = await localFolder.CreateFileAsync($"{this.pageNumber}", CreationCollisionOption.OpenIfExists);

                            using (var entryStream = comicReader.OpenEntryStream())
                            using (var writeStream = await sampleFile.OpenStreamForWriteAsync())
                            {
                                entryStream.CopyTo(writeStream);
                            }

                            using (var stream = await sampleFile.OpenReadAsync())
                            {
                                BitmapImage bitmapImage = new BitmapImage();
                                //bitmapImage.DecodePixelWidth = 600; //match the target Image.Width, not shown
                                await bitmapImage.SetSourceAsync(stream);
                                pages.Add(bitmapImage);
                            }
                            this.pageNumber++;
                        }
                    }
                }
            }
        }

        private async Task<StorageFile> CopyPrevPage()
        {
            this.pageNumber -= 1;
            var file = await localFolder.CreateFileAsync($"{this.pageNumber}", CreationCollisionOption.OpenIfExists);
            return file;
        }

        private async Task CopyNextPage()
        {
            if(this.comicReader.MoveToNextEntry())
            {
                if (!comicReader.Entry.IsDirectory)
                {
                    this.txtBlock.Text += $"\nFile {comicReader.Entry.Key}\n";
                    var sampleFile = await localFolder.CreateFileAsync($"{this.pageNumber}", CreationCollisionOption.OpenIfExists);

                    using (var entryStream = comicReader.OpenEntryStream())
                    using (var writeStream = await sampleFile.OpenStreamForWriteAsync())
                    {
                        entryStream.CopyTo(writeStream);
                    }

                    using (var stream = await sampleFile.OpenReadAsync())
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        //bitmapImage.DecodePixelWidth = 600; //match the target Image.Width, not shown
                        await bitmapImage.SetSourceAsync(stream);
                        pages.Add(bitmapImage);
                    }
                    this.pageNumber++;
                }
                //if (!comicReader.Entry.IsDirectory)
                //{
                //    this.txtBlock.Text += $"\nFile {comicReader.Entry.Key}\n";
                //    using (var entryStream = comicReader.OpenEntryStream())
                //    using (var writeStream = await file.OpenStreamForWriteAsync())
                //    {
                //        entryStream.CopyTo(writeStream);
                //    }
                //}
            } else
            {
                DisposeStreams();
            }
        }

        private async Task SetImage(StorageFile file)
        {
            using (var stream = await file.OpenReadAsync())
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.DecodePixelWidth = 600; //match the target Image.Width, not shown
                await bitmapImage.SetSourceAsync(stream);
                //imgPage.Source = bitmapImage;
                pages.Add(bitmapImage);

            }
        }

        private async void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            await CopyNextPage();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            DisposeStreams();
        }

        private async void Open_Comic_Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                StorageFile file = await localFolder.GetFileAsync($"{i}");
                await SetImage(file);
            }
        }

        private void DisposeStreams ()
        {
            comicReader.Dispose();
            comicStream.Dispose();

        }

        private async void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.lastFlipIndex == flip.SelectedIndex)
            {
                return;
            }
            
            if (this.lastFlipIndex > flip.SelectedIndex)
            {
                this.lastFlipIndex = flip.SelectedIndex;
                return;
            }

            if (this.lastFlipIndex < flip.SelectedIndex)
            {
                await CopyNextPage();
                this.lastFlipIndex = flip.SelectedIndex;
            }
        }
    }
}
