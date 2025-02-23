using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	[Il2CppEagerStaticClassConstruction]
	public struct Segment2
	{
		public float2 p0;
		public float2 p1;

		public readonly float2 dir	{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => p1 - p0; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Segment2(in float2 p0, in float2 p1)
		{
			this.p0 = p0;
			this.p1 = p1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float2 GetPoint(float segmentT)
		{
			return p0 + dir * segmentT;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float2 ClosestPoint(in float2 pt)
		{
			var d = dir;
			var l = dot(pt - p0, d) / dot(d, d);
			l = saturate(l);
			return p0 + l * d;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float ClosestPointDistSq(in float2 pt)
		{
			var diff = ClosestPoint(pt) - pt;
			return dot(diff, diff);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float ClosestPointDist(in float2 pt) => sqrt(ClosestPointDistSq(pt));

		public readonly bool Intersects(in Segment2 other)
		{
			var a1 = cross(p0 - other.p1, p1 - other.p1);
			var a2 = cross(p0 - other.p0, p1 - other.p0);
			if( a1 * a2 <= 0.0f )
			{
				var a3 = cross(other.p0 - p0, other.p1 - p0);
				var a4 = a3 + a2 - a1;
				if( a3 * a4 < 0.0f )
					return true;
				else if( a3 * a4 == 0.0f ) // lines are parallel or intersect in a point
				{
					float s1s2 = dot(p1 - p0, other.p1 - other.p0);
					if( s1s2 > 0.0f ) // co-directional
						return dot(other.p0 - p1, p0 - other.p1) >= 0.0f;
					else // opposite directional
						return dot(other.p1 - p1, p0 - other.p0) >= 0.0f;
				}
			}

			return false;
		}

		public readonly bool Intersects(in Segment2 other, out float2 intersection)
		{
			var a1 = cross(p0 - other.p1, p1 - other.p1);
			var a2 = cross(p0 - other.p0, p1 - other.p0);
			if (a1 * a2 <= 0.0f)
			{
				var a3 = cross(other.p0 - p0, other.p1 - p0);
				var a4 = a3 + a2 - a1;
				if (a3 * a4 < 0.0f)
				{
					float t = a3 / (a3 - a4);
					intersection = p0 + t * (p1 - p0);
					return true;
				}
				else if( a3 * a4 == 0.0f ) // lines are parallel or intersect in a point
				{
					float s1s2 = dot(p1 - p0, other.p1 - other.p0);
					if( s1s2 > 0.0f ) // co-directional
					{
						var test = dot(other.p0 - p1, p0 - other.p1);
						if( test == 0.0f ) // touch of segment points
						{
							if( lengthsq(other.p0 - p1) < 1e-3f )
								intersection = p1;
							else
								intersection = p0;
							return true;
						}
						else if( test > 0.0f ) // intersection of parallel segments
						{
							intersection = (p0 + p1 + other.p0 + other.p1) / 4.0f;
							return true;
						}
						// else - no intersection of parallel segments
					}
					else // opposite directional
					{
						var test = dot(other.p1 - p1, p0 - other.p0);
						if( test == 0.0f ) // touch of segment points
						{
							if( lengthsq(other.p1 - p1) < 1e-3f )
								intersection = p1;
							else
								intersection = p0;
							return true;
						}
						else if( test > 0.0f ) // intersection of parallel segments
						{
							intersection = (p0 + p1 + other.p0 + other.p1) / 4.0f;
							return true;
						}
						// else - no intersection of parallel segments
					}
				}
			}

			intersection = float2c.zero;
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float IntersectT(in Ray2 line)
		{
			var a1 = cross(line.dir, line.origin - p0);
			var a2 = cross(line.dir, dir);
			return a1 / a2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in Ray2 line)
		{
			float t = IntersectT(line);
			return t >= 0.0f && t <= 1.0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Intersects(in Ray2 line, out float2 intersection)
		{
			float t = IntersectT(line);
			if( t >= 0.0f && t <= 1.0f )
			{
				intersection = GetPoint(t);
				return true;
			}

			intersection = float2c.zero;
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("Segment2({0}, {1})", p0, p1);
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Segment2 Segment2(in float2 p0, in float2 p1) { return new Segment2(p0, p1); }
	}
}
