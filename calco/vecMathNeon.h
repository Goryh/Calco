#include <arm_neon.h>

typedef float32x4_t Vec;
typedef float32_t Vec1;
typedef uint32x4_t VecShufMask;
typedef int32x4_t IntVec;
typedef int32_t IntVec1;

FORCEINLINE Vec vec(float x)
{
	return vdupq_n_f32(x);
}

FORCEINLINE Vec1 vec1(float x)
{
    return float32_t(x);
}

FORCEINLINE Vec1 vec1(Vec v)
{
    return vgetq_lane_f32(v, 0);
}

FORCEINLINE Vec vec(float x, float y)
{
	const Vec zero = vdupq_n_f32(0);
	Vec ret = vsetq_lane_f32(x, zero, 0);
	ret = vsetq_lane_f32(y, ret, 1);
	return ret;
}

FORCEINLINE Vec vec(float x, float y, float z, float w)
{
	alignas(16) float f4[4] = {x, y, z, w};
	return vld1q_f32(f4);
}

FORCEINLINE Vec vec(float x, float y, float z)
{
	return vec(x, y, z, 0.0f);
}

FORCEINLINE Vec vecUnaligned(float* unalignedMemory)
{
	return vld1q_f32(unalignedMemory);
}

FORCEINLINE IntVec intVec(int x)
{
	return vdupq_n_s32(x);
}

FORCEINLINE IntVec intVec(unsigned int x)
{
	return vdupq_n_u32(x);
}

FORCEINLINE IntVec intVec(int x, int y, int z, int w)
{
	alignas(16) int i4[4] = {x, y, z, w};
	return vld1q_s32(i4);
}

FORCEINLINE IntVec1 intVec1(int x)
{
	return x;
}

FORCEINLINE IntVec1 intVec1(unsigned int x)
{
	return *(int*)(&x);
}

FORCEINLINE Vec vecMask(bool val)
{
	unsigned int value = val ? 0xffffffff : 0;
	return vreinterpretq_f32_u32(vdupq_n_u32(value));
}

FORCEINLINE Vec vecMergeLowWord(Vec v0, Vec v1)
{
	return vzipq_f32(v0, v1).val[1];
}

FORCEINLINE Vec vecMergeHighWord(Vec v0, Vec v1)
{
	return vzipq_f32(v0, v1).val[0];
}

FORCEINLINE float vecStore1(Vec v)
{
	return vgetq_lane_f32(v, 0);
}

FORCEINLINE float vecStore1(Vec1 v)
{
    return (float)v;
}

FORCEINLINE int vecStore1(IntVec v)
{
	return vgetq_lane_s32(v, 0);
}

FORCEINLINE void vecStore2Unaligned(Vec v, float* out)
{
	out[0] = vgetq_lane_f32(v, 0);
	out[1] = vgetq_lane_f32(v, 1);
}

FORCEINLINE void vecStore2Unaligned(IntVec v, int* out)
{
	out[0] = vgetq_lane_s32(v, 0);
	out[1] = vgetq_lane_s32(v, 1);
}

FORCEINLINE void vecStore3Unaligned(Vec v, float* out)
{
	out[0] = vgetq_lane_f32(v, 0);
	out[1] = vgetq_lane_f32(v, 1);
	out[2] = vgetq_lane_f32(v, 2);
}

FORCEINLINE void vecStore4Unaligned(Vec v, float* out)
{
	out[0] = vgetq_lane_f32(v, 0);
	out[1] = vgetq_lane_f32(v, 1);
	out[2] = vgetq_lane_f32(v, 2);
	out[3] = vgetq_lane_f32(v, 3);
}

/**
 * Once we need to set at least 3 lanes of a vec, it is faster to just do a load
 * instead of more set lanes
 * Must be macros as the NEON builtins expect a constant literal for the arguments
 */
#define INTERNAL_MASK_LOAD(ret, x, y, z, w)				\
	{													\
		const uint32_t array_[4] = {x, y, z, w};		\
		const uint32x4_t uintVec = vld1q_u32(array_);	\
		ret = uintVec;									\
	}

#define INTERNAL_MASK_SET(ret, x, y, xLane, yLane)				\
	{															\
		const uint32x4_t zero = vdupq_n_u32(0);					\
		const uint32x4_t mask = vsetq_lane_u32(x, zero, xLane);	\
		ret = vsetq_lane_u32(y, mask, yLane);					\
	}

FORCEINLINE Vec vecSignMaskXYZ()
{
	uint32x4_t ret;
	INTERNAL_MASK_LOAD(ret, 0x80000000, 0x80000000, 0x80000000, 0)
	return vreinterpretq_f32_u32(ret);
}

FORCEINLINE Vec vecSignMaskXYZW()
{
	return vreinterpretq_f32_u32(vdupq_n_u32(0x80000000));
}

FORCEINLINE Vec vecSignMaskXZ()
{
	uint32x4_t ret;
	INTERNAL_MASK_LOAD(ret, 0x80000000, 0x80000000, 0, 2)
	return vreinterpretq_f32_u32(ret);
}

FORCEINLINE Vec vecSignMaskYW()
{
	uint32x4_t ret;
	INTERNAL_MASK_SET(ret, 0x80000000, 0x80000000, 1, 3)
	return vreinterpretq_f32_u32(ret);
}

FORCEINLINE Vec vecSignMask(Vec v)
{
	uint32x4_t bitMask = vdupq_n_u32(0x80000000);
	return vreinterpretq_f32_u32(vceqq_s32(vandq_s32(v, bitMask), bitMask));
}

FORCEINLINE Vec vecZero()
{
	return vdupq_n_f32(0);
}

FORCEINLINE IntVec vecZeroInt()
{
	return vdupq_n_u32(0);
}

const Vec vecOne = vec(1.0f);
const Vec vecHalf = vec(0.5f);
const Vec vecQuarter = vec(0.25f);
const Vec vecTwo = vec(2.0f);
const Vec vecNegOne = vec(-1.0f);

FORCEINLINE Vec vecMin(Vec a, Vec b)
{
	return vminq_f32(a, b);
}

FORCEINLINE Vec vecMax(Vec a, Vec b)
{
	return vmaxq_f32(a, b);
}

FORCEINLINE IntVec vecMin(IntVec a, IntVec b)
{
	return vminq_s32(a, b);
}

FORCEINLINE IntVec vecMax(IntVec a, IntVec b)
{
	return vmaxq_s32(a, b);
}

FORCEINLINE IntVec vecMinAsUint(IntVec a, IntVec b)
{
	return vminq_u32(a, b);
}

FORCEINLINE IntVec vecMaxAsUint(IntVec a, IntVec b)
{
	return vmaxq_u32(a, b);
}

FORCEINLINE Vec vecNeg(Vec r)
{
	return vsubq_f32(vecZero(), r);
}

FORCEINLINE Vec1 vecNeg(Vec1 r)
{
	return -r;
}

FORCEINLINE Vec vecAdd(Vec r1, Vec r2)
{
	return vaddq_f32(r1, r2);
}

FORCEINLINE Vec1 vecAdd(Vec1 r1, Vec1 r2)
{
	return r1 + r2;
}

FORCEINLINE Vec vecSub(Vec r1, Vec r2)
{
	return vsubq_f32(r1, r2);
}

FORCEINLINE Vec1 vecSub(Vec1 r1, Vec1 r2)
{
	return r1 - r2;
}

FORCEINLINE IntVec vecAdd(IntVec r1, IntVec r2)
{
	return vaddq_s32(r1, r2);
}

FORCEINLINE IntVec vecSub(IntVec r1, IntVec r2)
{
	return vsubq_s32(r1, r2);
}

FORCEINLINE Vec vecMul(Vec r1, Vec r2)
{
	return vmulq_f32(r1, r2);
}

FORCEINLINE Vec1 vecMul(Vec1 r1, Vec1 r2)
{
	return r1 * r2;
}

FORCEINLINE Vec vecDiv(Vec r1, Vec r2)
{
	return vdivq_f32(r1, r2);
}

FORCEINLINE Vec1 vecDiv(Vec1 r1, Vec1 r2)
{
	return r1 / r2;
}

FORCEINLINE Vec vecMulAdd(Vec mul1, Vec mul2, Vec add)
{
	return vfmaq_f32(add, mul1, mul2);
}

FORCEINLINE Vec1 vecMulAdd(Vec1 mul1, Vec1 mul2, Vec1 add)
{
	return add + mul1 * mul2;
}

FORCEINLINE Vec vecMulSub(Vec subFrom, Vec mul1, Vec mul2)
{
	return vmlsq_f32(subFrom, mul1, mul2);
}

FORCEINLINE Vec1 vecMulSub(Vec1 subFrom, Vec1 mul1, Vec1 mul2)
{
	return subFrom - mul1 * mul2;
}

FORCEINLINE Vec vecRecipNoNewtonRaphson(Vec v)
{
	// Warning: VRECPE is only good for about 8 bits of precision on most ARM chips (compared to 12 bits on x86)
	return vrecpeq_f32(v);
}

FORCEINLINE Vec1 vecRecipNoNewtonRaphson(Vec1 v)
{
	// Warning: VRECPE is only good for about 8 bits of precision on most ARM chips (compared to 12 bits on x86)
	return vrecpes_f32(v);
}

FORCEINLINE Vec vecRecipSqrtNoNewtonRaphson(Vec v)
{
	// Warning: VRSQRTE is only good for about 8 bits of precision on most ARM chips (compared to 12 bits on x86)
	return vrsqrteq_f32(v);
}

FORCEINLINE Vec1 vecRecipSqrtNoNewtonRaphson(Vec1 v)
{
	// Warning: VRSQRTE is only good for about 8 bits of precision on most ARM chips (compared to 12 bits on x86)
	return vrsqrtes_f32(v);
}

FORCEINLINE Vec vecRecipEst(Vec v)
{
	// Do one iteration of Newton-Raphson, because VRECPE is less accurate than x86 equivalent
	const Vec estimate = vecRecipNoNewtonRaphson(v);

	// NEON has an intrinsic that does one step of Newton-Raphson
	const Vec step = vrecpsq_f32(estimate, v);

	return vecMul(estimate, step);
}

FORCEINLINE Vec1 vecRecipEst(Vec1 v)
{
	// Do one iteration of Newton-Raphson, because VRECPE is less accurate than x86 equivalent
	const Vec1 estimate = vecRecipNoNewtonRaphson(v);

	// NEON has an intrinsic that does one step of Newton-Raphson
	const Vec1 step = vrecpss_f32(estimate, v);

	return vecMul(estimate, step);
}

FORCEINLINE Vec vecRecipFast(Vec v)
{
	// Do two iterations of Newton-Raphson, because VRECPE is less accurate than x86 equivalent
	const Vec estimate = vecRecipEst(v);

	// NEON has an intrinsic that does one step of Newton-Raphson
	const Vec step = vrecpsq_f32(estimate, v);

	return vecMul(estimate, step);
}

FORCEINLINE Vec vecRecip(Vec v)
{
	// Do two iterations of Newton-Raphson
	const Vec estimate = vecRecipEst(v);

	// NEON has an intrinsic that does one step of Newton-Raphson
	const Vec step = vrecpsq_f32(estimate, v);

	return vecMul(estimate, step);
}

FORCEINLINE Vec1 vecRecip(Vec1 v)
{
	// Do two iterations of Newton-Raphson
	const Vec1 estimate = vecRecipEst(v);

	// NEON has an intrinsic that does one step of Newton-Raphson
	const Vec1 step = vrecpss_f32(estimate, v);

	return vecMul(estimate, step);
}

FORCEINLINE Vec vecRecipSqrtEst(Vec v)
{
	// Do one iteration of Newton-Raphson, because VRSQRTE is less accurate than x86 equivalent
	const Vec estimate = vecRecipSqrtNoNewtonRaphson(v);
	const Vec estimateSqr = vecMul(estimate, estimate);

	// NEON has an intrinsic that does one step of Newton-Raphson
	const Vec step = vrsqrtsq_f32(estimateSqr, v);
	return vecMul(estimate, step);
}

FORCEINLINE Vec1 vecRecipSqrtEst(Vec1 v)
{
	// Do one iteration of Newton-Raphson, because VRSQRTE is less accurate than x86 equivalent
	const Vec1 estimate = vecRecipSqrtNoNewtonRaphson(v);
	const Vec1 estimateSqr = estimate * estimate;

	// NEON has an intrinsic that does one step of Newton-Raphson
	const Vec1 step = vrsqrtss_f32(estimateSqr, v);
	return estimate * step;
}

FORCEINLINE Vec vecRecipSqrt(Vec v)
{
	// Do two iterations of Newton-Raphson
	const Vec estimate = vecRecipSqrtEst(v);
	const Vec estimateSqr = vecMul(estimate, estimate);

	// NEON has an intrinsic that does one step of Newton-Raphson
	const Vec step = vrsqrtsq_f32(estimateSqr, v);
	return vecMul(estimate, step);
}

FORCEINLINE Vec1 vecRecipSqrt(Vec1 v)
{
	// Do two iterations of Newton-Raphson
	const Vec1 estimate = vecRecipSqrtEst(v);
	const Vec1 estimateSqr = estimate * estimate;

	// NEON has an intrinsic that does one step of Newton-Raphson
	const Vec1 step = vrsqrtss_f32(estimateSqr, v);
	return estimate * step;
}

FORCEINLINE Vec1 vecSqrt(Vec1 v1)
{
    alignas(16) float f2[2] = {v1, 0};
	return vget_lane_f32(vsqrt_f32(vld1_f32(f2)), 0);
}

FORCEINLINE Vec vecSqrt(Vec v1)
{
	return vsqrtq_f32(v1);
}

FORCEINLINE Vec vecDivEst(Vec v1, Vec v2)
{
	const float32x4_t reciprocal = vrecpeq_f32(v2);
	return vmulq_f32(v1, reciprocal);
}

FORCEINLINE Vec vecMaskSel(Vec falseValue, Vec trueValue, Vec mostSignificantBitMask)
{
	return vbslq_f32((uint32x4_t)vshrq_n_s32(vreinterpretq_s32_f32(mostSignificantBitMask), 31), trueValue, falseValue);
}

FORCEINLINE IntVec vecMaskSel(IntVec falseValue, IntVec trueValue, IntVec mostSignificantBitMask)
{
	return vbslq_s32((uint32x4_t)vshrq_n_s32(mostSignificantBitMask, 31), trueValue, falseValue);
}

FORCEINLINE Vec vecBitSel(Vec falseValue, Vec trueValue, Vec mask)
{
	return vbslq_f32(vreinterpretq_u32_f32(mask), trueValue, falseValue);
}

FORCEINLINE IntVec vecBitSel(IntVec falseValue, IntVec trueValue, IntVec mask)
{
	return vbslq_s32((uint32x4_t)mask, trueValue, falseValue);
}

// works with all-bits on Neon and MSB on SSE
FORCEINLINE Vec vecSel(Vec falseValue, Vec trueValue, Vec mask)
{
	return vecBitSel(falseValue, trueValue, mask);
}

FORCEINLINE Vec1 vecSel(Vec1 falseValue, Vec1 trueValue, Vec1 mask)
{
	const uint32_t falseValueAsUint = *(uint32_t*)(&falseValue);
	const uint32_t trueValueAsUint  = *(uint32_t*)(&trueValue);
	const uint32_t maskAsUint		= *(uint32_t*)(&mask);
	const uint32_t result 			=  (maskAsUint & trueValueAsUint) | ((~maskAsUint) & falseValueAsUint);

	return *(Vec1*)(&result);
}

// works with all-bits on Neon and MSB on SSE
FORCEINLINE IntVec vecSel(IntVec falseValue, IntVec trueValue, IntVec mask)
{
	return vecBitSel(falseValue, trueValue, mask);
}

FORCEINLINE Vec vecAnd(Vec r1, Vec r2)
{
	const uint32x4_t r1AsUint = vreinterpretq_u32_f32(r1);
	const uint32x4_t r2AsUint = vreinterpretq_u32_f32(r2);
	uint32x4_t result = vandq_u32(r1AsUint, r2AsUint);

	return vreinterpretq_f32_u32(result);
}

FORCEINLINE Vec vecAndNot(Vec mask, Vec v)
{
	const uint32x4_t vAsUint = vreinterpretq_u32_f32(v);
	const uint32x4_t maskAsUint = vreinterpretq_u32_f32(mask);
	const uint32x4_t result = vandq_s32(vmvnq_s32(maskAsUint), vAsUint);

	return vreinterpretq_f32_u32(result);
}

FORCEINLINE Vec vecOr(Vec r1, Vec r2)
{
	const uint32x4_t r1AsUint = vreinterpretq_u32_f32(r1);
	const uint32x4_t r2AsUint = vreinterpretq_u32_f32(r2);
	const uint32x4_t result = vorrq_u32(r1AsUint, r2AsUint);

	return vreinterpretq_f32_u32(result);
}

FORCEINLINE Vec vecXor(Vec r1, Vec r2)
{
	const uint32x4_t r1AsUint = vreinterpretq_u32_f32(r1);
	const uint32x4_t r2AsUint = vreinterpretq_u32_f32(r2);
	const uint32x4_t result = veorq_u32(r1AsUint, r2AsUint);

	return vreinterpretq_f32_u32(result);
}

FORCEINLINE Vec vecNot(Vec v)
{
	return vreinterpretq_f32_u32(vmvnq_u32(vreinterpretq_u32_f32(v)));
}

FORCEINLINE IntVec vecAnd(IntVec r1, IntVec r2)
{
	return vandq_s32(r1, r2);
}

FORCEINLINE IntVec vecAndNot(IntVec mask, IntVec v)
{
	return vandq_s32(vmvnq_s32(mask), v);
}

FORCEINLINE IntVec vecOr(IntVec r1, IntVec r2)
{
	return vorrq_s32(r1, r2);
}

FORCEINLINE IntVec vecXor(IntVec r1, IntVec r2)
{
	return veorq_s32(r1, r2);
}

FORCEINLINE IntVec vecNot(IntVec v)
{
	return vmvnq_s32(v);
}

FORCEINLINE Vec vecCmpGT(Vec r1, Vec r2)
{
	const uint32x4_t compareResult = vcgtq_f32(r1, r2);
	return vreinterpretq_f32_u32(compareResult);
}

FORCEINLINE Vec vecCmpGE(Vec r1, Vec r2)
{
	const uint32x4_t compareResult = vcgeq_f32(r1, r2);
	return vreinterpretq_f32_u32(compareResult);
}

FORCEINLINE Vec vecCmpLT(Vec r1, Vec r2)
{
	const uint32x4_t compareResult = vcltq_f32(r1, r2);
	return vreinterpretq_f32_u32(compareResult);
}

FORCEINLINE Vec vecCmpLE(Vec r1, Vec r2)
{
	const uint32x4_t compareResult = vcleq_f32(r1, r2);
	return vreinterpretq_f32_u32(compareResult);
}

FORCEINLINE Vec vecCmpEQ(Vec r1, Vec r2)
{
	const uint32x4_t compareResult = vceqq_f32(r1, r2);
	return vreinterpretq_f32_u32(compareResult);
}

FORCEINLINE Vec vecCmpNE(Vec r1, Vec r2)
{
	const uint32x4_t compareResult = vmvnq_u32(vceqq_f32(r1, r2));
	return vreinterpretq_f32_u32(compareResult);
}

FORCEINLINE IntVec vecCmpGT(IntVec r1, IntVec r2)
{
	return (IntVec)vcgtq_s32(r1, r2);
}

FORCEINLINE IntVec vecCmpLT(IntVec r1, IntVec r2)
{
	return (IntVec)vcltq_s32(r1, r2);
}

FORCEINLINE IntVec vecCmpEQ(IntVec r1, IntVec r2)
{
	return (IntVec)vceqq_s32(r1, r2);
}

FORCEINLINE Vec vecAbs(Vec v)
{
	return vabsq_f32(v);
}

FORCEINLINE IntVec vecAbs(IntVec v)
{
	return vabsq_s32(v);
}

FORCEINLINE Vec1 vecDot2(Vec u, Vec v)
{
	const float32x4_t product = vmulq_f32(u, v);
	const float dot = vgetq_lane_f32(product, 0) + vgetq_lane_f32(product, 1);
	return dot;
}

FORCEINLINE Vec1 vecDot3(Vec u, Vec v)
{
	const float32x4_t product = vmulq_f32(u, v);
	const float dotResult = vgetq_lane_f32(product, 0) + vgetq_lane_f32(product, 1) + vgetq_lane_f32(product, 2);
	return dotResult;
}

FORCEINLINE Vec1 vecDot4(Vec u, Vec v)
{
	const float32x4_t product = vmulq_f32(u, v);
	const float32x4_t sum1 = vaddq_f32(product, vrev64q_f32(product));
	return vgetq_lane_f32(product, 0) + vgetq_lane_f32(product, 1);
}

FORCEINLINE Vec vecCross(Vec l, Vec r)
{
	return vec(
		vgetq_lane_f32(l, 1) * vgetq_lane_f32(r, 2) - vgetq_lane_f32(r, 1) * vgetq_lane_f32(l, 2),
		vgetq_lane_f32(l, 2) * vgetq_lane_f32(r, 0) - vgetq_lane_f32(r, 2) * vgetq_lane_f32(l, 0),
		vgetq_lane_f32(l, 0) * vgetq_lane_f32(r, 1) - vgetq_lane_f32(r, 0) * vgetq_lane_f32(l, 1));
}

FORCEINLINE Vec vecFloor(Vec r)
{
	// find truncated value and one less.
	Vec inrange = vecCmpGT(vreinterpretq_f32_s32(intVec(0x4b000000)), vecAbs(r));

	IntVec xi = vcvtq_s32_f32(r);
	IntVec xi1 = vsubq_s32(xi, intVec(1));

	Vec truncated  = vecSel(r, vcvtq_f32_s32(xi), inrange);
	Vec truncated1 = vecSel(r, vcvtq_f32_s32(xi1), inrange);

	// if truncated value is greater than input, subtract one.
	return vecSel(truncated, truncated1, vecCmpGT(truncated, r));
}

FORCEINLINE Vec vecCeil(Vec r)
{
	// find truncated value and one less.
	Vec inrange = vecCmpGT(vreinterpretq_f32_s32(intVec(0x4b000000)), vecAbs(r));

    IntVec xi = vcvtq_s32_f32(r);
    IntVec xi1 = vaddq_s32(xi, intVec(1));

	Vec truncated  = vecSel(r, vcvtq_f32_s32(xi), inrange);
	Vec truncated1 = vecSel(r, vcvtq_f32_s32(xi1), inrange);

	// if truncated value is greater than input, subtract one.
	return vecSel(truncated, truncated1, vecCmpGT(r, truncated));
}

FORCEINLINE Vec vecFraction(Vec r)
{
	const Vec returnValue = vecFloor(r);
	return vsubq_f32(r, returnValue);
}

FORCEINLINE Vec vecRound(Vec v)
{
	return vcvtq_f32_s32(vcvtnq_s32_f32(v));
}

FORCEINLINE IntVec vecRoundToInt(Vec v)
{
	return vcvtnq_s32_f32(v);
}

FORCEINLINE Vec vecTrunc(Vec v)
{
	return vcvtq_f32_s32(vcvtq_s32_f32(v));
}

FORCEINLINE IntVec vecTruncToInt(Vec v)
{
	return vcvtq_s32_f32(v);
}

FORCEINLINE Vec intVecToVec(IntVec xyzw)
{
	return vcvtq_f32_s32(xyzw);
}

FORCEINLINE Vec intVecCastToVec(IntVec xyzw)
{
	return vreinterpretq_f32_s32(xyzw);
}

FORCEINLINE IntVec vecCastToIntVec(Vec xyzw)
{
	return vreinterpretq_s32_f32(xyzw);
}

struct VecMask
{
	#define VEC_C_MASK(x,y,z,w)	((((x)&3) << 6) | (((y)&3) << 4) | (((z)&3) << 2) | ((w)&3))

	enum Mask
	{
		_xxxx = (VEC_C_MASK(0, 0, 0, 0) << 8) | 0,
		_yxxx = (VEC_C_MASK(1, 0, 0, 0) << 8) | 0,
		_zxxx = (VEC_C_MASK(2, 0, 0, 0) << 8) | 0,
		_wxxx = (VEC_C_MASK(3, 0, 0, 0) << 8) | 0,
		_xyxx = (VEC_C_MASK(0, 1, 0, 0) << 8) | 0,
		_yyxx = (VEC_C_MASK(1, 1, 0, 0) << 8) | 0,
		_zyxx = (VEC_C_MASK(2, 1, 0, 0) << 8) | 0,
		_wyxx = (VEC_C_MASK(3, 1, 0, 0) << 8) | 0,
		_xzxx = (VEC_C_MASK(0, 2, 0, 0) << 8) | 0,
		_yzxx = (VEC_C_MASK(1, 2, 0, 0) << 8) | 0,
		_zzxx = (VEC_C_MASK(2, 2, 0, 0) << 8) | 0,
		_wzxx = (VEC_C_MASK(3, 2, 0, 0) << 8) | 0,
		_xwxx = (VEC_C_MASK(0, 3, 0, 0) << 8) | 0,
		_ywxx = (VEC_C_MASK(1, 3, 0, 0) << 8) | 0,
		_zwxx = (VEC_C_MASK(2, 3, 0, 0) << 8) | 0,
		_wwxx = (VEC_C_MASK(3, 3, 0, 0) << 8) | 0,
		_xxyx = (VEC_C_MASK(0, 0, 1, 0) << 8) | 0,
		_yxyx = (VEC_C_MASK(1, 0, 1, 0) << 8) | 0,
		_zxyx = (VEC_C_MASK(2, 0, 1, 0) << 8) | 0,
		_wxyx = (VEC_C_MASK(3, 0, 1, 0) << 8) | 0,
		_xyyx = (VEC_C_MASK(0, 1, 1, 0) << 8) | 0,
		_yyyx = (VEC_C_MASK(1, 1, 1, 0) << 8) | 0,
		_zyyx = (VEC_C_MASK(2, 1, 1, 0) << 8) | 0,
		_wyyx = (VEC_C_MASK(3, 1, 1, 0) << 8) | 0,
		_xzyx = (VEC_C_MASK(0, 2, 1, 0) << 8) | 0,
		_yzyx = (VEC_C_MASK(1, 2, 1, 0) << 8) | 0,
		_zzyx = (VEC_C_MASK(2, 2, 1, 0) << 8) | 0,
		_wzyx = (VEC_C_MASK(3, 2, 1, 0) << 8) | 0,
		_xwyx = (VEC_C_MASK(0, 3, 1, 0) << 8) | 0,
		_ywyx = (VEC_C_MASK(1, 3, 1, 0) << 8) | 0,
		_zwyx = (VEC_C_MASK(2, 3, 1, 0) << 8) | 0,
		_wwyx = (VEC_C_MASK(3, 3, 1, 0) << 8) | 0,
		_xxzx = (VEC_C_MASK(0, 0, 2, 0) << 8) | 0,
		_yxzx = (VEC_C_MASK(1, 0, 2, 0) << 8) | 0,
		_zxzx = (VEC_C_MASK(2, 0, 2, 0) << 8) | 0,
		_wxzx = (VEC_C_MASK(3, 0, 2, 0) << 8) | 0,
		_xyzx = (VEC_C_MASK(0, 1, 2, 0) << 8) | 0,
		_yyzx = (VEC_C_MASK(1, 1, 2, 0) << 8) | 0,
		_zyzx = (VEC_C_MASK(2, 1, 2, 0) << 8) | 0,
		_wyzx = (VEC_C_MASK(3, 1, 2, 0) << 8) | 0,
		_xzzx = (VEC_C_MASK(0, 2, 2, 0) << 8) | 0,

		_zzzx = (VEC_C_MASK(2, 2, 2, 0) << 8) | 0,
		_wzzx = (VEC_C_MASK(3, 2, 2, 0) << 8) | 0,
		_xwzx = (VEC_C_MASK(0, 3, 2, 0) << 8) | 0,
		_ywzx = (VEC_C_MASK(1, 3, 2, 0) << 8) | 0,
		_zwzx = (VEC_C_MASK(2, 3, 2, 0) << 8) | 0,
		_wwzx = (VEC_C_MASK(3, 3, 2, 0) << 8) | 0,
		_xxwx = (VEC_C_MASK(0, 0, 3, 0) << 8) | 0,
		_yxwx = (VEC_C_MASK(1, 0, 3, 0) << 8) | 0,
		_zxwx = (VEC_C_MASK(2, 0, 3, 0) << 8) | 0,
		_wxwx = (VEC_C_MASK(3, 0, 3, 0) << 8) | 0,
		_xywx = (VEC_C_MASK(0, 1, 3, 0) << 8) | 0,
		_yywx = (VEC_C_MASK(1, 1, 3, 0) << 8) | 0,
		_zywx = (VEC_C_MASK(2, 1, 3, 0) << 8) | 0,
		_wywx = (VEC_C_MASK(3, 1, 3, 0) << 8) | 0,
		_xzwx = (VEC_C_MASK(0, 2, 3, 0) << 8) | 0,
		_yzwx = (VEC_C_MASK(1, 2, 3, 0) << 8) | 0,
		_zzwx = (VEC_C_MASK(2, 2, 3, 0) << 8) | 0,
		_wzwx = (VEC_C_MASK(3, 2, 3, 0) << 8) | 0,
		_xwwx = (VEC_C_MASK(0, 3, 3, 0) << 8) | 0,
		_ywwx = (VEC_C_MASK(1, 3, 3, 0) << 8) | 0,
		_zwwx = (VEC_C_MASK(2, 3, 3, 0) << 8) | 0,
		_wwwx = (VEC_C_MASK(3, 3, 3, 0) << 8) | 0,
		_xxxy = (VEC_C_MASK(0, 0, 0, 1) << 8) | 0,
		_yxxy = (VEC_C_MASK(1, 0, 0, 1) << 8) | 0,
		_zxxy = (VEC_C_MASK(2, 0, 0, 1) << 8) | 0,
		_wxxy = (VEC_C_MASK(3, 0, 0, 1) << 8) | 0,
		_xyxy = (VEC_C_MASK(0, 1, 0, 1) << 8) | 0,
		_yyxy = (VEC_C_MASK(1, 1, 0, 1) << 8) | 0,
		_zyxy = (VEC_C_MASK(2, 1, 0, 1) << 8) | 0,
		_wyxy = (VEC_C_MASK(3, 1, 0, 1) << 8) | 0,
		_xzxy = (VEC_C_MASK(0, 2, 0, 1) << 8) | 0,
		_yzxy = (VEC_C_MASK(1, 2, 0, 1) << 8) | 0,
		_zzxy = (VEC_C_MASK(2, 2, 0, 1) << 8) | 0,
		_wzxy = (VEC_C_MASK(3, 2, 0, 1) << 8) | 0,
		_xwxy = (VEC_C_MASK(0, 3, 0, 1) << 8) | 0,
		_ywxy = (VEC_C_MASK(1, 3, 0, 1) << 8) | 0,
		_zwxy = (VEC_C_MASK(2, 3, 0, 1) << 8) | 0,
		_wwxy = (VEC_C_MASK(3, 3, 0, 1) << 8) | 0,
		_xxyy = (VEC_C_MASK(0, 0, 1, 1) << 8) | 0,
		_yxyy = (VEC_C_MASK(1, 0, 1, 1) << 8) | 0,
		_zxyy = (VEC_C_MASK(2, 0, 1, 1) << 8) | 0,
		_wxyy = (VEC_C_MASK(3, 0, 1, 1) << 8) | 0,
		_xyyy = (VEC_C_MASK(0, 1, 1, 1) << 8) | 0,
		_yyyy = (VEC_C_MASK(1, 1, 1, 1) << 8) | 0,
		_zyyy = (VEC_C_MASK(2, 1, 1, 1) << 8) | 0,
		_wyyy = (VEC_C_MASK(3, 1, 1, 1) << 8) | 0,
		_xzyy = (VEC_C_MASK(0, 2, 1, 1) << 8) | 0,
		_yzyy = (VEC_C_MASK(1, 2, 1, 1) << 8) | 0,
		_zzyy = (VEC_C_MASK(2, 2, 1, 1) << 8) | 0,
		_wzyy = (VEC_C_MASK(3, 2, 1, 1) << 8) | 0,
		_xwyy = (VEC_C_MASK(0, 3, 1, 1) << 8) | 0,
		_ywyy = (VEC_C_MASK(1, 3, 1, 1) << 8) | 0,
		_zwyy = (VEC_C_MASK(2, 3, 1, 1) << 8) | 0,
		_wwyy = (VEC_C_MASK(3, 3, 1, 1) << 8) | 0,
		_xxzy = (VEC_C_MASK(0, 0, 2, 1) << 8) | 0,
		_yxzy = (VEC_C_MASK(1, 0, 2, 1) << 8) | 0,
		_zxzy = (VEC_C_MASK(2, 0, 2, 1) << 8) | 0,
		_wxzy = (VEC_C_MASK(3, 0, 2, 1) << 8) | 0,
		_xyzy = (VEC_C_MASK(0, 1, 2, 1) << 8) | 0,
		_yyzy = (VEC_C_MASK(1, 1, 2, 1) << 8) | 0,
		_zyzy = (VEC_C_MASK(2, 1, 2, 1) << 8) | 0,
		_wyzy = (VEC_C_MASK(3, 1, 2, 1) << 8) | 0,
		_xzzy = (VEC_C_MASK(0, 2, 2, 1) << 8) | 0,
		_yzzy = (VEC_C_MASK(1, 2, 2, 1) << 8) | 0,
		_zzzy = (VEC_C_MASK(2, 2, 2, 1) << 8) | 0,
		_wzzy = (VEC_C_MASK(3, 2, 2, 1) << 8) | 0,
		_xwzy = (VEC_C_MASK(0, 3, 2, 1) << 8) | 0,
		_ywzy = (VEC_C_MASK(1, 3, 2, 1) << 8) | 0,
		_zwzy = (VEC_C_MASK(2, 3, 2, 1) << 8) | 0,
		_wwzy = (VEC_C_MASK(3, 3, 2, 1) << 8) | 0,
		_xxwy = (VEC_C_MASK(0, 0, 3, 1) << 8) | 0,
		_yxwy = (VEC_C_MASK(1, 0, 3, 1) << 8) | 0,
		_zxwy = (VEC_C_MASK(2, 0, 3, 1) << 8) | 0,
		_wxwy = (VEC_C_MASK(3, 0, 3, 1) << 8) | 0,
		_xywy = (VEC_C_MASK(0, 1, 3, 1) << 8) | 0,
		_yywy = (VEC_C_MASK(1, 1, 3, 1) << 8) | 0,
		_zywy = (VEC_C_MASK(2, 1, 3, 1) << 8) | 0,
		_wywy = (VEC_C_MASK(3, 1, 3, 1) << 8) | 0,
		_xzwy = (VEC_C_MASK(0, 2, 3, 1) << 8) | 0,
		_yzwy = (VEC_C_MASK(1, 2, 3, 1) << 8) | 0,
		_zzwy = (VEC_C_MASK(2, 2, 3, 1) << 8) | 0,
		_wzwy = (VEC_C_MASK(3, 2, 3, 1) << 8) | 0,
		_xwwy = (VEC_C_MASK(0, 3, 3, 1) << 8) | 0,
		_ywwy = (VEC_C_MASK(1, 3, 3, 1) << 8) | 0,
		_zwwy = (VEC_C_MASK(2, 3, 3, 1) << 8) | 0,
		_wwwy = (VEC_C_MASK(3, 3, 3, 1) << 8) | 0,
		_xxxz = (VEC_C_MASK(0, 0, 0, 2) << 8) | 0,
		_yxxz = (VEC_C_MASK(1, 0, 0, 2) << 8) | 0,
		_zxxz = (VEC_C_MASK(2, 0, 0, 2) << 8) | 0,
		_wxxz = (VEC_C_MASK(3, 0, 0, 2) << 8) | 0,
		_xyxz = (VEC_C_MASK(0, 1, 0, 2) << 8) | 0,
		_yyxz = (VEC_C_MASK(1, 1, 0, 2) << 8) | 0,
		_zyxz = (VEC_C_MASK(2, 1, 0, 2) << 8) | 0,
		_wyxz = (VEC_C_MASK(3, 1, 0, 2) << 8) | 0,
		_xzxz = (VEC_C_MASK(0, 2, 0, 2) << 8) | 0,
		_yzxz = (VEC_C_MASK(1, 2, 0, 2) << 8) | 0,
		_zzxz = (VEC_C_MASK(2, 2, 0, 2) << 8) | 0,
		_wzxz = (VEC_C_MASK(3, 2, 0, 2) << 8) | 0,
		_xwxz = (VEC_C_MASK(0, 3, 0, 2) << 8) | 0,
		_ywxz = (VEC_C_MASK(1, 3, 0, 2) << 8) | 0,
		_zwxz = (VEC_C_MASK(2, 3, 0, 2) << 8) | 0,
		_wwxz = (VEC_C_MASK(3, 3, 0, 2) << 8) | 0,
		_xxyz = (VEC_C_MASK(0, 0, 1, 2) << 8) | 0,
		_yxyz = (VEC_C_MASK(1, 0, 1, 2) << 8) | 0,
		_zxyz = (VEC_C_MASK(2, 0, 1, 2) << 8) | 0,
		_wxyz = (VEC_C_MASK(3, 0, 1, 2) << 8) | 0,
		_xyyz = (VEC_C_MASK(0, 1, 1, 2) << 8) | 0,
		_yyyz = (VEC_C_MASK(1, 1, 1, 2) << 8) | 0,
		_zyyz = (VEC_C_MASK(2, 1, 1, 2) << 8) | 0,
		_wyyz = (VEC_C_MASK(3, 1, 1, 2) << 8) | 0,
		_xzyz = (VEC_C_MASK(0, 2, 1, 2) << 8) | 0,
		_yzyz = (VEC_C_MASK(1, 2, 1, 2) << 8) | 0,
		_zzyz = (VEC_C_MASK(2, 2, 1, 2) << 8) | 0,
		_wzyz = (VEC_C_MASK(3, 2, 1, 2) << 8) | 0,
		_xwyz = (VEC_C_MASK(0, 3, 1, 2) << 8) | 0,
		_ywyz = (VEC_C_MASK(1, 3, 1, 2) << 8) | 0,
		_zwyz = (VEC_C_MASK(2, 3, 1, 2) << 8) | 0,
		_wwyz = (VEC_C_MASK(3, 3, 1, 2) << 8) | 0,
		_xxzz = (VEC_C_MASK(0, 0, 2, 2) << 8) | 0,
		_yxzz = (VEC_C_MASK(1, 0, 2, 2) << 8) | 0,
		_zxzz = (VEC_C_MASK(2, 0, 2, 2) << 8) | 0,
		_wxzz = (VEC_C_MASK(3, 0, 2, 2) << 8) | 0,
		_xyzz = (VEC_C_MASK(0, 1, 2, 2) << 8) | 0,
		_yyzz = (VEC_C_MASK(1, 1, 2, 2) << 8) | 0,
		_zyzz = (VEC_C_MASK(2, 1, 2, 2) << 8) | 0,
		_wyzz = (VEC_C_MASK(3, 1, 2, 2) << 8) | 0,
		_xzzz = (VEC_C_MASK(0, 2, 2, 2) << 8) | 0,
		_yzzz = (VEC_C_MASK(1, 2, 2, 2) << 8) | 0,
		_zzzz = (VEC_C_MASK(2, 2, 2, 2) << 8) | 0,
		_wzzz = (VEC_C_MASK(3, 2, 2, 2) << 8) | 0,
		_xwzz = (VEC_C_MASK(0, 3, 2, 2) << 8) | 0,
		_ywzz = (VEC_C_MASK(1, 3, 2, 2) << 8) | 0,
		_zwzz = (VEC_C_MASK(2, 3, 2, 2) << 8) | 0,
		_wwzz = (VEC_C_MASK(3, 3, 2, 2) << 8) | 0,
		_xxwz = (VEC_C_MASK(0, 0, 3, 2) << 8) | 0,
		_yxwz = (VEC_C_MASK(1, 0, 3, 2) << 8) | 0,
		_zxwz = (VEC_C_MASK(2, 0, 3, 2) << 8) | 0,
		_wxwz = (VEC_C_MASK(3, 0, 3, 2) << 8) | 0,
		_xywz = (VEC_C_MASK(0, 1, 3, 2) << 8) | 0,
		_yywz = (VEC_C_MASK(1, 1, 3, 2) << 8) | 0,
		_zywz = (VEC_C_MASK(2, 1, 3, 2) << 8) | 0,
		_wywz = (VEC_C_MASK(3, 1, 3, 2) << 8) | 0,
		_xzwz = (VEC_C_MASK(0, 2, 3, 2) << 8) | 0,
		_yzwz = (VEC_C_MASK(1, 2, 3, 2) << 8) | 0,
		_zzwz = (VEC_C_MASK(2, 2, 3, 2) << 8) | 0,
		_wzwz = (VEC_C_MASK(3, 2, 3, 2) << 8) | 0,
		_xwwz = (VEC_C_MASK(0, 3, 3, 2) << 8) | 0,
		_ywwz = (VEC_C_MASK(1, 3, 3, 2) << 8) | 0,
		_zwwz = (VEC_C_MASK(2, 3, 3, 2) << 8) | 0,
		_wwwz = (VEC_C_MASK(3, 3, 3, 2) << 8) | 0,
		_xxxw = (VEC_C_MASK(0, 0, 0, 3) << 8) | 0,
		_yxxw = (VEC_C_MASK(1, 0, 0, 3) << 8) | 0,
		_zxxw = (VEC_C_MASK(2, 0, 0, 3) << 8) | 0,
		_wxxw = (VEC_C_MASK(3, 0, 0, 3) << 8) | 0,
		_xyxw = (VEC_C_MASK(0, 1, 0, 3) << 8) | 0,
		_yyxw = (VEC_C_MASK(1, 1, 0, 3) << 8) | 0,
		_zyxw = (VEC_C_MASK(2, 1, 0, 3) << 8) | 0,
		_wyxw = (VEC_C_MASK(3, 1, 0, 3) << 8) | 0,
		_xzxw = (VEC_C_MASK(0, 2, 0, 3) << 8) | 0,
		_yzxw = (VEC_C_MASK(1, 2, 0, 3) << 8) | 0,
		_zzxw = (VEC_C_MASK(2, 2, 0, 3) << 8) | 0,
		_wzxw = (VEC_C_MASK(3, 2, 0, 3) << 8) | 0,
		_xwxw = (VEC_C_MASK(0, 3, 0, 3) << 8) | 0,
		_ywxw = (VEC_C_MASK(1, 3, 0, 3) << 8) | 0,
		_zwxw = (VEC_C_MASK(2, 3, 0, 3) << 8) | 0,
		_wwxw = (VEC_C_MASK(3, 3, 0, 3) << 8) | 0,
		_xxyw = (VEC_C_MASK(0, 0, 1, 3) << 8) | 0,
		_yxyw = (VEC_C_MASK(1, 0, 1, 3) << 8) | 0,
		_zxyw = (VEC_C_MASK(2, 0, 1, 3) << 8) | 0,
		_wxyw = (VEC_C_MASK(3, 0, 1, 3) << 8) | 0,
		_xyyw = (VEC_C_MASK(0, 1, 1, 3) << 8) | 0,
		_yyyw = (VEC_C_MASK(1, 1, 1, 3) << 8) | 0,
		_zyyw = (VEC_C_MASK(2, 1, 1, 3) << 8) | 0,
		_wyyw = (VEC_C_MASK(3, 1, 1, 3) << 8) | 0,
		_xzyw = (VEC_C_MASK(0, 2, 1, 3) << 8) | 0,
		_yzyw = (VEC_C_MASK(1, 2, 1, 3) << 8) | 0,
		_zzyw = (VEC_C_MASK(2, 2, 1, 3) << 8) | 0,
		_wzyw = (VEC_C_MASK(3, 2, 1, 3) << 8) | 0,
		_xwyw = (VEC_C_MASK(0, 3, 1, 3) << 8) | 0,
		_ywyw = (VEC_C_MASK(1, 3, 1, 3) << 8) | 0,
		_zwyw = (VEC_C_MASK(2, 3, 1, 3) << 8) | 0,
		_wwyw = (VEC_C_MASK(3, 3, 1, 3) << 8) | 0,
		_xxzw = (VEC_C_MASK(0, 0, 2, 3) << 8) | 0,
		_yxzw = (VEC_C_MASK(1, 0, 2, 3) << 8) | 0,
		_zxzw = (VEC_C_MASK(2, 0, 2, 3) << 8) | 0,
		_wxzw = (VEC_C_MASK(3, 0, 2, 3) << 8) | 0,
		_xyzw = (VEC_C_MASK(0, 1, 2, 3) << 8) | 0,
		_yyzw = (VEC_C_MASK(1, 1, 2, 3) << 8) | 0,
		_zyzw = (VEC_C_MASK(2, 1, 2, 3) << 8) | 0,
		_wyzw = (VEC_C_MASK(3, 1, 2, 3) << 8) | 0,
		_xzzw = (VEC_C_MASK(0, 2, 2, 3) << 8) | 0,
		_yzzw = (VEC_C_MASK(1, 2, 2, 3) << 8) | 0,
		_zzzw = (VEC_C_MASK(2, 2, 2, 3) << 8) | 0,
		_wzzw = (VEC_C_MASK(3, 2, 2, 3) << 8) | 0,
		_xwzw = (VEC_C_MASK(0, 3, 2, 3) << 8) | 0,
		_ywzw = (VEC_C_MASK(1, 3, 2, 3) << 8) | 0,
		_zwzw = (VEC_C_MASK(2, 3, 2, 3) << 8) | 0,
		_wwzw = (VEC_C_MASK(3, 3, 2, 3) << 8) | 0,
		_xxww = (VEC_C_MASK(0, 0, 3, 3) << 8) | 0,
		_yxww = (VEC_C_MASK(1, 0, 3, 3) << 8) | 0,
		_zxww = (VEC_C_MASK(2, 0, 3, 3) << 8) | 0,
		_wxww = (VEC_C_MASK(3, 0, 3, 3) << 8) | 0,
		_xyww = (VEC_C_MASK(0, 1, 3, 3) << 8) | 0,
		_yyww = (VEC_C_MASK(1, 1, 3, 3) << 8) | 0,
		_zyww = (VEC_C_MASK(2, 1, 3, 3) << 8) | 0,
		_wyww = (VEC_C_MASK(3, 1, 3, 3) << 8) | 0,
		_xzww = (VEC_C_MASK(0, 2, 3, 3) << 8) | 0,
		_yzww = (VEC_C_MASK(1, 2, 3, 3) << 8) | 0,
		_zzww = (VEC_C_MASK(2, 2, 3, 3) << 8) | 0,
		_wzww = (VEC_C_MASK(3, 2, 3, 3) << 8) | 0,
		_xwww = (VEC_C_MASK(0, 3, 3, 3) << 8) | 0,
		_ywww = (VEC_C_MASK(1, 3, 3, 3) << 8) | 0,
		_zwww = (VEC_C_MASK(2, 3, 3, 3) << 8) | 0,
		_wwww = (VEC_C_MASK(3, 3, 3, 3) << 8) | 0,

		_0xxx = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x8 << 3) | 1,
		_0yxx = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0x8 << 3) | 1,
		_0zxx = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0x8 << 3) | 1,
		_0wxx = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0x8 << 3) | 1,
		_x0xx = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x4 << 3) | 1,
		_y0xx = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x4 << 3) | 1,
		_z0xx = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x4 << 3) | 1,
		_w0xx = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x4 << 3) | 1,
		_00xx = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0xc << 3) | 1,
		_0xyx = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0x8 << 3) | 1,
		_0yyx = ((VEC_C_MASK(0, 1, 1, 0) << 8) | 0x8 << 3) | 1,
		_0zyx = ((VEC_C_MASK(0, 2, 1, 0) << 8) | 0x8 << 3) | 1,
		_0wyx = ((VEC_C_MASK(0, 3, 1, 0) << 8) | 0x8 << 3) | 1,
		_x0yx = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0x4 << 3) | 1,
		_y0yx = ((VEC_C_MASK(1, 0, 1, 0) << 8) | 0x4 << 3) | 1,
		_z0yx = ((VEC_C_MASK(2, 0, 1, 0) << 8) | 0x4 << 3) | 1,
		_w0yx = ((VEC_C_MASK(3, 0, 1, 0) << 8) | 0x4 << 3) | 1,
		_00yx = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0xc << 3) | 1,
		_0xzx = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0x8 << 3) | 1,
		_0yzx = ((VEC_C_MASK(0, 1, 2, 0) << 8) | 0x8 << 3) | 1,
		_0zzx = ((VEC_C_MASK(0, 2, 2, 0) << 8) | 0x8 << 3) | 1,
		_0wzx = ((VEC_C_MASK(0, 3, 2, 0) << 8) | 0x8 << 3) | 1,
		_x0zx = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0x4 << 3) | 1,
		_y0zx = ((VEC_C_MASK(1, 0, 2, 0) << 8) | 0x4 << 3) | 1,
		_z0zx = ((VEC_C_MASK(2, 0, 2, 0) << 8) | 0x4 << 3) | 1,
		_w0zx = ((VEC_C_MASK(3, 0, 2, 0) << 8) | 0x4 << 3) | 1,
		_00zx = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0xc << 3) | 1,
		_0xwx = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0x8 << 3) | 1,
		_0ywx = ((VEC_C_MASK(0, 1, 3, 0) << 8) | 0x8 << 3) | 1,
		_0zwx = ((VEC_C_MASK(0, 2, 3, 0) << 8) | 0x8 << 3) | 1,
		_0wwx = ((VEC_C_MASK(0, 3, 3, 0) << 8) | 0x8 << 3) | 1,
		_x0wx = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0x4 << 3) | 1,
		_y0wx = ((VEC_C_MASK(1, 0, 3, 0) << 8) | 0x4 << 3) | 1,
		_z0wx = ((VEC_C_MASK(2, 0, 3, 0) << 8) | 0x4 << 3) | 1,
		_w0wx = ((VEC_C_MASK(3, 0, 3, 0) << 8) | 0x4 << 3) | 1,
		_00wx = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0xc << 3) | 1,
		_xx0x = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x2 << 3) | 1,
		_yx0x = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x2 << 3) | 1,
		_zx0x = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x2 << 3) | 1,
		_wx0x = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x2 << 3) | 1,
		_0x0x = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0xa << 3) | 1,
		_xy0x = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0x2 << 3) | 1,
		_yy0x = ((VEC_C_MASK(1, 1, 0, 0) << 8) | 0x2 << 3) | 1,
		_zy0x = ((VEC_C_MASK(2, 1, 0, 0) << 8) | 0x2 << 3) | 1,
		_wy0x = ((VEC_C_MASK(3, 1, 0, 0) << 8) | 0x2 << 3) | 1,
		_0y0x = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0xa << 3) | 1,
		_xz0x = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0x2 << 3) | 1,
		_yz0x = ((VEC_C_MASK(1, 2, 0, 0) << 8) | 0x2 << 3) | 1,
		_zz0x = ((VEC_C_MASK(2, 2, 0, 0) << 8) | 0x2 << 3) | 1,
		_wz0x = ((VEC_C_MASK(3, 2, 0, 0) << 8) | 0x2 << 3) | 1,
		_0z0x = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0xa << 3) | 1,
		_xw0x = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0x2 << 3) | 1,
		_yw0x = ((VEC_C_MASK(1, 3, 0, 0) << 8) | 0x2 << 3) | 1,
		_zw0x = ((VEC_C_MASK(2, 3, 0, 0) << 8) | 0x2 << 3) | 1,
		_ww0x = ((VEC_C_MASK(3, 3, 0, 0) << 8) | 0x2 << 3) | 1,
		_0w0x = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0xa << 3) | 1,
		_x00x = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x6 << 3) | 1,
		_y00x = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x6 << 3) | 1,
		_z00x = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x6 << 3) | 1,
		_w00x = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x6 << 3) | 1,
		_000x = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0xe << 3) | 1,
		_0xxy = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0x8 << 3) | 1,
		_0yxy = ((VEC_C_MASK(0, 1, 0, 1) << 8) | 0x8 << 3) | 1,
		_0zxy = ((VEC_C_MASK(0, 2, 0, 1) << 8) | 0x8 << 3) | 1,
		_0wxy = ((VEC_C_MASK(0, 3, 0, 1) << 8) | 0x8 << 3) | 1,
		_x0xy = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0x4 << 3) | 1,
		_y0xy = ((VEC_C_MASK(1, 0, 0, 1) << 8) | 0x4 << 3) | 1,
		_z0xy = ((VEC_C_MASK(2, 0, 0, 1) << 8) | 0x4 << 3) | 1,
		_w0xy = ((VEC_C_MASK(3, 0, 0, 1) << 8) | 0x4 << 3) | 1,
		_00xy = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0xc << 3) | 1,
		_0xyy = ((VEC_C_MASK(0, 0, 1, 1) << 8) | 0x8 << 3) | 1,
		_0yyy = ((VEC_C_MASK(0, 1, 1, 1) << 8) | 0x8 << 3) | 1,
		_0zyy = ((VEC_C_MASK(0, 2, 1, 1) << 8) | 0x8 << 3) | 1,
		_0wyy = ((VEC_C_MASK(0, 3, 1, 1) << 8) | 0x8 << 3) | 1,
		_x0yy = ((VEC_C_MASK(0, 0, 1, 1) << 8) | 0x4 << 3) | 1,
		_y0yy = ((VEC_C_MASK(1, 0, 1, 1) << 8) | 0x4 << 3) | 1,
		_z0yy = ((VEC_C_MASK(2, 0, 1, 1) << 8) | 0x4 << 3) | 1,
		_w0yy = ((VEC_C_MASK(3, 0, 1, 1) << 8) | 0x4 << 3) | 1,
		_00yy = ((VEC_C_MASK(0, 0, 1, 1) << 8) | 0xc << 3) | 1,
		_0xzy = ((VEC_C_MASK(0, 0, 2, 1) << 8) | 0x8 << 3) | 1,
		_0yzy = ((VEC_C_MASK(0, 1, 2, 1) << 8) | 0x8 << 3) | 1,
		_0zzy = ((VEC_C_MASK(0, 2, 2, 1) << 8) | 0x8 << 3) | 1,
		_0wzy = ((VEC_C_MASK(0, 3, 2, 1) << 8) | 0x8 << 3) | 1,
		_x0zy = ((VEC_C_MASK(0, 0, 2, 1) << 8) | 0x4 << 3) | 1,
		_y0zy = ((VEC_C_MASK(1, 0, 2, 1) << 8) | 0x4 << 3) | 1,
		_z0zy = ((VEC_C_MASK(2, 0, 2, 1) << 8) | 0x4 << 3) | 1,
		_w0zy = ((VEC_C_MASK(3, 0, 2, 1) << 8) | 0x4 << 3) | 1,
		_00zy = ((VEC_C_MASK(0, 0, 2, 1) << 8) | 0xc << 3) | 1,
		_0xwy = ((VEC_C_MASK(0, 0, 3, 1) << 8) | 0x8 << 3) | 1,
		_0ywy = ((VEC_C_MASK(0, 1, 3, 1) << 8) | 0x8 << 3) | 1,
		_0zwy = ((VEC_C_MASK(0, 2, 3, 1) << 8) | 0x8 << 3) | 1,
		_0wwy = ((VEC_C_MASK(0, 3, 3, 1) << 8) | 0x8 << 3) | 1,
		_x0wy = ((VEC_C_MASK(0, 0, 3, 1) << 8) | 0x4 << 3) | 1,
		_y0wy = ((VEC_C_MASK(1, 0, 3, 1) << 8) | 0x4 << 3) | 1,
		_z0wy = ((VEC_C_MASK(2, 0, 3, 1) << 8) | 0x4 << 3) | 1,
		_w0wy = ((VEC_C_MASK(3, 0, 3, 1) << 8) | 0x4 << 3) | 1,
		_00wy = ((VEC_C_MASK(0, 0, 3, 1) << 8) | 0xc << 3) | 1,
		_xx0y = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0x2 << 3) | 1,
		_yx0y = ((VEC_C_MASK(1, 0, 0, 1) << 8) | 0x2 << 3) | 1,
		_zx0y = ((VEC_C_MASK(2, 0, 0, 1) << 8) | 0x2 << 3) | 1,
		_wx0y = ((VEC_C_MASK(3, 0, 0, 1) << 8) | 0x2 << 3) | 1,
		_0x0y = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0xa << 3) | 1,
		_xy0y = ((VEC_C_MASK(0, 1, 0, 1) << 8) | 0x2 << 3) | 1,
		_yy0y = ((VEC_C_MASK(1, 1, 0, 1) << 8) | 0x2 << 3) | 1,
		_zy0y = ((VEC_C_MASK(2, 1, 0, 1) << 8) | 0x2 << 3) | 1,
		_wy0y = ((VEC_C_MASK(3, 1, 0, 1) << 8) | 0x2 << 3) | 1,
		_0y0y = ((VEC_C_MASK(0, 1, 0, 1) << 8) | 0xa << 3) | 1,
		_xz0y = ((VEC_C_MASK(0, 2, 0, 1) << 8) | 0x2 << 3) | 1,
		_yz0y = ((VEC_C_MASK(1, 2, 0, 1) << 8) | 0x2 << 3) | 1,
		_zz0y = ((VEC_C_MASK(2, 2, 0, 1) << 8) | 0x2 << 3) | 1,
		_wz0y = ((VEC_C_MASK(3, 2, 0, 1) << 8) | 0x2 << 3) | 1,
		_0z0y = ((VEC_C_MASK(0, 2, 0, 1) << 8) | 0xa << 3) | 1,
		_xw0y = ((VEC_C_MASK(0, 3, 0, 1) << 8) | 0x2 << 3) | 1,
		_yw0y = ((VEC_C_MASK(1, 3, 0, 1) << 8) | 0x2 << 3) | 1,
		_zw0y = ((VEC_C_MASK(2, 3, 0, 1) << 8) | 0x2 << 3) | 1,
		_ww0y = ((VEC_C_MASK(3, 3, 0, 1) << 8) | 0x2 << 3) | 1,
		_0w0y = ((VEC_C_MASK(0, 3, 0, 1) << 8) | 0xa << 3) | 1,
		_x00y = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0x6 << 3) | 1,
		_y00y = ((VEC_C_MASK(1, 0, 0, 1) << 8) | 0x6 << 3) | 1,
		_z00y = ((VEC_C_MASK(2, 0, 0, 1) << 8) | 0x6 << 3) | 1,
		_w00y = ((VEC_C_MASK(3, 0, 0, 1) << 8) | 0x6 << 3) | 1,
		_000y = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0xe << 3) | 1,
		_0xxz = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0x8 << 3) | 1,
		_0yxz = ((VEC_C_MASK(0, 1, 0, 2) << 8) | 0x8 << 3) | 1,
		_0zxz = ((VEC_C_MASK(0, 2, 0, 2) << 8) | 0x8 << 3) | 1,
		_0wxz = ((VEC_C_MASK(0, 3, 0, 2) << 8) | 0x8 << 3) | 1,
		_x0xz = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0x4 << 3) | 1,
		_y0xz = ((VEC_C_MASK(1, 0, 0, 2) << 8) | 0x4 << 3) | 1,
		_z0xz = ((VEC_C_MASK(2, 0, 0, 2) << 8) | 0x4 << 3) | 1,
		_w0xz = ((VEC_C_MASK(3, 0, 0, 2) << 8) | 0x4 << 3) | 1,
		_00xz = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0xc << 3) | 1,
		_0xyz = ((VEC_C_MASK(0, 0, 1, 2) << 8) | 0x8 << 3) | 1,
		_0yyz = ((VEC_C_MASK(0, 1, 1, 2) << 8) | 0x8 << 3) | 1,
		_0zyz = ((VEC_C_MASK(0, 2, 1, 2) << 8) | 0x8 << 3) | 1,
		_0wyz = ((VEC_C_MASK(0, 3, 1, 2) << 8) | 0x8 << 3) | 1,
		_x0yz = ((VEC_C_MASK(0, 0, 1, 2) << 8) | 0x4 << 3) | 1,
		_y0yz = ((VEC_C_MASK(1, 0, 1, 2) << 8) | 0x4 << 3) | 1,
		_z0yz = ((VEC_C_MASK(2, 0, 1, 2) << 8) | 0x4 << 3) | 1,
		_w0yz = ((VEC_C_MASK(3, 0, 1, 2) << 8) | 0x4 << 3) | 1,
		_00yz = ((VEC_C_MASK(0, 0, 1, 2) << 8) | 0xc << 3) | 1,
		_0xzz = ((VEC_C_MASK(0, 0, 2, 2) << 8) | 0x8 << 3) | 1,
		_0yzz = ((VEC_C_MASK(0, 1, 2, 2) << 8) | 0x8 << 3) | 1,
		_0zzz = ((VEC_C_MASK(0, 2, 2, 2) << 8) | 0x8 << 3) | 1,
		_0wzz = ((VEC_C_MASK(0, 3, 2, 2) << 8) | 0x8 << 3) | 1,
		_x0zz = ((VEC_C_MASK(0, 0, 2, 2) << 8) | 0x4 << 3) | 1,
		_y0zz = ((VEC_C_MASK(1, 0, 2, 2) << 8) | 0x4 << 3) | 1,
		_z0zz = ((VEC_C_MASK(2, 0, 2, 2) << 8) | 0x4 << 3) | 1,
		_w0zz = ((VEC_C_MASK(3, 0, 2, 2) << 8) | 0x4 << 3) | 1,
		_00zz = ((VEC_C_MASK(0, 0, 2, 2) << 8) | 0xc << 3) | 1,
		_0xwz = ((VEC_C_MASK(0, 0, 3, 2) << 8) | 0x8 << 3) | 1,
		_0ywz = ((VEC_C_MASK(0, 1, 3, 2) << 8) | 0x8 << 3) | 1,
		_0zwz = ((VEC_C_MASK(0, 2, 3, 2) << 8) | 0x8 << 3) | 1,
		_0wwz = ((VEC_C_MASK(0, 3, 3, 2) << 8) | 0x8 << 3) | 1,
		_x0wz = ((VEC_C_MASK(0, 0, 3, 2) << 8) | 0x4 << 3) | 1,
		_y0wz = ((VEC_C_MASK(1, 0, 3, 2) << 8) | 0x4 << 3) | 1,
		_z0wz = ((VEC_C_MASK(2, 0, 3, 2) << 8) | 0x4 << 3) | 1,
		_w0wz = ((VEC_C_MASK(3, 0, 3, 2) << 8) | 0x4 << 3) | 1,
		_00wz = ((VEC_C_MASK(0, 0, 3, 2) << 8) | 0xc << 3) | 1,
		_xx0z = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0x2 << 3) | 1,
		_yx0z = ((VEC_C_MASK(1, 0, 0, 2) << 8) | 0x2 << 3) | 1,
		_zx0z = ((VEC_C_MASK(2, 0, 0, 2) << 8) | 0x2 << 3) | 1,
		_wx0z = ((VEC_C_MASK(3, 0, 0, 2) << 8) | 0x2 << 3) | 1,
		_0x0z = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0xa << 3) | 1,
		_xy0z = ((VEC_C_MASK(0, 1, 0, 2) << 8) | 0x2 << 3) | 1,
		_yy0z = ((VEC_C_MASK(1, 1, 0, 2) << 8) | 0x2 << 3) | 1,
		_zy0z = ((VEC_C_MASK(2, 1, 0, 2) << 8) | 0x2 << 3) | 1,
		_wy0z = ((VEC_C_MASK(3, 1, 0, 2) << 8) | 0x2 << 3) | 1,
		_0y0z = ((VEC_C_MASK(0, 1, 0, 2) << 8) | 0xa << 3) | 1,
		_xz0z = ((VEC_C_MASK(0, 2, 0, 2) << 8) | 0x2 << 3) | 1,
		_yz0z = ((VEC_C_MASK(1, 2, 0, 2) << 8) | 0x2 << 3) | 1,
		_zz0z = ((VEC_C_MASK(2, 2, 0, 2) << 8) | 0x2 << 3) | 1,
		_wz0z = ((VEC_C_MASK(3, 2, 0, 2) << 8) | 0x2 << 3) | 1,
		_0z0z = ((VEC_C_MASK(0, 2, 0, 2) << 8) | 0xa << 3) | 1,
		_xw0z = ((VEC_C_MASK(0, 3, 0, 2) << 8) | 0x2 << 3) | 1,
		_yw0z = ((VEC_C_MASK(1, 3, 0, 2) << 8) | 0x2 << 3) | 1,
		_zw0z = ((VEC_C_MASK(2, 3, 0, 2) << 8) | 0x2 << 3) | 1,
		_ww0z = ((VEC_C_MASK(3, 3, 0, 2) << 8) | 0x2 << 3) | 1,
		_0w0z = ((VEC_C_MASK(0, 3, 0, 2) << 8) | 0xa << 3) | 1,
		_x00z = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0x6 << 3) | 1,
		_y00z = ((VEC_C_MASK(1, 0, 0, 2) << 8) | 0x6 << 3) | 1,
		_z00z = ((VEC_C_MASK(2, 0, 0, 2) << 8) | 0x6 << 3) | 1,
		_w00z = ((VEC_C_MASK(3, 0, 0, 2) << 8) | 0x6 << 3) | 1,
		_000z = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0xe << 3) | 1,
		_0xxw = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0x8 << 3) | 1,
		_0yxw = ((VEC_C_MASK(0, 1, 0, 3) << 8) | 0x8 << 3) | 1,
		_0zxw = ((VEC_C_MASK(0, 2, 0, 3) << 8) | 0x8 << 3) | 1,
		_0wxw = ((VEC_C_MASK(0, 3, 0, 3) << 8) | 0x8 << 3) | 1,
		_x0xw = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0x4 << 3) | 1,
		_y0xw = ((VEC_C_MASK(1, 0, 0, 3) << 8) | 0x4 << 3) | 1,
		_z0xw = ((VEC_C_MASK(2, 0, 0, 3) << 8) | 0x4 << 3) | 1,
		_w0xw = ((VEC_C_MASK(3, 0, 0, 3) << 8) | 0x4 << 3) | 1,
		_00xw = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0xc << 3) | 1,
		_0xyw = ((VEC_C_MASK(0, 0, 1, 3) << 8) | 0x8 << 3) | 1,
		_0yyw = ((VEC_C_MASK(0, 1, 1, 3) << 8) | 0x8 << 3) | 1,
		_0zyw = ((VEC_C_MASK(0, 2, 1, 3) << 8) | 0x8 << 3) | 1,
		_0wyw = ((VEC_C_MASK(0, 3, 1, 3) << 8) | 0x8 << 3) | 1,
		_x0yw = ((VEC_C_MASK(0, 0, 1, 3) << 8) | 0x4 << 3) | 1,
		_y0yw = ((VEC_C_MASK(1, 0, 1, 3) << 8) | 0x4 << 3) | 1,
		_z0yw = ((VEC_C_MASK(2, 0, 1, 3) << 8) | 0x4 << 3) | 1,
		_w0yw = ((VEC_C_MASK(3, 0, 1, 3) << 8) | 0x4 << 3) | 1,
		_00yw = ((VEC_C_MASK(0, 0, 1, 3) << 8) | 0xc << 3) | 1,
		_0xzw = ((VEC_C_MASK(0, 0, 2, 3) << 8) | 0x8 << 3) | 1,
		_0yzw = ((VEC_C_MASK(0, 1, 2, 3) << 8) | 0x8 << 3) | 1,
		_0zzw = ((VEC_C_MASK(0, 2, 2, 3) << 8) | 0x8 << 3) | 1,
		_0wzw = ((VEC_C_MASK(0, 3, 2, 3) << 8) | 0x8 << 3) | 1,
		_x0zw = ((VEC_C_MASK(0, 0, 2, 3) << 8) | 0x4 << 3) | 1,
		_y0zw = ((VEC_C_MASK(1, 0, 2, 3) << 8) | 0x4 << 3) | 1,
		_z0zw = ((VEC_C_MASK(2, 0, 2, 3) << 8) | 0x4 << 3) | 1,
		_w0zw = ((VEC_C_MASK(3, 0, 2, 3) << 8) | 0x4 << 3) | 1,
		_00zw = ((VEC_C_MASK(0, 0, 2, 3) << 8) | 0xc << 3) | 1,
		_0xww = ((VEC_C_MASK(0, 0, 3, 3) << 8) | 0x8 << 3) | 1,
		_0yww = ((VEC_C_MASK(0, 1, 3, 3) << 8) | 0x8 << 3) | 1,
		_0zww = ((VEC_C_MASK(0, 2, 3, 3) << 8) | 0x8 << 3) | 1,
		_0www = ((VEC_C_MASK(0, 3, 3, 3) << 8) | 0x8 << 3) | 1,
		_x0ww = ((VEC_C_MASK(0, 0, 3, 3) << 8) | 0x4 << 3) | 1,
		_y0ww = ((VEC_C_MASK(1, 0, 3, 3) << 8) | 0x4 << 3) | 1,
		_z0ww = ((VEC_C_MASK(2, 0, 3, 3) << 8) | 0x4 << 3) | 1,
		_w0ww = ((VEC_C_MASK(3, 0, 3, 3) << 8) | 0x4 << 3) | 1,
		_00ww = ((VEC_C_MASK(0, 0, 3, 3) << 8) | 0xc << 3) | 1,
		_xx0w = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0x2 << 3) | 1,
		_yx0w = ((VEC_C_MASK(1, 0, 0, 3) << 8) | 0x2 << 3) | 1,
		_zx0w = ((VEC_C_MASK(2, 0, 0, 3) << 8) | 0x2 << 3) | 1,
		_wx0w = ((VEC_C_MASK(3, 0, 0, 3) << 8) | 0x2 << 3) | 1,
		_0x0w = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0xa << 3) | 1,
		_xy0w = ((VEC_C_MASK(0, 1, 0, 3) << 8) | 0x2 << 3) | 1,
		_yy0w = ((VEC_C_MASK(1, 1, 0, 3) << 8) | 0x2 << 3) | 1,
		_zy0w = ((VEC_C_MASK(2, 1, 0, 3) << 8) | 0x2 << 3) | 1,
		_wy0w = ((VEC_C_MASK(3, 1, 0, 3) << 8) | 0x2 << 3) | 1,
		_0y0w = ((VEC_C_MASK(0, 1, 0, 3) << 8) | 0xa << 3) | 1,
		_xz0w = ((VEC_C_MASK(0, 2, 0, 3) << 8) | 0x2 << 3) | 1,
		_yz0w = ((VEC_C_MASK(1, 2, 0, 3) << 8) | 0x2 << 3) | 1,
		_zz0w = ((VEC_C_MASK(2, 2, 0, 3) << 8) | 0x2 << 3) | 1,
		_wz0w = ((VEC_C_MASK(3, 2, 0, 3) << 8) | 0x2 << 3) | 1,
		_0z0w = ((VEC_C_MASK(0, 2, 0, 3) << 8) | 0xa << 3) | 1,
		_xw0w = ((VEC_C_MASK(0, 3, 0, 3) << 8) | 0x2 << 3) | 1,
		_yw0w = ((VEC_C_MASK(1, 3, 0, 3) << 8) | 0x2 << 3) | 1,
		_zw0w = ((VEC_C_MASK(2, 3, 0, 3) << 8) | 0x2 << 3) | 1,
		_ww0w = ((VEC_C_MASK(3, 3, 0, 3) << 8) | 0x2 << 3) | 1,
		_0w0w = ((VEC_C_MASK(0, 3, 0, 3) << 8) | 0xa << 3) | 1,
		_x00w = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0x6 << 3) | 1,
		_y00w = ((VEC_C_MASK(1, 0, 0, 3) << 8) | 0x6 << 3) | 1,
		_z00w = ((VEC_C_MASK(2, 0, 0, 3) << 8) | 0x6 << 3) | 1,
		_w00w = ((VEC_C_MASK(3, 0, 0, 3) << 8) | 0x6 << 3) | 1,
		_000w = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0xe << 3) | 1,
		_xxx0 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x1 << 3) | 1,
		_yxx0 = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x1 << 3) | 1,
		_zxx0 = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x1 << 3) | 1,
		_wxx0 = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x1 << 3) | 1,
		_0xx0 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x9 << 3) | 1,
		_xyx0 = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0x1 << 3) | 1,
		_yyx0 = ((VEC_C_MASK(1, 1, 0, 0) << 8) | 0x1 << 3) | 1,
		_zyx0 = ((VEC_C_MASK(2, 1, 0, 0) << 8) | 0x1 << 3) | 1,
		_wyx0 = ((VEC_C_MASK(3, 1, 0, 0) << 8) | 0x1 << 3) | 1,
		_0yx0 = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0x9 << 3) | 1,
		_xzx0 = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0x1 << 3) | 1,
		_yzx0 = ((VEC_C_MASK(1, 2, 0, 0) << 8) | 0x1 << 3) | 1,
		_zzx0 = ((VEC_C_MASK(2, 2, 0, 0) << 8) | 0x1 << 3) | 1,
		_wzx0 = ((VEC_C_MASK(3, 2, 0, 0) << 8) | 0x1 << 3) | 1,
		_0zx0 = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0x9 << 3) | 1,
		_xwx0 = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0x1 << 3) | 1,
		_ywx0 = ((VEC_C_MASK(1, 3, 0, 0) << 8) | 0x1 << 3) | 1,
		_zwx0 = ((VEC_C_MASK(2, 3, 0, 0) << 8) | 0x1 << 3) | 1,
		_wwx0 = ((VEC_C_MASK(3, 3, 0, 0) << 8) | 0x1 << 3) | 1,
		_0wx0 = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0x9 << 3) | 1,
		_x0x0 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x5 << 3) | 1,
		_y0x0 = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x5 << 3) | 1,
		_z0x0 = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x5 << 3) | 1,
		_w0x0 = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x5 << 3) | 1,
		_00x0 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0xd << 3) | 1,
		_xxy0 = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0x1 << 3) | 1,
		_yxy0 = ((VEC_C_MASK(1, 0, 1, 0) << 8) | 0x1 << 3) | 1,
		_zxy0 = ((VEC_C_MASK(2, 0, 1, 0) << 8) | 0x1 << 3) | 1,
		_wxy0 = ((VEC_C_MASK(3, 0, 1, 0) << 8) | 0x1 << 3) | 1,
		_0xy0 = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0x9 << 3) | 1,
		_xyy0 = ((VEC_C_MASK(0, 1, 1, 0) << 8) | 0x1 << 3) | 1,
		_yyy0 = ((VEC_C_MASK(1, 1, 1, 0) << 8) | 0x1 << 3) | 1,
		_zyy0 = ((VEC_C_MASK(2, 1, 1, 0) << 8) | 0x1 << 3) | 1,
		_wyy0 = ((VEC_C_MASK(3, 1, 1, 0) << 8) | 0x1 << 3) | 1,
		_0yy0 = ((VEC_C_MASK(0, 1, 1, 0) << 8) | 0x9 << 3) | 1,
		_xzy0 = ((VEC_C_MASK(0, 2, 1, 0) << 8) | 0x1 << 3) | 1,
		_yzy0 = ((VEC_C_MASK(1, 2, 1, 0) << 8) | 0x1 << 3) | 1,
		_zzy0 = ((VEC_C_MASK(2, 2, 1, 0) << 8) | 0x1 << 3) | 1,
		_wzy0 = ((VEC_C_MASK(3, 2, 1, 0) << 8) | 0x1 << 3) | 1,
		_0zy0 = ((VEC_C_MASK(0, 2, 1, 0) << 8) | 0x9 << 3) | 1,
		_xwy0 = ((VEC_C_MASK(0, 3, 1, 0) << 8) | 0x1 << 3) | 1,
		_ywy0 = ((VEC_C_MASK(1, 3, 1, 0) << 8) | 0x1 << 3) | 1,
		_zwy0 = ((VEC_C_MASK(2, 3, 1, 0) << 8) | 0x1 << 3) | 1,
		_wwy0 = ((VEC_C_MASK(3, 3, 1, 0) << 8) | 0x1 << 3) | 1,
		_0wy0 = ((VEC_C_MASK(0, 3, 1, 0) << 8) | 0x9 << 3) | 1,
		_x0y0 = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0x5 << 3) | 1,
		_y0y0 = ((VEC_C_MASK(1, 0, 1, 0) << 8) | 0x5 << 3) | 1,
		_z0y0 = ((VEC_C_MASK(2, 0, 1, 0) << 8) | 0x5 << 3) | 1,
		_w0y0 = ((VEC_C_MASK(3, 0, 1, 0) << 8) | 0x5 << 3) | 1,
		_00y0 = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0xd << 3) | 1,
		_xxz0 = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0x1 << 3) | 1,
		_yxz0 = ((VEC_C_MASK(1, 0, 2, 0) << 8) | 0x1 << 3) | 1,
		_zxz0 = ((VEC_C_MASK(2, 0, 2, 0) << 8) | 0x1 << 3) | 1,
		_wxz0 = ((VEC_C_MASK(3, 0, 2, 0) << 8) | 0x1 << 3) | 1,
		_0xz0 = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0x9 << 3) | 1,
		_xyz0 = ((VEC_C_MASK(0, 1, 2, 0) << 8) | 0x1 << 3) | 1,
		_yyz0 = ((VEC_C_MASK(1, 1, 2, 0) << 8) | 0x1 << 3) | 1,
		_zyz0 = ((VEC_C_MASK(2, 1, 2, 0) << 8) | 0x1 << 3) | 1,
		_wyz0 = ((VEC_C_MASK(3, 1, 2, 0) << 8) | 0x1 << 3) | 1,
		_0yz0 = ((VEC_C_MASK(0, 1, 2, 0) << 8) | 0x9 << 3) | 1,
		_xzz0 = ((VEC_C_MASK(0, 2, 2, 0) << 8) | 0x1 << 3) | 1,
		_yzz0 = ((VEC_C_MASK(1, 2, 2, 0) << 8) | 0x1 << 3) | 1,
		_zzz0 = ((VEC_C_MASK(2, 2, 2, 0) << 8) | 0x1 << 3) | 1,
		_wzz0 = ((VEC_C_MASK(3, 2, 2, 0) << 8) | 0x1 << 3) | 1,
		_0zz0 = ((VEC_C_MASK(0, 2, 2, 0) << 8) | 0x9 << 3) | 1,
		_xwz0 = ((VEC_C_MASK(0, 3, 2, 0) << 8) | 0x1 << 3) | 1,
		_ywz0 = ((VEC_C_MASK(1, 3, 2, 0) << 8) | 0x1 << 3) | 1,
		_zwz0 = ((VEC_C_MASK(2, 3, 2, 0) << 8) | 0x1 << 3) | 1,
		_wwz0 = ((VEC_C_MASK(3, 3, 2, 0) << 8) | 0x1 << 3) | 1,
		_0wz0 = ((VEC_C_MASK(0, 3, 2, 0) << 8) | 0x9 << 3) | 1,
		_x0z0 = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0x5 << 3) | 1,
		_y0z0 = ((VEC_C_MASK(1, 0, 2, 0) << 8) | 0x5 << 3) | 1,
		_z0z0 = ((VEC_C_MASK(2, 0, 2, 0) << 8) | 0x5 << 3) | 1,
		_w0z0 = ((VEC_C_MASK(3, 0, 2, 0) << 8) | 0x5 << 3) | 1,
		_00z0 = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0xd << 3) | 1,
		_xxw0 = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0x1 << 3) | 1,
		_yxw0 = ((VEC_C_MASK(1, 0, 3, 0) << 8) | 0x1 << 3) | 1,
		_zxw0 = ((VEC_C_MASK(2, 0, 3, 0) << 8) | 0x1 << 3) | 1,
		_wxw0 = ((VEC_C_MASK(3, 0, 3, 0) << 8) | 0x1 << 3) | 1,
		_0xw0 = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0x9 << 3) | 1,
		_xyw0 = ((VEC_C_MASK(0, 1, 3, 0) << 8) | 0x1 << 3) | 1,
		_yyw0 = ((VEC_C_MASK(1, 1, 3, 0) << 8) | 0x1 << 3) | 1,
		_zyw0 = ((VEC_C_MASK(2, 1, 3, 0) << 8) | 0x1 << 3) | 1,
		_wyw0 = ((VEC_C_MASK(3, 1, 3, 0) << 8) | 0x1 << 3) | 1,
		_0yw0 = ((VEC_C_MASK(0, 1, 3, 0) << 8) | 0x9 << 3) | 1,
		_xzw0 = ((VEC_C_MASK(0, 2, 3, 0) << 8) | 0x1 << 3) | 1,
		_yzw0 = ((VEC_C_MASK(1, 2, 3, 0) << 8) | 0x1 << 3) | 1,
		_zzw0 = ((VEC_C_MASK(2, 2, 3, 0) << 8) | 0x1 << 3) | 1,
		_wzw0 = ((VEC_C_MASK(3, 2, 3, 0) << 8) | 0x1 << 3) | 1,
		_0zw0 = ((VEC_C_MASK(0, 2, 3, 0) << 8) | 0x9 << 3) | 1,
		_xww0 = ((VEC_C_MASK(0, 3, 3, 0) << 8) | 0x1 << 3) | 1,
		_yww0 = ((VEC_C_MASK(1, 3, 3, 0) << 8) | 0x1 << 3) | 1,
		_zww0 = ((VEC_C_MASK(2, 3, 3, 0) << 8) | 0x1 << 3) | 1,
		_www0 = ((VEC_C_MASK(3, 3, 3, 0) << 8) | 0x1 << 3) | 1,
		_0ww0 = ((VEC_C_MASK(0, 3, 3, 0) << 8) | 0x9 << 3) | 1,
		_x0w0 = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0x5 << 3) | 1,
		_y0w0 = ((VEC_C_MASK(1, 0, 3, 0) << 8) | 0x5 << 3) | 1,
		_z0w0 = ((VEC_C_MASK(2, 0, 3, 0) << 8) | 0x5 << 3) | 1,
		_w0w0 = ((VEC_C_MASK(3, 0, 3, 0) << 8) | 0x5 << 3) | 1,
		_00w0 = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0xd << 3) | 1,
		_xx00 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x3 << 3) | 1,
		_yx00 = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x3 << 3) | 1,
		_zx00 = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x3 << 3) | 1,
		_wx00 = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x3 << 3) | 1,
		_0x00 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0xb << 3) | 1,
		_xy00 = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0x3 << 3) | 1,
		_yy00 = ((VEC_C_MASK(1, 1, 0, 0) << 8) | 0x3 << 3) | 1,
		_zy00 = ((VEC_C_MASK(2, 1, 0, 0) << 8) | 0x3 << 3) | 1,
		_wy00 = ((VEC_C_MASK(3, 1, 0, 0) << 8) | 0x3 << 3) | 1,
		_0y00 = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0xb << 3) | 1,
		_xz00 = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0x3 << 3) | 1,
		_yz00 = ((VEC_C_MASK(1, 2, 0, 0) << 8) | 0x3 << 3) | 1,
		_zz00 = ((VEC_C_MASK(2, 2, 0, 0) << 8) | 0x3 << 3) | 1,
		_wz00 = ((VEC_C_MASK(3, 2, 0, 0) << 8) | 0x3 << 3) | 1,
		_0z00 = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0xb << 3) | 1,
		_xw00 = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0x3 << 3) | 1,
		_yw00 = ((VEC_C_MASK(1, 3, 0, 0) << 8) | 0x3 << 3) | 1,
		_zw00 = ((VEC_C_MASK(2, 3, 0, 0) << 8) | 0x3 << 3) | 1,
		_ww00 = ((VEC_C_MASK(3, 3, 0, 0) << 8) | 0x3 << 3) | 1,
		_0w00 = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0xb << 3) | 1,
		_x000 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x7 << 3) | 1,
		_y000 = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x7 << 3) | 1,
		_z000 = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x7 << 3) | 1,
		_w000 = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x7 << 3) | 1,

		_1xxx = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x8 << 3) | 2,
		_1yxx = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0x8 << 3) | 2,
		_1zxx = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0x8 << 3) | 2,
		_1wxx = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0x8 << 3) | 2,
		_x1xx = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x4 << 3) | 2,
		_y1xx = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x4 << 3) | 2,
		_z1xx = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x4 << 3) | 2,
		_w1xx = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x4 << 3) | 2,
		_11xx = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0xc << 3) | 2,
		_1xyx = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0x8 << 3) | 2,
		_1yyx = ((VEC_C_MASK(0, 1, 1, 0) << 8) | 0x8 << 3) | 2,
		_1zyx = ((VEC_C_MASK(0, 2, 1, 0) << 8) | 0x8 << 3) | 2,
		_1wyx = ((VEC_C_MASK(0, 3, 1, 0) << 8) | 0x8 << 3) | 2,
		_x1yx = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0x4 << 3) | 2,
		_y1yx = ((VEC_C_MASK(1, 0, 1, 0) << 8) | 0x4 << 3) | 2,
		_z1yx = ((VEC_C_MASK(2, 0, 1, 0) << 8) | 0x4 << 3) | 2,
		_w1yx = ((VEC_C_MASK(3, 0, 1, 0) << 8) | 0x4 << 3) | 2,
		_11yx = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0xc << 3) | 2,
		_1xzx = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0x8 << 3) | 2,
		_1yzx = ((VEC_C_MASK(0, 1, 2, 0) << 8) | 0x8 << 3) | 2,
		_1zzx = ((VEC_C_MASK(0, 2, 2, 0) << 8) | 0x8 << 3) | 2,
		_1wzx = ((VEC_C_MASK(0, 3, 2, 0) << 8) | 0x8 << 3) | 2,
		_x1zx = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0x4 << 3) | 2,
		_y1zx = ((VEC_C_MASK(1, 0, 2, 0) << 8) | 0x4 << 3) | 2,
		_z1zx = ((VEC_C_MASK(2, 0, 2, 0) << 8) | 0x4 << 3) | 2,
		_w1zx = ((VEC_C_MASK(3, 0, 2, 0) << 8) | 0x4 << 3) | 2,
		_11zx = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0xc << 3) | 2,
		_1xwx = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0x8 << 3) | 2,
		_1ywx = ((VEC_C_MASK(0, 1, 3, 0) << 8) | 0x8 << 3) | 2,
		_1zwx = ((VEC_C_MASK(0, 2, 3, 0) << 8) | 0x8 << 3) | 2,
		_1wwx = ((VEC_C_MASK(0, 3, 3, 0) << 8) | 0x8 << 3) | 2,
		_x1wx = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0x4 << 3) | 2,
		_y1wx = ((VEC_C_MASK(1, 0, 3, 0) << 8) | 0x4 << 3) | 2,
		_z1wx = ((VEC_C_MASK(2, 0, 3, 0) << 8) | 0x4 << 3) | 2,
		_w1wx = ((VEC_C_MASK(3, 0, 3, 0) << 8) | 0x4 << 3) | 2,
		_11wx = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0xc << 3) | 2,
		_xx1x = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x2 << 3) | 2,
		_yx1x = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x2 << 3) | 2,
		_zx1x = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x2 << 3) | 2,
		_wx1x = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x2 << 3) | 2,
		_1x1x = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0xa << 3) | 2,
		_xy1x = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0x2 << 3) | 2,
		_yy1x = ((VEC_C_MASK(1, 1, 0, 0) << 8) | 0x2 << 3) | 2,
		_zy1x = ((VEC_C_MASK(2, 1, 0, 0) << 8) | 0x2 << 3) | 2,
		_wy1x = ((VEC_C_MASK(3, 1, 0, 0) << 8) | 0x2 << 3) | 2,
		_1y1x = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0xa << 3) | 2,
		_xz1x = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0x2 << 3) | 2,
		_yz1x = ((VEC_C_MASK(1, 2, 0, 0) << 8) | 0x2 << 3) | 2,
		_zz1x = ((VEC_C_MASK(2, 2, 0, 0) << 8) | 0x2 << 3) | 2,
		_wz1x = ((VEC_C_MASK(3, 2, 0, 0) << 8) | 0x2 << 3) | 2,
		_1z1x = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0xa << 3) | 2,
		_xw1x = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0x2 << 3) | 2,
		_yw1x = ((VEC_C_MASK(1, 3, 0, 0) << 8) | 0x2 << 3) | 2,
		_zw1x = ((VEC_C_MASK(2, 3, 0, 0) << 8) | 0x2 << 3) | 2,
		_ww1x = ((VEC_C_MASK(3, 3, 0, 0) << 8) | 0x2 << 3) | 2,
		_1w1x = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0xa << 3) | 2,
		_x11x = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x6 << 3) | 2,
		_y11x = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x6 << 3) | 2,
		_z11x = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x6 << 3) | 2,
		_w11x = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x6 << 3) | 2,
		_111x = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0xe << 3) | 2,
		_1xxy = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0x8 << 3) | 2,
		_1yxy = ((VEC_C_MASK(0, 1, 0, 1) << 8) | 0x8 << 3) | 2,
		_1zxy = ((VEC_C_MASK(0, 2, 0, 1) << 8) | 0x8 << 3) | 2,
		_1wxy = ((VEC_C_MASK(0, 3, 0, 1) << 8) | 0x8 << 3) | 2,
		_x1xy = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0x4 << 3) | 2,
		_y1xy = ((VEC_C_MASK(1, 0, 0, 1) << 8) | 0x4 << 3) | 2,
		_z1xy = ((VEC_C_MASK(2, 0, 0, 1) << 8) | 0x4 << 3) | 2,
		_w1xy = ((VEC_C_MASK(3, 0, 0, 1) << 8) | 0x4 << 3) | 2,
		_11xy = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0xc << 3) | 2,
		_1xyy = ((VEC_C_MASK(0, 0, 1, 1) << 8) | 0x8 << 3) | 2,
		_1yyy = ((VEC_C_MASK(0, 1, 1, 1) << 8) | 0x8 << 3) | 2,
		_1zyy = ((VEC_C_MASK(0, 2, 1, 1) << 8) | 0x8 << 3) | 2,
		_1wyy = ((VEC_C_MASK(0, 3, 1, 1) << 8) | 0x8 << 3) | 2,
		_x1yy = ((VEC_C_MASK(0, 0, 1, 1) << 8) | 0x4 << 3) | 2,
		_y1yy = ((VEC_C_MASK(1, 0, 1, 1) << 8) | 0x4 << 3) | 2,
		_z1yy = ((VEC_C_MASK(2, 0, 1, 1) << 8) | 0x4 << 3) | 2,
		_w1yy = ((VEC_C_MASK(3, 0, 1, 1) << 8) | 0x4 << 3) | 2,
		_11yy = ((VEC_C_MASK(0, 0, 1, 1) << 8) | 0xc << 3) | 2,
		_1xzy = ((VEC_C_MASK(0, 0, 2, 1) << 8) | 0x8 << 3) | 2,
		_1yzy = ((VEC_C_MASK(0, 1, 2, 1) << 8) | 0x8 << 3) | 2,
		_1zzy = ((VEC_C_MASK(0, 2, 2, 1) << 8) | 0x8 << 3) | 2,
		_1wzy = ((VEC_C_MASK(0, 3, 2, 1) << 8) | 0x8 << 3) | 2,
		_x1zy = ((VEC_C_MASK(0, 0, 2, 1) << 8) | 0x4 << 3) | 2,
		_y1zy = ((VEC_C_MASK(1, 0, 2, 1) << 8) | 0x4 << 3) | 2,
		_z1zy = ((VEC_C_MASK(2, 0, 2, 1) << 8) | 0x4 << 3) | 2,
		_w1zy = ((VEC_C_MASK(3, 0, 2, 1) << 8) | 0x4 << 3) | 2,
		_11zy = ((VEC_C_MASK(0, 0, 2, 1) << 8) | 0xc << 3) | 2,
		_1xwy = ((VEC_C_MASK(0, 0, 3, 1) << 8) | 0x8 << 3) | 2,
		_1ywy = ((VEC_C_MASK(0, 1, 3, 1) << 8) | 0x8 << 3) | 2,
		_1zwy = ((VEC_C_MASK(0, 2, 3, 1) << 8) | 0x8 << 3) | 2,
		_1wwy = ((VEC_C_MASK(0, 3, 3, 1) << 8) | 0x8 << 3) | 2,
		_x1wy = ((VEC_C_MASK(0, 0, 3, 1) << 8) | 0x4 << 3) | 2,
		_y1wy = ((VEC_C_MASK(1, 0, 3, 1) << 8) | 0x4 << 3) | 2,
		_z1wy = ((VEC_C_MASK(2, 0, 3, 1) << 8) | 0x4 << 3) | 2,
		_w1wy = ((VEC_C_MASK(3, 0, 3, 1) << 8) | 0x4 << 3) | 2,
		_11wy = ((VEC_C_MASK(0, 0, 3, 1) << 8) | 0xc << 3) | 2,
		_xx1y = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0x2 << 3) | 2,
		_yx1y = ((VEC_C_MASK(1, 0, 0, 1) << 8) | 0x2 << 3) | 2,
		_zx1y = ((VEC_C_MASK(2, 0, 0, 1) << 8) | 0x2 << 3) | 2,
		_wx1y = ((VEC_C_MASK(3, 0, 0, 1) << 8) | 0x2 << 3) | 2,
		_1x1y = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0xa << 3) | 2,
		_xy1y = ((VEC_C_MASK(0, 1, 0, 1) << 8) | 0x2 << 3) | 2,
		_yy1y = ((VEC_C_MASK(1, 1, 0, 1) << 8) | 0x2 << 3) | 2,
		_zy1y = ((VEC_C_MASK(2, 1, 0, 1) << 8) | 0x2 << 3) | 2,
		_wy1y = ((VEC_C_MASK(3, 1, 0, 1) << 8) | 0x2 << 3) | 2,
		_1y1y = ((VEC_C_MASK(0, 1, 0, 1) << 8) | 0xa << 3) | 2,
		_xz1y = ((VEC_C_MASK(0, 2, 0, 1) << 8) | 0x2 << 3) | 2,
		_yz1y = ((VEC_C_MASK(1, 2, 0, 1) << 8) | 0x2 << 3) | 2,
		_zz1y = ((VEC_C_MASK(2, 2, 0, 1) << 8) | 0x2 << 3) | 2,
		_wz1y = ((VEC_C_MASK(3, 2, 0, 1) << 8) | 0x2 << 3) | 2,
		_1z1y = ((VEC_C_MASK(0, 2, 0, 1) << 8) | 0xa << 3) | 2,
		_xw1y = ((VEC_C_MASK(0, 3, 0, 1) << 8) | 0x2 << 3) | 2,
		_yw1y = ((VEC_C_MASK(1, 3, 0, 1) << 8) | 0x2 << 3) | 2,
		_zw1y = ((VEC_C_MASK(2, 3, 0, 1) << 8) | 0x2 << 3) | 2,
		_ww1y = ((VEC_C_MASK(3, 3, 0, 1) << 8) | 0x2 << 3) | 2,
		_1w1y = ((VEC_C_MASK(0, 3, 0, 1) << 8) | 0xa << 3) | 2,
		_x11y = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0x6 << 3) | 2,
		_y11y = ((VEC_C_MASK(1, 0, 0, 1) << 8) | 0x6 << 3) | 2,
		_z11y = ((VEC_C_MASK(2, 0, 0, 1) << 8) | 0x6 << 3) | 2,
		_w11y = ((VEC_C_MASK(3, 0, 0, 1) << 8) | 0x6 << 3) | 2,
		_111y = ((VEC_C_MASK(0, 0, 0, 1) << 8) | 0xe << 3) | 2,
		_1xxz = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0x8 << 3) | 2,
		_1yxz = ((VEC_C_MASK(0, 1, 0, 2) << 8) | 0x8 << 3) | 2,
		_1zxz = ((VEC_C_MASK(0, 2, 0, 2) << 8) | 0x8 << 3) | 2,
		_1wxz = ((VEC_C_MASK(0, 3, 0, 2) << 8) | 0x8 << 3) | 2,
		_x1xz = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0x4 << 3) | 2,
		_y1xz = ((VEC_C_MASK(1, 0, 0, 2) << 8) | 0x4 << 3) | 2,
		_z1xz = ((VEC_C_MASK(2, 0, 0, 2) << 8) | 0x4 << 3) | 2,
		_w1xz = ((VEC_C_MASK(3, 0, 0, 2) << 8) | 0x4 << 3) | 2,
		_11xz = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0xc << 3) | 2,
		_1xyz = ((VEC_C_MASK(0, 0, 1, 2) << 8) | 0x8 << 3) | 2,
		_1yyz = ((VEC_C_MASK(0, 1, 1, 2) << 8) | 0x8 << 3) | 2,
		_1zyz = ((VEC_C_MASK(0, 2, 1, 2) << 8) | 0x8 << 3) | 2,
		_1wyz = ((VEC_C_MASK(0, 3, 1, 2) << 8) | 0x8 << 3) | 2,
		_x1yz = ((VEC_C_MASK(0, 0, 1, 2) << 8) | 0x4 << 3) | 2,
		_y1yz = ((VEC_C_MASK(1, 0, 1, 2) << 8) | 0x4 << 3) | 2,
		_z1yz = ((VEC_C_MASK(2, 0, 1, 2) << 8) | 0x4 << 3) | 2,
		_w1yz = ((VEC_C_MASK(3, 0, 1, 2) << 8) | 0x4 << 3) | 2,
		_11yz = ((VEC_C_MASK(0, 0, 1, 2) << 8) | 0xc << 3) | 2,
		_1xzz = ((VEC_C_MASK(0, 0, 2, 2) << 8) | 0x8 << 3) | 2,
		_1yzz = ((VEC_C_MASK(0, 1, 2, 2) << 8) | 0x8 << 3) | 2,
		_1zzz = ((VEC_C_MASK(0, 2, 2, 2) << 8) | 0x8 << 3) | 2,
		_1wzz = ((VEC_C_MASK(0, 3, 2, 2) << 8) | 0x8 << 3) | 2,
		_x1zz = ((VEC_C_MASK(0, 0, 2, 2) << 8) | 0x4 << 3) | 2,
		_y1zz = ((VEC_C_MASK(1, 0, 2, 2) << 8) | 0x4 << 3) | 2,
		_z1zz = ((VEC_C_MASK(2, 0, 2, 2) << 8) | 0x4 << 3) | 2,
		_w1zz = ((VEC_C_MASK(3, 0, 2, 2) << 8) | 0x4 << 3) | 2,
		_11zz = ((VEC_C_MASK(0, 0, 2, 2) << 8) | 0xc << 3) | 2,
		_1xwz = ((VEC_C_MASK(0, 0, 3, 2) << 8) | 0x8 << 3) | 2,
		_1ywz = ((VEC_C_MASK(0, 1, 3, 2) << 8) | 0x8 << 3) | 2,
		_1zwz = ((VEC_C_MASK(0, 2, 3, 2) << 8) | 0x8 << 3) | 2,
		_1wwz = ((VEC_C_MASK(0, 3, 3, 2) << 8) | 0x8 << 3) | 2,
		_x1wz = ((VEC_C_MASK(0, 0, 3, 2) << 8) | 0x4 << 3) | 2,
		_y1wz = ((VEC_C_MASK(1, 0, 3, 2) << 8) | 0x4 << 3) | 2,
		_z1wz = ((VEC_C_MASK(2, 0, 3, 2) << 8) | 0x4 << 3) | 2,
		_w1wz = ((VEC_C_MASK(3, 0, 3, 2) << 8) | 0x4 << 3) | 2,
		_11wz = ((VEC_C_MASK(0, 0, 3, 2) << 8) | 0xc << 3) | 2,
		_xx1z = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0x2 << 3) | 2,
		_yx1z = ((VEC_C_MASK(1, 0, 0, 2) << 8) | 0x2 << 3) | 2,
		_zx1z = ((VEC_C_MASK(2, 0, 0, 2) << 8) | 0x2 << 3) | 2,
		_wx1z = ((VEC_C_MASK(3, 0, 0, 2) << 8) | 0x2 << 3) | 2,
		_1x1z = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0xa << 3) | 2,
		_xy1z = ((VEC_C_MASK(0, 1, 0, 2) << 8) | 0x2 << 3) | 2,
		_yy1z = ((VEC_C_MASK(1, 1, 0, 2) << 8) | 0x2 << 3) | 2,
		_zy1z = ((VEC_C_MASK(2, 1, 0, 2) << 8) | 0x2 << 3) | 2,
		_wy1z = ((VEC_C_MASK(3, 1, 0, 2) << 8) | 0x2 << 3) | 2,
		_1y1z = ((VEC_C_MASK(0, 1, 0, 2) << 8) | 0xa << 3) | 2,
		_xz1z = ((VEC_C_MASK(0, 2, 0, 2) << 8) | 0x2 << 3) | 2,
		_yz1z = ((VEC_C_MASK(1, 2, 0, 2) << 8) | 0x2 << 3) | 2,
		_zz1z = ((VEC_C_MASK(2, 2, 0, 2) << 8) | 0x2 << 3) | 2,
		_wz1z = ((VEC_C_MASK(3, 2, 0, 2) << 8) | 0x2 << 3) | 2,
		_1z1z = ((VEC_C_MASK(0, 2, 0, 2) << 8) | 0xa << 3) | 2,
		_xw1z = ((VEC_C_MASK(0, 3, 0, 2) << 8) | 0x2 << 3) | 2,
		_yw1z = ((VEC_C_MASK(1, 3, 0, 2) << 8) | 0x2 << 3) | 2,
		_zw1z = ((VEC_C_MASK(2, 3, 0, 2) << 8) | 0x2 << 3) | 2,
		_ww1z = ((VEC_C_MASK(3, 3, 0, 2) << 8) | 0x2 << 3) | 2,
		_1w1z = ((VEC_C_MASK(0, 3, 0, 2) << 8) | 0xa << 3) | 2,
		_x11z = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0x6 << 3) | 2,
		_y11z = ((VEC_C_MASK(1, 0, 0, 2) << 8) | 0x6 << 3) | 2,
		_z11z = ((VEC_C_MASK(2, 0, 0, 2) << 8) | 0x6 << 3) | 2,
		_w11z = ((VEC_C_MASK(3, 0, 0, 2) << 8) | 0x6 << 3) | 2,
		_111z = ((VEC_C_MASK(0, 0, 0, 2) << 8) | 0xe << 3) | 2,
		_1xxw = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0x8 << 3) | 2,
		_1yxw = ((VEC_C_MASK(0, 1, 0, 3) << 8) | 0x8 << 3) | 2,
		_1zxw = ((VEC_C_MASK(0, 2, 0, 3) << 8) | 0x8 << 3) | 2,
		_1wxw = ((VEC_C_MASK(0, 3, 0, 3) << 8) | 0x8 << 3) | 2,
		_x1xw = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0x4 << 3) | 2,
		_y1xw = ((VEC_C_MASK(1, 0, 0, 3) << 8) | 0x4 << 3) | 2,
		_z1xw = ((VEC_C_MASK(2, 0, 0, 3) << 8) | 0x4 << 3) | 2,
		_w1xw = ((VEC_C_MASK(3, 0, 0, 3) << 8) | 0x4 << 3) | 2,
		_11xw = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0xc << 3) | 2,
		_1xyw = ((VEC_C_MASK(0, 0, 1, 3) << 8) | 0x8 << 3) | 2,
		_1yyw = ((VEC_C_MASK(0, 1, 1, 3) << 8) | 0x8 << 3) | 2,
		_1zyw = ((VEC_C_MASK(0, 2, 1, 3) << 8) | 0x8 << 3) | 2,
		_1wyw = ((VEC_C_MASK(0, 3, 1, 3) << 8) | 0x8 << 3) | 2,
		_x1yw = ((VEC_C_MASK(0, 0, 1, 3) << 8) | 0x4 << 3) | 2,
		_y1yw = ((VEC_C_MASK(1, 0, 1, 3) << 8) | 0x4 << 3) | 2,
		_z1yw = ((VEC_C_MASK(2, 0, 1, 3) << 8) | 0x4 << 3) | 2,
		_w1yw = ((VEC_C_MASK(3, 0, 1, 3) << 8) | 0x4 << 3) | 2,
		_11yw = ((VEC_C_MASK(0, 0, 1, 3) << 8) | 0xc << 3) | 2,
		_1xzw = ((VEC_C_MASK(0, 0, 2, 3) << 8) | 0x8 << 3) | 2,
		_1yzw = ((VEC_C_MASK(0, 1, 2, 3) << 8) | 0x8 << 3) | 2,
		_1zzw = ((VEC_C_MASK(0, 2, 2, 3) << 8) | 0x8 << 3) | 2,
		_1wzw = ((VEC_C_MASK(0, 3, 2, 3) << 8) | 0x8 << 3) | 2,
		_x1zw = ((VEC_C_MASK(0, 0, 2, 3) << 8) | 0x4 << 3) | 2,
		_y1zw = ((VEC_C_MASK(1, 0, 2, 3) << 8) | 0x4 << 3) | 2,
		_z1zw = ((VEC_C_MASK(2, 0, 2, 3) << 8) | 0x4 << 3) | 2,
		_w1zw = ((VEC_C_MASK(3, 0, 2, 3) << 8) | 0x4 << 3) | 2,
		_11zw = ((VEC_C_MASK(0, 0, 2, 3) << 8) | 0xc << 3) | 2,
		_1xww = ((VEC_C_MASK(0, 0, 3, 3) << 8) | 0x8 << 3) | 2,
		_1yww = ((VEC_C_MASK(0, 1, 3, 3) << 8) | 0x8 << 3) | 2,
		_1zww = ((VEC_C_MASK(0, 2, 3, 3) << 8) | 0x8 << 3) | 2,
		_1www = ((VEC_C_MASK(0, 3, 3, 3) << 8) | 0x8 << 3) | 2,
		_x1ww = ((VEC_C_MASK(0, 0, 3, 3) << 8) | 0x4 << 3) | 2,
		_y1ww = ((VEC_C_MASK(1, 0, 3, 3) << 8) | 0x4 << 3) | 2,
		_z1ww = ((VEC_C_MASK(2, 0, 3, 3) << 8) | 0x4 << 3) | 2,
		_w1ww = ((VEC_C_MASK(3, 0, 3, 3) << 8) | 0x4 << 3) | 2,
		_11ww = ((VEC_C_MASK(0, 0, 3, 3) << 8) | 0xc << 3) | 2,
		_xx1w = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0x2 << 3) | 2,
		_yx1w = ((VEC_C_MASK(1, 0, 0, 3) << 8) | 0x2 << 3) | 2,
		_zx1w = ((VEC_C_MASK(2, 0, 0, 3) << 8) | 0x2 << 3) | 2,
		_wx1w = ((VEC_C_MASK(3, 0, 0, 3) << 8) | 0x2 << 3) | 2,
		_1x1w = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0xa << 3) | 2,
		_xy1w = ((VEC_C_MASK(0, 1, 0, 3) << 8) | 0x2 << 3) | 2,
		_yy1w = ((VEC_C_MASK(1, 1, 0, 3) << 8) | 0x2 << 3) | 2,
		_zy1w = ((VEC_C_MASK(2, 1, 0, 3) << 8) | 0x2 << 3) | 2,
		_wy1w = ((VEC_C_MASK(3, 1, 0, 3) << 8) | 0x2 << 3) | 2,
		_1y1w = ((VEC_C_MASK(0, 1, 0, 3) << 8) | 0xa << 3) | 2,
		_xz1w = ((VEC_C_MASK(0, 2, 0, 3) << 8) | 0x2 << 3) | 2,
		_yz1w = ((VEC_C_MASK(1, 2, 0, 3) << 8) | 0x2 << 3) | 2,
		_zz1w = ((VEC_C_MASK(2, 2, 0, 3) << 8) | 0x2 << 3) | 2,
		_wz1w = ((VEC_C_MASK(3, 2, 0, 3) << 8) | 0x2 << 3) | 2,
		_1z1w = ((VEC_C_MASK(0, 2, 0, 3) << 8) | 0xa << 3) | 2,
		_xw1w = ((VEC_C_MASK(0, 3, 0, 3) << 8) | 0x2 << 3) | 2,
		_yw1w = ((VEC_C_MASK(1, 3, 0, 3) << 8) | 0x2 << 3) | 2,
		_zw1w = ((VEC_C_MASK(2, 3, 0, 3) << 8) | 0x2 << 3) | 2,
		_ww1w = ((VEC_C_MASK(3, 3, 0, 3) << 8) | 0x2 << 3) | 2,
		_1w1w = ((VEC_C_MASK(0, 3, 0, 3) << 8) | 0xa << 3) | 2,
		_x11w = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0x6 << 3) | 2,
		_y11w = ((VEC_C_MASK(1, 0, 0, 3) << 8) | 0x6 << 3) | 2,
		_z11w = ((VEC_C_MASK(2, 0, 0, 3) << 8) | 0x6 << 3) | 2,
		_w11w = ((VEC_C_MASK(3, 0, 0, 3) << 8) | 0x6 << 3) | 2,
		_111w = ((VEC_C_MASK(0, 0, 0, 3) << 8) | 0xe << 3) | 2,
		_xxx1 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x1 << 3) | 2,
		_yxx1 = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x1 << 3) | 2,
		_zxx1 = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x1 << 3) | 2,
		_wxx1 = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x1 << 3) | 2,
		_1xx1 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x9 << 3) | 2,
		_xyx1 = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0x1 << 3) | 2,
		_yyx1 = ((VEC_C_MASK(1, 1, 0, 0) << 8) | 0x1 << 3) | 2,
		_zyx1 = ((VEC_C_MASK(2, 1, 0, 0) << 8) | 0x1 << 3) | 2,
		_wyx1 = ((VEC_C_MASK(3, 1, 0, 0) << 8) | 0x1 << 3) | 2,
		_1yx1 = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0x9 << 3) | 2,
		_xzx1 = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0x1 << 3) | 2,
		_yzx1 = ((VEC_C_MASK(1, 2, 0, 0) << 8) | 0x1 << 3) | 2,
		_zzx1 = ((VEC_C_MASK(2, 2, 0, 0) << 8) | 0x1 << 3) | 2,
		_wzx1 = ((VEC_C_MASK(3, 2, 0, 0) << 8) | 0x1 << 3) | 2,
		_1zx1 = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0x9 << 3) | 2,
		_xwx1 = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0x1 << 3) | 2,
		_ywx1 = ((VEC_C_MASK(1, 3, 0, 0) << 8) | 0x1 << 3) | 2,
		_zwx1 = ((VEC_C_MASK(2, 3, 0, 0) << 8) | 0x1 << 3) | 2,
		_wwx1 = ((VEC_C_MASK(3, 3, 0, 0) << 8) | 0x1 << 3) | 2,
		_1wx1 = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0x9 << 3) | 2,
		_x1x1 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x5 << 3) | 2,
		_y1x1 = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x5 << 3) | 2,
		_z1x1 = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x5 << 3) | 2,
		_w1x1 = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x5 << 3) | 2,
		_11x1 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0xd << 3) | 2,
		_xxy1 = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0x1 << 3) | 2,
		_yxy1 = ((VEC_C_MASK(1, 0, 1, 0) << 8) | 0x1 << 3) | 2,
		_zxy1 = ((VEC_C_MASK(2, 0, 1, 0) << 8) | 0x1 << 3) | 2,
		_wxy1 = ((VEC_C_MASK(3, 0, 1, 0) << 8) | 0x1 << 3) | 2,
		_1xy1 = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0x9 << 3) | 2,
		_xyy1 = ((VEC_C_MASK(0, 1, 1, 0) << 8) | 0x1 << 3) | 2,
		_yyy1 = ((VEC_C_MASK(1, 1, 1, 0) << 8) | 0x1 << 3) | 2,
		_zyy1 = ((VEC_C_MASK(2, 1, 1, 0) << 8) | 0x1 << 3) | 2,
		_wyy1 = ((VEC_C_MASK(3, 1, 1, 0) << 8) | 0x1 << 3) | 2,
		_1yy1 = ((VEC_C_MASK(0, 1, 1, 0) << 8) | 0x9 << 3) | 2,
		_xzy1 = ((VEC_C_MASK(0, 2, 1, 0) << 8) | 0x1 << 3) | 2,
		_yzy1 = ((VEC_C_MASK(1, 2, 1, 0) << 8) | 0x1 << 3) | 2,
		_zzy1 = ((VEC_C_MASK(2, 2, 1, 0) << 8) | 0x1 << 3) | 2,
		_wzy1 = ((VEC_C_MASK(3, 2, 1, 0) << 8) | 0x1 << 3) | 2,
		_1zy1 = ((VEC_C_MASK(0, 2, 1, 0) << 8) | 0x9 << 3) | 2,
		_xwy1 = ((VEC_C_MASK(0, 3, 1, 0) << 8) | 0x1 << 3) | 2,
		_ywy1 = ((VEC_C_MASK(1, 3, 1, 0) << 8) | 0x1 << 3) | 2,
		_zwy1 = ((VEC_C_MASK(2, 3, 1, 0) << 8) | 0x1 << 3) | 2,
		_wwy1 = ((VEC_C_MASK(3, 3, 1, 0) << 8) | 0x1 << 3) | 2,
		_1wy1 = ((VEC_C_MASK(0, 3, 1, 0) << 8) | 0x9 << 3) | 2,
		_x1y1 = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0x5 << 3) | 2,
		_y1y1 = ((VEC_C_MASK(1, 0, 1, 0) << 8) | 0x5 << 3) | 2,
		_z1y1 = ((VEC_C_MASK(2, 0, 1, 0) << 8) | 0x5 << 3) | 2,
		_w1y1 = ((VEC_C_MASK(3, 0, 1, 0) << 8) | 0x5 << 3) | 2,
		_11y1 = ((VEC_C_MASK(0, 0, 1, 0) << 8) | 0xd << 3) | 2,
		_xxz1 = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0x1 << 3) | 2,
		_yxz1 = ((VEC_C_MASK(1, 0, 2, 0) << 8) | 0x1 << 3) | 2,
		_zxz1 = ((VEC_C_MASK(2, 0, 2, 0) << 8) | 0x1 << 3) | 2,
		_wxz1 = ((VEC_C_MASK(3, 0, 2, 0) << 8) | 0x1 << 3) | 2,
		_1xz1 = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0x9 << 3) | 2,
		_xyz1 = ((VEC_C_MASK(0, 1, 2, 0) << 8) | 0x1 << 3) | 2,
		_yyz1 = ((VEC_C_MASK(1, 1, 2, 0) << 8) | 0x1 << 3) | 2,
		_zyz1 = ((VEC_C_MASK(2, 1, 2, 0) << 8) | 0x1 << 3) | 2,
		_wyz1 = ((VEC_C_MASK(3, 1, 2, 0) << 8) | 0x1 << 3) | 2,
		_1yz1 = ((VEC_C_MASK(0, 1, 2, 0) << 8) | 0x9 << 3) | 2,
		_xzz1 = ((VEC_C_MASK(0, 2, 2, 0) << 8) | 0x1 << 3) | 2,
		_yzz1 = ((VEC_C_MASK(1, 2, 2, 0) << 8) | 0x1 << 3) | 2,
		_zzz1 = ((VEC_C_MASK(2, 2, 2, 0) << 8) | 0x1 << 3) | 2,
		_wzz1 = ((VEC_C_MASK(3, 2, 2, 0) << 8) | 0x1 << 3) | 2,
		_1zz1 = ((VEC_C_MASK(0, 2, 2, 0) << 8) | 0x9 << 3) | 2,
		_xwz1 = ((VEC_C_MASK(0, 3, 2, 0) << 8) | 0x1 << 3) | 2,
		_ywz1 = ((VEC_C_MASK(1, 3, 2, 0) << 8) | 0x1 << 3) | 2,
		_zwz1 = ((VEC_C_MASK(2, 3, 2, 0) << 8) | 0x1 << 3) | 2,
		_wwz1 = ((VEC_C_MASK(3, 3, 2, 0) << 8) | 0x1 << 3) | 2,
		_1wz1 = ((VEC_C_MASK(0, 3, 2, 0) << 8) | 0x9 << 3) | 2,
		_x1z1 = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0x5 << 3) | 2,
		_y1z1 = ((VEC_C_MASK(1, 0, 2, 0) << 8) | 0x5 << 3) | 2,
		_z1z1 = ((VEC_C_MASK(2, 0, 2, 0) << 8) | 0x5 << 3) | 2,
		_w1z1 = ((VEC_C_MASK(3, 0, 2, 0) << 8) | 0x5 << 3) | 2,
		_11z1 = ((VEC_C_MASK(0, 0, 2, 0) << 8) | 0xd << 3) | 2,
		_xxw1 = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0x1 << 3) | 2,
		_yxw1 = ((VEC_C_MASK(1, 0, 3, 0) << 8) | 0x1 << 3) | 2,
		_zxw1 = ((VEC_C_MASK(2, 0, 3, 0) << 8) | 0x1 << 3) | 2,
		_wxw1 = ((VEC_C_MASK(3, 0, 3, 0) << 8) | 0x1 << 3) | 2,
		_1xw1 = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0x9 << 3) | 2,
		_xyw1 = ((VEC_C_MASK(0, 1, 3, 0) << 8) | 0x1 << 3) | 2,
		_yyw1 = ((VEC_C_MASK(1, 1, 3, 0) << 8) | 0x1 << 3) | 2,
		_zyw1 = ((VEC_C_MASK(2, 1, 3, 0) << 8) | 0x1 << 3) | 2,
		_wyw1 = ((VEC_C_MASK(3, 1, 3, 0) << 8) | 0x1 << 3) | 2,
		_1yw1 = ((VEC_C_MASK(0, 1, 3, 0) << 8) | 0x9 << 3) | 2,
		_xzw1 = ((VEC_C_MASK(0, 2, 3, 0) << 8) | 0x1 << 3) | 2,
		_yzw1 = ((VEC_C_MASK(1, 2, 3, 0) << 8) | 0x1 << 3) | 2,
		_zzw1 = ((VEC_C_MASK(2, 2, 3, 0) << 8) | 0x1 << 3) | 2,
		_wzw1 = ((VEC_C_MASK(3, 2, 3, 0) << 8) | 0x1 << 3) | 2,
		_1zw1 = ((VEC_C_MASK(0, 2, 3, 0) << 8) | 0x9 << 3) | 2,
		_xww1 = ((VEC_C_MASK(0, 3, 3, 0) << 8) | 0x1 << 3) | 2,
		_yww1 = ((VEC_C_MASK(1, 3, 3, 0) << 8) | 0x1 << 3) | 2,
		_zww1 = ((VEC_C_MASK(2, 3, 3, 0) << 8) | 0x1 << 3) | 2,
		_www1 = ((VEC_C_MASK(3, 3, 3, 0) << 8) | 0x1 << 3) | 2,
		_1ww1 = ((VEC_C_MASK(0, 3, 3, 0) << 8) | 0x9 << 3) | 2,
		_x1w1 = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0x5 << 3) | 2,
		_y1w1 = ((VEC_C_MASK(1, 0, 3, 0) << 8) | 0x5 << 3) | 2,
		_z1w1 = ((VEC_C_MASK(2, 0, 3, 0) << 8) | 0x5 << 3) | 2,
		_w1w1 = ((VEC_C_MASK(3, 0, 3, 0) << 8) | 0x5 << 3) | 2,
		_11w1 = ((VEC_C_MASK(0, 0, 3, 0) << 8) | 0xd << 3) | 2,
		_xx11 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x3 << 3) | 2,
		_yx11 = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x3 << 3) | 2,
		_zx11 = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x3 << 3) | 2,
		_wx11 = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x3 << 3) | 2,
		_1x11 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0xb << 3) | 2,
		_xy11 = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0x3 << 3) | 2,
		_yy11 = ((VEC_C_MASK(1, 1, 0, 0) << 8) | 0x3 << 3) | 2,
		_zy11 = ((VEC_C_MASK(2, 1, 0, 0) << 8) | 0x3 << 3) | 2,
		_wy11 = ((VEC_C_MASK(3, 1, 0, 0) << 8) | 0x3 << 3) | 2,
		_1y11 = ((VEC_C_MASK(0, 1, 0, 0) << 8) | 0xb << 3) | 2,
		_xz11 = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0x3 << 3) | 2,
		_yz11 = ((VEC_C_MASK(1, 2, 0, 0) << 8) | 0x3 << 3) | 2,
		_zz11 = ((VEC_C_MASK(2, 2, 0, 0) << 8) | 0x3 << 3) | 2,
		_wz11 = ((VEC_C_MASK(3, 2, 0, 0) << 8) | 0x3 << 3) | 2,
		_1z11 = ((VEC_C_MASK(0, 2, 0, 0) << 8) | 0xb << 3) | 2,
		_xw11 = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0x3 << 3) | 2,
		_yw11 = ((VEC_C_MASK(1, 3, 0, 0) << 8) | 0x3 << 3) | 2,
		_zw11 = ((VEC_C_MASK(2, 3, 0, 0) << 8) | 0x3 << 3) | 2,
		_ww11 = ((VEC_C_MASK(3, 3, 0, 0) << 8) | 0x3 << 3) | 2,
		_1w11 = ((VEC_C_MASK(0, 3, 0, 0) << 8) | 0xb << 3) | 2,
		_x111 = ((VEC_C_MASK(0, 0, 0, 0) << 8) | 0x7 << 3) | 2,
		_y111 = ((VEC_C_MASK(1, 0, 0, 0) << 8) | 0x7 << 3) | 2,
		_z111 = ((VEC_C_MASK(2, 0, 0, 0) << 8) | 0x7 << 3) | 2,
		_w111 = ((VEC_C_MASK(3, 0, 0, 0) << 8) | 0x7 << 3) | 2,
	};

	enum InsertMask
	{
		_0000 = 0,
		_0001 = 1,
		_0010 = 2,
		_0011 = 3,

		_0100 = 4,
		_0101 = 5,
		_0110 = 6,
		_0111 = 7,

		_1000 = 8,
		_1001 = 9,
		_1010 = 10,
		_1011 = 11,

		_1100 = 12,
		_1101 = 13,
		_1110 = 14,
		_1111 = 15,
	};
};

template<VecMask::Mask T>
FORCEINLINE Vec vecPermute(Vec a, Vec b)
{
	Vec ret;
	ret = vmovq_n_f32(vgetq_lane_f32((a), ((T >> 8) >> 6) & 0x3));
	ret = vsetq_lane_f32(vgetq_lane_f32((a), ((T >> 8) >> 4) & 0x3), ret, 1);
	ret = vsetq_lane_f32(vgetq_lane_f32((b), ((T >> 8) >> 2) & 0x3), ret, 2);
	ret = vsetq_lane_f32(vgetq_lane_f32((b), ((T >> 8) >> 0) & 0x3), ret, 3);
	return ret;
}

template<VecMask::Mask T>
FORCEINLINE Vec vecPermute(Vec x, Vec y, Vec z, Vec w)
{
	Vec ret;
	ret = vmovq_n_f32(vgetq_lane_f32((x), ((T >> 8) >> 6) & 0x3));
	ret = vsetq_lane_f32(vgetq_lane_f32((y), ((T >> 8) >> 4) & 0x3), ret, 1);
	ret = vsetq_lane_f32(vgetq_lane_f32((z), ((T >> 8) >> 2) & 0x3), ret, 2);
	ret = vsetq_lane_f32(vgetq_lane_f32((w), ((T >> 8) >> 0) & 0x3), ret, 3);
	return ret;
}

template<VecMask::Mask T>
FORCEINLINE Vec vecShuffle(Vec v)
{
	Vec ret = vecZero();

	switch (T & 0x7)
	{
	case 0 :
		{
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 6) & 0x3), ret, 0);
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 4) & 0x3), ret, 1);
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 2) & 0x3), ret, 2);
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 0) & 0x3), ret, 3);
			break;
		}

	case 1 :
		{
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 6) & 0x3), ret, 0);
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 4) & 0x3), ret, 1);
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 2) & 0x3), ret, 2);
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 0) & 0x3), ret, 3);

			if ((((T >> 3) & 0xf) >> 3) & 1)
				ret = vsetq_lane_f32(0.0, ret, 0);

			if ((((T >> 3) & 0xf) >> 2) & 1)
				ret = vsetq_lane_f32(0.0, ret, 1);

			if ((((T >> 3) & 0xf) >> 1) & 1)
				ret = vsetq_lane_f32(0.0, ret, 2);

			if ((((T >> 3) & 0xf) >> 0) & 1)
				ret = vsetq_lane_f32(0.0, ret, 3);

			break;
		}

	case 2 :
		{
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 6) & 0x3), ret, 0);
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 4) & 0x3), ret, 1);
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 2) & 0x3), ret, 2);
			ret = vsetq_lane_f32(vgetq_lane_f32(v, ((T >> 8) >> 0) & 0x3), ret, 3);

			if ((((T >> 3) & 0xf) >> 3) & 1)
				ret = vsetq_lane_f32(1.0, ret, 0);

			if ((((T >> 3) & 0xf) >> 2) & 1)
				ret = vsetq_lane_f32(1.0, ret, 1);

			if ((((T >> 3) & 0xf) >> 1) & 1)
				ret = vsetq_lane_f32(1.0, ret, 2);

			if ((((T >> 3) & 0xf) >> 0) & 1)
				ret = vsetq_lane_f32(1.0, ret, 3);

			break;
		}
	default:
		break;
	}

	return ret;
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_xxxx>(Vec v)
{
	return vdupq_lane_f32(vget_low_f32(v), 0);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_yyyy>(Vec v)
{
	return vdupq_lane_f32(vget_low_f32(v), 1);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_zzzz>(Vec v)
{
	return vdupq_lane_f32(vget_high_f32(v), 0);
}

template<> FORCEINLINE Vec vecShuffle<VecMask::_wwww>(Vec v)
{
	return vdupq_lane_f32(vget_high_f32(v), 1);
}

template<VecMask::InsertMask T>
FORCEINLINE Vec vecInsert(Vec v1, Vec v2)
{
	Vec returnValue;
	returnValue = v1;

	if (T & 8)
		returnValue = vsetq_lane_f32(vgetq_lane_f32(v2, 0), returnValue, 0);
	if (T & 4)
		returnValue = vsetq_lane_f32(vgetq_lane_f32(v2, 1), returnValue, 1);
	if (T & 2)
		returnValue = vsetq_lane_f32(vgetq_lane_f32(v2, 2), returnValue, 2);
	if (T & 1)
		returnValue = vsetq_lane_f32(vgetq_lane_f32(v2, 3), returnValue, 3);

	return returnValue;
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_0000>(Vec v1, Vec v2)
{
	return v1;
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_0001>(Vec v1, Vec v2)
{
	const uint32x4_t zero = vdupq_n_u32(0);
	const uint32x4_t insertMask = vsetq_lane_u32(0xFFFFFFFF, zero, 3);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_0010>(Vec v1, Vec v2)
{
	const uint32x4_t zero = vdupq_n_u32(0);
	const uint32x4_t insertMask = vsetq_lane_u32(0xFFFFFFFF, zero, 2);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_0011>(Vec v1, Vec v2)
{
	uint32x4_t insertMask;
	INTERNAL_MASK_SET(insertMask, 0xFFFFFFFF, 0xFFFFFFFF, 2, 3);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_0100>(Vec v1, Vec v2)
{
	const uint32x4_t zero = vdupq_n_u32(0);
	const uint32x4_t insertMask = vsetq_lane_u32(0xFFFFFFFF, zero, 1);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_0101>(Vec v1, Vec v2)
{
	uint32x4_t insertMask;
	INTERNAL_MASK_SET(insertMask, 0xFFFFFFFF, 0xFFFFFFFF, 1, 3);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_0110>(Vec v1, Vec v2)
{
	uint32x4_t insertMask;
	INTERNAL_MASK_SET(insertMask, 0xFFFFFFFF, 0xFFFFFFFF, 1, 2);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_0111>(Vec v1, Vec v2)
{
	uint32x4_t insertMask;
	INTERNAL_MASK_LOAD(insertMask, 0, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_1000>(Vec v1, Vec v2)
{
	const uint32x4_t zero = vdupq_n_u32(0);
	const uint32x4_t insertMask = vsetq_lane_u32(0xFFFFFFFF, zero, 0);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_1001>(Vec v1, Vec v2)
{
	uint32x4_t insertMask;
	INTERNAL_MASK_SET(insertMask, 0xFFFFFFFF, 0xFFFFFFFF, 0, 3);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_1010>(Vec v1, Vec v2)
{
	uint32x4_t insertMask;
	INTERNAL_MASK_SET(insertMask, 0xFFFFFFFF, 0xFFFFFFFF, 0, 2);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_1011>(Vec v1, Vec v2)
{
	uint32x4_t insertMask;
	INTERNAL_MASK_LOAD(insertMask, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_1100>(Vec v1, Vec v2)
{
	uint32x4_t insertMask;
	INTERNAL_MASK_SET(insertMask, 0xFFFFFFFF, 0xFFFFFFFF, 0, 1);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_1101>(Vec v1, Vec v2)
{
	uint32x4_t insertMask;
	INTERNAL_MASK_LOAD(insertMask, 0xFFFFFFFF, 0xFFFFFFFF, 0, 0xFFFFFFFF);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_1110>(Vec v1, Vec v2)
{
	uint32x4_t insertMask;
	INTERNAL_MASK_LOAD(insertMask, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0);
	return vbslq_f32(insertMask, v2, v1);
}

template<>
FORCEINLINE Vec vecInsert<VecMask::_1111>(Vec v1, Vec v2)
{
	return v2;
}

template<int bits>
FORCEINLINE IntVec vecShiftLeftLogical(IntVec v)
{
	return (int32x4_t)vshlq_n_u32((uint32x4_t)v, bits);
}

template<int bits>
FORCEINLINE IntVec vecShiftRightLogical(IntVec v)
{
	return (int32x4_t)vshrq_n_u32((uint32x4_t)v, bits);
}

template<int bits>
FORCEINLINE IntVec vecShiftRightArithmetic(IntVec v)
{
	return vshrq_n_s32(v, bits);
}

FORCEINLINE IntVec vecPackU32ToU16(IntVec a)
{
	uint16x4_t w = vqmovun_s32(a);
	return vreinterpretq_s32_u16(vcombine_u16(w, w));
}

FORCEINLINE IntVec vecPackU32ToU16(IntVec a, IntVec b)
{
	return vreinterpretq_s32_u16(vcombine_u16(vqmovun_s32(a), vqmovun_s32(b)));
}

FORCEINLINE IntVec vecPackS32ToS16(IntVec a)
{
	int16x4_t w = vqmovn_s32(a);
	return vreinterpretq_s32_s16(vcombine_s16(w, w));
}

FORCEINLINE IntVec vecPackS32ToS16(IntVec a, IntVec b)
{
	return vreinterpretq_s32_s16(vcombine_s16(vqmovn_s32(a), vqmovn_s32(b)));
}

FORCEINLINE IntVec vecUnpackU16ToU32(IntVec a)
{
	return vreinterpretq_s64_s16(vzip1q_s16(vreinterpretq_s16_s64(a), vdupq_n_s16(0)));
}

FORCEINLINE IntVec vecUnpackS16ToS32(IntVec a)
{
	return vmovl_s16(vget_low_s16(vreinterpretq_s16_s32(a)));
}
