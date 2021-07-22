using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using manager;

public class EventPane : MonoBehaviour
{

    private Button closeBtn;//关闭按钮
    private Button confirmBtn;//确认按钮
    private Text title;//标题
    private Text des;//描述
    private Image pic;//头像


    
    void Start()
    {
        closeBtn = GameObject.Find("Canvas/closeBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(closeFunc);

        confirmBtn = GameObject.Find("Canvas/confirmBtn").GetComponent<Button>();
        confirmBtn.onClick.AddListener(closeFunc);

        title = GameObject.Find("Canvas/title").GetComponent<Text>();
        des = GameObject.Find("Canvas/des").GetComponent<Text>();

        pic = GameObject.Find("Canvas/pic").GetComponent<Image>();
        pic.sprite = null;

        Debug.Log("EventPane e.name "+eventManager.currentEvent.name);
        Debug.Log("EventPane e.des "+eventManager.currentEvent.des);

        title.text = eventManager.currentEvent.name;
        des.text = eventManager.currentEvent.des;


        //奖励是role
        if(eventManager.currentEvent.type==eventStructureType.Role) {
            RawRoleStructure role = RawRoleManager.GetRoleByOrder(eventManager.currentEvent.roleIdSending);
            string headpic =role.headpic;
            Debug.Log("Application.streamingAssetsPath "+Application.streamingAssetsPath);
            Sprite sp  = ImageTool.LoadSpriteByIO( Application.streamingAssetsPath + headpic);

            pic.sprite = sp;
        }
        else if(eventManager.currentEvent.type==eventStructureType.Item) {
            itemStructure item = itemManager.GetItemByOrder(eventManager.currentEvent.itemSending);
            string headpic =item.headpic;
            Sprite sp  = ImageTool.LoadSpriteByIO( Application.streamingAssetsPath + headpic);

            pic.sprite = sp;
        }

    }

    void closeFunc() {
        SceneManager.UnloadSceneAsync("EventPane");

        eventManager.checkEvents();
    }


}
