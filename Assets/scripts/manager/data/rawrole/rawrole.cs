
using UnityEngine;

public class RawRoleStructure
{

    public int id;//血量
    public int hp;//血量
    public int att;//攻击
    public int critical;//暴击
    public int def;//防御
    public int speed;//速度
    public int darkres;//暗抗性
    public int lightres;//光抗性
    [Tooltip("quality  enemy=99,是boss级别")]
    public int quality;//品质
    public string name;//名字
    public string skills;//技能列表
    public string headpic { get; set; }//头像
    public string bodypic { get; set; }//身体图片
    public int coin { get; set; }//购买花费


};
