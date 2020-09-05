using Unity.Entities;

namespace alexnown.SegmentedTween
{
    public struct CopySegmentToTarget : IBufferElementData
    {
        public Entity Target;
    }
}