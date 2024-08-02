using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	[Il2CppEagerStaticClassConstruction]
	public ref struct Ray3da
	{
		public float3a origin;
		public float3a dir;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Ray3da(in float3a origin, in float3a dir)
		{
			this.origin = origin;
			this.dir = dir;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Ray3d(in Ray3da v)
		{
			return new Ray3d {origin = v.origin, dir = v.dir};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Ray3da(in Ray3d v)
		{
			return new Ray3da {origin = v.origin, dir = v.dir};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Ray3da(in UnityEngine.Ray r)
		{
			return new Ray3da(r.origin, r.direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator UnityEngine.Ray(in Ray3da r)
		{
			return new UnityEngine.Ray(r.origin, r.dir);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float3a GetPoint(float distance)
		{
			return origin + dir * distance;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float DistToPointSq(in float3a pt)
		{
			float l = dot(pt - origin, dir) / dot(dir, dir);
			var diff = origin + l * dir - pt;
			return dot(diff, diff);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float DistToPoint(in float3a pt) => sqrt(DistToPointSq(pt));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("Ray({0}, {1})", float3(origin), float3(dir));
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Ray3da Ray3da(in float3a origin, in float3a dir) => new Ray3da(origin, dir);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Ray3da transform(in RigidTransforma transf, in Ray3da r)
		{
			return new Ray3da {
				origin = transform(in transf, r.origin),
				dir = rotate(in transf, r.dir) };
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Ray3da transform(in float4x4 transf, in Ray3da r)
		{
			return new Ray3da {
				origin = transform(in transf, r.origin),
				dir = rotate(in transf, r.dir) };
		}
	}
}