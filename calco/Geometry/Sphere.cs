using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	[DebuggerDisplay("{position}, {radius}")]
	[Serializable]
	[Il2CppEagerStaticClassConstruction]
	public partial struct Sphere : IEquatable<Sphere>
	{
		public float4 posRadius;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Sphere(in float3a pos, float radius)
		{
			posRadius = new float4(pos, radius);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Sphere(in float4 positionAndRadius)
		{
			posRadius = positionAndRadius;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Sphere(in float4 positionAndRadius)
		{
			return new Sphere(positionAndRadius);
		}

		public float3a position
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => posRadius.xyza;
			[MethodImpl(MethodImplOptions.AggressiveInlining)] set => posRadius.xyz = value;
		}

		public float radius
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => posRadius.w;
			[MethodImpl(MethodImplOptions.AggressiveInlining)] set => posRadius.w = value;
		}

		public readonly float radiussq
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)] get => square(posRadius.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Translate(float3a translation)
		{
			position += translation;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float3a ClosestPoint(float3a point) => normalize(point - position) * radius;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float ClosestPointDist(float3a point) => abs(distance(position, point) - radius);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Contains(float3a point) => distancesq(point, position) <= radiussq;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Raycast(in Ray3a unitRay, out float hitDistMin, out float hitDistMax)
		{
			unitRay.CheckRayIsNormalized();

			var center = position;
			var l = center - unitRay.origin;
			float oToD = dot(l, unitRay.dir);
			float cToD2 = dot(l, l) - oToD * oToD;

			var radiusSquared = radiussq;
			if (cToD2 > radiusSquared)
			{
				hitDistMin = hitDistMax = 0;
				return false;
			}

			float dToHit = sqrt(radiusSquared - cToD2);
			hitDistMin = oToD - dToHit;
			hitDistMax = oToD + dToHit;

			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in Sphere other)
		{
			return distancesq(position, other.position) <= square(radius + other.radius);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in Ray3a unitRay)
		{
			unitRay.CheckRayIsNormalized();

			var center = position;
			var closestPointOnRay = unitRay.dir * max(0, dot(center - unitRay.origin, unitRay.dir));
			var centerToRay = unitRay.origin + closestPointOnRay - center;
			return lengthsq(centerToRay) <= radiussq;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in Plane3 plane)
		{
			float dist = plane.SignedDistanceToPoint(position);
			return abs(dist) <= radius;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in Segmenta segment)
		{
			var dir = normalize(segment.dir);
			return	Intersects(Ray3a(segment.p0, dir))
				&&  Intersects(Ray3a(segment.p1, -dir));
		}

		public readonly bool Intersects(in Trianglea triangle)
		{
			var trianglePlane = triangle.plane;

			if( !Intersects(trianglePlane) )
				return false;

			if( triangle.Contains(trianglePlane.ClosestPoint(position)) )
				return true;

			if( Intersects(Segmenta(triangle.v0, triangle.v1)) )
				return true;
			if( Intersects(Segmenta(triangle.v1, triangle.v2)) )
				return true;
			if( Intersects(Segmenta(triangle.v2, triangle.v0)) )
				return true;

			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(Sphere other)
		{
			return posRadius.Equals(other.posRadius);
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Sphere Sphere(in float3a pos, float radius) => new Sphere(pos, radius);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Sphere transform(in RigidTransforma transf, in Sphere sphere)
		{
			var pos = transform(in transf, sphere.position);
			return new Sphere(pos, sphere.radius);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Sphere transformWithUniformScale(in float4x4 transf, in Sphere sphere)
		{
			var pos = transform(in transf, sphere.position);
			return new Sphere(pos, sphere.radius * transf.UniformScale());
		}
	}
}
