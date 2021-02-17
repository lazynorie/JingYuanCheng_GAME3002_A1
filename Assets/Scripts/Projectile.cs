using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody ballPrefabs;
    public GameObject cursor;
    public Transform shootingpoint;
    public LayerMask layer;

    private Camera cam;

    void Start()
    {

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 1000f, layer))
        {
            cursor.SetActive(true);
            //set the cursor a little bit higher than the rayhit point 
            cursor.transform.position = hit.point + Vector3.up * 0.1f;

            //set initial speed of the ball
            Vector3 Vi = CalculateVelocity(hit.point, shootingpoint.position, 1f);

            //set the rotation of the player
            transform.rotation = Quaternion.LookRotation(Vi);

            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody ball = Instantiate(ballPrefabs, shootingpoint.position, Quaternion.identity);
                ball.velocity = Vi;
            }

        }
        else
        {
            cursor.SetActive(false);
        }


    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.Normalize(); 
        distanceXZ.y = 0.0f;

        float Dy = distance.y;
        float Dxz = distance.magnitude;

        //v=distance/time horizontal
        float Vxz = Dxz / time;
        //d = vt + 0.5 * a * t^2  --->  v = d/t + 0.5 * a * t
        float Vy = Dy / time + 0.5f * 9.8f * time;

        Vector3 Velocity = Vxz * distanceXZ;// keep in mind that disctanceXZ is a direction vector now
        Velocity.y = Vy;

        return Velocity;
    }
}

