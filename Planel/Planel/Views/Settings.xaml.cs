﻿using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.Phone.UI.Input;
using Windows.Foundation.Metadata;
using System.Threading.Tasks;
using Planel.Models;
using Planel;
using Windows.ApplicationModel.Store;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
   
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested +=
            App_BackRequested;


        }
        ~Settings()
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -=
             App_BackRequested;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }

        }

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.licenseactive == true)
            {
                if(App.License.IsActive == true)
                {
                    Licencer.Text = "License active WW";
                }
                if (App.License.IsActive == false)
                {
                    Licencer.Text = "License active IR";
                }
            }
            else
            {
                Licencer.Text = "Trial";
                declame.Visibility = Visibility.Visible;
            }


            Frame rootFrame = Window.Current.Content as Frame;

            string myPages = "";
            foreach (PageStackEntry page in rootFrame.BackStack)
            {
                myPages += page.SourcePageType.ToString() + "\n";
            }


            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
            namebox.Text = ApplicationData.Current.LocalSettings.Values["Username"].ToString();
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("avatar.jpg");

            avatar.ImageSource = new BitmapImage(new Uri(sampleFile.Path));
        }
        private string name;
        private string filename;

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {


                // Application now has read/write access to the picked file
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;

                try
                {
                    Windows.Storage.StorageFolder storageFolder =
    Windows.Storage.ApplicationData.Current.LocalFolder;
                    Windows.Storage.StorageFile sampleFile =
                        await storageFolder.GetFileAsync("avatar.jpg");
                    sampleFile.DeleteAsync(StorageDeleteOption.PermanentDelete);

                }
                catch { }

                StorageFile copiedFile = await file.CopyAsync(localFolder, "avatar.jpg");
                
                filename = "avatar.jpg";
                

                avatar.ImageSource = null;
                avatar.ImageSource = new BitmapImage(new Uri(copiedFile.Path));
                var messageDialog = new MessageDialog("Your picture had been saved successfuly.");
                messageDialog.ShowAsync();

            }
        }

        private void namebox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["Username"] = namebox.Text;
        }

        private  async void logout_Click(object sender, RoutedEventArgs e)
        {
            bye.Visibility = Visibility.Visible;

            await Task.Delay(3000);

            Localdb.Logout();
            
        }

        private async void Buy_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?productid=9n9c2hwnzcft"));
        }

        private void Iran_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(IranBye));
        }
    }
}
