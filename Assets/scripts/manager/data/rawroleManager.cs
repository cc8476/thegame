using System.Collections;
using Mono.Data.Sqlite;
using UnityEngine;

namespace manager
{
    public class RawRoleManager
    {
        public static ArrayList list;//数据库内的固定数据
        public static void init()
        {
            list = new ArrayList();

            SqliteDataReader SqlDataReader = SqlManager.Instance.getAllData("RawRole");
            while(SqlDataReader.Read())
            {
                RawRoleStructure result = setValue(SqlDataReader);
                list.Add(result);
            }

            SqlDataReader.Close();


        }

        public static RawRoleStructure setValue(SqliteDataReader SqlDataReader)
        {

            RawRoleStructure result = new RawRoleStructure();

            result.name = (string)SqlDataReader["name"];
            result.headpic = (string)SqlDataReader["headpic"];
            result.Bodypic = (string)SqlDataReader["bodypic"];

            result.id = int.Parse(SqlDataReader["id"].ToString());
            result.critial = int.Parse(SqlDataReader["critial"].ToString());
            result.def = int.Parse(SqlDataReader["def"].ToString());
            result.speed = int.Parse(SqlDataReader["speed"].ToString());
            result.darkres = int.Parse(SqlDataReader["darkres"].ToString());
            result.lightres = int.Parse(SqlDataReader["lightres"].ToString());
            result.quality = int.Parse(SqlDataReader["quality"].ToString());
            result.coin = int.Parse(SqlDataReader["coin"].ToString());
            result.att = int.Parse(SqlDataReader["att"].ToString());


            return result;
        }

        public static RawRoleStructure GetRoleByOrder(int order) {

            SqliteDataReader SqlDataReader = SqlManager.Instance.getDataById("RawRole", order);
            RawRoleStructure result = setValue(SqlDataReader);
            SqlDataReader.Close();

            Debug.Log("GetRoleByOrder"+ order);
            return result;
        }

    }

}