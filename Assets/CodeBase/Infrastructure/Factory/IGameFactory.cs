using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistantProgress;
using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject HUDGameobject { get; }
        void CleanUp();
        GameObject CreateMonster(MonsterTypeID monsterTypeID, Transform parent);
        LootPiece CreateLoot();
        void CreateSpawner(string id, MonsterTypeID MonsterTypeID, Vector3 position);
    }
}