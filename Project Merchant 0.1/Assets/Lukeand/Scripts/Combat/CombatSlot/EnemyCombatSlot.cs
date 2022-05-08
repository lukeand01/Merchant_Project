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
        List<AllyCombatSlot> allyList = CombatHandler.instance.allySlotList;
        List<EnemyCombatSlot> enemyList = CombatHandler.instance.enemySlotList;
        List<SkillBase> skillList = character.skills;

        character.GetEnemy().charBase.enemyAI.Act(this, allyList, enemyList, skillList);       
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
