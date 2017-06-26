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

namespace App1.Model
{
	public class GenaralSetting :IDisposable
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string license { get; set; }

		public void Dispose()
		{ID = 0;
			Name = Email = license = string.Empty;
		}}

}