using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    void Awake()
    {
        if(instance==null)
            instance=this;
        else
            Destroy(this);
    }
    void Start()
    {
        AddListeners();

        UIReferences.instance.fToSelectWeapon.SetActive(false);
        UIReferences.instance.cToCollectItem.SetActive(false);
        SetSelectedWeaponUIStatus(false);
    }
    void Update()
    {
        if(Weapon.ammoCount>0)
            UIReferences.instance.fToSelectWeapon.SetActive(true);
        else
            UIReferences.instance.fToSelectWeapon.SetActive(false);
    }

    void AddListeners()
    {
        UIReferences.instance.bagpackButton.onClick.AddListener(BagPackButtonClicked);
        UIReferences.instance.inventoryCrossButton.onClick.AddListener(InventoryCrossButtonClicked);
    }

    public void BagPackButtonClicked()
    {
        UIReferences.instance.bagScreen.SetActive(true);
    }
    public void InventoryCrossButtonClicked()
    {
        UIReferences.instance.bagScreen.SetActive(false);
    }
    
    public void SetSelectedWeaponUIStatus(bool active)
    {
        if(active)
        {
            UIReferences.instance.noWeaponCollection.SetActive(false);
            UIReferences.instance.containsWeaponCollection.SetActive(true);
        }
        else
        {
            UIReferences.instance.noWeaponCollection.SetActive(true);
            UIReferences.instance.containsWeaponCollection.SetActive(false);
        }
    }
}
