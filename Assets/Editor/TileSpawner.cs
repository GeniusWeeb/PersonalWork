using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace Platformer
{

    [CustomEditor(typeof(PlatformManager))]
    [CanEditMultipleObjects]
    public class TileSpawner : Editor
    {

        public override void OnInspectorGUI()
        {

            PlatformManager manager = target as PlatformManager;

            base.OnInspectorGUI();
            if (GUILayout.Button("spawn tile"))
                manager.SpawnTile();

        }

    }

}
