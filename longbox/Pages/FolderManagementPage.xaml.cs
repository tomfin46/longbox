using longbox.Controllers;
using longbox.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace longbox.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FolderManagementPage : Page
    {
        private FolderViewModel Manager = new FolderViewModel();

        public FolderManagementPage()
        {
            this.InitializeComponent();
        }

        private async void Add_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Manager.LoadFolder();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                await new MessageDialog("There was an error adding that folder, please try again").ShowAsync();
            }
        }
    }
}
