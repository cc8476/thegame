using manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//战斗场景中的人物形象展示
public class fightEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public Image win;
    public Image lose;
    private bool winBool;
    private bool loseBool;
    private Button closeBtn;//关闭按钮
    void OnEnable()
    {
        win = transform.Find("Canvas/win").GetComponent<Image>();
        win.color = new Color(255, 255, 255, 0);

        lose = transform.Find("Canvas/lose").GetComponent<Image>();
        lose.color = new Color(255, 255, 255, 0);

        closeBtn = GameObject.Find("Canvas/clickBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(closeFunc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void closeFunc()
    {
        if (this.winBool)
        {
            //成功的处理
            SceneManager.LoadScene(Scene.mapScene);//sceneName
            //TODO::处理数据
        }
        else
        {
            //失败的处理
            GameManager.Instance.clearBin();
            SceneManager.LoadScene(Scene.beginMenu);//sceneName
        }
    }

    public void render(bool winB,bool loseB) {

        this.winBool = winB;
        this.loseBool = loseB;

        win.color = new Color(255, 255, 255, 0);
        lose.color = new Color(255, 255, 255, 0);
        if (this.winBool) {
            win.color = new Color(255, 255, 255, 1);
        }
        else if(this.loseBool) {
            lose.color = new Color(255, 255, 255, 1);
        }


    }

}
