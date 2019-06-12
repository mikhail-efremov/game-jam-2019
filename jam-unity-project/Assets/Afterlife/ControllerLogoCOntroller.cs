using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ControllerLogoCOntroller : MonoBehaviour
{
    private Image _sr;
    void Start()
    {
        _sr = GetComponent<Image>();
        StartCoroutine(DA());
    }

    private IEnumerator DA()
    {
        yield return null;
        var col = _sr.color;
        col.a = 0;
        _sr.color = col;

        var fading = _sr.DOFade(1, 2f);

        var source = _sr.GetComponent<RectTransform>();
        var source1 = _sr.transform.position.y;
        //var y = source.
        source.DOMoveY(source1 - 10, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Flash);

        yield return new WaitForSeconds(7f);

        _sr.DOFade(0, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
