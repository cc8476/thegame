using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
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
