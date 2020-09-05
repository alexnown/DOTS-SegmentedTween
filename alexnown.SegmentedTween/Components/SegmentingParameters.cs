using Unity.Entities;

namespace alexnown.SegmentedTween
{
    public struct SegmentingParameters : IComponentData
    {
        public float TotalLength;
        public int SegmentsCount;
    }
}