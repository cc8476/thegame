using System;
public class enemyStruct : RoleStruct
{
    public int turnMin;//限制可以出现在哪一轮
    public int turnMax;//限制可以出现在哪一轮


    public void setValues(
        string name = "role's name",
        int hp = 100, int att = 10, int critial = 0, int def = 5,
        int speed = 5, int darkres = 0, int lightres = 0, int qulity = 1,
        string headpic = "default_enemy_head.png",
        int turnMin=0,int turnMax=99,
        int coin = 100, string bodypic = "default_enemy_body.png"
        )
    {
        this.hp = hp;
        this.att = att;
        this.critial = critial;
        this.def = def;
        this.speed = speed;
        this.darkres = darkres;
        this.lightres = lightres;
        this.quality = quality;
        this.name = name;
        this.headpic = "/enemy/" + headpic;
        this.coin = coin;
        this.bodypic = "/enemy/" + bodypic;

        this.turnMin = 0;
        this.turnMax = 99;
    }
}
