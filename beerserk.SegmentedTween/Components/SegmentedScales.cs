using Unity.Entities;

namespace beerserk.SegmentedTween
{
    public struct SegmentedScales : IComponentData
    {
        public BlobAssetReference<FloatArray> Reference;
    }
}