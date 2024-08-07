using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	[Il2CppEagerStaticClassConstruction]
	public ref struct LineSegmenta
	{
		public float3a p0;
		public float3a p1;

		public readonly float3a dir	{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => p1 - p0; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LineSegmenta(in float3a p0, in float3a p1)
		{
			this.p0 = p0;
			this.p1 = p1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator LineSegment(in LineSegmenta v)
		{
			return new LineSegment(v.p0, v.p1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator LineSegmenta(in LineSegment v)
		{
			return new LineSegmenta(v.p0, v.p1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float3a ClosestPoint(in float3a pt)
		{
			var d = dir;
			var l = dot(pt - p0, d) / dot(d, d);
			l = saturate(l);
			return p0 + l * d;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float ClosestPointDistSq(in float3a pt)
		{
			var diff = ClosestPoint(pt) - pt;
			return dot(diff, diff);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float ClosestPointDist(in float3a pt) => sqrt(ClosestPointDistSq(pt));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("LineSegment({0}, {1})", float3(p0), float3(p1));
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static LineSegmenta LineSegmenta(in float3 p0, in float3 p1) { return new LineSegmenta(p0, p1); }
	}
}
