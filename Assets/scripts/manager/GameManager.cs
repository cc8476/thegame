
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace manager
{
    public class GameManager : MonoBehaviour
    {

        static GameManager _instance;
        public int coin = 0;//金币
        public int level = 1;//等级
        public int turn = -1;//当前轮次
        public int wave = -1;//当前波次
        public List<int> itemList;//道具列表
        public List<int> enemyList;//当前波次的敌人列表
        public eventStruct currentEvent;//当前的事件奖励
        public int currentEventId;//当前的事件奖励Id (奖励过的不再查询)


        public void init()
        {
            //本场游戏,总的初始化
            Debug.Log("GameManager 初始化");
            itemList = new List<int>();
            enemyList = new List<int>();
            roleTable.Instance.clearData();

        }

        public void addCoin(int coin)
        {
            Debug.Log("游戏获得了coin: " + coin);
            GameManager.Instance.coin += coin;
            SaveBin();
        }

        public void setCurrentEnemyList()
        {
            enemyTable.Instance.clearData();


            int currentTurn = GameManager.Instance.turn;
            int count = 0;

            List<enemyStruct> eList = rawEnemyTable.Instance.getAllData();

            //现在是从enmey table获取id[1,2,3]
            //然后从enmey table去取

            //改成从rawEnemy中获取id[1,2,3]
            //把[1,2,3]的记录 insert 到enemy table
            //然后得到list [4,5,6]

            foreach (enemyStruct item in eList)
            {
                if (count == 3) break;
                if (item.turnMin <= currentTurn && item.turnMax >= currentTurn)
                {
                    int id = enemyTable.Instance.insert(item);


                    GameManager.Instance.enemyList.Add(id);
                    count++;
                }
            }

        }



        public void addItem(int itemId)
        {
            Debug.Log("addItem::::::::::::::::::::" + itemId);
            GameManager.Instance.itemList.Add(itemId);
            SaveBin();
        }


        //保存存档
        public void SaveBin()
        {
            string path = Application.dataPath + "/save.bin";
            FileStream file = null;
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(path, FileMode.Create);

            SaveData sd = new SaveData();
            sd.coin = GameManager.Instance.coin;
            sd.level = GameManager.Instance.level;
            sd.currentEventId = GameManager.Instance.currentEventId;
            sd.turn = GameManager.Instance.turn;
            sd.wave = GameManager.Instance.wave;
            sd.itemList = GameManager.Instance.itemList;
            sd.enemyList = GameManager.Instance.enemyList;

            bf.Serialize(file, sd);
            file.Close();
        }
        //读取存档
        public void ReadBin()
        {
            string path = Application.dataPath + "/save.bin";
            SaveData sd;
            FileStream file = null;
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(path, FileMode.Open);
            sd = (SaveData)bf.Deserialize(file);
            Debug.Log("读取日志 金币  " + sd.coin);
            Debug.Log("读取日志 等级  " + sd.level);
            Debug.Log("读取日志 turn  " + sd.turn);
            Debug.Log("读取日志 wave  " + sd.wave);

            GameManager.Instance.coin = sd.coin;
            GameManager.Instance.level = sd.level;
            GameManager.Instance.turn = sd.turn;
            GameManager.Instance.wave = sd.wave;
            GameManager.Instance.itemList = sd.itemList;
            GameManager.Instance.enemyList = sd.enemyList;
            GameManager.Instance.currentEventId = sd.currentEventId;
        }

        public void clearBin()
        {
            string path = Application.dataPath + "/save.bin";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
        }

        //检查是否有存档
        public bool checkBin()
        {
            string path = Application.dataPath + "/save.bin";
            if (File.Exists(path))
            {
                Debug.Log("有存档？？");
                return true;
            }
            else
            {
                Debug.Log("无存档？？");
                return false;
            }
        }



        public void showEventPane(eventStruct e)
        {
            currentEvent = e;
            if (SceneManager.GetSceneByName(Scene.EventPane).isLoaded == false)
            {
                SceneManager.LoadScene(Scene.EventPane, LoadSceneMode.Additive);
            }
            //怎么获取scene ,然后设置面板？
        }

        public void getRandomEvent()
        {

            //ps:如果以后要筛选list,那么就是把list先筛出来之后，进行后续的步骤

            //获取list的所有weight总和
            //得到[0,weight中的一个数] randomValue
            //循环列表，判断当前 总weight+当前weight >randomValue
            //确认得到list中的某个事件
            int allWeight = 0;//总的weight
            List<randomStruct> list = randomEventTable.Instance.getAllData();
            randomStruct goalItem = new randomStruct();//目标数据
            foreach (randomStruct item in list)
            {
                Debug.Log("getEvent....item.weight" + item.weight);
                allWeight += item.weight;
            }
            int randomValue = Random.Range(0, allWeight);

            Debug.Log("getEvent....allWeight" + allWeight);
            Debug.Log("getEvent....randomValue" + randomValue);
            int currentAllWeight = 0;//迭代中的总重量
            foreach (randomStruct item in list)
            {
                if (currentAllWeight + item.weight > randomValue)
                {
                    goalItem = item;
                    break;
                }
                currentAllWeight += item.weight;
            }

            //处理目标数据，添加到GameManager
            Debug.Log("goalItem");
            eventStruct showEvent = new eventStruct();
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

        public void checkEvents()
        {
            //检查是否触发事件
            List<eventStruct> list = eventTable.Instance.getAllData();
            foreach (eventStruct e in list)
            {
                //轮次和波次是否触发事件 ,并且id需要大于记录的历史id
                if (e.turn == GameManager.Instance.turn && e.wave == GameManager.Instance.wave
                    && e.id > this.currentEventId
                        )
                {

                    //增加角色
                    if (e.type == eventStructType.Role)
                    {
                        roleTable.Instance.insertByRawRoleId(e.roleIdSending);

                        this.currentEventId = e.id;
                        showEventPane(e);
                        return;


                    }
                    //增加金币
                    else if (e.type == eventStructType.Coin)
                    {
                        GameManager.Instance.addCoin(e.coinSending);

                        currentEventId = e.id;
                        showEventPane(e);
                        return;
                    }

                    //增加道具
                    else if (e.type == eventStructType.Item)
                    {

                        GameManager.Instance.addItem(e.itemIdSending);

                        currentEventId = e.id;
                        showEventPane(e);
                        return;
                    }

                    else if (e.type == eventStructType.MissionTips)
                    {
                        currentEventId = e.id;
                        showEventPane(e);

                        //初始化游戏后，turn和wave要从-1，-1改成0，0
                        if (GameManager.Instance.wave == -1 && GameManager.Instance.turn == -1)
                        {
                            //TODO:: 应该每次修改GameManager的属性，自动调用SaveBin();
                            GameManager.Instance.turn = 0;
                            GameManager.Instance.wave = 0;
                            GameManager.Instance.SaveBin();

                            Debug.Log("初始化游戏后，turn和wave要从-1，-1改成0，0");
                        }
                        return;
                    }

                }
            }
        }


        static public GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // 尝试寻找该类的实例。此处不能用GameObject.Find，因为MonoBehaviour继承自Component。
                    _instance = Object.FindObjectOfType(typeof(GameManager)) as GameManager;

                    if (_instance == null)  // 如果没有找到
                    {
                        GameObject go = new GameObject("_GameManager"); // 创建一个新的GameObject

                        DontDestroyOnLoad(go);  // 防止被销毁
                        _instance = go.AddComponent<GameManager>(); // 将实例挂载到GameObject上
                    }
                }
                return _instance;
            }
        }


    }


}





