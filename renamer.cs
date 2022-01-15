using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Text;

public class renamer : EditorWindow
{
    string nameToRemove = "";
    string nameToReplace = "";
    string folderPath = "";
    string[] files;

    [MenuItem("Yelby/Renamer")]
    public static void ShowWindow()
    {
        GetWindow<renamer>("Renamer");
    }
    private void OnGUI()
    {
        GUILayout.Label("Version: 1.0");
        GUILayout.BeginVertical();
        nameToRemove = EditorGUILayout.TextField("Remove", nameToRemove);
        nameToReplace = EditorGUILayout.TextField("Replace", nameToReplace);
        GUILayout.EndVertical();

        GUILayout.Label(folderPath);
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Folder"))
        {
            folderPath = EditorUtility.OpenFolderPanel("Folder to Rename", "", "");
            files = Directory.GetFiles(folderPath);
        }

        if (GUILayout.Button("Rename Items"))
        {
            files = Directory.GetFiles(folderPath);
            if (files.Length == 0)
            {
                Debug.Log("Folder Empty");
                return;
            }

            for (int i = 0; i < files.Length; i++)
            {
                if (!files[i].Contains(".meta"))
                {
                    if(files[i].Contains(".anim") && files[i].Contains(nameToRemove))
                    {
                        string assetPath = files[i].Substring(files[i].IndexOf("Assets/")).Replace('/', '\\');
                        var asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(AnimationClip));
                        AssetDatabase.RenameAsset(assetPath, asset.name.Replace(nameToRemove, nameToReplace));
                    }
                }
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        GUILayout.EndHorizontal();
    }
}
