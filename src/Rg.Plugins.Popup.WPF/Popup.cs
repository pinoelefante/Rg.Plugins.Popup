using System;
using System.Collections.Generic;
using System.Reflection;
using Rg.Plugins.Popup.WPF.Impl;
using Rg.Plugins.Popup.WPF.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Rg.Plugins.Popup
{
    [Preserve(AllMembers = true)]
    public static class Popup
    {
        internal static event EventHandler OnInitialized;

        internal static bool IsInitialized { get; private set; }

        public static void Init()
        {
            LinkAssemblies();

            IsInitialized = true;
            OnInitialized?.Invoke(null, EventArgs.Empty);
        }

        private static void LinkAssemblies()
        {
            DependencyService.Register<PopupPlatformWPF>();
            if (false.Equals(true))
            {
                var r = new PopupPageRenderer();
            }
        }
    }
}
