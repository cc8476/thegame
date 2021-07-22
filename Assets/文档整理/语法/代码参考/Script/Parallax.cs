using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;
    public float moveRate;
    private float StartPoint;
    void Start()
    {
        StartPoint = transform.position.x;
    }

    
    void Update()
    {
        transform.position = new Vector2(StartPoint - Cam.position.x * moveRate, transform.position.y);
    }
}
