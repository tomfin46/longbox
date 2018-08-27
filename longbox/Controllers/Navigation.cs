using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using longbox.Pages;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace longbox.Controllers
{

    class Navigation
    {
        private readonly IList<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("library", typeof(LibraryPage)),
            ("folder", typeof(FolderManagementPage)),
        };

        private readonly NavigationView _navView;
        private readonly Frame _frame;

        public Navigation(NavigationView navView, Frame frame)
        {
            _navView = navView;
            _frame = frame;

            _navView.ItemInvoked += NavView_ItemInvoked;
            _frame.Navigated += Frame_Navigated;
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

            if (args.IsSettingsInvoked)
            {
                //_frame.Navigate(typeof(SettingsPage));
            }
            else
            {
                // Getting the Tag from Content (args.InvokedItem is the content of NavigationViewItem)
                var navItemTag = _navView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(i => args.InvokedItem.Equals(i.Content))
                    .Tag.ToString();

                Navigate(navItemTag);
            }
        }

        private void Navigate(string navItemTag)
        {
            var item = _pages.First(p => p.Tag.Equals(navItemTag));
            _frame.Navigate(item.Page);
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            _navView.IsBackEnabled = _frame.CanGoBack;

            //if (_frame.SourcePageType == typeof(SettingsPage))
            //{
            //    // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag
            //    _navView.SelectedItem = (NavigationViewItem)_navView.SettingsItem;
            //}
            //else
            //{
                var item = _pages.First(p => p.Page == e.SourcePageType);

                _navView.SelectedItem = _navView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));
            //}
        }
    }
}
