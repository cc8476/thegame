
using System.Collections.Generic;

[System.Serializable]
public struct SaveData
{
    public int coin;//金币 这里不能赋值
    public int level;//等级
    public int turn;//当前轮次
    public int wave;//当前波次
    public int currentEventId;//奖励event记录
    public List<int> itemList;//当前波次
    public List<int> enemyList;//当前波次



}

