using Unity.Collections;
using Unity.Entities;

namespace beerserk.SegmentedTween
{
    [UpdateInGroup(typeof(SegmentedTweeningSystemGroup))]
    [UpdateAfter(typeof(UpdateSegmentSystem))]
    [UpdateBefore(typeof(ApplyTweeningSystemGroup))]
    public class CopySegmentToTargetsSystem : SystemBase
    {
        private EntityQuery _sourceSegments;
        protected override void OnCreate()
        {
            _sourceSegments = GetEntityQuery(
                ComponentType.ReadOnly<CopySegmentToTarget>(),
                ComponentType.ReadOnly<Segment>());
            RequireForUpdate(_sourceSegments);
        }
        protected override void OnUpdate()
        {
            var segments = new NativeArray<Segment>(_sourceSegments.CalculateEntityCount(), Allocator.TempJob);
            Entities.WithName("save_segments")
                .WithAll<CopySegmentToTarget>()
                .ForEach((int entityInQueryIndex, in Segment segment) => segments[entityInQueryIndex] = segment).Schedule();

            Entities.WithName("copy_to_targets")
                .WithAll<Segment>()
                .WithDeallocateOnJobCompletion(segments)
                .ForEach((int entityInQueryIndex, in DynamicBuffer<CopySegmentToTarget> targets) =>
                {
                    var segment = segments[entityInQueryIndex];
                    for (int i = 0; i < targets.Length; i++)
                    {
                        var target = targets[i].Target;
                        if (HasComponent<Segment>(target)) SetComponent(target, segment);
                    }
                }).Schedule();
        }
    }
}