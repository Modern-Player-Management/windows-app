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
using ModernPlayerManager.Models;
using ModernPlayerManager.Pages;
using ModernPlayerManager.ViewModels;
using NavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
using NavigationViewSelectionChangedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ModernPlayerManager
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; set; } = ViewModelsLocator.Instance.MainViewModel;

        public MainPage() {
            this.InitializeComponent();
            ViewModel.ContentFrame = contentFrame;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            ViewModel.FetchTeams();
        }

        private void AddTeam_OnTapped(object sender, TappedRoutedEventArgs e) {
            ViewModel.AddTeam();
        }

        private void NavigationView_OnSelectionChanged(NavigationView sender,
            NavigationViewSelectionChangedEventArgs args) {
            var selectedTeam = args.SelectedItem as Team;
            if(selectedTeam != null) {
                contentFrame.Navigate(typeof(TeamPage), selectedTeam.Id);
            }
            else {
                contentFrame.Navigate(typeof(TeamWelcomePage));
            }
        }

        private void UserProfile_OnTapped(object sender, TappedRoutedEventArgs e) {
            ViewModel.NavigateToUserProfile();
        }

        private void LogOut(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.LogOut();
        }
    }
}