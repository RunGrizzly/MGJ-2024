using TMPro;
using UnityEngine;



public enum WordType { Verb, Adverb, Adjective, Animal }

public class NameBanditSection : MonoBehaviour
{
    [SerializeField] private WordList _wordList;
    [SerializeField] private WordType _wordType = WordType.Verb;
    [SerializeField] private float _delay = 0.25f;
    private float _summit = 0f;
    private int _index = 0;
    [SerializeField] private TMP_Text _text;
    private bool isRunning = true;

    public void Start()
    {
        // _text = GetComponent<TMP_Text>();
        UpdateText();
    }

    public void Update()
    {
        if (!isRunning) return;

        _summit += Time.deltaTime;

        if (_summit > _delay)
        {
            _index = (_index + 1) % _wordList.GetWordCount(_wordType);
            UpdateText();
            _summit = 0;
        }
    }

    private void UpdateText()
    {
        _text.text = _wordList.GetWord(_wordType, _index);
    }

    public string Stop()
    {
        isRunning = false;
        return _wordList.GetWord(_wordType, _index);
    }
}
