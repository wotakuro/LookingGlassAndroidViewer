using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wotakuro
{
    [System.Serializable]
    public class QuiltViewInfo
    {
        [SerializeField]
        public int quiltWidth;
        [SerializeField]
        public int quiltHeight;

        public int tileWidth
        {
            get
            {
                return (quiltWidth / tileXNum);
            }
        }
        public int tileHeight
        {
            get
            {
                return (quiltHeight / tileYNum);
            }
        }

        [SerializeField]
        public int tileXNum;
        [SerializeField]
        public int tileYNum;

        [SerializeField]
        public float renderAspect;

        public int tileCount { 
            get
            {
                return tileXNum * tileYNum;
            } 
        }
        public float ViewPortionHorizontal => ((float)tileXNum * tileWidth) / quiltWidth;
        public float ViewPortionVertical => ((float)tileYNum * tileHeight) / quiltHeight;


    }

}