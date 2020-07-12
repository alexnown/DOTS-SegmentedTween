using Unity.Entities;

namespace beerserk.SegmentedTween
{
    [WriteGroup(typeof(Segment))]
    public struct CachedSegmentBounds : IComponentData
    {
        public float Start;
        public float End;
    }
}