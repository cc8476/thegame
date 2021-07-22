

using System.Collections;
using manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace manager
{
    public class itemManager
    {
        public static ArrayList list;//数据库内的固定数据
        public static void init()
        {
            list = new ArrayList();
            itemStructure r1 = new itemStructure();
            r1.setValues(0,"新手道具1","1.png");

            itemStructure r2 = new itemStructure();
            r2.setValues(0,"新手道具2","2.png");

            itemStructure r3 = new itemStructure();
            r3.setValues(0,"新手道具3","3.png");

            list.Add(r1);
            list.Add(r2);
            list.Add(r3);

            Debug.Log("itemManager.list 完成初始化 "+RawRoleManager.list.Count);

        }

        public static itemStructure GetItemByOrder(int order) {
            return (itemStructure)list[order];
        }

    }

}