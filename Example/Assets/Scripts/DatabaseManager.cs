using SQLite4Unity3d;
using SQLite4Unity3d.Example;
using UnityEngine;

namespace SQLite4Unity3d
{
	public class DatabaseManager : SingletonMono<DatabaseManager>
	{
		private SQLiteConnection _connection;
		[SerializeField] protected string databaseName;

		private void Awake()
		{
			Connect();
			// remove old table
			_connection.DropTable<Class1>();
			_connection.CreateTable<Class1>();
			var class1Test = new Class1
			{
				Boolean1 = new[] { true, true, false, true },
				String1 = "hello",
				Strings1 = new[] { "hello", "world", "123" },
				Float1 = -9.81f,
				Floats1 = new[] { 3.14f, 2.71f, 999f, 123.456f},
				Floats2 = new[] { 987.654f, -999.999f, 1.5f },
				Ints1 = new[] { 123, 456, 789 },
				Ints2 = new[] { -987, -654, -321, },
				Class2 = new Class2(),
				Vec2s = new[] { Vector2.down, Vector2.left },
				Vec3s = new[] { Vector3.back, Vector3.down }
			};
			// insert
			_connection.Insert(class1Test);
			// get table
			var data = _connection.Table<Class1>();
			foreach (var o in data)
			{
				Debug.Log(o.Id);
				foreach (var v in o.Boolean1)
					Debug.Log(v);
				Debug.Log(o.String1);
				foreach (var v in o.Strings1)
					Debug.Log(v);
				Debug.Log(o.Float1);
				foreach (var v in o.Floats1)
					Debug.Log(v);
				foreach (var v in o.Floats2)
					Debug.Log(v);
				foreach (var v in o.Ints1)
					Debug.Log(v);
				foreach (var v in o.Ints2)
					Debug.Log(v);
				foreach (var v in o.Vec2s)
					Debug.Log(v);
				foreach (var v in o.Vec3s)
					Debug.Log(v);
				Debug.Log(o.Class2);
			}
			_connection.Close();
		}

		private void Connect()
		{
#if UNITY_EDITOR
			var dbPath = $@"Assets/StreamingAssets/Database/{databaseName}";
#else
	        // check if file exists in Application.persistentDataPath
	        var filepath = $"{Application.persistentDataPath}/Database/{databaseName}";
	        if (!File.Exists(filepath))
	        {
	            Debug.Log("Database not in Persistent path");
				var dirpath = System.IO.Path.GetDirectoryName(filepath);
				if (!Directory.Exists(dirpath)) 
					Directory.CreateDirectory(dirpath);
	            // if it doesn't ->
	            // open StreamingAssets directory and load the db ->
#if UNITY_ANDROID 
	           var loadDb =
 new WWW("jar:file://" + Application.dataPath + "!/assets/Database/" + databaseName);  // this is the path to your StreamingAssets in android
	           while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
	            // then save to Application.persistentDataPath
				File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
				var loadDb =
 Application.dataPath + "/Raw/Database/" + databaseName;  // this is the path to your StreamingAssets in iOS
	                                                          // then save to Application.persistentDataPath
				File.Copy(loadDb, filepath);
#elif UNITY_WP8
				var loadDb =
 Application.dataPath + "/StreamingAssets/Database/" + databaseName;  // this is the path to your StreamingAssets in iOS
				// then save to Application.persistentDataPath
				File.Copy(loadDb, filepath);
#elif UNITY_WINRT
				var loadDb =
 Application.dataPath + "/StreamingAssets/Database/" + databaseName;  // this is the path to your StreamingAssets in iOS
				// then save to Application.persistentDataPath
				File.Copy(loadDb, filepath);
#elif UNITY_STANDALONE_OSX
				var loadDb =
 Application.dataPath + "/Resources/Data/StreamingAssets/Database/" + databaseName;  // this is the path to your StreamingAssets in iOS
				// then save to Application.persistentDataPath
				File.Copy(loadDb, filepath);
#else
				var loadDb =
 Application.dataPath + "/StreamingAssets/Database/" + databaseName;  // this is the path to your StreamingAssets in iOS
				// then save to Application.persistentDataPath
				File.Copy(loadDb, filepath);
#endif
	            Debug.Log("Database written");
	        }
	        var dbPath = filepath;
#endif
			_connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite
			                                           | SQLiteOpenFlags.Create);
			Debug.Log("Final PATH: " + dbPath);
		}
	}
}