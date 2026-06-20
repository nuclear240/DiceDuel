using Given.Manager;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    private static int gold = 10;
    public static event Action OnGoldUpdated;
    public static event Action OnDiceUpdated;
    // we can save as different combo of 1s and 0s??
    public static List<EDiceType> DiceInventory = new();
    public static int Gold
    {
        get => gold;
        set
        {
            gold = value;
            Save();
            OnGoldUpdated();
        }
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("Gold", Gold);
    }

    public static void Load()
    {

    }

    internal static void AddDice(ShopDice d)
    {
        DiceInventory.Add(d.dice);
        OnDiceUpdated?.Invoke();
    }


}
