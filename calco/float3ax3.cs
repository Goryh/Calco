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
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;

#pragma warning disable 0660, 0661

namespace calco
{
	[Il2CppEagerStaticClassConstruction]
	public static class float3ax3c
	{
		public static float3ax3 identity => new float3ax3(float3c.right, float3c.up, float3c.forward);
		public static float3ax3 zero => new float3ax3(float3c.zero, float3c.zero, float3c.zero);
	}

	[Il2CppEagerStaticClassConstruction]
	public ref partial struct float3ax3
	{
		public float3a c0;
		public float3a c1;
		public float3a c2;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3ax3(float3a c0, float3a c1, float3a c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3ax3(in float3x3 m)
        {
			return new float3ax3(m.c0, m.c1, m.c2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x3(in float3ax3 m)
        {
			return new float3x3(m.c0, m.c1, m.c2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3ax3 operator * (float3ax3 lhs, float rhs) { return new float3ax3 (lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3ax3 operator * (float lhs, float3ax3 rhs) { return new float3ax3 (lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2); }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3ax3 operator + (float3ax3 lhs, float3ax3 rhs) { return new float3ax3 (lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3ax3 operator - (float3ax3 lhs, float3ax3 rhs) { return new float3ax3 (lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2); }

		unsafe public readonly ref float3a this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
				if ((uint)index >= 3)
					throw new System.ArgumentException("index must be between[0...2]");
#endif
				fixed (float3ax3* array = &this) { return ref ((float3a*)array)[index]; }
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float3x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f)", c0.x, c1.x, c2.x, c0.y, c1.y, c2.y, c0.z, c1.z, c2.z);
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3ax3 float3ax3(float3a c0, float3a c1, float3a c2) { return new float3ax3(c0, c1, c2); }

		[MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
		static extern void vecILMathFloat3ax3Transpose(in float3ax3 m, out float3ax3 res);
		[MethodImpl(MethodImplOptions.AggressiveInlining), DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
		static extern void vecILMathFloat3ax3Inverse(in float3ax3 m, out float3ax3 res);

		/// <summary>Return the float3x3 transpose of a float3x3 matrix.</summary>
		/// <param name="v">Value to transpose.</param>
		/// <returns>Transposed value.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3ax3 transpose(in float3ax3 m)
		{
		 #if ENABLE_IL2CPP
			if( !IsBurstEnabled() )
			{
				vecILMathFloat3ax3Transpose(in m, out var res);
				return res;
			}
		#endif
			return float3ax3(
				float3(m.c0.x, m.c0.y, m.c0.z),
				float3(m.c1.x, m.c1.y, m.c1.z),
				float3(m.c2.x, m.c2.y, m.c2.z));
		}

		/// <summary>Returns the float3x3 full inverse of a float3x3 matrix.</summary>
		/// <param name="m">Matrix to invert.</param>
		/// <returns>The inverted matrix.</returns>
		public static float3ax3 inverse(in float3ax3 m)
		{
		 #if ENABLE_IL2CPP
			if( !IsBurstEnabled() )
			{
				vecILMathFloat3ax3Inverse(in m, out var res);
				return res;
			}
		#endif
			float3 c0 = m.c0;
			float3 c1 = m.c1;
			float3 c2 = m.c2;

			float3 t0 = float3(c1.x, c2.x, c0.x);
			float3 t1 = float3(c1.y, c2.y, c0.y);
			float3 t2 = float3(c1.z, c2.z, c0.z);

			float3 m0 = t1 * t2.yzx - t1.yzx * t2;
			float3 m1 = t0.yzx * t2 - t0 * t2.yzx;
			float3 m2 = t0 * t1.yzx - t0.yzx * t1;

			float rcpDet = 1.0f / csum(t0.zxy * m0);
			return float3ax3(m0, m1, m2) * rcpDet;
		}

		/// <summary>Returns the determinant of a float3x3 matrix.</summary>
		/// <param name="m">Matrix to use when computing determinant.</param>
		/// <returns>The determinant of the matrix.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float determinant(in float3ax3 m)
		{
			var c0 = m.c0;
			var c1 = m.c1;
			var c2 = m.c2;

			float m00 = c1.y * c2.z - c1.z * c2.y;
			float m01 = c0.y * c2.z - c0.z * c2.y;
			float m02 = c0.y * c1.z - c0.z * c1.y;

			return c0.x * m00 - c1.x * m01 + c2.x * m02;
		}
	}
}
