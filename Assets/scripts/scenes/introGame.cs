using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class introGame : MonoBehaviour
{
   
    private Button nextBtn;//下一步按钮
    private Button skipBtn;//下一步按钮
    private GameObject pane1;//介绍1
    private GameObject pane2;//介绍2
    private GameObject pane3;//介绍3


    private int currentStep =1;
    void Start()
    {
        nextBtn = GameObject.Find("Canvas/nextBtn").GetComponent<Button>();
        nextBtn.onClick.AddListener(nextStep);

        skipBtn = GameObject.Find("Canvas/skipBtn").GetComponent<Button>();
        skipBtn.onClick.AddListener(skipStep);

        pane1 = GameObject.Find("Canvas/Panel1");
        pane2 = GameObject.Find("Canvas/Panel2");
        pane3 = GameObject.Find("Canvas/Panel3");

        renderStep();
    }

    void nextStep() {
        currentStep+=1;
        Debug.Log("You selected currentStep:"+currentStep);
        renderStep();
    }

    void skipStep() {
        currentStep=4;
        Debug.Log("You selected currentStep:"+currentStep);
        renderStep();
    }


    void renderStep() {
        pane1.SetActive(false);
        pane2.SetActive(false);
        pane3.SetActive(false);

        switch (currentStep)
        {
            case 1:
                pane1.SetActive(true);
            break;
            case 2:
                pane2.SetActive(true);
            break;
            case 3:
                pane3.SetActive(true);
            break;
            case 4:
                SceneManager.LoadScene("town");//sceneName
            break;
        }
    }

}
