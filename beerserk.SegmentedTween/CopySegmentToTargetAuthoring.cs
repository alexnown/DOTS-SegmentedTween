using UnityEngine;
using Unity.Entities;

namespace beerserk.SegmentedTween
{
    public class CopySegmentToTargetAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public GameObject[] Targets;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var targetsBuffer = dstManager.AddBuffer<CopySegmentToTarget>(entity);
            for (int i = 0; i < Targets.Length; i++)
            {
                targetsBuffer.Add(new CopySegmentToTarget { Target = conversionSystem.GetPrimaryEntity(Targets[i]) });
            }
        }
    }
}