using Unity.Entities;

namespace alexnown.SegmentedTween
{
    [UpdateInGroup(typeof(SegmentedTweeningSystemGroup))]
    [UpdateAfter(typeof(UpdateSegmentSystem))]
    public class CopySegmentToTargetsSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var segmentsFromEntity = GetComponentDataFromEntity<Segment>(false);
            Entities.WithAll<Segment>()
                .WithDisposeOnCompletion(segmentsFromEntity)
                .ForEach((Entity e, in DynamicBuffer<CopySegmentToTarget> targets) =>
                {
                    var segment = segmentsFromEntity[e];
                    for (int i = 0; i < targets.Length; i++)
                    {
                        var target = targets[i].Target;
                        if (segmentsFromEntity.HasComponent(target))
                            segmentsFromEntity[target] = segment;
                    }
                }).Schedule();
        }
    }
}