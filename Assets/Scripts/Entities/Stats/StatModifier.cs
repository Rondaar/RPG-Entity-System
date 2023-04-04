namespace Entities.Stats
{
    public class StatModifier
    {
        public float Value { get; private set; }
        public string Source { get; private set; }

        public StatModifier(float value, string source)
        {
            Value = value;
            Source = source;
        }
    }
}