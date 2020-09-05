using Unity.Entities;

namespace alexnown.SegmentedTween
{
    public struct SegmentedNonUniformScales : IComponentData
    {
        public BlobAssetReference<Float3Array> Reference;
    }
}