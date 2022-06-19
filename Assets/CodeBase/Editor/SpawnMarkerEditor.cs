﻿using CodeBase.Logic;
using CodeBase.Logic.EnemySpawner;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType type)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawner.transform.position, 1f);
        }
    }
}