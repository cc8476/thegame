using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;


public class enemyTable
    {

    static enemyTable _instance;

    private SqliteConnection SqlConnection;
    private SqliteCommand SqlCommand;
    private SqliteDataReader SqlReader;


        public enemyTable()
        {
            //连接数据库
            SqlConnection = new SqliteConnection("data source=" + Application.streamingAssetsPath + "/database.db");
            SqlConnection.Open();
            SqlCommand = SqlConnection.CreateCommand();
        }

        public enemyStruct getDataById(int id)
        {
            SqlCommand.CommandText = "SELECT * FROM enemy WHERE id = " + id;
            SqlReader = SqlCommand.ExecuteReader();
            enemyStruct result = new enemyStruct();

            while (SqlReader.Read())
            {
                result.name = (string)SqlReader["name"];
                result.headpic =  (string)SqlReader["headpic"];
                result.bodypic =  (string)SqlReader["bodypic"];

                result.id = int.Parse(SqlReader["id"].ToString());
                result.critical = int.Parse(SqlReader["critical"].ToString());
                result.def = int.Parse(SqlReader["def"].ToString());
                result.speed = int.Parse(SqlReader["speed"].ToString());
                result.darkres = int.Parse(SqlReader["darkres"].ToString());
                result.lightres = int.Parse(SqlReader["lightres"].ToString());
                result.quality = int.Parse(SqlReader["quality"].ToString());
                result.coin = int.Parse(SqlReader["coin"].ToString());
                result.att = int.Parse(SqlReader["att"].ToString());

                result.turnMin = int.Parse(SqlReader["turnMin"].ToString());
                result.turnMax = int.Parse(SqlReader["turnMax"].ToString());

            }

            SqlReader.Close();

            return result;
        }



        public List<enemyStruct> getAllData()
        {
            //获取全部数据
            SqlCommand.CommandText = "SELECT * FROM enemy";
            SqlReader = SqlCommand.ExecuteReader();

            List<enemyStruct> list = new List<enemyStruct>();

            while (SqlReader.Read())
            {
                enemyStruct result = new enemyStruct();

                result.name = (string)SqlReader["name"];
                result.headpic = (string)SqlReader["headpic"];
                result.bodypic = (string)SqlReader["bodypic"];

                result.id = int.Parse(SqlReader["id"].ToString());
                result.critical = int.Parse(SqlReader["critical"].ToString());
                result.def = int.Parse(SqlReader["def"].ToString());
                result.speed = int.Parse(SqlReader["speed"].ToString());
                result.darkres = int.Parse(SqlReader["darkres"].ToString());
                result.lightres = int.Parse(SqlReader["lightres"].ToString());
                result.quality = int.Parse(SqlReader["quality"].ToString());
                result.coin = int.Parse(SqlReader["coin"].ToString());
                result.att = int.Parse(SqlReader["att"].ToString());

                result.turnMin = int.Parse(SqlReader["turnMin"].ToString());
                result.turnMax = int.Parse(SqlReader["turnMax"].ToString());

                list.Add(result);

            }

            SqlReader.Close();
            return list;
        }

    static public enemyTable Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new enemyTable();
            }
            return _instance;
        }
    }


}
