using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Video;

public class TestScript : MonoBehaviour
{

    public UnityEngine.UI.Text txt;
    public UnityEngine.UI.Text resolutionText;
    public UnityEngine.UI.RawImage img;

    private VideoPlayer videoPlayer;
    private RenderTexture videoRt;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        Wotakuro.MediaSelector.onSelectImage = OnSelectImg;
        Wotakuro.MediaSelector.onSelectVideo = OnSelectVideo;
    }
    private void OnSelectImg(string path)
    {
        Debug.Log(path);
        txt.text = path;
        Texture2D texture = new Texture2D(2,2);
        texture.LoadImage(System.IO.File.ReadAllBytes(path));
        img.texture = texture;
        resolutionText.text = texture.width + "x" + texture.height;
    }
    private void OnSelectVideo(string path)
    {
        Debug.Log(path);
        txt.text = path;
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = path;
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (player) =>
        {
            if (videoRt)
            {
                videoRt.Release();
            }
            videoRt = new RenderTexture((int)videoPlayer.width, (int)videoPlayer.height, 0);
            resolutionText.text = videoPlayer.width + "x" + videoPlayer.height;
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.targetTexture = videoRt;

            img.texture = videoRt;
            videoPlayer.Play();
        };

    }



    public void OnPressRequestFileAccess()
    {
        Wotakuro.MediaSelector.RequestAllFileAccess();
    }

    public void OnPressSelectImage()
    {
        Wotakuro.MediaSelector.SelectImage();
    }
    public void OnPressSelectVideo()
    {
        Wotakuro.MediaSelector.SelectVideo();
    }
}
