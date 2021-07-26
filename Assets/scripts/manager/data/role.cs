using manager;
using UnityEngine;

[System.Serializable]
public class RoleStruct
{
    //继承自RawRole
   public int id;//角色id 在游戏范围是唯一值，在添加(addRole)的时候被生成
   public int hp;//血量
   public int att;//攻击
   public int critial;//暴击
   public int def;//防御
   public int speed;//速度
   public int darkres;//暗抗性
   public int lightres;//光抗性
   public int qulity;//品质
   public string name;//名字
   public string headpic;//头像
   public string bodypic;//身体图片
   public int coin;//购买花费



    //新增的属性
   public int order;//在战斗中的排位
   public int curhp;//当前血量
   public int status;//1=训练中 0= free

   public void getFromRow(
       int order
       )
   {
       Debug.Log("getFromRow ..."+order);
       RawRoleStructure raw = (RawRoleStructure)RawRoleManager.list[order];

       this.hp = raw.hp;
       this.att = raw.att;
       this.critial = raw.critial;
       this.def = raw.def;
       this.speed = raw.speed;
       this.darkres = raw.darkres;
       this.lightres = raw.lightres;
       this.qulity = raw.qulity;
       this.name = raw.name;
       this.headpic = raw.headpic;
       this.coin = raw.coin;
       this.bodypic = raw.bodypic;

        this.order= 99;//默认都在最末尾
        this.curhp= raw.hp;
        this.status= 0;//

   }

   
};  

