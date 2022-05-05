using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonalUIController : MonoBehaviour
{
    //THIS GOES IN THE PLAYER UI. IT CONTROLS THE STUFF. THIS RECEIVES THE INFO.

    public static PersonalUIController instance;

    #region UI
    GameObject menuHolder;
    GameObject playerPanel;
    GameObject inventoryPanel;
    GameObject partyPanel;
    GameObject questPanel;
    GameObject mostrumPanel;
    GameObject characterPanel;

    #endregion



    #region TEMPLATES
    [Header("INVENTORY")]
    [SerializeField] GameObject crateTemplate;
    [SerializeField] GameObject itemTemplate;

    [Header("PARTY")]
    [SerializeField] GameObject partyTemplate;


    #endregion

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        
        
    }

    
    
    void GetReference()
    {
        menuHolder = transform.GetChild(0).gameObject;
        playerPanel = menuHolder.transform.GetChild(1).gameObject;
        inventoryPanel = menuHolder.transform.GetChild(2).gameObject;
        partyPanel = menuHolder.transform.GetChild(3).gameObject;
        questPanel = menuHolder.transform.GetChild(4).gameObject;
        mostrumPanel = menuHolder.transform.GetChild(5).gameObject;
        characterPanel = menuHolder.transform.GetChild(6).gameObject;


        Observer.instance.EventChangePlayerUI += ChangePlayerUI;
        Observer.instance.EventChangeWagon += HandleWagon;
        Observer.instance.EventOpenCrate += OpenCrate;
        Observer.instance.EventHandleMenu += HandleMenu;
    }

    
    private void Start()
    {
        GetReference();

    }

    #region BASE UI
    void ChangePlayerUI(PlayerUIButtonType type)
    {

        if(type == PlayerUIButtonType.Player)
        {
            CloseOtherUI(1);
            return;
        }
        if (type == PlayerUIButtonType.Inventory)
        {

            CloseOtherUI(2);
            return;
        }
        if (type == PlayerUIButtonType.Party)
        {

            CloseOtherUI(3);
            return;
        }
        if (type == PlayerUIButtonType.Quest)
        {

            CloseOtherUI(4);
            return;
        }
        if (type == PlayerUIButtonType.Monstorium)
        {

            CloseOtherUI(5);
            return;
        }
        if (type == PlayerUIButtonType.Upgrade)
        {

            CloseOtherUI(6);
            return;
        }

    }

    void CloseOtherUI(int choice)
    {
        for (int i = 1; i < menuHolder.transform.childCount; i++)
        {
            if (choice == i) 
            {
                menuHolder.transform.GetChild(i).gameObject.SetActive(true);
                continue;
            };

            menuHolder.transform.GetChild(i).gameObject.SetActive(false);

        }
    }

    void HandleMenu(int nothing)
    {
        //THIS OPENS OR CLOSES THE UI.
        if (menuHolder.activeInHierarchy) menuHolder.SetActive(false);
        else menuHolder.SetActive(true);
    }

    public bool IsMenuOpen()
    {
        if (menuHolder.activeInHierarchy)
        {
            return true;
        }
        return false;
    }
    #endregion


    #region PLAYER

   public void UpdatePlayerUI(PlayerHandler handler)
    {
        Debug.Log("i should update the player ui " + handler.playerStatList.Count);
        
        //WHERE DO I CALL THIS?
        //I JUST GET INFO AND PUT IT HERE. 
        playerPanel.transform.GetChild(0).GetComponent<Image>().sprite = handler.playerPortrait;
        playerPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = handler.playerName;
        playerPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{handler.playerLevel}";

        GameObject statsHolder = playerPanel.transform.GetChild(5).gameObject;
        statsHolder.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Speech: {handler.playerStatList[PlayerStats.Speech]}";
        statsHolder.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Barter: {handler.playerStatList[PlayerStats.Barter]}";
        statsHolder.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"Logistic: {handler.playerStatList[PlayerStats.Logistic]}";
        statsHolder.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"Medicine: {handler.playerStatList[PlayerStats.Medicine]}";
        statsHolder.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = $"Wisdom: {handler.playerStatList[PlayerStats.Wisdom]}";
        statsHolder.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = $"Luck: {handler.playerStatList[PlayerStats.Luck]}";

        //IF THERE IS AN LEVEL TO UPGRADE MAKE THE BUTTON APPEAR.

    }

    #endregion

    #region INVENTORY

 
    public void HandleWagon(List<CrateInventory> crateList)
    {       
        GameObject wagon = inventoryPanel.transform.GetChild(1).gameObject;

        ClearAll(wagon);
        for (int i = 0; i < crateList.Count; i++)
        {
            GameObject newObject = Instantiate(crateTemplate, wagon.transform.position, Quaternion.identity);
            newObject.transform.parent = wagon.transform;
            newObject.GetComponent<CrateInventory>().SetUp(i, crateList[i]);    
        }

    }

    void OpenCrate(int id, List<ItemEntity> entityList)
    {
        //GameObject 
        GameObject crateShower = inventoryPanel.transform.GetChild(2).gameObject;
        GameObject contentHolder = crateShower.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;

        //INCREASE THE CONTENTHOLDER BUT I WILL DO THAT LATER.
        ClearAll(contentHolder);

        for (int i = 0; i < entityList.Count; i++)
        {
            GameObject newObject = Instantiate(itemTemplate, contentHolder.transform.position, Quaternion.identity);
            newObject.transform.parent = contentHolder.transform;
            newObject.GetComponent<ItemInventory>().SetUp(entityList[i]);
        }


    }
    void OpenItemDescriber(ItemEntity entity)
    {
        Debug.Log("open describer");



    }
    #endregion

    #region PARTY
    public void UpdatePartyUI(List<CharacterAlly> charList)
    {
 
        //THIS IS WHERE WE PUT THE PREFABS IN THE UI.
        GameObject holder = partyPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
        ClearAll(holder);

        //ALSO NEED TO CHANGE THE HOLDER SIZE BASED ON THE NUMBER OF BASTARDS.
        RectTransform holderRect = holder.GetComponent<RectTransform>();

        for (int i = 0; i < charList.Count; i++)
        {
            //holderRect.sizeDelta = new Vector3(holderRect.sizeDelta.x, holderRect.sizeDelta.y + 50, 0);
            GameObject newObject = Instantiate(partyTemplate, holder.transform.position, Quaternion.identity);
            newObject.transform.parent = holder.transform;
            newObject.GetComponent<PartyUnit>().SetUp(charList[i]);
        }

    }


    #endregion

    #region QUEST
    public void UpdateQuestUI()
    {

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
