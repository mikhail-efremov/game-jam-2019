using System.Collections;
using System.Linq;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Fixer : MonoBehaviour
  {
    public bool CanFix;
    public bool IsFixing;

    private Side _mySide;

    private MapTile _mapTile;
    private Player _player;

    private AudioSource _fixingSoundSource;
    
    public void Init(Player player)
    {
      _player = player;
      _fixingSoundSource = gameObject.AddComponent<AudioSource>();
      _fixingSoundSource.clip = Map.Instance.FixingSound;
    }

    public bool StartFixing(Side side)
    {
      _mySide = side;
      
      // start animation
      var opponentTiles = Map.Instance.GetMyTiles(_player._playerIndex);
      var broken = opponentTiles.Where(t => t.IsBroken).ToList();
      var closest = broken.OrderBy(m => Helper.Distance(transform.position, m.transform.position)).FirstOrDefault();

      if (closest == null)
        return false;

      var dist = Helper.Distance(closest.transform.position, _player.transform.position); 
      
      if (dist > Map.Instance.FixDistance)
        return false;
      
      _mapTile = closest;
      // animation?
      StartCoroutine("Fixing");
      return true;
    }

    public IEnumerator Fixing()
    {
      Debug.Log("STARTING TO FIX");
      IsFixing = true;
      if(_mapTile != null)
        _mapTile.StartFixing();
      
      _fixingSoundSource.Play();
      yield return new WaitForSeconds(Map.Instance.FixTime);

      Debug.Log("FIXING FINISHED");
      
      if (_mapTile == null)
        yield break;
      
      _mapTile.Repair();

      var god = GameGod.Instance;
      var cur = god.GetHealthBySide(_mySide);
      god.SetHealthBySide(_mySide, cur + 1);
      
      IsFixing = false;
      if(_mapTile != null)
        _mapTile.StopFixing();
    }

    public void StopFixing()
    {
      _fixingSoundSource.Stop();
      // stop animation
      if (IsFixing)
      {
        StopCoroutine("Fixing");
        IsFixing = false;
        Debug.Log("FIXING JUST STOPPED");
        if(_mapTile != null)
          _mapTile.StopFixing();
      }
    }
  }
}