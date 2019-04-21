using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KMSCalendar.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using KMSCalendar;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace KMSCalendar.Droid
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                //Control.Background = null;
            }
        }
    }
}