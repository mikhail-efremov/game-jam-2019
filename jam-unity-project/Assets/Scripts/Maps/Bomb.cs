using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EZCameraShake;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Bomb : MonoBehaviour
  {
    public bool CanExplode;
    public float Radius;
    public float Timeout;
    
    public float Magnitude = 2f;
    public float Roughness = 10f;
    public float FadeOutTime = 5f;

    public event Action Exploded;

    public void Awake()
    {
      CanExplode = true;
      // animation?
      StartCoroutine(Ticking());
    }

    public IEnumerator Ticking()
    {
      var slow = transform.DOScale(1.1f, .3f).SetLoops(-1, LoopType.Yoyo);
      yield return new WaitForSeconds(Timeout / 3.33f);
      slow.Kill();
      var moderate = transform.DOScale(1.3f, .2f).SetLoops(-1, LoopType.Yoyo);
      yield return new WaitForSeconds(Timeout / 3.33f);
      moderate.Kill();
      var fast = transform.DOScale(1.5f, .1f).SetLoops(-1, LoopType.Yoyo);
      yield return new WaitForSeconds(Timeout / 3.33f);
      fast.Kill();
      // animation?
      //yield return new WaitForSeconds(1);
      // animation?

      while (!CanExplode)
        yield return new WaitForSeconds(0.2f);

      Exploded?.Invoke();
      
      Explode();
    }

    public void Explode()
    {
      var ftiles = Map.Instance.LeftPlayer;
      var stiles = Map.Instance.RightPlayer;

      ExplodeForTiles(ftiles);
      ExplodeForTiles(stiles);
      
      CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, 0, FadeOutTime);
    }

    private void ExplodeForTiles(List<MapTile> tiles)
    {
      foreach (var tile in tiles)
      {
        var tilePos = tile.transform.position;
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(tilePos.x, tilePos.z));
        if (distance < Radius)
        {
          tile.Break();
        }
      }

      Destroy(gameObject);
    }
  }
}