﻿using System;
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
using ModernPlayerManager.ViewModels;

// Pour plus d'informations sur le modèle d'élément Boîte de dialogue de contenu, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace ModernPlayerManager.Dialogs
{
    public sealed partial class DiscrepancyDialog : ContentDialog
    {
        public DiscrepancyViewModel ViewModel { get; set; } 

        public DiscrepancyDialog(DialogMode mode) {
            this.InitializeComponent();
            ViewModel = new DiscrepancyViewModel(mode);
        }
    }
}