using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Unity.IL2CPP.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [DebuggerTypeProxy(typeof(short2.DebuggerProxy))]
    [System.Serializable]
    [Il2CppEagerStaticClassConstruction]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct short2 : System.IEquatable<short2>, IFormattable
    {
        public short x;
        public short y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short2(short x, short y)
        {
            this.x = x;
            this.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short2(short2 xyzw)
        {
            this.x = xyzw.x;
            this.y = xyzw.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short2(short v)
        {
            this.x = v;
            this.y = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator short2(short v) { return new short2(v); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator == (short2 lhs, short2 rhs) { return new bool2 (lhs.x == rhs.x, lhs.y == rhs.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator == (short2 lhs, short rhs) { return new bool2 (lhs.x == rhs, lhs.y == rhs); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator == (short lhs, short2 rhs) { return new bool2 (lhs == rhs.x, lhs == rhs.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator != (short2 lhs, short2 rhs) { return new bool2 (lhs.x != rhs.x, lhs.y != rhs.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator != (short2 lhs, short rhs) { return new bool2 (lhs.x != rhs, lhs.y != rhs); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator != (short lhs, short2 rhs) { return new bool2 (lhs != rhs.x, lhs != rhs.y); }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short2 xx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short2(x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short2 xy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short2(x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short2 yy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short2(y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short2 yx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short2(y, x); }
        }

        unsafe public short this[int index]
        {
            readonly get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (short2* array = &this) { return ((short*)array)[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (short* array = &x) { array[index] = value; }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(short2 rhs) { return x == rhs.x && y == rhs.y;}
        public readonly override bool Equals(object o) { return o is short2 converted && Equals(converted); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override int GetHashCode() { return (int)math.hash(this); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override string ToString()
        {
            return string.Format("short2({0}, {1})", x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("short2({0}, {1})", x.ToString(format, formatProvider), y.ToString(format, formatProvider));
        }

        internal sealed class DebuggerProxy
        {
            public short x;
            public short y;
            public DebuggerProxy(short2 v)
            {
                x = v.x;
                y = v.y;
            }
        }
    }

    public static partial class math
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short2 short2(short x, short y) { return new short2(x, y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short2 short2(short2 xy) { return new short2(xy); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short2 short2(short v) { return new short2(v); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short2 short2(float2 v) { return new short2((short)v.x, (short)v.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short2 short2n(float2 v) { return new short2(floatToSnorm16(v.x), floatToSnorm16(v.y)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(short2 v)
        {
            return csum(uint2((uint)v.x, (uint)v.y) * uint2(0x4473BBB1u, 0xCBA11D5Fu)) + 0x685835CFu;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 hashwide(short2 v)
        {
            return (uint2((uint)v.x, (uint)v.y) * uint2(0xC3D32AE1u, 0xB966942Fu)) + 0xFE9856B3u;
        }
    }
}
