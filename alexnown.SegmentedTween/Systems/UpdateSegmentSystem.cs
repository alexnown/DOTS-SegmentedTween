using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace alexnown.SegmentedTween
{
    [UpdateInGroup(typeof(SegmentedTweeningSystemGroup))]
    public class UpdateSegmentSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;
            Entities.WithName("uniform_segments")
                .WithEntityQueryOptions(EntityQueryOptions.FilterWriteGroup)
                .ForEach((ref Segment segment, in Disposition disposition, in IsUniformSegments uniform, in SegmentingParameters parameters) =>
                {
                    bool isLastPoint = disposition.Value >= parameters.TotalLength;
                    var ratio = disposition.Value / uniform.SegmentLength;
                    segment.Index = math.select((int)ratio, parameters.SegmentsCount - 1, isLastPoint);
                    segment.Ratio = math.select(ratio - segment.Index, 1, isLastPoint);

                }).ScheduleParallel();

            Entities.WithName("non_uniform_segments")
                .WithEntityQueryOptions(EntityQueryOptions.FilterWriteGroup)
                .ForEach((ref Segment segment, ref CachedSegmentBounds bounds, in Disposition disposition, in SegmentingBounds allBounds) =>
                {
                    if (bounds.Start == bounds.End || disposition.Value < bounds.Start || disposition.Value > bounds.End)
                    {
                        segment.Index = FindSegmentSafe(disposition.Value, segment.Index, ref allBounds.Reference.Value.Value, ref bounds);
                    }
                    if (bounds.Start == bounds.End) return;
                    segment.Ratio = math.unlerp(bounds.Start, bounds.End, disposition.Value);
                }).ScheduleParallel();
        }

        private static int FindSegmentSafe(float disposition, int segmentIndex, ref BlobArray<float> bounds, ref CachedSegmentBounds saveBounds)
        {
            if (disposition <= 0)
            {
                saveBounds.Start = 0;
                saveBounds.End = bounds[0];
                return 0;
            }
            int lastSegment = bounds.Length - 1;
            saveBounds.End = bounds[lastSegment];
            if (disposition >= saveBounds.End)
            {
                saveBounds.Start = bounds[lastSegment - 1];
                return lastSegment;
            }
            return FindSegmentRecursively(disposition, math.clamp(segmentIndex, 0, lastSegment), ref bounds, ref saveBounds);
        }

        private static int FindSegmentRecursively(float disposition, int segmentIndex, ref BlobArray<float> bounds, ref CachedSegmentBounds saveBounds)
        {
            saveBounds.End = bounds[segmentIndex];
            if (saveBounds.End < disposition) return FindSegmentRecursively(disposition, segmentIndex + 1, ref bounds, ref saveBounds);
            saveBounds.Start = segmentIndex > 0 ? bounds[segmentIndex - 1] : 0;
            if (saveBounds.Start > disposition) return FindSegmentRecursively(disposition, segmentIndex - 1, ref bounds, ref saveBounds);
            return segmentIndex;
        }
    }
}