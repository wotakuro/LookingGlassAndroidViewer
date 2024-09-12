package com.wotakuro.lkgview;

import android.content.*;
import android.net.*;
import android.os.*;
import android.provider.*;
import android.util.*;
import com.unity3d.player.*;
import java.io.*;
import java.util.*;

import android.database.Cursor;

import static com.unity3d.player.UnityPlayer.UnitySendMessage;


public class LookingGlassViewerActivity extends UnityPlayerActivity {

  // Activityリクエストコード
  private static final int IMAGE_REQUEST_CODE = 83748;
  private static final int VIDEO_REQUEST_CODE = 83750;

  protected void onCreate(Bundle savedInstanceState) {
    super.onCreate(savedInstanceState);
  }

  @Override
  protected void onActivityResult(
    int requestCode,
    int resultCode,
    Intent resultData
  ) {
    super.onActivityResult(requestCode, resultCode, resultData);
    if(resultData == null){
      return;
    }
    if(resultData.getData() == null ){
      return;
    }
    if (requestCode == IMAGE_REQUEST_CODE ) {
      Uri uri = resultData.getData();
      String path = GetMediaPath(uri , MediaStore.Images.Media.EXTERNAL_CONTENT_URI );
      UnitySendMessage("AndroidMediaSelector","OnSelectImage", path);
    }else if (requestCode == VIDEO_REQUEST_CODE ) {
      Uri uri = resultData.getData();
      String path = GetMediaPath(uri , MediaStore.Video.Media.EXTERNAL_CONTENT_URI );
      UnitySendMessage("AndroidMediaSelector","OnSelectVideo", path);
    }
  }

  /**
    * 全ファイルへのアクセスを得ます
    */
  public static void requestAllFileAccess(){
    
    Intent intent = new Intent(Settings.ACTION_MANAGE_ALL_FILES_ACCESS_PERMISSION);
    UnityPlayer.currentActivity.startActivity(intent);
  }

  /**
   * 画像選択を開く
   */
  public static void selectImage() {
    Uri collection;
    if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.Q) {
      collection = MediaStore.Images.Media.getContentUri(MediaStore.VOLUME_EXTERNAL);
    } else {
      collection = MediaStore.Images.Media.EXTERNAL_CONTENT_URI;
    }
    Intent intent = new Intent(
      Intent.ACTION_OPEN_DOCUMENT,
      collection);
    
    intent.setType("image/*");
    UnityPlayer.currentActivity.startActivityForResult(intent,IMAGE_REQUEST_CODE);
  }

  /**
   * ビデオ選択を開く
   */
  public static void selectVideo() {
    Uri collection;
    if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.Q) {
      collection = MediaStore.Video.Media.getContentUri(MediaStore.VOLUME_EXTERNAL);
    } else {
      collection = MediaStore.Video.Media.EXTERNAL_CONTENT_URI;
    }
    Intent intent = new Intent(
      Intent.ACTION_OPEN_DOCUMENT,
      collection);
    
    intent.setType("video/*");
    UnityPlayer.currentActivity.startActivityForResult(intent,VIDEO_REQUEST_CODE);
  }


  /** メディアのURIからPathを取得します
    */
  private String GetMediaPath(Uri uri,Uri mediaType){
    
    String inputPath = "";
    try {
      // ファイルパス取得
      String strDocId = DocumentsContract.getDocumentId(uri);
      String[] strSplittedDocId = strDocId.split(":");
      String strId = strSplittedDocId[strSplittedDocId.length - 1];
      
      Cursor cursor = getContentResolver().query(
        mediaType,
        new String[]{MediaStore.MediaColumns.DATA},
        "_id=?", new String[]{strId},
        null
          );
      
      if (!cursor.moveToFirst()) {
        // failed
        cursor.close();
      } else {
        // success
        inputPath = cursor.getString(0);
        cursor.close();
        
        // inputPath : /storage/emulated/0/DCIM/Camera/VID_20190313_214028.mp4
      }
    }catch (Exception e){
    }
    return inputPath;
  }

}
