using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    //THIS WHERE THE LEVEL CHANGES AND WE HOLD EVERYTHING ELSE.
    //I DONT THINK ENEMIES NEED THAT. BECAUSE I DONT WANT TO HOLD THEIR PROGRESS.
 
    public int maxHealth;
    public int currentHealth;
    public int preferedPosition;

    public int maxResource;
    public int currentResource;  


    public Dictionary<ChampionStat, int> statsDi = new Dictionary<ChampionStat, int>();

    

    public void LidiStats(List<CharacterStatsList> statList)
    {
        for (int i = 0; i < statList.Count; i++)
        {
            statsDi.Add(statList[i].stat, statList[i].value);
        }


    }

    public virtual CharacterAlly GetAlly()
    {
        return null;
    }
    public virtual CharacterEnemy GetEnemy()
    {
        return null;
    }
}


