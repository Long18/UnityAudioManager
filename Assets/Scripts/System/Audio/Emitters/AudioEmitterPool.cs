using Long18.System.Audio.Helper;
using UnityEngine;

namespace Long18.System.Audio.Emitters
{
    public class AudioEmitterPool : ObjectPoolBase<AudioEmitter>
    {
        [SerializeField] private AudioEmitter _prefab;
        public AudioEmitter Request() => _pool.Get();
        protected override void OnDestroyObject(AudioEmitter obj) => Destroy(obj.gameObject);
        protected override void OnReleaseObject(AudioEmitter obj) => obj.gameObject.SetActive(false);

        protected override void OnGetObject(AudioEmitter obj)
        {
            obj.transform.SetAsFirstSibling();
            obj.gameObject.SetActive(true);
        }

        protected override AudioEmitter OnCreateObject()
        {
            AudioEmitter soundEmitter = Instantiate(_prefab, transform);
            soundEmitter.Init(_pool);
            return soundEmitter;
        }
    }
}