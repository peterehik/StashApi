using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Stash.Services
{
    public class Request
    {
        [JsonIgnore]
        public CancellationToken CancellationToken { get; set; }
    }
}
