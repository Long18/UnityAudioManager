namespace Long18.AudioSystem.Data.Utils
{
    public interface IListIndex
    {
        int Value { get; }
        IListIndex GoForward(int elementCount);
        IListIndex GoBackward(int elementCount);
    }
}