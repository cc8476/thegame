using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using manager;
using System.IO;

public class EventPane : MonoBehaviour
{

    private Button closeBtn;//关闭按钮
    private Button confirmBtn;//确认按钮
    private Text title;//标题
    private Text des;//描述
    private Image pic;//头像


    
    void Start()
    {
        closeBtn = GameObject.Find("Canvas/closeBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(closeFunc);

        confirmBtn = GameObject.Find("Canvas/confirmBtn").GetComponent<Button>();
        confirmBtn.onClick.AddListener(closeFunc);

        title = GameObject.Find("Canvas/title").GetComponent<Text>();
        des = GameObject.Find("Canvas/des").GetComponent<Text>();

        pic = GameObject.Find("Canvas/pic").GetComponent<Image>();
        pic.sprite = null;

        Debug.Log("EventPane e.name "+eventManager.currentEvent.name);
        Debug.Log("EventPane e.des "+eventManager.currentEvent.des);

        title.text = eventManager.currentEvent.name;
        des.text = eventManager.currentEvent.des;

        //奖励是role
        if(eventManager.currentEvent.type==1) {
            RawRoleStructure role = RawRoleManager.GetRoleByOrder(eventManager.currentEvent.roleIdSending);
            string headpic =role.headpic;
            Debug.Log("Application.streamingAssetsPath "+Application.streamingAssetsPath);
            Sprite sp  = LoadSpriteByIO( Application.streamingAssetsPath + headpic);

            pic.sprite = sp;
        }
        else if(eventManager.currentEvent.type==3) {
            itemStructure item = itemManager.GetItemByOrder(eventManager.currentEvent.itemSending);
            string headpic =item.headpic;
            Sprite sp  = LoadSpriteByIO( Application.streamingAssetsPath + headpic);

            pic.sprite = sp;
        }

    }

    void closeFunc() {
        SceneManager.UnloadSceneAsync("EventPane");

        eventManager.checkEvents();
    }







        public static Texture2D LoadTexture2DByIO(string _url)
        {
            //创建文件读取流
            FileStream _fileStream = new FileStream(_url, FileMode.Open, FileAccess.Read);
            _fileStream.Seek(0, SeekOrigin.Begin);
            //创建文件长度缓冲区
            byte[] _bytes = new byte[_fileStream.Length];
            _fileStream.Read(_bytes, 0, (int)_fileStream.Length);
            _fileStream.Close();
            _fileStream.Dispose();
            //创建Texture
            Texture2D _texture2D = new Texture2D(1, 1);
            _texture2D.LoadImage(_bytes);
            return _texture2D;
        }

        public static Sprite LoadSpriteByIO(string _url)
        {
            Texture2D _texture2D = LoadTexture2DByIO(_url);
            Sprite _sprite = Sprite.Create(_texture2D, new Rect(0, 0, _texture2D.width, _texture2D.height), new Vector2(0.5f, 0.5f));
            return _sprite;
        }

}
