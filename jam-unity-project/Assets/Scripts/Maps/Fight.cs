using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Fight
  {
    Random _rnd = new Random();
    /*private Player _player;*/
    public Fight(/*Player ref*/)
    {
      
    }
    
    public void Hold()
    {
      // _player.BlockMovement();
    }

    public void Throw()
    {
      // _release.ReleaseMovement();

      var targetTiles = Map.Instance.SecondPlayer;// TODO: _player.PlayerIndex
      var rndNumber = Random.Range(0, targetTiles.Count);
      var position = targetTiles[rndNumber].transform.position;
      
      var bomb = GameObject.Instantiate(Map.Instance.Bomb);
      bomb.transform.position = position;
    }
  }
}