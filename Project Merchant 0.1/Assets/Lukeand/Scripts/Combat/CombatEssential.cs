using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatEssential : MonoBehaviour
{
    //THIS WILL SET UP THE COMBAT.
    //COMBAT HANDLER WILL ONLY HANDLE THE ACTUAL PROCESS
    [SerializeField] GameObject allySlotHolder;
    [SerializeField] GameObject enemySlotHolder;
    [SerializeField] GameObject turnOrderBar;
    [SerializeField] GameObject slotTemplate;

    #region LISTS
    [HideInInspector] public List<CombatSlot> totalCombatSlots = new List<CombatSlot>();
    [HideInInspector] public List<AllyCombatSlot> allySlotList = new List<AllyCombatSlot>();
    [HideInInspector] public List<EnemyCombatSlot> enemySlotList = new List<EnemyCombatSlot>();

     public List<CombatSlot> turnList = new List<CombatSlot>();
    

    #endregion



    #region SET UP
    public void SetUp(List<CharacterAlly> allyList, List<CharacterEnemy> _enemyList)
    {
        //CREATE LISTS


        //SET UP POSITIONS IN THE CANVAS.
        SetUpPositionsAlly(allyList);
        SetUpPositionsEnemy(_enemyList);


        //CREATE A LIST OF THE TURN ORDER.
        CreateTurnList();



        //I NEED TO CHECK PASSIVE TRIGGERS 
    }


    void SetUpPositionsAlly(List<CharacterAlly> allyList)
    {
        int idGiver = 0;
        List<CharacterAlly> frontAlly = new List<CharacterAlly>();
        List<CharacterAlly> backAlly = new List<CharacterAlly>();

        for (int i = 0; i < allySlotHolder.transform.childCount; i++)
        {
            allySlotHolder.transform.GetChild(i).gameObject.SetActive(false);

        }


        for (int i = 0; i < allyList.Count; i++)
        {
            //THERE IS A PREFERENCE TO PUT THE POSITION IN THE RIGHT HOLDER.
            if (allyList[i].charBase.battlePosition == BattlePosition.Front)
            {
                frontAlly.Add(allyList[i]);
                continue;
            }
            if (allyList[i].charBase.battlePosition == BattlePosition.Back)
            {
                backAlly.Add(allyList[i]);
                continue;
            }

        }
        for (int i = 0; i < frontAlly.Count; i++)
        {
            //IT GOES FIRST.
            if (GetFreeAllySlot() == null)
            {
                //
                Debug.Log("something went wrong. couldnt find slot");
                return;
            }

            allySlotList.Add(GetFreeAllySlot());
            GetFreeAllySlot().GetSlotAlly().SetUpAllySlot(idGiver, frontAlly[i]);
            idGiver += 1;

        }


        for (int i = 0; i < backAlly.Count; i++)
        {
            if (GetFreeAllySlot() == null)
            {

                Debug.Log("something went wrong. couldnt find slot");
                return;
            }
            allySlotList.Add(GetFreeAllySlot());
            GetFreeAllySlot().SetUpAllySlot(idGiver, backAlly[i]);
            idGiver += 1;
        }

    }
    void SetUpPositionsEnemy(List<CharacterEnemy> _enemyList)
    {

        //I WANT TO BE MORE DYNAMIC WITH ENEMY POSITIONS.

        int idGiver = 0;

        List<CharacterEnemy> frontline = new List<CharacterEnemy>();
        List<CharacterEnemy> backline = new List<CharacterEnemy>();

        for (int i = 0; i < _enemyList.Count; i++)
        {
            //THERE IS A PREFERENCE TO PUT THE POSITION IN THE RIGHT HOLDER.
            if (_enemyList[i].charBase.battlePosition == BattlePosition.Front)
            {
                frontline.Add(_enemyList[i]);
                continue;
            }
            if (_enemyList[i].charBase.battlePosition == BattlePosition.Back)
            {
                backline.Add(_enemyList[i]);
                continue;
            }

        }


        //DO I DO SOMETHING ABOUT EITHER HAVING MORE THAN 4 IN LIST.

        for (int i = 0; i < frontline.Count; i++)
        {
            //THEN WE PUT IN THE FRONT.
            //WE JUST PLACE IT IN THE FRONT HOLDER. 
            GameObject frontHolder = enemySlotHolder.transform.GetChild(1).gameObject;

            GameObject newObject = Instantiate(slotTemplate, frontHolder.transform.position, Quaternion.identity);
            newObject.transform.parent = frontHolder.transform;
            newObject.GetComponent<EnemyCombatSlot>().SetUp(idGiver, frontline[i]);
            idGiver += 1;
            enemySlotList.Add(newObject.GetComponent<EnemyCombatSlot>());
        }

        for (int i = 0; i < backline.Count; i++)
        {
            //ACTUALLY JUST PLACE THEM THERE.
            //RANGED ATTACKS THEN CHECK IF THERE IS ANYNOE IN THE FRONT HOLDER.

            GameObject backHolder = enemySlotHolder.transform.GetChild(0).gameObject;
            GameObject newObject = Instantiate(slotTemplate, backHolder.transform.position, Quaternion.identity);
            newObject.transform.parent = backHolder.transform;
            newObject.GetComponent<EnemyCombatSlot>().SetUp(idGiver, backline[i]);
            idGiver += 1;

            enemySlotList.Add(newObject.GetComponent<EnemyCombatSlot>());

        }



    }


    void CreateTurnList()
    {
        //PUT EVERYONE IN AN ORDER 
        for (int i = 5; i > 0; i--) AddSlotTurn(i);

        //
        UpdateTurnUI(turnList, 0);
    }
   public void UpdateTurnUI(List<CombatSlot> _turnList, int currentTurn)
    {
        //JUST PUT STUFF IN ORDER. AND WE WILL DO THAT EVERYDAY.
        GameObject turnShowerHolder = turnOrderBar.transform.GetChild(2).gameObject;


        for (int i = 0; i < turnShowerHolder.transform.childCount; i++)
        {
            turnShowerHolder.transform.GetChild(i).gameObject.SetActive(false);
        }

        //HANDLE IF THERE IS MORE.
        //HANDLE IF THERE IS LESS.

        List<CombatSlot> newCombatList = CreateListFive(_turnList, currentTurn);

        for (int i = 0; i < newCombatList.Count; i++)
        {

            GameObject newObject = turnShowerHolder.transform.GetChild(i).gameObject;
            newObject.SetActive(true);

            newObject.transform.GetChild(0).GetComponent<Image>().sprite = newCombatList[i].slotSprite;

            if (newCombatList[i].GetSlotEnemy() != null)
            {
                newObject.GetComponent<Image>().color = Color.red;
                continue;
            }

            newObject.GetComponent<Image>().color = Color.green;


        }

       

       
    }


    void AddSlotTurn(int amount)
    {
        
        for (int i = 0; i < allySlotList.Count; i++)
        {

            if (allySlotList[i].GetSlotAlly().character.statsDi[ChampionStat.Agility] == amount)
            {

                turnList.Add(allySlotList[i]);
            }
        }
        for (int i = 0; i < enemySlotList.Count; i++)
        {
            if (enemySlotList[i].GetSlotEnemy().character.statsDi[ChampionStat.Agility] == amount)
            {

                turnList.Add(enemySlotList[i]);
            }
        }
    }


    #endregion


    #region HELP FUNCTIONS
    AllyCombatSlot GetFreeAllySlot()
    {
        //WE GET THE EXACT SLOT
        for (int i = 0; i < allySlotHolder.transform.childCount; i++)
        {
            AllyCombatSlot slot = allySlotHolder.transform.GetChild(i).GetComponent<AllyCombatSlot>();
            if (!slot.ready)
            {
                return slot;
            }


        }

        return null;
    }

    List<CombatSlot> CreateListFive(List<CombatSlot> slotList, int currentTurn)
    {
        List<CombatSlot> newList = new List<CombatSlot>();

        int y = currentTurn;
        bool looped = false;
        for (int i = 0; i < 5; i++)
        {
            if (y >= slotList.Count)
            {
                y = 0;
                looped = true;
            }
            if (looped && y == currentTurn)
            {
                //THAT MEANS WE HAVE GONE ALL THE WAY BACK. RETURN AS THINGS ARE.
                return newList;
            }
            newList.Add(slotList[y]);
            y++;
        }


        return newList;


    }

    #endregion
}
