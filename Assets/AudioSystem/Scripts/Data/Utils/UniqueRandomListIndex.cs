namespace Long18.AudioSystem.Data.Utils
{
    public class UniqueRandomListIndex : RandomListIndex
    {
        protected override int GenerateRandomIndex(int elementCount)
        {
            int nextIndex;
            do
            {
                nextIndex = base.GenerateRandomIndex(elementCount);
            } while (elementCount > 1 && nextIndex == Value);

            return nextIndex;
        }
    }
}