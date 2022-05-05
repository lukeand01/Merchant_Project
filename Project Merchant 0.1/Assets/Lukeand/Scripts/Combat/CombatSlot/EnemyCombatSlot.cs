using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatSlot : CombatSlot
{
    public CharacterEnemy character; //I WILL DO THIS BECAUSE I MIGHT WANT TO SAVE THE STATE OF A MONSTER.

    public void SetUp(int _id, CharacterEnemy newChar)
    {
        id = _id;
        character = newChar;

        slotSprite = character.charBase.charSprite;
        slotName = character.charBase.charName;


        maxHealth = character.charBase.initialHealth;
        currentHealth = maxHealth;

        maxResource = character.charBase.initialMaxResource;
        currentResource = maxResource;
        UpdateUI();
        ready = true;
    }


    void UpdateUI()
    {
        portrait.GetComponent<Image>().sprite = character.charBase.charSprite;
        slotNameText.text = character.charBase.charName;

        SetHealth();
        SetResource(character.charBase.resourceType);

    }

    public override void UseReource(int amount)
    {
        base.UseReource(amount);
        SetResource(character.charBase.resourceType);
    }

    //AI HERE. BUT 

    public void HandleEnemyTurn()
    {
        //HE WILL MAKE A CHOICE.
        //HE NEEDS TO MAKE A GOOD CHOICE. 
        //WHAT CHARACTERS DOES HE WNAT TO ATTACK?
        //WHAT ABILITIES HE WANTS TO USE.
        //THEY CAN ALSO TARGET THEMSELVS OR ALLIES.


        int skillRoll = Random.Range(0, character.skills.Count);

        SkillBase chosenSkill = character.skills[skillRoll];

        //


        if (chosenSkill.targetType == TargetType.Self)
        {
            //APPLY DIRECTLY TO HIMSELF.
        }

        if (chosenSkill.targetType == TargetType.Enemy)
        {
            TargettingEnemies(chosenSkill);

            return;
        }

        if (chosenSkill.targetType == TargetType.Ally)
        {
            TargettingAllies(chosenSkill);
            return;
        }


        Debug.Log("got to the end here");

    }

    void TargettingEnemies(SkillBase skill)
    {

        List<AllyCombatSlot> list = CombatHandler.instance.allySlotList;

        int targetRoll = Random.Range(0, list.Count - 1);

        if(skill.range == BattlePosition.Front && list[targetRoll].currentBattlePosition == BattlePosition.Back)
        {
            //IF THERE IS NOT ENOUGH RANGE WE SIMPLY DO IT AGAIN.
            TargettingEnemies(skill);
            Debug.Log("the chosen skill doesnt have good range");
            return;
        }

        

        list[targetRoll].SufferAction(skill);

    }
    void TargettingAllies(SkillBase skill)
    {
        List<EnemyCombatSlot> list = CombatHandler.instance.enemySlotList;

        int targetRoll = Random.Range(0, list.Count);


        //YOU SHOULD FOCUS ON HEALING CHARACTESR THAT LACK HEALTH.

        list[targetRoll].SufferAction(skill);


    }




    #region DEATH
    public override void Death()
    {
        base.Death();
        CombatHandler.instance.enemyDeadList.Add(this);
    }

    #endregion

    public override EnemyCombatSlot GetSlotEnemy()
    {
        return this;
    }
}
