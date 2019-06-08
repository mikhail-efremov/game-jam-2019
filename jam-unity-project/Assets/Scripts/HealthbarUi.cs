using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class HealthbarUi : MonoBehaviour
{
  private Image _image;
  
  void Start()
  {
    _image = GetComponent<Image>();
  }

  void Update()
  {
//    var max = GameGod.Instance.MaximumBullets;
  //  var cur = GameGod.Instance.CurrentBullets;
    
    //var value = ((float)cur / max);
    
    //_image.fillAmount = value;
  }
}