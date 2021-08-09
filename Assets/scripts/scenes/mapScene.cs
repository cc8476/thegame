using System;
using manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mapScene : MonoBehaviour
{
    private Text currentTurn;//轮次显示ui
    private Text currentWave;//波次显示ui

    private Button eventBtn;//事件按钮
    private Button bossBtn;//boss按钮
    private Button enemyBtn;//敌人按钮

    private Image bg;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("map scene");
        currentTurn = GameObject.Find("Canvas/currentTurn").GetComponent<Text>();
        currentWave = GameObject.Find("Canvas/currentWave").GetComponent<Text>();

        eventBtn = GameObject.Find("Canvas/eventBtn").GetComponent<Button>();
        bossBtn = GameObject.Find("Canvas/bossBtn").GetComponent<Button>();
        enemyBtn = GameObject.Find("Canvas/enemyBtn").GetComponent<Button>();
        bg = GameObject.Find("Canvas/background").GetComponent<Image>();



        eventBtn.onClick.AddListener(getEvent);
        enemyBtn.onClick.AddListener(IntoFight);
        bossBtn.onClick.AddListener(IntoBossFight);

        this.render();

    }

    //进入战斗
    private void IntoFight()
    {
        //生成当前轮次的敌人列表
        GameManager.Instance.setCurrentEnemyList();
        //进入战斗
        SceneManager.LoadScene(Scene.fightScene);
    }
    //进入Boss战斗
    private void IntoBossFight()
    {
        //生成当前轮次的敌人列表
        GameManager.Instance.setCurrentBossList();
        //进入战斗
        SceneManager.LoadScene(Scene.fightScene);
    }
    

    //生成随机事件
    void getEvent()
    {

        GameManager.Instance.getRandomEvent(this.transform);
        GameManager.Instance.wave += 1;
    }


    // Update is called once per frame
    void render()
    {
        currentTurn.text = "turn:" + GameManager.Instance.turn.ToString();
        currentWave.text = "wave:" + GameManager.Instance.wave.ToString() + "/" + Rule.maxWave;

        bg.sprite = ImageTool.LoadSpriteByIO(Application.streamingAssetsPath + "/map/"+ GameManager.Instance.turn.ToString()+".png");
        bg.SetNativeSize();
        eventBtn.gameObject.SetActive(false);
        bossBtn.gameObject.SetActive(false);
        enemyBtn.gameObject.SetActive(false);



        //可以显示event和enmey
        if (GameManager.Instance.wave < Rule.maxWave)
        {
            eventBtn.gameObject.SetActive(true);
            enemyBtn.gameObject.SetActive(true);
        }
        //只能显示boss
        else
        {
            bossBtn.gameObject.SetActive(true);
        }
    }
}
