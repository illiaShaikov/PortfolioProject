using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeID MonsterTypeID;
        public GameObject MonsterPrefab;
        [Range(1,100)]
        public int Hp;
        [Range(1,20)]
        public int MoveSpeed;
        [Range(1,5)]
        public int AttackCooldown;
        [Range(1,100)]
        public float Damage;
        [Range(0.5f,20)]
        public float EffectiveDistance;
        [Range(0.5f,20)]
        public float Cleavage;

        public int MinLoot;
        public int MaxLoot;

    }
}

