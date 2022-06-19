using CodeBase.Logic.EnemySpawner;
using CodeBase.StaticData;
using UnityEditor;
using System.Linq;
using CodeBase.Logic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
                    .Select(x => new EnemySpawnerData(x.GetComponent<UniqueID>().Id, x._monsterTypeID, x.transform.position))
                    .ToList();
                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }
            EditorUtility.SetDirty(target);
        }
    }
}