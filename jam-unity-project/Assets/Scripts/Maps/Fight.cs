using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Fight
  {
    Random _rnd = new Random();
    private Player _player;
    public Fight(Player player)
    {
      _player = player;
    }
    
    public void Hold()
    {
      _player.BlockMovement();
    }

    public void Throw()
    {
      _player.ReleaseMovement();

      var targetTiles = Map.Instance.SecondPlayer;// TODO: _player.PlayerIndex
      var rndNumber = Random.Range(0, targetTiles.Count);
      var position = targetTiles[rndNumber].transform.position;

      position.y += 1;
      
      var bomb = GameObject.Instantiate(Map.Instance.Bomb);
      bomb.transform.position = position;
    }
  }
}