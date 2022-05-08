using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillActionButton : ButtonBase
{
   [HideInInspector] public SkillBase skill;
    CombatSlot owner;

    [SerializeField] GameObject selected;
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] TextMeshProUGUI skillCost;
    [SerializeField] TextMeshProUGUI skillDamage;

    bool aiming;

    private void Start()
    {
        Observer.instance.EventAllowTarget += HandleSelected;
    }

    private void Update()
    {
        if (aiming)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //THEN WE STOP IT.
                aiming = false;
                Observer.instance.OnAllowTarget(TargetType.Null, null);
            }
        }
    }

    public void SetUp(CombatSlot slot, SkillBase newSkill)
    {
        skill = newSkill;

        owner = slot;

        skillName.text = skill.skillName;
        skillCost.text = skill.resourceCost.ToString();


        if(skill.damageType == DamageType.Physical)
        {
            skillDamage.color = Color.black;
        }
        if (skill.damageType == DamageType.Magical)
        {
            skillDamage.color = Color.blue;
        }
        if (skill.damageType == DamageType.True)
        {
            skillDamage.color = Color.gray;
        }
        if (skill.damageType == DamageType.Heal)
        {
            skillDamage.color = Color.green;
        }

        skillDamage.text = skill.strenght.ToString();


    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        //IF WE CLICK THIS THEN WE START THE TARGETING.
        //UNLESS IT IS SELF THEN WE JUST USE IT INSTANTLY.


        //I NEED TO FIRST CHECK IF THERE IS ENOUGH RESOURCES FOR THAT.

        if (owner.CanUseResource(skill.resourceCost))
        {

            if(skill.targetType == TargetType.Self)
            {
                //THEN WE ACTIVED INSTANTLY.

                //I NEED TO CHANGE THE SUFFER ACTION.
                CombatHandler.instance.GetCurrentTurn().SufferAction(skill, owner);
                return;

            }


            Observer.instance.OnAllowTarget(skill.targetType, skill);
            selected.SetActive(true);
            aiming = true;
        }
        else
        {
            //THEN WE SAY TAHT WE CANNOT USE THAT ABILITY.
        }


        

        //WE START AN EVENT THAT REQUIRES A GOAL TO BE MET IN ORDER TO DO IT.
        //AFTER THIS IS CLICKED WE NEVER MORE RETURN HERE.
       
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Observer.instance.OnSkillDescriber(skill.skillDescription + Utils.AdditionalDescription(skill));
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        Observer.instance.OnSkillDescriber("");
    }

    void HandleSelected(TargetType targetType, SkillBase _skill)
    {
        if (skill == null || _skill == null) return;
        if (_skill.skillName == skill.skillName) return;
        selected.SetActive(false);

    }

    private void OnDestroy()
    {
        Observer.instance.EventAllowTarget -= HandleSelected;
    }

}

//HOW TO APPLY THE DAMAGE AND STUFF LIKE THAT.
//THERE ARE FOUR TYPES OF ABILITIES.