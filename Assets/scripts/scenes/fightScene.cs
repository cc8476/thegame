
using System.Collections;
using manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fightScene : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject retreatBtn;//撤退按钮

    void Start()
    {
        retreatBtn = GameObject.Find("Canvas/retreatBtn");
        retreatBtn.GetComponent<Button>().onClick.AddListener(retreatFunc);

        GameObject go = new GameObject();
        go.AddComponent<Text>();

        Text testtxt = go.GetComponent<Text>();
        testtxt.text = "hahahaha";

        testtxt.transform.position = new Vector3(0.0f, 5.0f, 0.0f);

    }


    private void OnGUI()
    {

        //展示item和role的图标列表
        ArrayList itemList = GameManager.Instance.itemList;
        int position_x = 0;
        int position_y = 50;
        foreach (itemStructure item in itemList)
        {
            string headpic = item.headpic;
            Texture2D sp = ImageTool.LoadTexture2DByIO(Application.streamingAssetsPath + headpic);
            GUI.Box(new Rect(position_x, position_y, 50, 50), sp);
            position_x += 100;
        }

        //展示item和role的图标列表
        //展示role的人物形象
        ArrayList roleList = GameManager.Instance.roleList;
        int positionrole_x = 0;
        position_y = 100;
        int positionRole_y = 350;
        foreach (RoleStruct role in roleList)
        {
            string headpic = role.headpic;
            Texture2D spHead = ImageTool.LoadTexture2DByIO(Application.streamingAssetsPath + headpic);
            GUI.Box(new Rect(position_x, position_y, 50, 50), spHead);
            position_x += 100;


            string bodypic = role.bodypic;
            Texture2D spBody = ImageTool.LoadTexture2DByIO(Application.streamingAssetsPath + bodypic);
            GUI.Box(new Rect(positionrole_x, positionRole_y, 90, 90), spBody);
            positionrole_x += 100; 
        }


        //展示enemy的人物形象
        ArrayList enmeyList = GameManager.Instance.enmeyList;
        int positionenemy_x = 300;
        position_y = 100;
        int positionEnemy_y = 350;
        foreach (enemyStruct role in enmeyList)
        {
            string bodypic = role.bodypic;
            Texture2D spBody = ImageTool.LoadTexture2DByIO(Application.streamingAssetsPath + bodypic);
            GUI.Box(new Rect(positionenemy_x, positionEnemy_y, 90, 90), spBody);
            positionenemy_x += 100;
        }



    }



    // Update is called once per frame
    void Update()
    {
        
    }

    void retreatFunc()
    {
        GameManager.Instance.init();
        SceneManager.LoadScene("town");//sceneName
    }
}
