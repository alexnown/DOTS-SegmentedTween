using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace alexnown.SegmentedTween
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public class TweeningTransformsSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var translationJobHandler = Entities.WithName("tween_positions")
                .WithEntityQueryOptions(EntityQueryOptions.FilterWriteGroup)
                .ForEach((ref Translation position, in Segment segment, in SegmentedTranslations positions) =>
                {
                    var firstValue = positions.Reference.Value.Value[segment.Index];
                    var secondValue = positions.Reference.Value.Value[segment.Index + 1];
                    position.Value = math.lerp(firstValue, secondValue, segment.Ratio);
                }).ScheduleParallel(this.Dependency);

            translationJobHandler = Entities.WithName("tween_positions2d")
                .WithEntityQueryOptions(EntityQueryOptions.FilterWriteGroup)
                .ForEach((ref Translation position, in Segment segment, in SegmentedTranslations2D positions) =>
                {
                    var firstValue = positions.Reference.Value.Value[segment.Index];
                    var secondValue = positions.Reference.Value.Value[segment.Index + 1];
                    var newPosition = math.lerp(firstValue, secondValue, segment.Ratio);
                    position.Value.x = newPosition.x;
                    position.Value.y = math.select(position.Value.y, newPosition.y, !positions.AxisXZ);
                    position.Value.z = math.select(position.Value.z, newPosition.y, positions.AxisXZ);
                }).ScheduleParallel(translationJobHandler);

            var rotationJobHandler = Entities.WithName("tween_rotations")
                .ForEach((ref Rotation rotation, in Segment segment, in SegmentedRotations rotations) =>
                {
                    var firstValue = rotations.Reference.Value.Value[segment.Index];
                    var secondValue = rotations.Reference.Value.Value[segment.Index + 1];
                    rotation.Value = math.slerp(firstValue, secondValue, segment.Ratio);
                }).ScheduleParallel(this.Dependency);

            var scalesJobHandler = Entities.WithName("tween_scales")
                .ForEach((ref Scale scale, in Segment segment, in SegmentedScales scales) =>
                {
                    var firstValue = scales.Reference.Value.Value[segment.Index];
                    var secondValue = scales.Reference.Value.Value[segment.Index + 1];
                    scale.Value = math.lerp(firstValue, secondValue, segment.Ratio);
                }).ScheduleParallel(this.Dependency);

            var nonUniformScalesJobHandler = Entities.WithName("tween_non_uniform_scales")
                .ForEach((ref NonUniformScale scale, in Segment segment, in SegmentedNonUniformScales scales) =>
                {
                    var firstValue = scales.Reference.Value.Value[segment.Index];
                    var secondValue = scales.Reference.Value.Value[segment.Index + 1];
                    scale.Value = math.lerp(firstValue, secondValue, segment.Ratio);
                }).ScheduleParallel(this.Dependency);

            this.Dependency = JobHandle.CombineDependencies(translationJobHandler, rotationJobHandler);
            this.Dependency = JobHandle.CombineDependencies(this.Dependency, scalesJobHandler , nonUniformScalesJobHandler);
        }
    }
}