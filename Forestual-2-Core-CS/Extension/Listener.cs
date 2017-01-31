using System;

namespace F2Core.Extension
{
    public class Listener
    {
        public Event Event { get; set; }
        public Delegate Delegate { get; set; }
        public string ExtensionNamespace { get; set; }

        public Listener() { }

        public Listener(Event e, Delegate @delegate, string @namespace) {
            Event = e;
            Delegate = @delegate;
            ExtensionNamespace = @namespace;
        }
    }
}
