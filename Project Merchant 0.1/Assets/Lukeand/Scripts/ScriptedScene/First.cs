using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First : MonoBehaviour
{
    //THIS WILL HELP THE FIRST SCENE GO.
    [SerializeField] TextAsset firstDialogue;

    private void Start()
    {
        //IT STARTS WITH DARK BACKGROUND AND MUSIC.
        //NARRATION STARTS
        //AFTER THAT STARTS A DIALOGUE.
        //WE NEED TO HANDLE WHEN WE RETURN FROM COMBAT. WHAT HAPPENS?

        DialogueHandler.instance.SetUpDialogue(firstDialogue);

    }



}
