using System;

namespace Assets.Scripts.Core.Staff.Helpers
{
    public static class EventHandlerHelper
    {
        public static void Raise(this EventHandler self, Object sender)
        {
            var handler = self;
            if (handler != null) handler(sender, EventArgs.Empty);
        }
    }
}
