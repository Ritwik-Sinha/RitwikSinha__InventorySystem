using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    // public List<BaseItem> otherBagItems;
    public Dictionary<BaseItem.ItemType,int> otherBagItemsTypesCountDictionary;
    public Dictionary<BaseItem.ItemType,Sprite> otherBagItemsSpriteDictionary;
    public Dictionary<BaseItem.ItemType,GameObject> otherBagItemsPrefabDictionary;
    // public List<Weapon> weaponItems;
    public Dictionary<string,int> weaponItemTypesCountDictionary;
    public Dictionary<string,Sprite> weaponItemSpriteDictionary;
    public Dictionary<string,GameObject> weaponItemPrefabDictionary;
    public List<BaseItem> vicinityItems;
    public static BackPack instance;
    void Awake()
    {
        if(instance==null)
            instance=this;
        else
            Destroy(this);

        // otherBagItems=new List<BaseItem>();
        // weaponItems=new List<Weapon>();
        
        otherBagItemsTypesCountDictionary=new Dictionary<BaseItem.ItemType, int>();
        weaponItemTypesCountDictionary=new Dictionary<string, int>();

        otherBagItemsSpriteDictionary=new Dictionary<BaseItem.ItemType, Sprite>();
        weaponItemSpriteDictionary=new Dictionary<string, Sprite>();

        otherBagItemsPrefabDictionary=new Dictionary<BaseItem.ItemType, GameObject>();
        weaponItemPrefabDictionary=new Dictionary<string, GameObject>();

    }

    void Update()
    {
        if(vicinityItems.Count>0)
            UIReferences.instance.cToCollectItem.SetActive(true);
        else
            UIReferences.instance.cToCollectItem.SetActive(false);
    }
    public void CollectItemToBackpack()
    {
        var closesItem=FindClosestItem();
        if(closesItem==null) return;

        if(closesItem.weaponReferenceIfAvailable!=null)
        {
            var weaponComp=closesItem.GetComponent<Weapon>();
            // weaponItems.Add(weaponComp);
            Weapon.currentWeaponName=weaponComp.itemName;
            
            if(weaponItemTypesCountDictionary.ContainsKey(weaponComp.itemName))
                weaponItemTypesCountDictionary[weaponComp.itemName]+=1;
            else
                weaponItemTypesCountDictionary[weaponComp.itemName]=1;
            
            if(!weaponItemSpriteDictionary.ContainsKey(weaponComp.itemName))
                weaponItemSpriteDictionary[weaponComp.itemName]=weaponComp.itemUISprite;
            
            if(!weaponItemPrefabDictionary.ContainsKey(weaponComp.itemName))
                weaponItemPrefabDictionary[weaponComp.itemName]=weaponComp.itemPrefab;
        }
        else
        {
            // otherBagItems.Add(closesItem);

            if(otherBagItemsTypesCountDictionary.ContainsKey(closesItem.type))
            {
                if(closesItem.type==BaseItem.ItemType.Ammo)
                    otherBagItemsTypesCountDictionary[closesItem.type]+=30;
                else
                    otherBagItemsTypesCountDictionary[closesItem.type]+=1;
            }
            else
            {
                if(closesItem.type==BaseItem.ItemType.Ammo)
                    otherBagItemsTypesCountDictionary[closesItem.type]=30;
                else
                    otherBagItemsTypesCountDictionary[closesItem.type]=1;
            }
            
            if(!otherBagItemsSpriteDictionary.ContainsKey(closesItem.type))
                otherBagItemsSpriteDictionary[closesItem.type]=closesItem.itemUISprite;
            
            if(!otherBagItemsPrefabDictionary.ContainsKey(closesItem.type))
                otherBagItemsPrefabDictionary[closesItem.type]=closesItem.itemPrefab;
            
            if(closesItem.type==BaseItem.ItemType.Ammo)
                Weapon.ammoCount=otherBagItemsTypesCountDictionary[closesItem.type];
        }
        Destroy(closesItem.gameObject);
        vicinityItems.Remove(closesItem);

        UIReferences.instance.collectSound.Play();
        Player.instance.characterController.SetTrigger("Pickup");
        BackpackVisualizer.instance.RefreshList();
    }
    public void DropItem(BaseItem item)
    {
        if(item.weaponReferenceIfAvailable!=null)
        {
            var weaponComp=item.GetComponent<Weapon>();
            // weaponItems.Remove(weaponComp);
            
            if(weaponItemTypesCountDictionary[weaponComp.itemName]>1)
                weaponItemTypesCountDictionary[weaponComp.itemName]-=1;
            else
                weaponItemTypesCountDictionary.Remove(weaponComp.itemName);
        }
        else
        {
            // otherBagItems.Remove(item);

            if(otherBagItemsTypesCountDictionary[item.type]>1)
                otherBagItemsTypesCountDictionary[item.type]-=1;
            else
                otherBagItemsTypesCountDictionary.Remove(item.type);
        }

        BackpackVisualizer.instance.RefreshList();
    }

    public void DropLastWeapon(string weaponName)
    {
        if(weaponItemTypesCountDictionary[weaponName]>1)
            weaponItemTypesCountDictionary[weaponName]-=1;
        else
            weaponItemTypesCountDictionary.Remove(weaponName);

        var prefObj=weaponItemPrefabDictionary[weaponName];
        var pos=Player.instance.character.transform.position+new Vector3(0,0.5f,0)+Player.instance.character.transform.forward*1;
        Instantiate(prefObj, pos, Quaternion.identity);

        BackpackVisualizer.instance.RefreshList();
    }
    public void DropLastItem(BaseItem.ItemType type)
    {
        if(otherBagItemsTypesCountDictionary[type]>1)
            otherBagItemsTypesCountDictionary[type]-=1;
        else
            otherBagItemsTypesCountDictionary.Remove(type);
        
        var prefObj=otherBagItemsPrefabDictionary[type];
        var pos=Player.instance.character.transform.position+new Vector3(0,0.5f,0)+Player.instance.character.transform.forward*1;
        Instantiate(prefObj, pos, Quaternion.identity);
        
        BackpackVisualizer.instance.RefreshList();
    }

    public BaseItem FindClosestItem()
    {
        if(vicinityItems.Count==0)
            return null;
        BaseItem closestItem=vicinityItems[0];
        float lowestDist=(vicinityItems[0].transform.position-Player.instance.character.transform.position).magnitude;
        foreach (var item in vicinityItems)
        {
            var itemDist=(item.transform.position-Player.instance.character.transform.position).magnitude;
            if(itemDist<lowestDist)
            {
                lowestDist=itemDist;
                closestItem=item;
            }
        }
        return closestItem;
    }
}
