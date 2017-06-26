using System;
using SQLite;
namespace App1
{
	public class CheckInOut 
	{
		[PrimaryKey,AutoIncrement]
public int ID {
			get;
			set;
		}

		//public DateTime CheckINData {
		//	get;
		//	set;
		//}
		//public DateTime Checkout {
		//	get;
		//	set;
		//}
		//public decimal duration {
		//	get;
		//	set;
		//}
		public bool IsCheckout {
			get;
			set;
		}
	
	}
}

