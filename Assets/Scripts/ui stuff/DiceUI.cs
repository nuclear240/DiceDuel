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
       current = dropzone.GetComponentInParent<AbilityUI>();

        if (current != null)
        {
            current.addDice(diceType);
            

        }
    }

    private void OnDisable()
    {
        dadbruv.OnDropZoneChanged -= UpdateAbility; 
    }

}
