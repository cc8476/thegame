using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;


public class itemTable
{

    static itemTable _instance;

    private SqliteConnection SqlConnection;
    private SqliteCommand SqlCommand;
    private SqliteDataReader SqlReader;


    public itemTable()
    {
        //连接数据库
        SqlConnection = new SqliteConnection("data source=" + Application.streamingAssetsPath + "/database.db");
        SqlConnection.Open();
        SqlCommand = SqlConnection.CreateCommand();
    }

    public void clearData()
    {

        SqlCommand.CommandText = "Truncate Table item";
        SqlReader = SqlCommand.ExecuteReader();
        SqlReader.Close();
    }



    public itemStructure getDataById(int id)
    {
        Debug.Log("iiiid" + id);
        SqlCommand.CommandText = "SELECT * FROM item WHERE id = " + id;
        SqlReader = SqlCommand.ExecuteReader();
        itemStructure result = new itemStructure();

        while (SqlReader.Read())
        {
            result.name = (string)SqlReader["name"];
            result.headpic = (string)SqlReader["headpic"];

            result.type = int.Parse(SqlReader["type"].ToString());
            result.id = int.Parse(SqlReader["id"].ToString());
        }

        SqlReader.Close();

        return result;
    }


    public List<itemStructure> getAllData()
    {
        //获取全部数据
        SqlCommand.CommandText = "SELECT * FROM item";
        SqlReader = SqlCommand.ExecuteReader();

        List<itemStructure> list = new List<itemStructure>();

        while (SqlReader.Read())
        {
            itemStructure result = new itemStructure();

            result.name = (string)SqlReader["name"];
            result.type = (int)SqlReader["type"];
            result.headpic = (string)SqlReader["bodypic"];

            result.id = int.Parse(SqlReader["id"].ToString());

            list.Add(result);

        }

        SqlReader.Close();
        return list;
    }

    static public itemTable Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new itemTable();
            }
            return _instance;
        }
    }


}
