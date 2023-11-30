using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPrefabUnit : MonoBehaviour
{
    [HideInInspector]
    public int itemCount;
    public Button dropButton;
    public Button selectButton;
    public Image itemImage;
    public TextMeshProUGUI itemNameTextObject;
    public TextMeshProUGUI itemCountTextObject;
    public bool isWeapon;
    public string weaponName;
    public BaseItem.ItemType itemType;
    void Awake()
    {
    }
    void Start()
    {
        AddListeners();
    }

    void Update()
    {
        if(itemType==BaseItem.ItemType.Ammo)
            dropButton.gameObject.SetActive(false);
    }
    void AddListeners()
    {
        selectButton.onClick.AddListener(SelectButtonClicked);
        dropButton.onClick.AddListener(DropButtonClicked);
    }

    public void SelectButtonClicked()
    {
        if(isWeapon)
        {
            UIController.instance.SetSelectedWeaponUIStatus(true);
            UIReferences.instance.gunImage.overrideSprite=itemImage.overrideSprite;
            UIReferences.instance.bulletCountText.text=Weapon.ammoCount.ToString();

            Weapon.currentWeaponName=weaponName;
        }
    }
    public void DropButtonClicked()
    {
        if(isWeapon)
            BackPack.instance.DropLastWeapon(weaponName);
        else
            BackPack.instance.DropLastItem(itemType);
    }

}