using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for base item
public class BaseItem : MonoBehaviour
{
    public enum ItemType{Weapon, Ammo, Gas, HealthKit, Pan, Briefcase, Hammer}
    public string itemName;
    public Sprite itemUISprite;
    public GameObject itemPrefab;
    [HideInInspector]
    public Weapon weaponReferenceIfAvailable;
    public ItemType type;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            BackPack.instance.vicinityItems.Add(this);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            BackPack.instance.vicinityItems.Remove(this);
        }
    }
}
