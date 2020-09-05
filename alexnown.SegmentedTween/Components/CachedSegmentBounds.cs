using Unity.Entities;

namespace alexnown.SegmentedTween
{
    [WriteGroup(typeof(Segment))]
    public struct CachedSegmentBounds : IComponentData
    {
        public float Start;
        public float End;
    }
}