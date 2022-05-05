using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyUnit : MonoBehaviour
{
    //THIS SCRIPT GOES IN EVERY PARTY PREFAB.
    #region UI
    Image portrait;
    TextMeshProUGUI unitName;
    TextMeshProUGUI level;
    TextMeshProUGUI health;
    TextMeshProUGUI resource;
    TextMeshProUGUI moral;
    TextMeshProUGUI hunger;
    #endregion

    private void Awake()
    {
        portrait = transform.GetChild(0).GetComponent<Image>();
        unitName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        level = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        health = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        resource = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        moral = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        hunger = transform.GetChild(6).GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
     
        
    }
    public void SetUp(CharacterAlly character)
    {
        unitName.text = character.charBase.charName;
        level.text = $"{character.currentLevel}";
        health.text = $"{character.currentHealth} / {character.maxHealth}";

        
    }
    
    //THERE ARE BUTTONS AS WELL. 
}
