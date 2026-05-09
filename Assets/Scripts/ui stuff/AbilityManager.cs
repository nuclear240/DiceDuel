using Given.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private Transform abilityParent;
    [SerializeField] private AbilityUI abilityPrefab;
    [SerializeField] private Transform diceParent;
    [SerializeField] private DiceUI diceUIPrefab;

    List<AbilityUI> activeAbilitiesList = new();
    AnilityBase[] activeAbilities;

    public void GenerateDiceUI(EDiceType[] dice)
    {
        foreach (EDiceType type in dice)
        {
            DiceUI diceUI = Instantiate(diceUIPrefab, diceParent );
            diceUI.diceType = type;
        }
    }



    public void createUI()
    {
        foreach (AnilityBase ability in activeAbilities)
        {
            AbilityUI activeAbility = Instantiate(abilityPrefab, abilityParent);
            activeAbility.SetAbility(ability);
            activeAbilitiesList.Add(activeAbility);
        }
    }



    public void RegenerateAbilities(AnilityBase[] activeAbilities)
    {
        foreach (AbilityUI abilityUI in activeAbilitiesList)
            Destroy(abilityUI.gameObject);
        activeAbilitiesList.Clear();
        this.activeAbilities = activeAbilities;
        createUI();
    }
}
