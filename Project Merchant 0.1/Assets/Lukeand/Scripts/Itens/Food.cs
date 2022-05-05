using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item / Food")]
public class Food : Item
{
    

    public override Food GetFood()
    {
        return this;
    }
}
