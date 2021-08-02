using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//战斗场景中的人物属性面板
public class rolePane : MonoBehaviour
{
    // Start is called before the first frame update

    private Text roleNameTxt;//
    private Text roleHpTxt;//
    private Text attTxt;//
    private Text criticalTxt;//
    private Text defTxt;//
    private Text speedTxt;//
    private Text darkresTxt;//
    private Text lightresTxt;//

    public List<GameObject> sonobj = new List<GameObject>();



    public int hp;
    void Start()
    {
        roleNameTxt = GameObject.Find("Canvas/roleNameTxt").GetComponent<Text>();
        roleHpTxt = GameObject.Find("Canvas/roleHpTxt").GetComponent<Text>();
        attTxt = GameObject.Find("Canvas/attTxt").GetComponent<Text>();
        criticalTxt = GameObject.Find("Canvas/criticalTxt").GetComponent<Text>();
        defTxt = GameObject.Find("Canvas/defTxt").GetComponent<Text>();
        speedTxt = GameObject.Find("Canvas/speedTxt").GetComponent<Text>();
        darkresTxt = GameObject.Find("Canvas/darkresTxt").GetComponent<Text>();
        lightresTxt = GameObject.Find("Canvas/lightresTxt").GetComponent<Text>();
    }

    public void render(int roleId)
    {
        //渲染前销毁
        foreach (GameObject son in this.sonobj)
        {
            Destroy(son);
        }

        //从roldId中获取role
        RoleStruct role =  roleTable.Instance.getDataById(roleId);
        roleNameTxt.text = "角色名:"+role.name;
        roleHpTxt.text = "hp:" + role.hp.ToString();
        attTxt.text = "攻击:" + role.att.ToString();
        criticalTxt.text = "暴击率:" + role.critical.ToString();
        defTxt.text = "防御:" + role.def.ToString();
        speedTxt.text = "速度:" + role.speed.ToString();
        darkresTxt.text = "暗抗性:" + role.darkres.ToString();
        lightresTxt.text = "光抗性:" + role.lightres.ToString();

        string[] strArray = role.skills.ToString().Split(',');
        for (int i = 0; i < strArray.Length; i++)
        {
            Vector3 position = new Vector3(
                transform.position.x,
                transform.position.y - 100*i-200,
                transform.position.z
            );

            int skillId = int.Parse(strArray[i].ToString());
            GameObject skillDisplayOb = (GameObject)Instantiate(Resources.Load("skillDisplay"), transform.position, transform.rotation);
            skillDisplayOb.transform.parent = transform;
            skillDisplayOb.transform.position = position;

            skillDisplay sk = (skillDisplay)skillDisplayOb.GetComponent(typeof(skillDisplay));
            sk.render(skillId);
            this.sonobj.Add(skillDisplayOb);
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
