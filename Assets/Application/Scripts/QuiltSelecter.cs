using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace Wotakuro
{
    public class QuiltSelecter : MonoBehaviour
    {
        public UnityEngine.UI.Text dbgTxt;
        public UnityEngine.UI.RawImage dbgImg;
        public UnityEngine.UI.RawImage dbgImg2;
        public UnityEngine.UI.InputField xNumField;
        public UnityEngine.UI.InputField yNumField;
        public UnityEngine.UI.InputField renderAspectField;

        private QuiltToRenderTexture quiltRenderer;

        [SerializeField]
        private UnityEngine.UI.RawImage outputImage;

        [SerializeField]
        private VideoPlayer videoPlayer;

        private DisplayData displayData;

        private Texture2D quiltLoadTexture;

        private RenderTexture videoRt;
        private QuiltViewInfo viewInfo = new QuiltViewInfo();

        // Start is called before the first frame update
        public IEnumerator Start()
        {
            this.quiltLoadTexture = new Texture2D(2, 2, TextureFormat.RGBA32, false);

            quiltRenderer = new QuiltToRenderTexture();
            quiltRenderer.Initialize();
            yield return null;


            string path = System.IO.Path.Combine( Application.streamingAssetsPath , "visual.json");
            var request = UnityWebRequest.Get(path);
            yield return request.SendWebRequest();

            var str = request.downloadHandler.text;
            dbgTxt.text = str;
            var dispJson = OriginalDisplayJson.CreateFromString(str);
            this.displayData = dispJson.ConvertToData();

            MediaSelector.onSelectImage = OnSelectImage;
            MediaSelector.onSelectVideo = OnSelectVideo;

            //
            xNumField.text = "8";
            yNumField.text = "6";

            // Check the number of monitors connected.
            if (Display.displays.Length > 1)
            {
                // Activate the display 1 (second monitor connected to the system).
                Display.displays[1].Activate();
            }
        }

        void OnSelectVideo(string path)
        {
            dbgTxt.text = "OnSelectVideo " + path;
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = path;
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += (player) =>
            {
                dbgTxt.text = "PrepareComplete " + path;
                if (videoRt)
                {
                    videoRt.Release();
                }
                videoRt = new RenderTexture((int)videoPlayer.width, (int)videoPlayer.height, 0);
                videoPlayer.renderMode = VideoRenderMode.RenderTexture;
                videoPlayer.targetTexture = videoRt;
                videoPlayer.Play();
            };
        }

        private void Update()
        {
            if(!videoPlayer || !videoPlayer.isPlaying)
            {
                return;
            }
            viewInfo.tileXNum = int.Parse(xNumField.text);
            viewInfo.tileYNum = int.Parse(yNumField.text);
            viewInfo.renderAspect = float.Parse(renderAspectField.text);

            viewInfo.quiltWidth = videoRt.width;
            viewInfo.quiltHeight = videoRt.height;
            this.quiltRenderer.Setup(this.displayData, viewInfo);

            var rt = quiltRenderer.RenderFromQuilt(this.videoRt);

            dbgImg2.texture = rt;

            this.outputImage.texture = rt;
        }

        void OnSelectImage(string path)
        {

            if (videoPlayer && videoPlayer.isPlaying)
            {
                videoPlayer.Stop();
            }
            dbgTxt.text = path;
            quiltLoadTexture.LoadImage(System.IO.File.ReadAllBytes(path));


            viewInfo.tileXNum = int.Parse(xNumField.text);
            viewInfo.tileYNum = int.Parse(yNumField.text);
            viewInfo.renderAspect = float.Parse(renderAspectField.text);

            viewInfo.quiltWidth = quiltLoadTexture.width;
            viewInfo.quiltHeight = quiltLoadTexture.height;

            this.quiltRenderer.Setup(this.displayData, viewInfo);
            dbgImg.texture = quiltLoadTexture;

            var rt = quiltRenderer.RenderFromQuilt(quiltLoadTexture);
            dbgImg2.texture = rt;

            this.outputImage.texture = rt;
        }

    }
}
