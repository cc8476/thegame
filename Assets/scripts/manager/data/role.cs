using manager;
using UnityEngine;

[System.Serializable]
public class RoleStruct : RawRoleStructure
{

    //新增的属性
   public int rawRoldId;//在战斗中的排位
   public int order;//在战斗中的排位
   public int curhp;//当前血量
   public int status;//1=训练中 0= free



    public void getFromRow(
       int order
       )
   {
       Debug.Log("getFromRow ..."+order);
       RawRoleStructure raw = (RawRoleStructure)RawRoleManager.list[order];

       this.rawRoldId = raw.id;

       this.hp = raw.hp;
       this.att = raw.att;
       this.critial = raw.critial;
       this.def = raw.def;
       this.speed = raw.speed;
       this.darkres = raw.darkres;
       this.lightres = raw.lightres;
       this.quality = raw.quality;
       this.name = raw.name;
       this.headpic = raw.headpic;
       this.coin = raw.coin;
       this.bodypic = raw.bodypic;

        this.order= 99;//默认都在最末尾
        this.curhp= raw.hp;
        this.status= 0;//



    }




};  

