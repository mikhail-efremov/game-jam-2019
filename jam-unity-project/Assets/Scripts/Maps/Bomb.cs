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
    public GameObject Explosion;
    
    public bool CanExplode;
    public float Radius;
    public float Timeout;
    
    public float Magnitude = 2f;
    public float Roughness = 10f;
    public float FadeOutTime = 5f;

    public AudioClip ExplosionAudio;
    public AudioClip TickingAudio;
    
    private AudioSource _explosionAudioSource;
    private AudioSource _tickingAudioSource;

    public event Action Exploded;

    public void Awake()
    {
      CanExplode = true;
      _explosionAudioSource = gameObject.AddComponent<AudioSource>();
      _explosionAudioSource.clip = ExplosionAudio;
      _tickingAudioSource = gameObject.AddComponent<AudioSource>();
      _tickingAudioSource.clip = TickingAudio;
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
      
      _tickingAudioSource.Play();
      yield return new WaitForSeconds(Timeout / 3.33f);
      fast.Kill();
      _tickingAudioSource.Stop();
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

      ExplodeForTiles(ftiles, Side.Left);
      ExplodeForTiles(stiles, Side.Right);

explosionAudioSource.Play();

      StartCoroutine(Effect());
      
      CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, 0, FadeOutTime);
    }

    private IEnumerator Effect()
    {
      var explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
      yield return new WaitForSeconds(2);
      _explosionAudioSource.Stop();
      Destroy(explosion);
      Destroy(gameObject);
    }

    private void ExplodeForTiles(List<MapTile> tiles, Side side)
    {
      foreach (var tile in tiles)
      {
        var tilePos = tile.transform.position;
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(tilePos.x, tilePos.z));
        if (distance < Radius)
        {
          tile.Break();

          var baseHealth = GameGod.Instance.GetHealthBySide(side);
          GameGod.Instance.SetHealthBySide(side, baseHealth - 1);
        }
      }

      GetComponent<MeshRenderer>().enabled = false;
    }
  }
}