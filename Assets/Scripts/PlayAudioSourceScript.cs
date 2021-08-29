using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.EventSystems;
  
   public class PlayAudioSourceScript : MonoBehaviour, IPointerEnterHandler
   {
       public void OnPointerEnter(PointerEventData eventData)
      {
          GetComponent<AudioSource>().Play();
      }
 }