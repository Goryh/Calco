using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Unity.IL2CPP.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [DebuggerTypeProxy(typeof(short4.DebuggerProxy))]
    [System.Serializable]
    [Il2CppEagerStaticClassConstruction]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct short4 : System.IEquatable<short4>, IFormattable
    {
        public short x;
        public short y;
        public short z;
        public short w;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short4(short x, short y, short z, short w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short4(short4 xyzw)
        {
            this.x = xyzw.x;
            this.y = xyzw.y;
            this.z = xyzw.z;
            this.w = xyzw.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short4(short v)
        {
            this.x = v;
            this.y = v;
            this.z = v;
            this.w = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator short4(short v) { return new short4(v); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4 operator == (short4 lhs, short4 rhs) { return new bool4 (lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4 operator == (short4 lhs, short rhs) { return new bool4 (lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4 operator == (short lhs, short4 rhs) { return new bool4 (lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4 operator != (short4 lhs, short4 rhs) { return new bool4 (lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4 operator != (short4 lhs, short rhs) { return new bool4 (lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4 operator != (short lhs, short4 rhs) { return new bool4 (lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w); }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xxww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, x, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 xyzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(x, y, z, w); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; y = value.y; z = value.z; w = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xywx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xywy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 xywz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(x, y, w, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; y = value.y; w = value.z; z = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xyww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, y, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 xzyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(x, z, y, w); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; z = value.y; y = value.z; w = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 xzwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(x, z, w, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; z = value.y; w = value.z; y = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xzww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, z, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 xwyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(x, w, y, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; w = value.y; y = value.z; z = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 xwzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(x, w, z, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; w = value.y; z = value.z; y = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 xwww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(x, w, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 yxzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(y, x, z, w); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; x = value.y; z = value.z; w = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 yxwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(y, x, w, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; x = value.y; w = value.z; z = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yxww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, x, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yywx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yywy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yywz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yyww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, y, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 yzxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(y, z, x, w); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; z = value.y; x = value.z; w = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 yzwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(y, z, w, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; z = value.y; w = value.z; x = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 yzww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, z, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 ywxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(y, w, x, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; w = value.y; x = value.z; z = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 ywzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(y, w, z, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; w = value.y; z = value.z; x = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 ywww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(y, w, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 zxyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(z, x, y, w); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; x = value.y; y = value.z; w = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 zxwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(z, x, w, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; x = value.y; w = value.z; y = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zxww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, x, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 zyxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(z, y, x, w); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; y = value.y; x = value.z; w = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 zywx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(z, y, w, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; y = value.y; w = value.z; x = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zywy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zywz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zyww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, y, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zzww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, z, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 zwxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(z, w, x, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; w = value.y; x = value.z; y = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 zwyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(z, w, y, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { z = value.x; w = value.y; y = value.z; x = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 zwww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(z, w, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 wxyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(w, x, y, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { w = value.x; x = value.y; y = value.z; z = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 wxzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(w, x, z, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { w = value.x; x = value.y; z = value.z; y = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wxww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, x, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 wyxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(w, y, x, z); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { w = value.x; y = value.y; x = value.z; z = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wyxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wyyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wyyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 wyzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(w, y, z, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { w = value.x; y = value.y; z = value.z; x = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wyzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wyzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wyzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wywx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wywy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wywz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wyww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, y, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 wzxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(w, z, x, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { w = value.x; z = value.y; x = value.z; y = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public short4 wzyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get { return new short4(w, z, y, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { w = value.x; z = value.y; y = value.z; x = value.w; }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wzww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, z, w, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, x, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, x, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwxz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, x, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwxw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, x, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, y, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, y, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwyz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, y, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwyw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, y, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwzx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, z, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwzy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, z, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwzz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, z, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwzw
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, z, w); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwwx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, w, x); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwwy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, w, y); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwwz
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, w, z); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public readonly short4 wwww
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new short4(w, w, w, w); }
        }

        unsafe public short this[int index]
        {
            readonly get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 4)
                    throw new System.ArgumentException("index must be between[0...3]");
#endif
                fixed (short4* array = &this) { return ((short*)array)[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 4)
                    throw new System.ArgumentException("index must be between[0...3]");
#endif
                fixed (short* array = &x) { array[index] = value; }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(short4 rhs) { return x == rhs.x && y == rhs.y && z == rhs.z && w == rhs.w; }
        public readonly override bool Equals(object o) { return o is short4 converted && Equals(converted); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override int GetHashCode() { return (int)math.hash(this); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly override string ToString()
        {
            return string.Format("short4({0}, {1}, {2}, {3})", x, y, z, w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("short4({0}, {1}, {2}, {3})", x.ToString(format, formatProvider), y.ToString(format, formatProvider), z.ToString(format, formatProvider), w.ToString(format, formatProvider));
        }

        internal sealed class DebuggerProxy
        {
            public short x;
            public short y;
            public short z;
            public short w;
            public DebuggerProxy(short4 v)
            {
                x = v.x;
                y = v.y;
                z = v.z;
                w = v.w;
            }
        }
    }

    public static partial class math
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short floatToSnorm16(float v)
        {
            // according to D3D10 rules, the value "-1.0f" has two representations:
            //  0x1000 and 0x10001
            // this allows everyone to convert by just multiplying by 32767 instead
            // of multiplying the negative values by 32768 and 32767 for positive.
            return (short)clamp(v >= 0.0f ? (v * 32767.0f + 0.5f) : (v * 32767.0f - 0.5f), -32768.0f, 32767.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short4 short4(short x, short y, short z, short w) { return new short4(x, y, z, w); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short4 short4(short4 xyzw) { return new short4(xyzw); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short4 short4(short v) { return new short4(v); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short4 short4(float4 v) { return new short4((short)v.x, (short)v.y, (short)v.z, (short)v.w); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short4 short4n(float4 v) { return new short4(floatToSnorm16(v.x), floatToSnorm16(v.y), floatToSnorm16(v.z), floatToSnorm16(v.w)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(short4 v)
        {
            return csum(uint4((uint)v.x, (uint)v.y, (uint)v.z, (uint)v.w) * uint4(0x745ED837u, 0x9CDC88F5u, 0xFA62D721u, 0x7E4DB1CFu)) + 0x68EEE0F5u;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 hashwide(short4 v)
        {
            return (uint4((uint)v.x, (uint)v.y, (uint)v.z, (uint)v.w) * uint4(0xBC3B0A59u, 0x816EFB5Du, 0xA24E82B7u, 0x45A22087u)) + 0xFC104C3Bu;
        }
    }
}
