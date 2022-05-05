using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    //THIS HANDLES THE CREATION LOGIC.

    #region INTS

    [SerializeField] int initialStatPoints;
    int currentStatPoints;
    [SerializeField] int initialSkillPoints;
    int currentSkillPoints;

    #endregion

    #region LISTS
    Dictionary<PlayerStats, int> temporaryPlayerStatList;

    #endregion

    #region UI
    [Header("UI")]
    [SerializeField] TextMeshProUGUI statsTitleUnspent;
    [SerializeField] TextMeshProUGUI skillTitleUnspent;
    [SerializeField] TMP_InputField nameInput;


    #endregion

    [Header("PLAYER")]
    [SerializeField] GameObject playerPrefab;

    private void Awake()
    {
        currentStatPoints = initialStatPoints;
        currentSkillPoints = initialSkillPoints;
        
        temporaryPlayerStatList = Utils.GetBasePlayerStats();

        AssignEvents();
        Handle();
    }

    void AssignEvents()
    {
        Observer.instance.EventChangeStats += ReceiveStat;
    }

    #region UI FUNCTIONS
    void Handle()
    {
        //UPDATE ALL UI
        statsTitleUnspent.text = $"Unspent Stat Points: {currentStatPoints}";
        skillTitleUnspent.text = $"Unspent Skill Points: {currentSkillPoints}";

        
    }

    #endregion


    #region PLAYER PORTRAIT


    #endregion
    #region PLAYER STATS
    void ReceiveStat(PlayerStats stat, int value)
    {
        temporaryPlayerStatList[stat] += value;
        currentStatPoints += value * -1;
        //UPDATE THE SKILLS SELECTION. UPDATE THE UNSPENT POINTS UI.
        Handle();

        if(currentStatPoints <= 0)
        {
            Observer.instance.OnNoMorePoints(0);
            return;
        }
        Observer.instance.OnNoMorePoints(1);
    }


    #endregion

    #region PLAYER SKILLS


    #endregion


    public int CurrentStat() 
    {
        return currentStatPoints;
    }
    public int CurrentSkill()
    {
        return currentSkillPoints;
    }

    public void Play()
    {
        //CREATE IT AND PUT IN THE GAME.
        //GIVE THE DICTIONARY.
        if(nameInput.text.Length < 3 || nameInput.text.Length > 10)
        {
            Debug.Log("need a better name");
            return;
        }
        if(currentStatPoints > 0)
        {
            Debug.Log("Need to spend stat points");
            return;
        }
        if(currentSkillPoints > 0)
        {
            Debug.Log("need to spend skill points;");
            return;
        }

        GameObject newObject = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newObject.GetComponent<PlayerHandler>().SetUpPlayer(nameInput.text, temporaryPlayerStatList);
        SceneManager.LoadScene("First");

        //AND THEN WE MOVE 
    }
}
