using System;
using System.Collections;
using UnityEngine;

namespace manager
{
    public class enemyManager
    {
        public static ArrayList list;//数据库内的固定数据
        public static void init()
        {
            list = new ArrayList();
            enemyStruct r1 = new enemyStruct();
            r1.setValues("新手敌人1");

            enemyStruct r2 = new enemyStruct();
            r2.setValues("新手敌人2");

            enemyStruct r3 = new enemyStruct();
            r3.setValues("新手敌人3");

            enemyStruct r4 = new enemyStruct();
            r4.setValues("新手敌人4");

            enemyStruct r5 = new enemyStruct();
            r5.setValues("新手敌人5");

            list.Add(r1);
            list.Add(r2);
            list.Add(r3);
            list.Add(r4);
            list.Add(r5);

            Debug.Log("enemyManager.list 完成初始化 ");

        }

        public static RawRoleStructure GetRoleByOrder(int order) {
            return (RawRoleStructure)list[order];
        }

        public static void setCurrentList()
        {
            //根据turn的限制，简单取到最前的三个
            int currentTurn = GameManager.Instance.turn;
            int count = 0;

            ArrayList enmeyList = new ArrayList();
            foreach (enemyStruct item in list)
            {
                if (count == 3) break;
                if(item.turnMin<= currentTurn  && item.turnMax >= currentTurn)
                {
                    enmeyList.Add(item);
                    count++;
                }
            }

            GameManager.Instance.enmeyList = enmeyList;

        }
    }

}