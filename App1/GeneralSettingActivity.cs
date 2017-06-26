using System;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using App1.Data;
using App1.Model;
using Android.Support.Design.Widget;
using Android.Support.V7.App;

namespace App1
{
	[Activity (Label = "General Setting",Theme="@style/MyTheme")]
	public class GeneralSettingActivity : AppCompatActivity
    {
       
		private Button btnSave;
		private TextInputLayout txtName;
		private TextInputLayout txtemail;
		private TextInputLayout txtlicense;
		private static int Id=0;
		static GenaralSettingDatabase database;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.GeneralSetting);
     //       LayoutInflater inflater = LayoutInflater.From(this);
            // Get our button from the layout resource,
            // and attach an event to it
            fillView ();

			btnSave.Click += Onsave;
		}






        public static GenaralSettingDatabase Database {
			get {
				if (database == null) {
					database = new GenaralSettingDatabase ();
				}
				return database;
			}
		}

		public void fillView ()
		{
           
            btnSave = FindViewById<Button> (Resource.Id.btnSave);
             txtName = FindViewById<TextInputLayout> (Resource.Id.txtName);
			txtemail = FindViewById<TextInputLayout> (Resource.Id.txtEmail);
			txtlicense = FindViewById<TextInputLayout> (Resource.Id.txtlicense);
			var Count = Database.ISgenaralSetting ();
			var setting = Database.GetgenaralSetting ();
			if (Count != 0) {
				txtName.EditText.Text = setting.Name;
				txtemail.EditText.Text = setting.Email;
				txtlicense.EditText.Text = setting.license;
				Id = setting.ID;
			} 

			var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
			SetActionBar (toolbar);
			ActionBar.Title = "Check IN";

		}

		public void Onsave (object sender, EventArgs e)
		{
			using (GenaralSetting GenaralSettingObject = new GenaralSetting ()) {
				GenaralSettingObject.Name = txtName.EditText.Text;
				GenaralSettingObject.Email = txtemail.EditText.Text;
				GenaralSettingObject.license = txtlicense.EditText.Text;
				GenaralSettingObject.ID = Id;
				Database.SaveGenaralSetting (GenaralSettingObject);}
		
	
			Toast.MakeText (this, "saved successfully", ToastLength.Long).Show ();

			StartActivity (typeof(SplashActivity));
		}

	}
}

