using System.Collections;
using UnityEngine;

namespace manager
{
    public class RawRoleManager
    {
        public static ArrayList list;//数据库内的固定数据
        public static void init()
        {
            list = new ArrayList();
            RawRoleStructure r1 = new RawRoleStructure();
            r1.setValues("新手英雄1");

            RawRoleStructure r2 = new RawRoleStructure();
            r2.setValues("新手英雄2");

            RawRoleStructure r3 = new RawRoleStructure();
            r3.setValues("新手英雄3");

            list.Add(r1);
            list.Add(r2);
            list.Add(r3);

            Debug.Log("RawRoleManager.list 完成初始化 "+RawRoleManager.list.Count);

        }

        public static RawRoleStructure GetRoleByOrder(int order) {
            return (RawRoleStructure)list[order];
        }

    }

}