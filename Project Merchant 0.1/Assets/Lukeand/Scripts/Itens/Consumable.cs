using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item / Consumable")]
public class Consumable : Item
{


    public override Consumable GetConsumable()
    {
        return this;
    }
}
