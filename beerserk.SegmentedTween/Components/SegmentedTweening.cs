using Unity.Entities;

namespace beerserk.SegmentedTween
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