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
namespace App1
{
	public class CheckInOutDatabase {


		private SQLiteConnection connection;
		static object locker = new object ();
		public CheckInOutDatabase ()
		{	try {
				lock (locker) {
					GetConnetionDatabase a = new GetConnetionDatabase ();
					connection = a.GetConnection ();
					connection.CreateTable<CheckInOut> ();
				}


			} catch (SQLiteException ex) {
				// return ex.Message;
			}
		}
		public int  SaveParkingStatus (CheckInOut Check)
		{
			lock (locker) {
				if (Check.IsCheckout != false) {
					connection.Update (Check);
					return Check.ID;
				} else {

					connection.Insert (Check);
					return Check.ID;
				}

			}

		}
		public CheckInOut GetParkingStatus (int ID)
		{
			lock (locker) {
				var x = connection.Table<CheckInOut>().Where (c => c.ID == ID);
				return x.FirstOrDefault ();
			}

		}
	}
}

