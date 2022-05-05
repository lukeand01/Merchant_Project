using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item / Equipable")]
public class Equipable : Item
{
    //I WANT THE CLASS TO EQUIP HERE.
    //DO I GET DIFFERENT TYPES OF SWORD INHERIT FROM THIS CLASS? 

    public override Equipable GetWeapon()
    {
        return this;
    }
}
