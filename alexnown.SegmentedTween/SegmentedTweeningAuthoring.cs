using UnityEngine;
using Unity.Entities;

namespace alexnown.SegmentedTween
{
    public class SegmentedTweeningAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float Speed = 1;
        public TweeningType Type;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, default(Disposition));
            dstManager.AddComponentData(entity, default(Segment));
            dstManager.AddComponentData(entity, new SegmentedTweening { Speed = Speed, Type = Type });
        }
    }
}