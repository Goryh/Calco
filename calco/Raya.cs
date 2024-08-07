using System;
using System.Diagnostics;
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
		public readonly float3a ClosestPoint(in float3a pt)
		{
			float l = dot(pt - origin, dir) / dot(dir, dir);
			return GetPoint(l);
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
		public readonly float3a ClosestPointUnitRay(in float3a pt)
		{
			CheckRayIsNormalized();

			float l = dot(pt - origin, dir);
			return GetPoint(l);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float ClosestPointUnitRayDistSq(in float3a pt)
		{
			var diff = ClosestPointUnitRay(pt) - pt;
			return dot(diff, diff);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float ClosestPointUnitRayDist(in float3a pt) => sqrt(ClosestPointUnitRayDistSq(pt));

		public readonly bool ClosestPoint(in Ray3da other, out float thisRayT, out float otherRayT)
		{
			var ray1Dir = dir;
			var ray2Dir = other.dir;
			var ray1Origin = origin;
			var ray2Origin = other.origin;

			float a = dot(ray1Dir, ray1Dir);
			float b = dot(ray1Dir, ray2Dir);
			float e = dot(ray2Dir, ray2Dir);
			float d = a*e - b*b;

			if( d != 0.0f ) // lines are not parallel
			{
				var r = ray1Origin - ray2Origin;
				float c = dot(ray1Dir, r);
				float f = dot(ray2Dir, r);

				thisRayT = (b*f - c*e) / d;
				otherRayT = (a*f - c*b) / d;
				return true;
			}
			else
			{
				thisRayT = otherRayT = 0.0f;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("Ray({0}, {1})", float3(origin), float3(dir));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool IsNormalized()
		{
			return false;
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