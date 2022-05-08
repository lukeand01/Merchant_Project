using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllyCombatSlot : CombatSlot
{
    public CharacterAlly character;

    public void SetUpAllySlot(int _id, CharacterAlly newChar)
    {
        id = _id;
        character = newChar;

        slotSprite = character.charBase.charSprite;
        slotName = character.charBase.charName;


        portrait.GetComponent<Image>().sprite = newChar.charBase.charSprite;


        if(currentBattlePosition == BattlePosition.Front)
        {
            positionHolder.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1";
        }
        if(currentBattlePosition == BattlePosition.Back)
        {
            positionHolder.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "2";
        }



        maxHealth = character.maxHealth;
        currentHealth = character.currentHealth;

        maxResource = character.maxResource;
        currentResource = character.currentResource;

        currentLevel = character.currentLevel;

        

        UpdateUI();


        this.gameObject.SetActive(true);
        ready = true;
    }
    public void RemoveIt()
    {
        character = null;

        //THEN MAKE THE UI EMPTY;

        ready = false;
    }

    void UpdateUI()
    {
        slotNameText.text = character.charBase.charName;
        SetHealth();
        SetResource(character.charBase.resourceType);
    }

    public override void UseReource(int amount)
    {
        base.UseReource(amount);
        SetResource(character.charBase.resourceType);
    }


    #region DEATH

    public override void Death()
    {
        base.Death();
        CombatHandler.instance.allyDeadList.Add(this);
    }


    #endregion

    public override AllyCombatSlot GetSlotAlly()
    {
        return this;
    }

}
