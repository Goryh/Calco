using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using static Unity.Mathematics.math;

namespace Unity.Mathematics
{
    [Il2CppEagerStaticClassConstruction]
    public static class quaternionc
    {
        /// <summary>A quaternion representing the identity transform.</summary>
        public static quaternion identity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new(0.0f, 0.0f, 0.0f, 1.0f); }
    }

    /// <summary>
    /// A quaternion type for representing rotations.
    /// </summary>
    [Il2CppEagerStaticClassConstruction]
    [Serializable]
    public partial struct quaternion : System.IEquatable<quaternion>, IFormattable
    {
        /// <summary>
        /// The quaternion component values.
        /// </summary>
        public float4 value;

        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILQuatFromFloat3ax3(in float3ax3 m, out quaternion res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILQuatFromFloat4x4(in float4x4 m, out quaternion res);

        /// <summary>Constructs a quaternion from four float values.</summary>
        /// <param name="x">The quaternion x component.</param>
        /// <param name="y">The quaternion y component.</param>
        /// <param name="z">The quaternion z component.</param>
        /// <param name="w">The quaternion w component.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public quaternion(float x, float y, float z, float w) { value.x = x; value.y = y; value.z = z; value.w = w; }

        /// <summary>Constructs a quaternion from float4 vector.</summary>
        /// <param name="value">The quaternion xyzw component values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public quaternion(in float4 value) { this.value = value; }

        /// <summary>Implicitly converts a float4 vector to a quaternion.</summary>
        /// <param name="v">The quaternion xyzw component values.</param>
        /// <returns>The quaternion constructed from a float4 vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator quaternion(in float4 v) { return new quaternion(v); }

        /// <summary>Constructs a unit quaternion from a float3x3 rotation matrix. The matrix must be orthonormal.</summary>
        /// <param name="m">The float3x3 orthonormal rotation matrix.</param>
        public quaternion(in float3ax3 m)
        {
        #if ENABLE_IL2CPP
            if( !math.IsBurstEnabled() )
            {
                vecILQuatFromFloat3ax3(in m, out this);
                return;
            }
            else
        #endif
            {
                float3 u = m.c0;
                float3 v = m.c1;
                float3 w = m.c2;

                uint u_sign = (asuint(u.x) & 0x80000000);
                float t = v.y + asfloat(asuint(w.z) ^ u_sign);
                uint4 u_mask = uint4((int)u_sign >> 31);
                uint4 t_mask = uint4(asint(t) >> 31);

                float tr = 1.0f + abs(u.x);

                uint4 sign_flips = uint4(0x00000000, 0x80000000, 0x80000000, 0x80000000) ^ (u_mask & uint4(0x00000000, 0x80000000, 0x00000000, 0x80000000)) ^ (t_mask & uint4(0x80000000, 0x80000000, 0x80000000, 0x00000000));

                value = float4(tr, u.y, w.x, v.z) + asfloat(asuint(float4(t, v.x, u.z, w.y)) ^ sign_flips);   // +---, +++-, ++-+, +-++

                value = asfloat((asuint(value) & ~u_mask) | (asuint(value.zwxy) & u_mask));
                value = asfloat((asuint(value.wzyx) & ~t_mask) | (asuint(value) & t_mask));
                value = normalize(value);
            }
        }

        /// <summary>Constructs a unit quaternion from an orthonormal float4x4 matrix.</summary>
        /// <param name="m">The float4x4 orthonormal rotation matrix.</param>
        public quaternion(in float4x4 m)
        {
        #if ENABLE_IL2CPP
            if( !math.IsBurstEnabled() )
            {
                vecILQuatFromFloat4x4(in m, out this);
                return;
            }
            else
        #endif
            {
                float4 u = m.c0;
                float4 v = m.c1;
                float4 w = m.c2;

                uint u_sign = (asuint(u.x) & 0x80000000);
                float t = v.y + asfloat(asuint(w.z) ^ u_sign);
                uint4 u_mask = uint4((int)u_sign >> 31);
                uint4 t_mask = uint4(asint(t) >> 31);

                float tr = 1.0f + abs(u.x);

                uint4 sign_flips = uint4(0x00000000, 0x80000000, 0x80000000, 0x80000000) ^ (u_mask & uint4(0x00000000, 0x80000000, 0x00000000, 0x80000000)) ^ (t_mask & uint4(0x80000000, 0x80000000, 0x80000000, 0x00000000));

                value = float4(tr, u.y, w.x, v.z) + asfloat(asuint(float4(t, v.x, u.z, w.y)) ^ sign_flips);   // +---, +++-, ++-+, +-++

                value = asfloat((asuint(value) & ~u_mask) | (asuint(value.zwxy) & u_mask));
                value = asfloat((asuint(value.wzyx) & ~t_mask) | (asuint(value) & t_mask));

                value = normalize(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathQuatFromAxisAngle(in float3a axis, float angle, out quaternion q);

        /// <summary>
        /// Returns a quaternion representing a rotation around a unit axis by an angle in radians.
        /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The quaternion representing a rotation around an axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion AxisAngle(in float3a axis, float angle)
        {
        #if ENABLE_IL2CPP
            if( !IsBurstEnabled() )
            {
                vecILMathQuatFromAxisAngle(in axis, angle, out var res);
                return res;
            }
        #endif
            sincos(0.5f * angle, out float sina, out float cosa);
            return quaternion(float4(axis * sina, cosa));
        }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerXYZ(in float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateY(xyz.y), rotateX(xyz.x)));
            float3 s, c;
            sincos(0.5f * xyz, out s, out c);
            return quaternion(
                // s.x * c.y * c.z - s.y * s.z * c.x,
                // s.y * c.x * c.z + s.x * s.z * c.y,
                // s.z * c.x * c.y - s.x * s.y * c.z,
                // c.x * c.y * c.z + s.y * s.z * s.x
                float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(-1.0f, 1.0f, -1.0f, 1.0f)
                );
        }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerXZY(in float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateZ(xyz.z), rotateX(xyz.x)));
            float3 s, c;
            sincos(0.5f * xyz, out s, out c);
            return quaternion(
                // s.x * c.y * c.z + s.y * s.z * c.x,
                // s.y * c.x * c.z + s.x * s.z * c.y,
                // s.z * c.x * c.y - s.x * s.y * c.z,
                // c.x * c.y * c.z - s.y * s.z * s.x
                float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(1.0f, 1.0f, -1.0f, -1.0f)
                );
        }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerYXZ(in float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateX(xyz.x), rotateY(xyz.y)));
            float3 s, c;
            sincos(0.5f * xyz, out s, out c);
            return quaternion(
                // s.x * c.y * c.z - s.y * s.z * c.x,
                // s.y * c.x * c.z + s.x * s.z * c.y,
                // s.z * c.x * c.y + s.x * s.y * c.z,
                // c.x * c.y * c.z - s.y * s.z * s.x
                float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(-1.0f, 1.0f, 1.0f, -1.0f)
                );
        }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerYZX(in float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateZ(xyz.z), rotateY(xyz.y)));
            float3 s, c;
            sincos(0.5f * xyz, out s, out c);
            return quaternion(
                // s.x * c.y * c.z - s.y * s.z * c.x,
                // s.y * c.x * c.z - s.x * s.z * c.y,
                // s.z * c.x * c.y + s.x * s.y * c.z,
                // c.x * c.y * c.z + s.y * s.z * s.x
                float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(-1.0f, -1.0f, 1.0f, 1.0f)
                );
        }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerZXY(in float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateX(xyz.x), rotateZ(xyz.z)));
            float3 s, c;
            sincos(0.5f * xyz, out s, out c);
            return quaternion(
                // s.x * c.y * c.z + s.y * s.z * c.x,
                // s.y * c.x * c.z - s.x * s.z * c.y,
                // s.z * c.x * c.y - s.x * s.y * c.z,
                // c.x * c.y * c.z + s.y * s.z * s.x
                float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(1.0f, -1.0f, -1.0f, 1.0f)
                );
        }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerZYX(in float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateY(xyz.y), rotateZ(xyz.z)));
            float3 s, c;
            sincos(0.5f * xyz, out s, out c);
            return quaternion(
                // s.x * c.y * c.z + s.y * s.z * c.x,
                // s.y * c.x * c.z - s.x * s.z * c.y,
                // s.z * c.x * c.y + s.x * s.y * c.z,
                // c.x * c.y * c.z - s.y * s.x * s.z
                float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(1.0f, -1.0f, 1.0f, -1.0f)
                );
        }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerXYZ(float x, float y, float z) { return EulerXYZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerXZY(float x, float y, float z) { return EulerXZY(float3(x, y, z)); }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerYXZ(float x, float y, float z) { return EulerYXZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerYZX(float x, float y, float z) { return EulerYZX(float3(x, y, z)); }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerZXY(float x, float y, float z) { return EulerZXY(float3(x, y, z)); }

        /// <summary>
        /// Returns a quaternion constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The quaternion representing the Euler angle rotation in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion EulerZYX(float x, float y, float z) { return EulerZYX(float3(x, y, z)); }

        /// <summary>
        /// Returns a quaternion constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The quaternion representing the Euler angle rotation in the specified order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion Euler(in float3 xyz, RotationOrder order = RotationOrder.ZXY)
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
                    return quaternionc.identity;
            }
        }

        /// <summary>
        /// Returns a quaternion constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The quaternion representing the Euler angle rotation in the specified order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion Euler(float x, float y, float z, RotationOrder order = RotationOrder.Default)
        {
            return Euler(float3(x, y, z), order);
        }

        /// <summary>Returns a quaternion that rotates around the x-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the x-axis towards the origin in radians.</param>
        /// <returns>The quaternion representing a rotation around the x-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion RotateX(float angle)
        {
            float sina, cosa;
            math.sincos(0.5f * angle, out sina, out cosa);
            return quaternion(sina, 0.0f, 0.0f, cosa);
        }

        /// <summary>Returns a quaternion that rotates around the y-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the y-axis towards the origin in radians.</param>
        /// <returns>The quaternion representing a rotation around the y-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion RotateY(float angle)
        {
            float sina, cosa;
            math.sincos(0.5f * angle, out sina, out cosa);
            return quaternion(0.0f, sina, 0.0f, cosa);
        }

        /// <summary>Returns a quaternion that rotates around the z-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the z-axis towards the origin in radians.</param>
        /// <returns>The quaternion representing a rotation around the z-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion RotateZ(float angle)
        {
            float sina, cosa;
            math.sincos(0.5f * angle, out sina, out cosa);
            return quaternion(0.0f, 0.0f, sina, cosa);
        }

        // return a quaternion that rotates towards the newUP direction trying to keep the result
        // right and forward as close as possible to the provided
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion TiltRotationRobust(in float3a oldRight, in float3a oldForward, in float3a newUp)
        {
			var right = normalize(cross(newUp, oldForward));
			var forwardFromForward = cross(right, newUp);
			var forwardFromRight = normalize(cross(oldRight, newUp));

			right = normalize(cross(newUp, lerp(forwardFromForward, forwardFromRight, 0.5f))); // take the middle option of a forward restored from the original forward and from the original right
			var forward = cross(right, newUp);

			return quaternion(float3ax3(right, newUp, forward));
        }

        /// <summary>
        /// Returns a quaternion view rotation given a unit length forward vector and a unit length up vector.
        /// The two input vectors are assumed to be unit length and not collinear.
        /// If these assumptions are not met use float3x3.LookRotationSafe instead.
        /// </summary>
        /// <param name="forward">The view forward direction.</param>
        /// <param name="up">The view up direction.</param>
        /// <returns>The quaternion view rotation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion LookRotation(in float3a forward, in float3a up)
        {
            var t = normalize(cross(up, forward));
            return quaternion(float3ax3(t, cross(forward, t), forward));
        }

        /// <summary>
        /// Returns a quaternion view rotation given a forward vector and an up vector.
        /// The two input vectors are not assumed to be unit length.
        /// If the magnitude of either of the vectors is so extreme that the calculation cannot be carried out reliably or the vectors are collinear,
        /// the identity will be returned instead.
        /// </summary>
        /// <param name="forward">The view forward direction.</param>
        /// <param name="up">The view up direction.</param>
        /// <returns>The quaternion view rotation or the identity quaternion.</returns>
        public static quaternion LookRotationSafe(float3a forward, float3a up)
        {
            float forwardLengthSq = dot(forward, forward);
            float upLengthSq = dot(up, up);

            forward *= rsqrt(forwardLengthSq);
            up *= rsqrt(upLengthSq);

            var t = cross(up, forward);
            float tLengthSq = dot(t, t);
            t *= rsqrt(tLengthSq);

            float mn = min(min(forwardLengthSq, upLengthSq), tLengthSq);
            float mx = max(max(forwardLengthSq, upLengthSq), tLengthSq);

            bool accept = mn > 1e-35f && mx < 1e35f && isfinite(forwardLengthSq) && isfinite(upLengthSq) && isfinite(tLengthSq);
            return quaternion(select(float4(0.0f, 0.0f, 0.0f, 1.0f), quaternion(float3ax3(t, cross(forward, t),forward)).value, accept));
        }

        /// <summary>Returns true if the quaternion is equal to a given quaternion, false otherwise.</summary>
        /// <param name="x">The quaternion to compare with.</param>
        /// <returns>True if the quaternion is equal to the input, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(quaternion x) { return value.x == x.value.x && value.y == x.value.y && value.z == x.value.z && value.w == x.value.w; }

        /// <summary>Returns whether true if the quaternion is equal to a given quaternion, false otherwise.</summary>
        /// <param name="x">The object to compare with.</param>
        /// <returns>True if the quaternion is equal to the input, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override bool Equals(object x) { return x is quaternion converted && Equals(converted); }

        /// <summary>Returns a hash code for the quaternion.</summary>
        /// <returns>The hash code of the quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override int GetHashCode() { return (int)math.hash(this); }

        /// <summary>Returns a string representation of the quaternion.</summary>
        /// <returns>The string representation of the quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override string ToString()
        {
            return string.Format("quaternion({0}f, {1}f, {2}f, {3}f)", value.x, value.y, value.z, value.w);
        }

        /// <summary>Returns a string representation of the quaternion using a specified format and culture-specific format information.</summary>
        /// <param name="format">The format string.</param>
        /// <param name="formatProvider">The format provider to use during string formatting.</param>
        /// <returns>The formatted string representation of the quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("quaternion({0}f, {1}f, {2}f, {3}f)", value.x.ToString(format, formatProvider), value.y.ToString(format, formatProvider), value.z.ToString(format, formatProvider), value.w.ToString(format, formatProvider));
        }
    }

    public static partial class math
    {
        /// <summary>Returns a quaternion constructed from four float values.</summary>
        /// <param name="x">The x component of the quaternion.</param>
        /// <param name="y">The y component of the quaternion.</param>
        /// <param name="z">The z component of the quaternion.</param>
        /// <param name="w">The w component of the quaternion.</param>
        /// <returns>The quaternion constructed from individual components.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion quaternion(float x, float y, float z, float w) { return new quaternion(x, y, z, w); }

        /// <summary>Returns a quaternion constructed from a float4 vector.</summary>
        /// <param name="value">The float4 containing the components of the quaternion.</param>
        /// <returns>The quaternion constructed from a float4.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion quaternion(in float4 value) { return new quaternion(value); }

        /// <summary>Returns a unit quaternion constructed from a float3x3 rotation matrix. The matrix must be orthonormal.</summary>
        /// <param name="m">The float3x3 rotation matrix.</param>
        /// <returns>The quaternion constructed from a float3x3 matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion quaternion(in float3ax3 m) { return new quaternion(m); }

        /// <summary>Returns a unit quaternion constructed from a float4x4 matrix. The matrix must be orthonormal.</summary>
        /// <param name="m">The float4x4 matrix (must be orthonormal).</param>
        /// <returns>The quaternion constructed from a float4x4 matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion quaternion(in float4x4 m) { return new quaternion(m); }

       /// <summary>Returns the conjugate of a quaternion value.</summary>
       /// <param name="q">The quaternion to conjugate.</param>
       /// <returns>The conjugate of the input quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion conjugate(in quaternion q)
        {
            return quaternion(q.value * float4(-1.0f, -1.0f, -1.0f, 1.0f));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathQuaternionInverse(in quaternion q, out quaternion res);

       /// <summary>Returns the inverse of a quaternion value.</summary>
       /// <param name="q">The quaternion to invert.</param>
       /// <returns>The quaternion inverse of the input quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion inverse(in quaternion q)
        {
        #if ENABLE_IL2CPP
            if( !IsBurstEnabled() )
            {
                vecILMathQuaternionInverse(in q, out var res);
                return res;
            }
        #endif
            float4 x = q.value;
            return quaternion(rcp(dot(x, x)) * x * float4(-1.0f, -1.0f, -1.0f, 1.0f));
        }

        /// <summary>Returns the dot product of two quaternions.</summary>
        /// <param name="a">The first quaternion.</param>
        /// <param name="b">The second quaternion.</param>
        /// <returns>The dot product of two quaternions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float dot(in quaternion a, in quaternion b)
        {
            return dot(a.value, b.value);
        }

        /// <summary>Returns the length of a quaternion.</summary>
        /// <param name="q">The input quaternion.</param>
        /// <returns>The length of the input quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float length(in quaternion q)
        {
            return sqrt(dot(q.value, q.value));
        }

        /// <summary>Returns the squared length of a quaternion.</summary>
        /// <param name="q">The input quaternion.</param>
        /// <returns>The length squared of the input quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float lengthsq(in quaternion q)
        {
            return dot(q.value, q.value);
        }

        /// <summary>Returns a normalized version of a quaternion q by scaling it by 1 / length(q).</summary>
        /// <param name="q">The quaternion to normalize.</param>
        /// <returns>The normalized quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion normalize(in quaternion q)
        {
            float4 x = q.value;
            return quaternion(rsqrt(dot(x, x)) * x);
        }

        /// <summary>
        /// Returns a safe normalized version of the q by scaling it by 1 / length(q).
        /// Returns the identity when 1 / length(q) does not produce a finite number.
        /// </summary>
        /// <param name="q">The quaternion to normalize.</param>
        /// <returns>The normalized quaternion or the identity quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion normalizesafe(in quaternion q)
        {
            float4 x = q.value;
            float len = dot(x, x);
            return quaternion(select(Mathematics.quaternionc.identity.value, x * rsqrt(len), len > FLT_MIN_NORMAL));
        }

        /// <summary>
        /// Returns a safe normalized version of the q by scaling it by 1 / length(q).
        /// Returns the given default value when 1 / length(q) does not produce a finite number.
        /// </summary>
        /// <param name="q">The quaternion to normalize.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>The normalized quaternion or the default value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion normalizesafe(in quaternion q, in quaternion defaultvalue)
        {
            float4 x = q.value;
            float len = dot(x, x);
            return quaternion(select(defaultvalue.value, x * rsqrt(len), len > FLT_MIN_NORMAL));
        }

        /// <summary>Returns the natural exponent of a quaternion. Assumes w is zero.</summary>
        /// <param name="q">The quaternion with w component equal to zero.</param>
        /// <returns>The natural exponent of the input quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion unitexp(in quaternion q)
        {
            float v_rcp_len = rsqrt(dot(q.value.xyz, q.value.xyz));
            float v_len = rcp(v_rcp_len);
            float sin_v_len, cos_v_len;
            sincos(v_len, out sin_v_len, out cos_v_len);
            return quaternion(float4(q.value.xyz * v_rcp_len * sin_v_len, cos_v_len));
        }

        /// <summary>Returns the natural exponent of a quaternion.</summary>
        /// <param name="q">The quaternion.</param>
        /// <returns>The natural exponent of the input quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion exp(in quaternion q)
        {
            float v_rcp_len = rsqrt(dot(q.value.xyz, q.value.xyz));
            float v_len = rcp(v_rcp_len);
            float sin_v_len, cos_v_len;
            sincos(v_len, out sin_v_len, out cos_v_len);
            return quaternion(float4(q.value.xyz * v_rcp_len * sin_v_len, cos_v_len) * exp(q.value.w));
        }

        /// <summary>Returns the natural logarithm of a unit length quaternion.</summary>
        /// <param name="q">The unit length quaternion.</param>
        /// <returns>The natural logarithm of the unit length quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion unitlog(in quaternion q)
        {
            float w = clamp(q.value.w, -1.0f, 1.0f);
            float s = acos(w) * rsqrt(1.0f - w*w);
            return quaternion(float4(q.value.xyz * s, 0.0f));
        }

        /// <summary>Returns the natural logarithm of a quaternion.</summary>
        /// <param name="q">The quaternion.</param>
        /// <returns>The natural logarithm of the input quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion log(in quaternion q)
        {
            float v_len_sq = dot(q.value.xyz, q.value.xyz);
            float q_len_sq = v_len_sq + q.value.w*q.value.w;

            float s = acos(clamp(q.value.w * rsqrt(q_len_sq), -1.0f, 1.0f)) * rsqrt(v_len_sq);
            return quaternion(float4(q.value.xyz * s, 0.5f * log(q_len_sq)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathQuaternionMul(in quaternion a, in quaternion b, out quaternion res);

        /// <summary>Returns the result of transforming the quaternion b by the quaternion a.</summary>
        /// <param name="a">The quaternion on the left.</param>
        /// <param name="b">The quaternion on the right.</param>
        /// <returns>The result of transforming quaternion b by the quaternion a.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion mul(in quaternion a, in quaternion b)
        {
        #if ENABLE_IL2CPP
            if( !IsBurstEnabled() )
            {
                vecILMathQuaternionMul(in a, in b, out var res);
                return res;
            }
        #endif
            return quaternion(a.value.wwww * b.value + (a.value.xyzx * b.value.wwwx + a.value.yzxy * b.value.zxyy) * float4(1.0f, 1.0f, 1.0f, -1.0f) - a.value.zxyz * b.value.yzxz);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathQuaternionMulFloat3a(in quaternion q, in float3a v, out float3a res);

        /// <summary>Returns the result of transforming a vector by a quaternion.</summary>
        /// <param name="q">The quaternion transformation.</param>
        /// <param name="v">The vector to transform.</param>
        /// <returns>The transformation of vector v by quaternion q.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a mul(in quaternion q, in float3a v)
        {
        #if ENABLE_IL2CPP
            if( !IsBurstEnabled() )
            {
                vecILMathQuaternionMulFloat3a(in q, in v, out var res);
                return res;
            }
        #endif
            float3a t = 2 * cross(q.value.xyza, v);
            return v + q.value.w * t + cross(q.value.xyza, t);
        }

        /// <summary>Returns the result of rotating a vector by a unit quaternion.</summary>
        /// <param name="q">The quaternion rotation.</param>
        /// <param name="v">The vector to rotate.</param>
        /// <returns>The rotation of vector v by quaternion q.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a rotate(in quaternion q, in float3a v)
        {
            return mul(q, v);
        }

        /// <summary>Returns the result of a normalized linear interpolation between two quaternions q1 and a2 using an interpolation parameter t.</summary>
        /// <remarks>
        /// Prefer to use this over slerp() when you know the distance between q1 and q2 is small. This can be much
        /// higher performance due to avoiding trigonometric function evaluations that occur in slerp().
        /// </remarks>
        /// <param name="q1">The first quaternion.</param>
        /// <param name="q2">The second quaternion.</param>
        /// <param name="t">The interpolation parameter.</param>
        /// <returns>The normalized linear interpolation of two quaternions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion nlerp(quaternion q1, quaternion q2, float t)
        {
            return normalize(q1.value + t * (chgsign(q2.value, dot(q1, q2)) - q1.value));
        }

        /// <summary>Returns the result of a spherical interpolation between two quaternions q1 and a2 using an interpolation parameter t.</summary>
        /// <param name="q1">The first quaternion.</param>
        /// <param name="q2">The second quaternion.</param>
        /// <param name="t">The interpolation parameter.</param>
        /// <returns>The spherical linear interpolation of two quaternions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion slerp(in quaternion q1, quaternion q2, float t)
        {
            float dt = dot(q1, q2);
            if (dt < 0.0f)
            {
                dt = -dt;
                q2.value = -q2.value;
            }

            if (dt < 0.9995f)
            {
                float angle = acos(dt);
                float s = rsqrt(1.0f - dt * dt);    // 1.0f / sin(angle)
                float w1 = sin(angle * (1.0f - t)) * s;
                float w2 = sin(angle * t) * s;
                return quaternion(q1.value * w1 + q2.value * w2);
            }
            else
            {
                // if the angle is small, use linear interpolation
                return nlerp(q1, q2, t);
            }
        }

        /// <summary>Returns the angle in radians between two unit quaternions.</summary>
        /// <param name="q1">The first quaternion.</param>
        /// <param name="q2">The second quaternion.</param>
        /// <returns>The angle between two unit quaternions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float angle(quaternion q1, quaternion q2)
        {
            float diff = asin(length(normalize(mul(conjugate(q1), q2)).value.xyza));
            return diff + diff;
        }

        /// <summary>
        /// Extracts the rotation from a matrix.
        /// </summary>
        /// <remarks>This method supports any type of rotation matrix: if the matrix has a non uniform scale you should use this method.</remarks>
        /// <param name="m">Matrix to extract rotation from</param>
        /// <returns>Extracted rotation</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion rotation(float3ax3 m)
        {
            float det = determinant(m);
            if (abs(1f - det) < svd.k_EpsilonDeterminant)
                return quaternion(m);

            if (abs(det) > svd.k_EpsilonDeterminant)
            {
                float3ax3 tmp = mulScale(m, rsqrt(float3(lengthsq(m.c0), lengthsq(m.c1), lengthsq(m.c2))));
                if (abs(1f - determinant(tmp)) < svd.k_EpsilonDeterminant)
                    return quaternion(tmp);
            }

            return svd.svdRotation(m);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float3ax3 adj(float3ax3 m, out float det)
        {
            float3ax3 adjT;
            adjT.c0 = cross(m.c1, m.c2);
            adjT.c1 = cross(m.c2, m.c0);
            adjT.c2 = cross(m.c0, m.c1);
            det = dot(m.c0, adjT.c0);

            return transpose(adjT);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool adjInverse(float3ax3 m, out float3ax3 i, float epsilon = svd.k_EpsilonNormal)
        {
            i = adj(m, out float det);
            bool c = abs(det) > epsilon;
            var detInv = select(float3c.one, rcp(det), c);
            i = scaleMul(detInv, i);
            return c;
        }

        /// <summary>Returns a uint hash code of a quaternion.</summary>
        /// <param name="q">The quaternion to hash.</param>
        /// <returns>The hash code for the input quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(quaternion q)
        {
            return hash(q.value);
        }

        /// <summary>
        /// Returns a uint4 vector hash code of a quaternion.
        /// When multiple elements are to be hashes together, it can more efficient to calculate and combine wide hash
        /// that are only reduced to a narrow uint hash at the very end instead of at every step.
        /// </summary>
        /// <param name="q">The quaternion to hash.</param>
        /// <returns>The uint4 vector hash code of the input quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 hashwide(quaternion q)
        {
            return hashwide(q.value);
        }

        /// <summary>
        /// Transforms the forward vector by a quaternion.
        /// </summary>
        /// <param name="q">The quaternion transformation.</param>
        /// <returns>The forward vector transformed by the input quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a forward(quaternion q) { return mul(q, float3a(0, 0, 1)); }  // for compatibility
    }
}
