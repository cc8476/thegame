
using System.Collections;
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
        public int turn =0;//当前轮次
        public int wave =0;//当前波次
        public  ArrayList roleList;//角色列表
        public  ArrayList itemList;//道具列表
        public ArrayList enmeyList;//当前波次的敌人列表

        
        public void init()
        {
            //本场游戏,总的初始化
            Debug.Log("GameManager 初始化");
            roleList = new ArrayList();
            itemList = new ArrayList();
            RawRoleManager.init();
            eventManager.init();
            itemManager.init();
            randomEventManager.init();
            enemyManager.init();

        }

        public void addRole(RoleStruct r)
        {
            Debug.Log("addRole");
            GameManager.Instance.roleList.Add(r);
            SaveBin();
            
        }
        public void addCoin(int coin)
        {
            Debug.Log("游戏获得了coin: "+coin);
            GameManager.Instance.coin +=coin;
            SaveBin();
        }

        public void addItem(itemStructure r)
        {
            Debug.Log("addItem");
            GameManager.Instance.itemList.Add(r);
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
            sd.roleList = GameManager.Instance.roleList;
            bf.Serialize(file, sd);
            file.Close();
        }
        //检查是否有存档
        public bool checkBin() {
            string path = Application.dataPath + "/save.bin"; 
            if(File.Exists(path)) {
                Debug.Log("有存档？？");
                return true;
            }
            else {
                Debug.Log("无存档？？");
                return false;
            }
        }

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





