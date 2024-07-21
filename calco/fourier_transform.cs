using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using static Unity.Mathematics.math;

namespace Unity.Mathematics
{
	public enum FourierTransformDirection
	{
		// forward direction of Fourier transformation.
		Forward = 1,
		// backward direction of Fourier transformation.
		Inverse = -1
	}

	public static class DiscreteFourierTransform
	{
		// one dimensional Discrete Fourier Transform with fast sin-cos
		public static void DFT(ReadOnlySpan<float2> complexIn, Span<float2> complexOut, FourierTransformDirection direction)
		{
			int	n = complexIn.Length;

			for( int i = 0; i < n; i++ )
			{
				complexOut[i] = float2c.zero;

				float arg = -(int)direction * PI2 * i / n;

				// sum source elements
				for( int j = 0; j < n; j++ )
				{
					sincos(j * arg, out float s, out float c);

					complexOut[i].x += complexIn[j].x * c - complexIn[j].y * s;
					complexOut[i].y += complexIn[j].x * s + complexIn[j].y * c;
				}

				// divide for forward transform
				if( direction == FourierTransformDirection.Forward )
					complexOut[i] *= 1.0f / n;
			}
		}

		// one dimensional Discrete Fourier Transform with precise sin-cos
		public static void DFTprecise(ReadOnlySpan<float2> complexIn, Span<float2> complexOut, FourierTransformDirection direction)
		{
			int	n = complexIn.Length;

			for( int i = 0; i < n; i++ )
			{
				complexOut[i] = float2c.zero;

				float arg = - (int)direction * PI2 * i / n;

				// sum source elements
				for( int j = 0; j < n; j++ )
				{
					sincosprecise(j * arg, out float s, out float c);

					complexOut[i].x += complexIn[j].x * c - complexIn[j].y * s;
					complexOut[i].y += complexIn[j].x * s + complexIn[j].y * c;
				}

				// divide for forward transform
				if( direction == FourierTransformDirection.Forward )
					complexOut[i] *= 1.0f / n;
			}
		}

		// two dimensional Discrete Fourier Transform with fast sin-cos
		public static void DFT2(float2[,] complexInOut, float2[,] auxiliary, FourierTransformDirection direction)
		{
			int n = complexInOut.GetLength(0);	// rows
			int m = complexInOut.GetLength(1);	// columns

			// process rows
			for( int i = 0; i < n; i++ )
			{
				for( int j = 0; j < m; j++ )
				{
					auxiliary[i, j] = float2c.zero;

					float arg = - (int)direction * PI2 * j / m;

					// sum source elements
					for( int k = 0; k < m; k++ )
					{
						sincos(j * arg, out float s, out float c);

						auxiliary[i, j].x += complexInOut[i, k].x * c - complexInOut[i, k].y * s;
						auxiliary[i, j].y += complexInOut[i, k].x * s + complexInOut[i, k].y * c;
					}

					// divide also for forward transform
					if( direction == FourierTransformDirection.Forward )
						auxiliary[i, j] *= 1.0f / m;
				}
			}

			// process columns
			for( int j = 0; j < m; j++ )
			{
				for( int i = 0; i < n; i++ )
				{
					complexInOut[i, j] = float2c.zero;

					float arg = - (int)direction * PI2 * j / m;

					// sum source elements
					for( int k = 0; k < n; k++ )
					{
						sincos(j * arg, out float s, out float c);

						complexInOut[i, j].x += auxiliary[k, j].x * c - auxiliary[k, j].y * s;
						complexInOut[i, j].y += auxiliary[k, j].x * s + auxiliary[k, j].y * c;
					}

					// divide also for forward transform
					if( direction == FourierTransformDirection.Forward )
						complexInOut[i, j] *= 1.0f / n;
				}
			}
		}

		// two dimensional Discrete Fourier Transform with precise sin-cos
		public static void DFT2precise(float2[,] complexInOut, float2[,] auxiliary, FourierTransformDirection direction)
		{
			int n = complexInOut.GetLength(0);	// rows
			int m = complexInOut.GetLength(1);	// columns

			// process rows
			for( int i = 0; i < n; i++ )
			{
				for( int j = 0; j < m; j++ )
				{
					auxiliary[i, j] = float2c.zero;

					float arg = - (int)direction * PI2 * j / m;

					// sum source elements
					for( int k = 0; k < m; k++ )
					{
						sincosprecise(j * arg, out float s, out float c);

						auxiliary[i, j].x += complexInOut[i, k].x * c - complexInOut[i, k].y * s;
						auxiliary[i, j].y += complexInOut[i, k].x * s + complexInOut[i, k].y * c;
					}

					// divide also for forward transform
					if( direction == FourierTransformDirection.Forward )
						auxiliary[i, j] *= 1.0f / m;
				}
			}

			// process columns
			for( int j = 0; j < m; j++ )
			{
				for( int i = 0; i < n; i++ )
				{
					complexInOut[i, j] = float2c.zero;

					float arg = - (int)direction * PI2 * j / m;

					// sum source elements
					for( int k = 0; k < n; k++ )
					{
						sincosprecise(j * arg, out float s, out float c);

						complexInOut[i, j].x += auxiliary[k, j].x * c - auxiliary[k, j].y * s;
						complexInOut[i, j].y += auxiliary[k, j].x * s + auxiliary[k, j].y * c;
					}

					// divide also for forward transform
					if( direction == FourierTransformDirection.Forward )
						complexInOut[i, j] *= 1.0f / n;
				}
			}
		}
	}

	// Fast Fourier Transform should produce the same results as the Discrete one but way faster.
	// But it requires pow-2 array and some precalculated data
	[Serializable]
	public class FastFourierTransform
	{
		readonly int[]			reversedBits;
		readonly float2[,][]	complexRotations;

		public FastFourierTransform(uint maxDimensionSize)
		{
#if UNITY_ASSERTIONS
			if( maxDimensionSize < 2 || !ispow2(maxDimensionSize) )
				throw new System.ArgumentException("data must be a pow of 2");
#endif
			var numberOfBits = ceillog2(maxDimensionSize);			
			reversedBits = new int[maxDimensionSize];

			for( int i = 0; i < maxDimensionSize; i++ )
			{
				int oldBits = i;
				int newBits = 0;

				for( int j = 0; j < numberOfBits; j++ )
				{
					newBits = ( newBits << 1 ) | ( oldBits & 1 );
					oldBits >>= 1;
				}
				reversedBits[i] = newBits;
			}

			complexRotations = new float2[2, numberOfBits][];
			for( int numBits = 1; numBits <= numberOfBits; ++numBits )
			{
				PrecalculateComplexRotation(numBits, FourierTransformDirection.Forward);
				PrecalculateComplexRotation(numBits, FourierTransformDirection.Inverse);
			}
		}

		void PrecalculateComplexRotation(int numberOfBits, FourierTransformDirection direction)
		{
			int directionIndex = direction == FourierTransformDirection.Forward ? 0 : 1;

			int		n = 1 << (numberOfBits - 1);
			float	uR = 1.0f;
			float	uI = 0.0f;
			float	angle = PI / n * (int)direction;
			sincosprecise(angle, out float wI, out float wR);
			float2[]	rotations = new float2[n];

			for( int i = 0; i < n; ++i )
			{
				rotations[i] = float2(uR, uI);

				float newI = uR * wI + uI * wR;
				uR = uR * wR - uI * wI;
				uI = newI;
			}

			complexRotations[directionIndex, numberOfBits - 1] = rotations;
		}

		void ReorderData(Span<float2> data)
		{
			int len = data.Length;

			for( int i = 0; i < len; i++ )
			{
				int s = reversedBits[i];

				if( s > i )
				{
					var t = data[i];
					data[i] = data[s];
					data[s] = t;
				}
			}
		}

		void ReorderData(ReadOnlySpan<float2> dataIn, Span<float2> dataOut)
		{
			int len = dataIn.Length;

			for( int i = 0; i < len; i++ )
			{
				int s = reversedBits[i];
				dataOut[i] = dataIn[s];
			}
		}

		// one dimensional Fast Fourier Transform
		public void FFT(ReadOnlySpan<float2> complexIn, Span<float2> complexOut, FourierTransformDirection direction)
		{
			int	n = complexIn.Length;

#if UNITY_ASSERTIONS
			if( n != reversedBits.Length )
				throw new System.ArgumentException("data size must be the same as was precalculated for");
#endif
			var m = ceillog2(n);			

			ReorderData(complexIn, complexOut);

			int directionIndex = direction == FourierTransformDirection.Forward ? 0 : 1;
			int tn = 1, tm;
			for( int k = 1; k <= m; ++k )
			{
				var rotations = complexRotations[directionIndex, k - 1];

				tm = tn;
				tn *= 2;

				for( int i = 0; i < tm; ++i )
				{
					var t = rotations[i];

					for( int even = i; even < n; even += tn )
					{
						int	odd = even + tm;
						var	ce = complexOut[even];
						var	co = complexOut[odd];

						float tr = co.x * t.x - co.y * t.y;
						float ti = co.x * t.y + co.y * t.x;

						complexOut[even].x += tr;
						complexOut[even].y += ti;

						complexOut[odd].x = ce.x - tr;
						complexOut[odd].y = ce.y - ti;
					}
				}
			}

			if ( direction == FourierTransformDirection.Forward ) 
			{
				for( int i = 0; i < n; ++i )
					complexOut[i] *= 1.0f / n;
			}
		}

		// one dimensional Fast Fourier Transform
		public void FFT(Span<float2> complexInOut, FourierTransformDirection direction)
		{
			int	n = complexInOut.Length;

#if UNITY_ASSERTIONS
			if( n != reversedBits.Length )
				throw new System.ArgumentException("data size must be the same as was precalculated for");
#endif
			var m = ceillog2(n);			

			ReorderData(complexInOut);

			int directionIndex = direction == FourierTransformDirection.Forward ? 0 : 1;
			int tn = 1, tm;
			for( int k = 1; k <= m; ++k )
			{
				var rotations = complexRotations[directionIndex, k - 1];

				tm = tn;
				tn *= 2;

				for( int i = 0; i < tm; ++i )
				{
					var t = rotations[i];

					for( int even = i; even < n; even += tn )
					{
						int	odd = even + tm;
						var	ce = complexInOut[even];
						var	co = complexInOut[odd];

						float tr = co.x * t.x - co.y * t.y;
						float ti = co.x * t.y + co.y * t.x;

						complexInOut[even].x += tr;
						complexInOut[even].y += ti;

						complexInOut[odd].x = ce.x - tr;
						complexInOut[odd].y = ce.y - ti;
					}
				}
			}

			if ( direction == FourierTransformDirection.Forward ) 
			{
				for( int i = 0; i < n; ++i )
					complexInOut[i] *= 1.0f / n;
			}
		}

		public void FFT2(float2[,] complexInOut, float2[] auxiliary, FourierTransformDirection direction)
		{
			int k = complexInOut.GetLength( 0 );
			int n = complexInOut.GetLength( 1 );

#if UNITY_ASSERTIONS
			if( n != reversedBits.Length || k != reversedBits.Length )
				throw new System.ArgumentException("data size must be the same as was precalculated for, only squared are supported for now");

			if( auxiliary.Length < max(k, n) )
				throw new System.ArgumentException("auxiliary data size must fit the larger dimension");
#endif
			for( int i = 0; i < k; i++ )
			{
				// copy row
				for( int j = 0; j < n; j++ )
					auxiliary[j] = complexInOut[i, j];

				// transform it
				FFT(auxiliary, direction);

				// copy back
				for ( int j = 0; j < n; j++ )
					complexInOut[i, j] = auxiliary[j];
			}

			for( int j = 0; j < n; j++ )
			{
				// copy column
				for( int i = 0; i < k; i++ )
					auxiliary[i] = complexInOut[i, j];

				// transform it
				FFT(auxiliary, direction);

				// copy back
				for( int i = 0; i < k; i++ )
					complexInOut[i, j] = auxiliary[i];
			}
		}
	}
}
