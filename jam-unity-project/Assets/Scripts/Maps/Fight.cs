using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class Fight
  {
    public bool IsHolding;

    private Player _player;
    private Bomb _bomb;

    public Fight(Player player)
    {
      _player = player;
    }

    public void Hold()
    {
      if (IsHolding)
        return;

      var existingBombs = Object.FindObjectsOfType<Bomb>();
      if (existingBombs == null || existingBombs.Length == 0)
        return;

      var nearestBomb = existingBombs.OrderBy(b => Helper.Distance(b.transform.position, _player.transform.position)).FirstOrDefault();
      if (nearestBomb == null)
        return;

      if (Helper.Distance(nearestBomb.transform.position, _player.transform.position) > Map.Instance.PickUpDistance)
        return;

      _bomb = nearestBomb.GetComponent<Bomb>();
      _bomb.Exploded += BombOnExploded;
      IsHolding = true;
      _player.BlockMovement();

      var splitAudioSource = _player.gameObject.AddComponent<AudioSource>();
      splitAudioSource.clip = Map.Instance.PickupAudio;
      splitAudioSource.playOnAwake = false;
      splitAudioSource.Play();

      var position = _player.transform.position;
      position.y += 1;
      _bomb.transform.position = position;
    }

    private void BombOnExploded()
    {
      _bomb.Exploded -= BombOnExploded;
      IsHolding = false;
    }

    public void Throw()
    {
      if (!IsHolding)
        return;

      var splitAudioSource = _player.gameObject.AddComponent<AudioSource>();
      splitAudioSource.playOnAwake = false;
      splitAudioSource.clip = Map.Instance.BombThrowAudio;
      splitAudioSource.Play();

      IsHolding = false;
      _player.ReleaseMovement();

      var targetTiles = Map.Instance.GetOpponentTiles(_player._playerIndex);
      var targetTilesNotDamaged = targetTiles.Where(t => !t.IsBroken).ToList();

      if (!targetTilesNotDamaged.Any())
        return;

      var rndNumber = Random.Range(0, targetTilesNotDamaged.Count);

      var selectedTile = targetTilesNotDamaged[rndNumber];

      var targetPos = selectedTile.transform.position;
      targetPos.y += 1;

      _bomb.transform.DOJump(targetPos, 4f, 1, 0.74f)
        .SetEase(Ease.OutCirc)
        .OnStart(() => { _bomb.CanExplode = false; })
        .OnComplete(() =>
        {
          _bomb.CanExplode = true;
          var splitAudioSource2 = _player.gameObject.AddComponent<AudioSource>();
          splitAudioSource2.clip = Map.Instance.BombDropAudio;
          splitAudioSource2.Play();
        });
    }
  }
}