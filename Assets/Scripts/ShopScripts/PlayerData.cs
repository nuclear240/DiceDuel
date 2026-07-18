using Given.Manager;
using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    private static int gold = 10;
    public static event Action OnGoldUpdated;
    public static event Action OnDiceUpdated;
    public static event Action OnCurrenyDayUpdated;
    // we can save as different combo of 1s and 0s??
    public static List<EDiceType> DiceInventory = new();
    public static int Gold
    {
        get => gold;
        set
        {
            gold = value;
            Save();
            OnGoldUpdated?.Invoke();
        }
    }
    private static int currentDay = 10;
    public static int CurrentDay => currentDay;

    public static void GoToNextDay()
    {
        currentDay += 1;
        OnCurrenyDayUpdated?.Invoke();
        Save();
    }



    public static void Save()
    {
        PlayerPrefs.SetInt("Gold", Gold);
        PlayerPrefs.SetInt("Day", currentDay);
         string beuhuh = "";

        for (int i = 0; i < DiceInventory.Count; i += 1)
        {
            beuhuh += DiceInventory[i];

            if (i < DiceInventory.Count - 1)
            {
                beuhuh += ",";
            }


        }

        PlayerPrefs.SetString("beuhuh", beuhuh);
        PlayerPrefs.Save();
    }
    [RuntimeInitializeOnLoadMethod]
    public static void Load()
    {
       
        string beuhuh = PlayerPrefs.GetString("beuhuh","Twenty,Eight,Ten,Six,Four");
        gold = PlayerPrefs.GetInt("Gold", 100);
        currentDay = PlayerPrefs.GetInt("Day", 0);
        string[] data = beuhuh.Split(",");
        DiceInventory.Clear();

        for (int i = 0; i < data.Length; i++)
        {

            if (EDiceType.TryParse(data[i], out EDiceType diceType))
                DiceInventory.Add(diceType);
            else
            Debug.LogError($"Failed to add dice { data[i] }");
            

        }
        Debug.Log($"Loading {beuhuh}");
        
    }

    internal static void AddDice(ShopDice d)
    {
        DiceInventory.Add(d.dice);
        OnDiceUpdated?.Invoke();
    }


}
