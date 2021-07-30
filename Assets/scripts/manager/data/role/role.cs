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



};  

