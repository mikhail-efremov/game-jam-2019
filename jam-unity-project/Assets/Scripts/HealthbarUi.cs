﻿using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class HealthbarUi : MonoBehaviour
{
//  public Side
  
  private Image _image;
  
  void Start()
  {
    _image = GetComponent<Image>();
  }

  void Update()
  {
    var max = GameGod.Instance.MaxSideHealth;
    
    /*
    var cur = GameGod.Instance.CurrentBullets;
    
    var value = ((float)cur / max);
    
    _image.fillAmount = value;*/
  }
}