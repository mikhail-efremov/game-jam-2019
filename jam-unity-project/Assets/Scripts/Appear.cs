using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Appear : MonoBehaviour
{
  public float Timeout;
  public Image[] Targets;

  void Start()
  {
    foreach (var target in Targets)
    {
      target.DOFade(1, Timeout);
    }
  }
}