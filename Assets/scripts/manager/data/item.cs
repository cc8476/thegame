
public struct itemStructure
{


    public string name;//名字
    public string headpic;//头像

    public int type;//0=普通道具 1=神器


    public void setValues(
        int type = 0,
        string name = "item's name",
        string headpic = "default_head.png"
        )
    {

        this.name = name;
        this.headpic = "/item/"+headpic;
        this.type = type;
    }
};
