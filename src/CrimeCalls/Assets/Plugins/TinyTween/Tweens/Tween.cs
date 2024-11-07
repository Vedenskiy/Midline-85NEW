using System;
using FronkonGames.TinyTween.Easing;
using UnityEngine;

namespace FronkonGames.TinyTween.Tweens
{
    /// <summary> Tween operation. If it is created manually, Update() must be called. </summary>
    public abstract class Tween<T> : ITween<T> where T : struct
    {
        /// <inheritdoc/>
        public TweenState State { get; private set; } = TweenState.Paused;

        /// <inheritdoc/>
        public T Value { get; private set; }

        /// <inheritdoc/>
        public float Progress { get; private set; }

        /// <inheritdoc/>
        public int ExecutionCount { get; private set; }

        /// <summary> Time that the operation takes. </summary>
        public float Time { get; private set; }
    
        /// <summary> Does the Tween depend on another object? </summary>
        private bool IsOwned { get; set; }
    
        private object owner = null;
    
        private T origin, destination;
        private Ease easeIn = Ease.None, easeOut = Ease.None;
        private TweenLoop loop = TweenLoop.Once;

        private float duration = 1.0f;
        private float currentTime;
        private bool clamp;
        private int residueCount = -1;

        private Action<Tween<T>> updateFunction;
        private Action<Tween<T>> endFunction;
        private Func<Tween<T>, bool> condition;

        private readonly Func<Tween<T>, T, T, float, bool, T> interpolationFunction; // Tween, start, end, progress, clamp.
    
        /// <summary> Constructor. </summary>
        /// <param name="lerpFunc">Interpolation function.</param>
        protected Tween(Func<Tween<T>, T, T, float, bool, T> interpolationFunction) => this.interpolationFunction = interpolationFunction;

        /// <summary> Initial value. Use it only to create a new Tween. </summary>
        /// <returns>This.</returns>
        public Tween<T> Origin(T start) { origin = start; return this; }

        /// <summary> Final value. Use it only to create a new Tween. </summary>
        /// <returns>This.</returns>
        public Tween<T> Destination(T end) { destination = end; return this; }

        /// <summary> Time to execute the operation, must be greater than 0. Use it only to create a new Tween. </summary>
        /// <returns>This.</returns>
        public Tween<T> Duration(float duration) { this.duration = Mathf.Max(duration, 0.0f); return this; }

        /// <summary> Execution mode. Use it only to create a new Tween. </summary>
        /// <returns>This.</returns>
        public Tween<T> Loop(TweenLoop loop) { this.loop = loop; return this; }
    
        /// <summary> Easing function. Overwrite the In and Out functions. Use it only to create a new Tween. </summary>
        /// <param name="ease">Easing function.</param>
        /// <returns>This.</returns>
        public Tween<T> Easing(Ease ease) { easeIn = easeOut = ease; return this; }
    
        /// <summary> Easing In function. Use it only to create a new Tween. </summary>
        /// <param name="ease">Easing function.</param>
        /// <returns>This.</returns>
        public Tween<T> EasingIn(Ease ease) { easeIn = ease; return this; }
    
        /// <summary> Easing Out function. Use it only to create a new Tween. </summary>
        /// <param name="ease">Easing function.</param>
        /// <returns>This.</returns>
        public Tween<T> EasingOut(Ease ease) { easeOut = ease; return this; }
    
        /// <summary> Update callback. Use it to apply the Tween values. </summary>
        /// <param name="updateCallback">Callback.</param>
        /// <returns>This.</returns>
        public Tween<T> OnUpdate(Action<Tween<T>> updateCallback) { updateFunction = updateCallback; return this; }

        /// <summary> Executed at the end of the operation (optional). </summary>
        /// <param name="endCallback">Callback.</param>
        /// <returns>This.</returns>
        public Tween<T> OnEnd(Action<Tween<T>> endCallback) { endFunction = endCallback; return this; }

        /// <summary> Condition of progress, stops if the operation is not true (optional). </summary>
        /// <param name="condition">Condition function.</param>
        /// <returns>This.</returns>
        public Tween<T> Condition(Func<Tween<T>, bool> condition) { this.condition = condition; return this; }

        /// <summary> Limits the values of the interpolation to the range [0, 1]. </summary>
        /// <param name="clamp">Clamp.</param>
        /// <returns>This.</returns>
        public Tween<T> Clamp(bool clamp) { this.clamp = clamp; return this; }
    
        /// <summary>
        /// I set an object as the 'owner' of the Tween. If the object is destroyed, the Tween ends and is destroyed.
        /// </summary>
        /// <param name="owner">Owner</param>
        /// <returns>This.</returns>
        public Tween<T> Owner(object owner) { IsOwned = owner != null; this.owner = owner; return this; }

        /// <inheritdoc/>
        public Tween<T> Start()
        {
            Debug.Assert(duration > 0.0f, "[FronkonGames.TinyTween] The duration of the tween should be greater than zero.");
            Debug.Assert(easeIn != Ease.None && easeOut != Ease.None, "[FronkonGames.TinyTween] You must set some kind of Ease.");

            State = TweenState.Running;
            UpdateValue();

            return this;
        }

        /// <inheritdoc/>
        public void Pause() => State = TweenState.Paused;

        /// <inheritdoc/>
        public void Resume() => State = TweenState.Running;

        /// <inheritdoc/>
        public void Stop(bool moveToEnd = true)
        {
            if (State != TweenState.Finished)
            {
                State = TweenState.Finished;
                if (moveToEnd == true)
                {
                    currentTime = duration;

                    UpdateValue();
                }

                endFunction?.Invoke(this);
            }
        }

        /// <inheritdoc/>
        public void Reset()
        {
            currentTime = 0.0f;
            Value = origin;
        }
    
        /// <inheritdoc/>
        public void Update()
        {
            if (IsOwned == true && owner.Equals(null) || condition != null && condition(this) == false)
                Stop(false);
            else
            {
                currentTime += UnityEngine.Time.deltaTime;
                if (currentTime >= duration)
                {
                    residueCount--;
                    ExecutionCount++;

                    switch (loop)
                    {
                        case TweenLoop.Once: Stop(); break;

                        case TweenLoop.Loop:
                            if (residueCount == 0)
                                Stop();

                            Value = origin;
                            currentTime = Progress = 0.0f;
                            break;

                        case TweenLoop.YoYo:
                            if (residueCount == 0)
                                Stop();

                            (destination, origin) = (origin, destination);
                            currentTime = Progress = 0.0f;
                            break;

                        default: throw new ArgumentOutOfRangeException();
                    }
                }
                else
                    UpdateValue();
            }
        }

        private void UpdateValue()
        {
            float t = currentTime / duration;
            Progress = EasingFunctions.Calculate(t > 0.5f ? easeOut : easeIn, easeIn != Ease.None, easeOut != Ease.None, t);

            Value = interpolationFunction(this, origin, destination, Progress, clamp);

            updateFunction?.Invoke(this);
        }
    }
}