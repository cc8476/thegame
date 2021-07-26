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
    }

    public Object getDataById(string tableName,int id)
    {
        //获取全部数据
        SqlCommand.CommandText = "SELECT * FROM "+ tableName + " WHERE id = "+ id;
        SqlDataReader = SqlCommand.ExecuteReader();

        Object result;

        for (int i = 0; i < reader.FieldCount; i++)
        {
            var n = reader.GetName(i).ToLower();
            if (propInfos.ContainsKey(n))
            {
                PropertyInfo prop = propInfos[n];
                var IsValueType = prop.PropertyType.IsValueType;
                object defaultValue = null;//引用类型或可空值类型的默认值
                if (IsValueType)
                {
                    if ((!prop.PropertyType.IsGenericType)
                        || (prop.PropertyType.IsGenericType && !prop.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))))
                    {
                        defaultValue = 0;//非空值类型的默认值
                    }
                }
                var v = reader.GetValue(i);
                prop.SetValue(res, (Convert.IsDBNull(v) ? defaultValue : v), null);
            }
        }





        while (SqlDataReader.Read())//判断数据表中是否含有数据  
        {
            Debug.Log(SqlDataReader[0].ToString() + ",");//输出用户标识


        }
        SqlDataReader.Close();
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

