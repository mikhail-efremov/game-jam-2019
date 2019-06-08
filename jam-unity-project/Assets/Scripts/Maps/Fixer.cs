using System.Collections;
using System.Linq;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Fixer : MonoBehaviour
  {
    public bool CanFix;
    public bool IsFixing;

    private MapTile _mapTile;
    private Player _player;

    public void Init(Player player)
    {
      _player = player;
    }

    public void StartFixing()
    {
      // start animation
      var opponentTiles = Map.Instance.GetMyTiles(_player._playerIndex);
      var broken = opponentTiles.Where(t => t.IsBroken).ToList();
      var closest = broken.OrderBy(m => Helper.Distance(transform.position, m.transform.position)).FirstOrDefault();

      if (closest == null)
        return;
      
      if (Helper.Distance(closest.transform.position, _player.transform.position) > Map.Instance.FixDistance)
        return;
      
      _mapTile = closest;
      // animation?
      StartCoroutine("Fixing");
    }

    public IEnumerator Fixing()
    {
      Debug.Log("STARTING TO FIX");
      IsFixing = true;
      yield return new WaitForSeconds(Map.Instance.FixTime);

      Debug.Log("FIXING FINISHED");
      
      if (_mapTile == null)
        yield break;
      
      _mapTile.Repair();

      IsFixing = false;
    }

    public void StopFixing()
    {
      // stop animation
      if (IsFixing)
      {
        StopCoroutine("Fixing");
        IsFixing = false;
        Debug.Log("FIXING JUST STOPPED");
      }
    }
  }
}