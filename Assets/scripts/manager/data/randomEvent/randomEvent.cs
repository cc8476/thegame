public struct randomStruct
{


    public string name;//事件名称
    public string des;//事件描述
    public string pic;//事件图片

    public int id;
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


};



public enum randomEventType
{
    MissionTips = 0,
    Role = 1,
    Coin = 2,
    Item = 3,
    Miracle = 4
}
//0=任务提示;1=英雄；2=金币 3=道具 4=神器