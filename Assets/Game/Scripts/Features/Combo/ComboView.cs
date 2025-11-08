using TMPro;
using UnityEngine;

public class ComboView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] Transform effects;
    [SerializeField] string textBeforeScore;

    public void SetCombo(int combo, HitType hitType)
    {
        label.text = textBeforeScore + combo.ToString() + "X " + hitType.ToString();
    }

    public void ResetCombo()
    {
        label.text = textBeforeScore + "0";
    }
}
