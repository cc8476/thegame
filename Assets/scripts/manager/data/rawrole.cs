
public struct RawRoleStructure
{
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

   public void setValues(
       string name="role's name",
       int hp=100,int att=10,int critial=0,int def=5,
       int speed=5,int darkres=0,int lightres=0,int qulity=1,
       string headpic="default_head.png",
       int coin=100,string bodypic="default_body.png"
       )
   {
       this.hp = hp;
       this.att = att;
       this.critial = critial;
       this.def = def;
       this.speed = speed;
       this.darkres = darkres;
       this.lightres = lightres;
       this.qulity = qulity;
        this.name = name;
       this.headpic = "/role/"+headpic;
       this.coin = coin;
       this.bodypic = "/role/"+bodypic;
   }
};  
