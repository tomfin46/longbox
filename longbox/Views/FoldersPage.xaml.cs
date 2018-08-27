using longbox.ViewModels;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace longbox.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FoldersPage : Page
    {
        private FoldersViewModel ViewModel = new FoldersViewModel();

        public FoldersPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadFolders();
        }

        private async void AddFolder_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await ViewModel.AddFolder();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                await new MessageDialog("There was an error adding that folder, please try again").ShowAsync();
            }
        }
    }
}
