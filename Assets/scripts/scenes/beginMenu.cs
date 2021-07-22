using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using manager;

public class beginMenu : MonoBehaviour
{

    private GameObject playBtn;//开始按钮
    private GameObject quitBtn;//退出按钮
    private GameObject continueBtn;//继续游戏
    void Start()
    {
        playBtn = GameObject.Find("Canvas/playBtn");
        playBtn.GetComponent<Button>().onClick.AddListener(playFunc);

        continueBtn = GameObject.Find("Canvas/continueBtn");
        if(GameManager.Instance.checkBin()) {
            continueBtn.GetComponent<Button>().onClick.AddListener(continueFunc);
            continueBtn.SetActive(true);
        }
        else {
            continueBtn.SetActive(false);
        }

        quitBtn = GameObject.Find("Canvas/quitBtn");
        quitBtn.GetComponent<Button>().onClick.AddListener(quitFunc);
    }

    void playFunc() {
        GameManager.Instance.init();
        SceneManager.LoadScene("introGame");//sceneName
    }
    void quitFunc() {
        
    }
    void continueFunc() {
        GameManager.Instance.init();
        GameManager.Instance.ReadBin();
        SceneManager.LoadScene("town");//sceneName
    }
}
