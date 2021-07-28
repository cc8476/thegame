using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject m1;



    public void PlayGame() {
        SceneManager.LoadScene("firstScene"); 
    }


    public void QuitGame() {
        //PlayGame.destroy();
    }
}
