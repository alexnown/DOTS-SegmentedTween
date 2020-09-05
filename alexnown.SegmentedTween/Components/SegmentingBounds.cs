using Unity.Entities;

namespace alexnown.SegmentedTween
{
    public struct SegmentingBounds : IComponentData
    {
        public BlobAssetReference<FloatArray> Reference;
    }
}