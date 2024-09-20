namespace Common
{
    public record Range
    {
        public int Min { get; }
        public int Max { get; }
        
        public Range(int min, int max)
        {
            if (min > max)
            {
                (min, max) = (max, min);
            }
            
            Min = min;
            Max = max;
        }
        
        public int GetRandom()
        {
            return UnityEngine.Random.Range(Min, Max);
        }
        
        public bool Contains(float number)
        {
            return number >= Min && number <= Max;
        }
    }
}