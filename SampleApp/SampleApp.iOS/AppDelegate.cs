using System;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.XForms.iOS.Backdrop;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.DataForm;
using Syncfusion.XForms.iOS.TextInputLayout;
using Foundation;
using UIKit;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;

namespace SampleApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            SfCheckBoxRenderer.Init();
            Syncfusion.XForms.iOS.PopupLayout.SfPopupLayoutRenderer.Init();
            SfBackdropPageRenderer.Init();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            SfDataFormRenderer.Init();
            SfTextInputLayoutRenderer.Init();
            SfRadioButtonRenderer.Init();
            SfSegmentedControlRenderer.Init();
            new Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer();
            Syncfusion.XForms.iOS.Accordion.SfAccordionRenderer.Init();
            SfButtonRenderer.Init();
            SfListViewRenderer.Init();
            CachedImageRenderer.Init();
            var ignore = typeof(SvgCachedImage);
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
