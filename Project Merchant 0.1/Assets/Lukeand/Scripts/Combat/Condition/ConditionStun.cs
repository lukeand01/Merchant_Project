using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Condition / Stun")]
public class ConditionStun : Condition
{

    private void OnEnable()
    {
        conditionType = ConditionType.Stun;
    }

}
