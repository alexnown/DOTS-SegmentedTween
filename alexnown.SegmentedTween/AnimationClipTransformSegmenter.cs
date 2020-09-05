using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace alexnown.SegmentedTween
{
    public class AnimationClipTransformSegmenter : MonoBehaviour, IConvertGameObjectToEntity
    {
        public int Fps = 60;
        public bool SaveTranslates = true;
        public bool SaveRotations;
        public bool SaveScales;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
#if UNITY_EDITOR
            if (Fps < 1) throw new System.InvalidOperationException($"Fps must be greater than 0");
            if (!SaveTranslates && !SaveRotations && !SaveScales)
                throw new System.InvalidOperationException($"{name} clip segmenter: no parameters to save");
            var clip = AnimationUtility.GetAnimationClips(gameObject)[0];
            var totalLength = clip.length;
            var sampleRate = 1f / Fps;
            var frameCount = Mathf.CeilToInt(totalLength / sampleRate) + 1;
            if (frameCount < 2)
                throw new System.InvalidOperationException($"Required 2 or more frames for segmenting! Frames={frameCount}, clip length={totalLength}s, fps={Fps}");

            var time = 0f;
            var translations = SaveTranslates ? new float3[frameCount] : default;
            var rotations = SaveRotations ? new quaternion[frameCount] : default;
            var scales = SaveScales ? new float3[frameCount] : default;
            for (int i = 0; i < frameCount; i++)
            {
                clip.SampleAnimation(gameObject, time);
                if (SaveTranslates) translations[i] = transform.localPosition;
                if (SaveRotations) rotations[i] = transform.localRotation;
                if (SaveScales) scales[i] = transform.localScale;
                time += sampleRate;
            }
            if (SaveTranslates) dstManager.AddComponentData(entity, new SegmentedTranslations
            {
                Reference = BlobAllocatorHelper.AllocateFloat3Array(translations)
            });
            if (SaveRotations) dstManager.AddComponentData(entity, new SegmentedRotations
            {
                Reference = BlobAllocatorHelper.AllocateQuaternionArray(rotations)
            });
            if (SaveScales) dstManager.AddComponentData(entity, new SegmentedNonUniformScales
            {
                Reference = BlobAllocatorHelper.AllocateFloat3Array(scales)
            });
            dstManager.AddComponentData(entity, new SegmentingParameters { SegmentsCount = frameCount - 1, TotalLength = totalLength });
            dstManager.AddComponentData(entity, new IsUniformSegments { SegmentLength = sampleRate });
#else 
            throw new System.InvalidOperationException("Animation clip segmenting works only in editor, use SubScenes for serialization");
#endif
        }
    }
}