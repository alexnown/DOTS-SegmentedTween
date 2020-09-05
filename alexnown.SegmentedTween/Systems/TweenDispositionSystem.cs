using Unity.Entities;
using Unity.Mathematics;

namespace alexnown.SegmentedTween
{
    [UpdateInGroup(typeof(SegmentedTweeningSystemGroup))]
    [UpdateBefore(typeof(UpdateSegmentSystem))]
    public class TweenDispositionSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;
            Entities.ForEach((ref Disposition disposition, ref SegmentedTweening tweening, in SegmentingParameters parameters) =>
            {
                var progress = disposition.Value + tweening.Speed * dt;
                switch (tweening.Type)
                {
                    case TweeningType.Incremental:
                        progress = math.clamp(progress - math.floor(progress / parameters.TotalLength) * parameters.TotalLength, 0f, parameters.TotalLength);
                        break;
                    case TweeningType.Yoyo:
                        if (progress < 0)
                        {
                            tweening.Speed = math.abs(tweening.Speed);
                            progress = math.abs(progress);
                        }
                        else if (progress > parameters.TotalLength)
                        {
                            tweening.Speed = -math.abs(tweening.Speed);
                            progress = 2 * parameters.TotalLength - progress;
                        }
                        break;
                    default:
                        progress = math.clamp(progress, 0, parameters.TotalLength);
                        break;
                }
                disposition.Value = progress;
            }).ScheduleParallel();
        }
    }
}