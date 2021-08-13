using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    public Animator Ani;
    // Start is called before the first frame update
    void Start()
    {
        //TODO::beginMenu在Main之上的层级问题
        //TODO::其他场景加载，可以通过自定义event来通知Main进行加载
        GameObject.DontDestroyOnLoad(this.gameObject);
        StartCoroutine(this.LoadScene(Scene.beginMenu));

    }



    IEnumerator LoadScene(string name)
    {
        Ani.SetBool("isFadeIn", true);
        Ani.SetBool("isFadeOut", false);

        yield return new WaitForSeconds(1);

        AsyncOperation async = SceneManager.LoadSceneAsync(name);
        async.completed += OnloadScene;

    }

    private void OnloadScene(AsyncOperation obj)
    {


        Ani.SetBool("isFadeIn", true);
        Ani.SetBool("isFadeOut", false);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
