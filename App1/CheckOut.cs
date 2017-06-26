
using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace App1
{
	[Activity (Label = "CheckOut" ,Theme="@style/MyTheme")]			
	public class CheckOut : Activity
	{
		ISharedPreferences prefs = Application.Context.GetSharedPreferences ("CheckINID", FileCreationMode.Private);
		private static int Id = 0;
		static CheckInOutDatabase database;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.CheckOut);
			ImageButton CheckOut = FindViewById<ImageButton> (Resource.Id.imageCheckOut);

			CheckOut.Click += OnCheckOut;
    
			var CheckInID = Intent.Extras.GetInt ("CheckInID");
			Id = CheckInID;
			var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
			SetActionBar (toolbar);
			ActionBar.Title = "Check Out";


		}

		public static CheckInOutDatabase Database {
			get {
				if (database == null) {
					database = new CheckInOutDatabase ();
				}
				return database;
			}
		}

		public void OnCheckOut (object sender, EventArgs e)
		{
			CheckInOut Object = new CheckInOut (); 
			Object.ID = Id;
			Object.IsCheckout = true;
			var result = Database.SaveParkingStatus (Object);

			Intent myIntent = new Intent (this, typeof(CheckIn));
			myIntent.PutExtra ("ID", result);
			SetResult (Result.Ok, myIntent);
			//StartActivity(typeof(CheckIn));
			// StartActivity(new Intent(Application.Context, typeof(CheckIn)));
			Toast.MakeText (this, "Check Out at:" + DateTime.Now, ToastLength.Long).Show ();
			this.Finish ();


		}
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			
			MenuInflater.Inflate (Resource.Menu.home, menu);
			menu.RemoveItem (Resource.Id.Logout);
			return base.OnCreateOptionsMenu (menu);
		}
		public override bool OnOptionsItemSelected (IMenuItem item)
		{	switch (item.ItemId) {

			case Resource.Id.Genaral_settings:
				StartActivity (typeof(GeneralSettingActivity));
				break;
			case Resource.Id.Account_settings:
				break;

			default:
				break;
			}

			Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
			return base.OnOptionsItemSelected (item);
		}

	}
}

