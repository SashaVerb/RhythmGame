using System.Collections;
using TMPro;
using UnityEngine;

public partial class TextEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private float _duration;

    public void Show(string text)
    {
        _label.text = text;
        StartCoroutine(ShowProcess());
    }

    private IEnumerator ShowProcess()
    {
        yield return new WaitForSeconds(_duration);
        Destroy(gameObject);
    }
}
