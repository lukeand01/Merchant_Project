using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterAlly : Character
{
    public CharacterBase charBase; //SO I CAN USE TO REFERENCE SOME STUFF THAT DOESNT CHANGE LIKE SCALING.


    public int currentLevel;
    public int currentXp;


    Dictionary<ChampionScaling, float> championBaseScalingList = new Dictionary<ChampionScaling, float>();
    Dictionary<ChampionOtherStats, int> championOtherStats = new Dictionary<ChampionOtherStats, int>();

    public PassiveBase passive;
    public List<SkillBase> skills;


    #region FUNCTION
    //WHAT CAN THIS DO.
    public void SetUpCharacterAlly(CharacterBase _base)
    {

        charBase = _base;
        maxHealth = _base.initialHealth;
        currentHealth = maxHealth;
        currentLevel = 1;
        maxResource = _base.initialMaxResource;
        currentResource = maxResource;

        skills = _base.initialSkillList;

        LidiStats(_base.statList);
    }
    //
    #region HEALTH
    void TakeDamage(int amount) => currentHealth -= amount;
    void HealDamage(int amount) => currentHealth += amount;
    void SetHealth(int amount) => currentHealth = amount;

    #endregion





    #endregion


    public override CharacterAlly GetAlly()
    {
        return this;
    }
}
