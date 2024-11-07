////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                                             
//      _____ _            _____                   
//     |_   _|_|___ _ _   |_   _|_ _ _ ___ ___ ___ 
//       | | | |   | | |    | | | | | | -_| -_|   |
//       |_| |_|_|_|_  |    |_| |_____|___|___|_|_|
//                 |___|
// A Complete and Easy to use Tweens library in One File
//
// Basic use:
// using FronkonGames.TinyTween;
// GameObject gameObject = new();
// gameObject.TweenMove(new Vector3(10.0f, 0.0f, 0.0f), 1.0f, Ease.Bounce);
//
// Advanced use:
// using FronkonGames.TinyTween;
// GameObject clockHand = new();
// TweenQuaternion.Create()
//                .Origin(Quaternion.Euler(-30.0f, 0.0f, 0.0f))
//                .Destination(Quaternion.Euler(30.0f, 0.0f, 0.0f))
//                .Duration(1.0f)
//                .Loop(TweenLoop.YoYo)
//                .EasingIn(Ease.Back)
//                .EasingOut(Ease.Elastic)
//                .Owner(clockHand)
//                .Condition(tween => tween.ExecutionCount < 10)
//                .OnUpdate(tween => clockHand.transform.rotation = tween.Value)
//                .OnEnd(() => Debug.Log("It's show time!"))
//                .Start();
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using FronkonGames.TinyTween.Tweens;
using UnityEngine;

namespace FronkonGames.TinyTween
{
  /// <summary>
  /// Tweens manager, update tweens and delete those that have already ended.
  /// It is not necessary if you are in charge of maintaining your Tweens ;)
  /// </summary>
  public sealed class TinyTween : MonoBehaviour
  {
    public static TinyTween Instance => LazyInstance.Value;

    private static readonly Lazy<TinyTween> LazyInstance = new(CreateSingleton);
    
    private readonly List<ITween> tweens = new();

    /// <summary> Add an existing tween. </summary>
    /// <param name="tween">New tween</param>
    /// <returns>Tween.</returns>
    public ITween Add(ITween tween) { tweens.Add(tween); return tween; }

    private static TinyTween CreateSingleton()
    {
      GameObject ownerObject = new("TinyTween");
      TinyTween instance = ownerObject.AddComponent<TinyTween>();
      DontDestroyOnLoad(ownerObject);

      return instance;
    }

    private void Update()
    {
      int count = tweens.Count;
      for (int i = count - 1; i >= 0; --i)
      {
        ITween tween = tweens[i];

        if (tween.State == TweenState.Running)
          tween.Update();

        if (tween.State == TweenState.Finished && i < count)
          tweens.RemoveAt(i);
      }      
    }

    private void OnDisable() => tweens.Clear();
  }
}