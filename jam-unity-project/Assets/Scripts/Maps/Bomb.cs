using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Bomb : MonoBehaviour
  {
    public int Radius;
    public float Timeout;

    public void Awake()
    {
      // animation?
      StartCoroutine(Ticking());
    }

    public IEnumerator Ticking()
    {
      yield return new WaitForSeconds(1);
      // animation?
      //yield return new WaitForSeconds(1);
      // animation?

      Explode();
    }

    public void Explode()
    {
      var ftiles = Map.Instance.FirstPlayer;
      var stiles = Map.Instance.SecondPlayer;

      ExplodeForTiles(ftiles);
      ExplodeForTiles(stiles);
    }

    private void ExplodeForTiles(List<MapTile> tiles)
    {
      foreach (var tile in tiles)
      {
        float distanceSqr = (transform.position - tile.transform.position).sqrMagnitude;
        if (distanceSqr < Radius * Radius)
        {
          tile.Break();
        }
      }
      
      Destroy(gameObject);
    }
  }
}