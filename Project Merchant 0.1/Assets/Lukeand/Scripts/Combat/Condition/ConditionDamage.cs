using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Condition / Damage")]
public class ConditionDamage : Condition
{
    //THIS CAN BE EITHER POSITIVE OR NEGATIVE.

    private void Awake()
    {
        conditionType = ConditionType.Damage;
    }


}

