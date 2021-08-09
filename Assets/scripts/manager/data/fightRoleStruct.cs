using System;
using UnityEngine;

public class fightRoleStruct : RawRoleStructure
{
    [Tooltip("存放当前的enemy或者role")]
    public RawRoleStructure role;//
    [Tooltip("存放当前的enemy或者role")]
    public roleDisplay ui;//
    public int curHP;//当前的hp
    [Tooltip("当前轮次")]
    public int curRound;//当前轮次
    [Tooltip("当前激活 或死亡")]
    public bool active = true;//
    [Tooltip("是否行动过")]
    public bool acted = false;//
}

//RawRoleStructure 用于记录战斗中的专有数据
