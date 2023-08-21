using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gamekit2D
{
    public class AkAudioSource : MonoBehaviour
    {
        public AK.Wwise.Event akEvent;

        public AK.Wwise.Switch defaultSwitch;

        [System.Serializable]
        public struct TileOverride
        {
            public TileBase tile;
            public AK.Wwise.Switch akSwitch;
        }

        public TileOverride[] overrides;

        protected Dictionary<TileBase, AK.Wwise.Switch> m_LookupOverride;

        private void Awake()
        {
            m_LookupOverride = new Dictionary<TileBase, AK.Wwise.Switch>();

            for (int i = 0; i < overrides.Length; ++i)
            {
                if (overrides[i].tile == null)
                    continue;

                m_LookupOverride[overrides[i].tile] = overrides[i].akSwitch;
            }
        }

        public void PostEvent(TileBase surface = null)
        {
            AK.Wwise.Switch temp = defaultSwitch;
            if (surface != null && m_LookupOverride.TryGetValue(surface, out temp))
            {
                temp.SetValue(gameObject);
            }

            temp.SetValue(gameObject);

            akEvent.Post(gameObject);
        }

        public void Play()
        {
            akEvent.Post(gameObject);
        }

        public void Stop()
        {
            akEvent.Stop(gameObject);
        }
    }
}
