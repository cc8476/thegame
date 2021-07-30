using System.Collections;
using System.Collections.Generic;
using manager;
using UnityEngine;
using UnityEngine.UI;

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
        //从roldId中获取role
        RoleStruct role =  roleTable.Instance.getDataById(roleId);
        roleNameTxt.text = role.name;
        roleHpTxt.text = role.hp.ToString();
        attTxt.text = role.att.ToString();
        criticalTxt.text = role.critial.ToString();
        defTxt.text = role.def.ToString();
        speedTxt.text = role.speed.ToString();
        darkresTxt.text = role.darkres.ToString();
        lightresTxt.text = role.lightres.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
