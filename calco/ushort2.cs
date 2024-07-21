using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Unity.IL2CPP.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable 0660, 0661

namespace calco
{
    [DebuggerTypeProxy(typeof(ushort2.DebuggerProxy))]
    [System.Serializable]
    [Il2CppEagerStaticClassConstruction]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ushort2 : System.IEquatable<ushort2>, IFormattable
    {
        public ushort x;
        public ushort y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort2(ushort x, ushort y)
        {
            this.x = x;
            this.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort2(ushort2 xyzw)
        {
            this.x = xyzw.x;
            this.y = xyzw.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort2(ushort v)
        {
            this.x = v;
            this.y = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator ushort2(ushort v) { return new ushort2(v); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator == (ushort2 lhs, ushort2 rhs) { return new bool2 (lhs.x == rhs.x, lhs.y == rhs.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator == (ushort2 lhs, ushort rhs) { return new bool2 (lhs.x == rhs, lhs.y == rhs); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator == (ushort lhs, ushort2 rhs) { return new bool2 (lhs == rhs.x, lhs == rhs.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator != (ushort2 lhs, ushort2 rhs) { return new bool2 (lhs.x != rhs.x, lhs.y != rhs.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator != (ushort2 lhs, ushort rhs) { return new bool2 (lhs.x != rhs, lhs.y != rhs); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator != (ushort lhs, ushort2 rhs) { return new bool2 (lhs != rhs.x, lhs != rhs.y); }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly ushort2 xx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new ushort2(x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly ushort2 xy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new ushort2(x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly ushort2 yy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new ushort2(y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly ushort2 yx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new ushort2(y, x); }
        }

        unsafe public ushort this[int index]
        {
            readonly get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (ushort2* array = &this) { return ((ushort*)array)[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (ushort* array = &x) { array[index] = value; }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(ushort2 rhs) { return x == rhs.x && y == rhs.y;}
        public readonly override bool Equals(object o) { return o is ushort2 converted && Equals(converted); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override int GetHashCode() { return (int)math.hash(this); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override string ToString()
        {
            return string.Format("ushort2({0}, {1})", x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("ushort2({0}, {1})", x.ToString(format, formatProvider), y.ToString(format, formatProvider));
        }

        internal sealed class DebuggerProxy
        {
            public ushort x;
            public ushort y;
            public DebuggerProxy(ushort2 v)
            {
                x = v.x;
                y = v.y;
            }
        }
    }

    public static partial class math
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort2 ushort2(ushort x, ushort y) { return new ushort2(x, y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort2 ushort2(ushort2 xy) { return new ushort2(xy); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort2 ushort2(ushort v) { return new ushort2(v); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort2 ushort2(float2 v) { return new ushort2((ushort)v.x, (ushort)v.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort2 ushort2n(float2 v) { return new ushort2(floatToUnorm16(v.x), floatToUnorm16(v.y)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 uint2(ushort2 v) { return new uint2(v.x, v.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort2 ushort2FromPacked(uint xy)
        {
            unsafe
            {
                 return *(ushort2*)&xy;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint asuint(ushort2 x)
        {
            unsafe
            {
                 return *(uint*)&x;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(ushort2 v)
        {
            return csum(uint2((uint)v.x, (uint)v.y) * uint2(0x4473BBB1u, 0xCBA11D5Fu)) + 0x685835CFu;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 hashwide(ushort2 v)
        {
            return (uint2((uint)v.x, (uint)v.y) * uint2(0xC3D32AE1u, 0xB966942Fu)) + 0xFE9856B3u;
        }
    }
}
