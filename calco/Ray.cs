using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static Unity.Mathematics.math;

namespace Unity.Mathematics
{
	[Serializable]
	[Il2CppEagerStaticClassConstruction]
	public partial struct Ray3d : IEquatable<Ray3d>
	{
		public float3 origin;
		public float3 dir;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Ray3d(in float3a origin, in float3a dir)
		{
			this.origin = origin;
			this.dir = dir;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(Ray3d other)
		{
			return origin.Equals(other.origin) && dir.Equals(other.dir);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("Ray({0}, {1})", origin, dir);
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Ray3d Ray3d(in float3a origin, in float3a dir) => new Ray3d(origin, dir);
	}
}