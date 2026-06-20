using Given.Manager;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{

    [SerializeField] private DiceUI prefabDiceUI;


    void Start()
    {
        RebuildInventory();
        PlayerData.OnDiceUpdated += RebuildInventory;
    }


    void RebuildInventory()
    {
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        foreach (EDiceType ga in PlayerData.DiceInventory)
        {
            var dice = Instantiate(prefabDiceUI, transform);
            dice.diceType = ga;
        }
    }

    private void OnDestroy()
    {
        PlayerData.OnDiceUpdated -= RebuildInventory;
    }
}
