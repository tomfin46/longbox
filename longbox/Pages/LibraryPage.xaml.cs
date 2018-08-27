using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using longbox.Controllers;
using longbox.Models;
using System.Collections.ObjectModel;
using longbox.ViewModels;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace longbox.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LibraryPage : Page
    {
        private LibraryViewModel Library = new LibraryViewModel();

        public LibraryPage()
        {
            this.InitializeComponent();

            Library.Initialise();
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Box)
            {
                Library.OpenBox((e.ClickedItem as Box));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Library.CloseBox();
        }
    }
}
