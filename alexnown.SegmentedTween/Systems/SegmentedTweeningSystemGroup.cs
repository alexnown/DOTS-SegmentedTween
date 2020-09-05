using Unity.Entities;

namespace alexnown.SegmentedTween
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public class SegmentedTweeningSystemGroup : ComponentSystemGroup { }
}