using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Observer : MonoBehaviour
{
    public static Observer instance;

    private void OnEnable()
    {
        if (instance != null) Destroy(this.gameObject);
        if (instance == null) instance = this;

    }


    #region MAIN MENU UI

    public event Action<PlayerStats, int> EventChangeStats;
    public void OnChangeStats(PlayerStats stat, int value) => EventChangeStats?.Invoke(stat, value);

    public event Action<int> EventNoMorePoints;
    public void OnNoMorePoints(int empty) => EventNoMorePoints?.Invoke(empty);
    #endregion

    #region UI GENERAL



    #endregion

    #region UI PLAYER

    public event Action<PlayerUIButtonType> EventChangePlayerUI;
    public void OnChangePlayerUI(PlayerUIButtonType type) => EventChangePlayerUI?.Invoke(type);
    #endregion

    #region CITY


    #endregion

    #region MAP PLACES

    public event Action<MapPlace> EventArrivedPlace;
    public void OnArrivedPlace(MapPlace targetPlace) => EventArrivedPlace?.Invoke(targetPlace);
    #endregion

    #region PLAYER INVENTORIES 
    public event Action<int> EventHandleMenu;
    public void OnHandleMenu(int nothing) => EventHandleMenu?.Invoke(nothing);

    public event Action<int, List<ItemEntity>> EventOpenCrate;
    public void OnOpenCrate(int id, List<ItemEntity> entityList) => EventOpenCrate?.Invoke(id, entityList);

    public event Action<List<CrateInventory>> EventChangeWagon;
    public void OnChangeWagon(List<CrateInventory> crateList) => EventChangeWagon?.Invoke(crateList);

    public event Action<ItemEntity> EventOpenObserver;
    public void OnOpenObserver(ItemEntity entity) => EventOpenObserver?.Invoke(entity);
    #endregion

    #region PRE-DUNGEON
    public event Action<string, List<CharacterAlly>, List<ItemEntity>> EventPreDungeonPrepare;
    public void OnPreDungeonPrepare(string name, List<CharacterAlly> charList, List<ItemEntity> itemList) => EventPreDungeonPrepare?.Invoke(name, charList, itemList);

    public event Action<int, bool, CharacterAlly> EventPreChangeAlly;
    public void OnPreChangeAlly(int id, bool choice, CharacterAlly newAlly) => EventPreChangeAlly?.Invoke(id, choice, newAlly);

    public event Action<int> EventPreHandleAllySlot;
    public void OnPreHandleAllySlot(int id) => EventPreHandleAllySlot?.Invoke(id);


    public event Action<bool, ItemEntity> EventPreChangeItem;
    public void OnPreChangeItem(bool choice, ItemEntity newEntity) => EventPreChangeItem?.Invoke(choice, newEntity);
    #endregion

    #region COMBAT
    public event Action<string> EventSkillDescriber;
    public void OnSkillDescriber(string newString) => EventSkillDescriber?.Invoke(newString);

    public event Action<TargetType, SkillBase> EventAllowTarget;
    public void OnAllowTarget(TargetType target, SkillBase skill) => EventAllowTarget?.Invoke(target, skill);

    public event Action<ConditionBase> EventConditionButton;
    public void OnConditionButton(ConditionBase _base) => EventConditionButton?.Invoke(_base);



    #endregion
}

