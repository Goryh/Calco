using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	[Il2CppEagerStaticClassConstruction]
	public ref struct Trianglea
	{
		public float3a v0;
		public float3a v1;
		public float3a v2;

		public readonly Plane3d plane { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Plane3d.CreateFrom3Points(v0, v1, v2); }
		public readonly Plane3d nonUnitPlane { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Plane3d.CreateNonUnitFrom3Points(v0, v1, v2); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Trianglea(in float3a v0, in float3a v1, in float3a v2)
		{
			this.v0 = v0;
			this.v1 = v1;
			this.v2 = v2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Triangle(in Trianglea t)
		{
			return new Triangle(t.v0, t.v1, t.v2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Trianglea(in Triangle t)
		{
			return new Trianglea(t.v0, t.v1, t.v2);
		}

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public readonly bool Contains(in float3a pt)
	    {
		    var d0 = cross(pt - v0, v1 - v0);
		    var d1 = cross(pt - v1, v2 - v1);
		    var d2 = cross(pt - v2, v0 - v2);

		    return dot(d0, d1) > 0 && dot(d1, d2) > 0;
	    }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Raycast(in Ray3da ray, out float hitDist)
		{
			return Raycast(ray, nonUnitPlane, out hitDist);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Raycast(in Ray3da ray, in Plane3d precalculatedTriangleNonUnitPlane, out float hitDist)
		{
			var n = precalculatedTriangleNonUnitPlane.normala;
			var d = precalculatedTriangleNonUnitPlane.distance;

			float nDotRayDirection = dot(n, ray.dir);
			if( abs(nDotRayDirection) < EPSILON * 4 )
			{
				hitDist = 0;
				return false; // they are parallel, so they don't intersect
			}

			hitDist = -(dot(n, ray.origin) + d) / nDotRayDirection;

			// commented out to return both positive and negative side intersections
			// if( t < 0 )
			//	return false;

			var p = ray.GetPoint(hitDist);
			return Contains(p);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static float SignedTetraVolume(in float3a a, in float3a b, in float3a c, in float3a d)
		{
			return dot(cross(b - a, c - a), d - a);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in LineSegmenta segment)
		{
			float s1 = SignedTetraVolume(segment.p0, v0, v1, v2);
			float s2 = SignedTetraVolume(segment.p1, v0, v1, v2);

			if( s1 * s2 < 0 )
			{
				float s3 = SignedTetraVolume(segment.p0, segment.p1, v0, v1);
				float s4 = SignedTetraVolume(segment.p0, segment.p1, v1, v2);
				float s5 = SignedTetraVolume(segment.p0, segment.p1, v2, v0);
				if( s3 * s4 > 0 && s4 * s5 > 0 )
					return true;
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("Triangle({0}, {1}, {1})", float3(v0), float3(v1), float3(v2));
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Trianglea Trianglea(in float3 v0, in float3 v1, in float3 v2) { return new Trianglea(v0, v1, v2); }
	}
}
