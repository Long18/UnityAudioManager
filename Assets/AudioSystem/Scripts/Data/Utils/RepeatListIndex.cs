namespace Long18.AudioSystem.Data.Utils
{
    public class RepeatListIndex : IListIndex
    {
        public int Value { get; private set; } = 0;

        public IListIndex GoForward(int elementCount)
        {
            Value = (Value + 1) % elementCount;
            return this;
        }

        public IListIndex GoBackward(int elementCount)
        {
            Value = (Value - 1 + elementCount) % elementCount;
            return this;
        }
    }
}