using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;


public class eventTable
    {

    static eventTable _instance;

    private SqliteConnection SqlConnection;
    private SqliteCommand SqlCommand;
    private SqliteDataReader SqlReader;


        public eventTable()
        {
            //连接数据库
            SqlConnection = new SqliteConnection("data source=" + Application.streamingAssetsPath + "/database.db");
            SqlConnection.Open();
            SqlCommand = SqlConnection.CreateCommand();
        }

        public eventStruct getDataById(int id)
        {
            SqlCommand.CommandText = "SELECT * FROM event WHERE id = " + id;
            SqlReader = SqlCommand.ExecuteReader();
            eventStruct result = new eventStruct();

            while (SqlReader.Read())
            {
                result.name = (string)SqlReader["name"];
                result.des = (string)SqlReader["des"];
                result.pic = (string)SqlReader["pic"];

                result.id = int.Parse(SqlReader["id"].ToString());
                result.turn = int.Parse(SqlReader["turn"].ToString());
                result.wave = int.Parse(SqlReader["wave"].ToString());
                result.roleIdSending = int.Parse(SqlReader["roleIdSending"].ToString());
                result.coinSending = int.Parse(SqlReader["coinSending"].ToString());
                result.itemIdSending = int.Parse(SqlReader["itemIdSending"].ToString());
                result.miracleIdSending = int.Parse(SqlReader["miracleIdSending"].ToString());
                result.type = (eventStructType)int.Parse(SqlReader["type"].ToString());

        }

            SqlReader.Close();

            return result;
        }



        public List<eventStruct> getAllData()
        {
            //获取全部数据
            SqlCommand.CommandText = "SELECT * FROM event";
            SqlReader = SqlCommand.ExecuteReader();

            List<eventStruct> list = new List<eventStruct>();

            while (SqlReader.Read())
            {
                eventStruct result = new eventStruct();

                result.name = (string)SqlReader["name"];
                result.des = (string)SqlReader["des"];
                result.pic = (string)SqlReader["pic"];

                result.id = int.Parse(SqlReader["id"].ToString());
                result.turn = int.Parse(SqlReader["turn"].ToString());
                result.wave = int.Parse(SqlReader["wave"].ToString());
                result.roleIdSending = int.Parse(SqlReader["roleIdSending"].ToString());
                result.coinSending = int.Parse(SqlReader["coinSending"].ToString());
                result.itemIdSending = int.Parse(SqlReader["itemIdSending"].ToString());
                result.miracleIdSending = int.Parse(SqlReader["miracleIdSending"].ToString());
                result.type = (eventStructType)int.Parse(SqlReader["type"].ToString());


            list.Add(result);

            }

            SqlReader.Close();
            return list;
        }

    static public eventTable Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new eventTable();
            }
            return _instance;
        }
    }


}
