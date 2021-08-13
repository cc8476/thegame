
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
    public GameObject rolePaneInstance;// 人物面板
    private int currentRound = 1;//当前轮次
    private Dictionary<int, fightRoleStruct> roles = new Dictionary<int, fightRoleStruct>();//我方组 int = id
    private Dictionary<int, fightRoleStruct> enemies = new Dictionary<int, fightRoleStruct>();//敌人组
    [Tooltip("当前执行角色id")]
    private int currentChar = -1;
    [Tooltip("当前执行角色类别 0= roles, 1= enemies")]
    private int currentType = -1;
    [Tooltip("当前role")]
    private fightRoleStruct currentRole;//
    [Tooltip("当前被攻击的角色id")]
    private int currentBeAttackChar = -1;//
    [Tooltip("是否正在执行动作")]

    private int random = 0;
    private bool inAciton = false;

    void Start()
    {

        this.random = Random.Range(0, 10000);
        Debug.Log("fightscene ....onenable random:" + this.random);


        Debug.Log("fightscene ....start");
        retreatBtn = GameObject.Find("fightCanvas/retreatBtn");
        retreatBtn.GetComponent<Button>().onClick.AddListener(retreatFunc);
        roundTxt = GameObject.Find("fightCanvas/roundTxt").GetComponent<Text>();
        canvas = GameObject.Find("fightCanvas");


        this.initRolesEnemies();//初始化双方角色
        this.setCurrentChar();//设定当前轮次的ready阶段数据
        this.renderUI();//渲染当前ui
    }


    private void initRolesEnemies()
    {

        //展示唯一的人物面板
        Vector3 position = new Vector3(
            canvas.transform.position.x + 220,
            canvas.transform.position.y - 200,
            canvas.transform.position.z
        );
        this.rolePaneInstance = (GameObject)Instantiate(Resources.Load("rolePane"), transform.position, transform.rotation);
        this.rolePaneInstance.transform.parent = canvas.transform;
        this.rolePaneInstance.transform.position = position;

        Debug.Log("rolePane....");

        //展示role的人物形象
        List<RoleStruct> roleList = roleTable.Instance.getAllData();
        int positionrole_x = 0;
        foreach (RoleStruct role in roleList)
        {
            Vector3 v = new Vector3(canvas.transform.position.x + positionrole_x, canvas.transform.position.y, canvas.transform.position.z);
            roleDisplay ui = this.renderRoleDisplay(v, role, 0);
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
            enemyStruct enemy = enemyTable.Instance.getDataById(enemyId);
            Vector3 v = new Vector3(canvas.transform.position.x + positionenemy_x, canvas.transform.position.y, canvas.transform.position.z);
            positionenemy_x += 150;
            roleDisplay ui = this.renderRoleDisplay(v, enemy, 1);

            fightRoleStruct f = new fightRoleStruct();
            f.role = enemy;
            f.ui = ui;
            f.curHP = enemy.curhp;
            f.curRound = this.currentChar;
            this.enemies.Add(enemy.id, f);
        }




        // 添加事件侦听
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.HOVER_ROLE, showRolePaneHandler);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ATTACK_ROLE, attackRolePaneHandler);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ATTACK_AUTO, attackAutoPaneHandler);
    }

    //根据roles和enemies，得到当前的轮次
    //有返回值，说明该轮还有下一个role可行动
    private bool setCurrentChar()
    {
        int currentSpeed = -1;//当前最高speed
        int currentOrder = -1;//当前最高speed的id
        int currentType = -1;//0=role ,1= enemy
        fightRoleStruct currentRole =new fightRoleStruct();
        foreach (KeyValuePair<int, fightRoleStruct> single in roles)
        {
            if (!single.Value.acted && single.Value.active && single.Value.speed >= currentSpeed)
            {
                currentSpeed = single.Value.speed;
                currentOrder = single.Key;
                currentRole = single.Value;
                currentType = 0;
            }
        }

        foreach (KeyValuePair<int, fightRoleStruct> single in enemies)
        {
            if (!single.Value.acted && single.Value.active && single.Value.speed >= currentSpeed)
            {
                currentSpeed = single.Value.speed;
                currentOrder = single.Key;
                currentRole = single.Value;
                currentType = 1;
            }
        }

        Debug.Log("当前出列的是:" + currentOrder + ",当前出列类别是:" + currentType);

        this.currentChar = currentOrder;
        this.currentType = currentType;
        this.currentRole = currentRole;

        return (this.currentChar >= 0) ? true : false;
    }

    private void renderUI()
    {

        foreach (KeyValuePair<int, fightRoleStruct> single in roles)
        {
            single.Value.ui.notReady();
        }
        foreach (KeyValuePair<int, fightRoleStruct> single in enemies)
        {
            single.Value.ui.notReady();
        }

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

        try
        {
            //TODO::不知道这里为什么一直报错
            int[] data = (int[])uEvent.eventParams;
            rolePane rolepane = (rolePane)this.rolePaneInstance.GetComponent(typeof(rolePane));
            rolepane.render(data[0], data[1]);
        }
        catch (System.Exception ex)
        {

        }

    }

    
    private void attackAutoPaneHandler(UEvent uEvent)
    {
        var tempCollections = (currentType == 1) ? roles : enemies;
        int count = 0;
        foreach (var item in tempCollections)
        {
            if (item.Value.active )
            {
                count++;
            }
        }


        int order = Random.Range(0, count);

        int checkCount = 0;
        var target = new roleDisplay();
        foreach (var item in tempCollections)
        {
            if (item.Value.active)
            {
                if(checkCount==order)
                {
                    target = item.Value.ui;
                    break;
                }

                checkCount++;
            }
        }



        this.calculateAttack(target);




    }

    private async void  calculateAttack(roleDisplay target)
    {
        var collections = (currentType == 1) ? roles : enemies;

        foreach (var item in collections)
        {
            if (item.Value.ui == target)
            {

                /**
                当前技能的攻击力 + 当前攻击者的att
                判断是否暴击 critial 是的话，*1.5

                最后 * (1 - 防御力 / 100)

                得到最后的扣血量
                **/

                fightRoleStruct attackTarget = item.Value;
                skillStruct sk = FghtManager.Instance.sk;
                float att = (float)currentRole.role.att + (float)sk.att;

                Debug.Log("currentRole.role.att:" + currentRole.role.att);
                Debug.Log("sk.att:" + sk.att);
                Debug.Log("att:" + att);

                if (Random.Range(0, 100) <= currentRole.critical)
                {
                    att = Mathf.Round(att * 3 / 2);
                }

                att = att * (100 - attackTarget.def) / 100;




                //更新状态
                //int curhp = await attackTarget.ui.beAttack(att);
                this.inAciton = true;
                StartCoroutine(attackTarget.ui.beAttack(att,(curhp) => {
                    this.inAciton = false;
                    if (curhp <= 0)
                    {
                        attackTarget.active = false;//设置死亡状态
                    }
                    currentRole.acted = true;



                    //检查是否胜利  是否下一个角色
                    if (!this.checkGameEnd())
                    {
                        this.nextPlayer();
                    }
                }));





                break;
            }


        }

    }

    //当前被击中对象
    private void attackRolePaneHandler(UEvent uEvent)
    {
        if(this.inAciton)
        {
            return;
        }

        var target = (roleDisplay)uEvent.target;

        this.calculateAttack(target);

    }

    //是否可以下一轮
    //轮次ui
    //设定当前的攻击者箭头
    private void nextPlayer() {

        if (this.setCurrentChar())
        {
            this.renderUI();//渲染当前ui
        }
        else {
            this.nextRound();
     
        }


        

    }
    private void nextRound()
    {
        Debug.Log("下一轮 nextRound");
        this.currentRound += 1;
        foreach (KeyValuePair<int, fightRoleStruct> single in roles)
        {
            single.Value.acted = false;
        }

        foreach (KeyValuePair<int, fightRoleStruct> single in enemies)
        {
            single.Value.acted = false;
        }
        this.nextPlayer();
    }


    private bool checkGameEnd() {
        bool win = true;
        bool lose = true;
        foreach (KeyValuePair<int, fightRoleStruct> single in roles)
        {
            Debug.Log("single.Value.active:" + single.Value.active);
            if(single.Value.active)
            {
                lose = false;
                break;
            }
        }

        foreach (KeyValuePair<int, fightRoleStruct> single in enemies)
        {
            Debug.Log("enemies.Value.active:" + single.Value.active);
            if (single.Value.active)
            {
                win = false;
                break;
            }
        }

        if(win || lose) {
            GameObject instance = (GameObject)Instantiate(Resources.Load("fightEnd"), transform.position, canvas.transform.rotation);
            instance.transform.parent = canvas.transform;
            instance.transform.position = transform.position;

            fightEnd pane = (fightEnd)instance.GetComponent(typeof(fightEnd));
            pane.render(win, lose);

            if(GameManager.Instance.wave < Rule.maxWave)
            {
                GameManager.Instance.wave += 1;
            }
            else
            {
                GameManager.Instance.wave = 0;


                if (GameManager.Instance.turn < Rule.maxTurn)
                {
                    GameManager.Instance.turn += 1;
                }
                else
                {
                    //显示游戏胜利的面板
                    //TODO::为什么这里就不显示呢？去掉Enable后，再勾选上，就不显示了
                    var winPane = (GameObject)Instantiate(Resources.Load("winPane"), transform.position, transform.rotation);
                    winPane.transform.parent = canvas.transform;
                }


            }
            


            return true;
        }

        return false;




    }




    private roleDisplay renderRoleDisplay(Vector3 v, RoleStruct role, int chartype)
    {
        GameObject instance = (GameObject)Instantiate(Resources.Load("roleDisplay"), transform.position, canvas.transform.rotation);
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
            itemStructure item = itemTable.Instance.getDataById(itemId);
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

    private void OnDestroy()
    {
        Debug.Log("OnDestroy fightScene");
        this.enemies.Clear();
        this.roles.Clear();
        this.currentChar = -1;
        this.currentType = -1;
        this.currentRole = null;
        this.currentBeAttackChar = -1;
        this.rolePaneInstance = null;
    }
}
