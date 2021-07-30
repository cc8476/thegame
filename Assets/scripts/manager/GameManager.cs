
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace manager
{
    public class GameManager : MonoBehaviour
    {

        static GameManager _instance;
        public int coin = 0;//金币
        public int level = 1;//等级
        public int turn =-1;//当前轮次
        public int wave =-1;//当前波次
        public  List<int> itemList;//道具列表
        public List<int> enemyList;//当前波次的敌人列表


        
        public void init()
        {
            //本场游戏,总的初始化
            Debug.Log("GameManager 初始化");
            itemList = new List<int>();
            enemyList = new List<int>();
            eventManager.init();
            randomEventManager.init();
            roleTable.Instance.clearData();

        }

        public void addCoin(int coin)
        {
            Debug.Log("游戏获得了coin: "+coin);
            GameManager.Instance.coin +=coin;
            SaveBin();
        }

        public void setCurrentEnemyList() {
            int currentTurn = GameManager.Instance.turn;
            int count = 0;

            List<enemyStruct> eList = enemyTable.Instance.getAllData();
            foreach (enemyStruct item in eList)
            {
                if (count == 3) break;
                if(item.turnMin<= currentTurn  && item.turnMax >= currentTurn)
                {
                    GameManager.Instance.enemyList.Add(item.id);
                    count++;
                }
            }

        }



        public void addItem(int itemId)
        {
            GameManager.Instance.itemList.Add(itemId);
            SaveBin();
        }
        

        //保存存档
        public void SaveBin() {
            string path = Application.dataPath + "/save.bin"; 
            FileStream file = null;
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(path, FileMode.Create);

            SaveData sd = new SaveData();
            sd.coin = GameManager.Instance.coin;
            sd.level = GameManager.Instance.level;
            sd.turn = GameManager.Instance.turn;
            sd.wave = GameManager.Instance.wave;
            sd.itemList = GameManager.Instance.itemList;
            sd.enemyList = GameManager.Instance.enemyList;

            bf.Serialize(file, sd);
            file.Close();
        }
        //读取存档
        public void ReadBin() {
            string path = Application.dataPath + "/save.bin"; 
            SaveData sd ;
            FileStream file = null;
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(path, FileMode.Open);
            sd = (SaveData)bf.Deserialize(file);
            Debug.Log("读取日志 金币  "+sd.coin);
            Debug.Log("读取日志 等级  "+sd.level);
            Debug.Log("读取日志 turn  "+sd.turn);
            Debug.Log("读取日志 wave  "+sd.wave);

             GameManager.Instance.coin = sd.coin;
             GameManager.Instance.level = sd.level;
             GameManager.Instance.turn = sd.turn ;
             GameManager.Instance.wave = sd.wave;
             GameManager.Instance.itemList = sd.itemList;
             GameManager.Instance.enemyList = sd.enemyList;



            

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





