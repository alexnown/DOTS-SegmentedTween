using Unity.Entities;

namespace beerserk.SegmentedTween
{
    public struct SegmentedNonUniformScales : IComponentData
    {
        public BlobAssetReference<Float3Array> Reference;
    }
}