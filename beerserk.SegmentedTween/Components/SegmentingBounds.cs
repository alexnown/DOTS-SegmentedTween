using Unity.Entities;

namespace beerserk.SegmentedTween
{
    public struct SegmentingBounds : IComponentData
    {
        public BlobAssetReference<FloatArray> Reference;
    }
}