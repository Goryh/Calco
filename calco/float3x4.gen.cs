//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated. To update the generation of this file, modify and re-run calco.CodeGen.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

#pragma warning disable 0660, 0661

namespace calco
{
    /// <summary>A 3x4 matrix of floats.</summary>
    [System.Serializable]
    [Il2CppEagerStaticClassConstruction]
    public partial struct float3x4 : System.IEquatable<float3x4>, IFormattable
    {
        /// <summary>Column 0 of the matrix.</summary>
        public float3 c0;
        /// <summary>Column 1 of the matrix.</summary>
        public float3 c1;
        /// <summary>Column 2 of the matrix.</summary>
        public float3 c2;
        /// <summary>Column 3 of the matrix.</summary>
        public float3 c3;

        /// <summary>float3x4 zero value.</summary>
        public static readonly float3x4 zero;

        /// <summary>Constructs a float3x4 matrix from four float3 vectors.</summary>
        /// <param name="c0">The matrix column c0 will be set to this value.</param>
        /// <param name="c1">The matrix column c1 will be set to this value.</param>
        /// <param name="c2">The matrix column c2 will be set to this value.</param>
        /// <param name="c3">The matrix column c3 will be set to this value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3x4(float3 c0, float3 c1, float3 c2, float3 c3)
        {
            this.c0 = c0;
            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;
        }

        /// <summary>Constructs a float3x4 matrix from 12 float values given in row-major order.</summary>
        /// <param name="m00">The matrix at row 0, column 0 will be set to this value.</param>
        /// <param name="m01">The matrix at row 0, column 1 will be set to this value.</param>
        /// <param name="m02">The matrix at row 0, column 2 will be set to this value.</param>
        /// <param name="m03">The matrix at row 0, column 3 will be set to this value.</param>
        /// <param name="m10">The matrix at row 1, column 0 will be set to this value.</param>
        /// <param name="m11">The matrix at row 1, column 1 will be set to this value.</param>
        /// <param name="m12">The matrix at row 1, column 2 will be set to this value.</param>
        /// <param name="m13">The matrix at row 1, column 3 will be set to this value.</param>
        /// <param name="m20">The matrix at row 2, column 0 will be set to this value.</param>
        /// <param name="m21">The matrix at row 2, column 1 will be set to this value.</param>
        /// <param name="m22">The matrix at row 2, column 2 will be set to this value.</param>
        /// <param name="m23">The matrix at row 2, column 3 will be set to this value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3x4(float m00, float m01, float m02, float m03,
                        float m10, float m11, float m12, float m13,
                        float m20, float m21, float m22, float m23)
        {
            this.c0 = new float3(m00, m10, m20);
            this.c1 = new float3(m01, m11, m21);
            this.c2 = new float3(m02, m12, m22);
            this.c3 = new float3(m03, m13, m23);
        }

        /// <summary>Constructs a float3x4 matrix from a single float value by assigning it to every component.</summary>
        /// <param name="v">float to convert to float3x4</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3x4(float v)
        {
            this.c0 = v;
            this.c1 = v;
            this.c2 = v;
            this.c3 = v;
        }

        /// <summary>Constructs a float3x4 matrix from a single bool value by converting it to float and assigning it to every component.</summary>
        /// <param name="v">bool to convert to float3x4</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3x4(bool v)
        {
            this.c0 = math.select(new float3(0.0f), new float3(1.0f), v);
            this.c1 = math.select(new float3(0.0f), new float3(1.0f), v);
            this.c2 = math.select(new float3(0.0f), new float3(1.0f), v);
            this.c3 = math.select(new float3(0.0f), new float3(1.0f), v);
        }

        /// <summary>Constructs a float3x4 matrix from a single int value by converting it to float and assigning it to every component.</summary>
        /// <param name="v">int to convert to float3x4</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3x4(int v)
        {
            this.c0 = v;
            this.c1 = v;
            this.c2 = v;
            this.c3 = v;
        }

        /// <summary>Constructs a float3x4 matrix from a single uint value by converting it to float and assigning it to every component.</summary>
        /// <param name="v">uint to convert to float3x4</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3x4(uint v)
        {
            this.c0 = v;
            this.c1 = v;
            this.c2 = v;
            this.c3 = v;
        }


        /// <summary>Implicitly converts a single float value to a float3x4 matrix by assigning it to every component.</summary>
        /// <param name="v">float to convert to float3x4</param>
        /// <returns>Converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float3x4(float v) { return new float3x4(v); }

        /// <summary>Explicitly converts a single bool value to a float3x4 matrix by converting it to float and assigning it to every component.</summary>
        /// <param name="v">bool to convert to float3x4</param>
        /// <returns>Converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float3x4(bool v) { return new float3x4(v); }

        /// <summary>Implicitly converts a single int value to a float3x4 matrix by converting it to float and assigning it to every component.</summary>
        /// <param name="v">int to convert to float3x4</param>
        /// <returns>Converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float3x4(int v) { return new float3x4(v); }

        /// <summary>Implicitly converts a single uint value to a float3x4 matrix by converting it to float and assigning it to every component.</summary>
        /// <param name="v">uint to convert to float3x4</param>
        /// <returns>Converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float3x4(uint v) { return new float3x4(v); }


        /// <summary>Returns the result of a componentwise multiplication operation on two float3x4 matrices.</summary>
        /// <param name="lhs">Left hand side float3x4 to use to compute componentwise multiplication.</param>
        /// <param name="rhs">Right hand side float3x4 to use to compute componentwise multiplication.</param>
        /// <returns>float3x4 result of the componentwise multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator * (float3x4 lhs, float3x4 rhs) { return new float3x4 (lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3); }

        /// <summary>Returns the result of a componentwise multiplication operation on a float3x4 matrix and a float value.</summary>
        /// <param name="lhs">Left hand side float3x4 to use to compute componentwise multiplication.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise multiplication.</param>
        /// <returns>float3x4 result of the componentwise multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator * (float3x4 lhs, float rhs) { return new float3x4 (lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs); }

        /// <summary>Returns the result of a componentwise multiplication operation on a float value and a float3x4 matrix.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise multiplication.</param>
        /// <param name="rhs">Right hand side float3x4 to use to compute componentwise multiplication.</param>
        /// <returns>float3x4 result of the componentwise multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator * (float lhs, float3x4 rhs) { return new float3x4 (lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3); }


        /// <summary>Returns the result of a componentwise addition operation on two float3x4 matrices.</summary>
        /// <param name="lhs">Left hand side float3x4 to use to compute componentwise addition.</param>
        /// <param name="rhs">Right hand side float3x4 to use to compute componentwise addition.</param>
        /// <returns>float3x4 result of the componentwise addition.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator + (float3x4 lhs, float3x4 rhs) { return new float3x4 (lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3); }

        /// <summary>Returns the result of a componentwise addition operation on a float3x4 matrix and a float value.</summary>
        /// <param name="lhs">Left hand side float3x4 to use to compute componentwise addition.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise addition.</param>
        /// <returns>float3x4 result of the componentwise addition.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator + (float3x4 lhs, float rhs) { return new float3x4 (lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs); }

        /// <summary>Returns the result of a componentwise addition operation on a float value and a float3x4 matrix.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise addition.</param>
        /// <param name="rhs">Right hand side float3x4 to use to compute componentwise addition.</param>
        /// <returns>float3x4 result of the componentwise addition.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator + (float lhs, float3x4 rhs) { return new float3x4 (lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3); }


        /// <summary>Returns the result of a componentwise subtraction operation on two float3x4 matrices.</summary>
        /// <param name="lhs">Left hand side float3x4 to use to compute componentwise subtraction.</param>
        /// <param name="rhs">Right hand side float3x4 to use to compute componentwise subtraction.</param>
        /// <returns>float3x4 result of the componentwise subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator - (float3x4 lhs, float3x4 rhs) { return new float3x4 (lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3); }

        /// <summary>Returns the result of a componentwise subtraction operation on a float3x4 matrix and a float value.</summary>
        /// <param name="lhs">Left hand side float3x4 to use to compute componentwise subtraction.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise subtraction.</param>
        /// <returns>float3x4 result of the componentwise subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator - (float3x4 lhs, float rhs) { return new float3x4 (lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs); }

        /// <summary>Returns the result of a componentwise subtraction operation on a float value and a float3x4 matrix.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise subtraction.</param>
        /// <param name="rhs">Right hand side float3x4 to use to compute componentwise subtraction.</param>
        /// <returns>float3x4 result of the componentwise subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator - (float lhs, float3x4 rhs) { return new float3x4 (lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3); }


        /// <summary>Returns the result of a componentwise division operation on two float3x4 matrices.</summary>
        /// <param name="lhs">Left hand side float3x4 to use to compute componentwise division.</param>
        /// <param name="rhs">Right hand side float3x4 to use to compute componentwise division.</param>
        /// <returns>float3x4 result of the componentwise division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator / (float3x4 lhs, float3x4 rhs) { return new float3x4 (lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3); }

        /// <summary>Returns the result of a componentwise division operation on a float3x4 matrix and a float value.</summary>
        /// <param name="lhs">Left hand side float3x4 to use to compute componentwise division.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise division.</param>
        /// <returns>float3x4 result of the componentwise division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator / (float3x4 lhs, float rhs) { return new float3x4 (lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs); }

        /// <summary>Returns the result of a componentwise division operation on a float value and a float3x4 matrix.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise division.</param>
        /// <param name="rhs">Right hand side float3x4 to use to compute componentwise division.</param>
        /// <returns>float3x4 result of the componentwise division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator / (float lhs, float3x4 rhs) { return new float3x4 (lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3); }


        /// <summary>Returns the result of a componentwise modulus operation on two float3x4 matrices.</summary>
        /// <param name="lhs">Left hand side float3x4 to use to compute componentwise modulus.</param>
        /// <param name="rhs">Right hand side float3x4 to use to compute componentwise modulus.</param>
        /// <returns>float3x4 result of the componentwise modulus.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator % (float3x4 lhs, float3x4 rhs) { return new float3x4 (lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3); }

        /// <summary>Returns the result of a componentwise modulus operation on a float3x4 matrix and a float value.</summary>
        /// <param name="lhs">Left hand side float3x4 to use to compute componentwise modulus.</param>
        /// <param name="rhs">Right hand side float to use to compute componentwise modulus.</param>
        /// <returns>float3x4 result of the componentwise modulus.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator % (float3x4 lhs, float rhs) { return new float3x4 (lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs); }

        /// <summary>Returns the result of a componentwise modulus operation on a float value and a float3x4 matrix.</summary>
        /// <param name="lhs">Left hand side float to use to compute componentwise modulus.</param>
        /// <param name="rhs">Right hand side float3x4 to use to compute componentwise modulus.</param>
        /// <returns>float3x4 result of the componentwise modulus.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator % (float lhs, float3x4 rhs) { return new float3x4 (lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3); }


        /// <summary>Returns the result of a componentwise increment operation on a float3x4 matrix.</summary>
        /// <param name="val">Value to use when computing the componentwise increment.</param>
        /// <returns>float3x4 result of the componentwise increment.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator ++ (float3x4 val) { return new float3x4 (++val.c0, ++val.c1, ++val.c2, ++val.c3); }


        /// <summary>Returns the result of a componentwise decrement operation on a float3x4 matrix.</summary>
        /// <param name="val">Value to use when computing the componentwise decrement.</param>
        /// <returns>float3x4 result of the componentwise decrement.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator -- (float3x4 val) { return new float3x4 (--val.c0, --val.c1, --val.c2, --val.c3); }


        /// <summary>Returns the result of a componentwise unary minus operation on a float3x4 matrix.</summary>
        /// <param name="val">Value to use when computing the componentwise unary minus.</param>
        /// <returns>float3x4 result of the componentwise unary minus.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator - (float3x4 val) { return new float3x4 (-val.c0, -val.c1, -val.c2, -val.c3); }


        /// <summary>Returns the result of a componentwise unary plus operation on a float3x4 matrix.</summary>
        /// <param name="val">Value to use when computing the componentwise unary plus.</param>
        /// <returns>float3x4 result of the componentwise unary plus.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 operator + (float3x4 val) { return new float3x4 (+val.c0, +val.c1, +val.c2, +val.c3); }


        /// <summary>Returns the float3 element at a specified index.</summary>
        unsafe public ref float3 this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 4)
                    throw new System.ArgumentException("index must be between[0...3]");
#endif
                fixed (float3x4* array = &this) { return ref ((float3*)array)[index]; }
            }
        }

        /// <summary>Returns true if the float3x4 is equal to a given float3x4, false otherwise.</summary>
        /// <param name="rhs">Right hand side argument to compare equality with.</param>
        /// <returns>The result of the equality comparison.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(float3x4 rhs) { return c0.Equals(rhs.c0) && c1.Equals(rhs.c1) && c2.Equals(rhs.c2) && c3.Equals(rhs.c3); }

        /// <summary>Returns true if the float3x4 is equal to a given float3x4, false otherwise.</summary>
        /// <param name="o">Right hand side argument to compare equality with.</param>
        /// <returns>The result of the equality comparison.</returns>
        public override bool Equals(object o) { return o is float3x4 converted && Equals(converted); }


        /// <summary>Returns a hash code for the float3x4.</summary>
        /// <returns>The computed hash code.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() { return (int)math.hash(this); }


        /// <summary>Returns a string representation of the float3x4.</summary>
        /// <returns>String representation of the value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("float3x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f,  {8}f, {9}f, {10}f, {11}f)", c0.x, c1.x, c2.x, c3.x, c0.y, c1.y, c2.y, c3.y, c0.z, c1.z, c2.z, c3.z);
        }

        /// <summary>Returns a string representation of the float3x4 using a specified format and culture-specific format information.</summary>
        /// <param name="format">Format string to use during string formatting.</param>
        /// <param name="formatProvider">Format provider to use during string formatting.</param>
        /// <returns>String representation of the value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("float3x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f,  {8}f, {9}f, {10}f, {11}f)", c0.x.ToString(format, formatProvider), c1.x.ToString(format, formatProvider), c2.x.ToString(format, formatProvider), c3.x.ToString(format, formatProvider), c0.y.ToString(format, formatProvider), c1.y.ToString(format, formatProvider), c2.y.ToString(format, formatProvider), c3.y.ToString(format, formatProvider), c0.z.ToString(format, formatProvider), c1.z.ToString(format, formatProvider), c2.z.ToString(format, formatProvider), c3.z.ToString(format, formatProvider));
        }

    }

    public static partial class math
    {
        /// <summary>Returns a float3x4 matrix constructed from four float3 vectors.</summary>
        /// <param name="c0">The matrix column c0 will be set to this value.</param>
        /// <param name="c1">The matrix column c1 will be set to this value.</param>
        /// <param name="c2">The matrix column c2 will be set to this value.</param>
        /// <param name="c3">The matrix column c3 will be set to this value.</param>
        /// <returns>float3x4 constructed from arguments.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 float3x4(float3 c0, float3 c1, float3 c2, float3 c3) { return new float3x4(c0, c1, c2, c3); }

        /// <summary>Returns a float3x4 matrix constructed from from 12 float values given in row-major order.</summary>
        /// <param name="m00">The matrix at row 0, column 0 will be set to this value.</param>
        /// <param name="m01">The matrix at row 0, column 1 will be set to this value.</param>
        /// <param name="m02">The matrix at row 0, column 2 will be set to this value.</param>
        /// <param name="m03">The matrix at row 0, column 3 will be set to this value.</param>
        /// <param name="m10">The matrix at row 1, column 0 will be set to this value.</param>
        /// <param name="m11">The matrix at row 1, column 1 will be set to this value.</param>
        /// <param name="m12">The matrix at row 1, column 2 will be set to this value.</param>
        /// <param name="m13">The matrix at row 1, column 3 will be set to this value.</param>
        /// <param name="m20">The matrix at row 2, column 0 will be set to this value.</param>
        /// <param name="m21">The matrix at row 2, column 1 will be set to this value.</param>
        /// <param name="m22">The matrix at row 2, column 2 will be set to this value.</param>
        /// <param name="m23">The matrix at row 2, column 3 will be set to this value.</param>
        /// <returns>float3x4 constructed from arguments.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 float3x4(float m00, float m01, float m02, float m03,
                                        float m10, float m11, float m12, float m13,
                                        float m20, float m21, float m22, float m23)
        {
            return new float3x4(m00, m01, m02, m03,
                                m10, m11, m12, m13,
                                m20, m21, m22, m23);
        }

        /// <summary>Returns a float3x4 matrix constructed from a single float value by assigning it to every component.</summary>
        /// <param name="v">float to convert to float3x4</param>
        /// <returns>Converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 float3x4(float v) { return new float3x4(v); }

        /// <summary>Returns a float3x4 matrix constructed from a single bool value by converting it to float and assigning it to every component.</summary>
        /// <param name="v">bool to convert to float3x4</param>
        /// <returns>Converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 float3x4(bool v) { return new float3x4(v); }

        /// <summary>Returns a float3x4 matrix constructed from a single int value by converting it to float and assigning it to every component.</summary>
        /// <param name="v">int to convert to float3x4</param>
        /// <returns>Converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 float3x4(int v) { return new float3x4(v); }

        /// <summary>Returns a float3x4 matrix constructed from a single uint value by converting it to float and assigning it to every component.</summary>
        /// <param name="v">uint to convert to float3x4</param>
        /// <returns>Converted value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 float3x4(uint v) { return new float3x4(v); }

        /// <summary>Return the float4x3 transpose of a float3x4 matrix.</summary>
        /// <param name="v">Value to transpose.</param>
        /// <returns>Transposed value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 transpose(float3x4 v)
        {
            return float4x3(
                v.c0.x, v.c0.y, v.c0.z,
                v.c1.x, v.c1.y, v.c1.z,
                v.c2.x, v.c2.y, v.c2.z,
                v.c3.x, v.c3.y, v.c3.z);
        }

        /// <summary>Fast matrix inverse for rigid transforms (orthonormal basis and translation)</summary>
        /// <param name="m">Matrix to invert.</param>
        /// <returns>The inverted matrix.</returns>
        public static float3x4 fastinverse(float3x4 m)
        {
            float3 c0 = m.c0;
            float3 c1 = m.c1;
            float3 c2 = m.c2;
            float3 pos = m.c3;

            float3 r0 = float3(c0.x, c1.x, c2.x);
            float3 r1 = float3(c0.y, c1.y, c2.y);
            float3 r2 = float3(c0.z, c1.z, c2.z);

            pos = -(r0 * pos.x + r1 * pos.y + r2 * pos.z);

            return float3x4(r0, r1, r2, pos);
        }

        /// <summary>Returns a uint hash code of a float3x4 matrix.</summary>
        /// <param name="v">Matrix value to hash.</param>
        /// <returns>uint hash of the argument.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(float3x4 v)
        {
            return csum(asuint(v.c0) * uint3(0xF9EA92D5u, 0xC2FAFCB9u, 0x616E9CA1u) +
                        asuint(v.c1) * uint3(0xC5C5394Bu, 0xCAE78587u, 0x7A1541C9u) +
                        asuint(v.c2) * uint3(0xF83BD927u, 0x6A243BCBu, 0x509B84C9u) +
                        asuint(v.c3) * uint3(0x91D13847u, 0x52F7230Fu, 0xCF286E83u)) + 0xE121E6ADu;
        }

        /// <summary>
        /// Returns a uint3 vector hash code of a float3x4 matrix.
        /// When multiple elements are to be hashes together, it can more efficient to calculate and combine wide hash
        /// that are only reduced to a narrow uint hash at the very end instead of at every step.
        /// </summary>
        /// <param name="v">Matrix value to hash.</param>
        /// <returns>uint3 hash of the argument.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint3 hashwide(float3x4 v)
        {
            return (asuint(v.c0) * uint3(0xC9CA1249u, 0x69B60C81u, 0xE0EB6C25u) +
                    asuint(v.c1) * uint3(0xF648BEABu, 0x6BDB2B07u, 0xEF63C699u) +
                    asuint(v.c2) * uint3(0x9001903Fu, 0xA895B9CDu, 0x9D23B201u) +
                    asuint(v.c3) * uint3(0x4B01D3E1u, 0x7461CA0Du, 0x79725379u)) + 0xD6258E5Bu;
        }

    }
}
