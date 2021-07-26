using System.Collections;
using manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eventManager
{
    public static ArrayList list;

    public static eventStructure currentEvent;
    public static void init()
    {
        list = new ArrayList();
        eventStructure e1 = new eventStructure();
        e1.addHero("奖励英雄1",-1,-1,0);

        eventStructure e2 = new eventStructure();
        e2.addHero("奖励英雄2", -1, -1, 1);

        eventStructure e3 = new eventStructure();
        e3.addHero("奖励英雄3", 1, 1, 0);
        eventStructure e4 = new eventStructure();
        e4.addCoin("奖励金币", -1, -1, 100);
        eventStructure e41 = new eventStructure();
        e41.addItem("奖励道具", -1, -1, 0);

        eventStructure e5 = new eventStructure();
        e5.addGuide("开始战斗", -1, -1, "点击地图按钮，开始启程了！！！");

        list.Add(e1);
        list.Add(e2);
        list.Add(e3);
        list.Add(e4);
        list.Add(e41);
        list.Add(e5);
        Debug.Log("eventManager.list 完成初始化 " + eventManager.list.Count);
    }


    public static void showEventPane(eventStructure e) {
        currentEvent = e;
        if (SceneManager.GetSceneByName(Scene.EventPane).isLoaded == false) {
            SceneManager.LoadScene(Scene.EventPane, LoadSceneMode.Additive);
        }
        //怎么获取scene ,然后设置面板？
    }

    public static void checkEvents()
    {
        //检查是否触发事件
        Debug.Log("checkEvents " + eventManager.list.Count);
        foreach (eventStructure e in eventManager.list)
        {
            //轮次和波次是否触发事件
            if (e.turn == GameManager.Instance.turn && e.wave == GameManager.Instance.wave)
            {

                //增加角色
                if (e.type == eventStructureType.Role)
                {
                    RoleStruct r = new RoleStruct();
                    r.getFromRow(e.roleIdSending);
                    GameManager.Instance.addRole(r);

                    list.Remove(e);
                    showEventPane(e);
                    return;


                }
                //增加金币
                else if (e.type ==eventStructureType.Coin)
                {
                    GameManager.Instance.addCoin(e.coinSending);

                    list.Remove(e);
                    showEventPane(e);
                    return;
                }

                //增加道具
                else if (e.type ==eventStructureType.Item)
                {
                    itemStructure r = itemManager.GetItemByOrder(e.roleIdSending);
                    GameManager.Instance.addItem(r);

                    list.Remove(e);
                    showEventPane(e);
                    return;
                }

                else if (e.type ==eventStructureType.MissionTips)
                {
                    list.Remove(e);
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

}