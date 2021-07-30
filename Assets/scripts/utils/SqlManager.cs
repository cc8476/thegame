using Mono.Data.Sqlite;
using UnityEngine;


public class SqlManager
{
    static SqlManager _instance;

    private SqliteConnection SqlConnection;
    internal SqliteCommand SqlCommand;
    internal SqliteDataReader SqlReader;
    
    public SqlManager()
    {
        //连接数据库
        SqlConnection = new SqliteConnection("data source=" + Application.streamingAssetsPath + "/database.db");
        SqlConnection.Open();
        SqlCommand = SqlConnection.CreateCommand();

    }







    




    static public SqlManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SqlManager();
            }
            return _instance;
        }
    }

    public SqliteDataReader SqlDataReader { get; private set; }
}

