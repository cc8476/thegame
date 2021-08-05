using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//战斗场景中的人物形象展示
public class skillDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{

    public Text nameTxt;//姓名显示ui
    public Text attTxt;//血量显示ui
    public Text desTxt;//血量显示ui

    public int skillId;

    private int status;//0=未选中 1=选中


    // Start is called before the first frame update
    void Start()
    {
    }

    public void render(int skillId)
    {
        this.skillId = skillId;
        skillStruct sk = skillTable.Instance.getDataById(skillId);
        transform.Find("Canvas/nameTxt").GetComponent<Text>().text = sk.name.ToString();
        transform.Find("Canvas/attTxt").GetComponent<Text>().text = "攻击:" + sk.att.ToString();
        transform.Find("Canvas/desTxt").GetComponent<Text>().text = "描述:" + sk.des.ToString();

        transform.Find("Canvas/icon").GetComponent<Image>().sprite = ImageTool.LoadSpriteByIO(Application.streamingAssetsPath + sk.icon);
        transform.Find("Canvas/icon").GetComponent<Image>().GetComponent<Outline>().enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
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
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.SELECT_SKILL, eventData), this);
    }


    private void setOutline(bool show)
    {

        //transform.Find("Canvas/bodyImg").GetComponent<Outline>().enabled = show;
    }

    public void select()
    {
        this.status = 1;
        this.renderStatus();
    }
    public void unselect()
    {
        this.status = 0;
        this.renderStatus();
    }

    public void renderStatus()
    {
        switch (this.status)
        {
            case 1:
                transform.Find("Canvas/icon").GetComponent<Image>().GetComponent<Outline>().enabled = true;
                break;
            case 0:
                transform.Find("Canvas/icon").GetComponent<Image>().GetComponent<Outline>().enabled = false;
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
