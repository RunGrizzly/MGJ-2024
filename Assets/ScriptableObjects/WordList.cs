using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WordList : ScriptableObject

{
    public List<string> Verbs;
    public List<string> Adverbs;
    public List<string> Adjectives;
    public List<string> Animals;

    public int GetWordCount(WordType wordType) => wordType switch
    {
        WordType.Verb => Verbs.Count,
        WordType.Adjective => Adjectives.Count,
        WordType.Adverb => Adverbs.Count,
        WordType.Animal => Animals.Count,
        _ => Verbs.Count
    };

    public string GetWord(WordType wordType, int index) => wordType switch
    {
        WordType.Verb => Verbs[index],
        WordType.Adjective => Adjectives[index],
        WordType.Adverb => Adverbs[index],
        WordType.Animal => Animals[index],
        _ => Verbs[index]
    };
}
