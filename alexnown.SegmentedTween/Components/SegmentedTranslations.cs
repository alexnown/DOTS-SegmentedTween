using Unity.Entities;
using Unity.Transforms;

namespace alexnown.SegmentedTween
{
    [WriteGroup(typeof(Translation))]
    public struct SegmentedTranslations : IComponentData
    {
        public BlobAssetReference<Float3Array> Reference;
    }
}