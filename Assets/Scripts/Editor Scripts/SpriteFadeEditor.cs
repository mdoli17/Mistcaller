using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor (typeof (FadeOut))]
public class SpriteFadeEditor : Editor
{
    string[] choices = new [] { "Fade", "Darken" };
    int index;
    
    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector();
        index = EditorGUILayout.Popup(index, choices);
        var fader = target as FadeOut;
        if(index == 0)
            fader.setDarken();
        else
            fader.setDarken();
        EditorUtility.SetDirty(target);
    }
}
