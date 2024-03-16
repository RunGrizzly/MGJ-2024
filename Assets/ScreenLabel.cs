using TMPro;
using UnityEngine;

public class ScreenLabel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private RectTransform _rectTransform;

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
    }

    public void SetHeight(float height)
    {
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, height);
    }
}