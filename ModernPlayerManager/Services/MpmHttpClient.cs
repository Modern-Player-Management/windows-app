using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using ModernPlayerManager.Services.API;

namespace ModernPlayerManager.Services
{
    public sealed class MpmHttpClient : HttpClient
    {
        private static MpmHttpClient instance = null;

        public static MpmHttpClient Instance => instance ?? (instance = new MpmHttpClient());

        private MpmHttpClient() : base(new AuthenticationManager()) {
            if (ApplicationData.Current.LocalSettings.Values["backend_url"] is string backendUrl) {
                BaseAddress = new Uri(backendUrl);
            }
        }
    }
}
