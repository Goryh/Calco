#if !UNITY_DOTSPLAYER
using UnityEngine;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

#pragma warning disable 0660, 0661

namespace calco
{
    public partial struct float2
    {
        /// <summary>
        /// Converts a float2 to Vector2.
        /// </summary>
        /// <param name="v">float2 to convert.</param>
        /// <returns>The converted Vector2.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2(in float2 v)     { return new Vector2(v.x, v.y); }

        /// <summary>
        /// Converts a Vector2 to float2.
        /// </summary>
        /// <param name="v">Vector2 to convert.</param>
        /// <returns>The converted float2.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float2(in Vector2 v)     { return new float2(v.x, v.y); }
    }

    public partial struct float3
    {
        /// <summary>
        /// Converts a float3 to Vector3.
        /// </summary>
        /// <param name="v">float3 to convert.</param>
        /// <returns>The converted Vector3.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3(in float3 v)     { return new Vector3(v.x, v.y, v.z); }

        /// <summary>
        /// Converts a Vector3 to float3.
        /// </summary>
        /// <param name="v">Vector3 to convert.</param>
        /// <returns>The converted float3.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float3(in Vector3 v)     { return new float3(v.x, v.y, v.z); }
    }

    public partial struct float4
    {
        /// <summary>
        /// Converts a Vector4 to float4.
        /// </summary>
        /// <param name="v">Vector4 to convert.</param>
        /// <returns>The converted float4.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float4(in Vector4 v)     { return new float4(v.x, v.y, v.z, v.w); }

        /// <summary>
        /// Converts a float4 to Vector4.
        /// </summary>
        /// <param name="v">float4 to convert.</param>
        /// <returns>The converted Vector4.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4(in float4 v)     { return new Vector4(v.x, v.y, v.z, v.w); }
    }

    public partial struct quaternion
    {
        /// <summary>
        /// Converts a quaternion to Quaternion.
        /// </summary>
        /// <param name="q">quaternion to convert.</param>
        /// <returns>The converted Quaternion.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Quaternion(in quaternion q)  { return new Quaternion(q.value.x, q.value.y, q.value.z, q.value.w); }

        /// <summary>
        /// Converts a Quaternion to quaternion.
        /// </summary>
        /// <param name="q">Quaternion to convert.</param>
        /// <returns>The converted quaternion.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator quaternion(in Quaternion q)  { return new quaternion(q.x, q.y, q.z, q.w); }
    }

    public partial struct float4x4
    {
        /// <summary>
        /// Converts a Matrix4x4 to float4x4.
        /// </summary>
        /// <param name="m">Matrix4x4 to convert.</param>
        /// <returns>The converted float4x4.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float4x4(Matrix4x4 m) { unsafe{ return new float4x4(*(float4*)&m.m00, *(float4*)&m.m01, *(float4*)&m.m02, *(float4*)&m.m03); } }

        /// <summary>
        /// Converts a float4x4 to Matrix4x4.
        /// </summary>
        /// <param name="m">float4x4 to convert.</param>
        /// <returns>The converted Matrix4x4.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Matrix4x4(in float4x4 m) { return new Matrix4x4(m.c0, m.c1, m.c2, m.c3); }
    }

    public partial struct Aabb
    {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Aabb(in Bounds b)  { return new Aabb((float3)b.min, (float3)b.max); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Bounds(in Aabb b)  { return new Bounds((float3)b.center, (float3)b.extents); }
    }

    public partial struct Ray3d
    {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ray3d(in Ray r)  { return new Ray3d(r.origin, r.direction); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ray(in Ray3d r)  { return new Ray(r.origin, r.dir); }
    }

    public partial struct Plane3d
    {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Plane3d(in Plane p)  { return CreateFromUnitNormalAndDistance(p.normal, p.distance); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Plane(in Plane3d p)  { return new Plane(){normal = p.normal, distance = p.distance}; }
    }
}
#endif
