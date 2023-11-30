using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class ClearChild {
    public static Transform Clear(this Transform trans)
    {
        for (int i = trans.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(trans.GetChild(i).gameObject);
        }
        return trans;
    }
    public static Transform ClearImmediate(this Transform trans)
    {
        for (int i = trans.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(trans.GetChild(i).gameObject);
        }
        return trans;
    } 
 }