using UnityEngine;
using Unity.Entities;
using Unity.Collections;

namespace alexnown.SegmentedTween
{
    [ExecuteAlways]
    public class TransformationSegmenter : MonoBehaviour, IConvertGameObjectToEntity
    {
        public Transform[] Targets;
        public bool SavePositions = true;
        public bool SaveRotations;
        public bool SaveScales;
        [Header("Segmenting parameters")]
        public bool AutoUpdateSegmentDistances = true;
        public float TotalDistance;
        public float[] Distances;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            if (Targets.Length < 2) throw new System.InvalidOperationException("Segmenting possible only with 2 or more targets");
            if (SavePositions)
                dstManager.AddComponentData(entity, new SegmentedTranslations { Reference = SegmentPositions(Targets) });
            if (SaveRotations)
                dstManager.AddComponentData(entity, new SegmentedRotations { Reference = SegmentRotations(Targets) });
            if (SaveScales)
                dstManager.AddComponentData(entity, new SegmentedNonUniformScales { Reference = SegmentScales(Targets) });
            dstManager.AddComponentData(entity, new SegmentingBounds { Reference = BlobAllocatorHelper.AllocateFloatArray(Distances) });
            dstManager.AddComponentData(entity, new SegmentingParameters { SegmentsCount = Distances.Length, TotalLength = TotalDistance });
            dstManager.AddComponentData(entity, new CachedSegmentBounds { End = Distances[0] });
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (!AutoUpdateSegmentDistances || Targets.Length < 2) return;
            if (Distances.Length != Targets.Length - 1) Distances = new float[Targets.Length - 1];
            TotalDistance = 0;
            for (int i = 0; i < Targets.Length - 1; i++)
            {
                var target = Targets[i];
                var nextTarget = Targets[i + 1];
                if (nextTarget == null || target == null)
                {
                    TotalDistance = 0;
                    return;
                }
                TotalDistance += Vector3.Distance(target.localPosition, nextTarget.localPosition);
                Distances[i] = TotalDistance;
            }
        }
#endif

        private BlobAssetReference<Float3Array> SegmentPositions(Transform[] targets)
        {
            using (var builder = new BlobBuilder(Allocator.Temp))
            {
                ref var root = ref builder.ConstructRoot<Float3Array>();
                var array = builder.Allocate(ref root.Value, targets.Length);
                for (int i = 0; i < targets.Length; i++) array[i] = targets[i].transform.localPosition;
                return builder.CreateBlobAssetReference<Float3Array>(Allocator.Persistent);
            }
        }

        private BlobAssetReference<QuaternionArray> SegmentRotations(Transform[] targets)
        {
            using (var builder = new BlobBuilder(Allocator.Temp))
            {
                ref var root = ref builder.ConstructRoot<QuaternionArray>();
                var array = builder.Allocate(ref root.Value, targets.Length);
                for (int i = 0; i < targets.Length; i++) array[i] = targets[i].transform.localRotation;
                return builder.CreateBlobAssetReference<QuaternionArray>(Allocator.Persistent);
            }
        }

        private BlobAssetReference<Float3Array> SegmentScales(Transform[] targets)
        {
            using (var builder = new BlobBuilder(Allocator.Temp))
            {
                ref var root = ref builder.ConstructRoot<Float3Array>();
                var array = builder.Allocate(ref root.Value, targets.Length);
                for (int i = 0; i < targets.Length; i++) array[i] = targets[i].transform.localScale;
                return builder.CreateBlobAssetReference<Float3Array>(Allocator.Persistent);
            }
        }
    }
}