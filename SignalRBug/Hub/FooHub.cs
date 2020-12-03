using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRBug.Hub
{
    public class FooHub
        : Microsoft.AspNetCore.SignalR.Hub
    {


        public async IAsyncEnumerable<Bar> StreamBar()
        {
            for (var i = 0; i < 10; i++)
            {
                await Task.Delay(100);
                yield return new Bar{ Qux = i };
            }
            yield break;
        }
    }

    public class Bar
    {
        public int Qux { get; set; }
    }
}