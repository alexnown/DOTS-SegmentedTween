using Unity.Entities;

namespace beerserk.SegmentedTween
{
    [GenerateAuthoringComponent]
    [WriteGroup(typeof(Segment))]
    public struct IsUniformSegments : IComponentData
    {
        public float SegmentLength;
    }
}