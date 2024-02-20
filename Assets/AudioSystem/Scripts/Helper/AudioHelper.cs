using System;
using Long18.AudioSystem.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Long18.AudioSystem.Helper
{
    public static class AudioHelper
    {
        public static void TryToLoadData(AudioCueSO audioCue, Action<AudioClip> callback)
        {
            AssetReferenceT<AudioClip> currentCue = audioCue.GetPlayableAsset();

            if (currentCue.IsValid())
            {
                if (currentCue.Asset != null)
                {
                    callback?.Invoke((AudioClip)currentCue.Asset);
                    return;
                }

                currentCue.ReleaseAsset();
            }

            currentCue.LoadAssetAsync().Completed += handle => { callback?.Invoke(handle.Result); };
        }
    }
}