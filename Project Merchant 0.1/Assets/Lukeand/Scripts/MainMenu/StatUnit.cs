using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatUnit : MonoBehaviour
{
    TextMeshProUGUI title;

    GameObject increaseButton;
    GameObject decreaseButton;

    [SerializeField] PlayerStats Stat;
    int currentValue;

    private void Awake()
    {
        title = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        increaseButton = transform.GetChild(1).gameObject;
        decreaseButton = transform.GetChild(2).gameObject;
        
    }
    private void Start()
    {
        Observer.instance.EventNoMorePoints += NoMorePoints;
        Handle();
    }

    void Handle()
    {
        //UPDATE THE UI
        title.text = $"{Stat}: {currentValue}";

        increaseButton.SetActive(false);
        decreaseButton.SetActive(false);

        if(currentValue > 0)
        {
            decreaseButton.SetActive(true);
        }


        if(MainMenu.instance.creation.CurrentStat() > 0)
        {
            increaseButton.SetActive(true);
        }

    }

    public void IncreaseStat()
    {
        Observer.instance.OnChangeStats(Stat, +1);
        currentValue += 1;
        Handle();
    }
    public void DecreaseStat()
    {
        Observer.instance.OnChangeStats(Stat, -1);
        currentValue -= 1;
        Handle();
    }

    void NoMorePoints(int empty)
    {
        if(empty == 0)
        {
            increaseButton.SetActive(false);
            return;
        }

        increaseButton.SetActive(true);
    }
}
