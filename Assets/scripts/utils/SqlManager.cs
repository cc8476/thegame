using System;
using Mono.Data.Sqlite;
using UnityEngine;


public class SqlManager
{
    static SqlManager _instance;

    private SqliteConnection SqlConnection;
    private SqliteCommand SqlCommand;
    private SqliteDataReader SqlDataReader;

    public SqlManager()
    {
        //连接数据库
        SqlConnection = new SqliteConnection("data source=" + Application.streamingAssetsPath + "/database.db");
        SqlConnection.Open();
        SqlCommand = SqlConnection.CreateCommand();
        //插入
        /* INSERT INTO COMPANY (ID,NAME,AGE,ADDRESS,SALARY)
            VALUES (1, 'Paul', 32, 'California', 20000.00 );
        */
        //更新
        /* UPDATE table_name
        SET column1 = value1, column2 = value2...., columnN = valueN
        WHERE [condition]; */
        //删除记录
        /* DELETE FROM table_name
        WHERE [condition]; */
    }

    public SqliteDataReader getDataById(string tableName,int id)
    {
        //获取全部数据
        SqlCommand.CommandText = "SELECT * FROM "+ tableName + " WHERE id = "+ id;
        SqlDataReader = SqlCommand.ExecuteReader();
        return SqlDataReader;


        
    }

    internal void insertData(string tableName, object obj)
    {
        //插入一条数据
        SqlCommand.CommandText = "INSERT INTO "+ tableName + " VALUES (value1,value2,value3,...valueN)";
        SqlDataReader = SqlCommand.ExecuteReader();
        SqlDataReader.Close();
    }

    public SqliteDataReader getAllData(string tableName)
    {
        //获取全部数据
        SqlCommand.CommandText = "SELECT * FROM " + tableName;
        SqlDataReader = SqlCommand.ExecuteReader();
        return SqlDataReader;
    }
    


    public void test()
    {



        //获取全部数据
        SqlCommand.CommandText = "SELECT * FROM table_name WHERE name = 'cc'";
        SqlDataReader = SqlCommand.ExecuteReader();

        Debug.Log("...." + SqlDataReader.FieldCount.ToString());
        while (SqlDataReader.Read())//判断数据表中是否含有数据  
        {
            Debug.Log(SqlDataReader[0].ToString() + ",");//输出用户标识  
        }
        SqlDataReader.Close();

        ////获取单条数据
        //SqlCommand.CommandText = "SELECT * FROM table_name WHERE id = 1";
        //SqlDataReader = SqlCommand.ExecuteReader();

        //Debug.Log("!!!." + SqlDataReader.FieldCount.ToString());
        //while (SqlDataReader.Read())//判断数据表中是否含有数据  
        //{
        //    Debug.Log(SqlDataReader[0].ToString() + ",");//输出用户标识  
        //    Debug.Log(SqlDataReader["name"].ToString());//输出用户密码
        //    Debug.Log(SqlDataReader[1].ToString());//输出用户密码  
        //}
        //SqlDataReader.Close();

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
}

