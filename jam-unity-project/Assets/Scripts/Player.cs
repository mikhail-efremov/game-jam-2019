﻿using System.Collections.Generic;
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

    public PlayerRole Role;

    public bool CanControll = true;

    [SerializeField] private float _speed = 5;
    [SerializeField] public PlayerIndex _playerIndex;

    private void Awake()
    {
      _fight = new Fight(this);
      _fixer = gameObject.AddComponent<Fixer>();
      _fixer.Init(this);
    }

    private void FixedUpdate()
    {
      if (!CanControll)
        return;
      
      MoveHorizontal = Input.GetAxis(_controlls[_playerIndex][Controll.Horizontal]);
      MoveVertical = Input.GetAxis(_controlls[_playerIndex][Controll.Vertical]);
      
      var action = Input.GetAxis(_controlls[_playerIndex][Controll.Activate]) > 0.3;

      if (Role == PlayerRole.Big)
      {
        if (_playerIndex == PlayerIndex.One)
        {
          MoveHorizontal += Input.GetAxis(_controlls[PlayerIndex.Two][Controll.Horizontal]);
          MoveVertical += Input.GetAxis(_controlls[PlayerIndex.Two][Controll.Vertical]);
          
          action |= Input.GetAxis(_controlls[PlayerIndex.Two][Controll.Activate]) > 0.3;
        }

        if (_playerIndex == PlayerIndex.Three)
        {
          MoveHorizontal += Input.GetAxis(_controlls[PlayerIndex.Four][Controll.Horizontal]);
          MoveVertical += Input.GetAxis(_controlls[PlayerIndex.Four][Controll.Vertical]);
          
          action |= Input.GetAxis(_controlls[PlayerIndex.Four][Controll.Activate]) > 0.3;
        }

        Mathf.Clamp01(MoveHorizontal);
        Mathf.Clamp01(MoveVertical);
      }

      if (action && (Role == PlayerRole.Fix || Role == PlayerRole.Big))
      {
        Debug.LogError(_playerIndex + " " + Role + " tryed to fix");
        Debug.LogError(Input.GetAxis(_controlls[_playerIndex][Controll.Activate]));
        
        _fixer.StartFixing();
      }
      else if (!action && (Role == PlayerRole.Fix || Role == PlayerRole.Big))
      {     
        _fixer.StopFixing();
      }

      if (action && (Role == PlayerRole.Shoot || Role == PlayerRole.Big))
      {
        Debug.LogError(_playerIndex + " " + Role + " tryed to shoot");
        Debug.LogError(Input.GetAxis(_controlls[_playerIndex][Controll.Activate]));
        
        if (action && Time.time > _nextUse)
        {
          _nextUse = Time.time + UseRate;

          _fight.Hold();
          _fight.Throw();
        }
      }

      if (!CanControll)
        return;
      
      var movement = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
      var rigid = GetComponent<Rigidbody>();

      rigid.velocity = movement * _speed;
      
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
        PlayerIndex.Two, GamePad.Index.Two
      },
      {
        PlayerIndex.Three, GamePad.Index.Three
      },
      {
        PlayerIndex.Four, GamePad.Index.Four
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