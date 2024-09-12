using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

namespace Wotakuro
{
    internal class MediaSelectorProxy : MonoBehaviour
    {
        public System.Action onUpdate;
        public void OnSelectImage(string url)
        {
            if(MediaSelector.onSelectImage != null)
            {
                MediaSelector.onSelectImage(url);
            }
        }
        private void Start()
        {

        }
        private void Update()
        {
            if (onUpdate != null) { onUpdate(); }
        }

    }
}