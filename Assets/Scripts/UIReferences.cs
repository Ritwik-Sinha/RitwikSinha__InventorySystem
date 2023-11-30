using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIReferences : MonoBehaviour
{
    [Header("Outer UI")]
    public GameObject fToSelectWeapon;
    public GameObject cToCollectItem;
    public GameObject noWeaponCollection, containsWeaponCollection;
    public GameObject bagScreen;
    public TextMeshProUGUI bulletCountText; 
    public Image gunImage;
    public Button bagpackButton;

    [Header("Inventory UI")]
    public Button inventoryCrossButton;
    public GameObject inventoryContentArea;
    public GameObject inventoryContentPrefab;

    [Header("Sounds")]
    public AudioSource bulletSound;
    public AudioSource bgMusic;
    public AudioSource collectSound;
    public static UIReferences instance;
    
    [Header("Others")]
    public GameObject bulletPrefab;
    void Awake()
    {
        if(instance==null)
            instance=this;
        else
            Destroy(this);
    }
}
