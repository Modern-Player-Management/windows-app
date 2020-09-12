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
using ModernPlayerManager.ViewModels;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace ModernPlayerManager.Pages
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class TeamPage : Page
    {
        public TeamViewModel ViewModel {get; set; }

        public TeamPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            ViewModel = new TeamViewModel(e.Parameter.ToString());
            await ViewModel.FetchTeam();
        }

        private async void DeleteDiscrepancy(object sender, RoutedEventArgs e) {
            var discrepancy = (sender as Button)?.DataContext as Discrepancy;
            if(ViewModel.DeleteDiscrepancyCommand.CanExecute(discrepancy)) {
                await ViewModel.DeleteDiscrepancyCommand.ExecuteAsync(discrepancy);
            }
        }

        private async void EditDiscrepancy(object sender, RoutedEventArgs e)
        {
            var discrepancy = (sender as Button)?.DataContext as Discrepancy;
            if (ViewModel.EditDiscrepancyCommand.CanExecute(discrepancy))
            {
                await ViewModel.EditDiscrepancyCommand.ExecuteAsync(discrepancy);
            }
        }

        private void HandleClickAddDiscrepancy(object sender, RoutedEventArgs e) {
            var evt = (sender as Button)?.DataContext as Event;
            if(ViewModel.AddDiscrepancyCommand.CanExecute(evt)) {
                ViewModel.AddDiscrepancyCommand.Execute(evt);
            }
        }
    }


}
