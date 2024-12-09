using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	// these functions are C# copy of the corresponding vecMath.h routines to properly emulate calculation precision on the c# side as will be in the IL2CPP builds
	// this should also be faster the System.Math analogues
	[Il2CppEagerStaticClassConstruction]
	internal static class systemmath_emulation
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float poly0(float x, float c0)		=> c0;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float poly1(float x, float c0, float c1)		=> poly0(x, c1) * x + c0;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float poly2(float x, float c0, float c1, float c2)		=> poly1(x, c1, c2) * x + c0;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float poly3(float x, float c0, float c1, float c2, float c3)		=> poly2(x, c1, c2, c3) * x + c0;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float poly4(float x, float c0, float c1, float c2, float c3, float c4)		=> poly3(x, c1, c2, c3, c4) * x + c0;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float poly5(float x, float c0, float c1, float c2, float c3, float c4, float c5)	=> poly4(x, c1, c2, c3, c4, c5) * x + c0;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static (float, float) ExpDef(float x)
		{
			x = min(x, 120.0f);
			x = max(x, -126.99999f);
			int i = roundtoi(x - 0.5f);
			float f = x - i;
			float e = asfloat((i + 127) << 23);
			return (f, e);
		}

		// relative error is < 1e-7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp2EstP5Int(float x)
		{
			(float f, float e) = ExpDef(x);
			float expfpart = poly5(f, 9.9999994e-1f, 6.9315308e-1f, 2.4015361e-1f, 5.5826318e-2f, 8.9893397e-3f, 1.8775767e-3f);
			return select(e * expfpart, e, f == 0.0f); //ensure that exp2(int) = 2^int
		}

		// relative error is < 1e-7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp2EstP5(float x)
		{
			(float f, float e) = ExpDef(x);
			float expfpart = poly5(f, 9.9999994e-1f, 6.9315308e-1f, 2.4015361e-1f, 5.5826318e-2f, 8.9893397e-3f, 1.8775767e-3f);
			return e * expfpart;
		}

		// relative error is < 2.6e-6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp2EstP4(float x)
		{
			(float f, float e) = ExpDef(x);
			float expfpart = poly4(f, 1.0000026f, 6.9300383e-1f, 2.4144275e-1f, 5.2011464e-2f, 1.3534167e-2f);
			return e * expfpart;
		}

		// relative error is < 1e-4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp2EstP3(float x)
		{
			(float f, float e) = ExpDef(x);
			float expfpart = poly3(f, 9.9992520e-1f, 6.9583356e-1f, 2.2606716e-1f, 7.8024521e-2f);
			return e * expfpart;
		}

		// relative error is < 2e-3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp2EstP2(float x)
		{
			(float f, float e) = ExpDef(x);
			float expfpart = poly2(f, 1.0017247f, 6.5763628e-1f, 3.3718944e-1f);
			return e * expfpart;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static (float, float) LogDef(float x)
		{
			int i = asint(x);
			float e = (float)(((i & 0x7F800000) >> 23) - 127);
			float m = asfloat(i & 0x007FFFFF | 0x3f800000);
			return (m, e);
		}

		// relative error is < 1.0e-5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log2EstP5(float x)
		{
			(float m, float e) = LogDef(x);
			float p = poly5(m, 3.1157899f, -3.3241990f, 2.5988452f, -1.2315303f,  3.1821337e-1f, -3.4436006e-2f);
			return p * (m - 1) + e;
		}
		
		// relative error is < 5.7e-5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log2EstP4(float x)
		{
			(float m, float e) = LogDef(x);
			float p = poly4(m, 2.8882704548164776201f, -2.52074962577807006663f, 1.48116647521213171641f, -0.465725644288844778798f, 0.0596515482674574969533f);
			return p * (m - 1) + e;
		}

		// relative error is < 4e-4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log2EstP3(float x)
		{
			(float m, float e) = LogDef(x);
			float p = poly3(m, 2.61761038894603480148f, -1.75647175389045657003f, 0.688243882994381274313f, -0.107254423828329604454f);
			return p * (m - 1) + e;
		}

		// relative error is < 3e-3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log2EstP2(float x)
		{
			(float m, float e) = LogDef(x);
			float p = poly2(m, 2.28330284476918490682f, -1.04913055217340124191f, 0.204446009836232697516f);
			return p * (m - 1) + e;
		}

		// relative error is < 1e-5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log(float x)
		{
			return log2EstP5(x) * 0.6931471805599453f; // 1/log2(e)
		}

		// relative error is < 4e-4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float logfast(float x)
		{
			return log2EstP3(x) * 0.6931471805599453f; // 1/log2(e)
		}

		// relative error is < 1e-5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log2(float x)
		{
			return log2EstP5(x);
		}

		// relative error is < 4e-4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log2fast(float x)
		{
			return log2EstP3(x);
		}

		// relative error is < 1e-5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log10(float x)
		{
			return log2EstP5(x) * 0.301029995664f; // 1/log2(10)
		}

		// relative error is < 4e-4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log10fast(float x)
		{
			return log2EstP3(x) * 0.301029995664f; // 1/log2(10)
		}

		// relative error is < 1e-7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp(float x)
		{
			return exp2EstP5(x * 1.4426950408889634073599f); // log2(e)
		}

		// relative error is < 1e-4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float expfast(float x)
		{
			return exp2EstP3(x * 1.4426950408889634073599f); // log2(e)
		}

		// relative error is < 1e-7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp2(float x)
		{
			return exp2EstP5Int(x);
		}

		// relative error is < 1e-4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp2fast(float x)
		{
			return exp2EstP3(x);
		}

		// relative error is < 1e-7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp10(float x)
		{
			return exp2EstP5(x * 3.321928094887f); // log2(10)
		}

		// relative error is < 1e-4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp10fast(float x)
		{
			return exp2EstP3(x * 3.321928094887f); // log2(10)
		}

		// relative errors:
		//		<3e-5 for x range [0.1, 100000] and y range [-5; 5]
		//		<6e-5 for x range [0.1, 1000]   and y range [-10; 10]
		//		<9e-5 for x range [0.1, 100]    and y range [-15; 15]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float pow(float x, float y)
		{
			float ret = exp2EstP5(log2EstP5(x) * y);
			ret = select(ret, 1, y == 0.0f);
			return ret;
		}

		// relative errors:
		//		<1.4e-3 for x range [0.1, 100000] and y range [-5; 5]
		//		<3e-3   for x range [0.1, 1000]   and y range [-10; 10]
		//		<4e-3   for x range [0.1, 100]    and y range [-15; 15]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float powfast(float x, float y)
		{
			float ret = exp2EstP3(log2EstP3(x) * y);
			return ret;
		}
	}
}