using Given.Manager;
using TMPro;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private Transform diceArea;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private DiceUI prefabDiceUI;


    void Start()
    {
        RebuildInventory();
        UpdateGold();
        PlayerData.OnDiceUpdated += RebuildInventory;
        PlayerData.OnGoldUpdated += UpdateGold;
    }

    [ContextMenu("CheatGold")]
    public void CheatGold()
    {
        PlayerData.Gold = 1000000000;
    }

    void RebuildInventory()
    {
        for (int i = diceArea.childCount - 1; i >= 0; i--)
        {
            Destroy(diceArea.GetChild(i).gameObject);
        }

        foreach (EDiceType ga in PlayerData.DiceInventory)
        {
            var dice = Instantiate(prefabDiceUI, diceArea.transform);
            dice.diceType = ga;
        }
    }

    private void OnDestroy()
    {
        PlayerData.OnDiceUpdated -= RebuildInventory;
        PlayerData.OnGoldUpdated -= UpdateGold;
    }

    private void UpdateGold()
    {
        goldText.text = PlayerData.Gold.ToString();
    }
    
    [ContextMenu("ClearDice")]
    public void ClearDice()
    {
        PlayerData.DiceInventory.Clear();
        PlayerData.Save();
    }
}
