using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScore : MonoBehaviour
{
   
    public void Death()
    {
        
        Destroy(gameObject);
        
    }
    public void PlaySound()
    {
        FindObjectOfType<PlayConrtoll>().PlayAudio();
        FindObjectOfType<PlayConrtoll>().ScoreCount();
    }
}
