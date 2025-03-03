#pragma once

#if defined(__aarch64__) || defined(_M_ARM64) 
	#include "vecMathNeon.h"
#elif defined(__x86_64__) || defined(_M_X64) 
	#include "vecMathSse.h"
#endif

FORCEINLINE Vec vecMathQuaternionInverse(Vec q)
{
	// s = 1.0f / Dot(q, q);
	Vec s = vec(vecRecip(vecDot4(q, q)));

	// conj(q) * s;
	Vec v = vecXor(q, vecSignMaskXYZ());
	return vecMul(v, s);
}

FORCEINLINE Vec vecMathQuaternionTransformVec3(Vec q, Vec v)
{
	Vec qW = vecShuffle<VecMask::_wwww>(q);
	Vec q_cross_v = vecCross(q, v);
	Vec ret = vecMulAdd(qW, v, q_cross_v);
	ret = vecCross(q, ret);
	ret = vecMulAdd(vecTwo, ret, v);
	return ret;
}

FORCEINLINE Vec vecMathQuaternionMul(Vec a, Vec b)
{
	Vec negA = vecNeg(b);
	Vec xxxx = vecShuffle<VecMask::_xxxx>(a);
	Vec yyyy = vecShuffle<VecMask::_yyyy>(a);
	Vec zzzz = vecShuffle<VecMask::_zzzz>(a);
	Vec wwww = vecShuffle<VecMask::_wwww>(a);

	Vec wcya = vecPermute<VecMask::_wyzx>(b, negA);
	Vec zwab = vecPermute<VecMask::_zwxy>(b, negA);
	wcya = vecShuffle<VecMask::_xzyw>(wcya);
	Vec bxwc = vecPermute<VecMask::_yzxw>(negA, b);
	bxwc = vecShuffle<VecMask::_xzwy>(bxwc);

	Vec res = vecMul(xxxx, wcya);
	res = vecMulAdd(yyyy, zwab, res);
	res = vecMulAdd(zzzz, bxwc, res);
	res = vecMulAdd(wwww, b, res);

	return res;
}

FORCEINLINE void vecNormalizingQuatToMatrix(Vec& xAxis, Vec& yAxis, Vec& zAxis, Vec q)
{
	// Explanation of the math: https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation#Quaternion-derived_rotation_matrix
	Vec xyz_2 = vecMul(vecAdd(q, q), vec(vecRecip(vecDot4(q, q))));
	Vec www = vecShuffle<VecMask::_wwww>(q);
	Vec yzx = vecShuffle<VecMask::_yzxy>(q);
	Vec zxy = vecShuffle<VecMask::_zxyz>(q);
	Vec yzx_2 = vecShuffle<VecMask::_yzxy>(xyz_2);
	Vec zxy_2 = vecShuffle<VecMask::_zxyz>(xyz_2);

	Vec tmp0 = vecMul(yzx_2, www);
	Vec tmp1 = vecSub(vecOne, vecMul(yzx, yzx_2));
	Vec tmp2 = vecMul(yzx, xyz_2);
	tmp0 = vecMulAdd(zxy, xyz_2, tmp0);
	tmp1 = vecSub(tmp1, vecMul(zxy, zxy_2));
	tmp2 = vecSub(tmp2, vecMul(zxy_2, www));
	Vec tmp3 = vecInsert<VecMask::_1000>(tmp0, tmp1);
	Vec tmp4 = vecInsert<VecMask::_1000>(tmp1, tmp2);
	Vec tmp5 = vecInsert<VecMask::_1000>(tmp2, tmp0);

	xAxis = vecInsert<VecMask::_0011>(tmp3, tmp2);
	yAxis = vecInsert<VecMask::_0011>(tmp4, tmp0);
	zAxis = vecInsert<VecMask::_0011>(tmp5, tmp1);
}

FORCEINLINE Vec vecMathMatrixToQuaternion(Vec u, Vec v, Vec w)
{
	IntVec sign_mask = intVec(0x80000000);

	Vec u_x = vecShuffle<VecMask::_xxxx>(u);
	Vec v_y = vecShuffle<VecMask::_yyyy>(v);
	Vec w_z = vecShuffle<VecMask::_zzzz>(w);

	IntVec u_sign = vecAnd(vecCastToIntVec(u_x), sign_mask);
	IntVec u_mask = vecCmpEQ(u_sign, sign_mask);

	Vec t = vecAdd(v_y,
					intVecCastToVec(vecXor(vecCastToIntVec(w_z), u_sign)));
	IntVec t_mask = vecCmpEQ(vecAnd(vecCastToIntVec(t), sign_mask), sign_mask);

	Vec tr = vecAdd(vecOne, 
					intVecCastToVec(vecXor(vecCastToIntVec(u_x), u_sign)));

	IntVec sign_flips =	vecXor(						intVec(0x00000000, 0x80000000, 0x80000000, 0x80000000),
							vecXor( vecAnd(u_mask, intVec(0x00000000, 0x80000000, 0x00000000, 0x80000000)),
									vecAnd(t_mask, intVec(0x80000000, 0x80000000, 0x80000000, 0x00000000))));

	Vec a = vecPermute<VecMask::_xyxz>(tr, u, w, v); // float4(tr, u.y, w.x, v.z)
	Vec b = vecPermute<VecMask::_xxzy>(t,  v, u, w); // float4(t,  v.x, u.z, w.y)

	Vec res = vecAdd(a, 
					intVecCastToVec(vecXor(vecCastToIntVec(b), sign_flips)));// +---, +++-, ++-+, +-++

	IntVec intRes = vecCastToIntVec(res);

	intRes = vecOr(	vecAndNot(u_mask, intRes),
					vecAnd(u_mask, vecCastToIntVec(vecShuffle<VecMask::_zwxy>(intVecCastToVec(intRes)))));
	intRes = vecOr(	vecAndNot(t_mask, vecCastToIntVec(vecShuffle<VecMask::_wzyx>(intVecCastToVec(intRes)))),
					vecAnd(t_mask, intRes));

	res = intVecCastToVec(intRes);
	// do normalization
	Vec invNorm = vec(vecRecipSqrt(vecDot4(res, res)));
	return vecMul(res, invNorm); 
}

FORCEINLINE void vecTranspose3x3(Vec& outX, Vec& outY, Vec& outZ, Vec inX, Vec inY, Vec inZ)
{
	const Vec xyXY = vecPermute<VecMask::_xyxy>(inX, inY); // [m00 m01 m10 m11]
	const Vec xyZW = vecPermute<VecMask::_zwzw>(inX, inY); // [m02 m03 m12 m13]
	outX = vecPermute<VecMask::_xzxw>(xyXY, inZ); // [m00, m10, m20, m23]
	outY = vecPermute<VecMask::_ywyw>(xyXY, inZ); // [m01, m11, m21, m23]
	outZ = vecPermute<VecMask::_xzzw>(xyZW, inZ); // [m02, m12, m22, m23]
}

FORCEINLINE void vecTranspose4x4(Vec& outX, Vec& outY, Vec& outZ, Vec& outW,
											Vec inX, Vec inY, Vec inZ, Vec inW)
{
	Vec xyXZ = vecMergeHighWord(inX, inZ);
	Vec xyYW = vecMergeHighWord(inY, inW);

	Vec zwXZ = vecMergeLowWord(inX, inZ);
	Vec zwYW = vecMergeLowWord(inY, inW);

	outX = vecMergeHighWord(xyXZ, xyYW);
	outY = vecMergeLowWord(xyXZ, xyYW);
	outZ = vecMergeHighWord(zwXZ, zwYW);
	outW = vecMergeLowWord(zwXZ, zwYW);
}

FORCEINLINE void vecMatrix33Inverse(Vec& outX, Vec& outY, Vec& outZ,
										Vec inX, Vec inY, Vec inZ)
{
	Vec crossYZ = vecCross(inY, inZ);
	Vec crossZX = vecCross(inZ, inX);
	Vec crossXY = vecCross(inX, inY);

	Vec transposeX, transposeY, transposeZ;
	vecTranspose3x3(transposeX, transposeY, transposeZ, crossYZ, crossZX, crossXY);

	Vec1 det = vecDot3(inX, crossYZ);
	Vec recipDet = vec(vecRecip(det));

	outX = vecMul(transposeX, recipDet);
	outY = vecMul(transposeY, recipDet);
	outZ = vecMul(transposeZ, recipDet);
}

FORCEINLINE void vecLinearTransformNoScaleInverse(Vec& outX, Vec& outY, Vec& outZ, Vec& outW,
															Vec inX, Vec inY, Vec inZ, Vec inW)
{
	Vec transposeX, transposeY, transposeZ;
	vecTranspose3x3(transposeX, transposeY, transposeZ, inX, inY, inZ);

	const Vec negateTrans = vecSub(vecZero(), inW);

	// [-Trans * transposeX, -Trans * tranposeY, -Trans * transposeZ]
	const Vec X = vecShuffle<VecMask::_xxxx>(negateTrans);
	const Vec Y = vecShuffle<VecMask::_yyyy>(negateTrans);
	const Vec Z = vecShuffle<VecMask::_zzzz>(negateTrans);

	Vec inverseTrans = vecMul(transposeX, X);
	inverseTrans = vecMulAdd(transposeY, Y, inverseTrans);
	inverseTrans = vecMulAdd(transposeZ, Z, inverseTrans);

	outX = transposeX;
	outY = transposeY;
	outZ = transposeZ;
	outW = inverseTrans;
}

FORCEINLINE void vecLinearTransformInverse(Vec& outX, Vec& outY, Vec& outZ, Vec& outW,
													 Vec inX, Vec inY, Vec inZ, Vec inW)
{
	Vec inverseX, inverseY, inverseZ;
	vecMatrix33Inverse(inverseX, inverseY, inverseZ, inX, inY, inZ);

	const Vec negateTrans = vecSub(vecZero(), inW);

	// [-Trans * inverseX, -Trans * inverseY, -Trans * inverseZ]
	const Vec X = vecShuffle<VecMask::_xxxx>(negateTrans);
	const Vec Y = vecShuffle<VecMask::_yyyy>(negateTrans);
	const Vec Z = vecShuffle<VecMask::_zzzz>(negateTrans);

	Vec inverseTrans = vecMul(inverseX, X);
	inverseTrans = vecMulAdd(inverseY, Y, inverseTrans);
	inverseTrans = vecMulAdd(inverseZ, Z, inverseTrans);

	outX = inverseX;
	outY = inverseY;
	outZ = inverseZ;
	outW = inverseTrans;
}

FORCEINLINE void vecMatrix44Inverse(Vec& outX, Vec& outY, Vec& outZ, Vec& outW,
											  Vec inX, Vec inY, Vec inZ, Vec inW)
{
	// We do 2x2 Sub Block Matrix Inverse on the incoming 4x4 Matrix
	// https://en.wikipedia.org/wiki/Determinant#Relation_to_eigenvalues_and_trace
	// https://en.wikipedia.org/wiki/Invertible_matrix#Blockwise_inversion
	// Terminology:
	// |M| = determinant of Matrix M
	// 4x4 Matrix M can be represented as 4 2x2 sub matrices, which we call A B C D.
	// M = (A B | C D), from top to bottom and left to right; | denotes a new row in the matrix M
	// adjugate(A) = adj(A) = A#, for some 2x2 sub matrix A
	// trace(AB) = tr(AB) = AxBx + AyBz + AzBy + AwBw, for some 2x2 sub matrix A an B
	// inverse(A) = A^ = 1 / |A| * A#, for some 2x2 sub matrix A
	// We store a 2x2 matrix in a single vector register, left to right, top to botom so
	// x component is the top left and w component is the bottom right
	//
	// Properties:
	// Adjugate of 2x2 sub Matrices:
	// (AB)# = B#A#, (A#)# = A, (cA)# = cA#
	// Determinnt of 2x2 sub Matrices:
	// |A| = AxBw - AyBz, |-A| = |A|, |AB| = |A||B|, |A + B| = |A| + |B| + tr(A#B)
	// Trace of 2x2 sub Matrices:
	// tr(AB) = tr(BA), tr(-A) = -tr(A)
	//
	// For M = (A B | C D), |M| = |AD - BC| = |A||D| + |B||C| - tr(A#B * D#C)
	// Let (A B | C D)^ = (X Y | Z W)
	// M^ = (X Y | Z W) = 1 / |M| * ( (|D|A - B(D#C))#  (|B|C - D(A#B)#)# )
	//										( (|C|B - A(D#C)#)# (|A|D - C(A#B))#  )
	// NOTE: Notice that the |M| and calculations for X Y Z W, have been derived to use A#B and D#C wherever possible,
	//		 to save on register usage

	Vec detA, detB, detC, detD;
	{
		// detAll = (|A| |B| |C| |D|)
		const Vec v0 = vecPermute<VecMask::_xzxz>(inX, inZ);
		const Vec v2 = vecPermute<VecMask::_ywyw>(inX, inZ);
		const Vec v1 = vecPermute<VecMask::_ywyw>(inY, inW);
		const Vec v3 = vecPermute<VecMask::_xzxz>(inY, inW);
		Vec detAll = vecSub(vecMul(v0, v1), vecMul(v2, v3));

		detA = vecShuffle<VecMask::_xxxx>(detAll);
		detB = vecShuffle<VecMask::_yyyy>(detAll);
		detC = vecShuffle<VecMask::_zzzz>(detAll);
		detD = vecShuffle<VecMask::_wwww>(detAll);
	}

	// 2x2 sub block matrices
	Vec A = vecPermute<VecMask::_xyxy>(inX, inY);
	Vec B = vecPermute<VecMask::_zwzw>(inX, inY);
	Vec C = vecPermute<VecMask::_xyxy>(inZ, inW);
	Vec D = vecPermute<VecMask::_zwzw>(inZ, inW);

	// X Y Z W are the 2x2 inverse of A B C D; before determinant mul
	Vec adjDC, adjAB, X, Y, Z, W, detM;
	adjDC = vecSub(vecMul(vecShuffle<VecMask::_wwxx>(D), C),
					vecMul(vecShuffle<VecMask::_yyzz>(D), vecShuffle<VecMask::_zwxy>(C)));
	adjAB = vecSub(vecMul(vecShuffle<VecMask::_wwxx>(A), B),
					vecMul(vecShuffle<VecMask::_yyzz>(A), vecShuffle<VecMask::_zwxy>(B)));

	{
		const Vec adjDCShuf = vecShuffle<VecMask::_zyzy>(adjDC);
		// (|D|A - B(D#C))
		Vec temp0 = vecAdd(vecMul(B, vecShuffle<VecMask::_xwxw>(adjDC)),
							vecMul(vecShuffle<VecMask::_yxwz>(B), adjDCShuf));
		X = vecSub(vecMul(detD, A), temp0);

		// (|C|B - A(D#C)#)
		Vec temp3 = vecSub(vecMul(A, vecShuffle<VecMask::_wxwx>(adjDC)),
							vecMul(vecShuffle<VecMask::_yxwz>(A), adjDCShuf));
		Z = vecSub(vecMul(detC, B), temp3);

		const Vec adjABShuf = vecShuffle<VecMask::_zyzy>(adjAB);
		// (|A|D - C(A#B))
		Vec temp1 = vecAdd(vecMul(C, vecShuffle<VecMask::_xwxw>(adjAB)),
							vecMul(vecShuffle<VecMask::_yxwz>(C), adjABShuf));
		W = vecSub(vecMul(detA, D), temp1);

		// (|B|C - D(A#B)#)
		Vec temp2 = vecSub(vecMul(D, vecShuffle<VecMask::_wxwx>(adjAB)),
							vecMul(vecShuffle<VecMask::_yxwz>(D), adjABShuf));
		Y = vecSub(vecMul(detB, C), temp2);
	}

	detM = vecMul(detA, detD);
	detM = vecMulAdd(detB, detC, detM);
	// trace((A#B) * (D#C))
	Vec trace = vecShuffle<VecMask::_xzyw>(adjDC);
	trace = vec(vecDot4(adjAB, trace));
	// |M| = |AD - BC| = |A||D| + |B||C| - tr(A#B * D#C)
	detM = vecSub(detM, trace);

	const Vec detDiv = vec(1.0f, -1.0f, -1.0f, 1.0f);
	const Vec recipDetM = vecDiv(detDiv, detM);

	X = vecMul(X, recipDetM);
	Y = vecMul(Y, recipDetM);
	Z = vecMul(Z, recipDetM);
	W = vecMul(W, recipDetM);

	// These permutes do the final Adjugation of the sub block matrices
	outX = vecPermute<VecMask::_wywy>(X, Y);
	outY = vecPermute<VecMask::_zxzx>(X, Y);
	outZ = vecPermute<VecMask::_wywy>(Z, W);
	outW = vecPermute<VecMask::_zxzx>(Z, W);
}


const double PI_DBL = 3.14159265358979323846;
const double PI2_DBL = PI_DBL * 2.0;
const double HALFPI_DBL = PI_DBL * 0.5;
const float PI = (float)PI_DBL;
const float PI2 = (float)PI2_DBL;
const float HALFPI = (float)HALFPI_DBL;
const float QUARTPI = (float)(HALFPI_DBL / 2.0);
const float INVPI = (float)(1 / PI_DBL);
const float INVPI2 = (float)(1 / PI2_DBL);
const float INVHALFPI = (float)(1 / HALFPI_DBL);
const float INVQUARTPI = (float)(2.0 / HALFPI_DBL);

const float ChebyshevConstant1 = (float)(0.9996949 * PI2_DBL);
const float ChebyshevConstant2 = (float)(-0.1656700 * PI2_DBL * PI2_DBL * PI2_DBL);
const float ChebyshevConstant3 = (float)(0.0075134 * PI2_DBL * PI2_DBL * PI2_DBL * PI2_DBL * PI2_DBL);

FORCEINLINE Vec vecSininrange025(Vec x)
{
	Vec x2 = vecMul(x, x);
	Vec x3 = vecMul(x2, x);
	Vec x5 = vecMul(x3, x2);
	Vec res = vecMul(vec(ChebyshevConstant1), x);
	res =  vecMulAdd(vec(ChebyshevConstant2), x3, res);
	res =  vecMulAdd(vec(ChebyshevConstant3), x5, res);

	return res;
}

FORCEINLINE Vec vecSin(Vec x)
{
	const Vec zero = vecZero();
	const Vec one = vecOne;
	const Vec half = vecHalf;
	const Vec quater = vecQuarter;

	x = vecMulAdd(x, vec(INVPI2), quater);
	x = vecSub(x, vecTrunc(x));

	x = vecAdd(x, vecAnd(one, vecCmpLE(x, zero)));
	
	x = vecSub(x, quater);
	x = vecSel(x, vecSub(half, x), vecCmpGE(x, quater));

	return vecSininrange025(x);
}

FORCEINLINE Vec vecCos(Vec x)
{
	return vecSin(vecAdd(x, vec(HALFPI)));
}

FORCEINLINE Vec vecSinCosYW(Vec x)
{
	return vecSin(vecAdd(x, vec(0, HALFPI, 0, HALFPI)));
}

const float  SINCOS_CC0 = -0.0013602249f;
const float  SINCOS_CC1 =  0.0416566950f;
const float  SINCOS_CC2 = -0.4999990225f;
const float  SINCOS_SC0 = -0.0001950727f;
const float  SINCOS_SC1 =  0.0083320758f;
const float  SINCOS_SC2 = -0.1666665247f;
const float  SINCOS_KC1 =  1.57079625129f;
const float  SINCOS_KC2 =  7.54978995489e-8f;

FORCEINLINE void vecSinCosPrecise(Vec ang, Vec& s, Vec& c)
{
	IntVec q;
	IntVec offsetSin, offsetCos;
	IntVec C0 = vecZeroInt();
	IntVec C1 = intVec(1);
	IntVec C2 = intVec(2);
	IntVec C3 = intVec(3);

	Vec xl;
	xl = vecMul(ang, vec(INVHALFPI));
	xl = vecAdd(xl, vecBitSel(vecHalf, ang, vecSignMaskXYZW()));

	q = vecTruncToInt(xl);

	offsetSin = vecAnd(q, C3);
	offsetCos = vecAdd(C1, offsetSin);

	Vec qf = intVecToVec(q);
	Vec p1 = vecMulSub(ang, qf, vec(SINCOS_KC1));
		xl = vecMulSub(p1,  qf, vec(SINCOS_KC2));

	Vec xl2 = vecMul(xl, xl);
	Vec xl3 = vecMul(xl2, xl);

	Vec ct1 = vecMulAdd(vec(SINCOS_CC0), xl2, vec(SINCOS_CC1));
	Vec st1 = vecMulAdd(vec(SINCOS_SC0), xl2, vec(SINCOS_SC1));

	Vec ct2 = vecMulAdd(ct1, xl2, vec(SINCOS_CC2));
	Vec st2 = vecMulAdd(st1, xl2, vec(SINCOS_SC2));

	Vec cx = vecMulAdd(ct2, xl2, vecOne);
	Vec sx = vecMulAdd(st2, xl3, xl);

	Vec sinMask = intVecCastToVec(vecCmpEQ(vecAnd(offsetSin, C1), C0));
	Vec cosMask = intVecCastToVec(vecCmpEQ(vecAnd(offsetCos, C1), C0));
	s = vecSel(cx, sx, sinMask);
	c = vecSel(cx, sx, cosMask);

	sinMask = intVecCastToVec(vecCmpEQ(vecAnd(offsetSin, C2), C0));
	cosMask = intVecCastToVec(vecCmpEQ(vecAnd(offsetCos, C2), C0));

	s = vecSel(vecNeg(s), s, sinMask);
	c = vecSel(vecNeg(c), c, cosMask);
}

const float  MINUS_DP1 = -0.78515625f;
const float  MINUS_DP2 = -2.4187564849853515625e-4f;
const float  MINUS_DP3 = -3.77489497744594108e-8f;

const float  TAN_B0 = 1.0e-4f;
const float  TAN_B1 = 1.0e-35f;
const float  TAN_P0 = 9.38540185543e-3f;
const float  TAN_P1 = 3.11992232697e-3f;
const float  TAN_P2 = 2.44301354525e-2f;
const float  TAN_P3 = 5.34112807005e-2f;
const float  TAN_P4 = 1.33387994085e-1f;
const float  TAN_P5 = 3.33331568548e-1f;

FORCEINLINE Vec vecTan(Vec ang)
{
	Vec x = vecAbs(ang);
	Vec signBit = vecAnd(ang, vecSignMaskXYZW());
	Vec xl = vecMul(x, vec(INVQUARTPI));
	Vec cmp = vecCmpGT(x, vec(TAN_B0));

	IntVec q = vecTruncToInt(xl);
	q = vecAdd(q, intVec(1));
	q = vecAnd(q, intVec(~1));
	xl = intVecToVec(q);

	x = vecMulAdd(xl, vec(MINUS_DP1), x);
	x = vecMulAdd(xl, vec(MINUS_DP2), x);
	x = vecMulAdd(xl, vec(MINUS_DP3), x);

	Vec x2 = vecMul(x, x);

	Vec y = vecMulAdd(x2, vec(TAN_P0), vec(TAN_P1));
	y = vecMulAdd(y, x2, vec(TAN_P2));
	y = vecMulAdd(y, x2, vec(TAN_P3));
	y = vecMulAdd(y, x2, vec(TAN_P4));
	y = vecMulAdd(y, x2, vec(TAN_P5));
	y = vecMul(y, x2);
	y = vecMulAdd(y, x, x);

	y = vecSel(x, y, cmp);

	q = vecAnd(q, intVec(2));
	q = vecCmpEQ(q, vecZeroInt());
	cmp = intVecCastToVec(q);

	Vec z = vecDiv(vecNegOne, y);
	y = vecSel(z, y, cmp);
	return vecXor(y, signBit);
}

const float  ASIN_P0 = 4.2163199048e-2f;
const float  ASIN_P1 = 2.4181311049e-2f;
const float  ASIN_P2 = 4.5470025998e-2f;
const float  ASIN_P3 = 7.4953002686e-2f;
const float  ASIN_P4 = 1.6666752422e-1f;

FORCEINLINE Vec vecASin(Vec ang)
{
	Vec x = vecAbs(ang);
	Vec signBit = vecAnd(ang, vecSignMaskXYZW());
	Vec invalidMask = vecCmpGT(x, vecOne);
	Vec cmp = vecCmpGE(x, vecHalf);

	Vec z1 = vecMul(vecHalf, vecSub(vecOne, x));
	Vec x1 = vecSqrt(z1);
	Vec z2 = vecMul(x, x);

	Vec z = vecSel(z2, z1, cmp);
		x = vecSel(x,  x1, cmp);

	Vec y = vecMulAdd(z, vec(ASIN_P0), vec(ASIN_P1));
	y = vecMulAdd(y, z, vec(ASIN_P2));
	y = vecMulAdd(y, z, vec(ASIN_P3));
	y = vecMulAdd(y, z, vec(ASIN_P4));
	y = vecMul(y, z);
	y = vecMulAdd(y, x, x);

	Vec y2 = vecSub(vec(HALFPI), vecAdd(y, y));
	y2 = vecAnd(cmp, y2);
	y = vecSel(y, y2, cmp);

	y = vecXor(y, signBit);
	return vecOr(y, invalidMask);
}

FORCEINLINE Vec vecACos(Vec ang)
{
	Vec polyMask1 = vecCmpGE(vecNeg(vecHalf), ang);
	Vec polyMask2 = vecCmpGE(ang, vecHalf);

	Vec x1 = vecAdd(vecOne, ang);
	Vec x2 = vecSub(vecOne, ang);

	x1 = vecAnd(polyMask1, x1);
	x2 = vecAnd(polyMask2, x2);
	Vec x = vecOr(x1, x2);
	x = vecSqrt(vecMul(vecHalf, x));

	Vec polyMask3 = vecOr(polyMask1, polyMask2);
	Vec x3 = vecAndNot(polyMask3, ang);
	x = vecOr(x, x3);

	Vec signBit = vecAnd(x, vecSignMaskXYZW());
	x = vecAbs(x);

	Vec z = vecMul(x, x);
	Vec y = vecMulAdd(z, vec(ASIN_P0), vec(ASIN_P1));
	y = vecMulAdd(y, z, vec(ASIN_P2));
	y = vecMulAdd(y, z, vec(ASIN_P3));
	y = vecMulAdd(y, z, vec(ASIN_P4));
	y = vecMul(y, z);
	y = vecMulAdd(y, x, x);

	x = vecXor(y, signBit);

	Vec fact1 = vecAnd(polyMask1, vecNeg(vecTwo));
	Vec fact2 = vecAnd(polyMask2, vecTwo);
	Vec fact = vecOr(fact1, fact2);
	Vec fact3 = vecAndNot(polyMask3, vecNegOne);
	fact = vecAdd(fact, fact3);
	x = vecMul(x, fact);
	Vec offs1 = vecAnd(polyMask1, vec(PI));
	Vec offs3 = vecAndNot(polyMask3, vec(HALFPI));
	offs1 = vecOr(offs1, offs3);
	x = vecAdd(x, offs1);

	Vec absVal = vecAbs(ang);
	Vec invalidMask = vecCmpGT(absVal, vecOne);
	return vecOr(x, invalidMask);
}

const float ATAN_Q0 =  2.414213562373095f;
const float ATAN_Q1 =  0.414213562373095f;
const float ATAN_P0 =  8.05374449538e-2f;
const float ATAN_P1 = -1.38776856032e-1f;
const float ATAN_P2 =  1.99777106478e-1f;
const float ATAN_P3 = -3.33329491539e-1f;

FORCEINLINE Vec vecATan(Vec x)
{
	Vec signBit = vecAnd(x, vecSignMaskXYZW());
	x = vecAbs(x);

	Vec mask1 = vecCmpGE(x, vec(ATAN_Q0));
	Vec mask2 = vecAndNot(mask1, vecCmpGE(x, vec(ATAN_Q1)));
	Vec mask3 = vecOr(mask1, mask2);

	Vec y = vecAnd(mask1, vec(HALFPI));
	y = vecOr(y, vecAnd(mask2, vec(QUARTPI)));
	Vec x1 = vecDiv(vecNegOne, x);
	Vec x2 = vecDiv(vecSub(x, vecOne), vecAdd(x, vecOne));
	x = vecAndNot(mask3, x);
	x = vecOr(x, vecAnd(mask1, x1));
	x = vecOr(x, vecAnd(mask2, x2));

	Vec z = vecMul(x, x);
	Vec tmp = vecMulAdd(z, vec(ATAN_P0), vec(ATAN_P1));
	tmp = vecMulAdd(tmp, z, vec(ATAN_P2));
	tmp = vecMulAdd(tmp, z, vec(ATAN_P3));
	tmp = vecMul(tmp, z);
	tmp = vecMulAdd(tmp, x, x);
	tmp = vecAdd(tmp, y);

	return vecXor(tmp, signBit);
}

const float ATAN_EST_T0 = 0.91646118527267623468e-1f;
const float ATAN_EST_T1 = 0.13956945682312098640e1f;
const float ATAN_EST_T2 = 0.94393926122725531747e2f;
const float ATAN_EST_T3 = 0.12888383034157279340e2f;
const float ATAN_EST_S0 = 0.12797564625607904396e1f;
const float ATAN_EST_S1 = 0.21972168858277355914e1f;
const float ATAN_EST_S2 = 0.68193064729268275701e1f;
const float ATAN_EST_S3 = 0.28205206687035841409e2f;

// approximate atan_est |error| is < 0.00045
// calculates 4 in ~2.81x speed of win libc implementation for 1, with same precision
FORCEINLINE Vec vecATanEst(Vec x)  // any x
{
	Vec xRcp = vecRecipEst(x);

	Vec isOut1m1 = vecOr(vecCmpGT(x, vecOne), vecCmpGE(vecNegOne, x));
	Vec xUsed = vecSel(x, xRcp, isOut1m1);

	Vec xUsedSq = vecMul(xUsed, xUsed);
	Vec atanPoly;
	atanPoly = vecAdd(xUsedSq, vec(ATAN_EST_S0));
	atanPoly = vecMul(vecRecipEst(atanPoly), vec(ATAN_EST_T0));

	atanPoly = vecAdd(atanPoly, vecAdd(xUsedSq, vec(ATAN_EST_S1)));
	atanPoly = vecMul(vecRecipEst(atanPoly), vec(ATAN_EST_T1));

	atanPoly = vecAdd(atanPoly, vecAdd(xUsedSq, vec(ATAN_EST_S2)));
	atanPoly = vecMul(vecRecipEst(atanPoly), vec(ATAN_EST_T2));

	atanPoly = vecAdd(atanPoly, vecAdd(xUsedSq, vec(ATAN_EST_S3)));
	atanPoly = vecMul(vecRecipEst(atanPoly), vecMul(xUsed, vec(ATAN_EST_T3)));

	Vec res = vecOr(vecAnd(xUsed, vecSignMaskXYZW()), vec(HALFPI));
	res = vecSub(res, atanPoly);
	return vecSel(atanPoly, res, isOut1m1);
}

FORCEINLINE Vec vecATan2(Vec y, Vec x)
{
	Vec maskYgt0 = vecCmpGE(y, vecZero());
	Vec maskYlt0 = vecCmpGE(vecZero(), y);
	Vec tmp1 = vecAnd(maskYgt0, vec(HALFPI));
	Vec tmp2 = vecAnd(maskYlt0, vec(HALFPI));
	Vec val = vecSub(tmp1, tmp2);

	Vec maskXlt0 = vecCmpGE(vecZero(), x);
	maskYgt0 = vecAndNot(maskYlt0, maskXlt0);
	maskYlt0 = vecAnd(maskYlt0, maskXlt0);
	tmp1 = vecAnd(maskYgt0, vec(PI));
	tmp2 = vecAnd(maskYlt0, vec(PI));
	Vec offs = vecSub(tmp1, tmp2);

	Vec maskXeq0 = vecCmpEQ(x, vecZero());
	Vec atan = vecATan(vecDiv(y, x));

	atan = vecAdd(atan, offs);
	atan = vecAndNot(maskXeq0, atan);
	val = vecAnd(maskXeq0, val);
	return vecAdd(atan, val);
}

// fast approx atan version. |error| is < 0.0004
// ~40% faster then vecATan2
// NOTE: does not handle any of the following inputs:
// (+0, +0), (+0, -0), (-0, +0), (-0, -0)
FORCEINLINE Vec vecATan2Est(Vec y, Vec x)
{
	// compute the atan
	Vec raw_atan = vecATanEst(vecDivEst(y, x));

	Vec neg_x = vecSignMask(x);
	Vec neg_y = vecSignMask(y);

	Vec in_quad2 = vecAndNot(neg_y, neg_x);
	Vec quad2_fixed = vecSel(raw_atan, vecAdd(raw_atan, vec(PI)), in_quad2);

	// move from quadrant 1 to 3 by subtracting PI
	Vec in_quad3 = vecAnd(neg_x, neg_y);
	Vec quad23_fixed = vecSel(quad2_fixed, vecSub(raw_atan, vec(PI)), in_quad3);

	Vec y_zero = vecCmpEQ(x, vecZero());
	Vec yzeropos_fixed = vecSel(quad23_fixed, vec(HALFPI), vecAnd(y_zero, vecCmpGT(y, vecZero())));
	Vec yzeroneg_fixed = vecSel(yzeropos_fixed, vecNeg(vec(HALFPI)), vecAnd(y_zero, vecCmpGE(vecZero(), y)));
	return yzeroneg_fixed;
}

const unsigned int FloatExponentMask = 0x7F800000;
const unsigned int FloatMantissaMask = 0x007FFFFF;
const unsigned int FloatExponentSignOffset = 127;
const unsigned int FloatMantissaBits = 23;

#define POLY0(x, c0) vec(c0)
#define POLY1(x, c0, c1) vecMulAdd(POLY0(x, c1), x, vec(c0))
#define POLY2(x, c0, c1, c2) vecMulAdd(POLY1(x, c1, c2), x, vec(c0))
#define POLY3(x, c0, c1, c2, c3) vecMulAdd(POLY2(x, c1, c2, c3), x, vec(c0))
#define POLY4(x, c0, c1, c2, c3, c4) vecMulAdd(POLY3(x, c1, c2, c3, c4), x, vec(c0))
#define POLY5(x, c0, c1, c2, c3, c4, c5) vecMulAdd(POLY4(x, c1, c2, c3, c4, c5), x, vec(c0))

#define EXP_DEF_PART(bias)\
	IntVec ipart;\
	Vec fpart, expipart, expfpart;\
	x = vecMin(x, vec( 129.00000f));\
	x = vecMax(x, vec(-126.99999f));\
	ipart = vecRoundToInt(vecSub(x, bias));\
	fpart = vecSub(x, intVecToVec(ipart));\
	expipart = intVecCastToVec(vecShiftLeftLogical<FloatMantissaBits>(vecAdd(ipart, intVec(FloatExponentSignOffset))));\

const float Exp2Poly5C1 = 9.9999994e-1f;
const float Exp2Poly5C2 = 6.9315308e-1f;
const float Exp2Poly5C3 = 2.4015361e-1f;
const float Exp2Poly5C4 = 5.5826318e-2f;
const float Exp2Poly5C5 = 8.9893397e-3f;
const float Exp2Poly5C6 = 1.8775767e-3f;

// relative error is < 1e-7
FORCEINLINE Vec vecExp2EstP5Int(Vec x)
{
	EXP_DEF_PART(vec(0.5f - 1.192092896e-07f * 32)); // half minus some epsilon
	expfpart = POLY5(fpart, Exp2Poly5C1, Exp2Poly5C2, Exp2Poly5C3, Exp2Poly5C4, Exp2Poly5C5, Exp2Poly5C6);
	return vecSel(vecMul(expipart, expfpart), expipart, vecCmpEQ(fpart, vecZero())); //ensure that exp2(int) = 2^int
}

// relative error is < 1e-7
FORCEINLINE Vec vecExp2EstP5(Vec x)
{
	EXP_DEF_PART(vecHalf);
	expfpart = POLY5(fpart, Exp2Poly5C1, Exp2Poly5C2, Exp2Poly5C3, Exp2Poly5C4, Exp2Poly5C5, Exp2Poly5C6);
	return vecMul(expipart, expfpart);
}

const float Exp2Poly4C1 = 1.0000026f;
const float Exp2Poly4C2 = 6.9300383e-1f;
const float Exp2Poly4C3 = 2.4144275e-1f;
const float Exp2Poly4C4 = 5.2011464e-2f;
const float Exp2Poly4C5 = 1.3534167e-2f;

// relative error is < 2.6e-6
FORCEINLINE Vec vecExp2EstP4(Vec x)
{
	EXP_DEF_PART(vecHalf);
	expfpart = POLY4(fpart, Exp2Poly4C1, Exp2Poly4C2, Exp2Poly4C3, Exp2Poly4C4, Exp2Poly4C5);
	return vecMul(expipart, expfpart);
}

const float Exp2Poly3C1 = 9.9992520e-1f;
const float Exp2Poly3C2 = 6.9583356e-1f;
const float Exp2Poly3C3 = 2.2606716e-1f;
const float Exp2Poly3C4 = 7.8024521e-2f;

// relative error is < 1e-4
FORCEINLINE Vec vecExp2EstP3(Vec x)
{
	EXP_DEF_PART(vecHalf);
	expfpart = POLY3(fpart, Exp2Poly3C1, Exp2Poly3C2, Exp2Poly3C3, Exp2Poly3C4);
	return vecMul(expipart, expfpart);
}

const float Exp2Poly2C1 = 1.0017247f;
const float Exp2Poly2C2 = 6.5763628e-1f;
const float Exp2Poly2C3 = 3.3718944e-1f;

// relative error is < 2e-3
FORCEINLINE Vec vecExp2EstP2(Vec x)
{
	EXP_DEF_PART(vecHalf);
	expfpart = POLY2(fpart, Exp2Poly2C1, Exp2Poly2C2, Exp2Poly2C3);
	return vecMul(expipart, expfpart);
}

#undef EXP_DEF_PART

#define LOG_DEF_PART\
	IntVec i = vecCastToIntVec(x);\
	Vec e = intVecToVec(vecSub(vecShiftRightLogical<FloatMantissaBits>(vecAnd(i, intVec(FloatExponentMask))), intVec(FloatExponentSignOffset)));\
	Vec m = vecOr(intVecCastToVec(vecAnd(i, intVec(FloatMantissaMask))), vecOne);

const float Log2Poly5C1 = 3.1157899f;
const float Log2Poly5C2 = -3.3241990f;
const float Log2Poly5C3 = 2.5988452f;
const float Log2Poly5C4 = -1.2315303f;
const float Log2Poly5C5 = 3.1821337e-1f;
const float Log2Poly5C6 = -3.4436006e-2f;

// relative error is < 1.0e-5
FORCEINLINE Vec vecLog2EstP5(Vec x)
{
	LOG_DEF_PART
	Vec p = POLY5(m, Log2Poly5C1, Log2Poly5C2, Log2Poly5C3, Log2Poly5C4, Log2Poly5C5, Log2Poly5C6);
	return vecMulAdd(p, vecSub(m, vecOne), e); // this effectively increases the polynomial degree by one, but ensures that log2(1) == 0
}

const float Log2Poly4C1 = 2.8882704548164776201f;
const float Log2Poly4C2 = -2.52074962577807006663f;
const float Log2Poly4C3 = 1.48116647521213171641f;
const float Log2Poly4C4 = -0.465725644288844778798f;
const float Log2Poly4C5 = 0.0596515482674574969533f;

// relative error is < 5.7e-5
FORCEINLINE Vec vecLog2EstP4(Vec x)
{
	LOG_DEF_PART
	Vec p = POLY4(m, Log2Poly4C1, Log2Poly4C2, Log2Poly4C3, Log2Poly4C4, Log2Poly4C5);
	return vecMulAdd(p, vecSub(m, vecOne), e); // this effectively increases the polynomial degree by one, but ensures that log2(1) == 0
}

const float Log2Poly3C1 = 2.61761038894603480148f;
const float Log2Poly3C2 = -1.75647175389045657003f;
const float Log2Poly3C3 = 0.688243882994381274313f;
const float Log2Poly3C4 = -0.107254423828329604454f;

// relative error is < 4e-4
FORCEINLINE Vec vecLog2EstP3(Vec x)
{
	LOG_DEF_PART
	Vec p = POLY3(m, Log2Poly3C1, Log2Poly3C2, Log2Poly3C3, Log2Poly3C4);
	return vecMulAdd(p, vecSub(m, vecOne), e); // this effectively increases the polynomial degree by one, but ensures that log2(1) == 0
}

const float Log2Poly2C1 = 2.28330284476918490682f;
const float Log2Poly2C2 = -1.04913055217340124191f;
const float Log2Poly2C3 = 0.204446009836232697516f;

// relative error is < 3e-3
FORCEINLINE Vec vecLog2EstP2(Vec x)
{
	LOG_DEF_PART
	Vec p = POLY2(m, Log2Poly2C1, Log2Poly2C2, Log2Poly2C3);
	return vecMulAdd(p, vecSub(m, vecOne), e); // this effectively increases the polynomial degree by one, but ensures that log2(1) == 0
}

#undef LOG_DEF_PART
#undef POLY0
#undef POLY1
#undef POLY2
#undef POLY3
#undef POLY4
#undef POLY5

const float Log2OfE = 1.4426950408889634073599f; // log2(e)
const float Log2Of10 = 3.321928094887f; // log2(10)
const float InvLog2OfE = 2.28330284476918490682f; // 1/log2(e)
const float InvLog2Of10 = 0.301029995664f; // 1/log2(10)

// relative error is < 1e-5
FORCEINLINE Vec vecLog(Vec x)
{
	return vecMul(vecLog2EstP5(x), vec(InvLog2OfE));
}

// relative error is < 4e-4
FORCEINLINE Vec vecLogFast(Vec x)
{
	return vecMul(vecLog2EstP3(x), vec(InvLog2OfE));
}

// relative error is < 1e-5
FORCEINLINE Vec vecLog2(Vec x)
{
	return vecLog2EstP5(x);
}

// relative error is < 4e-4
FORCEINLINE Vec vecLog2Fast(Vec x)
{
	return vecLog2EstP3(x);
}

// relative error is < 1e-5
FORCEINLINE Vec vecLog10(Vec x)
{
	return vecMul(vecLog2EstP5(x), vec(InvLog2Of10));
}

// relative error is < 4e-4
FORCEINLINE Vec vecLog10Fast(Vec x)
{
	return vecMul(vecLog2EstP3(x), vec(InvLog2Of10));
}

// relative error is < 1e-7
FORCEINLINE Vec vecExp(Vec x)
{
	return vecExp2EstP5(vecMul(x, vec(Log2OfE)));
}

// relative error is < 1e-4
FORCEINLINE Vec vecExpFast(Vec x)
{
	return vecExp2EstP3(vecMul(x, vec(Log2OfE)));
}

// relative error is < 1e-7
FORCEINLINE Vec vecExp2(Vec x)
{
	return vecExp2EstP5Int(x);
}

// relative error is < 1e-4
FORCEINLINE Vec vecExp2Fast(Vec x)
{
	return vecExp2EstP3(x);
}

// relative error is < 1e-7
FORCEINLINE Vec vecExp10(Vec x)
{
	return vecExp2EstP5(vecMul(x, vec(Log2Of10)));
}

// relative error is < 1e-4
FORCEINLINE Vec vecExp10Fast(Vec x)
{
	return vecExp2EstP3(vecMul(x, vec(Log2Of10)));
}

// relative errors:
//		<3e-5 for x range [0.1, 100000] and y range [-5; 5]
//		<6e-5 for x range [0.1, 1000]   and y range [-10; 10]
//		<9e-5 for x range [0.1, 100]    and y range [-15; 15]
FORCEINLINE Vec vecPow(Vec x, Vec y)
{
	Vec ret = vecExp2EstP5(vecMul(vecLog2EstP5(x), y));
	ret = vecSel(ret, vecOne, vecCmpEQ(y, vecZero()));
	return ret;
}

// relative errors:
//		<1.4e-3 for x range [0.1, 100000] and y range [-5; 5]
//		<3e-3   for x range [0.1, 1000]   and y range [-10; 10]
//		<4e-3   for x range [0.1, 100]    and y range [-15; 15]
FORCEINLINE Vec vecPowFast(Vec x, Vec y)
{
	Vec ret = vecExp2EstP3(vecMul(vecLog2EstP3(x), y));
	return ret;
}

FORCEINLINE IntVec vecF32tof16(Vec x)
{
	IntVec infinity_32 = intVec(255 << 23);
	IntVec msk = intVec(0x7FFFF000u);
	IntVec notMsk = intVec(~0x7FFFF000u);

	IntVec ux = vecCastToIntVec(x);
	IntVec uux = vecAnd(ux, msk);

	// Clamp to signed infinity if overflowed
	IntVec h = vecCastToIntVec(vecMin(vecMul(intVecCastToVec(uux), vec(1.92592994e-34f)), vec(260042752.0f)));
	h = vecShiftRightLogical<13>(vecAdd(h, intVec(0x1000u)));

	// NaN->qNaN and Inf->Inf
	h = vecSel( 
				vecSel(intVec(0x7c00u), intVec(0x7e00u), vecCmpGT(uux, infinity_32)), 
				h,
				vecCmpGT(infinity_32, uux)
	);

	return vecOr(h, vecShiftRightLogical<16>(vecAnd(ux, notMsk)));
}

FORCEINLINE Vec vecF16tof32(IntVec x)
{
	IntVec shifted_exp = intVec(0x7c00u << 13);
	IntVec offset_exp = intVec((128u - 16u) << 23);
	IntVec uf = vecShiftLeftLogical<13>(vecAnd(x, intVec(0x7fffu)));
	IntVec e = vecAnd(uf, shifted_exp);
	uf = vecAdd(uf, offset_exp);
	uf = vecAdd(uf, vecSel(vecZeroInt(), offset_exp, vecCmpEQ(e, shifted_exp)));
	uf = vecSel(
				uf,
				vecCastToIntVec(vecAdd(
										intVecCastToVec(vecAdd(uf, intVec(1u << 23))),
										vec(-6.10351563e-05f)
				)),
				vecCmpEQ(e, vecZeroInt())
	);
	return intVecCastToVec(vecOr(uf, vecShiftLeftLogical<31>(vecShiftRightLogical<15>(x)))); // move sign from 15th to 31th bit
}

FORCEINLINE Vec vecSign(Vec x)
{
	Vec signBit = vecAnd(x, vecSignMaskXYZW());
	return vecSel(vecOr(vecOne, signBit), vecZero(), vecCmpEQ(x, vecZero()));
}

FORCEINLINE Vec vecSignNoZero(Vec x)
{
	Vec signBit = vecAnd(x, vecSignMaskXYZW());
	return vecOr(vecOne, signBit);
}
