using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class NameBandit : MonoBehaviour
    {
        private List<NameBanditSection> _sections;

        private int _chosenSectionCount = 0;

        private List<string> _chosenWords = new();
        
        private void Start()
        {
            _sections = GetComponentsInChildren<NameBanditSection>().ToList();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var word = _sections[_chosenSectionCount].Stop();
                _chosenWords.Add(word);
                _chosenSectionCount += 1;
            }

            if (_chosenSectionCount == _sections.Count)
            {
                // TODO: Set name on GameManager or wherever
                Debug.LogFormat("Player has chosen name {0}", string.Join(" ", _chosenWords));
            }
        }
    }
}