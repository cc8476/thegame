using System;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//战斗场景中的人物形象展示
public class roleDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{

    public Text nameTxt;//姓名显示ui
    public Text hpTxt;//血量显示ui
    public Image arrow;

    public int roldId;
    public int chartype;

    public int status;//1= 当前是攻击者

    // Start is called before the first frame update
    void OnEnable()
    {
        arrow = transform.Find("Canvas/arrow").GetComponent<Image>();
        arrow.color = new Color(255, 255, 255, 0);
        transform.Find("Canvas/bodyImg").GetComponent<Outline>().enabled = false;


    }

    public void render(RoleStruct role, int chartype)
    {
        //chartype =0  role , =1 enemy
        Debug.Log("zzzzzzzz" + JsonUtility.ToJson(role));

        transform.Find("Canvas/nameTxt").GetComponent<Text>().text = role.name;
        transform.Find("Canvas/hpTxt").GetComponent<Text>().text = role.hp.ToString();
        transform.Find("Canvas/bodyImg").GetComponent<Image>().sprite = ImageTool.LoadSpriteByIO(Application.streamingAssetsPath + role.bodypic);



        roldId = role.id;//
        this.chartype = chartype;//
    }

    //被攻击，找一个更好看的shader
    public void beAttack()
    {
        Material mat = Resources.Load<Material>("material/blurEffect");
        transform.Find("Canvas/bodyImg").GetComponent<Image>().material = mat;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        setOutline(true);

        // 触发事件
        int[] data = { this.roldId, this.chartype };
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.HOVER_ROLE, data), this);



    }
    public void OnPointerExit(PointerEventData eventData)
    {
        setOutline(false);
        Debug.Log("exit");
    }
    //按下就触发
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
    }

    //鼠标点击完成（按下，抬起）触发 ，如果按下的时候在别的地方抬起，则不会触发
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ATTACK_ROLE), this);
    }


    private void setOutline(bool show)
    {
        transform.Find("Canvas/bodyImg").GetComponent<Outline>().enabled = show;
    }


    // Update is called once per frame
    void Update()
    {
    }

    internal void ready()
    {
        this.status = 1;
        this.renderStatus();
    }

    internal void renderStatus()
    {
        switch (this.status)
        {
            case 1:
                arrow.color = new Color(255, 255, 255, 255);
                setOutline(true);
                break;
        }

    }
}
