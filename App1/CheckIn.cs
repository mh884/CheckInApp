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
using System.Net.Mail;
using System.Net;
using Java.Security.Cert;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using App1.Model;
using System.Threading.Tasks;
using Android.Support.V7.App;
namespace App1
{
	[Activity(Label = "CheckIn",Theme="@style/MyTheme")]
	public class CheckIn : Activity

    {
        ISharedPreferences prefs = Application.Context.GetSharedPreferences ("CheckINID", FileCreationMode.Private);
		private static int Id;
		static CheckInOutDatabase database;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CheckIn);
            // Create your application here
		
			ImageButton CheckIN = FindViewById<ImageButton>(Resource.Id.imageCheckIn);
			CheckIN.Click += OnCheckIn;
			var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);

			//Toolbar will now take on default actionbar characteristics
			SetActionBar (toolbar);



        }




		public static CheckInOutDatabase Database {
			get {
				if (database == null) {
					database = new CheckInOutDatabase ();
				}
				return database;
			}
		}

		public void OnCheckIn(object sender,EventArgs e)
        {
            Task startupWork = new Task(() => { sendEmail(); });

            startupWork.Start();

		this.Finish();
        }

        private void sendEmail() {

            try
            {
                var datatime = DateTime.Now;

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("mhamd884@gmail.com");
                mail.To.Add("mhamd884@hotmail.com");
                mail.Subject = "TEST";
                mail.Body = $"Check In " + datatime.ToString();
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("mhamd884@gmail.com", "50204243.002");
                SmtpServer.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                SmtpServer.Send(mail);

                CheckInOut Object = new CheckInOut();
                Object.ID = 0;
                Object.IsCheckout = false;
                Id = Database.SaveParkingStatus(Object);

               // Store ID in Shered Storage
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutInt("CheckinID", Id);
                editor.Apply();

                //Navigate to Checkout Activity
                var CheckouActivity = new Intent(this, typeof(CheckOut));
                CheckouActivity.PutExtra("CheckInID", Id);
                StartActivityForResult(CheckouActivity,100);



            }
            catch (Exception A)
            {
                Console.WriteLine("Ouch!" + A.ToString());
            }


        }
        protected override void OnActivityResult(int requestCode, Result ResultCode, Intent data)
        {

            base.OnActivityResult(requestCode, ResultCode, data);
            if (ResultCode == Result.Ok && requestCode == 100)
            {
			//	var dialog = new Android.Support.V7.App.AlertDialog.Builder(this);
              //  dialog.SetTitle("Confirmation");
              //  dialog.SetMessage(string.Format("You have checkout"));
              //  dialog.Show();
		//Toast.MakeText (this, "Check Out at:" + DateTime.Now, ToastLength.Long).Show ();
                Id = 0;
            }

        }
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			
			MenuInflater.Inflate (Resource.Menu.home, menu);
			return base.OnCreateOptionsMenu (menu);
		}
		public override bool OnOptionsItemSelected (IMenuItem item)
		{	switch (item.ItemId) {

			case Resource.Id.Genaral_settings:
				StartActivity (typeof(GeneralSettingActivity));
				break;
			case Resource.Id.Account_settings:
				//StartActivity (typeof(AccountSetting));
				break;

			default:
				break;
			}

			Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
			return base.OnOptionsItemSelected (item);
		}


    }
    }
