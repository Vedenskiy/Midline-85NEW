namespace FronkonGames.TinyTween.Tweens
{
    /// <summary> Generic interface of a tween. </summary>
    public interface ITween<T> : ITween where T : struct
    {
        /// <summary> Current value. </summary>
        T Value { get; }

        /// <summary> Tween operation progress (0, 1). </summary>
        float Progress { get; }

        /// <summary> Executions counter. </summary>
        int ExecutionCount { get; }
    
        /// <summary> Tween status. </summary>
        TweenState State { get; }
    
        /// <summary> Execute a tween operation. </summary>
        Tween<T> Start();

        /// <summary> Pause the tween. </summary>
        void Pause();

        /// <summary> Continue the tween. </summary>
        void Resume();

        /// <summary> Finish the Tween operation. </summary>
        /// <param name="moveToEnd">Move the value at the end or leave it as this.</param>
        void Stop(bool moveToEnd = true);

        /// <summary> Sets tween value at origin and time to 0. </summary>
        void Reset();
    
        /// <summary> Update the Tween operation. </summary>
        void Update();
    }

    /// <summary> Interface of a tween. </summary>
    public interface ITween
    {
        /// <summary> Tween status. </summary>
        TweenState State { get; }

        /// <summary> Update the Tween operation. </summary>
        void Update();
    }
}