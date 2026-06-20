using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{

    public Button buy;
    public int price;
    public int stock;
    public int minstock;
    public int maxstock;
    public TextMeshProUGUI cash;
    public TextMeshProUGUI stocky;
    public Item myItem;
    public Image iconspritelino;

     public void SetPrice(int newprice)
    {
        price = newprice;
        cash.text = price.ToString();
        buy.interactable = PlayerData.Gold >= price;
    }

     void Start()
    {
        SetPrice(myItem.price);
        PlayerData.OnGoldUpdated += UpdateButton;
        buy.onClick.AddListener(BUY);
        iconspritelino.sprite = myItem.icon;

    }

    void UpdateButton()
    {
        buy.interactable = PlayerData.Gold >= price;
    }

    private void OnDestroy()
    {
        PlayerData.OnGoldUpdated -= UpdateButton;
    }

    void BUY()
    {
        PlayerData.Gold -= price;
        if( myItem is ShopDice d )
        {
            PlayerData.AddDice(d);
        }
    }

}
