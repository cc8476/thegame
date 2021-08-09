using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    private SpriteRenderer sr;
    private Image im;
    // Start is called before the first frame update
    void Start()
    {

        sr = GameObject.Find("attack").GetComponent<SpriteRenderer>();
        im = GameObject.Find("Canvas/Image").GetComponent<Image>();

        im.sprite = sr.sprite;


        StartCoroutine(api());
    }

    IEnumerator api()
    {
        yield return new WaitForSeconds(2);  // 看上去只有在协程中使用

        //animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //animator.enabled = false;
        //animator.transform.position = new Vector3(1, 1, 0);

    }
}
