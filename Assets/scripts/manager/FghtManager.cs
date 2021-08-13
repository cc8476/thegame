
public class FghtManager
{
    public skillStruct sk;//当前选中的技能
    static FghtManager _instance;
    public FghtManager()
    {
    }


    static public FghtManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FghtManager();
            }
            return _instance;
        }
    }



}
