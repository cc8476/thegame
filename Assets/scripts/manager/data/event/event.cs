public struct eventStruct
{
    public int turn;//触发轮次 
    public int wave;//触发波次
    public int id;//
    public string name;//事件名称
    public string des;//事件描述
    public string pic;//事件图片


    public int roleQuality;//赠送的角色质量
    public int roleId;//实际赠与的roleId

    public int coinSending;//赠送的金币
    public int itemIdSending;//赠送的物品id
    public int miracleIdSending;//赠送的神器id

    public eventStructType type;//0=任务提示;1=英雄；2=金币 3=道具
                                //type==0比较特殊，会在处理的时候，进行判断，然后会影响turn和wave





    public void addItem(
       string name = "道具名",
       int turn = -1, int wave = -1, int itemIdSending = 0,
       string pic = "default_event.png", string des = "默认描述"
       )
    {
        this.name = name;
        this.turn = turn;
        this.wave = wave;
        this.itemIdSending = itemIdSending;
        this.pic = pic;
        this.des = des;

        this.type = eventStructType.Item;
    }


    public void addCoin(
        string name = "增加金币",
        int turn = -1, int wave = -1, int coinSending = 5,
        string pic = "default_event.png", string des = "默认描述"
        )
    {
        this.name = name;
        this.turn = turn;
        this.wave = wave;
        this.coinSending = coinSending;
        this.pic = pic;
        this.des = des;

        this.type = eventStructType.Coin;
    }

    public void addGuide(
        string name = "新手引导",
        int turn = -1, int wave = -1, string des = "默认描述"
        )
    {
        this.name = name;
        this.turn = turn;
        this.wave = wave;
        this.des = des;

        this.type = eventStructType.MissionTips;
    }



};



public enum eventStructType
{
    MissionTips = 0,
    Role = 1,
    Coin = 2,
    Item = 3,
}
//0=任务提示;1=英雄；2=金币 3=道具