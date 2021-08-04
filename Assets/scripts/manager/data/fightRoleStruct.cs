using System;
public class fightRoleStruct : RawRoleStructure
{
    public RawRoleStructure role;//存放当前的enemy或者role

    public int curHP;//当前的hp

    public int curRound;//当前轮次

    public bool active =true;//当前激活 或死亡

    public bool acted= false;//是否行动过
}

//RawRoleStructure 用于记录战斗中的专有数据
