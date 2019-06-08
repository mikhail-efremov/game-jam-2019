using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTemplateProjects.Maps;
using Random = UnityEngine.Random;

namespace UnityTemplateProjects
{
  public class GameGod : MonoBehaviour
  {
    private static GameGod _instance;
    public static GameGod Instance => _instance;

    public bool IsGameOver;
        
    public List<TimeBasedAction> Actions = new List<TimeBasedAction>();

    public int MaxSideHealth;

    public EvilMan EvilMan;

    private int _lastPreActionSeccond = -1;
    private int _lastSeccond = -1;

    private int _leftSideHealth;
    private int _rightSideHealth;
       
    private void Awake()
    {
      IsGameOver = false;
      _instance = this;
      DontDestroyOnLoad(gameObject);

      _leftSideHealth = MaxSideHealth;
      _rightSideHealth = MaxSideHealth;

      EvilMan = FindObjectOfType<EvilMan>();
    }

    private void Update()
    {
      var seccond = Mathf.RoundToInt(Time.timeSinceLevelLoad);
      if (_lastSeccond == seccond)
        return;
      
      _lastSeccond = seccond;
      var actions = Actions.Where(x => x.Seccond == seccond).ToList();
      
      if (actions.Count > 0)
        EvilMan.Action();
        
      foreach (var action in actions)
      {
        ProceedAction(action);
      }
    }

    public int GetHealthBySide(Side side)
    {
      return side == Side.Left ? _leftSideHealth : _rightSideHealth;
    }

    public void SetHealthBySide(Side side, int value)
    {
      if (side == Side.Left)
        _leftSideHealth = value;
      else
        _rightSideHealth = value;
    }

    private Side _lastBombSide = Side.Left;
    private Side _lastDecaySide = Side.Right;

    private void ProceedAction(TimeBasedAction action)
    {
      switch (action.Type)
      {
        case TimeBasedActionType.SpawnBomb:
        {
          var side = action.Side;
          
          if (action.Side == Side.Turn)
          {
            _lastBombSide = InvertSide(_lastBombSide);
            side = _lastBombSide;
          }

          var map = Map.Instance.BySide(side);
          var tileIndex = Random.Range(0, map.Count);
          var tile = map[tileIndex];
          
          var bomb = Instantiate(Map.Instance.Bomb);

          if (action.BompTimeout > 0)
            bomb.GetComponent<Bomb>().Timeout = action.BompTimeout;
          var pos = tile.transform.position;
          pos.y += 1;
          bomb.transform.position = pos;
          break;
        }
        case TimeBasedActionType.Split:
        {
          var side = action.Side;
          if (side == Side.Turn)
            side = Side.Left;

          var master = FindObjectsOfType<PlayerMaster>().First(x => x.Side == side);
          master.Split();
          break;
        }
        case TimeBasedActionType.GetTogether:
        {
          var side = action.Side;
          if (side == Side.Turn)
            side = Side.Left;

          var master = FindObjectsOfType<PlayerMaster>().First(x => x.Side == side);
          master.GetTogether();          
          break;
        }
        case TimeBasedActionType.RandomDecay:
        {
          var side = action.Side;
          
          if (action.Side == Side.Turn)
          {
            _lastDecaySide = InvertSide(_lastDecaySide);
            side = _lastDecaySide;
          }
          var map = Map.Instance.BySide(side);
          var tileIndex = Random.Range(0, map.Count);
          var tile = map[tileIndex];
          
          tile.Break();          
          break;
        }
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public Side InvertSide(Side side)
    {
      if (side == Side.Left)
        return Side.Right;
      return Side.Left;
    }
  }

  [Serializable]
  public class TimeBasedAction
  {
    public TimeBasedActionType Type;
    public Side Side;
    public int Seccond;
    public int BompTimeout;
  }

  public enum TimeBasedActionType
  {
    SpawnBomb,
    Split,
    GetTogether,
    RandomDecay
  }

  public enum Side
  {
    Left,
    Right,
    Turn
  }
}