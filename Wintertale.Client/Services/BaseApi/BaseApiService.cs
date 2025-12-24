using System;
using System.Collections.Generic;
using System.Text;

namespace Wintertale.Client.Services.BaseApi {
    internal abstract class BaseApiService {
        private string? baseUri;

        public string? BaseUri {
            get => baseUri; 
            protected set => baseUri = value?.TrimEnd('/');
        }
    }
}
