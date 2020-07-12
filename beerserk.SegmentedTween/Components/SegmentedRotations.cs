using Unity.Entities;

namespace beerserk.SegmentedTween
{
    public struct SegmentedRotations : IComponentData
    {
        public BlobAssetReference<QuaternionArray> Reference;
    }
}