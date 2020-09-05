using Unity.Entities;

namespace alexnown.SegmentedTween
{
    public struct SegmentedRotations : IComponentData
    {
        public BlobAssetReference<QuaternionArray> Reference;
    }
}