using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using manager;

public class townScene : MonoBehaviour
{
    private Button mapBtn;//地图按钮
    private Text coinTxt;//金币显示ui
    private Text roleTxt;//人数显示ui
    private Text turnTxt;//轮次显示ui
    private Text waveTxt;//波次显示ui


    void Start()
    {
        Debug.Log("town scene");
        eventManager.checkEvents();
        mapBtn = GameObject.Find("Canvas/mapButton").GetComponent<Button>();
        mapBtn.onClick.AddListener(mapSceneFunc);

        coinTxt = GameObject.Find("Canvas/coinTxt").GetComponent<Text>();
        roleTxt = GameObject.Find("Canvas/roleTxt").GetComponent<Text>();
        turnTxt = GameObject.Find("Canvas/turnTxt").GetComponent<Text>();
        waveTxt = GameObject.Find("Canvas/waveTxt").GetComponent<Text>();

    }

    void mapSceneFunc()
    {
        Debug.Log("You selected mapScene");
        SceneManager.LoadScene(Scene.mapScene);//sceneName
    }

    // Update is called once per frame
    void Update()
    {
        //TODO::数据的更新，除了依赖update()外，还能监听GameManager吗？
        coinTxt.text = "coin:"+GameManager.Instance.coin.ToString();
        roleTxt.text = "role:"+GameManager.Instance.roleList.Count.ToString();
        waveTxt.text = "wave:"+GameManager.Instance.turn.ToString();
        turnTxt.text = "turn:"+GameManager.Instance.wave.ToString();
    }
}
