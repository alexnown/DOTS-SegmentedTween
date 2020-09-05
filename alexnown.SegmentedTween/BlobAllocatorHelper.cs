using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;

namespace alexnown.SegmentedTween
{
    public struct FloatArray
    {
        public BlobArray<float> Value;
    }
    public struct Float2Array
    {
        public BlobArray<float2> Value;
    }
    public struct Float3Array
    {
        public BlobArray<float3> Value;
    }
    public struct QuaternionArray
    {
        public BlobArray<quaternion> Value;
    }

    public static class BlobAllocatorHelper
    {
        public static BlobAssetReference<FloatArray> AllocateFloatArray(params float[] values)
        {
            using (var builder = new BlobBuilder(Allocator.Temp))
            {
                ref var root = ref builder.ConstructRoot<FloatArray>();
                var array = builder.Allocate(ref root.Value, values.Length);
                for (int i = 0; i < values.Length; i++) array[i] = values[i];
                return builder.CreateBlobAssetReference<FloatArray>(Allocator.Persistent);
            }
        }
        public static BlobAssetReference<Float2Array> AllocateFloat2Array(params float2[] values)
        {
            using (var builder = new BlobBuilder(Allocator.Temp))
            {
                ref var root = ref builder.ConstructRoot<Float2Array>();
                var array = builder.Allocate(ref root.Value, values.Length);
                for (int i = 0; i < values.Length; i++) array[i] = values[i];
                return builder.CreateBlobAssetReference<Float2Array>(Allocator.Persistent);
            }
        }
        public static BlobAssetReference<Float3Array> AllocateFloat3Array(params UnityEngine.Vector3[] values)
        {
            using (var builder = new BlobBuilder(Allocator.Temp))
            {
                ref var root = ref builder.ConstructRoot<Float3Array>();
                var array = builder.Allocate(ref root.Value, values.Length);
                for (int i = 0; i < values.Length; i++) array[i] = values[i];
                return builder.CreateBlobAssetReference<Float3Array>(Allocator.Persistent);
            }
        }
        public static BlobAssetReference<Float3Array> AllocateFloat3Array(params float3[] values)
        {
            using (var builder = new BlobBuilder(Allocator.Temp))
            {
                ref var root = ref builder.ConstructRoot<Float3Array>();
                var array = builder.Allocate(ref root.Value, values.Length);
                for (int i = 0; i < values.Length; i++) array[i] = values[i];
                return builder.CreateBlobAssetReference<Float3Array>(Allocator.Persistent);
            }
        }
        public static BlobAssetReference<QuaternionArray> AllocateQuaternionArray(params quaternion[] values)
        {
            using (var builder = new BlobBuilder(Allocator.Temp))
            {
                ref var root = ref builder.ConstructRoot<QuaternionArray>();
                var array = builder.Allocate(ref root.Value, values.Length);
                for (int i = 0; i < values.Length; i++) array[i] = values[i];
                return builder.CreateBlobAssetReference<QuaternionArray>(Allocator.Persistent);
            }
        }
    }
}