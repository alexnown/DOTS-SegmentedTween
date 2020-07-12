using Unity.Entities;

namespace beerserk.SegmentedTween
{
    public struct CopySegmentToTarget : IBufferElementData
    {
        public Entity Target;
    }
}