﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModernPlayerManager.Services.API
{


    public class AuthenticationManager : HttpClientHandler
    {
        public static string Token { get; set; }
        public static string UserId { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, Token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
