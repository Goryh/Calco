#include <xmmintrin.h>
#include <emmintrin.h>
#include <pmmintrin.h>
#include <smmintrin.h>
#include <immintrin.h>

typedef __m128 Vec;
typedef __m128i VecShufMask;
typedef __m128i IntVec;

union Flt32UInt32Union
{
	float f;
	unsigned int i;
};

FORCEINLINE Vec vec(float v)
{
	return _mm_set1_ps(v);
}

FORCEINLINE Vec vec(Vec v)
{
	return v;
}

FORCEINLINE Vec vec1(float v)
{
	return _mm_set1_ps(v);
}

FORCEINLINE Vec vec1(Vec v)
{
	return v;
}

FORCEINLINE Vec vec(float x, float y)
{
	return _mm_setr_ps(x, y, 0.0f, 0.0f);
}

FORCEINLINE Vec vec(float x, float y, float z)
{
	return _mm_setr_ps(x, y, z, 0.0f);
}

FORCEINLINE Vec vec(float x, float y, float z, float w)
{
	return _mm_setr_ps(x, y, z, w);
}

FORCEINLINE Vec vecUnaligned(float* unalignedMemory)
{
	return _mm_loadu_ps(unalignedMemory);
}

FORCEINLINE IntVec intVec(int x)
{
	return _mm_set1_epi32(x);
}

FORCEINLINE IntVec intVec(unsigned int x)
{
	return _mm_set1_epi32((int)x);
}

FORCEINLINE IntVec intVec(int x, int y, int z, int w)
{
	return _mm_setr_epi32(x, y, z, w);
}

FORCEINLINE Vec vecMask(bool val)
{
	Flt32UInt32Union masks[2];
	masks[0].i = 0; masks[1].i = 0xFFFFFFFF;
	return _mm_set1_ps(masks[val ? 1 : 0].f);
}

FORCEINLINE Vec vecSignMaskXYZ()
{
	Flt32UInt32Union mask;
	mask.i = 0x80000000;
	return _mm_set_ps(0.0f, mask.f, mask.f, mask.f);
}

FORCEINLINE Vec vecSignMaskXYZW()
{
	return _mm_castsi128_ps(_mm_set1_epi32(0x80000000));
}

FORCEINLINE Vec vecSignMaskXZ()
{
	Flt32UInt32Union mask;
	mask.i = 0x80000000;
	return _mm_set_ps(0.0f, mask.f, 0.0f, mask.f);
}

FORCEINLINE Vec vecSignMaskYW()
{
	Flt32UInt32Union mask;
	mask.i = 0x80000000;
	return _mm_set_ps(mask.f, 0.0f, mask.f, 0.0f);
}

FORCEINLINE Vec vecSignMask(Vec v)
{
	Vec bitMask = vecSignMaskXYZW();
	return _mm_cmpeq_ps(_mm_and_ps(v, bitMask), bitMask);
}

FORCEINLINE Vec vecZero()
{
	return _mm_setzero_ps();
}

FORCEINLINE IntVec vecZeroInt()
{
	return _mm_setzero_si128();
}

const Vec vecOne = vec(1.0f);
const Vec vecHalf = vec(0.5f);
const Vec vecQuarter = vec(0.25f);
const Vec vecTwo = vec(2.0f);
const Vec vecNegOne = vec(-1.0f);

struct VecMask
{
	enum Mask
	{
		_xxxx = (_MM_SHUFFLE(0, 0, 0, 0) << 16) | 0,
		_yxxx = (_MM_SHUFFLE(0, 0, 0, 1) << 16) | 0,
		_zxxx = (_MM_SHUFFLE(0, 0, 0, 2) << 16) | 0,
		_wxxx = (_MM_SHUFFLE(0, 0, 0, 3) << 16) | 0,
		_xyxx = (_MM_SHUFFLE(0, 0, 1, 0) << 16) | 0,
		_yyxx = (_MM_SHUFFLE(0, 0, 1, 1) << 16) | 0,
		_zyxx = (_MM_SHUFFLE(0, 0, 1, 2) << 16) | 0,
		_wyxx = (_MM_SHUFFLE(0, 0, 1, 3) << 16) | 0,
		_xzxx = (_MM_SHUFFLE(0, 0, 2, 0) << 16) | 0,
		_yzxx = (_MM_SHUFFLE(0, 0, 2, 1) << 16) | 0,
		_zzxx = (_MM_SHUFFLE(0, 0, 2, 2) << 16) | 0,
		_wzxx = (_MM_SHUFFLE(0, 0, 2, 3) << 16) | 0,
		_xwxx = (_MM_SHUFFLE(0, 0, 3, 0) << 16) | 0,
		_ywxx = (_MM_SHUFFLE(0, 0, 3, 1) << 16) | 0,
		_zwxx = (_MM_SHUFFLE(0, 0, 3, 2) << 16) | 0,
		_wwxx = (_MM_SHUFFLE(0, 0, 3, 3) << 16) | 0,
		_xxyx = (_MM_SHUFFLE(0, 1, 0, 0) << 16) | 0,
		_yxyx = (_MM_SHUFFLE(0, 1, 0, 1) << 16) | 0,
		_zxyx = (_MM_SHUFFLE(0, 1, 0, 2) << 16) | 0,
		_wxyx = (_MM_SHUFFLE(0, 1, 0, 3) << 16) | 0,
		_xyyx = (_MM_SHUFFLE(0, 1, 1, 0) << 16) | 0,
		_yyyx = (_MM_SHUFFLE(0, 1, 1, 1) << 16) | 0,
		_zyyx = (_MM_SHUFFLE(0, 1, 1, 2) << 16) | 0,
		_wyyx = (_MM_SHUFFLE(0, 1, 1, 3) << 16) | 0,
		_xzyx = (_MM_SHUFFLE(0, 1, 2, 0) << 16) | 0,
		_yzyx = (_MM_SHUFFLE(0, 1, 2, 1) << 16) | 0,
		_zzyx = (_MM_SHUFFLE(0, 1, 2, 2) << 16) | 0,
		_wzyx = (_MM_SHUFFLE(0, 1, 2, 3) << 16) | 0,
		_xwyx = (_MM_SHUFFLE(0, 1, 3, 0) << 16) | 0,
		_ywyx = (_MM_SHUFFLE(0, 1, 3, 1) << 16) | 0,
		_zwyx = (_MM_SHUFFLE(0, 1, 3, 2) << 16) | 0,
		_wwyx = (_MM_SHUFFLE(0, 1, 3, 3) << 16) | 0,
		_xxzx = (_MM_SHUFFLE(0, 2, 0, 0) << 16) | 0,
		_yxzx = (_MM_SHUFFLE(0, 2, 0, 1) << 16) | 0,
		_zxzx = (_MM_SHUFFLE(0, 2, 0, 2) << 16) | 0,
		_wxzx = (_MM_SHUFFLE(0, 2, 0, 3) << 16) | 0,
		_xyzx = (_MM_SHUFFLE(0, 2, 1, 0) << 16) | 0,
		_yyzx = (_MM_SHUFFLE(0, 2, 1, 1) << 16) | 0,
		_zyzx = (_MM_SHUFFLE(0, 2, 1, 2) << 16) | 0,
		_wyzx = (_MM_SHUFFLE(0, 2, 1, 3) << 16) | 0,
		_xzzx = (_MM_SHUFFLE(0, 2, 2, 0) << 16) | 0,
		_yzzx = (_MM_SHUFFLE(0, 2, 2, 1) << 16) | 0,
		_zzzx = (_MM_SHUFFLE(0, 2, 2, 2) << 16) | 0,
		_wzzx = (_MM_SHUFFLE(0, 2, 2, 3) << 16) | 0,
		_xwzx = (_MM_SHUFFLE(0, 2, 3, 0) << 16) | 0,
		_ywzx = (_MM_SHUFFLE(0, 2, 3, 1) << 16) | 0,
		_zwzx = (_MM_SHUFFLE(0, 2, 3, 2) << 16) | 0,
		_wwzx = (_MM_SHUFFLE(0, 2, 3, 3) << 16) | 0,
		_xxwx = (_MM_SHUFFLE(0, 3, 0, 0) << 16) | 0,
		_yxwx = (_MM_SHUFFLE(0, 3, 0, 1) << 16) | 0,
		_zxwx = (_MM_SHUFFLE(0, 3, 0, 2) << 16) | 0,
		_wxwx = (_MM_SHUFFLE(0, 3, 0, 3) << 16) | 0,
		_xywx = (_MM_SHUFFLE(0, 3, 1, 0) << 16) | 0,
		_yywx = (_MM_SHUFFLE(0, 3, 1, 1) << 16) | 0,
		_zywx = (_MM_SHUFFLE(0, 3, 1, 2) << 16) | 0,
		_wywx = (_MM_SHUFFLE(0, 3, 1, 3) << 16) | 0,
		_xzwx = (_MM_SHUFFLE(0, 3, 2, 0) << 16) | 0,
		_yzwx = (_MM_SHUFFLE(0, 3, 2, 1) << 16) | 0,
		_zzwx = (_MM_SHUFFLE(0, 3, 2, 2) << 16) | 0,
		_wzwx = (_MM_SHUFFLE(0, 3, 2, 3) << 16) | 0,
		_xwwx = (_MM_SHUFFLE(0, 3, 3, 0) << 16) | 0,
		_ywwx = (_MM_SHUFFLE(0, 3, 3, 1) << 16) | 0,
		_zwwx = (_MM_SHUFFLE(0, 3, 3, 2) << 16) | 0,
		_wwwx = (_MM_SHUFFLE(0, 3, 3, 3) << 16) | 0,
		_xxxy = (_MM_SHUFFLE(1, 0, 0, 0) << 16) | 0,
		_yxxy = (_MM_SHUFFLE(1, 0, 0, 1) << 16) | 0,
		_zxxy = (_MM_SHUFFLE(1, 0, 0, 2) << 16) | 0,
		_wxxy = (_MM_SHUFFLE(1, 0, 0, 3) << 16) | 0,
		_xyxy = (_MM_SHUFFLE(1, 0, 1, 0) << 16) | 0,
		_yyxy = (_MM_SHUFFLE(1, 0, 1, 1) << 16) | 0,
		_zyxy = (_MM_SHUFFLE(1, 0, 1, 2) << 16) | 0,
		_wyxy = (_MM_SHUFFLE(1, 0, 1, 3) << 16) | 0,
		_xzxy = (_MM_SHUFFLE(1, 0, 2, 0) << 16) | 0,
		_yzxy = (_MM_SHUFFLE(1, 0, 2, 1) << 16) | 0,
		_zzxy = (_MM_SHUFFLE(1, 0, 2, 2) << 16) | 0,
		_wzxy = (_MM_SHUFFLE(1, 0, 2, 3) << 16) | 0,
		_xwxy = (_MM_SHUFFLE(1, 0, 3, 0) << 16) | 0,
		_ywxy = (_MM_SHUFFLE(1, 0, 3, 1) << 16) | 0,
		_zwxy = (_MM_SHUFFLE(1, 0, 3, 2) << 16) | 0,
		_wwxy = (_MM_SHUFFLE(1, 0, 3, 3) << 16) | 0,
		_xxyy = (_MM_SHUFFLE(1, 1, 0, 0) << 16) | 0,
		_yxyy = (_MM_SHUFFLE(1, 1, 0, 1) << 16) | 0,
		_zxyy = (_MM_SHUFFLE(1, 1, 0, 2) << 16) | 0,
		_wxyy = (_MM_SHUFFLE(1, 1, 0, 3) << 16) | 0,
		_xyyy = (_MM_SHUFFLE(1, 1, 1, 0) << 16) | 0,
		_yyyy = (_MM_SHUFFLE(1, 1, 1, 1) << 16) | 0,
		_zyyy = (_MM_SHUFFLE(1, 1, 1, 2) << 16) | 0,
		_wyyy = (_MM_SHUFFLE(1, 1, 1, 3) << 16) | 0,
		_xzyy = (_MM_SHUFFLE(1, 1, 2, 0) << 16) | 0,
		_yzyy = (_MM_SHUFFLE(1, 1, 2, 1) << 16) | 0,
		_zzyy = (_MM_SHUFFLE(1, 1, 2, 2) << 16) | 0,
		_wzyy = (_MM_SHUFFLE(1, 1, 2, 3) << 16) | 0,
		_xwyy = (_MM_SHUFFLE(1, 1, 3, 0) << 16) | 0,
		_ywyy = (_MM_SHUFFLE(1, 1, 3, 1) << 16) | 0,
		_zwyy = (_MM_SHUFFLE(1, 1, 3, 2) << 16) | 0,
		_wwyy = (_MM_SHUFFLE(1, 1, 3, 3) << 16) | 0,
		_xxzy = (_MM_SHUFFLE(1, 2, 0, 0) << 16) | 0,
		_yxzy = (_MM_SHUFFLE(1, 2, 0, 1) << 16) | 0,
		_zxzy = (_MM_SHUFFLE(1, 2, 0, 2) << 16) | 0,
		_wxzy = (_MM_SHUFFLE(1, 2, 0, 3) << 16) | 0,
		_xyzy = (_MM_SHUFFLE(1, 2, 1, 0) << 16) | 0,
		_yyzy = (_MM_SHUFFLE(1, 2, 1, 1) << 16) | 0,
		_zyzy = (_MM_SHUFFLE(1, 2, 1, 2) << 16) | 0,
		_wyzy = (_MM_SHUFFLE(1, 2, 1, 3) << 16) | 0,
		_xzzy = (_MM_SHUFFLE(1, 2, 2, 0) << 16) | 0,
		_yzzy = (_MM_SHUFFLE(1, 2, 2, 1) << 16) | 0,
		_zzzy = (_MM_SHUFFLE(1, 2, 2, 2) << 16) | 0,
		_wzzy = (_MM_SHUFFLE(1, 2, 2, 3) << 16) | 0,
		_xwzy = (_MM_SHUFFLE(1, 2, 3, 0) << 16) | 0,
		_ywzy = (_MM_SHUFFLE(1, 2, 3, 1) << 16) | 0,
		_zwzy = (_MM_SHUFFLE(1, 2, 3, 2) << 16) | 0,
		_wwzy = (_MM_SHUFFLE(1, 2, 3, 3) << 16) | 0,
		_xxwy = (_MM_SHUFFLE(1, 3, 0, 0) << 16) | 0,
		_yxwy = (_MM_SHUFFLE(1, 3, 0, 1) << 16) | 0,
		_zxwy = (_MM_SHUFFLE(1, 3, 0, 2) << 16) | 0,
		_wxwy = (_MM_SHUFFLE(1, 3, 0, 3) << 16) | 0,
		_xywy = (_MM_SHUFFLE(1, 3, 1, 0) << 16) | 0,
		_yywy = (_MM_SHUFFLE(1, 3, 1, 1) << 16) | 0,
		_zywy = (_MM_SHUFFLE(1, 3, 1, 2) << 16) | 0,
		_wywy = (_MM_SHUFFLE(1, 3, 1, 3) << 16) | 0,
		_xzwy = (_MM_SHUFFLE(1, 3, 2, 0) << 16) | 0,
		_yzwy = (_MM_SHUFFLE(1, 3, 2, 1) << 16) | 0,
		_zzwy = (_MM_SHUFFLE(1, 3, 2, 2) << 16) | 0,
		_wzwy = (_MM_SHUFFLE(1, 3, 2, 3) << 16) | 0,
		_xwwy = (_MM_SHUFFLE(1, 3, 3, 0) << 16) | 0,
		_ywwy = (_MM_SHUFFLE(1, 3, 3, 1) << 16) | 0,
		_zwwy = (_MM_SHUFFLE(1, 3, 3, 2) << 16) | 0,
		_wwwy = (_MM_SHUFFLE(1, 3, 3, 3) << 16) | 0,
		_xxxz = (_MM_SHUFFLE(2, 0, 0, 0) << 16) | 0,
		_yxxz = (_MM_SHUFFLE(2, 0, 0, 1) << 16) | 0,
		_zxxz = (_MM_SHUFFLE(2, 0, 0, 2) << 16) | 0,
		_wxxz = (_MM_SHUFFLE(2, 0, 0, 3) << 16) | 0,
		_xyxz = (_MM_SHUFFLE(2, 0, 1, 0) << 16) | 0,
		_yyxz = (_MM_SHUFFLE(2, 0, 1, 1) << 16) | 0,
		_zyxz = (_MM_SHUFFLE(2, 0, 1, 2) << 16) | 0,
		_wyxz = (_MM_SHUFFLE(2, 0, 1, 3) << 16) | 0,
		_xzxz = (_MM_SHUFFLE(2, 0, 2, 0) << 16) | 0,
		_yzxz = (_MM_SHUFFLE(2, 0, 2, 1) << 16) | 0,
		_zzxz = (_MM_SHUFFLE(2, 0, 2, 2) << 16) | 0,
		_wzxz = (_MM_SHUFFLE(2, 0, 2, 3) << 16) | 0,
		_xwxz = (_MM_SHUFFLE(2, 0, 3, 0) << 16) | 0,
		_ywxz = (_MM_SHUFFLE(2, 0, 3, 1) << 16) | 0,
		_zwxz = (_MM_SHUFFLE(2, 0, 3, 2) << 16) | 0,
		_wwxz = (_MM_SHUFFLE(2, 0, 3, 3) << 16) | 0,
		_xxyz = (_MM_SHUFFLE(2, 1, 0, 0) << 16) | 0,
		_yxyz = (_MM_SHUFFLE(2, 1, 0, 1) << 16) | 0,
		_zxyz = (_MM_SHUFFLE(2, 1, 0, 2) << 16) | 0,
		_wxyz = (_MM_SHUFFLE(2, 1, 0, 3) << 16) | 0,
		_xyyz = (_MM_SHUFFLE(2, 1, 1, 0) << 16) | 0,
		_yyyz = (_MM_SHUFFLE(2, 1, 1, 1) << 16) | 0,
		_zyyz = (_MM_SHUFFLE(2, 1, 1, 2) << 16) | 0,
		_wyyz = (_MM_SHUFFLE(2, 1, 1, 3) << 16) | 0,
		_xzyz = (_MM_SHUFFLE(2, 1, 2, 0) << 16) | 0,
		_yzyz = (_MM_SHUFFLE(2, 1, 2, 1) << 16) | 0,
		_zzyz = (_MM_SHUFFLE(2, 1, 2, 2) << 16) | 0,
		_wzyz = (_MM_SHUFFLE(2, 1, 2, 3) << 16) | 0,
		_xwyz = (_MM_SHUFFLE(2, 1, 3, 0) << 16) | 0,
		_ywyz = (_MM_SHUFFLE(2, 1, 3, 1) << 16) | 0,
		_zwyz = (_MM_SHUFFLE(2, 1, 3, 2) << 16) | 0,
		_wwyz = (_MM_SHUFFLE(2, 1, 3, 3) << 16) | 0,
		_xxzz = (_MM_SHUFFLE(2, 2, 0, 0) << 16) | 0,
		_yxzz = (_MM_SHUFFLE(2, 2, 0, 1) << 16) | 0,
		_zxzz = (_MM_SHUFFLE(2, 2, 0, 2) << 16) | 0,
		_wxzz = (_MM_SHUFFLE(2, 2, 0, 3) << 16) | 0,
		_xyzz = (_MM_SHUFFLE(2, 2, 1, 0) << 16) | 0,
		_yyzz = (_MM_SHUFFLE(2, 2, 1, 1) << 16) | 0,
		_zyzz = (_MM_SHUFFLE(2, 2, 1, 2) << 16) | 0,
		_wyzz = (_MM_SHUFFLE(2, 2, 1, 3) << 16) | 0,
		_xzzz = (_MM_SHUFFLE(2, 2, 2, 0) << 16) | 0,
		_yzzz = (_MM_SHUFFLE(2, 2, 2, 1) << 16) | 0,
		_zzzz = (_MM_SHUFFLE(2, 2, 2, 2) << 16) | 0,
		_wzzz = (_MM_SHUFFLE(2, 2, 2, 3) << 16) | 0,
		_xwzz = (_MM_SHUFFLE(2, 2, 3, 0) << 16) | 0,
		_ywzz = (_MM_SHUFFLE(2, 2, 3, 1) << 16) | 0,
		_zwzz = (_MM_SHUFFLE(2, 2, 3, 2) << 16) | 0,
		_wwzz = (_MM_SHUFFLE(2, 2, 3, 3) << 16) | 0,
		_xxwz = (_MM_SHUFFLE(2, 3, 0, 0) << 16) | 0,
		_yxwz = (_MM_SHUFFLE(2, 3, 0, 1) << 16) | 0,
		_zxwz = (_MM_SHUFFLE(2, 3, 0, 2) << 16) | 0,
		_wxwz = (_MM_SHUFFLE(2, 3, 0, 3) << 16) | 0,
		_xywz = (_MM_SHUFFLE(2, 3, 1, 0) << 16) | 0,
		_yywz = (_MM_SHUFFLE(2, 3, 1, 1) << 16) | 0,
		_zywz = (_MM_SHUFFLE(2, 3, 1, 2) << 16) | 0,
		_wywz = (_MM_SHUFFLE(2, 3, 1, 3) << 16) | 0,
		_xzwz = (_MM_SHUFFLE(2, 3, 2, 0) << 16) | 0,
		_yzwz = (_MM_SHUFFLE(2, 3, 2, 1) << 16) | 0,
		_zzwz = (_MM_SHUFFLE(2, 3, 2, 2) << 16) | 0,
		_wzwz = (_MM_SHUFFLE(2, 3, 2, 3) << 16) | 0,
		_xwwz = (_MM_SHUFFLE(2, 3, 3, 0) << 16) | 0,
		_ywwz = (_MM_SHUFFLE(2, 3, 3, 1) << 16) | 0,
		_zwwz = (_MM_SHUFFLE(2, 3, 3, 2) << 16) | 0,
		_wwwz = (_MM_SHUFFLE(2, 3, 3, 3) << 16) | 0,
		_xxxw = (_MM_SHUFFLE(3, 0, 0, 0) << 16) | 0,
		_yxxw = (_MM_SHUFFLE(3, 0, 0, 1) << 16) | 0,
		_zxxw = (_MM_SHUFFLE(3, 0, 0, 2) << 16) | 0,
		_wxxw = (_MM_SHUFFLE(3, 0, 0, 3) << 16) | 0,
		_xyxw = (_MM_SHUFFLE(3, 0, 1, 0) << 16) | 0,
		_yyxw = (_MM_SHUFFLE(3, 0, 1, 1) << 16) | 0,
		_zyxw = (_MM_SHUFFLE(3, 0, 1, 2) << 16) | 0,
		_wyxw = (_MM_SHUFFLE(3, 0, 1, 3) << 16) | 0,
		_xzxw = (_MM_SHUFFLE(3, 0, 2, 0) << 16) | 0,
		_yzxw = (_MM_SHUFFLE(3, 0, 2, 1) << 16) | 0,
		_zzxw = (_MM_SHUFFLE(3, 0, 2, 2) << 16) | 0,
		_wzxw = (_MM_SHUFFLE(3, 0, 2, 3) << 16) | 0,
		_xwxw = (_MM_SHUFFLE(3, 0, 3, 0) << 16) | 0,
		_ywxw = (_MM_SHUFFLE(3, 0, 3, 1) << 16) | 0,
		_zwxw = (_MM_SHUFFLE(3, 0, 3, 2) << 16) | 0,
		_wwxw = (_MM_SHUFFLE(3, 0, 3, 3) << 16) | 0,
		_xxyw = (_MM_SHUFFLE(3, 1, 0, 0) << 16) | 0,
		_yxyw = (_MM_SHUFFLE(3, 1, 0, 1) << 16) | 0,
		_zxyw = (_MM_SHUFFLE(3, 1, 0, 2) << 16) | 0,
		_wxyw = (_MM_SHUFFLE(3, 1, 0, 3) << 16) | 0,
		_xyyw = (_MM_SHUFFLE(3, 1, 1, 0) << 16) | 0,
		_yyyw = (_MM_SHUFFLE(3, 1, 1, 1) << 16) | 0,
		_zyyw = (_MM_SHUFFLE(3, 1, 1, 2) << 16) | 0,
		_wyyw = (_MM_SHUFFLE(3, 1, 1, 3) << 16) | 0,
		_xzyw = (_MM_SHUFFLE(3, 1, 2, 0) << 16) | 0,
		_yzyw = (_MM_SHUFFLE(3, 1, 2, 1) << 16) | 0,
		_zzyw = (_MM_SHUFFLE(3, 1, 2, 2) << 16) | 0,
		_wzyw = (_MM_SHUFFLE(3, 1, 2, 3) << 16) | 0,
		_xwyw = (_MM_SHUFFLE(3, 1, 3, 0) << 16) | 0,
		_ywyw = (_MM_SHUFFLE(3, 1, 3, 1) << 16) | 0,
		_zwyw = (_MM_SHUFFLE(3, 1, 3, 2) << 16) | 0,
		_wwyw = (_MM_SHUFFLE(3, 1, 3, 3) << 16) | 0,
		_xxzw = (_MM_SHUFFLE(3, 2, 0, 0) << 16) | 0,
		_yxzw = (_MM_SHUFFLE(3, 2, 0, 1) << 16) | 0,
		_zxzw = (_MM_SHUFFLE(3, 2, 0, 2) << 16) | 0,
		_wxzw = (_MM_SHUFFLE(3, 2, 0, 3) << 16) | 0,
		_xyzw = (_MM_SHUFFLE(3, 2, 1, 0) << 16) | 0,
		_yyzw = (_MM_SHUFFLE(3, 2, 1, 1) << 16) | 0,
		_zyzw = (_MM_SHUFFLE(3, 2, 1, 2) << 16) | 0,
		_wyzw = (_MM_SHUFFLE(3, 2, 1, 3) << 16) | 0,
		_xzzw = (_MM_SHUFFLE(3, 2, 2, 0) << 16) | 0,
		_yzzw = (_MM_SHUFFLE(3, 2, 2, 1) << 16) | 0,
		_zzzw = (_MM_SHUFFLE(3, 2, 2, 2) << 16) | 0,
		_wzzw = (_MM_SHUFFLE(3, 2, 2, 3) << 16) | 0,
		_xwzw = (_MM_SHUFFLE(3, 2, 3, 0) << 16) | 0,
		_ywzw = (_MM_SHUFFLE(3, 2, 3, 1) << 16) | 0,
		_zwzw = (_MM_SHUFFLE(3, 2, 3, 2) << 16) | 0,
		_wwzw = (_MM_SHUFFLE(3, 2, 3, 3) << 16) | 0,
		_xxww = (_MM_SHUFFLE(3, 3, 0, 0) << 16) | 0,
		_yxww = (_MM_SHUFFLE(3, 3, 0, 1) << 16) | 0,
		_zxww = (_MM_SHUFFLE(3, 3, 0, 2) << 16) | 0,
		_wxww = (_MM_SHUFFLE(3, 3, 0, 3) << 16) | 0,
		_xyww = (_MM_SHUFFLE(3, 3, 1, 0) << 16) | 0,
		_yyww = (_MM_SHUFFLE(3, 3, 1, 1) << 16) | 0,
		_zyww = (_MM_SHUFFLE(3, 3, 1, 2) << 16) | 0,
		_wyww = (_MM_SHUFFLE(3, 3, 1, 3) << 16) | 0,
		_xzww = (_MM_SHUFFLE(3, 3, 2, 0) << 16) | 0,
		_yzww = (_MM_SHUFFLE(3, 3, 2, 1) << 16) | 0,
		_zzww = (_MM_SHUFFLE(3, 3, 2, 2) << 16) | 0,
		_wzww = (_MM_SHUFFLE(3, 3, 2, 3) << 16) | 0,
		_xwww = (_MM_SHUFFLE(3, 3, 3, 0) << 16) | 0,
		_ywww = (_MM_SHUFFLE(3, 3, 3, 1) << 16) | 0,
		_zwww = (_MM_SHUFFLE(3, 3, 3, 2) << 16) | 0,
		_wwww = (_MM_SHUFFLE(3, 3, 3, 3) << 16) | 0,

		_0xxx = (_MM_SHUFFLE(0, 0, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yxx = (_MM_SHUFFLE(0, 0, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zxx = (_MM_SHUFFLE(0, 0, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wxx = (_MM_SHUFFLE(0, 0, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0xx = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0xx = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0xx = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0xx = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_00xx = (_MM_SHUFFLE(0, 0, 0, 0) << 16) | (0xc << 3) | 1,
		_0xyx = (_MM_SHUFFLE(0, 1, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yyx = (_MM_SHUFFLE(0, 1, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zyx = (_MM_SHUFFLE(0, 1, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wyx = (_MM_SHUFFLE(0, 1, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0yx = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0yx = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0yx = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0yx = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_00yx = (_MM_SHUFFLE(0, 1, 0, 0) << 16) | (0xc << 3) | 1,
		_0xzx = (_MM_SHUFFLE(0, 2, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yzx = (_MM_SHUFFLE(0, 2, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zzx = (_MM_SHUFFLE(0, 2, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wzx = (_MM_SHUFFLE(0, 2, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0zx = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0zx = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0zx = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0zx = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_00zx = (_MM_SHUFFLE(0, 2, 0, 0) << 16) | (0xc << 3) | 1,
		_0xwx = (_MM_SHUFFLE(0, 3, 0, 0) << 16) | (0x8 << 3) | 1,
		_0ywx = (_MM_SHUFFLE(0, 3, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zwx = (_MM_SHUFFLE(0, 3, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wwx = (_MM_SHUFFLE(0, 3, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0wx = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0wx = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0wx = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0wx = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_00wx = (_MM_SHUFFLE(0, 3, 0, 0) << 16) | (0xc << 3) | 1,
		_xx0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 0, 0) << 8) | (0x2 << 3) | 1,
		_yx0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 0, 1) << 8) | (0x2 << 3) | 1,
		_zx0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 0, 2) << 8) | (0x2 << 3) | 1,
		_wx0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 0, 3) << 8) | (0x2 << 3) | 1,
		_0x0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xy0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 1, 0) << 8) | (0x2 << 3) | 1,
		_yy0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 1, 1) << 8) | (0x2 << 3) | 1,
		_zy0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 1, 2) << 8) | (0x2 << 3) | 1,
		_wy0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 1, 3) << 8) | (0x2 << 3) | 1,
		_0y0x = (_MM_SHUFFLE(0, 0, 0, 1) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xz0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 2, 0) << 8) | (0x2 << 3) | 1,
		_yz0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 2, 1) << 8) | (0x2 << 3) | 1,
		_zz0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 2, 2) << 8) | (0x2 << 3) | 1,
		_wz0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 2, 3) << 8) | (0x2 << 3) | 1,
		_0z0x = (_MM_SHUFFLE(0, 0, 0, 2) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xw0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x2 << 3) | 1,
		_yw0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 1) << 8) | (0x2 << 3) | 1,
		_zw0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 2) << 8) | (0x2 << 3) | 1,
		_ww0x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0x2 << 3) | 1,
		_0w0x = (_MM_SHUFFLE(0, 0, 0, 3) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_x00x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_y00x = (_MM_SHUFFLE(0, 0, 0, 1) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_z00x = (_MM_SHUFFLE(0, 0, 0, 2) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_w00x = (_MM_SHUFFLE(0, 0, 0, 3) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_000x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0xe << 3) | 1,
		_0xxy = (_MM_SHUFFLE(1, 0, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yxy = (_MM_SHUFFLE(1, 0, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zxy = (_MM_SHUFFLE(1, 0, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wxy = (_MM_SHUFFLE(1, 0, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0xy = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0xy = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0xy = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0xy = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_00xy = (_MM_SHUFFLE(1, 0, 0, 0) << 16) | (0xc << 3) | 1,
		_0xyy = (_MM_SHUFFLE(1, 1, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yyy = (_MM_SHUFFLE(1, 1, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zyy = (_MM_SHUFFLE(1, 1, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wyy = (_MM_SHUFFLE(1, 1, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0yy = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0yy = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0yy = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0yy = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_00yy = (_MM_SHUFFLE(1, 1, 0, 0) << 16) | (0xc << 3) | 1,
		_0xzy = (_MM_SHUFFLE(1, 2, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yzy = (_MM_SHUFFLE(1, 2, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zzy = (_MM_SHUFFLE(1, 2, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wzy = (_MM_SHUFFLE(1, 2, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0zy = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0zy = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0zy = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0zy = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_00zy = (_MM_SHUFFLE(1, 2, 0, 0) << 16) | (0xc << 3) | 1,
		_0xwy = (_MM_SHUFFLE(1, 3, 0, 0) << 16) | (0x8 << 3) | 1,
		_0ywy = (_MM_SHUFFLE(1, 3, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zwy = (_MM_SHUFFLE(1, 3, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wwy = (_MM_SHUFFLE(1, 3, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0wy = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0wy = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0wy = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0wy = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_00wy = (_MM_SHUFFLE(1, 3, 0, 0) << 16) | (0xc << 3) | 1,
		_xx0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 0, 0) << 8) | (0x2 << 3) | 1,
		_yx0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 0, 1) << 8) | (0x2 << 3) | 1,
		_zx0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 0, 2) << 8) | (0x2 << 3) | 1,
		_wx0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 0, 3) << 8) | (0x2 << 3) | 1,
		_0x0y = (_MM_SHUFFLE(0, 0, 1, 0) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xy0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 1, 0) << 8) | (0x2 << 3) | 1,
		_yy0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 1, 1) << 8) | (0x2 << 3) | 1,
		_zy0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 1, 2) << 8) | (0x2 << 3) | 1,
		_wy0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 1, 3) << 8) | (0x2 << 3) | 1,
		_0y0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xz0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 2, 0) << 8) | (0x2 << 3) | 1,
		_yz0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 2, 1) << 8) | (0x2 << 3) | 1,
		_zz0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 2, 2) << 8) | (0x2 << 3) | 1,
		_wz0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 2, 3) << 8) | (0x2 << 3) | 1,
		_0z0y = (_MM_SHUFFLE(0, 0, 1, 2) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xw0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x2 << 3) | 1,
		_yw0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 1) << 8) | (0x2 << 3) | 1,
		_zw0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 2) << 8) | (0x2 << 3) | 1,
		_ww0y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0x2 << 3) | 1,
		_0w0y = (_MM_SHUFFLE(0, 0, 1, 3) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_x00y = (_MM_SHUFFLE(0, 0, 1, 0) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_y00y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_z00y = (_MM_SHUFFLE(0, 0, 1, 2) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_w00y = (_MM_SHUFFLE(0, 0, 1, 3) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_000y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0xe << 3) | 1,
		_0xxz = (_MM_SHUFFLE(2, 0, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yxz = (_MM_SHUFFLE(2, 0, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zxz = (_MM_SHUFFLE(2, 0, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wxz = (_MM_SHUFFLE(2, 0, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0xz = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(2, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0xz = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(2, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0xz = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(2, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0xz = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(2, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_00xz = (_MM_SHUFFLE(2, 0, 0, 0) << 16) | (0xc << 3) | 1,
		_0xyz = (_MM_SHUFFLE(2, 1, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yyz = (_MM_SHUFFLE(2, 1, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zyz = (_MM_SHUFFLE(2, 1, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wyz = (_MM_SHUFFLE(2, 1, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0yz = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(2, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0yz = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(2, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0yz = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(2, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0yz = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(2, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_00yz = (_MM_SHUFFLE(2, 1, 0, 0) << 16) | (0xc << 3) | 1,
		_0xzz = (_MM_SHUFFLE(2, 2, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yzz = (_MM_SHUFFLE(2, 2, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zzz = (_MM_SHUFFLE(2, 2, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wzz = (_MM_SHUFFLE(2, 2, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0zz = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(2, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0zz = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(2, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0zz = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(2, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0zz = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(2, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_00zz = (_MM_SHUFFLE(2, 2, 0, 0) << 16) | (0xc << 3) | 1,
		_0xwz = (_MM_SHUFFLE(2, 3, 0, 0) << 16) | (0x8 << 3) | 1,
		_0ywz = (_MM_SHUFFLE(2, 3, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zwz = (_MM_SHUFFLE(2, 3, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wwz = (_MM_SHUFFLE(2, 3, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0wz = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(2, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0wz = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(2, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0wz = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(2, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0wz = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(2, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_00wz = (_MM_SHUFFLE(2, 3, 0, 0) << 16) | (0xc << 3) | 1,
		_xx0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 0, 0) << 8) | (0x2 << 3) | 1,
		_yx0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 0, 1) << 8) | (0x2 << 3) | 1,
		_zx0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 0, 2) << 8) | (0x2 << 3) | 1,
		_wx0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 0, 3) << 8) | (0x2 << 3) | 1,
		_0x0z = (_MM_SHUFFLE(0, 0, 2, 0) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xy0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 1, 0) << 8) | (0x2 << 3) | 1,
		_yy0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 1, 1) << 8) | (0x2 << 3) | 1,
		_zy0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 1, 2) << 8) | (0x2 << 3) | 1,
		_wy0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 1, 3) << 8) | (0x2 << 3) | 1,
		_0y0z = (_MM_SHUFFLE(0, 0, 2, 1) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xz0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 2, 0) << 8) | (0x2 << 3) | 1,
		_yz0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 2, 1) << 8) | (0x2 << 3) | 1,
		_zz0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 2, 2) << 8) | (0x2 << 3) | 1,
		_wz0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 2, 3) << 8) | (0x2 << 3) | 1,
		_0z0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xw0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x2 << 3) | 1,
		_yw0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 1) << 8) | (0x2 << 3) | 1,
		_zw0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 2) << 8) | (0x2 << 3) | 1,
		_ww0z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0x2 << 3) | 1,
		_0w0z = (_MM_SHUFFLE(0, 0, 2, 3) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_x00z = (_MM_SHUFFLE(0, 0, 2, 0) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_y00z = (_MM_SHUFFLE(0, 0, 2, 1) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_z00z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_w00z = (_MM_SHUFFLE(0, 0, 2, 3) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_000z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0xe << 3) | 1,
		_0xxw = (_MM_SHUFFLE(3, 0, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yxw = (_MM_SHUFFLE(3, 0, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zxw = (_MM_SHUFFLE(3, 0, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wxw = (_MM_SHUFFLE(3, 0, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0xw = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0xw = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0xw = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0xw = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x4 << 3) | 1,
		_00xw = (_MM_SHUFFLE(3, 0, 0, 0) << 16) | (0xc << 3) | 1,
		_0xyw = (_MM_SHUFFLE(3, 1, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yyw = (_MM_SHUFFLE(3, 1, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zyw = (_MM_SHUFFLE(3, 1, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wyw = (_MM_SHUFFLE(3, 1, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0yw = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0yw = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0yw = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0yw = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x4 << 3) | 1,
		_00yw = (_MM_SHUFFLE(3, 1, 0, 0) << 16) | (0xc << 3) | 1,
		_0xzw = (_MM_SHUFFLE(3, 2, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yzw = (_MM_SHUFFLE(3, 2, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zzw = (_MM_SHUFFLE(3, 2, 2, 0) << 16) | (0x8 << 3) | 1,
		_0wzw = (_MM_SHUFFLE(3, 2, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0zw = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0zw = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0zw = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0zw = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 2, 3, 0) << 8) | (0x4 << 3) | 1,
		_00zw = (_MM_SHUFFLE(3, 2, 0, 0) << 16) | (0xc << 3) | 1,
		_0xww = (_MM_SHUFFLE(3, 3, 0, 0) << 16) | (0x8 << 3) | 1,
		_0yww = (_MM_SHUFFLE(3, 3, 1, 0) << 16) | (0x8 << 3) | 1,
		_0zww = (_MM_SHUFFLE(3, 3, 2, 0) << 16) | (0x8 << 3) | 1,
		_0www = (_MM_SHUFFLE(3, 3, 3, 0) << 16) | (0x8 << 3) | 1,
		_x0ww = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_y0ww = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_z0ww = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_w0ww = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 3, 3, 0) << 8) | (0x4 << 3) | 1,
		_00ww = (_MM_SHUFFLE(3, 3, 0, 0) << 16) | (0xc << 3) | 1,
		_xx0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 0, 0) << 8) | (0x2 << 3) | 1,
		_yx0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 0, 1) << 8) | (0x2 << 3) | 1,
		_zx0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 0, 2) << 8) | (0x2 << 3) | 1,
		_wx0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 0, 3) << 8) | (0x2 << 3) | 1,
		_0x0w = (_MM_SHUFFLE(0, 0, 3, 0) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xy0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 1, 0) << 8) | (0x2 << 3) | 1,
		_yy0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 1, 1) << 8) | (0x2 << 3) | 1,
		_zy0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 1, 2) << 8) | (0x2 << 3) | 1,
		_wy0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 1, 3) << 8) | (0x2 << 3) | 1,
		_0y0w = (_MM_SHUFFLE(0, 0, 3, 1) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xz0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 2, 0) << 8) | (0x2 << 3) | 1,
		_yz0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 2, 1) << 8) | (0x2 << 3) | 1,
		_zz0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 2, 2) << 8) | (0x2 << 3) | 1,
		_wz0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 2, 3) << 8) | (0x2 << 3) | 1,
		_0z0w = (_MM_SHUFFLE(0, 0, 3, 2) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_xw0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x2 << 3) | 1,
		_yw0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 1) << 8) | (0x2 << 3) | 1,
		_zw0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 2) << 8) | (0x2 << 3) | 1,
		_ww0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0x2 << 3) | 1,
		_0w0w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 1,
		_x00w = (_MM_SHUFFLE(0, 0, 3, 0) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_y00w = (_MM_SHUFFLE(0, 0, 3, 1) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_z00w = (_MM_SHUFFLE(0, 0, 3, 2) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_w00w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 1,
		_000w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0xe << 3) | 1,
		_xxx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 0, 0) << 8) | (0x1 << 3) | 1,
		_yxx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 0, 1) << 8) | (0x1 << 3) | 1,
		_zxx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 0, 2) << 8) | (0x1 << 3) | 1,
		_wxx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 0, 3) << 8) | (0x1 << 3) | 1,
		_0xx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xyx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 1, 0) << 8) | (0x1 << 3) | 1,
		_yyx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 1, 1) << 8) | (0x1 << 3) | 1,
		_zyx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 1, 2) << 8) | (0x1 << 3) | 1,
		_wyx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 1, 3) << 8) | (0x1 << 3) | 1,
		_0yx0 = (_MM_SHUFFLE(0, 0, 0, 1) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xzx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 2, 0) << 8) | (0x1 << 3) | 1,
		_yzx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 2, 1) << 8) | (0x1 << 3) | 1,
		_zzx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 2, 2) << 8) | (0x1 << 3) | 1,
		_wzx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 2, 3) << 8) | (0x1 << 3) | 1,
		_0zx0 = (_MM_SHUFFLE(0, 0, 0, 2) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xwx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x1 << 3) | 1,
		_ywx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 1) << 8) | (0x1 << 3) | 1,
		_zwx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 2) << 8) | (0x1 << 3) | 1,
		_wwx0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0x1 << 3) | 1,
		_0wx0 = (_MM_SHUFFLE(0, 0, 0, 3) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_x0x0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_y0x0 = (_MM_SHUFFLE(0, 0, 0, 1) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_z0x0 = (_MM_SHUFFLE(0, 0, 0, 2) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_w0x0 = (_MM_SHUFFLE(0, 0, 0, 3) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_00x0 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0xd << 3) | 1,
		_xxy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 0, 0) << 8) | (0x1 << 3) | 1,
		_yxy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 0, 1) << 8) | (0x1 << 3) | 1,
		_zxy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 0, 2) << 8) | (0x1 << 3) | 1,
		_wxy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 0, 3) << 8) | (0x1 << 3) | 1,
		_0xy0 = (_MM_SHUFFLE(0, 0, 1, 0) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xyy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 1, 0) << 8) | (0x1 << 3) | 1,
		_yyy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 1, 1) << 8) | (0x1 << 3) | 1,
		_zyy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 1, 2) << 8) | (0x1 << 3) | 1,
		_wyy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 1, 3) << 8) | (0x1 << 3) | 1,
		_0yy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xzy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 2, 0) << 8) | (0x1 << 3) | 1,
		_yzy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 2, 1) << 8) | (0x1 << 3) | 1,
		_zzy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 2, 2) << 8) | (0x1 << 3) | 1,
		_wzy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 2, 3) << 8) | (0x1 << 3) | 1,
		_0zy0 = (_MM_SHUFFLE(0, 0, 1, 2) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xwy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x1 << 3) | 1,
		_ywy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 1) << 8) | (0x1 << 3) | 1,
		_zwy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 2) << 8) | (0x1 << 3) | 1,
		_wwy0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0x1 << 3) | 1,
		_0wy0 = (_MM_SHUFFLE(0, 0, 1, 3) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_x0y0 = (_MM_SHUFFLE(0, 0, 1, 0) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_y0y0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_z0y0 = (_MM_SHUFFLE(0, 0, 1, 2) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_w0y0 = (_MM_SHUFFLE(0, 0, 1, 3) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_00y0 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0xd << 3) | 1,
		_xxz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 0, 0) << 8) | (0x1 << 3) | 1,
		_yxz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 0, 1) << 8) | (0x1 << 3) | 1,
		_zxz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 0, 2) << 8) | (0x1 << 3) | 1,
		_wxz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 0, 3) << 8) | (0x1 << 3) | 1,
		_0xz0 = (_MM_SHUFFLE(0, 0, 2, 0) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xyz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 1, 0) << 8) | (0x1 << 3) | 1,
		_yyz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 1, 1) << 8) | (0x1 << 3) | 1,
		_zyz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 1, 2) << 8) | (0x1 << 3) | 1,
		_wyz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 1, 3) << 8) | (0x1 << 3) | 1,
		_0yz0 = (_MM_SHUFFLE(0, 0, 2, 1) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xzz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 2, 0) << 8) | (0x1 << 3) | 1,
		_yzz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 2, 1) << 8) | (0x1 << 3) | 1,
		_zzz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 2, 2) << 8) | (0x1 << 3) | 1,
		_wzz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 2, 3) << 8) | (0x1 << 3) | 1,
		_0zz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xwz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x1 << 3) | 1,
		_ywz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 1) << 8) | (0x1 << 3) | 1,
		_zwz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 2) << 8) | (0x1 << 3) | 1,
		_wwz0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0x1 << 3) | 1,
		_0wz0 = (_MM_SHUFFLE(0, 0, 2, 3) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_x0z0 = (_MM_SHUFFLE(0, 0, 2, 0) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_y0z0 = (_MM_SHUFFLE(0, 0, 2, 1) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_z0z0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_w0z0 = (_MM_SHUFFLE(0, 0, 2, 3) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_00z0 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0xd << 3) | 1,
		_xxw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 0, 0) << 8) | (0x1 << 3) | 1,
		_yxw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 0, 1) << 8) | (0x1 << 3) | 1,
		_zxw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 0, 2) << 8) | (0x1 << 3) | 1,
		_wxw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 0, 3) << 8) | (0x1 << 3) | 1,
		_0xw0 = (_MM_SHUFFLE(0, 0, 3, 0) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xyw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 1, 0) << 8) | (0x1 << 3) | 1,
		_yyw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 1, 1) << 8) | (0x1 << 3) | 1,
		_zyw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 1, 2) << 8) | (0x1 << 3) | 1,
		_wyw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 1, 3) << 8) | (0x1 << 3) | 1,
		_0yw0 = (_MM_SHUFFLE(0, 0, 3, 1) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xzw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 2, 0) << 8) | (0x1 << 3) | 1,
		_yzw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 2, 1) << 8) | (0x1 << 3) | 1,
		_zzw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 2, 2) << 8) | (0x1 << 3) | 1,
		_wzw0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 2, 3) << 8) | (0x1 << 3) | 1,
		_0zw0 = (_MM_SHUFFLE(0, 0, 3, 2) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_xww0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x1 << 3) | 1,
		_yww0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 1) << 8) | (0x1 << 3) | 1,
		_zww0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 2) << 8) | (0x1 << 3) | 1,
		_www0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0x1 << 3) | 1,
		_0ww0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 1,
		_x0w0 = (_MM_SHUFFLE(0, 0, 3, 0) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_y0w0 = (_MM_SHUFFLE(0, 0, 3, 1) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_z0w0 = (_MM_SHUFFLE(0, 0, 3, 2) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_w0w0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 1,
		_00w0 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0xd << 3) | 1,
		_xx00 = (_MM_SHUFFLE(0, 0, 0, 0) << 16) | (0x3 << 3) | 1,
		_yx00 = (_MM_SHUFFLE(0, 0, 0, 1) << 16) | (0x3 << 3) | 1,
		_zx00 = (_MM_SHUFFLE(0, 0, 0, 2) << 16) | (0x3 << 3) | 1,
		_wx00 = (_MM_SHUFFLE(0, 0, 0, 3) << 16) | (0x3 << 3) | 1,
		_0x00 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 3, 0, 3) << 8) | (0xb << 3) | 1,
		_xy00 = (_MM_SHUFFLE(0, 0, 1, 0) << 16) | (0x3 << 3) | 1,
		_yy00 = (_MM_SHUFFLE(0, 0, 1, 1) << 16) | (0x3 << 3) | 1,
		_zy00 = (_MM_SHUFFLE(0, 0, 1, 2) << 16) | (0x3 << 3) | 1,
		_wy00 = (_MM_SHUFFLE(0, 0, 1, 3) << 16) | (0x3 << 3) | 1,
		_0y00 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 3, 0, 3) << 8) | (0xb << 3) | 1,
		_xz00 = (_MM_SHUFFLE(0, 0, 2, 0) << 16) | (0x3 << 3) | 1,
		_yz00 = (_MM_SHUFFLE(0, 0, 2, 1) << 16) | (0x3 << 3) | 1,
		_zz00 = (_MM_SHUFFLE(0, 0, 2, 2) << 16) | (0x3 << 3) | 1,
		_wz00 = (_MM_SHUFFLE(0, 0, 2, 3) << 16) | (0x3 << 3) | 1,
		_0z00 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 3, 0, 3) << 8) | (0xb << 3) | 1,
		_xw00 = (_MM_SHUFFLE(0, 0, 3, 0) << 16) | (0x3 << 3) | 1,
		_yw00 = (_MM_SHUFFLE(0, 0, 3, 1) << 16) | (0x3 << 3) | 1,
		_zw00 = (_MM_SHUFFLE(0, 0, 3, 2) << 16) | (0x3 << 3) | 1,
		_ww00 = (_MM_SHUFFLE(0, 0, 3, 3) << 16) | (0x3 << 3) | 1,
		_0w00 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 3, 0, 3) << 8) | (0xb << 3) | 1,
		_x000 = (_MM_SHUFFLE(3, 2, 1, 0) << 16) | (0x7 << 3) | 1,
		_y000 = (_MM_SHUFFLE(3, 2, 1, 1) << 16) | (0x7 << 3) | 1,
		_z000 = (_MM_SHUFFLE(3, 2, 1, 2) << 16) | (0x7 << 3) | 1,
		_w000 = (_MM_SHUFFLE(3, 2, 1, 3) << 16) | (0x7 << 3) | 1,

		_1xxx = (_MM_SHUFFLE(0, 0, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yxx = (_MM_SHUFFLE(0, 0, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zxx = (_MM_SHUFFLE(0, 0, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wxx = (_MM_SHUFFLE(0, 0, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1xx = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1xx = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1xx = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1xx = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_11xx = (_MM_SHUFFLE(0, 0, 0, 0) << 16) | (0xc << 3) | 2,
		_1xyx = (_MM_SHUFFLE(0, 1, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yyx = (_MM_SHUFFLE(0, 1, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zyx = (_MM_SHUFFLE(0, 1, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wyx = (_MM_SHUFFLE(0, 1, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1yx = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1yx = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1yx = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1yx = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_11yx = (_MM_SHUFFLE(0, 1, 0, 0) << 16) | (0xc << 3) | 2,
		_1xzx = (_MM_SHUFFLE(0, 2, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yzx = (_MM_SHUFFLE(0, 2, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zzx = (_MM_SHUFFLE(0, 2, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wzx = (_MM_SHUFFLE(0, 2, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1zx = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1zx = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1zx = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1zx = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_11zx = (_MM_SHUFFLE(0, 2, 0, 0) << 16) | (0xc << 3) | 2,
		_1xwx = (_MM_SHUFFLE(0, 3, 0, 0) << 16) | (0x8 << 3) | 2,
		_1ywx = (_MM_SHUFFLE(0, 3, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zwx = (_MM_SHUFFLE(0, 3, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wwx = (_MM_SHUFFLE(0, 3, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1wx = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1wx = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1wx = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1wx = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_11wx = (_MM_SHUFFLE(0, 3, 0, 0) << 16) | (0xc << 3) | 2,
		_xx1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 0, 0) << 8) | (0x2 << 3) | 2,
		_yx1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 0, 1) << 8) | (0x2 << 3) | 2,
		_zx1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 0, 2) << 8) | (0x2 << 3) | 2,
		_wx1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 0, 3) << 8) | (0x2 << 3) | 2,
		_1x1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xy1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 1, 0) << 8) | (0x2 << 3) | 2,
		_yy1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 1, 1) << 8) | (0x2 << 3) | 2,
		_zy1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 1, 2) << 8) | (0x2 << 3) | 2,
		_wy1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 1, 3) << 8) | (0x2 << 3) | 2,
		_1y1x = (_MM_SHUFFLE(0, 0, 0, 1) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xz1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 2, 0) << 8) | (0x2 << 3) | 2,
		_yz1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 2, 1) << 8) | (0x2 << 3) | 2,
		_zz1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 2, 2) << 8) | (0x2 << 3) | 2,
		_wz1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 2, 3) << 8) | (0x2 << 3) | 2,
		_1z1x = (_MM_SHUFFLE(0, 0, 0, 2) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xw1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x2 << 3) | 2,
		_yw1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 1) << 8) | (0x2 << 3) | 2,
		_zw1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 2) << 8) | (0x2 << 3) | 2,
		_ww1x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0x2 << 3) | 2,
		_1w1x = (_MM_SHUFFLE(0, 0, 0, 3) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_x11x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_y11x = (_MM_SHUFFLE(0, 0, 0, 1) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_z11x = (_MM_SHUFFLE(0, 0, 0, 2) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_w11x = (_MM_SHUFFLE(0, 0, 0, 3) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_111x = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0xe << 3) | 2,
		_1xxy = (_MM_SHUFFLE(1, 0, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yxy = (_MM_SHUFFLE(1, 0, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zxy = (_MM_SHUFFLE(1, 0, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wxy = (_MM_SHUFFLE(1, 0, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1xy = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1xy = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1xy = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1xy = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_11xy = (_MM_SHUFFLE(1, 0, 0, 0) << 16) | (0xc << 3) | 2,
		_1xyy = (_MM_SHUFFLE(1, 1, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yyy = (_MM_SHUFFLE(1, 1, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zyy = (_MM_SHUFFLE(1, 1, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wyy = (_MM_SHUFFLE(1, 1, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1yy = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1yy = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1yy = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1yy = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_11yy = (_MM_SHUFFLE(1, 1, 0, 0) << 16) | (0xc << 3) | 2,
		_1xzy = (_MM_SHUFFLE(1, 2, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yzy = (_MM_SHUFFLE(1, 2, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zzy = (_MM_SHUFFLE(1, 2, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wzy = (_MM_SHUFFLE(1, 2, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1zy = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1zy = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1zy = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1zy = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_11zy = (_MM_SHUFFLE(1, 2, 0, 0) << 16) | (0xc << 3) | 2,
		_1xwy = (_MM_SHUFFLE(1, 3, 0, 0) << 16) | (0x8 << 3) | 2,
		_1ywy = (_MM_SHUFFLE(1, 3, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zwy = (_MM_SHUFFLE(1, 3, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wwy = (_MM_SHUFFLE(1, 3, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1wy = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1wy = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1wy = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1wy = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_11wy = (_MM_SHUFFLE(1, 3, 0, 0) << 16) | (0xc << 3) | 2,
		_xx1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 0, 0) << 8) | (0x2 << 3) | 2,
		_yx1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 0, 1) << 8) | (0x2 << 3) | 2,
		_zx1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 0, 2) << 8) | (0x2 << 3) | 2,
		_wx1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 0, 3) << 8) | (0x2 << 3) | 2,
		_1x1y = (_MM_SHUFFLE(0, 0, 1, 0) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xy1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 1, 0) << 8) | (0x2 << 3) | 2,
		_yy1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 1, 1) << 8) | (0x2 << 3) | 2,
		_zy1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 1, 2) << 8) | (0x2 << 3) | 2,
		_wy1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 1, 3) << 8) | (0x2 << 3) | 2,
		_1y1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xz1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 2, 0) << 8) | (0x2 << 3) | 2,
		_yz1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 2, 1) << 8) | (0x2 << 3) | 2,
		_zz1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 2, 2) << 8) | (0x2 << 3) | 2,
		_wz1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 2, 3) << 8) | (0x2 << 3) | 2,
		_1z1y = (_MM_SHUFFLE(0, 0, 1, 2) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xw1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x2 << 3) | 2,
		_yw1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 1) << 8) | (0x2 << 3) | 2,
		_zw1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 2) << 8) | (0x2 << 3) | 2,
		_ww1y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0x2 << 3) | 2,
		_1w1y = (_MM_SHUFFLE(0, 0, 1, 3) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_x11y = (_MM_SHUFFLE(0, 0, 1, 0) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_y11y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_z11y = (_MM_SHUFFLE(0, 0, 1, 2) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_w11y = (_MM_SHUFFLE(0, 0, 1, 3) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_111y = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0xe << 3) | 2,
		_1xxz = (_MM_SHUFFLE(2, 0, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yxz = (_MM_SHUFFLE(2, 0, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zxz = (_MM_SHUFFLE(2, 0, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wxz = (_MM_SHUFFLE(2, 0, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1xz = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(2, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1xz = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(2, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1xz = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(2, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1xz = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(2, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_11xz = (_MM_SHUFFLE(2, 0, 0, 0) << 16) | (0xc << 3) | 2,
		_1xyz = (_MM_SHUFFLE(2, 1, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yyz = (_MM_SHUFFLE(2, 1, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zyz = (_MM_SHUFFLE(2, 1, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wyz = (_MM_SHUFFLE(2, 1, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1yz = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(2, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1yz = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(2, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1yz = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(2, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1yz = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(2, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_11yz = (_MM_SHUFFLE(2, 1, 0, 0) << 16) | (0xc << 3) | 2,
		_1xzz = (_MM_SHUFFLE(2, 2, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yzz = (_MM_SHUFFLE(2, 2, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zzz = (_MM_SHUFFLE(2, 2, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wzz = (_MM_SHUFFLE(2, 2, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1zz = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(2, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1zz = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(2, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1zz = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(2, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1zz = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(2, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_11zz = (_MM_SHUFFLE(2, 2, 0, 0) << 16) | (0xc << 3) | 2,
		_1xwz = (_MM_SHUFFLE(2, 3, 0, 0) << 16) | (0x8 << 3) | 2,
		_1ywz = (_MM_SHUFFLE(2, 3, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zwz = (_MM_SHUFFLE(2, 3, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wwz = (_MM_SHUFFLE(2, 3, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1wz = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(2, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1wz = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(2, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1wz = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(2, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1wz = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(2, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_11wz = (_MM_SHUFFLE(2, 3, 0, 0) << 16) | (0xc << 3) | 2,
		_xx1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 0, 0) << 8) | (0x2 << 3) | 2,
		_yx1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 0, 1) << 8) | (0x2 << 3) | 2,
		_zx1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 0, 2) << 8) | (0x2 << 3) | 2,
		_wx1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 0, 3) << 8) | (0x2 << 3) | 2,
		_1x1z = (_MM_SHUFFLE(0, 0, 2, 0) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xy1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 1, 0) << 8) | (0x2 << 3) | 2,
		_yy1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 1, 1) << 8) | (0x2 << 3) | 2,
		_zy1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 1, 2) << 8) | (0x2 << 3) | 2,
		_wy1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 1, 3) << 8) | (0x2 << 3) | 2,
		_1y1z = (_MM_SHUFFLE(0, 0, 2, 1) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xz1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 2, 0) << 8) | (0x2 << 3) | 2,
		_yz1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 2, 1) << 8) | (0x2 << 3) | 2,
		_zz1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 2, 2) << 8) | (0x2 << 3) | 2,
		_wz1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 2, 3) << 8) | (0x2 << 3) | 2,
		_1z1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xw1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x2 << 3) | 2,
		_yw1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 1) << 8) | (0x2 << 3) | 2,
		_zw1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 2) << 8) | (0x2 << 3) | 2,
		_ww1z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0x2 << 3) | 2,
		_1w1z = (_MM_SHUFFLE(0, 0, 2, 3) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_x11z = (_MM_SHUFFLE(0, 0, 2, 0) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_y11z = (_MM_SHUFFLE(0, 0, 2, 1) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_z11z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_w11z = (_MM_SHUFFLE(0, 0, 2, 3) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_111z = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0xe << 3) | 2,
		_1xxw = (_MM_SHUFFLE(3, 0, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yxw = (_MM_SHUFFLE(3, 0, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zxw = (_MM_SHUFFLE(3, 0, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wxw = (_MM_SHUFFLE(3, 0, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1xw = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1xw = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1xw = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1xw = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x4 << 3) | 2,
		_11xw = (_MM_SHUFFLE(3, 0, 0, 0) << 16) | (0xc << 3) | 2,
		_1xyw = (_MM_SHUFFLE(3, 1, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yyw = (_MM_SHUFFLE(3, 1, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zyw = (_MM_SHUFFLE(3, 1, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wyw = (_MM_SHUFFLE(3, 1, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1yw = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1yw = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1yw = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1yw = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x4 << 3) | 2,
		_11yw = (_MM_SHUFFLE(3, 1, 0, 0) << 16) | (0xc << 3) | 2,
		_1xzw = (_MM_SHUFFLE(3, 2, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yzw = (_MM_SHUFFLE(3, 2, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zzw = (_MM_SHUFFLE(3, 2, 2, 0) << 16) | (0x8 << 3) | 2,
		_1wzw = (_MM_SHUFFLE(3, 2, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1zw = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1zw = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1zw = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1zw = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 2, 3, 0) << 8) | (0x4 << 3) | 2,
		_11zw = (_MM_SHUFFLE(3, 2, 0, 0) << 16) | (0xc << 3) | 2,
		_1xww = (_MM_SHUFFLE(3, 3, 0, 0) << 16) | (0x8 << 3) | 2,
		_1yww = (_MM_SHUFFLE(3, 3, 1, 0) << 16) | (0x8 << 3) | 2,
		_1zww = (_MM_SHUFFLE(3, 3, 2, 0) << 16) | (0x8 << 3) | 2,
		_1www = (_MM_SHUFFLE(3, 3, 3, 0) << 16) | (0x8 << 3) | 2,
		_x1ww = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_y1ww = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_z1ww = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_w1ww = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 3, 3, 0) << 8) | (0x4 << 3) | 2,
		_11ww = (_MM_SHUFFLE(3, 3, 0, 0) << 16) | (0xc << 3) | 2,
		_xx1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 0, 0) << 8) | (0x2 << 3) | 2,
		_yx1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 0, 1) << 8) | (0x2 << 3) | 2,
		_zx1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 0, 2) << 8) | (0x2 << 3) | 2,
		_wx1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 0, 3) << 8) | (0x2 << 3) | 2,
		_1x1w = (_MM_SHUFFLE(0, 0, 3, 0) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xy1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 1, 0) << 8) | (0x2 << 3) | 2,
		_yy1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 1, 1) << 8) | (0x2 << 3) | 2,
		_zy1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 1, 2) << 8) | (0x2 << 3) | 2,
		_wy1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 1, 3) << 8) | (0x2 << 3) | 2,
		_1y1w = (_MM_SHUFFLE(0, 0, 3, 1) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xz1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 2, 0) << 8) | (0x2 << 3) | 2,
		_yz1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 2, 1) << 8) | (0x2 << 3) | 2,
		_zz1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 2, 2) << 8) | (0x2 << 3) | 2,
		_wz1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 2, 3) << 8) | (0x2 << 3) | 2,
		_1z1w = (_MM_SHUFFLE(0, 0, 3, 2) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_xw1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 0) << 8) | (0x2 << 3) | 2,
		_yw1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 1) << 8) | (0x2 << 3) | 2,
		_zw1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 2) << 8) | (0x2 << 3) | 2,
		_ww1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0x2 << 3) | 2,
		_1w1w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 3, 0, 3) << 8) | (0xa << 3) | 2,
		_x11w = (_MM_SHUFFLE(0, 0, 3, 0) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_y11w = (_MM_SHUFFLE(0, 0, 3, 1) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_z11w = (_MM_SHUFFLE(0, 0, 3, 2) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_w11w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(1, 3, 3, 0) << 8) | (0x6 << 3) | 2,
		_111w = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(0, 3, 3, 3) << 8) | (0xe << 3) | 2,
		_xxx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 0, 0) << 8) | (0x1 << 3) | 2,
		_yxx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 0, 1) << 8) | (0x1 << 3) | 2,
		_zxx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 0, 2) << 8) | (0x1 << 3) | 2,
		_wxx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 0, 3) << 8) | (0x1 << 3) | 2,
		_1xx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xyx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 1, 0) << 8) | (0x1 << 3) | 2,
		_yyx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 1, 1) << 8) | (0x1 << 3) | 2,
		_zyx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 1, 2) << 8) | (0x1 << 3) | 2,
		_wyx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 1, 3) << 8) | (0x1 << 3) | 2,
		_1yx1 = (_MM_SHUFFLE(0, 0, 0, 1) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xzx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 2, 0) << 8) | (0x1 << 3) | 2,
		_yzx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 2, 1) << 8) | (0x1 << 3) | 2,
		_zzx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 2, 2) << 8) | (0x1 << 3) | 2,
		_wzx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 2, 3) << 8) | (0x1 << 3) | 2,
		_1zx1 = (_MM_SHUFFLE(0, 0, 0, 2) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xwx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x1 << 3) | 2,
		_ywx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 1) << 8) | (0x1 << 3) | 2,
		_zwx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 2) << 8) | (0x1 << 3) | 2,
		_wwx1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0x1 << 3) | 2,
		_1wx1 = (_MM_SHUFFLE(0, 0, 0, 3) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_x1x1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_y1x1 = (_MM_SHUFFLE(0, 0, 0, 1) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_z1x1 = (_MM_SHUFFLE(0, 0, 0, 2) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_w1x1 = (_MM_SHUFFLE(0, 0, 0, 3) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_11x1 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0xd << 3) | 2,
		_xxy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 0, 0) << 8) | (0x1 << 3) | 2,
		_yxy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 0, 1) << 8) | (0x1 << 3) | 2,
		_zxy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 0, 2) << 8) | (0x1 << 3) | 2,
		_wxy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 0, 3) << 8) | (0x1 << 3) | 2,
		_1xy1 = (_MM_SHUFFLE(0, 0, 1, 0) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xyy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 1, 0) << 8) | (0x1 << 3) | 2,
		_yyy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 1, 1) << 8) | (0x1 << 3) | 2,
		_zyy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 1, 2) << 8) | (0x1 << 3) | 2,
		_wyy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 1, 3) << 8) | (0x1 << 3) | 2,
		_1yy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xzy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 2, 0) << 8) | (0x1 << 3) | 2,
		_yzy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 2, 1) << 8) | (0x1 << 3) | 2,
		_zzy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 2, 2) << 8) | (0x1 << 3) | 2,
		_wzy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 2, 3) << 8) | (0x1 << 3) | 2,
		_1zy1 = (_MM_SHUFFLE(0, 0, 1, 2) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xwy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x1 << 3) | 2,
		_ywy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 1) << 8) | (0x1 << 3) | 2,
		_zwy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 2) << 8) | (0x1 << 3) | 2,
		_wwy1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0x1 << 3) | 2,
		_1wy1 = (_MM_SHUFFLE(0, 0, 1, 3) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_x1y1 = (_MM_SHUFFLE(0, 0, 1, 0) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_y1y1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_z1y1 = (_MM_SHUFFLE(0, 0, 1, 2) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_w1y1 = (_MM_SHUFFLE(0, 0, 1, 3) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_11y1 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0xd << 3) | 2,
		_xxz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 0, 0) << 8) | (0x1 << 3) | 2,
		_yxz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 0, 1) << 8) | (0x1 << 3) | 2,
		_zxz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 0, 2) << 8) | (0x1 << 3) | 2,
		_wxz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 0, 3) << 8) | (0x1 << 3) | 2,
		_1xz1 = (_MM_SHUFFLE(0, 0, 2, 0) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xyz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 1, 0) << 8) | (0x1 << 3) | 2,
		_yyz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 1, 1) << 8) | (0x1 << 3) | 2,
		_zyz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 1, 2) << 8) | (0x1 << 3) | 2,
		_wyz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 1, 3) << 8) | (0x1 << 3) | 2,
		_1yz1 = (_MM_SHUFFLE(0, 0, 2, 1) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xzz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 2, 0) << 8) | (0x1 << 3) | 2,
		_yzz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 2, 1) << 8) | (0x1 << 3) | 2,
		_zzz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 2, 2) << 8) | (0x1 << 3) | 2,
		_wzz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 2, 3) << 8) | (0x1 << 3) | 2,
		_1zz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xwz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x1 << 3) | 2,
		_ywz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 1) << 8) | (0x1 << 3) | 2,
		_zwz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 2) << 8) | (0x1 << 3) | 2,
		_wwz1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0x1 << 3) | 2,
		_1wz1 = (_MM_SHUFFLE(0, 0, 2, 3) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_x1z1 = (_MM_SHUFFLE(0, 0, 2, 0) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_y1z1 = (_MM_SHUFFLE(0, 0, 2, 1) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_z1z1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_w1z1 = (_MM_SHUFFLE(0, 0, 2, 3) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_11z1 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0xd << 3) | 2,
		_xxw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 0, 0) << 8) | (0x1 << 3) | 2,
		_yxw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 0, 1) << 8) | (0x1 << 3) | 2,
		_zxw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 0, 2) << 8) | (0x1 << 3) | 2,
		_wxw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 0, 3) << 8) | (0x1 << 3) | 2,
		_1xw1 = (_MM_SHUFFLE(0, 0, 3, 0) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xyw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 1, 0) << 8) | (0x1 << 3) | 2,
		_yyw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 1, 1) << 8) | (0x1 << 3) | 2,
		_zyw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 1, 2) << 8) | (0x1 << 3) | 2,
		_wyw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 1, 3) << 8) | (0x1 << 3) | 2,
		_1yw1 = (_MM_SHUFFLE(0, 0, 3, 1) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xzw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 2, 0) << 8) | (0x1 << 3) | 2,
		_yzw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 2, 1) << 8) | (0x1 << 3) | 2,
		_zzw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 2, 2) << 8) | (0x1 << 3) | 2,
		_wzw1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 2, 3) << 8) | (0x1 << 3) | 2,
		_1zw1 = (_MM_SHUFFLE(0, 0, 3, 2) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_xww1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 0) << 8) | (0x1 << 3) | 2,
		_yww1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 1) << 8) | (0x1 << 3) | 2,
		_zww1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 2) << 8) | (0x1 << 3) | 2,
		_www1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0x1 << 3) | 2,
		_1ww1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 1, 0, 3) << 8) | (0x9 << 3) | 2,
		_x1w1 = (_MM_SHUFFLE(0, 0, 3, 0) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_y1w1 = (_MM_SHUFFLE(0, 0, 3, 1) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_z1w1 = (_MM_SHUFFLE(0, 0, 3, 2) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_w1w1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 1, 3, 0) << 8) | (0x5 << 3) | 2,
		_11w1 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 0, 3, 3) << 8) | (0xd << 3) | 2,
		_xx11 = (_MM_SHUFFLE(0, 0, 0, 0) << 16) | (0x3 << 3) | 2,
		_yx11 = (_MM_SHUFFLE(0, 0, 0, 1) << 16) | (0x3 << 3) | 2,
		_zx11 = (_MM_SHUFFLE(0, 0, 0, 2) << 16) | (0x3 << 3) | 2,
		_wx11 = (_MM_SHUFFLE(0, 0, 0, 3) << 16) | (0x3 << 3) | 2,
		_1x11 = (_MM_SHUFFLE(0, 0, 0, 0) << 16)| (_MM_SHUFFLE(3, 3, 0, 3) << 8) | (0xb << 3) | 2,
		_xy11 = (_MM_SHUFFLE(0, 0, 1, 0) << 16) | (0x3 << 3) | 2,
		_yy11 = (_MM_SHUFFLE(0, 0, 1, 1) << 16) | (0x3 << 3) | 2,
		_zy11 = (_MM_SHUFFLE(0, 0, 1, 2) << 16) | (0x3 << 3) | 2,
		_wy11 = (_MM_SHUFFLE(0, 0, 1, 3) << 16) | (0x3 << 3) | 2,
		_1y11 = (_MM_SHUFFLE(0, 0, 1, 1) << 16)| (_MM_SHUFFLE(3, 3, 0, 3) << 8) | (0xb << 3) | 2,
		_xz11 = (_MM_SHUFFLE(0, 0, 2, 0) << 16) | (0x3 << 3) | 2,
		_yz11 = (_MM_SHUFFLE(0, 0, 2, 1) << 16) | (0x3 << 3) | 2,
		_zz11 = (_MM_SHUFFLE(0, 0, 2, 2) << 16) | (0x3 << 3) | 2,
		_wz11 = (_MM_SHUFFLE(0, 0, 2, 3) << 16) | (0x3 << 3) | 2,
		_1z11 = (_MM_SHUFFLE(0, 0, 2, 2) << 16)| (_MM_SHUFFLE(3, 3, 0, 3) << 8) | (0xb << 3) | 2,
		_xw11 = (_MM_SHUFFLE(0, 0, 3, 0) << 16) | (0x3 << 3) | 2,
		_yw11 = (_MM_SHUFFLE(0, 0, 3, 1) << 16) | (0x3 << 3) | 2,
		_zw11 = (_MM_SHUFFLE(0, 0, 3, 2) << 16) | (0x3 << 3) | 2,
		_ww11 = (_MM_SHUFFLE(0, 0, 3, 3) << 16) | (0x3 << 3) | 2,
		_1w11 = (_MM_SHUFFLE(0, 0, 3, 3) << 16)| (_MM_SHUFFLE(3, 3, 0, 3) << 8) | (0xb << 3) | 2,
		_x111 = (_MM_SHUFFLE(3, 2, 1, 0) << 16) | (0x7 << 3) | 2,
		_y111 = (_MM_SHUFFLE(3, 2, 1, 1) << 16) | (0x7 << 3) | 2,
		_z111 = (_MM_SHUFFLE(3, 2, 1, 2) << 16) | (0x7 << 3) | 2,
		_w111 = (_MM_SHUFFLE(3, 2, 1, 3) << 16) | (0x7 << 3) | 2,
	};

	enum InsertMask
	{
		_0000 = 0,
		_0001 = 8,
		_0010 = 4,
		_0011 = 12,

		_0100 = 2,
		_0101 = 10,
		_0110 = 6,
		_0111 = 14,

		_1000 = 1,
		_1001 = 9,
		_1010 = 5,
		_1011 = 13,

		_1100 = 3,
		_1101 = 11,
		_1110 = 7,
		_1111 = 15,
	};

	template<int S, Mask T> struct VecShuffleInnerInternal;

	template<Mask T> struct VecShuffleInnerInternal<1, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			retValue = _mm_shuffle_ps(v, constReg, (T >> 16) & 0xff);
			retValue = _mm_shuffle_ps(v, retValue, (T >>  8) & 0xff);
			return retValue;
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<2, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			retValue = _mm_shuffle_ps(v, constReg, (T >> 16) & 0xff);
			retValue = _mm_shuffle_ps(v, retValue, (T >>  8) & 0xff);
			return retValue;
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<3, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			return _mm_shuffle_ps(v, constReg, (T >> 16) & 0xff);
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<4, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			retValue = _mm_shuffle_ps(v, constReg, (T >> 16) & 0xff);
			retValue = _mm_shuffle_ps(retValue, v, (T >>  8) & 0xff);
			return retValue;
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<5, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			retValue = _mm_shuffle_ps(v,        constReg, (T >> 16) & 0xff);
			retValue = _mm_shuffle_ps(retValue, retValue, (T >>  8) & 0xff);
			return retValue;
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<6, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			retValue = _mm_shuffle_ps(v,        constReg, (T >> 16) & 0xff);
			retValue = _mm_shuffle_ps(retValue, retValue, (T >>  8) & 0xff);
			return retValue;
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<7, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			if (((T >> 16) & 0xff) != _MM_SHUFFLE(3, 2, 1, 0))
				retValue = _mm_move_ss(constReg, _mm_castsi128_ps(_mm_shuffle_epi32(_mm_castps_si128(v), (T >> 16) & 0xff)));
			else
				retValue = _mm_move_ss(constReg, v);
			return retValue;
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<8, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			if (((T >> 16) & 0xff) != _MM_SHUFFLE(3, 2, 1, 0))
				retValue = _mm_move_ss(_mm_castsi128_ps(_mm_shuffle_epi32(_mm_castps_si128(v), (T >> 16) & 0xff)), constReg);
			else
				retValue = _mm_move_ss(v, constReg);
			return retValue;
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<9, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			retValue = _mm_shuffle_ps(v,        constReg, (T >> 16) & 0xff);
			retValue = _mm_shuffle_ps(retValue, retValue, (T >>  8) & 0xff);
			return retValue;
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<0xA, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			retValue = _mm_shuffle_ps(v,        constReg, (T >> 16) & 0xff);
			retValue = _mm_shuffle_ps(retValue, retValue, (T >>  8) & 0xff);
			return retValue;
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<0xB, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			retValue = _mm_shuffle_ps(v,        constReg, (T >> 16) & 0xff);
			retValue = _mm_shuffle_ps(retValue, retValue, (T >>  8) & 0xff);
			return retValue;
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<0xC, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			return _mm_shuffle_ps(constReg, v, (T >> 16) & 0xff);
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<0xD, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			retValue = _mm_shuffle_ps(v,        constReg, (T >> 16) & 0xff);
			retValue = _mm_shuffle_ps(retValue, retValue, (T >>  8) & 0xff);
			return retValue;
		}
	};

	template<Mask T> struct VecShuffleInnerInternal<0xE, T>
	{
		static FORCEINLINE Vec apply(Vec v, Vec constReg)
		{
			Vec retValue;
			retValue = _mm_shuffle_ps(v,        constReg, (T >> 16) & 0xff);
			retValue = _mm_shuffle_ps(retValue, retValue, (T >>  8) & 0xff);
			return retValue;
		}
	};

	template<int S, Mask T> struct VecShuffleOuterInternal;

	template<Mask T> struct VecShuffleOuterInternal<0, T>
	{
		static FORCEINLINE Vec apply(Vec v)
		{
			return _mm_castsi128_ps(_mm_shuffle_epi32(_mm_castps_si128(v), (T >> 16) & 0xff));
		}
	};

	template<Mask T> struct VecShuffleOuterInternal<1, T>
	{
		static FORCEINLINE Vec apply(Vec v)
		{
			return VecShuffleInnerInternal<(T >> 3) & 0xf, T>::apply(v, vecZero());
		}
	};

	template<Mask T> struct VecShuffleOuterInternal<2, T>
	{
		static FORCEINLINE Vec apply(Vec v)
		{
			return VecShuffleInnerInternal<(T >> 3) & 0xf, T>::apply(v, vecOne);
		}
	};
};

template<VecMask::Mask T> FORCEINLINE Vec vecShuffle(Vec v)
{
	return VecMask::VecShuffleOuterInternal<T & 0x7, T>::apply(v);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_x1y1>(Vec v)
{
	return _mm_unpacklo_ps(v, vecOne);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_x0y0>(Vec v)
{
	return _mm_unpacklo_ps(v, vecZero());
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_1x1y>(Vec v)
{
	return _mm_unpacklo_ps(vecOne, v);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_0x0y>(Vec v)
{
	return _mm_unpacklo_ps(vecZero(), v);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_z1w1>(Vec v)
{
	return _mm_unpackhi_ps(v, vecOne);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_z0w0>(Vec v)
{
	return _mm_unpackhi_ps(v, vecZero());
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_1z1w>(Vec v)
{
	return _mm_unpackhi_ps(vecOne, v);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_0z0w>(Vec v)
{
	return _mm_unpackhi_ps(vecZero(), v);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_xyz0>(Vec v)
{
	// Could do _mm_blend_ps(v, vecZero(), 8) but that forces usage of
	// a zero register. With this implementation we can get away with
	// less registers.
	return _mm_insert_ps(v, v, 8);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_xyz1>(Vec v)
{
	return _mm_blend_ps(v, vecOne, 8);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_x0z0>(Vec v)
{
	return _mm_insert_ps(v, v, 2|8);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_yzw0>(Vec v)
{
	return _mm_castsi128_ps(_mm_srli_si128(_mm_castps_si128(v), 4));
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_zw00>(Vec v)
{
	return _mm_castsi128_ps(_mm_srli_si128(_mm_castps_si128(v), 8));
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_w000>(Vec v)
{
	return _mm_castsi128_ps(_mm_srli_si128(_mm_castps_si128(v), 12));
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_0xyz>(Vec v)
{
	return _mm_castsi128_ps(_mm_slli_si128(_mm_castps_si128(v), 4));
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_00xy>(Vec v)
{
	return _mm_castsi128_ps(_mm_slli_si128(_mm_castps_si128(v), 8));
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_000x>(Vec v)
{
	return _mm_castsi128_ps(_mm_slli_si128(_mm_castps_si128(v), 12));
}

template<VecMask::Mask T> FORCEINLINE Vec vecPermute(Vec a, Vec b)
{
	return _mm_shuffle_ps(a, b, (T >> 16) & 0xff);
}

template<VecMask::Mask T> FORCEINLINE Vec vecPermute(Vec x, Vec y, Vec z, Vec w)
{
	constexpr uint mask = (T >> 16) & 0xff;

	constexpr uint maskX0Y0 = (mask & 0x03) | ((mask & 0x0c) << 2);
	constexpr uint maskXXYY = maskX0Y0 | (maskX0Y0 << 2);
	Vec xxyy = _mm_shuffle_ps(x, y, maskXXYY);
	Vec xyxy = vecShuffle<VecMask::_xzxz>(xxyy);

	constexpr uint maskZ0W0 = ((mask & 0x30) >> 4) | ((mask & 0xc0) >> 2);
	constexpr uint maskZZWW = maskZ0W0 | (maskZ0W0 << 2);
	Vec zzww = _mm_shuffle_ps(z, w, maskZZWW);
	Vec zwzw = vecShuffle<VecMask::_xzxz>(zzww);

	return vecPermute<VecMask::_xyxy>(xyxy, zwzw);
}

FORCEINLINE Vec vecMergeLowWord(Vec v0, Vec v1)
{
	return _mm_unpackhi_ps(v0, v1);
}

FORCEINLINE Vec vecMergeHighWord(Vec v0, Vec v1)
{
	return _mm_unpacklo_ps(v0, v1);
}

template<VecMask::InsertMask T> FORCEINLINE Vec vecInsert(Vec v1, Vec v2)
{
	return _mm_blend_ps(v1, v2, int(T));
}

FORCEINLINE Vec vecMaskSel(Vec falseValue, Vec trueValue, Vec mostSignificantBitMask)
{
	return _mm_blendv_ps(falseValue, trueValue, mostSignificantBitMask);
}

FORCEINLINE IntVec vecMaskSel(IntVec falseValue, IntVec trueValue, IntVec mostSignificantBitMask)
{
	return _mm_castps_si128(_mm_blendv_ps(_mm_castsi128_ps(falseValue), _mm_castsi128_ps(trueValue), _mm_castsi128_ps(mostSignificantBitMask)));
}

FORCEINLINE Vec vecBitSel(Vec falseValue, Vec trueValue, Vec mask)
{
	return _mm_or_ps(_mm_and_ps(mask, trueValue), _mm_andnot_ps(mask, falseValue));
}

FORCEINLINE IntVec vecBitSel(IntVec falseValue, IntVec trueValue, IntVec mask)
{
	return _mm_or_si128(_mm_and_si128(mask, trueValue), _mm_andnot_si128(mask, falseValue));
}

// works with all-bits on Neon and MSB on SSE
FORCEINLINE Vec vecSel(Vec falseValue, Vec trueValue, Vec mask)
{
	return vecMaskSel(falseValue, trueValue, mask);
}

// works with all-bits on Neon and MSB on SSE
FORCEINLINE IntVec vecSel(IntVec falseValue, IntVec trueValue, IntVec mask)
{
	return vecMaskSel(falseValue, trueValue, mask);
}

FORCEINLINE Vec vecFloor(Vec r)
{
	return _mm_round_ps(r, _MM_FROUND_TO_NEG_INF|_MM_FROUND_NO_EXC);
}

FORCEINLINE Vec vecCeil(Vec r)
{
	return _mm_round_ps(r, _MM_FROUND_TO_POS_INF|_MM_FROUND_NO_EXC);
}

FORCEINLINE Vec vecFraction(Vec r)
{
	return _mm_sub_ps(r, vecFloor(r));
}

FORCEINLINE Vec vecRound(Vec v)
{
	return _mm_round_ps(v, _MM_FROUND_RINT);
}

FORCEINLINE IntVec vecRoundToInt(Vec v)
{
	return _mm_cvtps_epi32(v);
}

FORCEINLINE Vec vecTrunc(Vec v)
{
	return _mm_cvtepi32_ps(_mm_cvttps_epi32(v));
}

FORCEINLINE IntVec vecTruncToInt(Vec v)
{
	return _mm_cvttps_epi32(v);
}

FORCEINLINE Vec intVecToVec(IntVec xyzw)
{
	return _mm_cvtepi32_ps(xyzw);
}

FORCEINLINE Vec intVecCastToVec(IntVec xyzw)
{
	return _mm_castsi128_ps(xyzw);
}

FORCEINLINE IntVec vecCastToIntVec(Vec xyzw)
{
	return _mm_castps_si128(xyzw);
}

FORCEINLINE float vecStore1(Vec v)
{
	return _mm_cvtss_f32(v);
}

FORCEINLINE int vecStore1(IntVec v)
{
	return _mm_cvtsi128_si32(v);
}

FORCEINLINE void vecStore2Unaligned(Vec v, float* out)
{
	_mm_storel_epi64(reinterpret_cast<__m128i*>(out), _mm_castps_si128(v));
}

FORCEINLINE void vecStore2Unaligned(IntVec v, int* out)
{
	out[0] = _mm_cvtsi128_si32(v);
	out[1] = _mm_extract_epi32(v, 1);
}

FORCEINLINE void vecStore3Unaligned(Vec v, float* out)
{
	_mm_storel_epi64(reinterpret_cast<__m128i*>(out), _mm_castps_si128(v));
	_mm_store_ss(&out[2], vecShuffle<VecMask::_zzzz>(v));
}

FORCEINLINE void vecStore4Unaligned(Vec v, float* out)
{
	_mm_storeu_ps(out, v);
}

FORCEINLINE Vec vecMin(Vec a, Vec b)
{
	return _mm_min_ps(a, b);
}

FORCEINLINE Vec vecMax(Vec a, Vec b)
{
	return _mm_max_ps(a, b);
}

FORCEINLINE IntVec vecMin(IntVec a, IntVec b)
{
	return _mm_min_epi32(a, b);
}

FORCEINLINE IntVec vecMax(IntVec a, IntVec b)
{
	return _mm_max_epi32(a, b);
}

FORCEINLINE IntVec vecMinAsUint(IntVec a, IntVec b)
{
	return _mm_min_epu32(a, b);
}

FORCEINLINE IntVec vecMaxAsUint(IntVec a, IntVec b)
{
	return _mm_max_epu32(a, b);
}

FORCEINLINE Vec vecNeg(Vec r)
{
	return _mm_sub_ps(vecZero(), r);
}

FORCEINLINE Vec vecAdd(Vec r1, Vec r2)
{
	return _mm_add_ps(r1, r2);
}

FORCEINLINE Vec vecSub(Vec r1, Vec r2)
{
	return _mm_sub_ps(r1, r2);
}

FORCEINLINE IntVec vecAdd(IntVec r1, IntVec r2)
{
	return _mm_add_epi32(r1, r2);
}

FORCEINLINE IntVec vecSub(IntVec r1, IntVec r2)
{
	return _mm_sub_epi32(r1, r2);
}

FORCEINLINE Vec vecMul(Vec r1, Vec r2)
{
	return _mm_mul_ps(r1, r2);
}

FORCEINLINE Vec vecDiv(Vec v1, Vec v2)
{
	return _mm_div_ps(v1, v2);
}

FORCEINLINE Vec vecMulAdd(Vec mul1, Vec mul2, Vec add)
{
	return _mm_add_ps(_mm_mul_ps(mul1, mul2), add);
}

FORCEINLINE Vec vecMulSub(Vec subFrom, Vec mul1, Vec mul2)
{
	return _mm_sub_ps(subFrom, _mm_mul_ps(mul1, mul2));
}

FORCEINLINE Vec vecLerp(Vec a, Vec b, Vec t)
{
	const Vec one_minus_t = vecSub(vecOne, t);
	return vecAdd(vecMul(a, one_minus_t), vecMul(b, t));
}

FORCEINLINE Vec vecNegMulSub(Vec v1, Vec v2, Vec sub)
{
	return _mm_sub_ps(sub, _mm_mul_ps(v1, v2));
}

FORCEINLINE Vec vecRecipNoNewtonRaphson(Vec r)
{
	return _mm_rcp_ps(r);
}

FORCEINLINE Vec vecRecipSqrtNoNewtonRaphson(Vec v)
{
	return _mm_rsqrt_ps(v);
}

FORCEINLINE Vec vecRecipSqrtNoNewtonRaphson1(Vec v)
{
	return _mm_rsqrt_ss(v);
}

FORCEINLINE Vec vecRecipEst(Vec r)
{
	return vecRecipNoNewtonRaphson(r);
}

FORCEINLINE Vec vecRecipFast(Vec r)
{
	// Do one iteration of Newton-Raphson
	const Vec two = vecTwo;

	const Vec est = vecRecipNoNewtonRaphson(r);

	const Vec twoMinusRMulEst = vecSub(two, vecMul(est, r));

	return vecMul(twoMinusRMulEst, est);
}

FORCEINLINE Vec vecRecip(Vec r)
{
	// Do two iterations of Newton-Raphson
	const Vec two = vecTwo;

	const Vec recipEst = vecRecipEst(r);

	const Vec twoMinusRMulEst = vecNegMulSub(recipEst, r, two);

	return vecMul(twoMinusRMulEst, recipEst);
}

FORCEINLINE Vec vecRecipSqrtEst(Vec v)
{
	return vecRecipSqrtNoNewtonRaphson(v);
}

FORCEINLINE Vec vecRecipSqrtFast(Vec v)
{
	// Do one iteration of Newton-Raphson
	const Vec half = vecHalf;

	const Vec estimate = vecRecipSqrtNoNewtonRaphson(v);

	const Vec halfV = vecMul(v, half);

    const Vec estimateSqr = vecMul(estimate, estimate);

	const Vec halfMinusEstSqrHalfV = vecNegMulSub(halfV, estimateSqr, half);

	return vecMulAdd(estimate, halfMinusEstSqrHalfV, estimate);
}

FORCEINLINE Vec vecRecipSqrt(Vec v)
{
	// Do two iterations of Newton-Raphson
	const Vec half = vecHalf;

    const Vec estimate = vecRecipSqrtFast(v);

	const Vec halfV = vecMul(v, half);

    const Vec estimateSqr = vecMul(estimate, estimate);

	const Vec halfMinusEstSqrHalfV = vecNegMulSub(halfV, estimateSqr, half);

    return vecMulAdd(estimate, halfMinusEstSqrHalfV, estimate);
}

FORCEINLINE Vec vecRecipSqrtFast1(Vec v)
{
	// Do one iteration of Newton-Raphson
	const Vec half = vecHalf;
	const Vec estimate = vecRecipSqrtNoNewtonRaphson1(v);
	const Vec halfV = _mm_mul_ss(v, half);
    const Vec estimateSqr = _mm_mul_ss(estimate, estimate);
	const Vec halfMinusEstSqrHalfV = _mm_sub_ss(half, _mm_mul_ss(halfV, estimateSqr));
	return _mm_add_ss(_mm_mul_ss(estimate, halfMinusEstSqrHalfV), estimate);
}

FORCEINLINE Vec vecRecipSqrt1(Vec v)
{
	// Do two iterations of Newton-Raphson
	const Vec half = vecHalf;
    const Vec estimate = vecRecipSqrtFast1(v);
	const Vec halfV = _mm_mul_ss(v, half);
    const Vec estimateSqr = _mm_mul_ss(estimate, estimate);
	const Vec halfMinusEstSqrHalfV = _mm_sub_ss(half, _mm_mul_ss(halfV, estimateSqr));
	return _mm_add_ss(_mm_mul_ss(estimate, halfMinusEstSqrHalfV), estimate);
}

FORCEINLINE Vec vecSqrt1(Vec v1)
{
	return _mm_sqrt_ss(v1);
}

FORCEINLINE Vec vecSqrt(Vec v1)
{
	return _mm_sqrt_ps(v1);
}

FORCEINLINE Vec vecDivEst(Vec v1, Vec v2)
{
	Vec inv = vecRecipEst(v2);
	return vecMul(v1, inv);
}

FORCEINLINE Vec vecAnd(Vec r1, Vec r2)
{
	return _mm_and_ps(r1, r2);
}

FORCEINLINE Vec vecAndNot(Vec mask, Vec v)
{
	return _mm_andnot_ps(mask, v);
}

FORCEINLINE Vec vecOr(Vec r1, Vec r2)
{
	return _mm_or_ps(r1, r2);
}

FORCEINLINE Vec vecXor(Vec r1, Vec r2)
{
	return _mm_xor_ps(r1, r2);
}

FORCEINLINE Vec vecNot(Vec v)
{
	Vec ffffffff = _mm_undefined_ps(); 
	ffffffff = _mm_cmpeq_ps(ffffffff, ffffffff);
	return _mm_xor_ps(v, ffffffff);
}

FORCEINLINE IntVec vecAnd(IntVec r1, IntVec r2)
{
	return _mm_and_si128(r1, r2);
}

FORCEINLINE IntVec vecAndNot(IntVec mask, IntVec v)
{
	return _mm_andnot_si128(mask, v);
}

FORCEINLINE IntVec vecOr(IntVec r1, IntVec r2)
{
	return _mm_or_si128(r1, r2);
}

FORCEINLINE IntVec vecXor(IntVec r1, IntVec r2)
{
	return _mm_xor_si128(r1, r2);
}

FORCEINLINE IntVec vecNot(IntVec v)
{
	IntVec ffffffff = _mm_undefined_si128(); 
	ffffffff = _mm_cmpeq_epi32(ffffffff, ffffffff);
	return _mm_xor_si128(v, ffffffff);
}

FORCEINLINE Vec vecCmpGT(Vec r1, Vec r2)
{
	return _mm_cmpgt_ps(r1, r2);
}

FORCEINLINE Vec vecCmpGE(Vec r1, Vec r2)
{
	return _mm_cmpge_ps(r1, r2);
}

FORCEINLINE Vec vecCmpLT(Vec r1, Vec r2)
{
	return _mm_cmplt_ps(r1, r2);
}

FORCEINLINE Vec vecCmpLE(Vec r1, Vec r2)
{
	return _mm_cmple_ps(r1, r2);
}

FORCEINLINE Vec vecCmpEQ(Vec r1, Vec r2)
{
	return _mm_cmpeq_ps(r1, r2);
}

FORCEINLINE Vec vecCmpNE(Vec r1, Vec r2)
{
	return _mm_cmpneq_ps(r1, r2);
}

FORCEINLINE IntVec vecCmpGT(IntVec r1, IntVec r2)
{
	return _mm_cmpgt_epi32(r1, r2);
}

FORCEINLINE IntVec vecCmpLT(IntVec r1, IntVec r2)
{
	return _mm_cmplt_epi32(r1, r2);
}

FORCEINLINE IntVec vecCmpEQ(IntVec r1, IntVec r2)
{
	return _mm_cmpeq_epi32(r1, r2);
}

FORCEINLINE Vec vecAbs(Vec v)
{
	return _mm_castsi128_ps(_mm_srli_epi32(_mm_slli_epi32(_mm_castps_si128(v), 1), 1));
}

FORCEINLINE IntVec vecAbs(IntVec v)
{
	return _mm_abs_epi32(v);
}

FORCEINLINE Vec vecDot2(Vec u, Vec v)
{
	Vec prod = _mm_mul_ps(u, v);
	Vec x = vecShuffle<VecMask::_xxxx>(prod);
	Vec y = vecShuffle<VecMask::_yyyy>(prod);
	return _mm_add_ps(x, y);
}

FORCEINLINE Vec vecDot3(Vec u, Vec v)
{
	Vec mulR = _mm_mul_ps(u, v);
	Vec vec1 = vecShuffle<VecMask::_xxxx>(mulR);
	Vec vec2 = vecShuffle<VecMask::_yyyy>(mulR);
	Vec vec3 = vecShuffle<VecMask::_zzzz>(mulR);
	return _mm_add_ps(_mm_add_ps(vec1, vec2), vec3);
}

FORCEINLINE Vec vecDot4(Vec u, Vec v)
{
	Vec mulR = vecMul(u, v);
	Vec tmp0 = vecAdd(mulR, vecShuffle<VecMask::_yxwz>(mulR));
	Vec tmp1 = vecShuffle<VecMask::_zwxy>(tmp0);
	return vecAdd(tmp0, tmp1);
}

FORCEINLINE Vec vecCross(Vec u, Vec v)
{
	Vec uYZX = vecShuffle<VecMask::_yzxw>(u);
	Vec vYZX = vecShuffle<VecMask::_yzxw>(v);
	Vec r = vecSub(vecMul(u, vYZX), vecMul(uYZX, v));
	return vecShuffle<VecMask::_yzxw>(r);
}

template<int bits>
FORCEINLINE IntVec vecShiftLeftLogical(IntVec v)
{
	return _mm_slli_epi32(v, bits);
}

template<int bits>
FORCEINLINE IntVec vecShiftRightLogical(IntVec v)
{
	return _mm_srli_epi32(v, bits);
}

template<int bits>
FORCEINLINE IntVec vecShiftRightArithmetic(IntVec v)
{
	return _mm_srai_epi32(v, bits);
}

FORCEINLINE IntVec vecPackU32ToU16(IntVec a)
{
	return _mm_packus_epi32(a, a);
}

FORCEINLINE IntVec vecPackU32ToU16(IntVec a, IntVec b)
{
	return _mm_packus_epi32(a, b);
}

FORCEINLINE IntVec vecPackS32ToS16(IntVec a)
{
	return _mm_packs_epi32(a, a);
}

FORCEINLINE IntVec vecPackS32ToS16(IntVec a, IntVec b)
{
	return _mm_packs_epi32(a, b);
}

FORCEINLINE IntVec vecUnpackU16ToU32(IntVec a)
{
	return _mm_unpacklo_epi16(a, vecZeroInt());
}

FORCEINLINE IntVec vecUnpackS16ToS32(IntVec a)
{
	IntVec sx = _mm_cmplt_epi16(a, vecZeroInt());
	return _mm_unpacklo_epi16(a, sx);
}
