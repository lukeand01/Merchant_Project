using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ConditionButton : ButtonBase
{
    [SerializeField] ConditionBase _base = new ConditionBase();

    [SerializeField] Color buffColor;
    [SerializeField] Color debuffColor;

    [SerializeField] Image portrait;
    [SerializeField] TextMeshProUGUI quantity;
    [SerializeField] TextMeshProUGUI temporaryPortrait;
    [SerializeField] GameObject actived;
    [SerializeField] GameObject background;

    private void Start()
    {
        Observer.instance.EventConditionButton += ConditionActived;

    }

    public void SetUp(ConditionBase newBase)
    {
        _base = newBase;

        UpdateUI();
    }

    //BUFFS AND DEBUFFS SHOULD HAVE DIFFERENT COLORS


    void UpdateUI()
    {
        //temporaryPortrait.sprite = Utils.GetConditionSprite(_base.type.ToString());

        temporaryPortrait.text = _base._condition.conditionType.ToString();
        quantity.text = _base.stacks.ToString();

        if (Utils.IsBuff(_base))
        {
            background.GetComponent<Image>().color = buffColor;
        }
        else
        {
            Debug.Log("it is a debuff");
            background.GetComponent<Image>().color = debuffColor;
        }


    }    

    void ConditionActived(ConditionBase condition)
    {

        //IT SHOULD SHAKE A BIT.
       
        //WE SHOW THAT WE REMOVED.
        //quantity.text = $"{_base.stacks - 1}";
        StartCoroutine(Shake(1, 1));


    }

    IEnumerator Process()
    {
        actived.SetActive(true);
        yield return new WaitForSeconds(1);
        actived.SetActive(false);

    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        
    }

    private void OnDestroy()
    {
        Observer.instance.EventConditionButton -= ConditionActived;
    }

    IEnumerator Shake(float duration, float magnitude)
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
}
