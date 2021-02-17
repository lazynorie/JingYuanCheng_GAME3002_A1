using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class keepermovement : MonoBehaviour
{
    private Vector3 pos1 = new Vector3(-2, 0, 1);
    private Vector3 pos2 = new Vector3(2, 0, 1);
    public float speed = 1.0f;

    void Update()
    {
        //PingPong for moving the objective back and forth b/w two points
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
