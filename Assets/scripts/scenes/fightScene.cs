
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
    private Text roundTxt;//

    private GameObject canvas;//画布

    private GameObject rolePane;// 人物面板
    private int currentRound =1 ;//当前轮次
    private Dictionary<int, fightRoleStruct> roles = new Dictionary<int, fightRoleStruct>();//我方组 int = id
    private Dictionary<int, fightRoleStruct> enemies = new Dictionary<int, fightRoleStruct>();//敌人组
    private int currentChar =-1;//当前执行角色id
    private int currentType = -1;//当前执行角色类别 0= roles, 1= enemies

    private int currentBeAttackChar = -1;//当前被攻击的角色id

    void Start()
    {

        retreatBtn = GameObject.Find("Canvas/retreatBtn");
        retreatBtn.GetComponent<Button>().onClick.AddListener(retreatFunc);

        roundTxt = GameObject.Find("Canvas/roundTxt").GetComponent<Text>();

        canvas = GameObject.Find("Canvas");


        //展示role的人物形象
        List<RoleStruct> roleList = roleTable.Instance.getAllData();
        int positionrole_x = 0;
        foreach (RoleStruct role in roleList)
        {
            Vector3 v = new Vector3(canvas.transform.position.x + positionrole_x, canvas.transform.position.y, canvas.transform.position.z);
            roleDisplay ui = this.render("roleDisplay", v, role,0);
            positionrole_x -= 150;

            fightRoleStruct f = new fightRoleStruct();
            f.role = role; 
            f.curHP = role.curhp;
            f.curRound = this.currentChar;
            f.ui = ui;
            this.roles.Add(role.id, f);
        }


        //展示enemy的人物形象
        List<int> enemyList = GameManager.Instance.enemyList;
        int positionenemy_x = 300;
        foreach (int enemyId in enemyList)
        {
            enemyStruct enemy =  enemyTable.Instance.getDataById(enemyId);
            Vector3 v = new Vector3(canvas.transform.position.x + positionenemy_x, canvas.transform.position.y, canvas.transform.position.z);
            positionenemy_x += 150;
            roleDisplay ui = this.render("roleDisplay", v, enemy,1);

            fightRoleStruct f = new fightRoleStruct();
            f.role = enemy;
            f.ui = ui;
            f.curHP = enemy.curhp;
            f.curRound = this.currentChar;
            this.enemies.Add(enemy.id, f);
        }


        //展示唯一的人物面板
        Vector3 position = new Vector3(
            canvas.transform.position.x+220,
            canvas.transform.position.y -200,
            canvas.transform.position.z
        );
         rolePane = (GameObject)Instantiate(Resources.Load("rolePane"), transform.position, transform.rotation);
        rolePane.transform.parent = canvas.transform;
        rolePane.transform.position = position;

        // 添加事件侦听
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.HOVER_ROLE, showRolePaneHandler);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ATTACK_ROLE, attackRolePaneHandler);

        this.setCurrentChar();
        


    }

    //根据roles和enemies，得到当前的轮次
    private void setCurrentChar()
    {
        int currentSpeed = -1;//当前最高speed
        int currentOrder = -1;//当前最高speed的id
        int currentType = -1;//0=role ,1= enemy
        foreach (KeyValuePair< int, fightRoleStruct > single in roles)
        {
            if(!single.Value.acted && single.Value.speed>= currentSpeed)
            {
                currentSpeed = single.Value.speed;
                currentOrder = single.Key;
                currentType = 0;
            }
        }

        foreach (KeyValuePair<int, fightRoleStruct> single in enemies)
        {
            if (!single.Value.acted && single.Value.speed >= currentSpeed)
            {
                currentSpeed = single.Value.speed;
                currentOrder = single.Key;
                currentType = 1;
            }
        }

        Debug.Log("当前出列的是:"+ currentOrder+ ",当前出列类别是:" + currentType);

        this.currentChar = currentOrder;
        this.currentType = currentType;


        renderUI();



    }

    private void renderUI()
    {

        //开始渲染
        roundTxt.text = "第" + this.currentRound.ToString() + "轮";

        roleDisplay rd;
        if (this.currentType == 0)
        {
            rd = (roleDisplay)roles[this.currentChar].ui;
        }
        else
        {
            rd = (roleDisplay)enemies[this.currentChar].ui;
        }

        rd.ready();
    }
 
    private void showRolePaneHandler(UEvent uEvent)
    {
        int[] data = (int[])uEvent.eventParams;
        
        rolePane rolepane = (rolePane)rolePane.GetComponent(typeof(rolePane));
        rolepane.render(data[0],data[1]);
    }

    //当前被击中对象
    private void attackRolePaneHandler(UEvent uEvent)
    {
        var target = (roleDisplay)uEvent.target;
        var collections = (currentType == 1) ? roles : enemies;

        foreach (var item in collections)
        {
            if (item.Value.ui == target)
            {
                
                Debug.Log("当前被攻击的是1:" + item.Key);
                Debug.Log("当前被攻击的是2:" + item.Value);
                this.currentBeAttackChar = item.Key;
                fightRoleStruct attackTarget = item.Value;
                attackTarget.ui.beAttack();



                break;
            }


        }




    }

    

    private roleDisplay render(string type, Vector3 v, RoleStruct role,int chartype )
    {        
        GameObject instance = (GameObject)Instantiate(Resources.Load(type), transform.position, canvas.transform.rotation);
        instance.transform.parent = canvas.transform;
        instance.transform.position = v;

        roleDisplay pane = (roleDisplay)instance.GetComponent(typeof(roleDisplay));
        pane.render(role, chartype);
        return pane;
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
