using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using ModernPlayerManager.Services.API;
using ModernPlayerManager.ViewModels.DataViewModels;
using Refit;

// Pour plus d'informations sur le modèle d'élément Boîte de dialogue de contenu, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace ModernPlayerManager.Dialogs
{
    public sealed partial class CreateTeamDialog : ContentDialog
    {
        public TeamViewModel ViewModel { get; set; } = new TeamViewModel(new Team());
        public ITeamApi Api = RestService.For<ITeamApi>(new HttpClient(new AuthenticatedHttpClientHandler()) { BaseAddress = new Uri("https://api-mpm.herokuapp.com") });

        public CreateTeamDialog()
        {
            this.InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
            ViewModel.Model = await Api.CreateTeam(ViewModel.Model);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
