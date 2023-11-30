using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateContinously : MonoBehaviour
{
    public GameObject rotateObject;

    [HideInInspector]
    public float angley;
    void Start()
    {
        angley=0;
        if(rotateObject==null)
            rotateObject=this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        angley+=Time.deltaTime*100;
        rotateObject.transform.localRotation=Quaternion.Euler(0,angley,0);
    }
}