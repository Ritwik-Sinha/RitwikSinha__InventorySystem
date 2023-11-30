using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    void Awake()
    {
        if(instance==null)
            instance=this;
        else
            Destroy(this);
    }
}