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
    // Start is called before the first frame update
    void Start()
    {
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
