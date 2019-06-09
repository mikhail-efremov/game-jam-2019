using UnityEngine;

namespace UnityTemplateProjects.Maps
{
  public class MusicMaster : MonoBehaviour
  {
    public static MusicMaster Instance;
    private AudioSource _menuAudioSource;
    private AudioSource _mainGameMusic;

    public void Start()
    {
      Instance = this;
      _menuAudioSource = gameObject.AddComponent<AudioSource>();
      _menuAudioSource.loop = true;
      _menuAudioSource.clip = Map.Instance.MainMenuAudio;
      
      _mainGameMusic = gameObject.AddComponent<AudioSource>();
      _mainGameMusic.loop = true;
      _mainGameMusic.clip = Map.Instance.MainGameAudio;
    }

    public void PlayMenuMusic()
    {
      _mainGameMusic.Stop();
      _menuAudioSource.Play();
    }

    public void PlayMainGameMusic()
    {
      _menuAudioSource.Stop();
      _mainGameMusic.Play();
    }
  }
}