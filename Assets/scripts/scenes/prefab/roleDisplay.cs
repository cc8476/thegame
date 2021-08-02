using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//战斗场景中的人物形象展示
public class roleDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{

    public Text nameTxt;//姓名显示ui
    public Text hpTxt;//血量显示ui

    public int roldId;

    
    // Start is called before the first frame update
    void Start()
    {
        //transform.Find("Canvas/bodyImg").GetComponent<Image>().enabled = false;
    }

    public void render(RoleStruct role)
    {
        Debug.Log("zzzzzzzz" + JsonUtility.ToJson(role));

        transform.Find("Canvas/nameTxt").GetComponent<Text>().text = role.name;
        transform.Find("Canvas/hpTxt").GetComponent<Text>().text = role.hp.ToString();
        transform.Find("Canvas/bodyImg").GetComponent<Image>().sprite = ImageTool.LoadSpriteByIO(Application.streamingAssetsPath + role.bodypic);

        roldId = role.id;//
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        setOutline(true);

        // 触发事件
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.HOVER_ROLE, roldId), this);



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
    }


    private void setOutline(bool show)
    {

        transform.Find("Canvas/bodyImg").GetComponent<Outline>().enabled = show;
    }


    // Update is called once per frame
    void Update()
    {
    }
}
