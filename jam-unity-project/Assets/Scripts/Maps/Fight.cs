using System.Linq;
using DG.Tweening;
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

      var targetTiles = Map.Instance.GetOpponentTiles(_player._playerIndex);

      var targetTilesNotDamaged = targetTiles.Where(t => !t.IsBroken).ToList();

      if (!targetTilesNotDamaged.Any())
        return;

      var rndNumber = Random.Range(0, targetTilesNotDamaged.Count);

      var selectedTile = targetTilesNotDamaged[rndNumber];
      var position = _player.transform.position; //selectedTile.transform.position;

      position.y += 1;

      var bomb = Object.Instantiate(Map.Instance.Bomb);
      bomb.transform.position = position;


      var targetPos = selectedTile.transform.position;
      targetPos.y += 1;
//      bomb.transform.DOMoveX(targetPos.x, 1f)
//        .SetEase(Ease.InOutExpo)
//        .OnComplete(() => { bomb.GetComponent<Bomb>().StartTicking(); });
//      
//      bomb.transform.DOMoveX(targetPos.x, 1f)
//        .SetEase(Ease.InOutExpo)
//        .OnComplete(() => { bomb.GetComponent<Bomb>().StartTicking(); });

      bomb.transform.DOJump(targetPos, 4f, 1, 0.7f)
        .SetEase(Ease.OutCirc)
        .OnComplete(() => { bomb.GetComponent<Bomb>().StartTicking(); });
      
    }
  }
}