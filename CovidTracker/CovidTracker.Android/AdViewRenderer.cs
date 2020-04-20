using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CovidTracker.Controls;
using CovidTracker.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: Xamarin.Forms.ExportRenderer(typeof(AdControlView), typeof(AdViewRenderer))]
namespace CovidTracker.Droid
{
    [Obsolete]
    public class AdViewRenderer: ViewRenderer<Controls.AdControlView, AdView>
    {
        private AdView _adView;

        public AdViewRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<AdControlView> e)
        {
            base.OnElementChanged(e);

            if (Control == null && e.NewElement != null)
                SetNativeControl(CreateAdView());
        }

        private AdView CreateAdView()
        {
            if (_adView != null)
                return _adView;

            _adView = new AdView(Context)
            {
                AdUnitId = Element.AdUnitId,
                AdSize = AdSize.SmartBanner
            };

            _adView.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

            _adView.LoadAd(new AdRequest.Builder().Build());

            return _adView;
        }
    }
}