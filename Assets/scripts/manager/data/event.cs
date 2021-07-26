public struct eventStructure
{
   public int turn;//触发轮次 
   public int wave;//触发波次

   public string name;//事件名称
   public string des;//事件描述
   public string pic;//事件图片


    public int roleIdSending;//赠送的角色id
    public int coinSending;//赠送的金币
    public int itemSending;//赠送的物品id
    public int miracleSending;//赠送的神器id

    public eventStructureType type;//0=任务提示;1=英雄；2=金币 3=道具
    //type==0比较特殊，会在处理的时候，进行判断，然后会影响turn和wave





   public void setValues(
       string name="事件名称",
       int turn=-1,int wave=-1,int roleIdSending=0,int coinSending=5,
       int itemSending=5,int miracleSending=0,string pic="default_event.png",string des="默认描述"
       )
   {
       this.name = name;
       this.turn = turn;
       this.wave = wave;
       this.roleIdSending = roleIdSending;
       this.coinSending = coinSending;
       this.itemSending = itemSending;
       this.miracleSending = miracleSending;
       this.pic = pic;
       this.des = des;
   }

    public void addHero(
       string name="英雄名",
       int turn=-1,int wave=-1,int roleIdSending=0,
       string pic="default_event.png",string des="默认描述"
       )
   {
       this.name = name;
       this.turn = turn;
       this.wave = wave;
       this.roleIdSending = roleIdSending;
       this.pic = pic;
       this.des = des;

       this.type = eventStructureType.Role;
   }
    public void addItem(
       string name="道具名",
       int turn=-1,int wave=-1,int itemSending=0,
       string pic="default_event.png",string des="默认描述"
       )
   {
       this.name = name;
       this.turn = turn;
       this.wave = wave;
       this.itemSending = itemSending;
       this.pic = pic;
       this.des = des;

       this.type = eventStructureType.Item;
   }
   

   public void addCoin(
       string name="增加金币",
       int turn=-1,int wave=-1,int coinSending=5,
       string pic="default_event.png",string des="默认描述"
       )
   {
       this.name = name;
       this.turn = turn;
       this.wave = wave;
       this.coinSending = coinSending;
       this.pic = pic;
       this.des = des;

       this.type = eventStructureType.Coin;
   }

   public void addGuide(
       string name="新手引导",
       int turn=-1,int wave=-1,string des="默认描述"
       )
   {
       this.name = name;
       this.turn = turn;
       this.wave = wave;
       this.des = des;

       this.type = eventStructureType.MissionTips;
   }

    

};  



public enum eventStructureType
{
    MissionTips = 0,
    Role = 1,
    Coin = 2,
    Item =3,
}
//0=任务提示;1=英雄；2=金币 3=道具