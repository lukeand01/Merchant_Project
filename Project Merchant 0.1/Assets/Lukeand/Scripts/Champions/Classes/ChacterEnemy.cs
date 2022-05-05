using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnemy : Character
{
    //THE POURPOSE OF THIS IS TO PUT THE BASE IN THE AND HOLD A TEMPORARY INFORMATION.
    //I MIGHT NEED.
    public EnemyBase charBase;

    public PassiveBase passive;
    public List<SkillBase> skills;


    public void SetUpCharacterEnemy(EnemyBase _charBase)
    {
        charBase = _charBase;

        passive = _charBase.passive;
        skills = _charBase.initialSkillList;
         

        LidiStats(_charBase.statList);

    }

    public override CharacterEnemy GetEnemy()
    {
        return this;
    }
}
