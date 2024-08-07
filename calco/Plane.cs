using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	/// <summary>
	/// A plane represented by a normal vector and a distance along the normal from the origin.
	/// </summary>
	/// <remarks>
	/// A plane splits the 3D space in half.  The normal vector points to the positive half and the other half is
	/// considered negative.
	/// </remarks>
	[DebuggerDisplay("{normal}, {distance}")]
	[Serializable]
	[Il2CppEagerStaticClassConstruction]
	public partial struct Plane3d : IEquatable<Plane3d>
	{
		/// <summary>
		/// A plane in the form Ax + By + Cz + Dw = 0.
		/// </summary>
		/// <remarks>
		/// Stores the plane coefficients A, B, C, D where (A, B, C) is a unit normal vector and D is the distance
		/// from the origin.  A plane stored with a unit normal vector is called a normalized plane.
		/// </remarks>
		public float4 normalAndDistance;

		/// <summary>
		/// Constructs a Plane from arbitrary coefficients A, B, C, D of the plane equation Ax + By + Cz + Dw = 0.
		/// </summary>
		/// <remarks>
		/// The constructed plane will be the normalized form of the plane specified by the given coefficients.
		/// </remarks>
		/// <param name="coefficientA">Coefficient A from plane equation.</param>
		/// <param name="coefficientB">Coefficient B from plane equation.</param>
		/// <param name="coefficientC">Coefficient C from plane equation.</param>
		/// <param name="coefficientD">Coefficient D from plane equation.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane3d(float coefficientA, float coefficientB, float coefficientC, float coefficientD)
		{
			normalAndDistance = new float4(coefficientA, coefficientB, coefficientC, coefficientD);
			this = normalize(this);
		}

		/// <summary>
		/// Constructs a plane with a normal vector and distance from the origin.
		/// </summary>
		/// <remarks>
		/// !!!WARNING!!! consider using Plane3d.CreateFromUnitNormalAndDistance instead
		/// The constructed plane will be the normalized form of the plane specified by the inputs.
		/// </remarks>
		/// <param name="normal">A non-zero vector that is perpendicular to the plane.  It may be any length.</param>
		/// <param name="distance">Distance from the origin along the normal.  A negative value moves the plane in the
		/// same direction as the normal while a positive value moves it in the opposite direction.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane3d(in float3a normal, float distance)
		{
			normalAndDistance = new float4(normal, distance);
			this = normalize(this);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane3d(in float4 normalAndDistance)
		{
			this.normalAndDistance = normalAndDistance;
		}

		/// <summary>
		/// Constructs a plane with a normal vector and a point that lies in the plane.
		/// </summary>
		/// <remarks>
		/// !!!WARNING!!! consider using Plane3d.CreateFromUnitNormalAndPointInPlane instead
		/// The constructed plane will be the normalized form of the plane specified by the inputs.
		/// </remarks>
		/// <param name="normal">A non-zero vector that is perpendicular to the plane.  It may be any length.</param>
		/// <param name="pointInPlane">A point that lies in the plane.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane3d(in float3a normal, in float3a pointInPlane)
			: this(normal, -dot(normal, pointInPlane))
		{
		}

		/// <summary>
		/// Constructs a plane with two vectors and a point that all lie in the plane.
		/// </summary>
		/// <remarks>
		/// The constructed plane will be the normalized form of the plane specified by the inputs.
		/// </remarks>
		/// <param name="vector1InPlane">A non-zero vector that lies in the plane.  It may be any length.</param>
		/// <param name="vector2InPlane">A non-zero vector that lies in the plane.  It may be any length and must not be a scalar multiple of <paramref name="vector1InPlane"/>.</param>
		/// <param name="pointInPlane">A point that lies in the plane.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane3d(in float3a vector1InPlane, in float3a vector2InPlane, in float3a pointInPlane)
			: this(cross(vector1InPlane, vector2InPlane), pointInPlane)
		{
		}

		/// <summary>
		/// Creates a normalized Plane directly without normalization cost.
		/// </summary>
		/// <remarks>
		/// If you have a unit length normal vector, you can create a Plane faster than using one of its constructors
		/// by calling this function.
		/// </remarks>
		/// <param name="unitNormal">A non-zero vector that is perpendicular to the plane.  It must be unit length.</param>
		/// <param name="distance">Distance from the origin along the normal.  A negative value moves the plane in the
		/// same direction as the normal while a positive value moves it in the opposite direction.</param>
		/// <returns>Normalized Plane constructed from given inputs.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d CreateFromUnitNormalAndDistance(in float3a unitNormal, float distance)
		{
			return new Plane3d { normalAndDistance = new float4(unitNormal, distance) };
		}

		/// <summary>
		/// Creates a normalized Plane without normalization cost.
		/// </summary>
		/// <remarks>
		/// If you have a unit length normal vector, you can create a Plane faster than using one of its constructors
		/// by calling this function.
		/// </remarks>
		/// <param name="unitNormal">A non-zero vector that is perpendicular to the plane.  It must be unit length.</param>
		/// <param name="pointInPlane">A point that lies in the plane.</param>
		/// <returns>Normalized Plane constructed from given inputs.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d CreateFromUnitNormalAndPointInPlane(in float3a unitNormal, in float3a pointInPlane)
		{
			return new Plane3d { normalAndDistance = new float4(unitNormal, -dot(unitNormal, pointInPlane)) };
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d CreateFrom3Points(in float3a p0, in float3a p1, in float3a p2)
		{
			return new Plane3d(p1 - p0, p2 - p0, p0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d CreateNonUnitFrom3Points(in float3a p0, in float3a p1, in float3a p2)
		{
			var vector1InPlane = p1 - p0;
			var vector2InPlane = p2 - p0;

			var N = cross(vector1InPlane, vector2InPlane);
			float d = -dot(N, p0);

			return new Plane3d(float4(N, d));
		}

		/// <summary>
		/// Get/set the normal vector of the plane.
		/// </summary>
		/// <remarks>
		/// It is assumed that the normal is unit length.  If you set a new plane such that Ax + By + Cz + Dw = 0 but
		/// (A, B, C) is not unit length, then you must normalize the plane by calling <see cref="Normalize(Plane)"/>.
		/// </remarks>
		public float3 normal
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => normalAndDistance.xyz;
			[MethodImpl(MethodImplOptions.AggressiveInlining)] set => normalAndDistance.xyz = value;
		}

		public float3a normala
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => normalAndDistance.xyza;
			[MethodImpl(MethodImplOptions.AggressiveInlining)] set => normalAndDistance.xyz = value;
		}
		/// <summary>
		/// Get/set the distance of the plane from the origin.  May be a negative value.
		/// </summary>
		/// <remarks>
		/// It is assumed that the normal is unit length.  If you set a new plane such that Ax + By + Cz + Dw = 0 but
		/// (A, B, C) is not unit length, then you must normalize the plane by calling <see cref="Normalize(Plane)"/>.
		/// </remarks>
		public float distance
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => normalAndDistance.w;
			[MethodImpl(MethodImplOptions.AggressiveInlining)] set => normalAndDistance.w = value;
		}

		//
		// Summary:
		//	 Moves the plane in space by the translation vector.
		//
		// Parameters:
		//   translation:
		//	 The offset in space to move the plane with.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Translate(float3a translation)
		{
			distance += dot(normala, translation);
		}

		/// <summary>
		/// Get the signed distance from the point to the plane.
		/// </summary>
		/// <remarks>
		/// Plane must be normalized prior to calling this function.  Distance is positive if point is on side of the
		/// plane the normal points to, negative if on the opposite side and zero if the point lies in the plane.
		/// Avoid comparing equality with 0.0f when testing if a point lies exactly in the plane and use an approximate
		/// comparison instead.
		/// </remarks>
		/// <param name="point">Point to find the signed distance with.</param>
		/// <returns>Signed distance of the point from the plane.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float SignedDistanceToPoint(float3a point)
		{
			CheckPlaneIsNormalized();
			return dot(normalAndDistance, new float4(point, 1.0f));
		}

		/// is a point on the positive side of the plane?
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool IsOnPositiveSide(float3a point)
		{
			return dot(normalAndDistance, new float4(point, 1.0f)) > 0.0f;
		}

		/// <summary>
		/// Projects the given point onto the plane.
		/// </summary>
		/// <remarks>
		/// Plane must be normalized prior to calling this function.  The result is the position closest to the point
		/// that still lies in the plane.
		/// </remarks>
		/// <param name="point">Point to project onto the plane.</param>
		/// <returns>Projected point that's inside the plane.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float3a Projection(float3a point)
		{
			CheckPlaneIsNormalized();
			return point - normala * SignedDistanceToPoint(point);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float3a ClosestPoint(float3a point) => Projection(point);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float ClosestPointDist(float3a point) => abs(SignedDistanceToPoint(point));

		/// <summary>
		/// Flips the plane so the normal points in the opposite direction.
		/// </summary>
		public readonly Plane3d Flipped { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(-normalAndDistance); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Raycast(in Ray3da ray, out float hitDist)
		{
			float num = dot(ray.dir, normala);
			float num2 = -SignedDistanceToPoint(ray.origin);
			if( abs(num) < EPSILON * 4 )
			{
				hitDist = 0f;
				return false;
			}

			hitDist = num2 / num;
			return hitDist > 0f;
		}

		//Find the line of intersection between two planes.	The planes are defined by a normal and a point on that plane.
		//The outputs are a point on the line and a vector which indicates it's direction. If the planes are not parallel, 
		//the function outputs true, otherwise false.
		public readonly bool Intersection(in Plane3d other, out Ray3da intersectionRay)
		{
			var plane1Normal = this.normala;
			var plane2Normal = other.normala;
			//We can get the direction of the line of intersection of the two planes by calculating the 
			//cross product of the normals of the two planes. Note that this is just a direction and the line
			//is not fixed in space yet. We need a point for that to go with the line vector.
			var resLineVec = cross(plane1Normal, plane2Normal);

			//Next is to calculate a point on the line to fix it's position in space. This is done by finding a vector from
			//the plane2 location, moving parallel to it's plane, and intersecting plane1. To prevent rounding
			//errors, this vector also has to be perpendicular to lineDirection. To get this vector, calculate
			//the cross product of the normal of plane2 and the lineDirection.		
			var ldir = cross(plane2Normal, resLineVec);		

			float denominator = dot(plane1Normal, ldir);

			//Prevent divide by zero and rounding errors by requiring about 5 degrees angle between the planes.
			if( abs(denominator) > 0.006f )
			{
				var pointOnThePlane1 = this.Projection(float3c.zero);
				var pointOnThePlane2 = other.Projection(float3c.zero);

				var plane1ToPlane2 = pointOnThePlane1 - pointOnThePlane2;
				float t = dot(plane1Normal, plane1ToPlane2) / denominator;
				var resLinePoint = pointOnThePlane2 + t * ldir;

				intersectionRay = new Ray3da(resLinePoint, resLineVec);
				return true;
			}
			else // output is not valid
			{
				intersectionRay = new Ray3da(float3c.zero, float3c.zero);
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(Plane3d other)
		{
			return normalAndDistance.Equals(other.normalAndDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool SameNormalized(Plane3d other, float tolerance = 1e-3f)
		{
			return dot(normal, other.normal) > (1 - tolerance) && abs(distance - other.distance) < tolerance;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool IsNormalized()
		{
			float ll = lengthsq(normal.xyz);
			const float lowerBound = 0.999f * 0.999f;
			const float upperBound = 1.001f * 1.001f;

			return ll >= lowerBound && ll <= upperBound;
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		public readonly void CheckPlaneIsNormalized()
		{
			if( !IsNormalized() )
				throw new System.ArgumentException("The plane must be normalized. Call normalize(Plane) for normalization.");
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
		static extern void vecILMathRigidTransformMulPlane(in RigidTransforma t, in Plane3d p, out Plane3d res);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d Plane3d(float coefficientA, float coefficientB, float coefficientC, float coefficientD) => new Plane3d(coefficientA, coefficientB, coefficientC, coefficientD);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d Plane3d(in float3a normal, float distance) => new Plane3d(normal, distance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d Plane3d(in float4 normalAndDistance) => new Plane3d(normalAndDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d Plane3d(in float3a normal, in float3a pointInPlane) => new Plane3d(normal, pointInPlane);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d Plane3d(in float3a vector1InPlane, in float3a vector2InPlane, in float3a pointInPlane) => new Plane3d(vector1InPlane, vector2InPlane, pointInPlane);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Plane3d Plane3d(in UnityEngine.Plane p) { return calco.Plane3d.CreateFromUnitNormalAndDistance(float3(p.normal), p.distance); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Plane Plane(in Plane3d p) { return new UnityEngine.Plane(Vec3(p.normal), p.distance); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d transform(in RigidTransforma transf, in Plane3d p)
		{
		#if ENABLE_IL2CPP
			vecILMathRigidTransformMulPlane(in transf, in p, out var res);
			return res;
        #else
			var normal = rotate(in transf, p.normal);
			return new Plane3d(normal, normal * -p.distance + transf.pos);
        #endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d transform(in float4x4 transf, in Plane3d p)
		{
			var pp = p.normal * -p.distance;
			return new Plane3d(rotate(in transf, p.normal), transform(in transf, pp));
		}
	}
}
