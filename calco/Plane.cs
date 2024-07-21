using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;

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
			normalAndDistance = Normalize(new float4(coefficientA, coefficientB, coefficientC, coefficientD));
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
			normalAndDistance = Normalize(new float4(normal, distance));
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
			: this(normal, -math.dot(normal, pointInPlane))
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
			: this(math.cross(vector1InPlane, vector2InPlane), pointInPlane)
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
			return new Plane3d { normalAndDistance = new float4(unitNormal, -math.dot(unitNormal, pointInPlane)) };
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

		/// <summary>
		/// Normalizes the given Plane.
		/// </summary>
		/// <param name="plane">Plane to normalize.</param>
		/// <returns>Normalized Plane.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d Normalize(Plane3d plane)
		{
			return new Plane3d { normalAndDistance = Normalize(plane.normalAndDistance) };
		}

		/// <summary>
		/// Normalizes the plane represented by the given plane coefficients.
		/// </summary>
		/// <remarks>
		/// The plane coefficients are A, B, C, D and stored in that order in the <see cref="float4"/>.
		/// </remarks>
		/// <param name="planeCoefficients">Plane coefficients A, B, C, D stored in x, y, z, w (respectively).</param>
		/// <returns>Normalized plane coefficients.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float4 Normalize(float4 planeCoefficients)
		{
			float recipLength = math.rsqrt(math.lengthsq(planeCoefficients.xyza));
			return planeCoefficients * recipLength;
		}

		//
		// Summary:
		//	 Moves the plane in space by the translation vector.
		//
		// Parameters:
		//   translation:
		//	 The offset in space to move the plane with.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Translate(float3 translation)
		{
			distance += math.dot(normal, translation);
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
			return math.dot(normalAndDistance, new float4(point, 1.0f));
		}

		/// is a point on the positive side of the plane?
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool IsOnPositiveSide(float3 point)
		{
			return math.dot(normalAndDistance, new float4(point, 1.0f)) > 0.0f;
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
		public readonly float3a ClosestPointOnPlane(float3a point) => Projection(point);

		/// <summary>
		/// Flips the plane so the normal points in the opposite direction.
		/// </summary>
		public readonly Plane3d Flipped { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(-normalAndDistance); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Raycast(in Ray3da ray, out float hitDist)
		{
			float num = math.dot(ray.dir, normala);
			float num2 = -SignedDistanceToPoint(ray.origin);
			if( math.abs(num) < math.EPSILON * 4 )
			{
				hitDist = 0f;
				return false;
			}

			hitDist = num2 / num;
			return hitDist > 0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(Plane3d other)
		{
			return normalAndDistance.Equals(other.normalAndDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool SameNormalized(Plane3d other, float tolerance = 1e-3f)
		{
			return math.dot(normal, other.normal) > (1 - tolerance) && math.abs(distance - other.distance) < tolerance;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool IsNormalized()
		{
			float ll = math.lengthsq(normal.xyz);
			const float lowerBound = 0.999f * 0.999f;
			const float upperBound = 1.001f * 1.001f;

			return ll >= lowerBound && ll <= upperBound;
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		readonly void CheckPlaneIsNormalized()
		{
			if( !IsNormalized() )
				throw new System.ArgumentException("Plane must be normalized. Call Plane.Normalize() to normalize plane.");
		}
	}

	public static partial class math
	{
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

		[MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
		static extern void vecILMathRigidTransformMulPlane(in RigidTransform t, in Plane3d p, out Plane3d res);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d transform(in RigidTransform transf, in Plane3d p)
		{
		#if ENABLE_IL2CPP
			if( !IsBurstEnabled() )
			{
				vecILMathRigidTransformMulPlane(in transf, in p, out var res);
				return res;
			}
		#endif
			var normal = rotate(in transf, p.normal);
			return new Plane3d(normal, normal * -p.distance + transf.posa);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane3d transform(in float4x4 transf, in Plane3d p)
		{
			var pp = p.normal * -p.distance;
			return new Plane3d(rotate(in transf, p.normal), transform(in transf, pp));
		}
	}
}
