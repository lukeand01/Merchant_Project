using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    //THIS SCRIPT IS THE SOUL OF THE PLAYER.
    //IT HOLDS REFERENCES TO ALL OTHER SCRIPTS.

    public static PlayerHandler instance;

    #region PLAYER INFO
    [HideInInspector] public string playerName;
    [HideInInspector] public Sprite playerPortrait;
    [HideInInspector] public int playerLevel;
    [HideInInspector] public Vector3 mapPosition;
    #endregion

    #region COMPONENTS
    PlayerControl control;
    PlayerParty party;
    PlayerInventory inventory;
    PlayerMapControl mapControl;
    PersonalUIController UIController;
    #endregion


    #region LISTS
   public Dictionary<PlayerStats, int> playerStatList = new Dictionary<PlayerStats, int>();

    #endregion

    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;
        DontDestroyOnLoad(this.gameObject);
        GetReferences();
    }

    private void Start()
    {
        //I WILL TELL ALL THE UI COMPONENTS TO DO ITS THING.
        //UpdateAllUI();

        //I WILL HANDLE HERE EVERYTIME I GET IN A MAP.
    }

    void GetReferences()
    {
        control = GetComponent<PlayerControl>();
        party = GetComponent<PlayerParty>();
        inventory = GetComponent<PlayerInventory>();
        mapControl = GetComponent<PlayerMapControl>();


        //UIController = GameObject.Find("PlayerMenu").GetComponent<PersonalUIController>();
    }
    public void SetUpPlayer(string _playerName, Dictionary<PlayerStats, int> _newStatList)
    {
        playerName = _playerName;
        playerStatList = _newStatList;
        //HOW TO GET ORIGINAL POSITION AND HOW TO GET PERSONAL UI?

        SetMapPosition(new Vector3(-8, 0, -10));
    }

    #region UI

    void UpdateAllUI()
    {

        //UpdatePlayerUI(); //THIS WONT BE TRIGGERED NOW BECAUSE THE STATS COME FROM THE PLAYER CREATION.
        //INVENTORY DOESNT NEED TO BE CALLED BECAUSE IT ALREADY IS WHEN IT GETS A NEW ITEM.
        UpdatePartyUI();

    }

    void UpdatePlayerUI() => UIController.UpdatePlayerUI(this);

   
    void UpdatePartyUI() => UIController.UpdatePartyUI(party.partyList);

    //void UpdateQuestUI() => UIController.UpdateQuestUI();





    #endregion


    #region PARTY

    public List<CharacterAlly> GetPartyList() => party.partyList;

    #endregion

    #region INVENTORY
    public List<ItemEntity> GetUsefulItem() => inventory.GetUsefulItens();

    #endregion

    public void SetMapPosition(Vector3 newPosition) => mapPosition = new Vector3(newPosition.x, newPosition.y, -10);


    public void EnterCity(GameObject originalGate) => mapControl.EnterCity(originalGate);
   


    private void OnLevelWasLoaded(int level)
    {
        //WE CHECK WHAT KIND OF LEVEL IT IS AND WE DISABLE OR ENABLE THE MAP MOVEMENT.
        
    }
}
