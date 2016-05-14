using Microsoft.AspNet.SignalR;

namespace SignalR.Reactive
{
    public static class DependencyResolverContext
    {
        public static IDependencyResolver Instance { get; set; }
    }
}
