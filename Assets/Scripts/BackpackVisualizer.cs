using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackVisualizer : MonoBehaviour
{
    public List<InventoryPrefabUnit> itemUnitsUI;
    public static BackpackVisualizer instance;
    void Awake()
    {
        if(instance==null)
            instance=this;
        else
            Destroy(this);
    }

    void Start()
    {
        itemUnitsUI=new List<InventoryPrefabUnit>();
        UIReferences.instance.inventoryContentArea.transform.Clear();
    }

    public void RefreshList()
    {
        UIReferences.instance.inventoryContentArea.transform.Clear();
        itemUnitsUI.Clear();
        
        if(BackPack.instance.weaponItemTypesCountDictionary.Count==0)
            UIController.instance.SetSelectedWeaponUIStatus(false);
        else
            UIController.instance.SetSelectedWeaponUIStatus(true);
        
        var weaponFound=false;
        foreach (KeyValuePair<string, int> kvp in BackPack.instance.weaponItemTypesCountDictionary)
        {
            var obj=Instantiate(UIReferences.instance.inventoryContentPrefab, UIReferences.instance.inventoryContentArea.transform);
            var scr=obj.GetComponent<InventoryPrefabUnit>();
            itemUnitsUI.Add(scr);

            scr.itemCount=kvp.Value;
            scr.itemCountTextObject.text=kvp.Value.ToString();
            scr.itemNameTextObject.text=kvp.Key.ToString();
            scr.weaponName=kvp.Key.ToString();
            scr.isWeapon=true;

            scr.itemImage.overrideSprite=BackPack.instance.weaponItemSpriteDictionary[kvp.Key];

            if(Weapon.currentWeaponName==scr.weaponName)
            {
                scr.SelectButtonClicked();
                weaponFound=true;
            }
        }

        if(!weaponFound && BackPack.instance.weaponItemTypesCountDictionary.Count>0)
        {
            var scr=itemUnitsUI.Find((x)=>{return x.isWeapon;});
            if(scr!=null)
                scr.SelectButtonClicked();
        }

        foreach (KeyValuePair<BaseItem.ItemType,int> kvp in BackPack.instance.otherBagItemsTypesCountDictionary)
        {
            var obj=Instantiate(UIReferences.instance.inventoryContentPrefab, UIReferences.instance.inventoryContentArea.transform);
            var scr=obj.GetComponent<InventoryPrefabUnit>();
            itemUnitsUI.Add(scr);

            scr.itemCount=kvp.Value;
            scr.itemCountTextObject.text=kvp.Value.ToString();
            scr.itemNameTextObject.text=nameof(kvp.Key).ToString();
            scr.weaponName=nameof(kvp.Key).ToString();
            scr.isWeapon=false;
            scr.itemType=kvp.Key;

            scr.itemImage.overrideSprite=BackPack.instance.otherBagItemsSpriteDictionary[kvp.Key];
        }
    }
}
