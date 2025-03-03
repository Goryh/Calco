using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
    [Il2CppEagerStaticClassConstruction]
    public static class AffineTransformc
    {
        public static AffineTransforma identity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new AffineTransforma(float3c.zero, float3ax3c.identity); }
    }

    /// <summary>
    /// An affine transformation type.
    /// </summary>
    [Il2CppEagerStaticClassConstruction]
    [Serializable]
    public struct AffineTransform : IEquatable<AffineTransform>, IFormattable
    {
        /// <summary>
        /// The rotation and scale part of the affine transformation.
        /// </summary>
        public float3x3 rs;

        /// <summary>
        /// The translation part of the affine transformation.
        /// </summary>
        public float3 t;

        /// <summary>Constructs an AffineTransform from a translation represented by a float3 vector and rotation represented by a unit quaternion.</summary>
        /// <param name="translation">The translation vector.</param>
        /// <param name="rotation">The rotation quaternion.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransform(in float3 translation, in quaternion rotation)
        {
            rs = float3ax3(rotation);
            t = translation;
        }

        /// <summary>Constructs an AffineTransform from a translation represented by a float3 vector, rotation represented by a unit quaternion and scale represented by a float3 vector.</summary>
        /// <param name="translation">The translation vector.</param>
        /// <param name="rotation">The rotation quaternion.</param>
        /// <param name="scale">The scale vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransform(in float3 translation, in quaternion rotation, in float3 scale)
        {
            rs = mulScale(math.float3ax3(rotation), scale);
            t = translation;
        }

        /// <summary>Constructs an AffineTransform from a translation represented by float3 vector and a float3x3 matrix representing both rotation and scale.</summary>
        /// <param name="translation">The translation vector.</param>
        /// <param name="rotationScale">The rotation and scale matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransform(in float3 translation, in float3x3 rotationScale)
        {
            rs = rotationScale;
            t = translation;
        }

        /// <summary>Constructs an AffineTransform from float3x3 matrix representating both rotation and scale.</summary>
        /// <param name="rotationScale">The rotation and scale matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransform(in float3x3 rotationScale)
        {
            rs = rotationScale;
            t = float3c.zero;
        }

        /// <summary>Constructs an AffineTransform from a RigidTransform.</summary>
        /// <param name="rigid">The RigidTransform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransform(in RigidTransforma rigid)
        {
            rs = math.float3ax3(rigid.rot);
            t = rigid.pos;
        }

        /// <summary>Constructs an AffineTransform from a float3x4 matrix.</summary>
        /// <param name="m">The float3x4 matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransform(in float3x4 m)
        {
            rs = math.float3ax3(m.c0, m.c1, m.c2);
            t = m.c3;
        }

        /// <summary>Constructs an AffineTransform from a float4x4 matrix.</summary>
        /// <param name="m">The float4x4 matrix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransform(in float4x4 m)
        {
            rs = math.float3ax3(m.c0.xyza, m.c1.xyza, m.c2.xyza);
            t = m.c3.xyz;
        }

        /// <summary>Implicit float3x4 cast operator.</summary>
        /// <param name="m">The AffineTransform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float3x4(in AffineTransform m) { return float3x4(m.rs.c0, m.rs.c1, m.rs.c2, m.t); }

        /// <summary>Implicit float4x4 cast operator.</summary>
        /// <param name="m">The AffineTransform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float4x4(in AffineTransform m) { return float4x4(float4(m.rs.c0, 0f), float4(m.rs.c1, 0f), float4(m.rs.c2, 0f), float4(m.t, 1f)); }

        /// <summary>Returns true if the AffineTransform is equal to a given AffineTransform, false otherwise.</summary>
        /// <param name="rhs">Right hand side argument to compare equality with.</param>
        /// <returns>The result of the equality comparison.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(AffineTransform rhs) { return rs.Equals(rhs.rs) && t.Equals(rhs.t); }

        /// <summary>Returns true if the AffineTransform is equal to a given AffineTransform, false otherwise.</summary>
        /// <param name="o">Right hand side argument to compare equality with.</param>
        /// <returns>The result of the equality comparison.</returns>
        public override bool Equals(object o) { return o is AffineTransform converted && Equals(converted); }

        /// <summary>Returns a hash code for the AffineTransform.</summary>
        /// <returns>The computed hash code.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() { return (int)hash(this); }

        /// <summary>Returns a string representation of the AffineTransform.</summary>
        /// <returns>String representation of the value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("AffineTransform(({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f), ({9}f, {10}f, {11}f))",
                rs.c0.x, rs.c1.x, rs.c2.x, rs.c0.y, rs.c1.y, rs.c2.y, rs.c0.z, rs.c1.z, rs.c2.z, t.x, t.y, t.z
            );
        }

        /// <summary>Returns a string representation of the AffineTransform using a specified format and culture-specific format information.</summary>
        /// <param name="format">Format string to use during string formatting.</param>
        /// <param name="formatProvider">Format provider to use during string formatting.</param>
        /// <returns>String representation of the value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("AffineTransform(({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f), ({9}f, {10}f, {11}f))",
                rs.c0.x.ToString(format, formatProvider), rs.c1.x.ToString(format, formatProvider), rs.c2.x.ToString(format, formatProvider),
                rs.c0.y.ToString(format, formatProvider), rs.c1.y.ToString(format, formatProvider), rs.c2.y.ToString(format, formatProvider),
                rs.c0.z.ToString(format, formatProvider), rs.c1.z.ToString(format, formatProvider), rs.c2.z.ToString(format, formatProvider),
                t.x.ToString(format, formatProvider), t.y.ToString(format, formatProvider), t.z.ToString(format, formatProvider)
            );
        }
    }

    public static partial class math
    {
        /// <summary>Returns a uint hash code of an AffineTransform.</summary>
        /// <param name="a">The AffineTransform to hash.</param>
        /// <returns>The hash code of the input AffineTransform.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(in AffineTransform a)
        {
            return hash(a.rs) + 0xC5C5394Bu * hash(a.t);
        }

        /// <summary>
        /// Returns a uint4 vector hash code of an AffineTransform.
        /// When multiple elements are to be hashes together, it can more efficient to calculate and combine wide hash
        /// that are only reduced to a narrow uint hash at the very end instead of at every step.
        /// </summary>
        /// <param name="a">The AffineTransform to hash.</param>
        /// <returns>The uint4 wide hash code.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 hashwide(in AffineTransform a)
        {
            return hashwide(a.rs).xyzz + 0xC5C5394Bu * hashwide(a.t).xyzz;
        }
    }
}
