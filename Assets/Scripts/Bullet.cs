using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Vector3 direction;
    public int speed =4;
    public float timeBeforeDestroy=2;
    private bool isFiring;
    private float startTime;
    void Update()
    {
        if(isFiring)
        {
            if(Time.time-startTime<timeBeforeDestroy)
                this.transform.position+=direction*speed*Time.deltaTime;
            else
                Destroy(this.gameObject);
        }
    }

    
    public void FireBullet(Vector3 direction)
    {
        this.direction=direction;
        isFiring=true;
        startTime=Time.time;
    }
}
