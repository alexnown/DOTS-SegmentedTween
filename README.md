# Segmented Tween

Custom unity DOTS package implements tweening depends on stored segments. Useful for moving along waypoints, sampling and playing back animation clips, tweening custom values, etc.

# Features

* Segmenting root transformations by animation clip.
* Segmenting tweening between waypoints.
* Supports uniform and non uniform segments tweening.
* Multithreaded and optimized (caches the bounds of the current segment).
* You can copy Segment value to some targets with CopySegmentToTarget buffer. It allows you to have one active tweening for many targets with Segment component.

# How it works

For tweening along segments entity required base components:
* Segment: actual segment index and the segment passed ratio
* SegmentingParameters: segments count and total length.
* Disposition: actual position regarding all length. Disposition = 5 with TotalLength=10 means that actual position exactly in the middle of all path.
* SegmentedTweening: contains tweening speed and tweening mode (Clamped, Yoyo or Incremental)
* IsUniformSegments or SegmentedBounds&CachedSegmentBounds components, depends on segmenting type.
* SegmentedTranslations / SegmentedTranslations2D / SegmentedRotations / SegmentedScales / SegmentedNonUniformScales / Any custom segmented values, which will apply to some entity values.


# Uniform segments

Uniform segments are used for the same intervals (useful for sampled animations or pathes). For tweening along uniform segments required: 
* IsUniformSegments component, that contains single segment length

# Non uniform segments

If segments have different length, you need add some additional components:
* SegmentedBounds: contains an array with a distance to the end of the segment. If five-segment path has length = 10, the bounds array may be [1, 2, 5, 7, 10].
* CachedSegmentBounds: cached start and end distances for active segment. For first segment it will be [0, 1], for last - [7, 10] in this example.


