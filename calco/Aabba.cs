using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	[Il2CppEagerStaticClassConstruction]
	public ref struct Aabba
	{
		public float3a min;
		public float3a max;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Aabba(in Aabba bound)
		{
			this.min = bound.min;
			this.max = bound.max;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Aabba(float3a min, float3a max)
		{
			this.min = min;
			this.max = max;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Aabb(in Aabba v)
		{
			return new Aabb(v.min, v.max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Aabba(in Aabb v)
		{
			return new Aabba(v.min, v.max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Aabba(in UnityEngine.Bounds b)
		{
			return new Aabba(b.min, b.max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator UnityEngine.Bounds(in Aabba b)
		{
			return new UnityEngine.Bounds(b.center, b.extents);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Aabba CreateInvalid()
		{
			return new Aabba(float3a(float.MaxValue), float3a(float.MinValue));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Aabba CreateHuge()
		{
			return new Aabba(float3a(-1e+38f), float3a(1e+38f)); // don't use float.MaxValue/float.MinValue here as some subsystems might not like that special cases
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Aabba CreateFromCenterAndHalfExtents(in float3a center, in float3a halfExtents)
		{
			return new Aabba(center - halfExtents, center + halfExtents);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Aabba CreateFromCenterAndExtents(in float3a center, in float3a extents)
		{
			return CreateFromCenterAndHalfExtents(center, extents * 0.5f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Aabba CreateFromMinMax(float3a min, float3a max)
		{
			return new Aabba(min, max);
		}

		public float3a extents		{	[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => max - min;
										[MethodImpl(MethodImplOptions.AggressiveInlining)] set {SetCenterAndHalfExtents(center, value / 2);} }
		public float3a halfExtents	{	[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => (max - min) * 0.5f;
										[MethodImpl(MethodImplOptions.AggressiveInlining)] set {SetCenterAndHalfExtents(center, value);}  }
		public float3a center		{	[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => (max + min) * 0.5f;
										[MethodImpl(MethodImplOptions.AggressiveInlining)] set {SetCenterAndHalfExtents(value, halfExtents);} }
		public readonly bool isValid {	[MethodImpl(MethodImplOptions.AggressiveInlining)] get => all(min <= max); }

		public readonly float4 inscribedSphere		{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(center, cmin(halfExtents)); }
		public readonly float4 inscribedSphereMax	{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(center, cmax(halfExtents)); }
		public readonly float4 inscribedSphereAver	{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(center, (cmin(halfExtents) + cmax(halfExtents)) / 2); }
		public readonly float4 circumscribedSphere	{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => float4(center, length(halfExtents)); }

		/// Computes the surface area for this axis aligned bounding box.
		public readonly float surfaceArea
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)] 
			get
			{
				var diff = max - min;
				return 2 * dot(diff, diff.yzx);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCenterAndHalfExtents(float3a center, float3a halfExtents)
		{
			min = center - halfExtents;
			max = center + halfExtents;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Contains(float3a point) => all(point >= min & point <= max);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Contains(Aabba aabb) => all((min <= aabb.min) & (max >= aabb.max));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Overlaps(Aabba aabb) => all(max >= aabb.min & min <= aabb.max);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Expand(float signedDistance)
		{
			min -= signedDistance;
			max += signedDistance;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(Aabba aabb)
		{
			min = min(min, aabb.min);
			max = max(max, aabb.max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(float3a point)
		{
			min = min(min, point);
			max = max(max, point);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(float factor)
		{
			var c = center;
			min = (min - c) * factor + c;
			max = (max - c) * factor + c;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(float3a factor)
		{
			var c = center;
			min = (min - c) * factor + c;
			max = (max - c) * factor + c;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(float3a scaleOrigin, float factor)
		{
			min = (min - scaleOrigin) * factor + scaleOrigin;
			max = (max - scaleOrigin) * factor + scaleOrigin;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(float3a scaleOrigin, float3a factor)
		{
			min = (min - scaleOrigin) * factor + scaleOrigin;
			max = (max - scaleOrigin) * factor + scaleOrigin;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly float3a ClosestPoint(float3a point)
		{
			// For each coordinate axis, if the point coordinate value is
			// outside box, clamp it to the box, else keep it as is
			return clamp(point, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly float ClosestPointDistSq(float3a point)
		{
			return lengthsq(ClosestPoint(point) - point);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly float ClosestPointDist(float3a point) => square(ClosestPointDist(point));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in Sphere sphere)
		{
			return ClosestPointDistSq(sphere.positiona) <= sphere.radiussq;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]

		public readonly bool Raycast(in Ray3da r, out float hitDistMin, out float hitDistMax)
		{
			var t1 = (min - r.origin) / r.dir;
			var t2 = (max - r.origin) / r.dir;

			hitDistMin = cmax(min(t1, t2));
			hitDistMax = cmin(max(t1, t2));

			return hitDistMax > hitDistMin;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in Ray3da r)
		{
			var t1 = (min - r.origin) / r.dir;
			var t2 = (max - r.origin) / r.dir;

			float tmin = cmax(min(t1, t2));
			float tmax = cmin(max(t1, t2));

			tmin = max(tmin, 0.0f); // only positive side of the ray

			return tmax > tmin;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in LineSegmenta segment, out float hitDistMin, out float hitDistMax)
		{
			var invRayDir = float3c.one / segment.dir;

			var firstPlaneIntersections = (min - segment.p0) * invRayDir;
			var secondPlaneIntersections = (max - segment.p0) * invRayDir;
			var closestPlaneIntersections = min(firstPlaneIntersections, secondPlaneIntersections);
			var furthestPlaneIntersections = max(firstPlaneIntersections, secondPlaneIntersections);

			hitDistMin = saturate(cmax(closestPlaneIntersections));
			hitDistMax = saturate(cmin(furthestPlaneIntersections));

			return hitDistMin < hitDistMax;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in LineSegmenta segment)
		{
			return Intersects(segment, out var _, out var _);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in Plane3d plane)
		{
			var projSize = dot(halfExtents, abs(plane.normala));
			var dist = plane.SignedDistanceToPoint(center);
			return abs(dist) <= projSize;
		}

		public readonly bool Intersects(in Trianglea triangle)
		{
			var trianglePlane = triangle.plane;

			if( !Intersects(trianglePlane) )
				return false;

			if( triangle.Contains(trianglePlane.ClosestPoint(center)) )
				return true;

			if( Intersects(LineSegmenta(triangle.v0, triangle.v1)) )
				return true;
			if( Intersects(LineSegmenta(triangle.v1, triangle.v2)) )
				return true;
			if( Intersects(LineSegmenta(triangle.v2, triangle.v0)) )
				return true;

			return false;
		}

		// gives some false-positives
		public readonly bool IsOutsideFrustum(Plane3d[] frustumPlanes5)
		{
			for( int i = 0; i < 5; ++i )
			{
				var projSize = dot(halfExtents, abs(frustumPlanes5[i].normala));
				var dist = frustumPlanes5[i].SignedDistanceToPoint(center);
				if( dist <= -projSize )
					return true;
			}

			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("Aabb({0}, {1})", float3(min), float3(max));
		}
	}

	public static partial class math
	{
		public static Aabba transform(in RigidTransforma transf, in Aabba aabb)
		{
			float3 halfExtentsInA = aabb.halfExtents;

			// Rotate each axis individually and find their new positions in the rotated space.
			float3a x = rotate(transf.rot, new float3a(halfExtentsInA.x, 0, 0));
			float3a y = rotate(transf.rot, new float3a(0, halfExtentsInA.y, 0));
			float3a z = rotate(transf.rot, new float3a(0, 0, halfExtentsInA.z));

			// Find the new max corner by summing the rotated axes.  Absolute value of each axis
			// since we are trying to find the max corner.
			float3a halfExtentsInB = abs(x) + abs(y) + abs(z);
			float3a centerInB = transform(transf, aabb.center);

			return Aabba.CreateFromCenterAndHalfExtents(centerInB, halfExtentsInB);
		}

		public static Aabba transform(in float4x4 transf, in Aabba aabb)
		{
			var center = transform(transf, aabb.center);
			var halfExt = aabb.halfExtents;
			halfExt =	halfExt.x * abs(transf.c0.xyza) + 
						halfExt.y * abs(transf.c1.xyza) + 
						halfExt.z * abs(transf.c2.xyza);

			return Aabba.CreateFromCenterAndHalfExtents(center, halfExt);
		}

		public static Aabba transform(in float3ax3 transf, in Aabba aabb)
		{
			var halfExt = aabb.halfExtents;
			halfExt =	halfExt.x * abs(transf.c0) + 
						halfExt.y * abs(transf.c1) + 
						halfExt.z * abs(transf.c2);

			return Aabba.CreateFromCenterAndHalfExtents(aabb.center, halfExt);
		}
	}
}
