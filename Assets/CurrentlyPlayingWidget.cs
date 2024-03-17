using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentlyPlayingWidget : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _nameBox = null;

   private void OnEnable()
   {
    Brain.ins.EventHandler.StartRoundEvent.AddListener(OnRoundStarted);  
    Brain.ins.EventHandler.EndRoundEvent.AddListener(OnRoundEnded);  
   }
   
   private void OnDisable()
   {
       Brain.ins.EventHandler.StartRoundEvent.RemoveListener(OnRoundStarted);  
       Brain.ins.EventHandler.EndRoundEvent.RemoveListener(OnRoundEnded);  
   }
   
   private void OnRoundStarted(Round round)
   {
       _nameBox.text = "Currently playing: \n"+  round.Player.Name;
   }
   private void OnRoundEnded(Round round)
   {
       _nameBox.text = "";
   }
}
