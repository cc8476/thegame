using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;


public class roleTable
    {

    static roleTable _instance;

    private SqliteConnection SqlConnection;
    private SqliteCommand SqlCommand;
    private SqliteDataReader SqlReader;


        public roleTable()
        {
            //连接数据库
            SqlConnection = new SqliteConnection("data source=" + Application.streamingAssetsPath + "/database.db");
            SqlConnection.Open();
            SqlCommand = SqlConnection.CreateCommand();
        }

        public void clearData() {

            SqlCommand.CommandText = "Truncate Table role";
            SqlReader = SqlCommand.ExecuteReader();
            SqlReader.Close();
        }

        public void insertByRawRoleId(int rowroleId)
        {
            RawroleStruct r = rawroleTable.Instance.getDataById(rowroleId);
            string sqlQuery = string.Format(
                "insert into role (hp, att, critical,def,speed,darkres,lightres,quality," +
                "name,headpic,bodypic,coin,rawRoleId,order,status,curhp) values" +
                " (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\")",
                r.hp, r.att,r.critial,r.def,r.speed,r.darkres,r.lightres,r.quality,r.name,r.headpic,r.bodypic,r.coin,r.id,r.order,r.status,r.curhp);// table name

            SqlCommand.CommandText = sqlQuery;
            SqlReader = SqlCommand.ExecuteReader();
            SqlReader.Close();

    }

        public RoleStruct getDataById(int id)
        {
            SqlCommand.CommandText = "SELECT * FROM role WHERE id = " + id;
            SqlReader = SqlCommand.ExecuteReader();
            RoleStruct result = new RoleStruct();

            while (SqlReader.Read())
            {
                result.name = (string)SqlReader["name"];
                result.headpic = (string)SqlReader["headpic"];
                result.bodypic = (string)SqlReader["bodypic"];

                result.id = int.Parse(SqlReader["id"].ToString());
                result.critial = int.Parse(SqlReader["critial"].ToString());
                result.def = int.Parse(SqlReader["def"].ToString());
                result.speed = int.Parse(SqlReader["speed"].ToString());
                result.darkres = int.Parse(SqlReader["darkres"].ToString());
                result.lightres = int.Parse(SqlReader["lightres"].ToString());
                result.quality = int.Parse(SqlReader["quality"].ToString());
                result.coin = int.Parse(SqlReader["coin"].ToString());
                result.att = int.Parse(SqlReader["att"].ToString());


            result.rawRoldId = int.Parse(SqlReader["rawRoldId"].ToString());
            result.order = int.Parse(SqlReader["order"].ToString());
            result.curhp = int.Parse(SqlReader["curhp"].ToString());
            result.status = int.Parse(SqlReader["status"].ToString());

        }

            SqlReader.Close();

            return result;
        }


        public List<RoleStruct> getAllData()
        {
            //获取全部数据
            SqlCommand.CommandText = "SELECT * FROM role";
            SqlReader = SqlCommand.ExecuteReader();

            List<RoleStruct> list = new List<RoleStruct>();

            while (SqlReader.Read())
            {
                RoleStruct result = new RoleStruct();

                result.name = (string)SqlReader["name"];
                result.headpic = (string)SqlReader["headpic"];
                result.bodypic = (string)SqlReader["bodypic"];

                result.id = int.Parse(SqlReader["id"].ToString());
                result.critial = int.Parse(SqlReader["critial"].ToString());
                result.def = int.Parse(SqlReader["def"].ToString());
                result.speed = int.Parse(SqlReader["speed"].ToString());
                result.darkres = int.Parse(SqlReader["darkres"].ToString());
                result.lightres = int.Parse(SqlReader["lightres"].ToString());
                result.quality = int.Parse(SqlReader["quality"].ToString());
                result.coin = int.Parse(SqlReader["coin"].ToString());
                result.att = int.Parse(SqlReader["att"].ToString());

                result.rawRoldId = int.Parse(SqlReader["rawRoldId"].ToString());
                result.order = int.Parse(SqlReader["order"].ToString());
                result.curhp = int.Parse(SqlReader["curhp"].ToString());
                result.status = int.Parse(SqlReader["status"].ToString());

                list.Add(result);

            }

            SqlReader.Close();
            return list;
        }

    static public roleTable Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new roleTable();
            }
            return _instance;
        }
    }


}
