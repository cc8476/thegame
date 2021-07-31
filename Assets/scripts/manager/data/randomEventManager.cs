using System.Collections;
using manager;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace manager
{

    public class randomEventManager
    {
        public static ArrayList list;
        public static randomStructure currentRandom;
        public static void init()
        {
            list = new ArrayList();
            randomStructure e1 = new randomStructure();
            e1.addHero("得到英雄");

            randomStructure e2 = new randomStructure();
            e2.setItem("得到物品");

            randomStructure e3 = new randomStructure();
            e3.setCoin("得到coin", 50, 100);

            randomStructure e4 = new randomStructure();
            e4.setCoin("失去coin", -100, -10);

            list.Add(e1);
            list.Add(e2);
            list.Add(e3);
            list.Add(e4);
            Debug.Log("randomEventManager.list 完成初始化 ");
        }

        public static void getRandomEvent()
        {

            //ps:如果以后要筛选list,那么就是把list先筛出来之后，进行后续的步骤

            //获取list的所有weight总和
            //得到[0,weight中的一个数] randomValue
            //循环列表，判断当前 总weight+当前weight >randomValue
            //确认得到list中的某个事件
            int allWeight = 0;//总的weight
            randomStructure goalItem = new randomStructure();//目标数据
            foreach (randomStructure item in list)
            {
                Debug.Log("getEvent....item.weight"+item.weight);
                allWeight +=item.weight;
            }
            int randomValue = Random.Range(0, allWeight);

            Debug.Log("getEvent....allWeight"+allWeight);
            Debug.Log("getEvent....randomValue"+randomValue);
            int currentAllWeight = 0;//迭代中的总重量
            foreach (randomStructure item in list)
            {
                if(currentAllWeight+item.weight>randomValue) {
                    goalItem = item;
                    break;
                }
                currentAllWeight +=item.weight;
            }

            //处理目标数据，添加到GameManager
            Debug.Log("goalItem");
            eventStruct showEvent=new eventStruct();
            showEvent.name = goalItem.name;
            showEvent.des = goalItem.des;
            showEvent.pic = goalItem.pic;

            switch (goalItem.type)
            {
                case randomEventType.Coin:
                    int coin = Random.Range(goalItem.min, goalItem.max);
                    GameManager.Instance.addCoin(coin);
                    showEvent.coinSending = coin;


                    break;
                case randomEventType.Role:
                    RoleStruct r = new RoleStruct();
                    int count = rawroleTable.Instance.getAllData().Count;
                    int roleId = Random.Range(0, count);
                    roleTable.Instance.insertByRawRoleId(roleId);
                    showEvent.roleIdSending = roleId;

                    break;
                case randomEventType.Item:
                    int itemId = Random.Range(0, GameManager.Instance.itemList.Count);
                    GameManager.Instance.addItem(itemId);

                    showEvent.itemIdSending = itemId;
                    break;

                    
            }

                GameManager.Instance.showEventPane(showEvent);

        }



        public static void showRandomEvent(randomStructure item)
        {
            currentRandom = item;
            if (SceneManager.GetSceneByName(Scene.randomPane).isLoaded == false) {
                SceneManager.LoadScene(Scene.randomPane, LoadSceneMode.Additive);
            }
        }



    }

}