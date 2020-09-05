using Unity.Entities;

namespace alexnown.SegmentedTween
{
    public enum TweeningType : byte
    {
        Clamped,
        Yoyo,
        Incremental
    }

    public struct SegmentedTweening : IComponentData
    {
        public float Speed;
        public TweeningType Type;
    }
}