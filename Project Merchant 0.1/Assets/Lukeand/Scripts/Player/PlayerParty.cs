using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParty : MonoBehaviour
{



    #region LISTS
    public List<AllyBase> baseCharacterList = new List<AllyBase>();
    public List<CharacterAlly> partyList = new List<CharacterAlly>();

    #endregion

  

    //I NEED TO TURN THE BASE CHARACTER INTO ACTUAL CHARACTERS.

    //THE LIST NEEDS TO BE UPDATE EVERYTIME ANYTHING HAPPENS TO A CHARACTER.
    //UPDATE AFTER BATTLE.
    //NEED TO UPDATE ABOUT HUNGER.
    //I JUST NEED TO UPDATE THE ESPECIFIC ONE. 

    private void Awake()
    {
        TurnBaseIntoCharacter(baseCharacterList);

        //SEND IT TO THE UI
    }

    
    void TurnBaseIntoCharacter(List<AllyBase> baseList)
    {
        for (int i = 0; i < baseList.Count; i++)
        {
            CharacterAlly newChar = new CharacterAlly();

            newChar.SetUpCharacterAlly(baseList[i]);

            partyList.Add(newChar);
        }

        baseCharacterList.Clear();
    }

    

}
