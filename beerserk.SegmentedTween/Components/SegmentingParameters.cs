using Unity.Entities;

namespace beerserk.SegmentedTween
{
    public struct SegmentingParameters : IComponentData
    {
        public float TotalLength;
        public int SegmentsCount;
    }
}