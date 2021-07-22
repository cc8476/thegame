
using System.IO;
using UnityEngine;

public class ImageTool
    {

        public static Sprite LoadSpriteByIO(string _url)
        {
            Texture2D _texture2D = LoadTexture2DByIO(_url);
            Sprite _sprite = Sprite.Create(_texture2D, new Rect(0, 0, _texture2D.width, _texture2D.height), new Vector2(0.5f, 0.5f));
            return _sprite;
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


    }
