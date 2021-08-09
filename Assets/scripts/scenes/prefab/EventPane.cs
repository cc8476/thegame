using UnityEngine;
using UnityEngine.UI;
using manager;

public class EventPane : MonoBehaviour
{

    private Button closeBtn;//关闭按钮
    private Button confirmBtn;//确认按钮
    private Text title;//标题
    private Text des;//描述
    private Image pic;//头像
    private CanvasGroup cg;//

    // 表示Panel正在打开
    public bool _opening;
    // 表示Panel正在关闭
    public bool _closing;


    void OnEnable()
    {
        Debug.Log("OnEnable........");
        closeBtn = GameObject.Find("eventCanvas/closeBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(closeFunc);

        confirmBtn = GameObject.Find("eventCanvas/confirmBtn").GetComponent<Button>();
        confirmBtn.onClick.AddListener(closeFunc);

        cg = GameObject.Find("eventCanvas").GetComponent<CanvasGroup>();
        cg.alpha = 0;

        title = GameObject.Find("eventCanvas/title").GetComponent<Text>();
        des = GameObject.Find("eventCanvas/des").GetComponent<Text>();

        pic = GameObject.Find("eventCanvas/pic").GetComponent<Image>();
        pic.sprite = null;

    }

    public void render(eventStruct currentEvent) {

        _opening = true;
        _closing = false;

        //eventStruct currentEvent = GameManager.Instance.currentEvent;
        Debug.Log("eventPane  render");
        title.text = currentEvent.name;
        des.text = currentEvent.des;


        //奖励是role
        if (currentEvent.type == eventStructType.Role)
        {
            RawRoleStructure role = rawroleTable.Instance.getDataById(currentEvent.roleIdSending);

            string headpic = role.headpic;
            Debug.Log("Application.streamingAssetsPath " + Application.streamingAssetsPath);
            Sprite sp = ImageTool.LoadSpriteByIO(Application.streamingAssetsPath + headpic);
            pic.sprite = sp;
        }
        else if (currentEvent.type == eventStructType.Item)
        {
            itemStructure item = itemTable.Instance.getDataById(currentEvent.itemIdSending);
            string headpic = item.headpic;
            Sprite sp = ImageTool.LoadSpriteByIO(Application.streamingAssetsPath + headpic);
            pic.sprite = sp;
        }
    }

    void closeFunc() {
        Debug.Log("close-eventPane");
        GameManager.Instance.checkEvents(null);
    }

    public void hidden()
    {
        _closing = true;
    }

    void Update()
    {
        // 处于打开状态，alpha随时间增加
        if (_opening == true)
        {
            // 找到Panel的alpha的值，随时间增加，实现淡入效果
            cg.alpha += Time.deltaTime * 4f;

            // 当alpha>=1时，打开状态结束，_opening设置false
            if (cg.alpha >= 1)
                _opening = false;
        }

        if (_closing == true)
        {
            cg.alpha -= Time.deltaTime * 4f;
            if (cg.alpha <= 0)
            {
                _closing = false;
                Destroy(gameObject);
            }
        }
    }


}
