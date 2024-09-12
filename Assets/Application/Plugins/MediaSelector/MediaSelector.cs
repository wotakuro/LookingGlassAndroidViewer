using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wotakuro
{
    public class MediaSelector
    {
        public delegate void OnSelectMedia(string path);

        public static OnSelectMedia onSelectImage;
        public static OnSelectMedia onSelectVideo;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            var gmo = new GameObject("AndroidMediaSelector");
            GameObject.DontDestroyOnLoad(gmo);
            var proxy = gmo.AddComponent<MediaSelectorProxy>();
            //RequestAllFileAccess();
        }

        public static void SelectImage()
        {
            using var staticCls = new AndroidJavaClass("com.wotakuro.lkgview.LookingGlassViewerActivity");
            staticCls.CallStatic("selectImage");
        }
        public static void SelectVideo()
        {
            using var staticCls = new AndroidJavaClass("com.wotakuro.lkgview.LookingGlassViewerActivity");
            staticCls.CallStatic("selectVideo");
        }


        public static void RequestAllFileAccess()
        {
            Debug.Log("SelectImage");
            using var staticCls = new AndroidJavaClass("com.wotakuro.lkgview.LookingGlassViewerActivity");
            staticCls.CallStatic("requestAllFileAccess");
        }
        private static void OnUpdate()
        {

        }
    }
}