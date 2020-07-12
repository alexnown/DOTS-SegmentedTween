using Unity.Entities;
using Unity.Transforms;

namespace beerserk.SegmentedTween
{
    [WriteGroup(typeof(Translation))]
    public struct SegmentedTranslations2D : IComponentData
    {
        public bool AxisXZ;
        public BlobAssetReference<Float2Array> Reference;
    }
}