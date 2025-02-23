using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	/// <summary>
	/// Axis aligned bounding box (AABB) stored in min and max form.
	/// </summary>
	/// <remarks>
	/// Axis aligned bounding boxes (AABB) are boxes where each side is parallel with one of the Cartesian coordinate axes
	/// X, Y, and Z. AABBs are useful for approximating the region an object (or collection of objects) occupies and quickly
	/// testing whether or not that object (or collection of objects) is relevant. Because they are axis aligned, they
	/// are very cheap to construct and perform overlap tests with them.
	/// </remarks>
	[Serializable]
	[Il2CppEagerStaticClassConstruction]
	public partial struct Aabb : IEquatable<Aabb>
	{
		public float3 min;
		public float3 max;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Aabb(in Aabb bound)
		{
			this.min = bound.min;
			this.max = bound.max;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Aabb(float3 min, float3 max)
		{
			this.min = min;
			this.max = max;
		}

		public float3 extents		{	[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => max - min;
										[MethodImpl(MethodImplOptions.AggressiveInlining)] set {SetCenterAndHalfExtents(center, value / 2);} }
		public float3 halfExtents	{	[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => (max - min) * 0.5f;
										[MethodImpl(MethodImplOptions.AggressiveInlining)] set {SetCenterAndHalfExtents(center, value);}  }
		public float3 center		{	[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => (max + min) * 0.5f;
										[MethodImpl(MethodImplOptions.AggressiveInlining)] set {SetCenterAndHalfExtents(value, halfExtents);} }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCenterAndHalfExtents(float3 center, float3 halfExtents)
		{
			min = center - halfExtents;
			max = center + halfExtents;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Contains(float3a point) => all(point >= float3a(min) & point <= float3a(max));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Contains(Aabba aabb) => all((float3a(min) <= aabb.min) & (float3a(max) >= aabb.max));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Overlaps(Aabba aabb) => all(float3a(max) >= aabb.min & float3a(min) <= aabb.max);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(Aabb other)
		{
			return min.Equals(other.min) && max.Equals(other.max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("Aabb({0}, {1})", min, max);
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Aabb Aabb(in Aabb xyz) { return new Aabb(xyz); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Bounds Bounds(in Aabb b) { return new UnityEngine.Bounds(Vec3(b.center), Vec3(b.extents)); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Aabb Aabb(in UnityEngine.Bounds b) { return new Aabb(float3(b.min), float3(b.max)); }
	}
}
