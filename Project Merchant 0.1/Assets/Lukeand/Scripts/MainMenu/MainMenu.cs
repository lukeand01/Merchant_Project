using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //THIS WILL CONTROL JUST THE UI NONE OF THE LOGIC.

    public static MainMenu instance;

    [Header("PANELS")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject saveSlotPanel;
    [SerializeField] GameObject creationPanel;


    #region COMPONENTS
   [HideInInspector] public CharacterCreation creation;

    #endregion

    private void Awake()
    {
        instance = this;
        creation = GetComponent<CharacterCreation>();
    }

    public void Play()
    {
        //GO TO SAVE. WHICH THEN GOES TO EITHER THE GAME OR CREATION.


    }
    public void Settings()
    {
        //JUST SOUND SETTINGS FOR NOW.

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Back()
    {
        //THIS ALWAYS RETURN TO THE LAST THING. I WILL SIMPLY USE A SWITCH STATEMENT TO DECIDED THAT.
    }

}
