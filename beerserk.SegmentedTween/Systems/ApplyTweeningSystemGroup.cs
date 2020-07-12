using Unity.Entities;

namespace beerserk.SegmentedTween
{
    [UpdateInGroup(typeof(SegmentedTweeningSystemGroup), OrderLast = true)]
    //[UpdateAfter(typeof(UpdateSegmentSystem))]
    public class ApplyTweeningSystemGroup : ComponentSystemGroup { }
}