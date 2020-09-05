using Unity.Entities;

namespace alexnown.SegmentedTween
{
    [GenerateAuthoringComponent]
    [WriteGroup(typeof(Segment))]
    public struct IsUniformSegments : IComponentData
    {
        public float SegmentLength;
    }
}