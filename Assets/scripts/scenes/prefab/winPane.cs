using UnityEngine;
using UnityEngine.UI;
using manager;
using UnityEngine.SceneManagement;

public class winPane : MonoBehaviour
{

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

        confirmBtn = GameObject.Find("winCanvas/confirmBtn").GetComponent<Button>();
        confirmBtn.onClick.AddListener(closeFunc);

        cg = GameObject.Find("winCanvas").GetComponent<CanvasGroup>();
        cg.alpha = 0;

        title = GameObject.Find("winCanvas/title").GetComponent<Text>();
        des = GameObject.Find("winCanvas/des").GetComponent<Text>();

        pic = GameObject.Find("winCanvas/pic").GetComponent<Image>();
        pic.sprite = null;


    }

    

    void closeFunc() {
        GameManager.Instance.clearBin();
        SceneManager.LoadScene(Scene.beginMenu);//sceneName
    }

    public void hidden()
    {
        _closing = true;
    }



}
