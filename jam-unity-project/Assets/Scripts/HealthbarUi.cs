using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class HealthbarUi : MonoBehaviour
{
  public Side Side;
  
  private Image _image;
    private TweenerCore<float, float, FloatOptions> _tweener;

    private float _prevValue = -1;
  
  void Start()
  {
    _image = GetComponent<Image>();
  }

  void Update()
  {
    var max = GameGod.Instance.MaxSideHealth;

    var cur = GameGod.Instance.GetHealthBySide(Side);    
    var value = (float) cur / max;

        if(_prevValue != value)
        {
            if (_tweener != null)
                _tweener.Pause();

            _tweener = _image.DOFillAmount(value, 1f);
        }

  }
}