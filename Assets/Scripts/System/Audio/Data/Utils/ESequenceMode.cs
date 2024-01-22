namespace Long18.System.Audio.Data.Utils
{
    /// <summary>
    /// Enum for audio clips sequence mode
    /// </summary>
    public enum ESequenceMode
    {
        /// <summary>
        ///  Randomly play audio clips
        /// </summary>
        Random = 0,

        /// <summary>
        ///  Play audio clips in order and repeat from start when end is reached
        /// </summary>
        Repeat = 1,

        /// <summary>
        ///  Play audio clips in order and stop when end is reached 
        /// </summary>
        Sequential = 2,
    }
}