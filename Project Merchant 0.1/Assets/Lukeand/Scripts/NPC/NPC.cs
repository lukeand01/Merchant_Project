using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

[CreateAssetMenu]
public class NPC : ScriptableObject
{
    //THIS HOLDS OPINIONS.
    //BUT IT DOESNT HOLD SPRITES, BECAUSE THEY WILL BE HELD IN RESOURCE.

    public int opinionAboutPlayer;

    //I WILL HOLD SPRITES HERE AS WELL.
    public NpcSprites npcSprite = new NpcSprites();



    public TextAsset story;

}

[System.Serializable]
public class NpcSprites
{
   public List<NpcListSprite> listSprite = new List<NpcListSprite>();

    

    public Dictionary<string, Sprite> portraitsList;
}

[System.Serializable]
public class NpcListSprite
{
    public string potraitName;
    public Sprite portraitSprite;
}