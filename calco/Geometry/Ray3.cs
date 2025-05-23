using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	[Serializable]
	[Il2CppEagerStaticClassConstruction]
	public partial struct Ray3 : IEquatable<Ray3>
	{
		public float3 origin;
		public float3 dir;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Ray3(in float3a origin, in float3a dir)
		{
			this.origin = origin;
			this.dir = dir;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(Ray3 other)
		{
			return origin.Equals(other.origin) && dir.Equals(other.dir);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("Ray3({0}, {1})", origin, dir);
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Ray3 Ray3(in float3a origin, in float3a dir) => new Ray3(origin, dir);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnityEngine.Ray Ray(in Ray3 r) { return new UnityEngine.Ray(r.origin, r.dir); }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray3 Ray3(in UnityEngine.Ray r) { return new Ray3(r.origin, r.direction); }
	}
}