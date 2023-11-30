using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Character")]
    public GameObject character;
    public Animator characterController;
    
    public static Player instance;
    void Awake()
    {
        if(instance==null)
            instance=this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
