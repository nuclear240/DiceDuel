using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CharUI : MonoBehaviour
{
    [SerializeField] basechar owner;
    [SerializeField] Image helth;
    [SerializeField] TextMeshProUGUI text;

    public void LateUpdate()
    {
        helth.fillAmount = (float) owner.CurrentHealth / owner.MaxHealth;
        text.text = owner.shield.ToString();
    }
}
