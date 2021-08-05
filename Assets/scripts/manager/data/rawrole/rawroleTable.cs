using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;


public class rawroleTable
{

    static rawroleTable _instance;

    private SqliteConnection SqlConnection;
    private SqliteCommand SqlCommand;
    private SqliteDataReader SqlReader;

    //private string tableName = 'RawRole';

    public rawroleTable()
    {
        //连接数据库
        SqlConnection = new SqliteConnection("data source=" + Application.streamingAssetsPath + "/database.db");
        SqlConnection.Open();
        SqlCommand = SqlConnection.CreateCommand();
    }

    public RawRoleStructure getDataById(int id)
    {
        SqlCommand.CommandText = "SELECT * FROM RawRole WHERE id = " + id;
        SqlReader = SqlCommand.ExecuteReader();
        RawRoleStructure result = new RawRoleStructure();

        while (SqlReader.Read())
        {
            result.name = (string)SqlReader["name"];
            result.headpic = (string)SqlReader["headpic"];
            result.bodypic = (string)SqlReader["bodypic"];
            result.skills = (string)SqlReader["skills"];

            result.id = int.Parse(SqlReader["id"].ToString());
            result.critical = int.Parse(SqlReader["critical"].ToString());
            result.def = int.Parse(SqlReader["def"].ToString());
            result.speed = int.Parse(SqlReader["speed"].ToString());
            result.darkres = int.Parse(SqlReader["darkres"].ToString());
            result.lightres = int.Parse(SqlReader["lightres"].ToString());
            result.quality = int.Parse(SqlReader["quality"].ToString());
            result.coin = int.Parse(SqlReader["coin"].ToString());
            result.att = int.Parse(SqlReader["att"].ToString());

        }

        SqlReader.Close();

        return result;
    }


    public List<RawRoleStructure> getAllData()
    {
        //获取全部数据
        SqlCommand.CommandText = "SELECT * FROM RawRole";
        SqlReader = SqlCommand.ExecuteReader();

        List<RawRoleStructure> list = new List<RawRoleStructure>();

        while (SqlReader.Read())
        {
            RawRoleStructure result = new RawRoleStructure();

            result.name = (string)SqlReader["name"];
            result.headpic = (string)SqlReader["headpic"];
            result.bodypic = (string)SqlReader["bodypic"];
            result.skills = (string)SqlReader["skills"];

            result.id = int.Parse(SqlReader["id"].ToString());
            result.critical = int.Parse(SqlReader["critical"].ToString());
            result.def = int.Parse(SqlReader["def"].ToString());
            result.speed = int.Parse(SqlReader["speed"].ToString());
            result.darkres = int.Parse(SqlReader["darkres"].ToString());
            result.lightres = int.Parse(SqlReader["lightres"].ToString());
            result.quality = int.Parse(SqlReader["quality"].ToString());
            result.coin = int.Parse(SqlReader["coin"].ToString());
            result.att = int.Parse(SqlReader["att"].ToString());

            list.Add(result);

        }

        SqlReader.Close();
        return list;
    }

    static public rawroleTable Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new rawroleTable();
            }
            return _instance;
        }
    }


}
