using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

namespace Wotakuro
{
    internal class MediaSelectorProxy : MonoBehaviour
    {
        private List<string> imagesPath = new List<string>();
        private List<string> videosPath = new List<string>();
        private List<string> tmpList = new List<string>();

        public void OnSelectImage(string url)
        {
            lock (imagesPath)
            {
                imagesPath.Add(url);
            }
        }
        public void OnSelectVideo(string url)
        {
            lock(videosPath)
            {
                videosPath.Add(url);
            }
        }

        private void Update()
        {
            lock (imagesPath)
            {
                tmpList.Clear();
                foreach(var path in imagesPath)
                {
                    tmpList.Add(path);
                }
                imagesPath.Clear();
            }
            foreach( var path in tmpList)
            {
                if (MediaSelector.onSelectImage != null)
                {
                    MediaSelector.onSelectImage(path);
                }
            }

            lock (videosPath)
            {
                tmpList.Clear();
                foreach (var path in videosPath)
                {
                    tmpList.Add(path);
                }
                videosPath.Clear();
            }
            foreach (var path in tmpList)
            {
                if (MediaSelector.onSelectVideo != null)
                {
                    MediaSelector.onSelectVideo(path);
                }
            }

        }

    }
}