
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using App1.Data;
using App1.Model;
namespace App1
{
	[Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : AppCompatActivity
	{
		ISharedPreferences prefs = Application.Context.GetSharedPreferences ("CheckINID", FileCreationMode.Private);
		static GenaralSettingDatabase GenaralSettingdatabase;
		static CheckInOutDatabase CheckInOutdatabase;
		static readonly string TAG = "X:" + typeof(SplashActivity).Name;

		public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
		{
			base.OnCreate(savedInstanceState, persistentState);
			SwitchPage ();
			//Log.Debug(TAG, "SplashActivity.OnCreate");
		}


		private void SwitchPage(){
			var Count = Database.ISgenaralSetting ();

			if (Count == 0) {
				StartActivity (new Intent (Application.Context, typeof(GeneralSettingActivity)));
			} else if (Count > 0) {
				var value1 = prefs.GetInt ("CheckinID", 0);
				if (value1 != 0) {
					var IsCheckOut = DatabaseCheckin.GetParkingStatus (value1);
					if (IsCheckOut.IsCheckout != false) {
						StartActivity (new Intent (Application.Context, typeof(CheckIn)));
					} else {
                        var CheckouActivity = new Intent(this, typeof(CheckOut));
                        CheckouActivity.PutExtra("CheckInID", value1);
                        StartActivity(CheckouActivity);
                        
					}
				} else {StartActivity (new Intent (Application.Context, typeof(CheckIn)));
				}




			}}
		protected override void OnResume()
		{
			base.OnResume();

			Task startupWork = new Task(() => {
				//Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
				SwitchPage ();
				Task.Delay(5000);  // Simulate a bit of startup work.
				//Log.Debug(TAG, "Working in the background - important stuff.");
			});

			startupWork.ContinueWith(t => {
				//Log.Debug(TAG, "Work is finished - start Activity1.");
				//StartActivity(new Intent(Application.Context, typeof(Activity1)));
			}, TaskScheduler.FromCurrentSynchronizationContext());

			startupWork.Start();
		}

		public static GenaralSettingDatabase Database {
			get {
				if (GenaralSettingdatabase == null) {
					GenaralSettingdatabase = new GenaralSettingDatabase ();
				}
				return GenaralSettingdatabase;
			}
		}
		public static CheckInOutDatabase DatabaseCheckin {
			get {
				if (CheckInOutdatabase == null) {
					CheckInOutdatabase = new CheckInOutDatabase ();
				}
				return CheckInOutdatabase;
			}
		}
	}
}

