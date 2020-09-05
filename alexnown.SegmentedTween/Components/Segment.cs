using Unity.Entities;

namespace alexnown.SegmentedTween
{
    [GenerateAuthoringComponent]
    public struct Segment : IComponentData
    {
        public int Index;
        public float Ratio;
    }
}