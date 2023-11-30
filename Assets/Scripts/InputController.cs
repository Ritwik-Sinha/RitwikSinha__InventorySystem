using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles all the input
public class InputController : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(Weapon.ammoCount>0 && UIReferences.instance.containsWeaponCollection.activeInHierarchy)
            {
                var pos=Player.instance.character.transform.position+new Vector3(0,0.8f,0)+Player.instance.character.transform.forward*1;
                var obj=Instantiate(UIReferences.instance.bulletPrefab, pos, Player.instance.character.transform.rotation);
                var scr=obj.GetComponent<Bullet>();
                scr.FireBullet(Player.instance.character.transform.forward);
                UIReferences.instance.bulletSound.Play();

                BackPack.instance.otherBagItemsTypesCountDictionary[BaseItem.ItemType.Ammo]-=1;
                Weapon.ammoCount-=1;
                UIReferences.instance.bulletCountText.text=Weapon.ammoCount.ToString();
                
                if(BackPack.instance.otherBagItemsTypesCountDictionary[BaseItem.ItemType.Ammo]==0)
                    BackPack.instance.otherBagItemsTypesCountDictionary.Remove(BaseItem.ItemType.Ammo);
                BackpackVisualizer.instance.RefreshList();
            }
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            BackPack.instance.CollectItemToBackpack();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(UIReferences.instance.bagScreen.activeInHierarchy)
                UIReferences.instance.bagScreen.SetActive(false);
        }       
    }
}
