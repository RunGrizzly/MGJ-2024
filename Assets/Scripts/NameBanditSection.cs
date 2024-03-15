using TMPro;
using UnityEngine;

public class NameBanditSection : MonoBehaviour
{
    [SerializeField] private WordList _wordList;
    [SerializeField] private float _delay = 0.25f;
    private float _summit = 0f;
    private int _index = 0;
    private TMP_Text _text;
    private bool isRunning = true;

    public void Start()
    {
        _text = GetComponent<TMP_Text>();
        UpdateText();
    }

    public void Update()
    {
        if (!isRunning) return;
        
        _summit += Time.deltaTime;
        
        if (_summit > _delay)
        {
            _index = (_index + 1) % _wordList.Words.Count;
            UpdateText();
            _summit = 0;
        }
    }

    private void UpdateText()
    {
        _text.text = _wordList.Words[_index];
    }

    public string Stop()
    {
        isRunning = false;
        return _wordList.Words[_index];
    }
}
