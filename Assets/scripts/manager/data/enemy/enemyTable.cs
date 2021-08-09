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

    public void clearData()
    {
        SqlCommand.CommandText = "DELETE FROM enemy";
        SqlReader = SqlCommand.ExecuteReader();
        
        SqlReader.Close();
    }

    public int insert(enemyStruct r)
    {
        

        string sqlQuery = string.Format(
            "insert into enemy (hp, att, critical,def,speed,darkres,lightres,quality," +
            "name,headpic,bodypic,coin,rawRoleId,status,curhp,skills,turnMin,turnMax) values" +
            " (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\")",
            r.hp, r.att, r.critical, r.def, r.speed, r.darkres, r.lightres, r.quality, r.name, r.headpic, r.bodypic, r.coin, r.id, 1, r.hp, r.skills,r.turnMin,r.turnMax);// table name


        Debug.Log("sql:::" + sqlQuery);

        SqlCommand.CommandText = sqlQuery;
        SqlReader = SqlCommand.ExecuteReader();
        SqlReader.Close();
        SqlCommand.CommandText = "select last_insert_rowid() from enemy";
        SqlReader = SqlCommand.ExecuteReader();
        int id = -1;
        while (SqlReader.Read())
        {
            id = SqlReader.GetInt32(0);
        }

        SqlReader.Close();

        return id;
    }

    public enemyStruct getDataById(int id)
    {
        SqlCommand.CommandText = "SELECT * FROM enemy WHERE id = " + id;
        SqlReader = SqlCommand.ExecuteReader();
        enemyStruct result = new enemyStruct();

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
            result.hp = int.Parse(SqlReader["hp"].ToString());

            result.curhp = int.Parse(SqlReader["curhp"].ToString());



            result.turnMin = int.Parse(SqlReader["turnMin"].ToString());
            result.turnMax = int.Parse(SqlReader["turnMax"].ToString());


        }

        SqlReader.Close();

        return result;
    }


    public enemyStruct loseHP(float lostHp, int roleId)
    {
        enemyStruct role = getDataById(roleId);
        int curHp = role.curhp - (int)lostHp;

        string command = "update enemy set curhp = " + curHp + " where id=" + roleId;
        SqlCommand.CommandText = command;
        Debug.Log("command:" + command);
        SqlReader = SqlCommand.ExecuteReader();
        SqlReader.Close();

        return getDataById(roleId);
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
            result.hp = int.Parse(SqlReader["hp"].ToString());
            result.curhp = int.Parse(SqlReader["curhp"].ToString());

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
