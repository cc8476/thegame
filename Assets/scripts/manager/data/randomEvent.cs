public struct randomStructure
{


   public string name;//事件名称
   public string des;//事件描述
   public string pic;//事件图片


    public int roleId;//赠送的角色id
    public int coin;//赠送的金币
    public int itemId;//赠送的物品id
    public int miracleId;//赠送的神器id

    public int weight;//权重
    public int limit;//限制
    public int limitType;//限制类型(根据等级等因素)

    public int min;//最大值
    public int max;//最大值
    public int num;//固定值
    public randomEventType type;//1=英雄；2=金币 3=道具 4=神器




    public void addHero(
       string name="随机事件",
       int roleId=0,
       int weight=10,
       string pic="default_event.png",string des="默认描述"
       )
   {
       this.name = name;
       this.roleId = roleId;
       this.weight = weight;
       this.pic = pic;
       this.des = des;

       this.type = randomEventType.Role;
   }
    public void setItem(
       string name="随机事件",
              int weight=10,
              int num=1,
       string pic="default_event.png",string des="默认描述"
       )
   {
       this.name = name;
       this.num = num;
       this.pic = pic;
       this.weight = weight;
       this.des = des;

       this.type = randomEventType.Item;
   }

    public void setCoin(
       string name="随机事件",
       int min=0,
       int max=100,
              int weight=10,
       string pic="default_event.png",string des="默认描述"
       )
   {
       this.name = name;
       this.min = min;
       this.max = max;
       this.pic = pic;
       this.weight = weight;
       this.des = des;

       this.type = randomEventType.Coin;
   }
   


    

};  



public enum randomEventType
{
    MissionTips = 0,
    Role = 1,
    Coin = 2,
    Item =3,
    Miracle =4
}
//0=任务提示;1=英雄；2=金币 3=道具 4=神器