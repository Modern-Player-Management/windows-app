using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernPlayerManager.ViewModels
{
    public class ViewModelsLocator
    {
        private static ViewModelsLocator viewModelsLocator;

        public static ViewModelsLocator Instance =>
            viewModelsLocator ?? (viewModelsLocator = new ViewModelsLocator());

        private MainViewModel mainViewModel;

        public MainViewModel MainViewModel => mainViewModel ?? (mainViewModel = new MainViewModel());
    }
}
