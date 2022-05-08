using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CombatHandler : MonoBehaviour
{
    public static CombatHandler instance;


    [Header("ENEMY TESTE")]
    [SerializeField] List<EnemyBase> TESTEenemyListBase = new List<EnemyBase>();
    //BEFORE ANYTHING I NEED TO TAKE THIS AND PUT IN THE ACTUAL LIST JUST FOR THE TEST.


    [Header("SPRITES")]
    [HideInInspector] public Sprite deathSprite;


    CombatEssential essential;
    [SerializeField] public ControlBar controlBar; 

    #region LISTS
   [SerializeField] List<CharacterAlly> characterList = new List<CharacterAlly>();
    List<CharacterEnemy> enemyList = new List<CharacterEnemy>();

    [HideInInspector]  public List<CombatSlot> totalCombatSlots = new List<CombatSlot>();
    [HideInInspector] public List<AllyCombatSlot> allySlotList = new List<AllyCombatSlot>();
    [HideInInspector] public List<EnemyCombatSlot> enemySlotList = new List<EnemyCombatSlot>();

    [HideInInspector] public List<CombatSlot> waitingHandling = new List<CombatSlot>();

    [HideInInspector] public List<AllyCombatSlot> allyDeadList = new List<AllyCombatSlot>();
    [HideInInspector] public List<EnemyCombatSlot> enemyDeadList = new List<EnemyCombatSlot>();


    List<CombatSlot> turnList = new List<CombatSlot>();
    int currentTurn;

    List<CombatSlot> multipleSlotChosen = new List<CombatSlot>();
    #endregion

    #region CONDITIONS LIST
    //THSE ARE LISTS TO USE LATER. EXAMPLE: DEATH, PASSIVE PROC.
    //WE NEED TO SOLVE THESE LISTS AS SOON AS WE CAN.



    #endregion

    #region EVENTS
    //I WILL USE EVENTS TO TRIGGER STUFF.


    #endregion


    public CombatState currentState;

    private void Start()
    {
        instance = this;

        essential = GetComponent<CombatEssential>();
        TesteTranslateBase(TESTEenemyListBase);
        CreateLists();
        essential.SetUp(characterList, enemyList);
        GetNewLists();
        Presentation();
    }
    void TesteTranslateBase(List<EnemyBase> baseList)
    {
        //AND WE TURN INTO A NORMAL LIST
        for (int i = 0; i < baseList.Count; i++)
        {
            CharacterEnemy enemyChar = new CharacterEnemy();
            enemyChar.SetUpCharacterEnemy(baseList[i]);
            enemyList.Add(enemyChar);
        }
    }

    #region SET UP
    void CreateLists()
    {
        //GET ALLY LIST AND 
        characterList = PlayerHandler.instance.GetPartyList();

        //enemyList = enemyList;

    }
    void GetNewLists()
    {
        totalCombatSlots = essential.totalCombatSlots;
        allySlotList = essential.allySlotList;
        enemySlotList = essential.enemySlotList;
        turnList = essential.turnList;
    }
    #endregion


    #region MAIN TURN FUNCTIONS
    //WHAT ARE THE MAIN TURNS?
    void Presentation()
    {
        //THIS IS ALWAYS CALLED IN THE BEGGINING AND ONLY IN THE BEGGINING.

        //HERE WE DESCRIBE THE SCENE A BIT.
        //TELL ABOUT IMPORTANT INFO: LIKE VISIBILITY.
        //

        //THE PROCESS IS WE CALL NARRATION.
        //THEN WE CALL THE TRANSITION.
        //TRANSITION DECIDES WHO IS NEXT TO PLAY AND IF THERE IS ANYTHING WORTH ATTENTION BEFORE THAT.

        currentState = CombatState.Presentation;
        turnList[currentTurn].HandleTurn(true);
        controlBar.StartNarration(Utils.PresentationNarration());

    }
    void AllyTurn()
    {
        //THIS IS WHEN WE CHECK THAT ITS AN ALLY TURN
        //AN ALLY TURN CONSISTS OF GETTING THE MOVES AND THEN MAKING THE CHOICES.



        controlBar.AllyTurn(turnList[currentTurn].GetSlotAlly());


    }
    void EnemyTurn()
    {
        //THIS IS WHEN WE CHECK THAT ITS AN ENEMY TURN
        //GIVE THE NARRATION SOMETHING TO SAY. JUST FOR NOW.

        //WE TELL IT THAT THE ENEMY WILL PLAY NOW. 
        //WE TELL ANY OTHER STATS.
        //DO WE TELL CONDITIONS?

        currentState = CombatState.EnemyTurn;
        controlBar.StartNarration(Utils.EnemyTurnNarration(turnList[currentTurn].GetSlotEnemy()));
       

    }
    void SecondEnemyTurn()
    {
        //THIS IS WHERE WE CHECK CONDITIONS LIST.


        turnList[currentTurn].GetSlotEnemy().HandleEnemyTurn();
    }
    #endregion

    #region TURN FUNCTIONS

    public void Transition()
    {
        //TRANSITIONS HANDLES THE RETURN OF FUNCTION.
        //BASICALLY WHEN WE COME BACK FROM NARRATION WE RETURN TO WHERE WE WERE.

        if(currentState == CombatState.EnemyTurn)
        {
            SecondEnemyTurn();
            return;
        }

        if (currentState == CombatState.Presentation)
        {
            //THAT CHOOSE WHO PLAYS NOW.
            HandleTurn(false);
            return;
        }
        if (currentState == CombatState.CombatEnd)
        {
            //THIS IS PLAYED IN THE END. WE GO TO THE NEXT TURN.

            NextTurn();
            return;

        }
        if(currentState == CombatState.HandlingConditions)
        {
            //WE WILL CALL EVERY ACTIVE CONDITION IN THE TEXT.
            Debug.Log("there are more conditions");
            turnList[currentTurn].conditionHandler.HandleActiveConditions();

            return;
        }
        if(currentState == CombatState.ConditionsApplied)
        {
            Debug.Log("handling conditions was finalized");
            HandleTurn(true);
            return;
        }
       
        

    }
    void HandleTurn(bool conditionsApplied)
    {

        if (turnList[currentTurn].conditionHandler.ActiveConditionsList().Count > 0 && !conditionsApplied)
        {
            //THERE ARE FELLAS TO TAKE CARE. WE MUST ALWAYS PROCESS FIRST THE HEALER ONES FIRST.
            //WE LEAVE THIS FUNCTION.
            //WE GO TO ANOTHER FUNCTION HANDLE EVERY SINGLE ONE THEN WE RETURN HERE WITH A TRUE BOOLEAN.
            Debug.Log("got sent here");
            currentState = CombatState.HandlingConditions;
            GetCurrentTurn().conditionHandler.HandleActiveConditions();

            return;

        }

        if (turnList[currentTurn].HandleConditions()) //EITHER STUNNED OR DEAD.
        {
            currentState = CombatState.CombatEnd;            
            controlBar.StartNarration(Utils.CCNarration(turnList[currentTurn]));                      
            return;
        }

        

        essential.UpdateTurnUI(turnList, currentTurn);
        //THIS IS WHEN WE GIVE THE CURRENT PLAYER THE TURN CONTROL.
        if (turnList[currentTurn].GetSlotAlly() != null) AllyTurn();
        if (turnList[currentTurn].GetSlotEnemy() != null) EnemyTurn();
    }
    void NextTurn()
    {
        turnList[currentTurn].HandleTurn(false);
        currentTurn += 1;

        if (currentTurn >= turnList.Count)
        {
            currentTurn = 0;
        }

        
        turnList[currentTurn].HandleTurn(true);

        if (waitingHandling.Count > 0) //THIS IS IN CASE SOMEONE DIED.
        {
            //THEN WE CALL IT.
            StartCoroutine(HandleDeath());
            return;
        }



        HandleTurn(false);
    }

   public void Action(SkillBase skill, CombatSlot target)
    {
        currentState = CombatState.CombatEnd;      
        controlBar.StartNarration(Utils.CombatNarration(skill, target, turnList[currentTurn]));
         
    }

    #endregion

    #region COMBAT FUNCTIONS

    
    public void HandleMultipleList(bool shouldAdd, CombatSlot chosenSlot)
    {
        


        if (shouldAdd)
        {
            multipleSlotChosen.Add(chosenSlot);

        }
        if (!shouldAdd)
        {
            for (int i = 0; i < multipleSlotChosen.Count; i++)
            {
                if(chosenSlot.id == multipleSlotChosen[i].id)
                {
                    multipleSlotChosen.RemoveAt(i);
                }

            }
        }
    }


    #endregion

    #region DEATH
    IEnumerator HandleDeath()
    {

        controlBar.HandleActionBar(false);

        for (int i = 0; i < waitingHandling.Count; i++)
        {

            waitingHandling[i].Death();

            yield return new WaitForSeconds(1);
        }

        waitingHandling.Clear();

        if(GetEnemyAlive().Count == 0)
        {
            //WIN
            Debug.Log("you have won");
            WonCombat();
            yield return null;
        }
        if(GetAllyAlive().Count == 0)
        {
            //LOSE
            Debug.Log("you have lost");
            LostCombat();
            yield return null;
        }

        if(GetAllyAlive().Count > 0 && GetEnemyAlive().Count > 0)
        {
            HandleTurn(false);
        }
        
        yield return null;
    }

    #endregion


    #region END COMBAT
    //


    void WonCombat()
    {

    }
    void LostCombat()
    {

    }




    #endregion


    #region HELP FUNCTIONS

    public void RemoveFromTurnList(CombatSlot slot)
    {

        for (int i = 0; i < turnList.Count; i++)
        {

            if(turnList[i].id == slot.id && turnList[i].slotName == slot.slotName)
            {
                turnList.RemoveAt(i);
                return;
            }


        }


    }

    List<EnemyCombatSlot> GetEnemyAlive()
    {
        List<EnemyCombatSlot> newList = new List<EnemyCombatSlot>();

        for (int i = 0; i < enemySlotList.Count; i++)
        {
            if (enemySlotList[i].currentHealth <= 0)
            {
                continue;
            }
            newList.Add(enemySlotList[i]);
        }

        return newList;

    } 

    List<AllyCombatSlot> GetAllyAlive()
    {
        List<AllyCombatSlot> newList = new List<AllyCombatSlot>();

        for (int i = 0; i < allySlotList.Count; i++)
        {
            if (allySlotList[i].currentHealth <= 0)
            {
                continue;
            }
            newList.Add(allySlotList[i]);
        }

        return newList;
    }

    public CombatSlot GetCurrentTurn()
    {
        return turnList[currentTurn];
    }

    #endregion
}




public enum CombatState
{
    Presentation,
    CombatEnd,
    EnemyTurn,
    TurnPresentation,
    HandlingConditions,
    ConditionsApplied

}