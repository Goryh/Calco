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
        public static implicit operator Vector2(float2 v)        { unsafe{ return *(Vector2*)&v; } }

        /// <summary>
        /// Converts a Vector2 to float2.
        /// </summary>
        /// <param name="v">Vector2 to convert.</param>
        /// <returns>The converted float2.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float2(Vector2 v)        { unsafe{ return *(float2*)&v; } }
    }

    public partial struct float4
    {
        /// <summary>
        /// Converts a Vector4 to float4.
        /// </summary>
        /// <param name="v">Vector4 to convert.</param>
        /// <returns>The converted float4.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float4(Vector4 v)        { unsafe{ return *(float4*)&v; } }

        /// <summary>
        /// Converts a float4 to Vector4.
        /// </summary>
        /// <param name="v">float4 to convert.</param>
        /// <returns>The converted Vector4.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4(float4 v)        { unsafe{ return *(Vector4*)&v; } }
    }

    public partial struct quaternion
    {
        /// <summary>
        /// Converts a quaternion to Quaternion.
        /// </summary>
        /// <param name="q">quaternion to convert.</param>
        /// <returns>The converted Quaternion.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Quaternion(quaternion q)  { unsafe{ return *(Quaternion*)&q; } }

        /// <summary>
        /// Converts a Quaternion to quaternion.
        /// </summary>
        /// <param name="q">Quaternion to convert.</param>
        /// <returns>The converted quaternion.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator quaternion(Quaternion q)  { unsafe{ return *(quaternion*)&q; } }
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
}
#endif
