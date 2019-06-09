using DG.Tweening;
using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class MusicMaster : MonoBehaviour
  {
    public static MusicMaster Instance;
    private AudioSource _menuAudioSource;
    private AudioSource _mainGameMusic;
    private int _lastSeccond = -1;

    public void Start()
    {
      Instance = this;
      _menuAudioSource = gameObject.AddComponent<AudioSource>();
      _menuAudioSource.loop = true;
      _menuAudioSource.clip = Map.Instance.MainMenuAudio;
      
      _mainGameMusic = gameObject.AddComponent<AudioSource>();
      _mainGameMusic.loop = true;
      _mainGameMusic.clip = Map.Instance.MainGameAudio;
      _mainGameMusic.playOnAwake = false;
    }

    private void Update()
    {
      var seccond = Mathf.RoundToInt(Time.timeSinceLevelLoad);
      
      if (_lastSeccond == seccond)
        return;

      _lastSeccond = seccond;
      if (_lastSeccond == 30)
      {
        _mainGameMusic.Stop();
        _mainGameMusic.clip = Map.Instance.FastMainGameAudio;
        _mainGameMusic.Play();
      }
    }

    public void PlayMenuMusic()
    {
      _mainGameMusic.Stop();
      _menuAudioSource.volume = 0;
      _menuAudioSource.Play();
      
      DOTween.To(() => _menuAudioSource.volume, x => _menuAudioSource.volume = x, 1, 6);
    }

    public void PlayMainGameMusic()
    {
      _menuAudioSource.Stop();
      _mainGameMusic.volume = 0;
      _mainGameMusic.Play();
      
      DOTween.To(() => _mainGameMusic.volume, x => _mainGameMusic.volume = x, .7f, 6);
    }

    public void StopAllMusic()
    {
      DOTween.To(() => _menuAudioSource.volume, x => _menuAudioSource.volume = x, 0, 1f);
      DOTween.To(() => _mainGameMusic.volume, x => _mainGameMusic.volume = x, 0, 1f);
    }
  }
}