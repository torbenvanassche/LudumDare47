﻿using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomTileContainer))]
public class RoomContainerEditor : OdinEditor
{
    protected override void OnEnable()
    {
        var t = target as RoomTileContainer;

        var clips = AssetDatabase.FindAssets($"t:{nameof(Sprite)}", new[] {"Assets/Sprites/Source"});
        
        //find function data
        var functions = AssetDatabase.FindAssets($"t:{nameof(TileFunctions)}", new[] {"Assets/Sprites"});
        if (functions.Length == 0)
        {
            AssetDatabase.CreateAsset(CreateInstance<TileFunctions>(), "Assets/Sprites/TileFunctions.asset");
        }

        foreach (var clip in clips)
        {
            var path = AssetDatabase.GUIDToAssetPath(clip);
            var container = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            if (t.data.All(pair => pair.Value != container))
            {
                t.data.Add(container.name, container);
            }
        }
    }
}