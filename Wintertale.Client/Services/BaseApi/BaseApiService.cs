using System;
using System.Collections.Generic;
using System.Text;

namespace Wintertale.Client.Services.BaseApi {
    internal abstract class BaseApiService {
        public string BaseUri {
            get;
            protected set {
                value.TrimEnd('/');
                BaseUri = value;
            } 
        }
    }
}
