namespace Long18.System.Audio.Data.Utils
{
    public static class ListIndexFactory
    {
        public static IListIndex Create(ESequenceMode sequenceMode) => sequenceMode switch
        {
            ESequenceMode.Random => new RandomListIndex(),
            ESequenceMode.Repeat => new UniqueRandomListIndex(),
            _ => new RepeatListIndex()
        };
    }
}