using Unity.Entities;

namespace beerserk.SegmentedTween
{
    [UpdateBefore(typeof(Unity.Transforms.TransformSystemGroup))]
    public class SegmentedTweeningSystemGroup : ComponentSystemGroup { }
}