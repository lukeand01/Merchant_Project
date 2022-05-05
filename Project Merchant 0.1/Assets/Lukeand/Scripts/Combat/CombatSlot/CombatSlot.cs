using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CombatSlot : ButtonBase
{
    

    #region COMPONENTS
    [HideInInspector] public GameObject selected;
    [HideInInspector] public GameObject currentTurn;
    [HideInInspector] public GameObject portraitBackground;
    [HideInInspector] public GameObject portrait;
    [HideInInspector] public GameObject conditionHolder;
    [HideInInspector] public GameObject passiveTrigger;
    [HideInInspector] public TextMeshProUGUI slotNameText;

    [HideInInspector] public GameObject healthHolder;
    [HideInInspector] public Image healthBar;
    [HideInInspector] public TextMeshProUGUI healthBarText;

    [HideInInspector] public GameObject resourceHolder;
    [HideInInspector] public Image resourceBar;
    [HideInInspector] public TextMeshProUGUI resourceBarText;

    [HideInInspector] public GameObject animationHolder;
    [HideInInspector] public GameObject positionHolder;
    [HideInInspector] public TextMeshProUGUI damageText;
    [HideInInspector] public TextMeshProUGUI resourceText;
    #endregion

    #region SCRIPTS
    [SerializeField] public ConditionHandler conditionHandler;


    #endregion

    #region BOOLS

    [SerializeField] bool inRight;
    bool animationProcess;

    public bool ready;
    public bool target;
    public bool dead;
    #endregion

    #region  STATS
    public int maxHealth;
    public int currentHealth;

    public int maxResource;
    public int currentResource;


    #endregion

    #region BUFFS
    //HERE WE HOLD A LIST OF STATS. THOSE LISTS OF STATS COUNT AS ADDITIONAL STATS EVERYTIME WE MUST 
    //IN THE START WE CHECK IF THERE ARE ALREADY PRESENT BUFFS GRANTED BY WEAPON OR SPECIAL CONDITIONS.


    //I NEED TO THINK ABOUT BETTER ABOUT CHARACTER STATS. THE PLAYER STATS ARE FINE.



    #endregion



    #region IMPORTANT PARTS

    [HideInInspector] public BattlePosition currentBattlePosition;
    [HideInInspector] public int id;
    [HideInInspector] public Sprite slotSprite;
    [HideInInspector] public string slotName;
    [SerializeField] GameObject conditionTemplate;
    int turnsPlayed; //SO WE CAN CHECK CERTAINS PASSIVES.
    #endregion

    //I THINK CHANGE THIS WHEN THE COMPUTER REALIZES THERE IS NO MORE FRONT LINE.
    //ONLY ONE.

    //MAYBE I SHOULD PLACE THE ENEMY SLOTS INSTEAD OF SETTING THEM MANUALLY.

    SkillBase currentSkill;

    private void Awake()
    {
        GetReferences();

        if (inRight) RevertPart();
    }

    private void Start()
    {
        Observer.instance.EventAllowTarget += HandleTargeting;
    }


    #region BASE FUNCTIONS
    void GetReferences()
    {
        currentTurn = transform.GetChild(0).gameObject;
        portraitBackground = transform.GetChild(1).gameObject;
        portrait = transform.GetChild(2).gameObject;
        conditionHolder = transform.GetChild(3).gameObject;
        passiveTrigger = transform.GetChild(4).gameObject;
        slotNameText = transform.GetChild(5).GetComponent<TextMeshProUGUI>();

        healthHolder = transform.GetChild(6).gameObject;
        healthBar = healthHolder.transform.GetChild(0).GetComponent<Image>();
        healthBarText = healthHolder.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        resourceHolder = transform.GetChild(7).gameObject;
        resourceBar = resourceHolder.transform.GetChild(0).GetComponent<Image>();
        resourceBarText = resourceHolder.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        animationHolder = transform.GetChild(8).gameObject;
        positionHolder = transform.GetChild(9).gameObject;
        damageText = transform.GetChild(10).GetComponent<TextMeshProUGUI>();
        resourceText = transform.GetChild(11).GetComponent<TextMeshProUGUI>();

        selected = transform.GetChild(12).gameObject;
    }
    void RevertPart()
    {
        //WE PUT SOME STUFF IN THE OTHER SIDE.
        damageText.transform.localPosition = new Vector3(-70, damageText.transform.localPosition.y, 0);
        resourceText.transform.localPosition = new Vector3(-70, resourceText.transform.localPosition.y, 0);

        passiveTrigger.transform.localPosition = new Vector3(-70, passiveTrigger.transform.localPosition.y, 0);
    }

    public void ChangePosition(BattlePosition newPosition)
    {
        currentBattlePosition = newPosition;

        //I THINK I ALSO NEED TO MOVE ITS POSITION.
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        //I SHOULD BE ABLE TO GET INFORMATION ABOUT SOMEONE WITH THIS.

        if(eventData.button == PointerEventData.InputButton.Left && target)
        {
            //THIS ISNT RIGHT STILL.
            //EVENTUALLY I WANT TO CLICK ON IT AND TEH GAME REALIZE THAT I HAVE MULTIPLE TARGETS.
            //FOR NOW THIS IS FINE.

            //I ALSO NEED TO CARE ABOUT RANGE.

            //THERE WILL BE A BOOLEAN. MULTIPLE OR SINGLE. IF ITS SINGLE THEN ITS DIRECT OTHERWISE WE SELECT THIS FELLA.
            //ALSO CHECK IF I HAVE SELECTED ENOUGH.
            SufferAction(null);
            //
           
        }
        if (eventData.button == PointerEventData.InputButton.Right && !target)
        {
            //DESCRIBER
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (target)
        {
            selected.SetActive(true);
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (target)
        {
            selected.SetActive(false);
        }
    }

    #endregion

    #region COMBAT
    void HandleTargeting(TargetType targetType, SkillBase skill)
    {
        if (targetType == TargetType.Null)
        {
            target = false;

            return;
        }

        currentSkill = skill;




        if (targetType == TargetType.Enemy)
        {

            if(GetSlotEnemy() != null)
            {
                target = true;
            }
            if(GetSlotAlly() != null)
            {
                target = false;
            }
            
            return;
        }

        if(targetType == TargetType.Ally)
        {
            if (GetSlotEnemy() != null)
            {
                target = false;
            }
            if (GetSlotAlly() != null)
            {
                target = true;
            }
            return;
        }


        if(targetType == TargetType.Any)
        {
            target = true;
            return;
        }


    }

    public void SufferAction(SkillBase skill)
    {


        if(skill != null)
        {
            currentSkill = skill;
        }
        //OVERWATCH DOESNT WORK IN GROUP ATTACKS.

        if (conditionHandler.HasCondition(ConditionType.Overwatch))
        {
            //INSTEAD THE OVERWATCH TAKES IT.
            //THIS IS NOT EVEN A BIT GOOD FOR 


        }

        if (currentSkill.GetSS() != null)
        {
            if (conditionHandler.HasCondition(ConditionType.Overwatch))
            {
                //THEN WE ATTACK SOMEONE ELSE.
                //WE NEED TO SAY THAT IT ACTUALL DEFENDED BY SOMEONE ELSE.

                ConditionBase condition = conditionHandler.GetCondition(ConditionType.Overwatch);

                Debug.Log($"the {slotName} was defended by {condition.actor}");
                HandleSingleAction(condition.actor);
            }       
            HandleSingleAction(this);
        }



        StartCoroutine(ShakeSlot(1, 1));
    }

    void HandleSingleAction(CombatSlot actualTarget)
    {
        currentSkill.GetSS().Action(actualTarget);
        CombatHandler.instance.Action(currentSkill.GetSS(), actualTarget);
        target = false;
        selected.SetActive(false);
        Observer.instance.OnAllowTarget(TargetType.Null, null);
    }

    void HandleMultipleAction()
    {

    }

    public void HandleTurn(bool ownTurn)
    {


        turnsPlayed += 1;
        //MAKE IT A BIT BIGGER.
        //BUT IF I HOVER OVER ANY SLOT IT {SOBREPOR}
        RectTransform thing = this.gameObject.GetComponent<RectTransform>();
        currentTurn.SetActive(ownTurn);

        if (ownTurn)
        {
            
            thing.localScale = new Vector3(1.1f, 1.1f);
        }
        else
        {
            thing.localScale = new Vector3(1f, 1f);
        }

        

    }

    
    public bool UnableToMove() 
    {
        //ALSO: IF HAS FEAR AND HAS JUST ONE TARGET.


        if (conditionHandler.HasCondition(ConditionType.Stun)) return true;

        return dead;

    }

    #endregion


    #region COMBAT - SHAKE

    void DecideShake()
    {
        //DECIDE IF IT SHAKES OR NOT BUT I WONT USE IT FOR NOW.


    }
    IEnumerator ShakeSlot(float duration, float magnitude)
    {
        //WE RECEIVE DAMAGE. DEPENDING OF THE DAMAGE WE SHAKE.
        //CRITS SHOULD SHAKE A LOT.\

        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {

            float x = Random.Range(-1, 1);
            float y = Random.Range(-1, 1);

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;

        }

        transform.localPosition = originalPosition;

    }


    #endregion


    #region CONDITIONS AND BUFFS
    public bool HandleConditions()
    {
  

        bool theBool = UnableToMove();
        conditionHandler.UpdateConditions();

        return theBool;

    }

    public void AddBuff()
    {
        //ADD BUFF TO THIS SLOT. BUFFS HAVE A 

    }



    #endregion

    #region HEALTH FUNCTIONS

    public void SetHealth()
    {
        TextMeshProUGUI healthText = healthHolder.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        healthText.text = $"{currentHealth} / {maxHealth}"; 

        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
    public void RecoverHealth(int amount)
    {
        HandleDamageAnimation(amount);

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        SetHealth();
    }
    public void LoseHealth(int amount, DamageType type)
    {
        //SHOULD I DIVIDE THE DAMAGE IN DIFFERENT NUMBERS.
        //int newAmount = Utils.DamageMitigationCalculation()


        HandleDamageAnimation(-amount);
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);



        if(currentHealth <= 0)
        {
            //THEN WE DIE.
            DeathOrder();
        }

        SetHealth();
    }
    void HandleDamageAnimation(int amount)
    {
        if (animationProcess)
        {
            StopAllCoroutines();
            StartCoroutine(DamageEffect(amount));
        }
        else
        {
            StartCoroutine(DamageEffect(amount));
        }
    }
    IEnumerator DamageEffect(int amount)
    {
        //SHOULD ADD COLOR DEPENDING ON AMOUNT AND CRIT OR TYPE.

        //THERE IS THE DAMAGE AND THE TYPES OF DAMAGE.



        animationProcess = true;
        //START INCREASING THE NUMBER
        damageText.gameObject.SetActive(true);
        if (amount > 0) damageText.text = $"+{amount}";
        if (amount <= 0) damageText.text = $"{amount}";

        damageText.fontSize = 25;
        damageText.color = new Color(1, 1, 1, 1);

        for (int i = 0; i < 4; i++)
        {
            damageText.fontSize += 10f;

            yield return new WaitForSeconds(0.1f);
        }


        //THEN FADE IT
        yield return new WaitForSeconds(0.5f);

        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            damageText.color = new Color(1, 1, 1, i);
            yield return null;
        }

        damageText.gameObject.SetActive(false);
        animationProcess = false;

    }
    #endregion

    #region RESOURCE FUNCTIONS


    public bool CanUseResource(int amount)
    {
        if(currentResource >= amount)
        {
            return true;
        }
        return false;
    }
    public virtual void UseReource(int amount)
    {
        currentResource -= amount;
      
    }

    public void SetResource(ResourceType type)
    {
        TextMeshProUGUI resourceText = resourceHolder.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (type == ResourceType.Stamina)
        {
            resourceBar.color = Color.green;
            resourceText.color = Color.black;
        }
        if(type == ResourceType.Mana)
        {
            resourceBar.color = Color.blue;
            resourceText.color = Color.white;
        }
        if(type == ResourceType.Energy)
        {
            resourceBar.color = Color.yellow;
            resourceText.color = Color.black;
        } 

        resourceText.text = $"{currentResource} / {maxResource}";
        resourceBar.fillAmount = (float)currentResource/ maxResource;
    }


    #endregion

    #region DEATH

    public void DeathOrder()
    {
        //THIS IS WHERE WE TELL THE HANDLER THAT WE NEED TO TAKE CARE OF THE DEATH.
        dead = true;
        CombatHandler.instance.waitingHandling.Add(this);

    }

    public virtual void Death()
    {
        ClearAll(conditionHolder);

        ShakeSlot(1, 1);
        portrait.GetComponent<Image>().sprite = CombatHandler.instance.deathSprite;

        RectTransform rect = this.gameObject.GetComponent<RectTransform>();
        rect.localScale = new Vector3(0.8f, 0.8f, 0.8f);


        //I NEED TO REMOVE THIS CHARACTER FROM TurnLIST.
        //THEN WE CONTINUE.
        CombatHandler.instance.RemoveFromTurnList(this);
    }

    #endregion


    void ClearAll(GameObject target)
    {
        for (int i = 0; i < target.transform.childCount; i++)
        {
            Destroy(target.transform.GetChild(i).gameObject);
        }
    }


    public virtual AllyCombatSlot GetSlotAlly()
    {
        return null;
    }
    public virtual EnemyCombatSlot GetSlotEnemy()
    {
        return null;
    }
}
