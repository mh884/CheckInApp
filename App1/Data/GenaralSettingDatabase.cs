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
using SQLite;
using App1.Model;
using System.IO;

namespace App1.Data
{
	public class GenaralSettingDatabase
	{
		private SQLiteConnection connection;
		static object locker = new object ();

		public GenaralSettingDatabase ()
		{
			try {
				lock (locker) {
					GetConnetionDatabase a = new GetConnetionDatabase ();
					connection = a.GetConnection ();
					connection.CreateTable<GenaralSetting> ();
				}


			} catch (SQLiteException ex) {
				// return ex.Message;
			}
		}

		public void  SaveGenaralSetting (GenaralSetting setting)
		{
			lock (locker) {
				if (ISgenaralSetting() > 0) {
					connection.Update (setting);
					// return setting.ID;
				} else {

					connection.Insert (setting);

				}

			}

		}


		public GenaralSetting GetgenaralSetting ()
		{
			lock (locker) {
				var x = connection.Table<GenaralSetting> ().Where (c => c.ID == 0);
				return x.FirstOrDefault ();
			}

		}

		public int ISgenaralSetting ()
		{
			lock (locker) { 
				return connection.Table<GenaralSetting> ().Select (a => a.Name).Count ();
			}

		}
	}
}