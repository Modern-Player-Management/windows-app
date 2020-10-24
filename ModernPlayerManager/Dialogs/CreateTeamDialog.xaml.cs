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
using ModernPlayerManager.Services;
using ModernPlayerManager.Services.API;
using Refit;

namespace ModernPlayerManager.Dialogs
{
    public sealed partial class CreateTeamDialog : ContentDialog
    {
        public Team Team { get; set; } = new Team();
        public ITeamApi Api = RestService.For<ITeamApi>(MpmHttpClient.Instance);

        public CreateTeamDialog()
        {
            this.InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
            Team = await Api.CreateTeam(Team);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
