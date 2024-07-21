using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
    public partial struct float2x2
    {
        /// <summary>
        /// Computes a float2x2 matrix representing a counter-clockwise rotation by an angle in radians.
        /// </summary>
        /// <remarks>
        /// A positive rotation angle will produce a counter-clockwise rotation and a negative rotation angle will
        /// produce a clockwise rotation.
        /// </remarks>
        /// <param name="angle">Rotation angle in radians.</param>
        /// <returns>Returns the 2x2 rotation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x2 Rotate(float angle)
        {
            float s, c;
            sincos(angle, out s, out c);
            return float2x2(c, -s,
                            s,  c);
        }

        /// <summary>Returns a float2x2 matrix representing a uniform scaling of both axes by s.</summary>
        /// <param name="s">The scaling factor.</param>
        /// <returns>The float2x2 matrix representing uniform scale by s.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x2 Scale(float s)
        {
            return float2x2(s,    0.0f,
                            0.0f, s);
        }

        /// <summary>Returns a float2x2 matrix representing a non-uniform axis scaling by x and y.</summary>
        /// <param name="x">The x-axis scaling factor.</param>
        /// <param name="y">The y-axis scaling factor.</param>
        /// <returns>The float2x2 matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x2 Scale(float x, float y)
        {
            return float2x2(x,    0.0f,
                            0.0f, y);
        }

        /// <summary>Returns a float2x2 matrix representing a non-uniform axis scaling by the components of the float2 vector v.</summary>
        /// <param name="v">The float2 containing the x and y axis scaling factors.</param>
        /// <returns>The float2x2 matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2x2 Scale(float2 v)
        {
            return Scale(v.x, v.y);
        }
    }

    public partial struct float3x3
    {
        /// <summary>
        /// Constructs a float3x3 from the upper left 3x3 of a float4x4.
        /// </summary>1
        /// <param name="f4x4"><see cref="float4x4"/> to extract a float3x3 from.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3x3(in float4x4 f4x4)
        {
            c0 = f4x4.c0.xyz;
            c1 = f4x4.c1.xyz;
            c2 = f4x4.c2.xyz;
        }

        /// <summary>Constructs a float3x3 matrix from a unit quaternion.</summary>
        /// <param name="q">The quaternion rotation.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3x3(in quaternion q)
        {
            float4 v = q.value;
            float4 v2 = v + v;

            uint3 npn = uint3(0x80000000, 0x00000000, 0x80000000);
            uint3 nnp = uint3(0x80000000, 0x80000000, 0x00000000);
            uint3 pnn = uint3(0x00000000, 0x80000000, 0x80000000);
            c0 = v2.y * asfloat(asuint(v.yxw) ^ npn) - v2.z * asfloat(asuint(v.zwx) ^ pnn) + float3(1, 0, 0);
            c1 = v2.z * asfloat(asuint(v.wzy) ^ nnp) - v2.x * asfloat(asuint(v.yxw) ^ npn) + float3(0, 1, 0);
            c2 = v2.x * asfloat(asuint(v.zwx) ^ pnn) - v2.y * asfloat(asuint(v.wzy) ^ nnp) + float3(0, 0, 1);
        }

        /// <summary>
        /// Returns a float3x3 matrix representing a rotation around a unit axis by an angle in radians.
        /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="axis">The rotation axis.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The float3x3 matrix representing the rotation around an axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 AxisAngle(in float3 axis, float angle)
        {
            float sina, cosa;
            math.sincos(angle, out sina, out cosa);

            float3 u = axis;
            float3 u_yzx = u.yzx;
            float3 u_zxy = u.zxy;
            float3 u_inv_cosa = u - u * cosa;  // u * (1.0f - cosa);
            float4 t = float4(u * sina, cosa);

            uint3 ppn = uint3(0x00000000, 0x00000000, 0x80000000);
            uint3 npp = uint3(0x80000000, 0x00000000, 0x00000000);
            uint3 pnp = uint3(0x00000000, 0x80000000, 0x00000000);

            return float3x3(
                u.x * u_inv_cosa + asfloat(asuint(t.wzy) ^ ppn),
                u.y * u_inv_cosa + asfloat(asuint(t.zwx) ^ npp),
                u.z * u_inv_cosa + asfloat(asuint(t.yxw) ^ pnp)
                );
            /*
            return float3x3(
                cosa + u.x * u.x * (1.0f - cosa),       u.y * u.x * (1.0f - cosa) - u.z * sina, u.z * u.x * (1.0f - cosa) + u.y * sina,
                u.x * u.y * (1.0f - cosa) + u.z * sina, cosa + u.y * u.y * (1.0f - cosa),       u.y * u.z * (1.0f - cosa) - u.x * sina,
                u.x * u.z * (1.0f - cosa) - u.y * sina, u.y * u.z * (1.0f - cosa) + u.x * sina, cosa + u.z * u.z * (1.0f - cosa)
                );
                */
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerXYZ(in float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateY(xyz.y), rotateX(xyz.x)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,  c.z * s.x * s.y - c.x * s.z,    c.x * c.z * s.y + s.x * s.z,
                c.y * s.z,  c.x * c.z + s.x * s.y * s.z,    c.x * s.y * s.z - c.z * s.x,
                -s.y,       c.y * s.x,                      c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerXZY(in float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateZ(xyz.z), rotateX(xyz.x))); }
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,  s.x * s.y - c.x * c.y * s.z,    c.x * s.y + c.y * s.x * s.z,
                s.z,        c.x * c.z,                      -c.z * s.x,
                -c.z * s.y, c.y * s.x + c.x * s.y * s.z,    c.x * c.y - s.x * s.y * s.z
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerYXZ(in float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateX(xyz.x), rotateY(xyz.y)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z - s.x * s.y * s.z,    -c.x * s.z, c.z * s.y + c.y * s.x * s.z,
                c.z * s.x * s.y + c.y * s.z,    c.x * c.z,  s.y * s.z - c.y * c.z * s.x,
                -c.x * s.y,                     s.x,        c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerYZX(in float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateZ(xyz.z), rotateY(xyz.y)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,                      -s.z,       c.z * s.y,
                s.x * s.y + c.x * c.y * s.z,    c.x * c.z,  c.x * s.y * s.z - c.y * s.x,
                c.y * s.x * s.z - c.x * s.y,    c.z * s.x,  c.x * c.y + s.x * s.y * s.z
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerZXY(in float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateX(xyz.x), rotateZ(xyz.z)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z + s.x * s.y * s.z,    c.z * s.x * s.y - c.y * s.z,    c.x * s.y,
                c.x * s.z,                      c.x * c.z,                      -s.x,
                c.y * s.x * s.z - c.z * s.y,    c.y * c.z * s.x + s.y * s.z,    c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerZYX(in float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateY(xyz.y), rotateZ(xyz.z)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,                      -c.y * s.z,                     s.y,
                c.z * s.x * s.y + c.x * s.z,    c.x * c.z - s.x * s.y * s.z,    -c.y * s.x,
                s.x * s.z - c.x * c.z * s.y,    c.z * s.x + c.x * s.y * s.z,    c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerXYZ(float x, float y, float z) { return EulerXYZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerXZY(float x, float y, float z) { return EulerXZY(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerYXZ(float x, float y, float z) { return EulerYXZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerYZX(float x, float y, float z) { return EulerYZX(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerZXY(float x, float y, float z) { return EulerZXY(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 EulerZYX(float x, float y, float z) { return EulerZYX(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in the given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 Euler(in float3 xyz, RotationOrder order = RotationOrder.Default)
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
                    return float3x3.identity;
            }
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in the given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 Euler(float x, float y, float z, RotationOrder order = RotationOrder.Default)
        {
            return Euler(float3(x, y, z), order);
        }

        /// <summary>Returns a float3x3 matrix that rotates around the x-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the x-axis towards the origin in radians.</param>
        /// <returns>The float3x3 rotation matrix representing a rotation around the x-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 RotateX(float angle)
        {
            // {{1, 0, 0}, {0, c_0, -s_0}, {0, s_0, c_0}}
            float s, c;
            sincos(angle, out s, out c);
            return float3x3(1.0f, 0.0f, 0.0f,
                            0.0f, c,    -s,
                            0.0f, s,    c);
        }

        /// <summary>Returns a float3x3 matrix that rotates around the y-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the y-axis towards the origin in radians.</param>
        /// <returns>The float3x3 rotation matrix representing a rotation around the y-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 RotateY(float angle)
        {
            // {{c_1, 0, s_1}, {0, 1, 0}, {-s_1, 0, c_1}}
            float s, c;
            sincos(angle, out s, out c);
            return float3x3(c,    0.0f, s,
                            0.0f, 1.0f, 0.0f,
                            -s,   0.0f, c);
        }

        /// <summary>Returns a float3x3 matrix that rotates around the z-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the z-axis towards the origin in radians.</param>
        /// <returns>The float3x3 rotation matrix representing a rotation around the z-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 RotateZ(float angle)
        {
            // {{c_2, -s_2, 0}, {s_2, c_2, 0}, {0, 0, 1}}
            float s, c;
            sincos(angle, out s, out c);
            return float3x3(c,    -s,   0.0f,
                            s,    c,    0.0f,
                            0.0f, 0.0f, 1.0f);
        }

        /// <summary>Returns a float3x3 matrix representing a uniform scaling of all axes by s.</summary>
        /// <param name="s">The uniform scaling factor.</param>
        /// <returns>The float3x3 matrix representing a uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 Scale(float s)
        {
            return float3x3(s,    0.0f, 0.0f,
                            0.0f, s,    0.0f,
                            0.0f, 0.0f, s);
        }

        /// <summary>Returns a float3x3 matrix representing a non-uniform axis scaling by x, y and z.</summary>
        /// <param name="x">The x-axis scaling factor.</param>
        /// <param name="y">The y-axis scaling factor.</param>
        /// <param name="z">The z-axis scaling factor.</param>
        /// <returns>The float3x3 rotation matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 Scale(float x, float y, float z)
        {
            return float3x3(x,    0.0f, 0.0f,
                            0.0f, y,    0.0f,
                            0.0f, 0.0f, z);
        }

        /// <summary>Returns a float3x3 matrix representing a non-uniform axis scaling by the components of the float3 vector v.</summary>
        /// <param name="v">The vector containing non-uniform scaling factors.</param>
        /// <returns>The float3x3 rotation matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 Scale(in float3 v)
        {
            return Scale(v.x, v.y, v.z);
        }

        /// <summary>
        /// Returns a float3x3 view rotation matrix given a unit length forward vector and a unit length up vector.
        /// The two input vectors are assumed to be unit length and not collinear.
        /// If these assumptions are not met use float3x3.LookRotationSafe instead.
        /// </summary>
        /// <param name="forward">The forward vector to align the center of view with.</param>
        /// <param name="up">The up vector to point top of view toward.</param>
        /// <returns>The float3x3 view rotation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 LookRotation(in float3a forward, in float3a up)
        {
            var t = normalize(cross(up, forward));
            return float3x3(t, cross(forward, t), forward);
        }

        /// <summary>
        /// Returns a float3x3 view rotation matrix given a forward vector and an up vector.
        /// The two input vectors are not assumed to be unit length.
        /// If the magnitude of either of the vectors is so extreme that the calculation cannot be carried out reliably or the vectors are collinear,
        /// the identity will be returned instead.
        /// </summary>
        /// <param name="forward">The forward vector to align the center of view with.</param>
        /// <param name="up">The up vector to point top of view toward.</param>
        /// <returns>The float3x3 view rotation matrix or the identity matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x3 LookRotationSafe(float3a forward, float3a up)
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
            return float3x3(
                select(float3c.right, t, accept),
                select(float3c.up, cross(forward, t), accept),
                select(float3c.forward, forward, accept));
        }

        /// <summary>
        /// Converts a float4x4 to a float3x3.
        /// </summary>
        /// <param name="f4x4">The float4x4 to convert to a float3x3.</param>
        /// <returns>The float3x3 constructed from the upper left 3x3 of the input float4x4 matrix.</returns>
        public static explicit operator float3x3(in float4x4 f4x4) => new float3x3(f4x4);
    }

    public ref partial struct float3ax3
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3ax3FromQuat(in quaternion q, out float3a res0, out float3a res1, out float3a res2);

        /// <summary>
        /// Constructs a float3x3 from the upper left 3x3 of a float4x4.
        /// </summary>1
        /// <param name="f4x4"><see cref="float4x4"/> to extract a float3x3 from.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3ax3(in float4x4 f4x4)
        {
            c0 = f4x4.c0.xyz;
            c1 = f4x4.c1.xyz;
            c2 = f4x4.c2.xyz;
        }

        /// <summary>Constructs a float3x3 matrix from a unit quaternion.</summary>
        /// <param name="q">The quaternion rotation.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3ax3(in quaternion q)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3ax3FromQuat(in q, out c0, out c1, out c2);
        #else
            float4 v = q.value;
            float4 v2 = v + v;

            uint3 npn = uint3(0x80000000, 0x00000000, 0x80000000);
            uint3 nnp = uint3(0x80000000, 0x80000000, 0x00000000);
            uint3 pnn = uint3(0x00000000, 0x80000000, 0x80000000);
            c0 = v2.y * asfloat(asuint(v.yxw) ^ npn) - v2.z * asfloat(asuint(v.zwx) ^ pnn) + float3(1, 0, 0);
            c1 = v2.z * asfloat(asuint(v.wzy) ^ nnp) - v2.x * asfloat(asuint(v.yxw) ^ npn) + float3(0, 1, 0);
            c2 = v2.x * asfloat(asuint(v.zwx) ^ pnn) - v2.y * asfloat(asuint(v.wzy) ^ nnp) + float3(0, 0, 1);
        #endif
        }

        /// <summary>
        /// Returns a float3x3 matrix representing a rotation around a unit axis by an angle in radians.
        /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="axis">The rotation axis.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The float3x3 matrix representing the rotation around an axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 AxisAngle(in float3a axis, float angle)
        {
            float sina, cosa;
            math.sincos(angle, out sina, out cosa);

            float3 u = axis;
            float3 u_yzx = u.yzx;
            float3 u_zxy = u.zxy;
            float3 u_inv_cosa = u - u * cosa;  // u * (1.0f - cosa);
            float4 t = float4(u * sina, cosa);

            uint3 ppn = uint3(0x00000000, 0x00000000, 0x80000000);
            uint3 npp = uint3(0x80000000, 0x00000000, 0x00000000);
            uint3 pnp = uint3(0x00000000, 0x80000000, 0x00000000);

            return float3x3(
                u.x * u_inv_cosa + asfloat(asuint(t.wzy) ^ ppn),
                u.y * u_inv_cosa + asfloat(asuint(t.zwx) ^ npp),
                u.z * u_inv_cosa + asfloat(asuint(t.yxw) ^ pnp)
                );
            /*
            return float3x3(
                cosa + u.x * u.x * (1.0f - cosa),       u.y * u.x * (1.0f - cosa) - u.z * sina, u.z * u.x * (1.0f - cosa) + u.y * sina,
                u.x * u.y * (1.0f - cosa) + u.z * sina, cosa + u.y * u.y * (1.0f - cosa),       u.y * u.z * (1.0f - cosa) - u.x * sina,
                u.x * u.z * (1.0f - cosa) - u.y * sina, u.y * u.z * (1.0f - cosa) + u.x * sina, cosa + u.z * u.z * (1.0f - cosa)
                );
                */
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerXYZ(in float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateY(xyz.y), rotateX(xyz.x)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,  c.z * s.x * s.y - c.x * s.z,    c.x * c.z * s.y + s.x * s.z,
                c.y * s.z,  c.x * c.z + s.x * s.y * s.z,    c.x * s.y * s.z - c.z * s.x,
                -s.y,       c.y * s.x,                      c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerXZY(in float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateZ(xyz.z), rotateX(xyz.x))); }
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,  s.x * s.y - c.x * c.y * s.z,    c.x * s.y + c.y * s.x * s.z,
                s.z,        c.x * c.z,                      -c.z * s.x,
                -c.z * s.y, c.y * s.x + c.x * s.y * s.z,    c.x * c.y - s.x * s.y * s.z
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerYXZ(in float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateX(xyz.x), rotateY(xyz.y)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z - s.x * s.y * s.z,    -c.x * s.z, c.z * s.y + c.y * s.x * s.z,
                c.z * s.x * s.y + c.y * s.z,    c.x * c.z,  s.y * s.z - c.y * c.z * s.x,
                -c.x * s.y,                     s.x,        c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerYZX(in float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateZ(xyz.z), rotateY(xyz.y)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,                      -s.z,       c.z * s.y,
                s.x * s.y + c.x * c.y * s.z,    c.x * c.z,  c.x * s.y * s.z - c.y * s.x,
                c.y * s.x * s.z - c.x * s.y,    c.z * s.x,  c.x * c.y + s.x * s.y * s.z
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerZXY(in float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateX(xyz.x), rotateZ(xyz.z)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z + s.x * s.y * s.z,    c.z * s.x * s.y - c.y * s.z,    c.x * s.y,
                c.x * s.z,                      c.x * c.z,                      -s.x,
                c.y * s.x * s.z - c.z * s.y,    c.y * c.z * s.x + s.y * s.z,    c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerZYX(in float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateY(xyz.y), rotateZ(xyz.z)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float3x3(
                c.y * c.z,                      -c.y * s.z,                     s.y,
                c.z * s.x * s.y + c.x * s.z,    c.x * c.z - s.x * s.y * s.z,    -c.y * s.x,
                s.x * s.z - c.x * c.z * s.y,    c.z * s.x + c.x * s.y * s.z,    c.x * c.y
                );
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerXYZ(float x, float y, float z) { return EulerXYZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerXZY(float x, float y, float z) { return EulerXZY(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerYXZ(float x, float y, float z) { return EulerYXZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerYZX(float x, float y, float z) { return EulerYZX(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerZXY(float x, float y, float z) { return EulerZXY(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 EulerZYX(float x, float y, float z) { return EulerZYX(float3(x, y, z)); }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in the given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 Euler(in float3 xyz, RotationOrder order = RotationOrder.Default)
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
                    return float3x3.identity;
            }
        }

        /// <summary>
        /// Returns a float3x3 rotation matrix constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The float3x3 rotation matrix representing the rotation by Euler angles in the given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 Euler(float x, float y, float z, RotationOrder order = RotationOrder.Default)
        {
            return Euler(float3(x, y, z), order);
        }

        /// <summary>Returns a float3x3 matrix that rotates around the x-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the x-axis towards the origin in radians.</param>
        /// <returns>The float3x3 rotation matrix representing a rotation around the x-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 RotateX(float angle)
        {
            // {{1, 0, 0}, {0, c_0, -s_0}, {0, s_0, c_0}}
            float s, c;
            sincos(angle, out s, out c);
            return float3x3(1.0f, 0.0f, 0.0f,
                            0.0f, c,    -s,
                            0.0f, s,    c);
        }

        /// <summary>Returns a float3x3 matrix that rotates around the y-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the y-axis towards the origin in radians.</param>
        /// <returns>The float3x3 rotation matrix representing a rotation around the y-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 RotateY(float angle)
        {
            // {{c_1, 0, s_1}, {0, 1, 0}, {-s_1, 0, c_1}}
            float s, c;
            sincos(angle, out s, out c);
            return float3x3(c,    0.0f, s,
                            0.0f, 1.0f, 0.0f,
                            -s,   0.0f, c);
        }

        /// <summary>Returns a float3x3 matrix that rotates around the z-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the z-axis towards the origin in radians.</param>
        /// <returns>The float3x3 rotation matrix representing a rotation around the z-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 RotateZ(float angle)
        {
            // {{c_2, -s_2, 0}, {s_2, c_2, 0}, {0, 0, 1}}
            float s, c;
            sincos(angle, out s, out c);
            return float3x3(c,    -s,   0.0f,
                            s,    c,    0.0f,
                            0.0f, 0.0f, 1.0f);
        }

        /// <summary>Returns a float3x3 matrix representing a uniform scaling of all axes by s.</summary>
        /// <param name="s">The uniform scaling factor.</param>
        /// <returns>The float3x3 matrix representing a uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 Scale(float s)
        {
            return float3x3(s,    0.0f, 0.0f,
                            0.0f, s,    0.0f,
                            0.0f, 0.0f, s);
        }

        /// <summary>Returns a float3x3 matrix representing a non-uniform axis scaling by x, y and z.</summary>
        /// <param name="x">The x-axis scaling factor.</param>
        /// <param name="y">The y-axis scaling factor.</param>
        /// <param name="z">The z-axis scaling factor.</param>
        /// <returns>The float3x3 rotation matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 Scale(float x, float y, float z)
        {
            return float3x3(x,    0.0f, 0.0f,
                            0.0f, y,    0.0f,
                            0.0f, 0.0f, z);
        }

        /// <summary>Returns a float3x3 matrix representing a non-uniform axis scaling by the components of the float3 vector v.</summary>
        /// <param name="v">The vector containing non-uniform scaling factors.</param>
        /// <returns>The float3x3 rotation matrix representing a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 Scale(in float3 v)
        {
            return Scale(v.x, v.y, v.z);
        }

        /// <summary>
        /// Returns a float3x3 view rotation matrix given a unit length forward vector and a unit length up vector.
        /// The two input vectors are assumed to be unit length and not collinear.
        /// If these assumptions are not met use float3x3.LookRotationSafe instead.
        /// </summary>
        /// <param name="forward">The forward vector to align the center of view with.</param>
        /// <param name="up">The up vector to point top of view toward.</param>
        /// <returns>The float3x3 view rotation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 LookRotation(in float3a forward, in float3a up)
        {
            float3a t = normalize(cross(up, forward));
            return float3ax3(t, cross(forward, t), forward);
        }

        /// <summary>
        /// Returns a float3x3 view rotation matrix given a forward vector and an up vector.
        /// The two input vectors are not assumed to be unit length.
        /// If the magnitude of either of the vectors is so extreme that the calculation cannot be carried out reliably or the vectors are collinear,
        /// the identity will be returned instead.
        /// </summary>
        /// <param name="forward">The forward vector to align the center of view with.</param>
        /// <param name="up">The up vector to point top of view toward.</param>
        /// <returns>The float3x3 view rotation matrix or the identity matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 LookRotationSafe(float3a forward, float3a up)
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
            return float3x3(
                select(float3c.right,   t,                  accept),
                select(float3c.up,      cross(forward, t),  accept),
                select(float3c.forward, forward,            accept));
        }

        /// <summary>
        /// Converts a float4x4 to a float3x3.
        /// </summary>
        /// <param name="f4x4">The float4x4 to convert to a float3x3.</param>
        /// <returns>The float3x3 constructed from the upper left 3x3 of the input float4x4 matrix.</returns>
        public static explicit operator float3ax3(in float4x4 f4x4) => new float3ax3(f4x4);
    }

    public partial struct float4x4
    {
        /// <summary>Constructs a float4x4 from a float3x3 rotation matrix and a float3 translation vector.</summary>
        /// <param name="rotation">The float3x3 rotation matrix.</param>
        /// <param name="translation">The translation vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x4(in float3ax3 rotation, in float3a translation)
        {
            c0 = float4(rotation.c0, 0.0f);
            c1 = float4(rotation.c1, 0.0f);
            c2 = float4(rotation.c2, 0.0f);
            c3 = float4(translation, 1.0f);
        }

        /// <summary>Constructs a float4x4 from a quaternion and a float3 translation vector.</summary>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <param name="translation">The translation vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x4(in quaternion rotation, in float3a translation)
        {
            float3ax3 rot = float3ax3(rotation);
            c0 = float4(rot.c0, 0.0f);
            c1 = float4(rot.c1, 0.0f);
            c2 = float4(rot.c2, 0.0f);
            c3 = float4(translation, 1.0f);
        }

        /// <summary>Constructs a float4x4 from a RigidTransform.</summary>
        /// <param name="transform">The RigidTransform.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x4(in RigidTransform transform)
        {
            float3ax3 rot = float3ax3(transform.rot);
            c0 = float4(rot.c0, 0.0f);
            c1 = float4(rot.c1, 0.0f);
            c2 = float4(rot.c2, 0.0f);
            c3 = float4(transform.pos, 1.0f);
        }

        /// <summary>
        /// Returns a float4x4 matrix representing a rotation around a unit axis by an angle in radians.
        /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The float4x4 matrix representing the rotation about an axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 AxisAngle(in float3a axis, float angle)
        {
            float sina, cosa;
            math.sincos(angle, out sina, out cosa);

            float4 u = float4(axis, 0.0f);
            float4 u_yzx = u.yzxx;
            float4 u_zxy = u.zxyx;
            float4 u_inv_cosa = u - u * cosa;  // u * (1.0f - cosa);
            float4 t = float4(u.xyz * sina, cosa);

            uint4 ppnp = uint4(0x00000000, 0x00000000, 0x80000000, 0x00000000);
            uint4 nppp = uint4(0x80000000, 0x00000000, 0x00000000, 0x00000000);
            uint4 pnpp = uint4(0x00000000, 0x80000000, 0x00000000, 0x00000000);
            uint4 mask = uint4(0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x00000000);

            return float4x4(
                u.x * u_inv_cosa + asfloat((asuint(t.wzyx) ^ ppnp) & mask),
                u.y * u_inv_cosa + asfloat((asuint(t.zwxx) ^ nppp) & mask),
                u.z * u_inv_cosa + asfloat((asuint(t.yxwx) ^ pnpp) & mask),
                float4(0.0f, 0.0f, 0.0f, 1.0f)
                );

        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerXYZ(in float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateY(xyz.y), rotateX(xyz.x)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z,  c.z * s.x * s.y - c.x * s.z,    c.x * c.z * s.y + s.x * s.z,    0.0f,
                c.y * s.z,  c.x * c.z + s.x * s.y * s.z,    c.x * s.y * s.z - c.z * s.x,    0.0f,
                -s.y,       c.y * s.x,                      c.x * c.y,                      0.0f,
                0.0f,       0.0f,                           0.0f,                           1.0f
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerXZY(in float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateZ(xyz.z), rotateX(xyz.x))); }
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z,  s.x * s.y - c.x * c.y * s.z,    c.x * s.y + c.y * s.x * s.z,    0.0f,
                s.z,        c.x * c.z,                      -c.z * s.x,                     0.0f,
                -c.z * s.y, c.y * s.x + c.x * s.y * s.z,    c.x * c.y - s.x * s.y * s.z,    0.0f,
                0.0f,       0.0f,                           0.0f,                           1.0f
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerYXZ(in float3 xyz)
        {
            // return mul(rotateZ(xyz.z), mul(rotateX(xyz.x), rotateY(xyz.y)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z - s.x * s.y * s.z,    -c.x * s.z, c.z * s.y + c.y * s.x * s.z,    0.0f,
                c.z * s.x * s.y + c.y * s.z,    c.x * c.z,  s.y * s.z - c.y * c.z * s.x,    0.0f,
                -c.x * s.y,                     s.x,        c.x * c.y,                      0.0f,
                0.0f,                           0.0f,       0.0f,                           1.0f
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerYZX(in float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateZ(xyz.z), rotateY(xyz.y)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z,                      -s.z,       c.z * s.y,                      0.0f,
                s.x * s.y + c.x * c.y * s.z,    c.x * c.z,  c.x * s.y * s.z - c.y * s.x,    0.0f,
                c.y * s.x * s.z - c.x * s.y,    c.z * s.x,  c.x * c.y + s.x * s.y * s.z,    0.0f,
                0.0f,                           0.0f,       0.0f,                           1.0f
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerZXY(in float3 xyz)
        {
            // return mul(rotateY(xyz.y), mul(rotateX(xyz.x), rotateZ(xyz.z)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z + s.x * s.y * s.z,    c.z * s.x * s.y - c.y * s.z,    c.x * s.y,  0.0f,
                c.x * s.z,                      c.x * c.z,                      -s.x,       0.0f,
                c.y * s.x * s.z - c.z * s.y,    c.y * c.z * s.x + s.y * s.z,    c.x * c.y,  0.0f,
                0.0f,                           0.0f,                           0.0f,       1.0f
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerZYX(in float3 xyz)
        {
            // return mul(rotateX(xyz.x), mul(rotateY(xyz.y), rotateZ(xyz.z)));
            float3 s, c;
            sincos(xyz, out s, out c);
            return float4x4(
                c.y * c.z,                      -c.y * s.z,                     s.y,        0.0f,
                c.z * s.x * s.y + c.x * s.z,    c.x * c.z - s.x * s.y * s.z,    -c.y * s.x, 0.0f,
                s.x * s.z - c.x * c.z * s.y,    c.z * s.x + c.x * s.y * s.z,    c.x * c.y,  0.0f,
                0.0f,                           0.0f,                           0.0f,       1.0f
                );
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in x-y-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerXYZ(float x, float y, float z) { return EulerXYZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in x-z-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerXZY(float x, float y, float z) { return EulerXZY(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in y-x-z order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerYXZ(float x, float y, float z) { return EulerYXZ(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in y-z-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerYZX(float x, float y, float z) { return EulerYZX(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// This is the default order rotation order in Unity.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in z-x-y order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerZXY(float x, float y, float z) { return EulerZXY(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in z-y-x order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 EulerZYX(float x, float y, float z) { return EulerZYX(float3(x, y, z)); }

        /// <summary>
        /// Returns a float4x4 constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Euler(in float3 xyz, RotationOrder order = RotationOrder.Default)
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
                    return float4x4c.identity;
            }
        }

        /// <summary>
        /// Returns a float4x4 rotation matrix constructed by first performing 3 rotations around the principal axes in a given order.
        /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
        /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
        /// Euler rotation constructors such as EulerZXY(...).
        /// </summary>
        /// <param name="x">The rotation angle around the x-axis in radians.</param>
        /// <param name="y">The rotation angle around the y-axis in radians.</param>
        /// <param name="z">The rotation angle around the z-axis in radians.</param>
        /// <param name="order">The order in which the rotations are applied.</param>
        /// <returns>The float4x4 rotation matrix of the Euler angle rotation in given order.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Euler(float x, float y, float z, RotationOrder order = RotationOrder.Default)
        {
            return Euler(float3(x, y, z), order);
        }

        /// <summary>Returns a float4x4 matrix that rotates around the x-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the x-axis towards the origin in radians.</param>
        /// <returns>The float4x4 rotation matrix that rotates around the x-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 RotateX(float angle)
        {
            // {{1, 0, 0}, {0, c_0, -s_0}, {0, s_0, c_0}}
            float s, c;
            sincos(angle, out s, out c);
            return float4x4(1.0f, 0.0f, 0.0f, 0.0f,
                            0.0f, c,    -s,   0.0f,
                            0.0f, s,    c,    0.0f,
                            0.0f, 0.0f, 0.0f, 1.0f);

        }

        /// <summary>Returns a float4x4 matrix that rotates around the y-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the y-axis towards the origin in radians.</param>
        /// <returns>The float4x4 rotation matrix that rotates around the y-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 RotateY(float angle)
        {
            // {{c_1, 0, s_1}, {0, 1, 0}, {-s_1, 0, c_1}}
            float s, c;
            sincos(angle, out s, out c);
            return float4x4(c,    0.0f, s,    0.0f,
                            0.0f, 1.0f, 0.0f, 0.0f,
                            -s,   0.0f, c,    0.0f,
                            0.0f, 0.0f, 0.0f, 1.0f);

        }

        /// <summary>Returns a float4x4 matrix that rotates around the z-axis by a given number of radians.</summary>
        /// <param name="angle">The clockwise rotation angle when looking along the z-axis towards the origin in radians.</param>
        /// <returns>The float4x4 rotation matrix that rotates around the z-axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 RotateZ(float angle)
        {
            // {{c_2, -s_2, 0}, {s_2, c_2, 0}, {0, 0, 1}}
            float s, c;
            sincos(angle, out s, out c);
            return float4x4(c,    -s,   0.0f, 0.0f,
                            s,    c,    0.0f, 0.0f,
                            0.0f, 0.0f, 1.0f, 0.0f,
                            0.0f, 0.0f, 0.0f, 1.0f);

        }

        /// <summary>Returns a float4x4 scale matrix given 3 axis scales.</summary>
        /// <param name="s">The uniform scaling factor.</param>
        /// <returns>The float4x4 matrix that represents a uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Scale(float s)
        {
            return float4x4(s,    0.0f, 0.0f, 0.0f,
                            0.0f, s,    0.0f, 0.0f,
                            0.0f, 0.0f, s,    0.0f,
                            0.0f, 0.0f, 0.0f, 1.0f);
        }

        /// <summary>Returns a float4x4 scale matrix given a float3 vector containing the 3 axis scales.</summary>
        /// <param name="x">The x-axis scaling factor.</param>
        /// <param name="y">The y-axis scaling factor.</param>
        /// <param name="z">The z-axis scaling factor.</param>
        /// <returns>The float4x4 matrix that represents a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Scale(float x, float y, float z)
        {
            return float4x4(x,    0.0f, 0.0f, 0.0f,
                            0.0f, y,    0.0f, 0.0f,
                            0.0f, 0.0f, z,    0.0f,
                            0.0f, 0.0f, 0.0f, 1.0f);
        }

        /// <summary>Returns a float4x4 scale matrix given a float3 vector containing the 3 axis scales.</summary>
        /// <param name="scales">The vector containing scale factors for each axis.</param>
        /// <returns>The float4x4 matrix that represents a non-uniform scale.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Scale(in float3 scales)
        {
            return Scale(scales.x, scales.y, scales.z);
        }

        /// <summary>Returns a float4x4 translation matrix given a float3 translation vector.</summary>
        /// <param name="vector">The translation vector.</param>
        /// <returns>The float4x4 translation matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Translate(in float3 vector)
        {
            return float4x4(float4(1.0f, 0.0f, 0.0f, 0.0f),
                            float4(0.0f, 1.0f, 0.0f, 0.0f),
                            float4(0.0f, 0.0f, 1.0f, 0.0f),
                            float4(vector.x, vector.y, vector.z, 1.0f));
        }

        /// <summary>
        /// Returns a float4x4 view matrix given an eye position, a target point and a unit length up vector.
        /// The up vector is assumed to be unit length, the eye and target points are assumed to be distinct and
        /// the vector between them is assumes to be collinear with the up vector.
        /// If these assumptions are not met use float4x4.LookRotationSafe instead.
        /// </summary>
        /// <param name="eye">The eye position.</param>
        /// <param name="target">The view target position.</param>
        /// <param name="up">The eye up direction.</param>
        /// <returns>The float4x4 view matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 LookAt(in float3a eye, in float3a target, in float3a up)
        {
            float3ax3 rot = float3ax3.LookRotation(normalize(target - eye), up);

            float4x4 matrix;
            matrix.c0 = float4(rot.c0, 0.0F);
            matrix.c1 = float4(rot.c1, 0.0F);
            matrix.c2 = float4(rot.c2, 0.0F);
            matrix.c3 = float4(eye, 1.0F);
            return matrix;
        }

        /// <summary>
        /// Returns a float4x4 centered orthographic projection matrix.
        /// </summary>
        /// <param name="width">The width of the view volume.</param>
        /// <param name="height">The height of the view volume.</param>
        /// <param name="near">The distance to the near plane.</param>
        /// <param name="far">The distance to the far plane.</param>
        /// <returns>The float4x4 centered orthographic projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 Ortho(float width, float height, float near, float far)
        {
            float rcpdx = 1.0f / width;
            float rcpdy = 1.0f / height;
            float rcpdz = 1.0f / (far - near);

            return float4x4(
                2.0f * rcpdx,   0.0f,            0.0f,           0.0f,
                0.0f,           2.0f * rcpdy,    0.0f,           0.0f,
                0.0f,           0.0f,           -2.0f * rcpdz,  -(far + near) * rcpdz,
                0.0f,           0.0f,            0.0f,           1.0f
                );
        }

        /// <summary>
        /// Returns a float4x4 off-center orthographic projection matrix.
        /// </summary>
        /// <param name="left">The minimum x-coordinate of the view volume.</param>
        /// <param name="right">The maximum x-coordinate of the view volume.</param>
        /// <param name="bottom">The minimum y-coordinate of the view volume.</param>
        /// <param name="top">The minimum y-coordinate of the view volume.</param>
        /// <param name="near">The distance to the near plane.</param>
        /// <param name="far">The distance to the far plane.</param>
        /// <returns>The float4x4 off-center orthographic projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 OrthoOffCenter(float left, float right, float bottom, float top, float near, float far)
        {
            float rcpdx = 1.0f / (right - left);
            float rcpdy = 1.0f / (top - bottom);
            float rcpdz = 1.0f / (far - near);

            return float4x4(
                2.0f * rcpdx,   0.0f,           0.0f,               -(right + left) * rcpdx,
                0.0f,           2.0f * rcpdy,   0.0f,               -(top + bottom) * rcpdy,
                0.0f,           0.0f,          -2.0f * rcpdz,       -(far + near) * rcpdz,
                0.0f,           0.0f,           0.0f,                1.0f
                );
        }

        /// <summary>
        /// Returns a float4x4 perspective projection matrix based on field of view.
        /// </summary>
        /// <param name="verticalFov">Vertical Field of view in radians.</param>
        /// <param name="aspect">X:Y aspect ratio.</param>
        /// <param name="near">Distance to near plane. Must be greater than zero.</param>
        /// <param name="far">Distance to far plane. Must be greater than zero.</param>
        /// <returns>The float4x4 perspective projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 PerspectiveFov(float verticalFov, float aspect, float near, float far)
        {
            float cotangent = 1.0f / tan(verticalFov * 0.5f);
            float rcpdz = 1.0f / (near - far);

            return float4x4(
                cotangent / aspect, 0.0f,       0.0f,                   0.0f,
                0.0f,               cotangent,  0.0f,                   0.0f,
                0.0f,               0.0f,       (far + near) * rcpdz,   2.0f * near * far * rcpdz,
                0.0f,               0.0f,      -1.0f,                   0.0f
                );
        }

        /// <summary>
        /// Returns a float4x4 off-center perspective projection matrix.
        /// </summary>
        /// <param name="left">The x-coordinate of the left side of the clipping frustum at the near plane.</param>
        /// <param name="right">The x-coordinate of the right side of the clipping frustum at the near plane.</param>
        /// <param name="bottom">The y-coordinate of the bottom side of the clipping frustum at the near plane.</param>
        /// <param name="top">The y-coordinate of the top side of the clipping frustum at the near plane.</param>
        /// <param name="near">Distance to the near plane. Must be greater than zero.</param>
        /// <param name="far">Distance to the far plane. Must be greater than zero.</param>
        /// <returns>The float4x4 off-center perspective projection matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
        {
            float rcpdz = 1.0f / (near - far);
            float rcpWidth = 1.0f / (right - left);
            float rcpHeight = 1.0f / (top - bottom);

            return float4x4(
                2.0f * near * rcpWidth,     0.0f,                       (left + right) * rcpWidth,     0.0f,
                0.0f,                       2.0f * near * rcpHeight,    (bottom + top) * rcpHeight,    0.0f,
                0.0f,                       0.0f,                        (far + near) * rcpdz,          2.0f * near * far * rcpdz,
                0.0f,                       0.0f,                       -1.0f,                          0.0f
                );
        }

        /// <summary>
        /// Returns a float4x4 matrix representing a combined scale-, rotation- and translation transform.
        /// Equivalent to mul(translationTransform, mul(rotationTransform, scaleTransform)).
        /// </summary>
        /// <param name="translation">The translation vector.</param>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <param name="scale">The scaling factors of each axis.</param>
        /// <returns>The float4x4 matrix representing the translation, rotation, and scale by the inputs.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 TRS(in float3a translation, in quaternion rotation, in float3a scale)
        {
            float3ax3 r = float3ax3(rotation);
            return float4x4(  float4(r.c0 * scale.x, 0.0f),
                              float4(r.c1 * scale.y, 0.0f),
                              float4(r.c2 * scale.z, 0.0f),
                              float4(translation, 1.0f));
        }
    }

    public partial struct float4x3
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat4x3FromRotTrans(in quaternion rot, in float3a trans, out float4 res0, out float4 res1, out float4 res2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(in float3ax3 rotation, in float3 translation)
        {
            float3ax3 rot = transpose(rotation);
            c0 = float4(rot.c0, translation.x);
            c1 = float4(rot.c1, translation.y);
            c2 = float4(rot.c2, translation.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(in quaternion rotation, in float3a translation)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat4x3FromRotTrans(in rotation, in translation, out c0, out c1, out c2);
        #else
            float3ax3 rot = transpose(float3ax3(rotation));
            c0 = float4(rot.c0, translation.x);
            c1 = float4(rot.c1, translation.y);
            c2 = float4(rot.c2, translation.z);
        #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(in RigidTransform transform)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat4x3FromRotTrans(in transform.rot, transform.posa, out c0, out c1, out c2);
        #else
            float3ax3 rot = transpose(float3ax3(transform.rot));
            c0 = float4(rot.c0, transform.pos.x);
            c1 = float4(rot.c1, transform.pos.y);
            c2 = float4(rot.c2, transform.pos.z);
        #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 Translate(in float3 vector)
        {
            return float4x3(float4(1.0f, 0.0f, 0.0f, vector.x),
                            float4(0.0f, 1.0f, 0.0f, vector.y),
                            float4(0.0f, 0.0f, 1.0f, vector.z));
        }
    }

    partial class math
    {
        /// <summary>
        /// Extracts a float3x3 from the upper left 3x3 of a float4x4.
        /// </summary>
        /// <param name="f4x4"><see cref="float4x4"/> to extract a float3x3 from.</param>
        /// <returns>Upper left 3x3 matrix as float3x3.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 float3ax3(in float4x4 f4x4)
        {
            return new float3ax3(f4x4);
        }

        /// <summary>Returns a float3x3 matrix constructed from a quaternion.</summary>
        /// <param name="rotation">The quaternion representing a rotation.</param>
        /// <returns>The float3x3 constructed from a quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 float3ax3(in quaternion rotation)
        {
            return new float3ax3(rotation);
        }

        /// <summary>Returns a float4x4 constructed from a float3x3 rotation matrix and a float3 translation vector.</summary>
        /// <param name="rotation">The float3x3 rotation matrix.</param>
        /// <param name="translation">The translation vector.</param>
        /// <returns>The float4x4 constructed from a rotation and translation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 float4x4(in float3ax3 rotation, in float3a translation)
        {
            return new float4x4(rotation, translation);
        }

        /// <summary>Returns a float4x4 constructed from a quaternion and a float3 translation vector.</summary>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <param name="translation">The translation vector.</param>
        /// <returns>The float4x4 constructed from a rotation and translation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 float4x4(in quaternion rotation, in float3 translation)
        {
            return new float4x4(rotation, translation);
        }

        /// <summary>Returns a float4x4 constructed from a RigidTransform.</summary>
        /// <param name="transform">The rigid transformation.</param>
        /// <returns>The float4x4 constructed from a RigidTransform.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 float4x4(in RigidTransform transform)
        {
            return new float4x4(transform);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(in quaternion rotation, in float3 translation)
        {
            return new float4x3(rotation, translation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(in RigidTransform transform)
        {
            return new float4x3(transform);
        }

        /// <summary>Returns an orthonormalized version of a float3x3 matrix.</summary>
        /// <param name="i">The float3x3 to be orthonormalized.</param>
        /// <returns>The orthonormalized float3x3 matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 orthonormalize(in float3ax3 i)
        {
            float3ax3 o;

            var u = i.c0;
            var v = i.c1 - i.c0 * math.dot(i.c1, i.c0);

            float lenU = math.length(u);
            float lenV = math.length(v);

            bool c = lenU > 1e-30f && lenV > 1e-30f;

            o.c0 = math.select(float3c.right,   u / lenU, c);
            o.c1 = math.select(float3c.up,      v / lenV, c);
            o.c2 = math.cross(o.c0, o.c1);

            return o;
        }

        /// <summary>
        /// Computes the pseudoinverse of a matrix.
        /// </summary>
        /// <param name="m">Matrix to invert.</param>
        /// <returns>The pseudoinverse of m.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3ax3 pseudoinverse(float3ax3 m)
        {
            float scaleSq = 0.333333f * (math.lengthsq(m.c0) + math.lengthsq(m.c1) + math.lengthsq(m.c2));
            if (scaleSq < svd.k_EpsilonNormal)
                return float3ax3c.zero;

            var scaleInv = math.rsqrt(scaleSq);
            float3ax3 ms = mulScale(m, scaleInv);
            if (!adjInverse(ms, out float3ax3 i, svd.k_EpsilonDeterminant))
            {
                i = svd.svdInverse(ms);
            }

            return mulScale(i, scaleInv);
        }
    }
}
