using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	[Il2CppEagerStaticClassConstruction]
	public struct Ray2
	{
		public float2 origin;
		public float2 dir;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Ray2(in float2 origin, in float2 dir)
		{
			this.origin = origin;
			this.dir = dir;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float2 GetPoint(float distance)
		{
			return origin + dir * distance;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float2 ClosestPoint(in float2 pt)
		{
			CheckRayIsNormalized();

			float l = dot(pt - origin, dir);
			return GetPoint(l);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float ClosestPointDist(in float2 pt)
		{
			CheckRayIsNormalized();

			var perpDir = float2(dir.y, -dir.x);
			return abs(dot(pt - origin, perpDir));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool IsClockwise(in float2 pt)
		{
			var perpDir = float2(dir.y, -dir.x);
			return dot(pt - origin, perpDir) >= 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("Ray2({0}, {1})", origin, dir);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool IsNormalized()
		{
			float ll = lengthsq(dir);
			const float lowerBound = 0.999f * 0.999f;
			const float upperBound = 1.001f * 1.001f;

			return ll >= lowerBound && ll <= upperBound;
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		public readonly void CheckRayIsNormalized()
		{
			if( !IsNormalized() )
				throw new System.ArgumentException("The ray must be normalized. Call normalize(Ray) for normalization.");
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Ray2 Ray2(in float2 origin, in float2 dir) => new Ray2(origin, dir);
	}
}