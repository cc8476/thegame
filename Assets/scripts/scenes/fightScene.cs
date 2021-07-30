
using System.Collections;
using System.Collections.Generic;
using manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fightScene : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject retreatBtn;//撤退按钮

    private GameObject canvas;//画布

    private GameObject rolePane;// 人物面板

    void Start()
    {
        retreatBtn = GameObject.Find("Canvas/retreatBtn");
        retreatBtn.GetComponent<Button>().onClick.AddListener(retreatFunc);

        canvas = GameObject.Find("Canvas");


        //展示role的人物形象
        List<RoleStruct> roleList = roleTable.Instance.getAllData();
        int positionrole_x = 0;
        foreach (RoleStruct role in roleList)
        {
            Vector3 v = new Vector3(canvas.transform.position.x + positionrole_x, canvas.transform.position.y, canvas.transform.position.z);
            render("roleDisplay", v, role);
            positionrole_x += 50;
        }


        //展示enemy的人物形象
        List<int> enemyList = GameManager.Instance.enemyList;
        int positionenemy_x = 300;
        foreach (int enemyId in enemyList)
        {
            enemyStruct enemy =  enemyTable.Instance.getDataById(enemyId);
            Vector3 v = new Vector3(canvas.transform.position.x + positionenemy_x, canvas.transform.position.y, canvas.transform.position.z);
            positionenemy_x += 50;
        }


        //展示唯一的人物面板
        Vector3 position = new Vector3(
            canvas.transform.position.x+350,
            canvas.transform.position.y -250,
            canvas.transform.position.z
        );
         rolePane = (GameObject)Instantiate(Resources.Load("rolePane"), transform.position, transform.rotation);
        rolePane.transform.parent = canvas.transform;
        rolePane.transform.position = position;



        // 添加事件侦听
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.HOVER_ROLE, showRolePaneHandler);
    }

    private void showRolePaneHandler(UEvent uEvent)
    {
        int roleId = (int)uEvent.eventParams;
        rolePane rolepane = (rolePane)rolePane.GetComponent(typeof(rolePane));
        rolepane.render(roleId);
    }

    private void render(string type, Vector3 v, RoleStruct role )
    {        
        GameObject instance = (GameObject)Instantiate(Resources.Load(type), transform.position, canvas.transform.rotation);
        instance.transform.parent = canvas.transform;
        instance.transform.position = v;

        roleDisplay pane = (roleDisplay)instance.GetComponent(typeof(roleDisplay));
        pane.render(role);
    }


    private void OnGUI()
    {

        //TODO::改成用Prefab的形式
        //展示item和role的图标列表
        List<int> itemList = GameManager.Instance.itemList;
        int position_x = 0;
        int position_y = 50;
        foreach (int itemId in itemList)
        {
            itemStructure item =  itemTable.Instance.getDataById(itemId);
            string headpic = item.headpic;
            Texture2D sp = ImageTool.LoadTexture2DByIO(Application.streamingAssetsPath + headpic);
            GUI.Box(new Rect(position_x, position_y, 50, 50), sp);
            position_x += 100;
        }

        //展示item和role的图标列表
        //展示role的人物形象
        List<RoleStruct> roleList = roleTable.Instance.getAllData();
        position_y = 100;
        foreach (RoleStruct role in roleList)
        {
            string headpic = role.headpic;
            Texture2D spHead = ImageTool.LoadTexture2DByIO(Application.streamingAssetsPath + headpic);
            GUI.Box(new Rect(position_x, position_y, 50, 50), spHead);
            position_x += 100;
        }


    }



    // Update is called once per frame
    void Update()
    {
        
    }

    void retreatFunc()
    {
        SceneManager.LoadScene(Scene.town);//sceneName
    }
}
