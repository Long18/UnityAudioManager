namespace Long18.System.Audio.Data.Utils
{
    public interface IListIndex
    {
        int Value { get; }
        IListIndex GoForward(int elementCount);
        IListIndex GoBackward(int elementCount);
    }
}