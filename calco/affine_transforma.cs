using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
    [Il2CppEagerStaticClassConstruction]
    [Serializable]
    public ref struct AffineTransforma
    {
        public float3ax3 rs;
        public float3a t;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransforma(in float3a translation, in quaternion rotation)
        {
            rs = float3ax3(rotation);
            t = translation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransforma(in float3a translation, in quaternion rotation, in float3a scale)
        {
            rs = mulScale(math.float3ax3(rotation), scale);
            t = translation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransforma(in float3a translation, in float3ax3 rotationScale)
        {
            rs = rotationScale;
            t = translation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransforma(in float3ax3 rotationScale)
        {
            rs = rotationScale;
            t = float3c.zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransforma(in RigidTransforma rigid)
        {
            rs = math.float3ax3(in rigid.rot);
            t = rigid.pos;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransforma(in float3x4 m)
        {
            rs = math.float3ax3(m.c0, m.c1, m.c2);
            t = m.c3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AffineTransforma(in float4x4 m)
        {
            rs = math.float3ax3(m.c0.xyza, m.c1.xyza, m.c2.xyza);
            t = m.c3.xyza;
        }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator AffineTransform(in AffineTransforma v)
		{
			return new AffineTransform(v.t, v.rs);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator AffineTransforma(in AffineTransform v)
		{
			return new AffineTransforma(v.t, v.rs);
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float3x4(in AffineTransforma m) { return float3x4(m.rs.c0, m.rs.c1, m.rs.c2, m.t); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float4x4(in AffineTransforma m) { return float4x4(float4(m.rs.c0, 0f), float4(m.rs.c1, 0f), float4(m.rs.c2, 0f), float4(m.t, 1f)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("AffineTransform(({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f), ({9}f, {10}f, {11}f))",
                rs.c0.x, rs.c1.x, rs.c2.x, rs.c0.y, rs.c1.y, rs.c2.y, rs.c0.z, rs.c1.z, rs.c2.z, t.x, t.y, t.z
            );
        }
    }

    public static partial class math
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineTransforma AffineTransforma(in float3a translation, in quaternion rotation) { return new AffineTransforma(in translation, in rotation); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineTransforma AffineTransforma(in float3a translation, in quaternion rotation, in float3a scale) { return new AffineTransforma(in translation, in rotation, in scale); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineTransforma AffineTransforma(in float3a translation, in float3ax3 rotationScale) { return new AffineTransforma(in translation, in rotationScale); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineTransforma AffineTransforma(in float3ax3 rotationScale) { return new AffineTransforma(in rotationScale); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineTransforma AffineTransforma(in float4x4 m) { return new AffineTransforma(in m); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineTransforma AffineTransforma(in float3x4 m) { return new AffineTransforma(in m); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineTransforma AffineTransforma(in RigidTransforma rigid) { return new AffineTransforma(in rigid); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 float4x4(in AffineTransforma transform) { return float4x4(float4(transform.rs.c0, 0f), float4(transform.rs.c1, 0f), float4(transform.rs.c2, 0f), float4(transform.t, 1f)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 float3x4(in AffineTransforma transform) { return float3x4(transform.rs.c0, transform.rs.c1, transform.rs.c2, transform.t); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineTransforma mul(in AffineTransforma a, in AffineTransforma b)
        {
            return new AffineTransforma(transform(a, b.t), mul(a.rs, b.rs));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineTransforma mul(in float3ax3 a, in AffineTransforma b)
        {
            return new AffineTransforma(mul(a, b.t), mul(a, b.rs));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineTransforma mul(in AffineTransforma a, in float3ax3 b)
        {
            return new AffineTransforma(a.t, mul(b, a.rs));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 mul(in AffineTransforma a, in float4 pos)
        {
            return float4(mul(a.rs, pos.xyza) + a.t * pos.w, pos.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a rotate(in AffineTransforma a, in float3a dir)
        {
            return mul(a.rs, dir);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a transform(in AffineTransforma a, in float3a pos)
        {
            return a.t + mul(a.rs, pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineTransforma inverse(in AffineTransforma a)
        {
            AffineTransforma inv;
            inv.rs = pseudoinverse(a.rs);
            inv.t = mul(inv.rs, -a.t);
            return inv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void decompose(in AffineTransforma a, out float3a translation, out quaternion rotation, out float3a scale)
        {
            translation = a.t;
            rotation = math.rotation(a.rs);
            var sm = mul(float3ax3(conjugate(rotation)), a.rs);
            scale = float3(sm.c0.x, sm.c1.y, sm.c2.z);
        }
    }
}
