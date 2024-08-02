using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
    [Il2CppEagerStaticClassConstruction]
    public static class RigidTransformc
    {
        public static RigidTransforma identity      { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(quaternionc.identity  , float3c.zero); }
        public static RigidTransforma rotateX90	    { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(quaternionc.rotateX90 , float3c.zero); }
        public static RigidTransforma rotateXn90	{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(quaternionc.rotateXn90, float3c.zero); }
        public static RigidTransforma rotateY90	    { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(quaternionc.rotateY90 , float3c.zero); }
        public static RigidTransforma rotateYn90	{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(quaternionc.rotateYn90, float3c.zero); }
        public static RigidTransforma rotateZ90	    { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(quaternionc.rotateZ90 , float3c.zero); }
        public static RigidTransforma rotateZn90	{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(quaternionc.rotateZn90, float3c.zero); }
        public static RigidTransforma rotateX180	{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(quaternionc.rotateX180, float3c.zero); }
        public static RigidTransforma rotateY180	{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(quaternionc.rotateY180, float3c.zero); }
        public static RigidTransforma rotateZ180	{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(quaternionc.rotateZ180, float3c.zero); }

    }

    /// <summary>
    /// A rigid transformation type.
    /// </summary>
    [Il2CppEagerStaticClassConstruction]
    [Serializable]
    public struct RigidTransform
    {
        /// <summary>
        /// The rotation part of the rigid transformation.
        /// </summary>
        public quaternion rot;

        /// <summary>
        /// The translation part of the rigid transformation.
        /// </summary>
        public float3 pos;

        public float3a posa { [MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get => float3a(pos);
                              [MethodImpl(MethodImplOptions.AggressiveInlining)] set {pos = value;} }

        /// <summary>Constructs a RigidTransform from a rotation represented by a unit quaternion and a translation represented by a float3 vector.</summary>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <param name="translation">The translation vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RigidTransform(in quaternion rotation, in float3a translation)
        {
            this.rot = rotation;
            this.pos = translation;
        }

        /// <summary>Constructs a RigidTransform from a rotation represented by a float3x3 matrix and a translation represented by a float3 vector.</summary>
        /// <param name="rotation">The float3x3 rotation matrix.</param>
        /// <param name="translation">The translation vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RigidTransform(in float3ax3 rotation, in float3a translation)
        {
            this.rot = new quaternion(rotation);
            this.pos = translation;
        }

        /// <summary>Constructs a RigidTransform from a float4x4. Assumes the matrix is orthonormal.</summary>
        /// <param name="transform">The float4x4 transformation matrix, must be orthonormal.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RigidTransform(in float4x4 transform)
        {
            this.rot = new quaternion(transform);
            this.pos = transform.c3.xyz;
        }

        /// <summary>Returns true if the RigidTransform is equal to a given RigidTransform, false otherwise.</summary>
        /// <param name="x">The RigidTransform to compare with.</param>
        /// <returns>True if the RigidTransform is equal to the input, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(RigidTransform x) { return rot.Equals(x.rot) && pos.Equals(x.pos); }

        /// <summary>Returns true if the RigidTransform is equal to a given RigidTransform, false otherwise.</summary>
        /// <param name="x">The object to compare with.</param>
        /// <returns>True if the RigidTransform is equal to the input, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override bool Equals(object x) { return Equals((RigidTransform)x); }

        /// <summary>Returns a hash code for the RigidTransform.</summary>
        /// <returns>The hash code of the RigidTransform.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override int GetHashCode() { return (int)math.hash(this); }

        /// <summary>Returns a string representation of the RigidTransform.</summary>
        /// <returns>The string representation of the RigidTransform.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override string ToString()
        {
            return string.Format("RigidTransform(({0}f, {1}f, {2}f, {3}f),  ({4}f, {5}f, {6}f))",
                rot.value.x, rot.value.y, rot.value.z, rot.value.w, pos.x, pos.y, pos.z);
        }

        /// <summary>Returns a string representation of the RigidTransform using a specified format and culture-specific format information.</summary>
        /// <param name="format">The format string.</param>
        /// <param name="formatProvider">The format provider to use during formatting.</param>
        /// <returns>The formatted string representation of the RigidTransform.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("float4x4(({0}f, {1}f, {2}f, {3}f),  ({4}f, {5}f, {6}f))",
                rot.value.x.ToString(format, formatProvider),
                rot.value.y.ToString(format, formatProvider),
                rot.value.z.ToString(format, formatProvider),
                rot.value.w.ToString(format, formatProvider),
                pos.x.ToString(format, formatProvider),
                pos.y.ToString(format, formatProvider),
                pos.z.ToString(format, formatProvider));
        }
    }

    public static partial class math
    {
        /// <summary>Returns a uint hash code of a RigidTransform.</summary>
        /// <param name="t">The RigidTransform to hash.</param>
        /// <returns>The hash code of the input RigidTransform</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(in RigidTransform t)
        {
            return hash(t.rot) + 0xC5C5394Bu * hash(t.pos);
        }

        /// <summary>
        /// Returns a uint4 vector hash code of a RigidTransform.
        /// When multiple elements are to be hashes together, it can more efficient to calculate and combine wide hash
        /// that are only reduced to a narrow uint hash at the very end instead of at every step.
        /// </summary>
        /// <param name="t">The RigidTransform to hash.</param>
        /// <returns>The uint4 wide hash code.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 hashwide(in RigidTransform t)
        {
            return hashwide(t.rot) + 0xC5C5394Bu * hashwide(t.pos).xyzz;
        }
    }
}
