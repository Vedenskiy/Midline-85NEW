using System;

namespace CodeBase.Features.Calls.Handlers.Choices
{
    public class ChoiceTimer
    {
        public float Elapsed { get; private set; }
        
        public float Duration { get; private set; }

        public float Progress => Elapsed / Duration;
        
        public bool IsElapsed { get; private set; }

        public event Action Started;

        public event Action Stopped;

        public event Action<float> Updated; 

        public void Start(float duration)
        {
            Duration = duration;
            Elapsed = 0f;
            IsElapsed = false;
            Started?.Invoke();
        }

        public void Update(float tick)
        {
            if (IsElapsed)
                return;
            
            Elapsed += tick;
            IsElapsed = Elapsed >= Duration;
            Updated?.Invoke(Progress);
        }

        public void Stop()
        {
            Elapsed = 0f;
            IsElapsed = false;
            Stopped?.Invoke();
        }
    }
}