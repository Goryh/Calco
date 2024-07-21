using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;

#pragma warning disable 0660, 0661

namespace calco
{
    /// <summary>A 3 component vector of floats.</summary>
    [Il2CppEagerStaticClassConstruction]
    public ref struct float3a
    {
        public float x;
        public float y;
        public float z;

        public float pad;

        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat4ToFloat3a(in float4 a, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3ToFloat3a(in float3 a, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aToFloat3(in float3a a, out float3 res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathVector3ToFloat3a(in UnityEngine.Vector3 a, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aToVector3(in float3a a, out UnityEngine.Vector3 res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloatToFloat3a(float a, out float3a res);

        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aAdd3a(in float3a lhs, in float3a rhs, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aAdd(float lhs, in float3a rhs, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aSub3a(in float3a lhs, in float3a rhs, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aSub(float lhs, in float3a rhs, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aSubF(in float3a lhs, float rhs, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aMul(float lhs, in float3a rhs, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aMul3a(in float3a lhs, in float3a rhs, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aDiv(float lhs, in float3a rhs, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aDivF(in float3a lhs, float rhs, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aDiv3a(in float3a lhs, in float3a rhs, out float3a res);
        [MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        static extern void vecILMathFloat3aNeg(in float3a a, out float3a res);

        /// <summary>Constructs a float3a vector from three float values.</summary>
        /// <param name="x">The constructed vector's x component will be set to this value.</param>
        /// <param name="y">The constructed vector's y component will be set to this value.</param>
        /// <param name="z">The constructed vector's z component will be set to this value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3a(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.pad = 0;
        }

        /// <summary>Constructs a float3a vector from a float value and a float2 vector.</summary>
        /// <param name="x">The constructed vector's x component will be set to this value.</param>
        /// <param name="yz">The constructed vector's yz components will be set to this value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3a(float x, float2 yz)
        {
            this.x = x;
            this.y = yz.x;
            this.z = yz.y;
            this.pad = 0;
        }

        /// <summary>Constructs a float3a vector from a float2 vector and a float value.</summary>
        /// <param name="xy">The constructed vector's xy components will be set to this value.</param>
        /// <param name="z">The constructed vector's z component will be set to this value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3a(float2 xy, float z)
        {
            this.x = xy.x;
            this.y = xy.y;
            this.z = z;
            this.pad = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3a(in float4 xyz)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat4ToFloat3a(in xyz, out this);
        #else
            this.x = xyz.x;
            this.y = xyz.y;
            this.z = xyz.z;
            this.pad = xyz.w;
        #endif
        }

        /// <summary>Constructs a float3a vector from a single float value by assigning it to every component.</summary>
        /// <param name="v">float to convert to float3a</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3a(float v)
        {
        #if ENABLE_IL2CPP
            vecILMathFloatToFloat3a(v, out this);
        #else
            this.x = v;
            this.y = v;
            this.z = v;
            this.pad = 0;
        #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float3(in float3a v)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aToFloat3(in v, out var res);
            return res;
        #else
            return new float3(v.x, v.y, v.z);
        #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float3a(in float3 v)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3ToFloat3a(in v, out var res);
            return res;
        #else
            return new float3a(new float4(v, 0));
        #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UnityEngine.Vector3(in float3a v)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aToVector3(in v, out var res);
            return res;
        #else
            return new UnityEngine.Vector3(v.x, v.y, v.z);
        #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float3a(in UnityEngine.Vector3 v)
        {
        #if ENABLE_IL2CPP
            vecILMathVector3ToFloat3a(in v, out var res);
            return res;
        #else
            return new float3a(v.x, v.y, v.z);
        #endif
        }

        /// <summary>Implicitly converts a single float value to a float3a vector by assigning it to every component.</summary>
        /// <param name="v">float to convert to float3a</param>
        /// <returns>Converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float3a(float v) { return new float3a(v); }

        /// <summary>Returns the result of a componentwise multiplication operation on two float3a vectors.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise multiplication.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise multiplication.</param>
        /// <returns>float3a result of the componentwise multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator * (in float3a lhs, in float3a rhs)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aMul3a(in lhs, in rhs, out var res);
            return res;
        #else
            return new float3a (lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
        #endif
        }

        /// <summary>Returns the result of a componentwise multiplication operation on a float3a vector and a float value.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise multiplication.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise multiplication.</param>
        /// <returns>float3a result of the componentwise multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator * (in float3a lhs, float rhs)  { return rhs * lhs; }

        /// <summary>Returns the result of a componentwise multiplication operation on a float value and a float3a vector.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise multiplication.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise multiplication.</param>
        /// <returns>float3a result of the componentwise multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator * (float lhs, in float3a rhs)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aMul(lhs, in rhs, out var res);
            return res;
        #else
            return new float3a (lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
        #endif
        }


        /// <summary>Returns the result of a componentwise addition operation on two float3a vectors.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise addition.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise addition.</param>
        /// <returns>float3a result of the componentwise addition.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator + (in float3a lhs, in float3a rhs)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aAdd3a(in lhs, in rhs, out var res);
            return res;
        #else
            return new float3a (lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        #endif
        }

        /// <summary>Returns the result of a componentwise addition operation on a float3a vector and a float value.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise addition.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise addition.</param>
        /// <returns>float3a result of the componentwise addition.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator + (in float3a lhs, float rhs) { return rhs + lhs; }

        /// <summary>Returns the result of a componentwise addition operation on a float value and a float3a vector.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise addition.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise addition.</param>
        /// <returns>float3a result of the componentwise addition.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator + (float lhs, in float3a rhs)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aAdd(lhs, in rhs, out var res);
            return res;
        #else
            return new float3a (lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);
        #endif
        }


        /// <summary>Returns the result of a componentwise subtraction operation on two float3a vectors.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise subtraction.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise subtraction.</param>
        /// <returns>float3a result of the componentwise subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator - (in float3a lhs, in float3a rhs)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aSub3a(in lhs, in rhs, out var res);
            return res;
        #else
            return new float3a (lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        #endif
        }

        /// <summary>Returns the result of a componentwise subtraction operation on a float3a vector and a float value.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise subtraction.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise subtraction.</param>
        /// <returns>float3a result of the componentwise subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator - (in float3a lhs, float rhs)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aSubF(in lhs, rhs, out var res);
            return res;
        #else
            return new float3a (lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
        #endif
        }

        /// <summary>Returns the result of a componentwise subtraction operation on a float value and a float3a vector.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise subtraction.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise subtraction.</param>
        /// <returns>float3a result of the componentwise subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator - (float lhs, in float3a rhs)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aSub(lhs, in rhs, out var res);
            return res;
        #else
            return new float3a (lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);
        #endif
        }


        /// <summary>Returns the result of a componentwise division operation on two float3a vectors.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise division.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise division.</param>
        /// <returns>float3a result of the componentwise division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator / (in float3a lhs, in float3a rhs)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aDiv3a(in lhs, in rhs, out var res);
            return res;
        #else
            return new float3a (lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
        #endif
        }

        /// <summary>Returns the result of a componentwise division operation on a float3a vector and a float value.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise division.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise division.</param>
        /// <returns>float3a result of the componentwise division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator / (in float3a lhs, float rhs)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aDivF(in lhs, rhs, out var res);
            return res;
        #else
            return new float3a (lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
        #endif
        }

        /// <summary>Returns the result of a componentwise division operation on a float value and a float3a vector.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise division.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise division.</param>
        /// <returns>float3a result of the componentwise division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator / (float lhs, in float3a rhs)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aDiv(lhs, in rhs, out var res);
            return res;
        #else
            return new float3a (lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);
        #endif
        }


        /// <summary>Returns the result of a componentwise modulus operation on two float3a vectors.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise modulus.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise modulus.</param>
        /// <returns>float3a result of the componentwise modulus.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator % (in float3a lhs, in float3a rhs) { return new float3a (lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z); }

        /// <summary>Returns the result of a componentwise modulus operation on a float3a vector and a float value.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise modulus.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise modulus.</param>
        /// <returns>float3a result of the componentwise modulus.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator % (in float3a lhs, float rhs) { return new float3a (lhs.x % rhs, lhs.y % rhs, lhs.z % rhs); }

        /// <summary>Returns the result of a componentwise modulus operation on a float value and a float3a vector.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise modulus.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise modulus.</param>
        /// <returns>float3a result of the componentwise modulus.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator % (float lhs, in float3a rhs) { return new float3a (lhs % rhs.x, lhs % rhs.y, lhs % rhs.z); }


        /// <summary>Returns the result of a componentwise increment operation on a float3a vector.</summary>
        /// <param name="val">Value to use when computing the componentwise increment.</param>
        /// <returns>float3a result of the componentwise increment.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator ++ (float3a val) { return new float3a (++val.x, ++val.y, ++val.z); }


        /// <summary>Returns the result of a componentwise decrement operation on a float3a vector.</summary>
        /// <param name="val">Value to use when computing the componentwise decrement.</param>
        /// <returns>float3a result of the componentwise decrement.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator -- (float3a val) { return new float3a (--val.x, --val.y, --val.z); }


        /// <summary>Returns the result of a componentwise less than operation on two float3a vectors.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise less than.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise less than.</param>
        /// <returns>bool3 result of the componentwise less than.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator < (in float3a lhs, in float3a rhs) { return new bool3 (lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z); }

        /// <summary>Returns the result of a componentwise less than operation on a float3a vector and a float value.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise less than.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise less than.</param>
        /// <returns>bool3 result of the componentwise less than.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator < (in float3a lhs, float rhs) { return new bool3 (lhs.x < rhs, lhs.y < rhs, lhs.z < rhs); }

        /// <summary>Returns the result of a componentwise less than operation on a float value and a float3a vector.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise less than.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise less than.</param>
        /// <returns>bool3 result of the componentwise less than.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator < (float lhs, in float3a rhs) { return new bool3 (lhs < rhs.x, lhs < rhs.y, lhs < rhs.z); }


        /// <summary>Returns the result of a componentwise less or equal operation on two float3a vectors.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise less or equal.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise less or equal.</param>
        /// <returns>bool3 result of the componentwise less or equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator <= (in float3a lhs, in float3a rhs) { return new bool3 (lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z); }

        /// <summary>Returns the result of a componentwise less or equal operation on a float3a vector and a float value.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise less or equal.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise less or equal.</param>
        /// <returns>bool3 result of the componentwise less or equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator <= (in float3a lhs, float rhs) { return new bool3 (lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs); }

        /// <summary>Returns the result of a componentwise less or equal operation on a float value and a float3a vector.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise less or equal.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise less or equal.</param>
        /// <returns>bool3 result of the componentwise less or equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator <= (float lhs, in float3a rhs) { return new bool3 (lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z); }


        /// <summary>Returns the result of a componentwise greater than operation on two float3a vectors.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise greater than.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise greater than.</param>
        /// <returns>bool3 result of the componentwise greater than.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator > (in float3a lhs, in float3a rhs) { return new bool3 (lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z); }

        /// <summary>Returns the result of a componentwise greater than operation on a float3a vector and a float value.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise greater than.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise greater than.</param>
        /// <returns>bool3 result of the componentwise greater than.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator > (in float3a lhs, float rhs) { return new bool3 (lhs.x > rhs, lhs.y > rhs, lhs.z > rhs); }

        /// <summary>Returns the result of a componentwise greater than operation on a float value and a float3a vector.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise greater than.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise greater than.</param>
        /// <returns>bool3 result of the componentwise greater than.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator > (float lhs, in float3a rhs) { return new bool3 (lhs > rhs.x, lhs > rhs.y, lhs > rhs.z); }


        /// <summary>Returns the result of a componentwise greater or equal operation on two float3a vectors.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise greater or equal.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise greater or equal.</param>
        /// <returns>bool3 result of the componentwise greater or equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator >= (in float3a lhs, in float3a rhs) { return new bool3 (lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z); }

        /// <summary>Returns the result of a componentwise greater or equal operation on a float3a vector and a float value.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise greater or equal.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise greater or equal.</param>
        /// <returns>bool3 result of the componentwise greater or equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator >= (in float3a lhs, float rhs) { return new bool3 (lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs); }

        /// <summary>Returns the result of a componentwise greater or equal operation on a float value and a float3a vector.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise greater or equal.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise greater or equal.</param>
        /// <returns>bool3 result of the componentwise greater or equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator >= (float lhs, in float3a rhs) { return new bool3 (lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z); }


        /// <summary>Returns the result of a componentwise unary minus operation on a float3a vector.</summary>
        /// <param name="val">Value to use when computing the componentwise unary minus.</param>
        /// <returns>float3a result of the componentwise unary minus.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator - (in float3a val)
        {
        #if ENABLE_IL2CPP
            vecILMathFloat3aNeg(in val, out var res);
            return res;
        #else
            return new float3a (-val.x, -val.y, -val.z);
        #endif
        }


        /// <summary>Returns the result of a componentwise unary plus operation on a float3a vector.</summary>
        /// <param name="val">Value to use when computing the componentwise unary plus.</param>
        /// <returns>float3a result of the componentwise unary plus.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a operator + (in float3a val) { return val; }


        /// <summary>Returns the result of a componentwise equality operation on two float3a vectors.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise equality.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise equality.</param>
        /// <returns>bool3 result of the componentwise equality.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator == (in float3a lhs, in float3a rhs) { return new bool3 (lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z); }

        /// <summary>Returns the result of a componentwise equality operation on a float3a vector and a float value.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise equality.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise equality.</param>
        /// <returns>bool3 result of the componentwise equality.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator == (in float3a lhs, float rhs) { return new bool3 (lhs.x == rhs, lhs.y == rhs, lhs.z == rhs); }

        /// <summary>Returns the result of a componentwise equality operation on a float value and a float3a vector.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise equality.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise equality.</param>
        /// <returns>bool3 result of the componentwise equality.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator == (float lhs, in float3a rhs) { return new bool3 (lhs == rhs.x, lhs == rhs.y, lhs == rhs.z); }


        /// <summary>Returns the result of a componentwise not equal operation on two float3a vectors.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise not equal.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise not equal.</param>
        /// <returns>bool3 result of the componentwise not equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator != (in float3a lhs, in float3a rhs) { return new bool3 (lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z); }

        /// <summary>Returns the result of a componentwise not equal operation on a float3a vector and a float value.</summary>
        /// <param name="lhs">Left hand side float3a to use to compute componentwise not equal.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise not equal.</param>
        /// <returns>bool3 result of the componentwise not equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator != (in float3a lhs, float rhs) { return new bool3 (lhs.x != rhs, lhs.y != rhs, lhs.z != rhs); }

        /// <summary>Returns the result of a componentwise not equal operation on a float value and a float3a vector.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise not equal.</param>
        /// <param name="rhs">Right hand side float3a to use to compute componentwise not equal.</param>
        /// <returns>bool3 result of the componentwise not equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator != (float lhs, in float3a rhs) { return new bool3 (lhs != rhs.x, lhs != rhs.y, lhs != rhs.z); }




        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, x, x, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xxxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, x, x, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, x, y, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, x, y, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xxyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, x, y, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xxzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, x, z, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xxzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, x, z, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xxzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, x, z, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, y, x, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, y, x, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xyxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, y, x, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, y, y, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, y, y, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xyyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, y, y, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xyzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, y, z, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xyzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, y, z, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xyzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, y, z, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xzxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, z, x, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xzxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, z, x, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xzxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, z, x, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xzyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, z, y, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xzyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, z, y, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xzyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, z, y, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xzzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, z, z, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xzzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, z, z, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 xzzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(x, z, z, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, x, x, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, x, x, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yxxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, x, x, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, x, y, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, x, y, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yxyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, x, y, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yxzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, x, z, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yxzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, x, z, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yxzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, x, z, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, y, x, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, y, x, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yyxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, y, x, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, y, y, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yyyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, y, y, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yyzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, y, z, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yyzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, y, z, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yyzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, y, z, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yzxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, z, x, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yzxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, z, x, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yzxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, z, x, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yzyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, z, y, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yzyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, z, y, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yzyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, z, y, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yzzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, z, z, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yzzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, z, z, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 yzzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(y, z, z, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, x, x, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, x, x, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zxxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, x, x, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, x, y, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, x, y, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zxyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, x, y, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zxzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, x, z, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zxzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, x, z, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zxzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, x, z, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, y, x, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, y, x, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zyxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, y, x, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, y, y, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, y, y, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zyyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, y, y, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zyzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, y, z, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zyzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, y, z, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zyzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, y, z, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zzxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, z, x, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zzxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, z, x, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zzxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, z, x, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zzyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, z, y, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zzyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, z, y, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zzyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, z, y, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zzzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, z, z, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zzzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z, z, z, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float4 zzzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float4(z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a xxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a xxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(x, x, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a xxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(x, x, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a xyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(x, y, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a xyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(x, y, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3a xyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return this; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; y = value.y; z = value.z; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a xzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(x, z, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3a xzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new float3a(x, z, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; z = value.y; y = value.z; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a xzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(x, z, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a yxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(y, x, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a yxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(y, x, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3a yxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new float3a(y, x, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; x = value.y; z = value.z; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a yyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(y, y, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a yyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a yyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(y, y, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3a yzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new float3a(y, z, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; z = value.y; x = value.z; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a yzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(y, z, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a yzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(y, z, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a zxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(z, x, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3a zxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new float3a(z, x, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; x = value.y; y = value.z; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a zxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(z, x, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float3a zyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new float3a(z, y, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; y = value.y; x = value.z; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a zyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(z, y, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a zyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(z, y, z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a zzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(z, z, x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a zzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(z, z, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float3a zzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float3a(z); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float2 xx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float2(x); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 xy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new float2(x, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; y = value.y; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 xz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new float2(x, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; z = value.y; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 yx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new float2(y, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; x = value.y; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float2 yy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float2(y, y); }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 yz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new float2(y, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; z = value.y; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 zx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new float2(z, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; x = value.y; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public float2 zy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new float2(z, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; y = value.y; }
        }


        /// <summary>Swizzles the vector.</summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly float2 zz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new float2(z, z); }
        }


        /// <summary>Returns the float element at a specified index.</summary>
        unsafe public float this[int index]
        {
    		[MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 3)
                    throw new System.ArgumentException("index must be between[0...2]");
#endif
                fixed (float3a* array = &this) { return ((float*)array)[index]; }
            }
    		[MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 3)
                    throw new System.ArgumentException("index must be between[0...2]");
#endif
                fixed (float* array = &x) { array[index] = value; }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override string ToString()
        {
            return string.Format("float3({0}f, {1}f, {2}f)", x, y, z);
        }
    }

    public static partial class math
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a float3a(float x, float y, float z) { return new float3a(x, y, z); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a float3a(float x, in float2 yz) { return new float3a(x, yz); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a float3a(in float2 xy, float z) { return new float3a(xy, z); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a float3a(in float3 xyz) { return (float3a)xyz; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 float3(in float3a xyz) { return (float3)xyz; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a float3a(in float4 xyz)  { return new float3a(in xyz); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a float3a(float v) { return new float3a(v); }


        /// <summary>Returns the result of specified shuffling of the components from two float3a vectors into a float value.</summary>
        /// <param name="left">float3a to use as the left argument of the shuffle operation.</param>
        /// <param name="right">float3a to use as the right argument of the shuffle operation.</param>
        /// <param name="x">The ShuffleComponent to use when setting the resulting float.</param>
        /// <returns>float result of the shuffle operation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float shuffle(in float3a a, in float3a b, ShuffleComponent x)
        {
            return select_shuffle_component(a, b, x);
        }

        /// <summary>Returns the result of specified shuffling of the components from two float3a vectors into a float2 vector.</summary>
        /// <param name="left">float3a to use as the left argument of the shuffle operation.</param>
        /// <param name="right">float3a to use as the right argument of the shuffle operation.</param>
        /// <param name="x">The ShuffleComponent to use when setting the resulting float2 x component.</param>
        /// <param name="y">The ShuffleComponent to use when setting the resulting float2 y component.</param>
        /// <returns>float2 result of the shuffle operation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 shuffle(in float3a a, in float3a b, ShuffleComponent x, ShuffleComponent y)
        {
            return float2(
                select_shuffle_component(a, b, x),
                select_shuffle_component(a, b, y));
        }

        /// <summary>Returns the result of specified shuffling of the components from two float3a vectors into a float3a vector.</summary>
        /// <param name="left">float3a to use as the left argument of the shuffle operation.</param>
        /// <param name="right">float3a to use as the right argument of the shuffle operation.</param>
        /// <param name="x">The ShuffleComponent to use when setting the resulting float3a x component.</param>
        /// <param name="y">The ShuffleComponent to use when setting the resulting float3a y component.</param>
        /// <param name="z">The ShuffleComponent to use when setting the resulting float3a z component.</param>
        /// <returns>float3a result of the shuffle operation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3a shuffle(in float3a a, in float3a b, ShuffleComponent x, ShuffleComponent y, ShuffleComponent z)
        {
            return float3a(
                select_shuffle_component(a, b, x),
                select_shuffle_component(a, b, y),
                select_shuffle_component(a, b, z));
        }

        /// <summary>Returns the result of specified shuffling of the components from two float3a vectors into a float4 vector.</summary>
        /// <param name="left">float3a to use as the left argument of the shuffle operation.</param>
        /// <param name="right">float3a to use as the right argument of the shuffle operation.</param>
        /// <param name="x">The ShuffleComponent to use when setting the resulting float4 x component.</param>
        /// <param name="y">The ShuffleComponent to use when setting the resulting float4 y component.</param>
        /// <param name="z">The ShuffleComponent to use when setting the resulting float4 z component.</param>
        /// <param name="w">The ShuffleComponent to use when setting the resulting float4 w component.</param>
        /// <returns>float4 result of the shuffle operation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 shuffle(in float3a a, in float3a b, ShuffleComponent x, ShuffleComponent y, ShuffleComponent z, ShuffleComponent w)
        {
            return float4(
                select_shuffle_component(a, b, x),
                select_shuffle_component(a, b, y),
                select_shuffle_component(a, b, z),
                select_shuffle_component(a, b, w));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float select_shuffle_component(in float3a a, in float3a b, ShuffleComponent component)
        {
            switch(component)
            {
                case ShuffleComponent.LeftX:
                    return a.x;
                case ShuffleComponent.LeftY:
                    return a.y;
                case ShuffleComponent.LeftZ:
                    return a.z;
                case ShuffleComponent.RightX:
                    return b.x;
                case ShuffleComponent.RightY:
                    return b.y;
                case ShuffleComponent.RightZ:
                    return b.z;
                default:
                    throw new System.ArgumentException("Invalid shuffle component: " + component);
            }
        }

    }
}
