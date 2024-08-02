using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
    [Il2CppEagerStaticClassConstruction]
    [Serializable]
    public ref struct RigidTransforma
    {
        public quaternion rot;
        public float3a pos;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RigidTransforma(in quaternion rotation, in float3a translation)
        {
            this.rot = rotation;
            this.pos = translation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RigidTransforma(in float3ax3 rotation, in float3a translation)
        {
            this.rot = new quaternion(rotation);
            this.pos = translation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RigidTransforma(in float4x4 transform)
        {
            this.rot = new quaternion(transform);
            this.pos = transform.c3.xyza;
        }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator RigidTransform(in RigidTransforma v)
		{
			return new RigidTransform(v.rot, v.pos);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator RigidTransforma(in RigidTransform v)
		{
			return new RigidTransforma(v.rot, v.pos);
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma AxisAngle(in float3a axis, float angle) { return new RigidTransforma(quaternion.AxisAngle(axis, angle), float3c.zero); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerXYZ(in float3 xyz) { return new RigidTransforma(quaternion.EulerXYZ(xyz), float3c.zero); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerXZY(in float3 xyz) { return new RigidTransforma(quaternion.EulerXZY(xyz), float3c.zero); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerYXZ(in float3 xyz) { return new RigidTransforma(quaternion.EulerYXZ(xyz), float3c.zero); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerYZX(in float3 xyz) { return new RigidTransforma(quaternion.EulerYZX(xyz), float3c.zero); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerZXY(in float3 xyz) { return new RigidTransforma(quaternion.EulerZXY(xyz), float3c.zero); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerZYX(in float3 xyz) { return new RigidTransforma(quaternion.EulerZYX(xyz), float3c.zero); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerXYZ(float x, float y, float z) { return EulerXYZ(float3(x, y, z)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerXZY(float x, float y, float z) { return EulerXZY(float3(x, y, z)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerYXZ(float x, float y, float z) { return EulerYXZ(float3(x, y, z)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerYZX(float x, float y, float z) { return EulerYZX(float3(x, y, z)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerZXY(float x, float y, float z) { return EulerZXY(float3(x, y, z)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma EulerZYX(float x, float y, float z) { return EulerZYX(float3(x, y, z)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma Euler(in float3 xyz, RotationOrder order = RotationOrder.ZXY)
        {
            switch (order)
            {
                case RotationOrder.XYZ:
                    return EulerXYZ(xyz);
                case RotationOrder.XZY:
                    return EulerXZY(xyz);
                case RotationOrder.YXZ:
                    return EulerYXZ(xyz);
                case RotationOrder.YZX:
                    return EulerYZX(xyz);
                case RotationOrder.ZXY:
                    return EulerZXY(xyz);
                case RotationOrder.ZYX:
                    return EulerZYX(xyz);
                default:
                    return RigidTransformc.identity;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma Euler(float x, float y, float z, RotationOrder order = RotationOrder.Default)
        {
            return Euler(float3(x, y, z), order);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma RotateX(float angle) { return new RigidTransforma(quaternion.RotateX(angle), float3c.zero); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma RotateY(float angle) { return new RigidTransforma(quaternion.RotateY(angle), float3c.zero); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma RotateZ(float angle) { return new RigidTransforma(quaternion.RotateZ(angle), float3c.zero); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma Translate(in float3a vector) { return new RigidTransforma(quaternionc.identity, vector); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override string ToString()
        {
            return string.Format("RigidTransform(({0}f, {1}f, {2}f, {3}f),  ({4}f, {5}f, {6}f))",
                rot.value.x, rot.value.y, rot.value.z, rot.value.w, pos.x, pos.y, pos.z);
        }
    }

    public static partial class math
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathRigidTransformaInverse(in RigidTransforma t, out RigidTransforma res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathRigidTransformaMul(in RigidTransforma a, in RigidTransforma b, out RigidTransforma res);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma RigidTransforma(in quaternion rot, in float3a pos) { return new RigidTransforma(rot, pos); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma RigidTransforma(in float3ax3 rotation, in float3a translation) { return new RigidTransforma(rotation, translation); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma RigidTransforma(in float4x4 transform) { return new RigidTransforma(transform); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma inverse(in RigidTransforma t)
        {
        #if ENABLE_IL2CPP
            vecILMathRigidTransformaInverse(in t, out var res);
            return res;
        #else
            quaternion invRotation = inverse(t.rot);
            var invTranslation = mul(invRotation, -t.pos);
            return new RigidTransforma(invRotation, invTranslation);
        #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma mul(in RigidTransforma a, in RigidTransforma b)
        {
        #if ENABLE_IL2CPP
            vecILMathRigidTransformaMul(in a, in b, out var res);
            return res;
        #else
            return new RigidTransforma(mul(a.rot, b.rot), mul(a.rot, b.pos) + a.pos);
        #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 mul(in RigidTransforma a, in float4 pos)
        {
            return float4(mul(a.rot, pos.xyza) + a.pos * pos.w, pos.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a rotate(in RigidTransforma a, in float3a dir)
        {
            return mul(a.rot, dir);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransforma lerp(in RigidTransforma a, in RigidTransforma b, float s)
        {
            return RigidTransforma(slerp(a.rot, b.rot, s), lerp(a.pos, b.pos, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a transform(in RigidTransforma a, in float3a pos)
        {
            return mul(a.rot, pos) + a.pos;
        }
    }
}
