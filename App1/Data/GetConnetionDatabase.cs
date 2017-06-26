using App1.Data;
using System;
using SQLite;
using System.IO;


namespace App1
{

	public class GetConnetionDatabase : ISQLite  {
		public GetConnetionDatabase () {
          //  GetConnection();
        }
 

        public SQLiteConnection GetConnection () {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = Path.Combine(path, "WeParkSQLite");
                var con = new SQLite.SQLiteConnection(path);
                
            return con;
			}


    }


	

}

