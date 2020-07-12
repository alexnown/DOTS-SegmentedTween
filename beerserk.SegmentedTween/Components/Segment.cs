using Unity.Entities;

namespace beerserk.SegmentedTween
{
    [GenerateAuthoringComponent]
    public struct Segment : IComponentData
    {
        public int Index;
        public float Ratio;
    }
}