using System.Collections.Generic;
using System.Linq;
using GamepadInput;
using UnityEngine;
using UnityTemplateProjects.Maps;

namespace UnityTemplateProjects
{
  public enum PlayerIndex
  {
    One,
    Two,
    Three,
    Four
  }

  public enum Controll
  {
    Horizontal,
    Vertical,
    Activate,
    Cancel
  }

  public enum PlayerRole
  {
    Big,
    Fix,
    Shoot
  }

  public class Player : MonoBehaviour
  {
    public float MoveHorizontal = 0f;
    public float MoveVertical = 0f;

    private float _nextUse;
    private const float UseRate = 0.1f;

    public Fight _fight;
    public Fixer _fixer;

    private Animator _animator;

    public PlayerRole Role;

    public bool CanControll = true;

    [SerializeField] private float _speed = 5;
    [SerializeField] public PlayerIndex _playerIndex;

    private AudioSource _stepsAudioSource;

    private void Awake()
    {
      _fight = new Fight(this);
      _fixer = gameObject.AddComponent<Fixer>();
      _fixer.Init(this);

      _animator = GetComponentInChildren<Animator>();

      _stepsAudioSource = gameObject.AddComponent<AudioSource>();
      _stepsAudioSource.clip = Map.Instance.StepsAudio;
      _stepsAudioSource.loop = true;
    }

    private void FixedUpdate()
    {
      MoveHorizontal = Input.GetAxis(_controlls[_playerIndex][Controll.Horizontal]);
      MoveVertical = Input.GetAxis(_controlls[_playerIndex][Controll.Vertical]);

      var action = false;

      if (Role == PlayerRole.Fix && Input.GetKey("z"))
        action = GamePad.GetButton(GamePad.Button.Back, _gamePadMap[_playerIndex]);
      if (Role == PlayerRole.Shoot && Input.GetKey("x"))
        action = GamePad.GetButton(GamePad.Button.Start, _gamePadMap[_playerIndex]);

      if (Role == PlayerRole.Big)
      {
        if (_playerIndex == PlayerIndex.One)
        {
          MoveHorizontal += Input.GetAxis(_controlls[PlayerIndex.Two][Controll.Horizontal]);
          MoveVertical += Input.GetAxis(_controlls[PlayerIndex.Two][Controll.Vertical]);

          action = GamePad.GetButton(GamePad.Button.Start, GamePad.Index.One)
                   || GamePad.GetButton(GamePad.Button.Back, GamePad.Index.One)
                   || Input.GetKey("z");
        }

        if (_playerIndex == PlayerIndex.Three)
        {
          MoveHorizontal += Input.GetAxis(_controlls[PlayerIndex.Four][Controll.Horizontal]);
          MoveVertical += Input.GetAxis(_controlls[PlayerIndex.Four][Controll.Vertical]);

          action = GamePad.GetButton(GamePad.Button.Back, GamePad.Index.Two)
                   || GamePad.GetButton(GamePad.Button.Start, GamePad.Index.Two)
                   || Input.GetKey("x");
        }

        Mathf.Clamp01(MoveHorizontal);
        Mathf.Clamp01(MoveVertical);
      }

      if (action && (Role == PlayerRole.Fix || Role == PlayerRole.Big))
      {
        _fixer.StartFixing();
      }
      else if (!action && (Role == PlayerRole.Fix || Role == PlayerRole.Big))
      {
        _fixer.StopFixing();
      }


      if (action && (Role == PlayerRole.Shoot || Role == PlayerRole.Big))
      {
        _fight.Hold();
      }
      else if (!action && (Role == PlayerRole.Shoot || Role == PlayerRole.Big))
      {
        _fight.Throw();
      }

      if (!CanControll)
        return;

      var movement = new Vector3(MoveHorizontal, 0.0f, MoveVertical);

      if (movement != Vector3.zero)
      {
        _animator.SetBool("Run", true);
        if (!_stepsAudioSource.isPlaying)
          _stepsAudioSource.Play();
      }
      else
      {
        if (_stepsAudioSource.isPlaying)
          _stepsAudioSource.Stop();
        _animator.SetBool("Run", false);
      }

      var rigid = GetComponent<Rigidbody>();

      if (CanControll)
        rigid.velocity = movement * _speed;
      else
        rigid.velocity = Vector3.zero;

      var rot = transform.rotation;
      rot.eulerAngles = Vector3.zero;
    }

    private readonly Dictionary<PlayerIndex, Dictionary<Controll, string>> _controlls =
      new Dictionary<PlayerIndex, Dictionary<Controll, string>>
      {
        {
          PlayerIndex.One, new Dictionary<Controll, string>
          {
            {
              Controll.Horizontal, "Horizontal_P1"
            },
            {
              Controll.Vertical, "Vertical_P1"
            },
            {
              Controll.Activate, "Activate_P1"
            },
            {
              Controll.Cancel, "Cancel_P1"
            }
          }
        },
        {
          PlayerIndex.Two,
          new Dictionary<Controll, string>
          {
            {
              Controll.Horizontal, "Horizontal_P2"
            },
            {
              Controll.Vertical, "Vertical_P2"
            },
            {
              Controll.Activate, "Activate_P2"
            },
            {
              Controll.Cancel, "Cancel_P2"
            }
          }
        },
        {
          PlayerIndex.Three,
          new Dictionary<Controll, string>
          {
            {
              Controll.Horizontal, "Horizontal_P3"
            },
            {
              Controll.Vertical, "Vertical_P3"
            },
            {
              Controll.Activate, "Activate_P3"
            },
            {
              Controll.Cancel, "Cancel_P3"
            }
          }
        },
        {
          PlayerIndex.Four,
          new Dictionary<Controll, string>
          {
            {
              Controll.Horizontal, "Horizontal_P4"
            },
            {
              Controll.Vertical, "Vertical_P4"
            },
            {
              Controll.Activate, "Activate_P4"
            },
            {
              Controll.Cancel, "Cancel_P4"
            }
          }
        }
      };

    private readonly Dictionary<PlayerIndex, GamePad.Index> _gamePadMap = new Dictionary<PlayerIndex, GamePad.Index>
    {
      {
        PlayerIndex.One, GamePad.Index.One
      },
      {
        PlayerIndex.Two, GamePad.Index.One
      },
      {
        PlayerIndex.Three, GamePad.Index.Two
      },
      {
        PlayerIndex.Four, GamePad.Index.Two
      },
    };

    public void BlockMovement()
    {
      CanControll = false;
      var rigid = GetComponent<Rigidbody>();
      rigid.velocity = Vector3.zero;
    }

    public void ReleaseMovement()
    {
      CanControll = true;
    }
  }
}