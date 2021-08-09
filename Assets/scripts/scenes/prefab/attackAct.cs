using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attackAct : MonoBehaviour
{
    // Start is called before the first frame update
    private Image im;
    public List<Sprite> SpriteFrames = new List<Sprite>();


    private int mCurFrame = 0;
    private float mDelta = 0;
    public float FPS = 24;
    public bool IsPlaying = false;
    public bool AutoPlay = false;
    public bool Loop = false;
    public int FrameCount = 8;

    void OnEnable()
    {
        Debug.Log("prefab");
        im = GameObject.Find("actCanvas/Image").GetComponent<Image>();
        Debug.Log("prefab");

    }

    private void SetSprite(int curFrame)
    {
        im.sprite = SpriteFrames[curFrame];

    }



    public void render(string name, int count)
    {
        for (int i = 1; i <= count; i++)
        {
            SpriteFrames.Add(ImageTool.LoadSpriteByIO(Application.streamingAssetsPath + "/action/" + name + "/" + i + ".png"));

        }


        IsPlaying = true;
    }


    void Update()
    {
        if (!IsPlaying || 0 == FrameCount)
        {
            return;
        }
        mDelta += Time.deltaTime;
        if (mDelta > 1 / FPS)
        {
            mDelta = 0;
            mCurFrame++;
            if (mCurFrame >= FrameCount)
            {
                IsPlaying = false;
                Destroy(gameObject);
                return;
            }
            else if (mCurFrame < 0)
            {
                IsPlaying = false;

                return;
            }
            SetSprite(mCurFrame);
        }
    }

}
