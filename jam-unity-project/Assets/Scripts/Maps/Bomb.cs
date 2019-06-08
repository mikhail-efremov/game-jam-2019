using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Bomb : MonoBehaviour
  {
    public bool CanExplode;
    public float Radius;
    public float Timeout;

    public void Awake()
    {
      // animation?
      StartCoroutine(Ticking());
    }

    public IEnumerator Ticking()
    {var slow = transform.DOPunchScale(new Vector3(1, 1, 1),3f).SetLoops(-1, LoopType.Yoyo);
      yield return new WaitForSeconds(Timeout / 3.33f);
      slow.Kill();
      var moderate = transform.DOPunchScale(new Vector3(1, 1, 1),2f).SetLoops(-1, LoopType.Yoyo);
      yield return new WaitForSeconds(Timeout / 3.33f);
      moderate.Kill();
      var fast = transform.DOPunchScale(new Vector3(1, 1, 1),2f).SetLoops(-1, LoopType.Yoyo);
      yield return new WaitForSeconds(Timeout / 3.33f);
      fast.Kill();
      // animation?
      //yield return new WaitForSeconds(1);
      // animation?

      while(!CanExplode)
        yield return new WaitForSeconds(0.2f);
      
      Explode();
    }

    public void Explode()
    {
      var ftiles = Map.Instance.LeftPlayer;
      var stiles = Map.Instance.RightPlayer;

      ExplodeForTiles(ftiles);
      ExplodeForTiles(stiles);
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