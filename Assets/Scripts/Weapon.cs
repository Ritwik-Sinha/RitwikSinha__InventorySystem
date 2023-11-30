using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BaseItem
{
    public enum WeaponType{Shotgun, Pistol, Assault, Sniper, Smg}
    public WeaponType weaponType;
    public static string currentWeaponName;
    public static int ammoCount;
    void Start()
    {
        weaponReferenceIfAvailable=this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
