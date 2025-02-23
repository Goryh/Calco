using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	[Serializable]
	[Il2CppEagerStaticClassConstruction]
	public struct LineSegment : IEquatable<LineSegment>
	{
		public float3 p0;
		public float3 p1;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LineSegment(in float3 p0, in float3 p1)
		{
			this.p0 = p0;
			this.p1 = p1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(LineSegment other)
		{
			return p0.Equals(other.p0) && p1.Equals(other.p1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("LineSegment({0}, {1})", p0, p1);
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static LineSegment LineSegment(in float3 p0, in float3 p1) { return new LineSegment(p0, p1); }
	}
}
