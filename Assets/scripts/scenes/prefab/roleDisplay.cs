using System;
using System.Collections;
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
    public Image body;

    public int roleId;
    [Tooltip("0=role , 1=enemy")]
    public int chartype;



    public int status;//1= 当前是攻击者 0=什么都不是

    // Start is called before the first frame update
    void OnEnable()
    {
        arrow = transform.Find("Canvas/arrow").GetComponent<Image>();
        arrow.color = new Color(255, 255, 255, 0);
        transform.Find("Canvas/bodyImg").GetComponent<Outline>().enabled = false;


        body = transform.Find("Canvas/bodyImg").GetComponent<Image>();

    }

    public void render(RoleStruct role, int chartype)
    {
        Debug.Log("zzzzzzzz" + JsonUtility.ToJson(role));

        transform.Find("Canvas/nameTxt").GetComponent<Text>().text = role.name;
        transform.Find("Canvas/hpTxt").GetComponent<Text>().text = role.curhp.ToString() +"/"+role.hp.ToString();
        body.sprite = ImageTool.LoadSpriteByIO(Application.streamingAssetsPath + role.bodypic);



        roleId = role.id;//
        this.chartype = chartype;//

        if(role.curhp<=0) {
            Material mat = Resources.Load<Material>("material/greyEffect");
            transform.Find("Canvas/bodyImg").GetComponent<Image>().material = mat;
        }


    }

    //被攻击，找一个更好看的shader
    public IEnumerator beAttack(float loseHP, Action<int> taskCompletedCallBack)
    {
        Debug.Log("beAttack:"+loseHP);
        RoleStruct r = new RoleStruct();
        if (this.chartype==0)
        {
          r = roleTable.Instance.loseHP(loseHP, this.roleId);
        }
        else
        {
            r = enemyTable.Instance.loseHP(loseHP, this.roleId);
        }

        //攻击动画
        GameObject instance = (GameObject)Instantiate(Resources.Load("attackAct"));
        instance.transform.parent = body.transform;
        instance.transform.position = body.transform.position;

        attackAct pane = (attackAct)instance.GetComponent(typeof(attackAct));
        pane.render("attack", 8);
        //攻击动画


        yield return new WaitForSeconds(1);  //
        Debug.Log("当前损失：" + loseHP);
        Material mat = Resources.Load<Material>("material/blurEffect");
        transform.Find("Canvas/bodyImg").GetComponent<Image>().material = mat;

        this.render(r, this.chartype);
        taskCompletedCallBack(r.curhp);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        setOutline(true);

        this.showRolePane();
    }

    private void showRolePane()
    {
        // 触发事件
        int[] data = { this.roleId, this.chartype };
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

    //被攻击
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

    //准备阶段
    internal void ready()
    {
        this.status = 1;
        this.renderStatus();
        this.autoAttack();
    }

    //敌人，触发自动攻击
    private void autoAttack()
    {

        if(this.chartype==1) {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ATTACK_AUTO), null);
        }

    }

    internal void notReady()
    {
        this.status = 0;
        this.renderStatus();
    }

    internal void renderStatus()
    {
        switch (this.status)
        {
            case 1:
                arrow.color = new Color(255, 255, 255, 255);
                setOutline(true);
                this.showRolePane();

                break;
            case 0:
                arrow.color = new Color(255, 255, 255, 0);
                setOutline(false);
                break;
        }

    }
}
