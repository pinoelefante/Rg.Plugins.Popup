using System;
using System.Threading.Tasks;
using System.Windows;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.WPF;
using WinPopup = global::System.Windows.Controls.Primitives.Popup;
using Size = System.Windows.Size;
using WinApplication = System.Windows.Application;
using Rg.Plugins.Popup.WPF.Renderers;

[assembly:ExportRenderer(typeof(PopupPage), typeof(PopupPageRenderer))]
namespace Rg.Plugins.Popup.WPF.Renderers
{
    [Preserve(AllMembers = true)]
    public class PopupPageRenderer : PageRenderer
    {
        public WinPopup Container { get; private set; }

        internal PopupPage CurrentElement => (PopupPage)Element;
        [Preserve]
        public PopupPageRenderer()
        {
        }
        internal void Prepare(WinPopup container)
        {
            SetCenterPopup(container);
            Container = container;
            Container.LostFocus += (s, e) => Container.Focus();
            Container.IsOpen = true;
        }

        internal void Destroy()
        {
            Container.IsOpen = false;
            Container = null;
        }
        private void SetCenterPopup(WinPopup popup)
        {
            var screen_w = System.Windows.SystemParameters.PrimaryScreenWidth;
            var screen_h = System.Windows.SystemParameters.PrimaryScreenHeight;

            popup.MaxHeight = screen_h - (screen_h*0.2);
            popup.MaxWidth = screen_w - (screen_w *0.2);

            var window = System.Windows.Application.Current.MainWindow;

            popup.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
            popup.PlacementTarget = window;
        }
    }
}
