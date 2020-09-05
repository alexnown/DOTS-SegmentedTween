using Unity.Entities;

namespace alexnown.SegmentedTween
{
    public struct SegmentedScales : IComponentData
    {
        public BlobAssetReference<FloatArray> Reference;
    }
}