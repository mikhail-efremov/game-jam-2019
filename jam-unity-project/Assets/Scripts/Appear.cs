using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Appear : MonoBehaviour
{
  public float Timeout;

  void Start()
  {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().DOFade(1, Timeout);
    }
}