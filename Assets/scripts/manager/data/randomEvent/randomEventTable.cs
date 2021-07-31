using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;


public class randomEventTable
    {

    static randomEventTable _instance;

    private SqliteConnection SqlConnection;
    private SqliteCommand SqlCommand;
    private SqliteDataReader SqlReader;


        public randomEventTable()
        {
            //连接数据库
            SqlConnection = new SqliteConnection("data source=" + Application.streamingAssetsPath + "/database.db");
            SqlConnection.Open();
            SqlCommand = SqlConnection.CreateCommand();
        }

        public void clearData() {

            SqlCommand.CommandText = "Truncate Table randomEvent";
            SqlReader = SqlCommand.ExecuteReader();
            SqlReader.Close();
        }

        

        public randomStruct getDataById(int id)
        {
            SqlCommand.CommandText = "SELECT * FROM randomEvent WHERE id = " + id;
            SqlReader = SqlCommand.ExecuteReader();
            randomStruct result = new randomStruct();

            while (SqlReader.Read())
            {
                result.name = (string)SqlReader["name"];
                result.des = (string)SqlReader["des"];
                result.pic =   (string)SqlReader["pic"];

                result.id = int.Parse(SqlReader["id"].ToString());
                result.roleId = int.Parse(SqlReader["roleId"].ToString());
                result.coin = int.Parse(SqlReader["coin"].ToString());
                result.itemId = int.Parse(SqlReader["itemId"].ToString());
                result.miracleId = int.Parse(SqlReader["miracleId"].ToString());
                result.weight = int.Parse(SqlReader["weight"].ToString());
                result.limit = int.Parse(SqlReader["limit"].ToString());
                result.limitType = int.Parse(SqlReader["limitType"].ToString());
                result.min = int.Parse(SqlReader["min"].ToString());
                result.max = int.Parse(SqlReader["max"].ToString());
                result.num = int.Parse(SqlReader["num"].ToString());
                result.type = (randomEventType)int.Parse(SqlReader["type"].ToString());
        }

            SqlReader.Close();

            return result;
        }


        public List<randomStruct> getAllData()
        {
            //获取全部数据
            SqlCommand.CommandText = "SELECT * FROM randomEvent";
            SqlReader = SqlCommand.ExecuteReader();

            List<randomStruct> list = new List<randomStruct>();

            while (SqlReader.Read())
            {
            randomStruct result = new randomStruct();

                result.name = (string)SqlReader["name"];
                result.des = (string)SqlReader["des"];
                result.pic =  (string)SqlReader["pic"];

                result.id = int.Parse(SqlReader["id"].ToString());
                result.roleId = int.Parse(SqlReader["roleId"].ToString());
                result.coin = int.Parse(SqlReader["coin"].ToString());
                result.itemId = int.Parse(SqlReader["itemId"].ToString());
                result.miracleId = int.Parse(SqlReader["miracleId"].ToString());
                result.weight = int.Parse(SqlReader["weight"].ToString());
                result.limit = int.Parse(SqlReader["limit"].ToString());
                result.limitType = int.Parse(SqlReader["limitType"].ToString());
                result.min = int.Parse(SqlReader["min"].ToString());
                result.max = int.Parse(SqlReader["max"].ToString());
                result.num = int.Parse(SqlReader["num"].ToString());
                result.type = (randomEventType)int.Parse(SqlReader["type"].ToString());

                list.Add(result);

            }

            SqlReader.Close();
            return list;
        }

    static public randomEventTable Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new randomEventTable();
            }
            return _instance;
        }
    }


}
