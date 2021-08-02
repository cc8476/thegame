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


    // Start is called before the first frame update
    void Start()
    {
    }

    public void render(int skillId)
    {
        this.skillId = skillId;
        skillStruct sk = skillTable.Instance.getDataById(skillId);
        transform.Find("Canvas/nameTxt").GetComponent<Text>().text = sk.name.ToString();
        transform.Find("Canvas/attTxt").GetComponent<Text>().text = "攻击:"+sk.att.ToString();
        transform.Find("Canvas/desTxt").GetComponent<Text>().text = "描述:"+sk.des.ToString();

        transform.Find("Canvas/icon").GetComponent<Image>().sprite = ImageTool.LoadSpriteByIO(Application.streamingAssetsPath + sk.icon);

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
    }


    private void setOutline(bool show)
    {

        //transform.Find("Canvas/bodyImg").GetComponent<Outline>().enabled = show;
    }


    // Update is called once per frame
    void Update()
    {
    }
}
