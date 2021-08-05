using manager;
using UnityEngine;

[System.Serializable]
public class skillStruct
{

    public int id;
    public int att;//攻击力
    public int attPosition;//攻击范围 :000  ,根据每一位，0=不攻击，1=攻击 
    public int attAll;//是否攻击全部
    public int attType;//0=无属性，1= darkres,2=lightres

    public string icon;//技能图标
    public string name;//名字
    public string des;//描述




};

