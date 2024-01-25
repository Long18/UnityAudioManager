using UnityEngine;

namespace Long18.System.Audio.Data
{
    public delegate void AudioCueEventDelegate(AudioCueSO audioToPlay, bool requestPlay);

    /// <summary>
    /// Event channel for handling audio cues in a decoupled manner.
    /// This ScriptableObject serves as a communication hub for broadcasting requests
    /// to play audio cues. It utilizes the <see cref="AudioCueEventDelegate"/> delegate
    /// to define the signature of the event.
    /// </summary>
    public class AudioCueEventChannelSO : ScriptableObject
    {
        /// <summary>
        /// Event triggered when an audio cue is requested to be played.
        /// Subscribers should listen for this event and respond accordingly.
        /// </summary>
        public event AudioCueEventDelegate OnRequested;

        /// <summary>
        /// Plays the specified audio cue with an optional request flag.
        /// </summary>
        /// <param name="audioToPlay">The <see cref="AudioCueSO"/> to be played.</param>
        /// <param name="requestPlay">Optional flag indicating whether the play request is intentional. Default is true.</param>
        public void PlayAudio(AudioCueSO audioToPlay, bool requestPlay = true)
        {
            if (audioToPlay == null)
            {
                Debug.LogWarning($"The current audio was null");
                return;
            }

            if (audioToPlay.GetClips().Length == 0)
            {
                Debug.LogWarning("The current audio was empty source");
                return;
            }

            if (OnRequested == null)
            {
                Debug.Log("Event was raised but no one was listening");
                return;
            }

            OnRequested!.Invoke(audioToPlay, requestPlay);
        }
    }
}