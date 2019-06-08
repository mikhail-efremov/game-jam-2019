﻿using System.Collections.Generic;
using GamepadInput;
using UnityEngine;

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

  public class Player : MonoBehaviour
  {
    public float MoveHorizontal = 0f;
    public float MoveVertical = 0f;

    private float _nextUse;
    private const float UseRate = 0.1f;

    public bool CanControll = true;

    [SerializeField] private float _speed = 5;
    [SerializeField] private PlayerIndex _playerIndex;

    private void FixedUpdate()
    {
      MoveHorizontal = Input.GetAxis(_controlls[_playerIndex][Controll.Horizontal]);
      MoveVertical = Input.GetAxis(_controlls[_playerIndex][Controll.Vertical]);

      var action = Input.GetAxis(_controlls[_playerIndex][Controll.Activate]) > 0;
      var needCansel = Input.GetAxis(_controlls[_playerIndex][Controll.Cancel]) > 0;

      if (GamePad.GetButtonDown(GamePad.Button.B, _gamePadMap[_playerIndex]))
      {
			Debug.Log("Activate_P" + ((int)_playerIndex + 1));
        action = true;
      }

      if (GamePad.GetButtonDown(GamePad.Button.X, _gamePadMap[_playerIndex]))
      {
        needCansel = true;
			Debug.Log("Cansel_P" + ((int)_playerIndex + 1));
      }

      if (action && Time.time > _nextUse)
      {
        _nextUse = Time.time + UseRate;

        Debug.LogError("USE SHIIIT");
      }

      if (!CanControll)
        return;

      var movement = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
      Debug.LogError(movement.ToString());

      var rigid = GetComponent<Rigidbody>();

      rigid.velocity = movement * _speed;

      // The step size is equal to speed times frame time.
      float step = 5 * Time.deltaTime;
      var vector = Quaternion.AngleAxis(90, Vector3.up) * rigid.velocity;

      var newDir = Vector3.RotateTowards(transform.forward, vector, step, 0.0f);

      // Move our position a step closer to the target.
      rigid.rotation = Quaternion.LookRotation(newDir);
    }

    private Dictionary<PlayerIndex, Dictionary<Controll, string>> _controlls =
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
  }
}