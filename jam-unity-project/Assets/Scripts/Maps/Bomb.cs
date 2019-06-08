using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Bomb : MonoBehaviour
  {
    public float Radius;
    public float Timeout;

    public void Awake()
    {
      // animation?
      
    }


    public void StartTicking()
    {
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