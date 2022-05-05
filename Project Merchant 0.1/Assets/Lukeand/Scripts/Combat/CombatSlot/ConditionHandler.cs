using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionHandler : MonoBehaviour
{
    //WE WILL DEAL WITH CONDITIONS HERE.
   public List<ConditionBase> conditionList = new List<ConditionBase>();


    [SerializeField] CombatSlot slot;
    [SerializeField] GameObject template;
    [SerializeField] GameObject conditionHolder;

    //THE GOAL: CREATE A SYSTEM THAT FITS BOTH POSITIVE AND NEGATIVES CONDITIONS.

    #region CHECK CONDITION
    public bool HasCondition(ConditionType type)
    {
        for (int i = 0; i < conditionList.Count; i++)
        {
            if (conditionList[i]._condition.conditionType == type) return true;
        }

        return false;
    }
    bool IsConditionActive(ConditionType type)
    {
        if (type == ConditionType.Bleed) return true;
        if (type == ConditionType.Poison) return true;
        if (type == ConditionType.Regrowth) return true;

        return false;
    }

    bool IsImeddiateCondition(ConditionType type)
    {
        if (type == ConditionType.Protection) return true;
      
        if(type == ConditionType.Hidden) return true;
       
        return false;

    }


    public ConditionBase GetCondition(ConditionType type)
    {
        for (int i = 0; i < conditionList.Count; i++)
        {
            if (conditionList[i]._condition.conditionType == type) return conditionList[i];
        }

        return null;
    }

    public List<ConditionBase> ActiveConditionsList()
    {
        List<ConditionBase> newList = new List<ConditionBase>();

        for (int i = 0; i < conditionList.Count; i++)
        {
            if (IsConditionActive(conditionList[i]._condition.conditionType))
            {
                newList.Add(conditionList[i]);
            }
        }

        return newList;
    }

    #endregion

    #region ADD CONDITION

    //NO CC CAN STACK.

    public void AddCondition(ConditionBase condition)
    {
        //HIDDEN CONDITION WILL BE REMOVED IF YOU DO ANYTHING.
        //




        if(condition._condition.conditionType == ConditionType.Stun && HasCondition(ConditionType.Stun))
        {
            Debug.Log("cannot stack on stun");
            return;
        }

        //DO THE CHECK FOR CC.

        //


        if (IsImeddiateCondition(condition._condition.conditionType))
        {
            
        }

        if (StackCondition(condition)) return;


        //EVERYTIME WE ADD AN IMMEDIATE CONDITION WE DO SOMETHING

        CreateCondition(condition);

    }

    bool StackCondition(ConditionBase _base)
    {
        if (HasCondition(_base._condition.conditionType))
        {
            for (int i = 0; i < conditionList.Count; i++)
            {
                if(conditionList[i]._condition.conditionType == _base._condition.conditionType)
                {
                    _base.actor = CombatHandler.instance.GetCurrentTurn();
                    conditionList[i].stacks += 1;
                    return true;
                }
            }



        }

        return false;
    }

    void CreateCondition(ConditionBase _base)
    {

        _base.actor = CombatHandler.instance.GetCurrentTurn();

        conditionList.Add(_base);
        UpdateConditionUI();
        

    }

    #endregion

    #region HANDLE CONDITIONS

    /*
  public  void HandleCondition(ConditionBase condition)
    {
        //WE HANDLE ACTIVE CONDITIONS HERE.

        ConditionType type = condition._condition.conditionType;

        if(condition._condition.conditionType == ConditionType.Bleed || condition._condition.conditionType == ConditionType.Poison)
        {
            slot.LoseHealth(condition.strenght, condition.damageType);
            return;
        }
        if(condition._condition.conditionType == ConditionType.Regrowth)
        {
            slot.RecoverHealth(condition.strenght);
            return;
        }
        
        if(condition._condition.conditionType == ConditionType.Hidden)
        {
            //WE MAKE IT LESS VISIBLE 
            Debug.Log("got here");
            var tempColor = slot.portrait.GetComponent<Image>().color;
            tempColor.a = 0.5f;

            slot.portrait.GetComponent<Image>().color = tempColor;

            Debug.Log("supposed to be insivible");
        }

        if(condition._condition.conditionType == ConditionType.Protection)
        {

        }

        if(condition._condition.conditionType == ConditionType.Overwatch)
        {
            //WE WILL LOWER THE OVERWATCH HERE

        }

        //DONT KNOW IF TEHRE ACTIVES THAT DONT DEAL DAMAGE.
       
    }
  
    */

    public void UpdateConditions()
    {
        for (int i = 0; i < conditionList.Count; i++)
        {
            conditionList[i].stacks -= 1;

            if(conditionList[i].stacks <= 0)
            {
                conditionList.RemoveAt(i);
            }
        }
        UpdateConditionUI();

    } //THIS REMOVES THE CONDITIONS.
    void UpdateConditionUI()
    {
        ClearAll(conditionHolder);

        for (int i = 0; i < conditionList.Count; i++)
        {
            GameObject newObject = Instantiate(template, conditionHolder.transform.position, Quaternion.identity);
            newObject.transform.parent = conditionHolder.transform;
            newObject.GetComponent<ConditionButton>().SetUp(conditionList[i]);
        }

    }


    public void HandleActiveConditions()
    {
        Debug.Log("handle active condition was called");
        StartCoroutine(ProcessActiveConditions());
    }

    int currentHandledCondition = 0;
   public IEnumerator ProcessActiveConditions()
    {
        //WE DO THE NARRATION. IF ITS THE LAST ONE THEN WE COME BACK OTHERWISE.
        //WONT SENT TO NARRATION. IT MIGHT GET SLOW AND BORING.
        //INSTEAD I WILL MAKE THE CONDITION SLOT SHINE AND INDEPENDETLY WE DEAL THE DAMAGE.
        //CALL NARRATION

        List<ConditionBase> conditionList = ActiveConditionsList();
        Observer.instance.OnConditionButton(conditionList[currentHandledCondition]);
        //HandleCondition(conditionList[currentHandledCondition]);
        CombatHandler.instance.controlBar.StartNarration(Utils.ConditionNarration(ActiveConditionsList()[currentHandledCondition], slot));
        currentHandledCondition += 1;

        if (currentHandledCondition > conditionList.Count - 1)
        {
            CombatHandler.instance.currentState = CombatState.ConditionsApplied;           
            currentHandledCondition = 0;
            
        }
        else
        {
            CombatHandler.instance.currentState = CombatState.HandlingConditions;
           
        }

        
        

        yield return null;
    }
    #endregion

    #region CONDITION INTERACTION


    void ConditionInteraction(ConditionType type)
    {
        //THIS IS CALLED BY AN EVENT.
        //CERTAIN CONDITION REACT TO TEH GAME.

        //HIDDEN WILL BE REMOVED IF THE USER USES ANY OTHER ABILITY.
        //OVERWATCH WILL BE REMOVED IF THE ABILITY IS REMOVED AGAIN.

        //I SHOULD DEFINE TYPES OF ACTIONS

        if (!HasCondition(type)) return; 

        if(type == ConditionType.Hidden)
        {
            //THIS SHOULD ONLY BE PROCED ONLY WE FINALIZE AN ACTION. HE CANNOT TAKE DAMAGE WHEN HIDDEN.

            RemoveCondition(ConditionType.Hidden);
        }
        if(type == ConditionType.Overwatch)
        {
            //WE REMOVE FROM THE LIST
            //BUT WE ONLY REMOVE IT IF WE ARE ATTACKED


        }
        

    }

    void RemoveCondition(ConditionType type)
    {
        for (int i = 0; i < conditionList.Count; i++)
        {
            if(conditionList[i]._condition.conditionType == type)
            {
                Debug.Log("removed");
                conditionList.RemoveAt(i);
                UpdateConditionUI();
                return;

            }
        }

        Debug.Log("didnt find it to delete it");
    }


    

    #endregion

    void ClearAll(GameObject target)
    {
        for (int i = 0; i < target.transform.childCount; i++)
        {
            Destroy(target.transform.GetChild(i).gameObject);            
        }
    }
}


//CERTAIN CONDITIONS ARE PASSIVE. THEY ONLY MATTER WHEN YOU DO SOMETHING THAT THEY EITHER ALLOW OR DONT.
//OTHERS ARE ACTIVE. EVERYTIME WE CHECK ON THEM THEY DO SOMETHING.

//ONLY CC IS CHECKED BY TENACITY.