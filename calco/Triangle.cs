using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using static calco.math;

namespace calco
{
	[Serializable]
	[Il2CppEagerStaticClassConstruction]
	public struct Triangle : IEquatable<Triangle>
	{
		public float3 v0;
		public float3 v1;
		public float3 v2;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Triangle(in float3 v0, in float3 v1, in float3 v2)
		{
			this.v0 = v0;
			this.v1 = v1;
			this.v2 = v2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(Triangle other)
		{
			return v0.Equals(other.v0) && v1.Equals(other.v1) && v2.Equals(other.v2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override string ToString()
		{
			return string.Format("Triangle({0}, {1}, {2})", v0, v1, v2);
		}
	}

	public static partial class math
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Triangle Triangle(in float3 v0, in float3 v1, in float3 v2) { return new Triangle(v0, v1, v2); }
	}
}
