using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;


public class skillTable
    {

    static skillTable _instance;

    private SqliteConnection SqlConnection;
    private SqliteCommand SqlCommand;
    private SqliteDataReader SqlReader;


        public skillTable()
        {
            //连接数据库
            SqlConnection = new SqliteConnection("data source=" + Application.streamingAssetsPath + "/database.db");
            SqlConnection.Open();
            SqlCommand = SqlConnection.CreateCommand();
        }

        public void clearData() {

            SqlCommand.CommandText = "delete from skill";
            SqlReader = SqlCommand.ExecuteReader();
            SqlReader.Close();
        }


        public skillStruct getDataById(int id)
        {
            SqlCommand.CommandText = "SELECT * FROM skill WHERE id = " + id;
            SqlReader = SqlCommand.ExecuteReader();
            skillStruct result = new skillStruct();

            while (SqlReader.Read())
            {
                result.name = (string)SqlReader["name"];
                result.icon =  (string)SqlReader["icon"];
                result.des = (string)SqlReader["des"];

                result.id = int.Parse(SqlReader["id"].ToString());
                result.att = int.Parse(SqlReader["att"].ToString());
                result.attAll = int.Parse(SqlReader["attAll"].ToString());
                result.attPosition = int.Parse(SqlReader["attPosition"].ToString());
                result.attType = int.Parse(SqlReader["attType"].ToString());
  
        }

            SqlReader.Close();

            return result;
        }


        public List<skillStruct> getAllData()
        {
            //获取全部数据
            SqlCommand.CommandText = "SELECT * FROM skill";
            SqlReader = SqlCommand.ExecuteReader();

            List<skillStruct> list = new List<skillStruct>();

            while (SqlReader.Read())
            {
                skillStruct result = new skillStruct();

                result.name = (string)SqlReader["name"];
                result.icon =  (string)SqlReader["icon"];
                result.des = (string)SqlReader["des"];

                result.id = int.Parse(SqlReader["id"].ToString());
                result.att = int.Parse(SqlReader["att"].ToString());
                result.attAll = int.Parse(SqlReader["attAll"].ToString());
                result.attPosition = int.Parse(SqlReader["attPosition"].ToString());
                result.attType = int.Parse(SqlReader["attType"].ToString());

                list.Add(result);

            }

            SqlReader.Close();
            return list;
        }

    static public skillTable Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new skillTable();
            }
            return _instance;
        }
    }


}
