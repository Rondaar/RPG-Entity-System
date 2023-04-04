using UnityEngine;

namespace Entities.Effects
{
    public abstract class ContinuousEffect<TriggerArgs> : EffectBase<TriggerArgs> where TriggerArgs : EffectArgs
    {
        private float duration;

        public bool IsInfinite { get; protected set; }

        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public override bool ExpirationConditions => Duration <= 0;

        public ContinuousEffect(string name, float startingDuration)
        {
            Name = name;
            duration = startingDuration;
            IsInfinite = false;
        }

        /// <summary>
        /// If you want an effect to be infinite, use this constructor
        /// </summary>
        public ContinuousEffect(string name)
        {
            Name = name;
            IsInfinite = true;
        }

        protected override void OnPerform(TriggerArgs args)
        {
            if (Duration <= 0 || IsInfinite)
            {
                return;
            }

            Duration -= Time.deltaTime;
        }

        public override string ToString() => Name + ": " +
                                             (IsInfinite ? "Infinite" : Duration.ToString()) + " duration remaining";
    }
}