using System;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.WPF.Impl;
using Rg.Plugins.Popup.WPF.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.WPF;
using XPlatform = Xamarin.Forms.Platform.WPF.Platform;
using WinApplication = System.Windows.Application;

[assembly: Dependency(typeof(PopupPlatformWPF))]
namespace Rg.Plugins.Popup.WPF.Impl
{
    [Preserve(AllMembers = true)]
    internal class PopupPlatformWPF : IPopupPlatform
    {
        private IPopupNavigation PopupNavigationInstance => PopupNavigation.Instance;

        public event EventHandler OnInitialized
        {
            add => Popup.OnInitialized += value;
            remove => Popup.OnInitialized -= value;
        }

        public bool IsInitialized => Popup.IsInitialized;

        public bool IsSystemAnimationEnabled => false;

        [Preserve]
        public PopupPlatformWPF()
        {

        }

        public async Task AddAsync(PopupPage page)
        {
            var popup = new global::System.Windows.Controls.Primitives.Popup() { IsOpen = false };
            var renderer = (PopupPageRenderer)XPlatform.GetOrCreateRenderer(page);

            renderer.Prepare(popup);
            popup.Child = renderer.GetNativeElement();
            popup.IsOpen = true;
            page.ForceLayout();

            await Task.Delay(5);
        }
        public async Task RemoveAsync(PopupPage page)
        {
            var renderer = (PopupPageRenderer)XPlatform.GetOrCreateRenderer(page);
            var popup = renderer.Container;
            
            if (popup != null)
            {
                renderer.Destroy();

                Cleanup(page);

                page.Parent = null;
                popup.Child = null;
                popup.IsOpen = false;
            }

            await Task.Delay(5);
        }

        internal static void Cleanup(VisualElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var elementRenderer = XPlatform.GetRenderer(element);
            foreach (Element descendant in element.Descendants())
            {
                var child = descendant as VisualElement;
                if (child != null)
                {
                    var childRenderer = XPlatform.GetRenderer(child);
                    if (childRenderer != null)
                    {
                        childRenderer.Dispose();
                        XPlatform.SetRenderer(child, null);
                    }
                }
            }
            if (elementRenderer == null)
                return;

            elementRenderer.Dispose();
            XPlatform.SetRenderer(element, null);
        }
    }
}
