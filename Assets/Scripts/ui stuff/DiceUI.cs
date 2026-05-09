using Given.Manager;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
    private draginobject dadbruv;

    public EDiceType diceType { get => type; set { type = value; bruh.sprite = DataManager.Instance.DiceSprites [ (int) type ]; } }
    private EDiceType type;
    [SerializeField] Image bruh;
    private AbilityUI current;

     void Awake()
    {
        dadbruv = GetComponent<draginobject>();
    }

    private void OnEnable()
    {
        dadbruv.OnDropZoneChanged += UpdateAbility;
    }

    private void UpdateAbility(dropzone dropzone)
    {
        current?.removeDice(diceType);
        AbilityUI idkagain = dropzone.GetComponent<AbilityUI>();

        if (idkagain != null)
        {
            idkagain.addDice(diceType);
            current = idkagain;

        }
    }

    private void OnDisable()
    {
        dadbruv.OnDropZoneChanged -= UpdateAbility; 
    }

}
