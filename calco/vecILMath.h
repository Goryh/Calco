#pragma once

#include "vecMath.h"

typedef unsigned int uint;

struct float2_internal
{
	float x;
	float y;

	Vec load()
	{
		return vec(x, y);
	}

	void store(Vec v)
	{
		vecStore2Unaligned(v, (float*)this);
	}
};

struct float3_internal
{
	float x;
	float y;
	float z;

	Vec load()
	{
		return vec(x, y, z);
	}

	Vec load4Unaligned()
	{
		return vecUnaligned((float*)this);
	}

	Vec load1()
	{
		return vec(x, y, z, 1.0f);
	}

	void store(Vec v)
	{
		vecStore3Unaligned(v, (float*)this);
	}
};

struct float3a_internal
{
	float x;
	float y;
	float z;
	float pad;

	Vec load()
	{
		return vecUnaligned((float*)this);
	}

	Vec load1()
	{
		Vec v = vecUnaligned((float*)this);
		return vecShuffle<VecMask::_xyz1>(v);
	}

	void store(Vec v)
	{
		vecStore4Unaligned(v, (float*)this);
	}
};

struct float4_internal
{
	float x;
	float y;
	float z;
	float w;

	Vec load()
	{
		return vecUnaligned((float*)this);
	}

	void store(Vec v)
	{
		vecStore4Unaligned(v, (float*)this);
	}
};

struct plane3d_internal
{
	float nx;
	float ny;
	float nz;
	float dist;

	Vec load()
	{
		return vecUnaligned((float*)this);
	}

	void store(Vec n, Vec d)
	{
		vecStore3Unaligned(n, (float*)this);
		dist = vecStore1(d);
	}
};

struct quaternion_internal
{
	float x;
	float y;
	float z;
	float w;

	Vec load()
	{
		return vecUnaligned((float*)this);
	}

	void store(Vec v)
	{
		vecStore4Unaligned(v, (float*)this);
	}
};

struct rigidTransform_internal
{
	quaternion_internal rot;
	float3_internal pos;
};

struct float3x3_internal
{
	float3_internal c0;
	float3_internal c1;
	float3_internal c2;
};

struct float3ax3_internal
{
	float3a_internal c0;
	float3a_internal c1;
	float3a_internal c2;
};

struct float4x4_internal
{
	float4_internal c0;
	float4_internal c1;
	float4_internal c2;
	float4_internal c3;
};

struct uint2_internal
{
	uint x;
	uint y;
};

extern "C" {

struct float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA;
struct float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E;
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2;
struct float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6;
struct float4_t89D9A294E7A79BD81BFBDD18654508532958555E;
struct quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4;
struct RigidTransform_tDC22CD9569EC04E16791C2AB53DA5ABD34E88FDD;
struct Plane3d_t8F375C04D65DD2A7C8D0BF2F6DA1F17CA0FA6076;
struct float3x3_tB318DB8C7E54B6CA9E14EB9AC7F5964C1189FC79;
struct float3ax3_tA3A55BF35F9576BCFD662464DC46E4CF3225D8D3;
struct float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2;
struct uint2_t157753816C23B82EB918C3D3AFCFDDE06A04C05F;

FORCEINLINE void __cdecl vecILMathFloat4ToFloat3a(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inV, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float4_internal* v 		= (float4_internal*)inV;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(v->load());
}

FORCEINLINE void __cdecl vecILMathFloat3ToFloat3a(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inV, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3_internal* v 		= (float3_internal*)inV;
	float3a_internal* res	= (float3a_internal*)resF;

	*res = *(float3a_internal*)v; // this syntax produces a shorter code on MSVC, the same on arm
	//res->store(v->load4Unaligned());
}

FORCEINLINE void __cdecl vecILMathFloat3aToFloat3(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inV, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3a_internal* v 	= (float3a_internal*)inV;
	float3_internal* res	= (float3_internal*)resF;

	*res = *(float3_internal*)v; // this syntax produces a shorter code on MSVC, the same on arm
	//res->store(v->load());
}

FORCEINLINE void __cdecl vecILMathVector3ToFloat3a(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* RESTRICT inV, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3_internal* v 		= (float3_internal*)inV;
	float3a_internal* res	= (float3a_internal*)resF;

	*res = *(float3a_internal*)v; // this syntax produces a shorter code on MSVC, the same on arm
	//res->store(v->load4Unaligned());
}

FORCEINLINE void __cdecl vecILMathFloat3aToVector3(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inV, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2* RESTRICT resF)
{
	float3a_internal* v 	= (float3a_internal*)inV;
	float3_internal* res	= (float3_internal*)resF;

	*res = *(float3_internal*)v; // this syntax produces a shorter code on MSVC, the same on arm
	//res->store(v->load());
}

FORCEINLINE void __cdecl vecILMathFloatToFloat3a(float inF, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vec(inF));
}

FORCEINLINE void __cdecl vecILMathFloat3aToFloat4(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inXYZ, float w, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float3a_internal* xyz 	= (float3a_internal*)inXYZ;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecInsert<VecMask::_0001>(xyz->load(), vec(w)));
}

FORCEINLINE void __cdecl vecILMathFloat3Cross(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inFa, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inFb, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* a = (float3_internal*)inFa;
	float3_internal* b = (float3_internal*)inFb;
	float3_internal* res = (float3_internal*)resF;

	res->store(vecCross(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat3aCross(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inFa, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inFb, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* a = (float3a_internal*)inFa;
	float3a_internal* b = (float3a_internal*)inFb;
	float3a_internal* res = (float3a_internal*)resF;

	res->store(vecCross(a->load(), b->load()));
}

FORCEINLINE float __cdecl vecILMathFloat3Dot(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inFa, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inFb)
{
	float3_internal* a = (float3_internal*)inFa;
	float3_internal* b = (float3_internal*)inFb;

	return vecStore1(vecDot3(a->load(), b->load()));
}

FORCEINLINE float __cdecl vecILMathFloat3aDot(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inFa, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inFb)
{
	float3a_internal* a = (float3a_internal*)inFa;
	float3a_internal* b = (float3a_internal*)inFb;

	return vecStore1(vecDot3(a->load(), b->load()));
}

FORCEINLINE float __cdecl vecILMathFloat4Dot(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inFa, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inFb)
{
	float4_internal* a = (float4_internal*)inFa;
	float4_internal* b = (float4_internal*)inFb;

	return vecStore1(vecDot4(a->load(), b->load()));
}
	
FORCEINLINE void __cdecl vecILMathQuaternionInverse(quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4* RESTRICT inQ, quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4* RESTRICT resQ)
{
	quaternion_internal* q = (quaternion_internal*)inQ;
	quaternion_internal* res = (quaternion_internal*)resQ;

	res->store(vecMathQuaternionInverse(q->load()));
}

FORCEINLINE void __cdecl vecILMathQuaternionMul(quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4* RESTRICT inQa, quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4* RESTRICT inQb, quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4* RESTRICT resQ)
{
	quaternion_internal* a = (quaternion_internal*)inQa;
	quaternion_internal* b = (quaternion_internal*)inQb;
	quaternion_internal* res = (quaternion_internal*)resQ;

	res->store(vecMathQuaternionMul(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathQuaternionMulFloat3a(quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4* RESTRICT inQ, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inV, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resV)
{
	quaternion_internal* q = (quaternion_internal*)inQ;
	float3a_internal* v   = (float3a_internal*)inV;
	float3a_internal* res = (float3a_internal*)resV;

	res->store(vecMathQuaternionTransformVec3(q->load(), v->load()));
}

FORCEINLINE void __cdecl vecILMathQuatFromAxisAngle(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inAxis, float angle, quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4* RESTRICT resQ)
{
	float3a_internal* axis   = (float3a_internal*)inAxis;
	quaternion_internal* res = (quaternion_internal*)resQ;

	Vec sinCos = vecSinCosYW(vecMul(vec(angle), vecHalf()));

	res->store(vecMul(axis->load1(), vecShuffle<VecMask::_xxxy>(sinCos)));
}

FORCEINLINE void __cdecl vecILMathRigidTransformInverse(RigidTransform_tDC22CD9569EC04E16791C2AB53DA5ABD34E88FDD* RESTRICT inT, RigidTransform_tDC22CD9569EC04E16791C2AB53DA5ABD34E88FDD* RESTRICT resT)
{
	rigidTransform_internal* t = (rigidTransform_internal*)inT;
	rigidTransform_internal* res = (rigidTransform_internal*)resT;

	Vec invRotation = vecMathQuaternionInverse(t->rot.load());
	Vec invTranslation = vecMathQuaternionTransformVec3(invRotation, vecNeg(t->pos.load()));

	res->rot.store(invRotation);
	res->pos.store(invTranslation);
}

FORCEINLINE void __cdecl vecILMathRigidTransformMul(RigidTransform_tDC22CD9569EC04E16791C2AB53DA5ABD34E88FDD* RESTRICT inTa, RigidTransform_tDC22CD9569EC04E16791C2AB53DA5ABD34E88FDD* RESTRICT inTb, RigidTransform_tDC22CD9569EC04E16791C2AB53DA5ABD34E88FDD* RESTRICT resT)
{
	rigidTransform_internal* a = (rigidTransform_internal*)inTa;
	rigidTransform_internal* b = (rigidTransform_internal*)inTb;
	rigidTransform_internal* res = (rigidTransform_internal*)resT;

	Vec aRot = a->rot.load();
	Vec bRot = b->rot.load();
	Vec aPos = a->pos.load();
	Vec bPos = b->pos.load();

	res->rot.store(vecMathQuaternionMul(aRot, bRot));
	res->pos.store(vecAdd(vecMathQuaternionTransformVec3(aRot, bPos), aPos));
}

FORCEINLINE void __cdecl vecILMathRigidTransformMulPlane(RigidTransform_tDC22CD9569EC04E16791C2AB53DA5ABD34E88FDD* RESTRICT inT, Plane3d_t8F375C04D65DD2A7C8D0BF2F6DA1F17CA0FA6076* RESTRICT inP, Plane3d_t8F375C04D65DD2A7C8D0BF2F6DA1F17CA0FA6076* RESTRICT resP)
{
	rigidTransform_internal* t 	= (rigidTransform_internal*)inT;
	plane3d_internal* p 		= (plane3d_internal*)inP;
	plane3d_internal* res 		= (plane3d_internal*)resP;

	Vec pVec = p->load();
	Vec normal = vecMathQuaternionTransformVec3(t->rot.load(), pVec);
	Vec point = vecAdd(vecMul(normal, vecNeg(vecShuffle<VecMask::_wwww>(pVec))), t->pos.load());
	Vec dist = vecDot3(vecNeg(normal), point);

	res->store(normal, dist);
}

FORCEINLINE void __cdecl vecILMathFloat3ax3FromQuat(quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4* RESTRICT inQ, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF0, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF1, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF2)
{
	quaternion_internal* q = (quaternion_internal*)inQ;
	float3a_internal* res0 = (float3a_internal*)resF0;
	float3a_internal* res1 = (float3a_internal*)resF1;
	float3a_internal* res2 = (float3a_internal*)resF2;

	Vec out0, out1, out2;
	vecNormalizingQuatToMatrix(out0, out1, out2, q->load());

	res0->store(out0);
	res1->store(out1);
	res2->store(out2);
}

FORCEINLINE void __cdecl vecILQuatFromFloat3ax3(float3ax3_tA3A55BF35F9576BCFD662464DC46E4CF3225D8D3* RESTRICT inM, quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4* RESTRICT resQ)
{
	float3ax3_internal* m    = (float3ax3_internal*)inM;
	quaternion_internal* res = (quaternion_internal*)resQ;

	res->store(vecMathMatrixToQuaternion(m->c0.load(), m->c1.load(), m->c2.load()));
}

FORCEINLINE void __cdecl vecILQuatFromFloat4x4(float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2* RESTRICT inM, quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4* RESTRICT resQ)
{
	float4x4_internal* m   	 = (float4x4_internal*)inM;
	quaternion_internal* res = (quaternion_internal*)resQ;

	res->store(vecMathMatrixToQuaternion(m->c0.load(), m->c1.load(), m->c2.load()));
}

FORCEINLINE void __cdecl vecILMathFloat4x3FromRotTrans(quaternion_tD6BCBECAF088B9EBAE2345EC8534C7A1A4C910D4* RESTRICT inQ, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inT, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF0, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF1, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF2)
{
	quaternion_internal* q = (quaternion_internal*)inQ;
	float3a_internal* t = (float3a_internal*)inT;
	float4_internal* res0 = (float4_internal*)resF0;
	float4_internal* res1 = (float4_internal*)resF1;
	float4_internal* res2 = (float4_internal*)resF2;

	Vec out0, out1, out2;
	vecNormalizingQuatToMatrix(out0, out1, out2, q->load());

	vecTranspose3x3(out0, out1, out2, out0, out1, out2);

	Vec tVec = t->load();
	out0 = vecInsert<VecMask::_0001>(out0, vecShuffle<VecMask::_xxxx>(tVec));
	out1 = vecInsert<VecMask::_0001>(out1, vecShuffle<VecMask::_yyyy>(tVec));
	out2 = vecInsert<VecMask::_0001>(out2, vecShuffle<VecMask::_zzzz>(tVec));

	res0->store(out0);
	res1->store(out1);
	res2->store(out2);
}

FORCEINLINE void __cdecl vecILMathFloat3ax3Transpose(float3ax3_tA3A55BF35F9576BCFD662464DC46E4CF3225D8D3* RESTRICT inM, float3ax3_tA3A55BF35F9576BCFD662464DC46E4CF3225D8D3* RESTRICT resM)
{
	float3ax3_internal* m   = (float3ax3_internal*)inM;
	float3ax3_internal* res = (float3ax3_internal*)resM;

	Vec out0, out1, out2;
	vecTranspose3x3(out0, out1, out2, m->c0.load(), m->c1.load(), m->c2.load());

	res->c0.store(out0);
	res->c1.store(out1);
	res->c2.store(out2);
}

FORCEINLINE void __cdecl vecILMathFloat3ax3Inverse(float3ax3_tA3A55BF35F9576BCFD662464DC46E4CF3225D8D3* RESTRICT inM, float3ax3_tA3A55BF35F9576BCFD662464DC46E4CF3225D8D3* RESTRICT resM)
{
	float3ax3_internal* m   = (float3ax3_internal*)inM;
	float3ax3_internal* res = (float3ax3_internal*)resM;

	Vec out0, out1, out2;
	vecMatrix33Inverse(out0, out1, out2, m->c0.load(), m->c1.load(), m->c2.load());

	res->c0.store(out0);
	res->c1.store(out1);
	res->c2.store(out2);
}

FORCEINLINE void __cdecl vecILMathFloat3x3Inverse(float3x3_tB318DB8C7E54B6CA9E14EB9AC7F5964C1189FC79* RESTRICT inM, float3x3_tB318DB8C7E54B6CA9E14EB9AC7F5964C1189FC79* RESTRICT resM)
{
	float3x3_internal* m   = (float3x3_internal*)inM;
	float3x3_internal* res = (float3x3_internal*)resM;

	Vec out0, out1, out2;
	vecMatrix33Inverse(out0, out1, out2, m->c0.load(), m->c1.load(), m->c2.load());

	res->c0.store(out0);
	res->c1.store(out1);
	res->c2.store(out2);
}

FORCEINLINE void __cdecl vecILMathFloat4x4Transpose(float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2* RESTRICT inM, float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2* RESTRICT resM)
{
	float4x4_internal* m   = (float4x4_internal*)inM;
	float4x4_internal* res = (float4x4_internal*)resM;

	Vec out0, out1, out2, out3;
	vecTranspose4x4(out0, out1, out2, out3, m->c0.load(), m->c1.load(), m->c2.load(), m->c3.load());

	res->c0.store(out0);
	res->c1.store(out1);
	res->c2.store(out2);
	res->c3.store(out3);
}

FORCEINLINE void __cdecl vecILMathFloat4x4Inverse(float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2* RESTRICT inM, float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2* RESTRICT resM)
{
	float4x4_internal* m   = (float4x4_internal*)inM;
	float4x4_internal* res = (float4x4_internal*)resM;

	Vec out0, out1, out2, out3;
	vecMatrix44Inverse(out0, out1, out2, out3, m->c0.load(), m->c1.load(), m->c2.load(), m->c3.load());

	res->c0.store(out0);
	res->c1.store(out1);
	res->c2.store(out2);
	res->c3.store(out3);
}

FORCEINLINE void __cdecl vecILMathFloat4x4LinearTransformInverse(float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2* RESTRICT inM, float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2* RESTRICT resM)
{
	float4x4_internal* m   = (float4x4_internal*)inM;
	float4x4_internal* res = (float4x4_internal*)resM;

	Vec out0, out1, out2, out3;
	vecLinearTransformInverse(out0, out1, out2, out3, m->c0.load(), m->c1.load(), m->c2.load(), m->c3.load());

	res->c0.store(out0);
	res->c1.store(out1);
	res->c2.store(out2);
	res->c3.store(out3);
}

FORCEINLINE void __cdecl vecILMathFloat4x4LinearTransformNoScaleInverse(float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2* RESTRICT inM, float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2* RESTRICT resM)
{
	float4x4_internal* m   = (float4x4_internal*)inM;
	float4x4_internal* res = (float4x4_internal*)resM;

	Vec out0, out1, out2, out3;
	vecLinearTransformNoScaleInverse(out0, out1, out2, out3, m->c0.load(), m->c1.load(), m->c2.load(), m->c3.load());

	res->c0.store(out0);
	res->c1.store(out1);
	res->c2.store(out2);
	res->c3.store(out3);
}

FORCEINLINE void __cdecl vecILMathFloat4x4MulFloat4(float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2* RESTRICT inM, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inV, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4x4_internal* m = (float4x4_internal*)inM;
	float4_internal* vf  = (float4_internal*)inV;
	float4_internal* res = (float4_internal*)resF;

	Vec v = vf->load();

	Vec result = vecMul(m->c0.load(), vecShuffle<VecMask::_xxxx>(v));
	result  = vecMulAdd(m->c1.load(), vecShuffle<VecMask::_yyyy>(v), result);
	result  = vecMulAdd(m->c2.load(), vecShuffle<VecMask::_zzzz>(v), result);
	result  = vecMulAdd(m->c3.load(), vecShuffle<VecMask::_wwww>(v), result);

	res->store(result);
}

FORCEINLINE void __cdecl vecILMathFloat4MulFloat4x4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inV, float4x4_t7EDD16F7F57DC7F61A6302535F7C19FB97915DF2* RESTRICT inM, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4x4_internal* m = (float4x4_internal*)inM;
	float4_internal* vf  = (float4_internal*)inV;
	float4_internal* res = (float4_internal*)resF;

	Vec v = vf->load();
	Vec c0 = m->c0.load();
	Vec c1 = m->c1.load();
	Vec c2 = m->c2.load();
	Vec c3 = m->c3.load();

	Vec c0t, c1t, c2t, c3t;
	vecTranspose4x4(c0t, c1t, c2t, c3t,
					c0,  c1,  c2,  c3);

	Vec result = vecMul(c0t, vecShuffle<VecMask::_xxxx>(v));
	result  = vecMulAdd(c1t, vecShuffle<VecMask::_yyyy>(v), result);
	result  = vecMulAdd(c2t, vecShuffle<VecMask::_zzzz>(v), result);
	result  = vecMulAdd(c3t, vecShuffle<VecMask::_wwww>(v), result);

	res->store(result);
}

FORCEINLINE float __cdecl vecILMathSininrange025(float x)
{
	return vecStore1(vecSininrange025(vec(x)));
}

FORCEINLINE float __cdecl vecILMathSin(float x)
{
	return vecStore1(vecSin(vec(x)));
}

FORCEINLINE void __cdecl vecILMathSin2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res	= (float2_internal*)resF;

	x->store(vecSin(x->load()));
}

FORCEINLINE void __cdecl vecILMathSin3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res	= (float3_internal*)resF;

	x->store(vecSin(x->load()));
}

FORCEINLINE void __cdecl vecILMathSin4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res	= (float4_internal*)resF;

	x->store(vecSin(x->load()));
}

FORCEINLINE float __cdecl vecILMathCos(float x)
{
	return vecStore1(vecCos(vec(x)));
}

FORCEINLINE void __cdecl vecILMathCos2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res	= (float2_internal*)resF;

	x->store(vecCos(x->load()));
}

FORCEINLINE void __cdecl vecILMathCos3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res	= (float3_internal*)resF;

	x->store(vecCos(x->load()));
}

FORCEINLINE void __cdecl vecILMathCos4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res	= (float4_internal*)resF;

	x->store(vecCos(x->load()));
}

FORCEINLINE void __cdecl vecILMathSinCos(float x, float* s, float* c)
{
	float2_internal res;
	res.store(vecSinCosYW(vec(x)));
	*s = res.x;
	*c = res.y;
}

FORCEINLINE void __cdecl vecILMathSinCos2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resS, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resC)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* s	= (float2_internal*)resS;
	float2_internal* c	= (float2_internal*)resC;

	Vec vecX = x->load();
	vecX = vecShuffle<VecMask::_xxyy>(vecX);

	float4_internal res;
	res.store(vecSinCosYW(vecX));

	s->x = res.x;
	c->x = res.y;
	s->y = res.z;
	c->y = res.w;
}

FORCEINLINE void __cdecl vecILMathSinCos3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resS, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resC)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* s	= (float3_internal*)resS;
	float3_internal* c	= (float3_internal*)resC;

	Vec vecX = x->load();
	s->store(vecSin(vecX));
	c->store(vecCos(vecX));
}

FORCEINLINE void __cdecl vecILMathSinCos4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resS, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resC)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* s	= (float4_internal*)resS;
	float4_internal* c	= (float4_internal*)resC;

	Vec vecX = x->load();
	s->store(vecSin(vecX));
	c->store(vecCos(vecX));
}

FORCEINLINE void __cdecl vecILMathSinCosPrecise(float x, float* s, float* c)
{
	Vec vecS, vecC;
	vecSinCosPrecise(vec(x), vecS, vecC);

	*s = vecStore1(vecS);
	*c = vecStore1(vecC);
}

FORCEINLINE void __cdecl vecILMathSinCosPrecise2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resS, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resC)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* s	= (float2_internal*)resS;
	float2_internal* c	= (float2_internal*)resC;

	Vec vecS, vecC;
	vecSinCosPrecise(x->load(), vecS, vecC);

	s->store(vecS);
	c->store(vecC);
}

FORCEINLINE void __cdecl vecILMathSinCosPrecise3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resS, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resC)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* s	= (float3_internal*)resS;
	float3_internal* c	= (float3_internal*)resC;

	Vec vecS, vecC;
	vecSinCosPrecise(x->load(), vecS, vecC);

	s->store(vecS);
	c->store(vecC);
}

FORCEINLINE void __cdecl vecILMathSinCosPrecise4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resS, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resC)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* s	= (float4_internal*)resS;
	float4_internal* c	= (float4_internal*)resC;

	Vec vecS, vecC;
	vecSinCosPrecise(x->load(), vecS, vecC);

	s->store(vecS);
	c->store(vecC);
}

FORCEINLINE float __cdecl vecILMathSinPrecise(float x)
{
	Vec vecS, vecC;
	vecSinCosPrecise(vec(x), vecS, vecC);

	return vecStore1(vecS);
}

FORCEINLINE void __cdecl vecILMathSinPrecise2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	Vec vecS, vecC;
	vecSinCosPrecise(x->load(), vecS, vecC);

	res->store(vecS);
}

FORCEINLINE void __cdecl vecILMathSinPrecise3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	Vec vecS, vecC;
	vecSinCosPrecise(x->load(), vecS, vecC);

	res->store(vecS);
}

FORCEINLINE void __cdecl vecILMathSinPrecise4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	Vec vecS, vecC;
	vecSinCosPrecise(x->load(), vecS, vecC);

	res->store(vecS);
}

FORCEINLINE float __cdecl vecILMathCosPrecise(float x)
{
	Vec vecS, vecC;
	vecSinCosPrecise(vec(x), vecS, vecC);

	return vecStore1(vecC);
}

FORCEINLINE void __cdecl vecILMathCosPrecise2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	Vec vecS, vecC;
	vecSinCosPrecise(x->load(), vecS, vecC);

	res->store(vecC);
}

FORCEINLINE void __cdecl vecILMathCosPrecise3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	Vec vecS, vecC;
	vecSinCosPrecise(x->load(), vecS, vecC);

	res->store(vecC);
}

FORCEINLINE void __cdecl vecILMathCosPrecise4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	Vec vecS, vecC;
	vecSinCosPrecise(x->load(), vecS, vecC);

	res->store(vecC);
}

FORCEINLINE float __cdecl vecILMathTan(float x)
{
	return vecStore1(vecTan(vec(x)));
}

FORCEINLINE void __cdecl vecILMathTan2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecTan(x->load()));
}

FORCEINLINE void __cdecl vecILMathTan3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecTan(x->load()));
}

FORCEINLINE void __cdecl vecILMathTan4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecTan(x->load()));
}

FORCEINLINE float __cdecl vecILMathASin(float x)
{
	return vecStore1(vecASin(vec(x)));
}

FORCEINLINE void __cdecl vecILMathASin2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecASin(x->load()));
}

FORCEINLINE void __cdecl vecILMathASin3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecASin(x->load()));
}

FORCEINLINE void __cdecl vecILMathASin4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecASin(x->load()));
}

FORCEINLINE float __cdecl vecILMathACos(float x)
{
	return vecStore1(vecACos(vec(x)));
}

FORCEINLINE void __cdecl vecILMathACos2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecACos(x->load()));
}

FORCEINLINE void __cdecl vecILMathACos3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecACos(x->load()));
}

FORCEINLINE void __cdecl vecILMathACos4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecACos(x->load()));
}

FORCEINLINE float __cdecl vecILMathATan(float x)
{
	return vecStore1(vecATan(vec(x)));
}

FORCEINLINE void __cdecl vecILMathATan_2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecATan(x->load()));
}

FORCEINLINE void __cdecl vecILMathATan_3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecATan(x->load()));
}

FORCEINLINE void __cdecl vecILMathATan_4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecATan(x->load()));
}

FORCEINLINE float __cdecl vecILMathATanEst(float x)
{
	return vecStore1(vecATanEst(vec(x)));
}

FORCEINLINE void __cdecl vecILMathATanEst2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecATanEst(x->load()));
}

FORCEINLINE void __cdecl vecILMathATanEst3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecATanEst(x->load()));
}

FORCEINLINE void __cdecl vecILMathATanEst4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecATanEst(x->load()));
}

FORCEINLINE float __cdecl vecILMathATan2(float y, float x)
{
	return vecStore1(vecATan2(vec(y), vec(x)));
}

FORCEINLINE void __cdecl vecILMathATan2_2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inY, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* y 	= (float2_internal*)inY;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecATan2(y->load(), x->load()));
}

FORCEINLINE void __cdecl vecILMathATan2_3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inY, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* y 	= (float3_internal*)inY;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecATan2(y->load(), x->load()));
}

FORCEINLINE void __cdecl vecILMathATan2_4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inY, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* y 	= (float4_internal*)inY;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecATan2(y->load(), x->load()));
}

FORCEINLINE float __cdecl vecILMathATan2Est(float y, float x)
{
	return vecStore1(vecATan2Est(vec(y), vec(x)));
}

FORCEINLINE void __cdecl vecILMathATan2Est2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inY, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* y 	= (float2_internal*)inY;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecATan2Est(y->load(), x->load()));
}

FORCEINLINE void __cdecl vecILMathATan2Est3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inY, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* y 	= (float3_internal*)inY;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecATan2Est(y->load(), x->load()));
}

FORCEINLINE void __cdecl vecILMathATan2Est4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inY, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* y 	= (float4_internal*)inY;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecATan2Est(y->load(), x->load()));
}


FORCEINLINE float __cdecl vecILMathLog(float x)
{
	return vecStore1(vecLog(vec(x)));
}

FORCEINLINE void __cdecl vecILMathLog_2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecLog(x->load()));
}

FORCEINLINE void __cdecl vecILMathLog_3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecLog(x->load()));
}

FORCEINLINE void __cdecl vecILMathLog_4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecLog(x->load()));
}

FORCEINLINE float __cdecl vecILMathLog2(float x)
{
	return vecStore1(vecLog2(vec(x)));
}

FORCEINLINE void __cdecl vecILMathLog2_2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecLog2(x->load()));
}

FORCEINLINE void __cdecl vecILMathLog2_3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecLog2(x->load()));
}

FORCEINLINE void __cdecl vecILMathLog2_4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecLog2(x->load()));
}

FORCEINLINE float __cdecl vecILMathLog10(float x)
{
	return vecStore1(vecLog10(vec(x)));
}

FORCEINLINE void __cdecl vecILMathLog10_2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecLog10(x->load()));
}

FORCEINLINE void __cdecl vecILMathLog10_3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecLog10(x->load()));
}

FORCEINLINE void __cdecl vecILMathLog10_4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecLog10(x->load()));
}

FORCEINLINE float __cdecl vecILMathExp(float x)
{
	return vecStore1(vecExp(vec(x)));
}

FORCEINLINE void __cdecl vecILMathExp_2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecExp(x->load()));
}

FORCEINLINE void __cdecl vecILMathExp_3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecExp(x->load()));
}

FORCEINLINE void __cdecl vecILMathExp_4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecExp(x->load()));
}

FORCEINLINE float __cdecl vecILMathExp2(float x)
{
	return vecStore1(vecExp2(vec(x)));
}

FORCEINLINE void __cdecl vecILMathExp2_2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecExp2(x->load()));
}

FORCEINLINE void __cdecl vecILMathExp2_3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecExp2(x->load()));
}

FORCEINLINE void __cdecl vecILMathExp2_4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecExp2(x->load()));
}

FORCEINLINE float __cdecl vecILMathExp10(float x)
{
	return vecStore1(vecExp10(vec(x)));
}

FORCEINLINE void __cdecl vecILMathExp10_2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecExp10(x->load()));
}

FORCEINLINE void __cdecl vecILMathExp10_3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecExp10(x->load()));
}

FORCEINLINE void __cdecl vecILMathExp10_4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecExp10(x->load()));
}

FORCEINLINE float __cdecl vecILMathPow(float x, float p)
{
	return vecStore1(vecPow(vec(x), vec(p)));
}

FORCEINLINE void __cdecl vecILMathPow2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inP, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* p 	= (float2_internal*)inP;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecPow(x->load(), p->load()));
}

FORCEINLINE void __cdecl vecILMathPow3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inP, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* p 	= (float3_internal*)inP;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecPow(x->load(), p->load()));
}

FORCEINLINE void __cdecl vecILMathPow4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inP, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* p 	= (float4_internal*)inP;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecPow(x->load(), p->load()));
}

FORCEINLINE float __cdecl vecILMathSqrt(float x)
{
	return vecStore1(vecSqrt1(vec1(x)));
}

FORCEINLINE void __cdecl vecILMathSqrt2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecSqrt(x->load()));
}

FORCEINLINE void __cdecl vecILMathSqrt3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecSqrt(x->load()));
}

FORCEINLINE void __cdecl vecILMathSqrt3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inX, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* x 	= (float3a_internal*)inX;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecSqrt(x->load()));
}

FORCEINLINE void __cdecl vecILMathSqrt4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecSqrt(x->load()));
}

FORCEINLINE float __cdecl vecILMathRsqrt(float x)
{
	return vecStore1(vecRecipSqrt1(vec1(x)));
}

FORCEINLINE void __cdecl vecILMathRsqrt2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecRecipSqrt(x->load()));
}

FORCEINLINE void __cdecl vecILMathRsqrt3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecRecipSqrt(x->load()));
}

FORCEINLINE void __cdecl vecILMathRsqrt3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inX, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* x 	= (float3a_internal*)inX;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecRecipSqrt(x->load()));
}

FORCEINLINE void __cdecl vecILMathRsqrt4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecRecipSqrt(x->load()));
}

FORCEINLINE uint __cdecl vecILMathF32tof16(float x)
{
	return (uint)vecStore1(vecF32tof16(vec(x)));
}

FORCEINLINE uint __cdecl vecILMathF32tof16_2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX)
{
	float2_internal* x 	= (float2_internal*)inX;

	return (uint)vecStore1(vecPackU32ToU16(vecF32tof16(x->load())));
}

FORCEINLINE void __cdecl vecILMathF32tof16_3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, uint2_t157753816C23B82EB918C3D3AFCFDDE06A04C05F* RESTRICT resU)
{
	float3_internal* x 	= (float3_internal*)inX;
	uint2_internal* res = (uint2_internal*)resU;

	IntVec packedRes = vecPackU32ToU16(vecF32tof16(x->load()));

	vecStore2Unaligned(packedRes, (int*)res);
}

FORCEINLINE void __cdecl vecILMathF32tof16_4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, uint2_t157753816C23B82EB918C3D3AFCFDDE06A04C05F* RESTRICT resU)
{
	float4_internal* x 	= (float4_internal*)inX;
	uint2_internal* res = (uint2_internal*)resU;

	IntVec packedRes = vecPackU32ToU16(vecF32tof16(x->load()));

	vecStore2Unaligned(packedRes, (int*)res);
}

FORCEINLINE float __cdecl vecILMathF16tof32(uint x)
{
	return vecStore1(vecF16tof32(intVec(x)));
}

FORCEINLINE void __cdecl vecILMathF16tof32_2(uint x, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* res = (float2_internal*)resF;

	res->store(vecF16tof32(vecUnpackU16ToU32(intVec(x))));
}

FORCEINLINE void __cdecl vecILMathF16tof32_3(uint2_t157753816C23B82EB918C3D3AFCFDDE06A04C05F* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	uint2_internal* x = (uint2_internal*)inX;
	float3_internal* res = (float3_internal*)resF;

	IntVec vecX = intVec(x->x, x->y, 0, 0);
	res->store(vecF16tof32(vecUnpackU16ToU32(vecX)));
}

FORCEINLINE void __cdecl vecILMathF16tof32_4(uint2_t157753816C23B82EB918C3D3AFCFDDE06A04C05F* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	uint2_internal* x = (uint2_internal*)inX;
	float4_internal* res = (float4_internal*)resF;

	IntVec vecX = intVec(x->x, x->y, 0, 0);
	res->store(vecF16tof32(vecUnpackU16ToU32(vecX)));
}

FORCEINLINE float __cdecl vecILMathFloor(float x)
{
	return vecStore1(vecFloor(vec(x)));
}

FORCEINLINE void __cdecl vecILMathFloor2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecFloor(x->load()));
}

FORCEINLINE void __cdecl vecILMathFloor3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecFloor(x->load()));
}

FORCEINLINE void __cdecl vecILMathFloor3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inX, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* x 	= (float3a_internal*)inX;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecFloor(x->load()));
}

FORCEINLINE void __cdecl vecILMathFloor4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecFloor(x->load()));
}

FORCEINLINE float __cdecl vecILMathCeil(float x)
{
	return vecStore1(vecCeil(vec(x)));
}

FORCEINLINE void __cdecl vecILMathCeil2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecCeil(x->load()));
}

FORCEINLINE void __cdecl vecILMathCeil3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecCeil(x->load()));
}

FORCEINLINE void __cdecl vecILMathCeil3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inX, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* x 	= (float3a_internal*)inX;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecCeil(x->load()));
}

FORCEINLINE void __cdecl vecILMathCeil4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecCeil(x->load()));
}

FORCEINLINE float __cdecl vecILMathRound(float x)
{
	return vecStore1(vecRound(vec(x)));
}

FORCEINLINE void __cdecl vecILMathRound2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecRound(x->load()));
}

FORCEINLINE void __cdecl vecILMathRound3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecRound(x->load()));
}

FORCEINLINE void __cdecl vecILMathRound3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inX, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* x 	= (float3a_internal*)inX;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecRound(x->load()));
}

FORCEINLINE void __cdecl vecILMathRound4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecRound(x->load()));
}

FORCEINLINE float __cdecl vecILMathTrunc(float x)
{
	return vecStore1(vecTrunc(vec(x)));
}

FORCEINLINE void __cdecl vecILMathTrunc2(float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT inX, float2_t24AA5C0F612B0672315EDAFEC9D9E7F1C4A5B0BA* RESTRICT resF)
{
	float2_internal* x 	= (float2_internal*)inX;
	float2_internal* res= (float2_internal*)resF;

	res->store(vecTrunc(x->load()));
}

FORCEINLINE void __cdecl vecILMathTrunc3(float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT inX, float3_t4AB5D88249ADB24F69FFD0793E8ED25E1CC3745E* RESTRICT resF)
{
	float3_internal* x 	= (float3_internal*)inX;
	float3_internal* res= (float3_internal*)resF;

	res->store(vecTrunc(x->load()));
}

FORCEINLINE void __cdecl vecILMathTrunc3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inX, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* x 	= (float3a_internal*)inX;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecTrunc(x->load()));
}

FORCEINLINE void __cdecl vecILMathTrunc4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecTrunc(x->load()));
}

FORCEINLINE void __cdecl vecILMathAbs3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inX, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* x 	= (float3a_internal*)inX;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecAbs(x->load()));
}

FORCEINLINE void __cdecl vecILMathAbs4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inX, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* x 	= (float4_internal*)inX;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecAbs(x->load()));
}

FORCEINLINE void __cdecl vecILMathFloat3aAdd3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inA, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* a 	= (float3a_internal*)inA;
	float3a_internal* b 	= (float3a_internal*)inB;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecAdd(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat4Add4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inA, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* a 	= (float4_internal*)inA;
	float4_internal* b 	= (float4_internal*)inB;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecAdd(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat3aAdd(float inA, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* b 	= (float3a_internal*)inB;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecAdd(vec(inA), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat4Add(float inA, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* b 	= (float4_internal*)inB;
	float4_internal* res= (float4_internal*)resF;

	res->store(vecAdd(vec(inA), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat3aSub3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inA, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* a 	= (float3a_internal*)inA;
	float3a_internal* b 	= (float3a_internal*)inB;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecSub(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat4Sub4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inA, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* a 		= (float4_internal*)inA;
	float4_internal* b 		= (float4_internal*)inB;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecSub(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat3aSub(float inA, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* b 	= (float3a_internal*)inB;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecSub(vec(inA), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat4Sub(float inA, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* b 		= (float4_internal*)inB;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecSub(vec(inA), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat4SubF(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inA, float inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* a 		= (float4_internal*)inA;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecSub(a->load(), vec(inB)));
}

FORCEINLINE void __cdecl vecILMathFloat3aSubF(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inA, float inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* a 	= (float3a_internal*)inA;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecSub(a->load(), vec(inB)));
}

FORCEINLINE void __cdecl vecILMathFloat3aMul3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inA, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* a 	= (float3a_internal*)inA;
	float3a_internal* b 	= (float3a_internal*)inB;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecMul(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat4Mul4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inA, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* a 		= (float4_internal*)inA;
	float4_internal* b 		= (float4_internal*)inB;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecMul(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat3aMul(float inA, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* b 	= (float3a_internal*)inB;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecMul(vec(inA), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat4Mul(float inA, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* b 		= (float4_internal*)inB;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecMul(vec(inA), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat3aDiv3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inA, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* a 	= (float3a_internal*)inA;
	float3a_internal* b 	= (float3a_internal*)inB;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecDiv(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat4Div4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inA, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* a 		= (float4_internal*)inA;
	float4_internal* b 		= (float4_internal*)inB;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecDiv(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat3aDiv(float inA, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* b 	= (float3a_internal*)inB;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecDiv(vec(inA), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat4Div(float inA, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* b 		= (float4_internal*)inB;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecDiv(vec(inA), b->load()));
}

FORCEINLINE void __cdecl vecILMathFloat3aDivF(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inA, float inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* a 	= (float3a_internal*)inA;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecDiv(a->load(), vec(inB)));
}

FORCEINLINE void __cdecl vecILMathFloat4DivF(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inA, float inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* a 		= (float4_internal*)inA;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecDiv(a->load(), vec(inB)));
}

FORCEINLINE void __cdecl vecILMathFloat3aNeg(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inA, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* a 	= (float3a_internal*)inA;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecNeg(a->load()));
}

FORCEINLINE void __cdecl vecILMathFloat4Neg(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inA, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* a 		= (float4_internal*)inA;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecNeg(a->load()));
}

FORCEINLINE void __cdecl vecILMathMin3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inA, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* a 	= (float3a_internal*)inA;
	float3a_internal* b 	= (float3a_internal*)inB;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecMin(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathMin4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inA, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* a 		= (float4_internal*)inA;
	float4_internal* b 		= (float4_internal*)inB;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecMin(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathMax3a(float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inA, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT inB, float3a_t4CE7BE4EE0FF9B76368D63B4AB7261DF9BE661C6* RESTRICT resF)
{
	float3a_internal* a 	= (float3a_internal*)inA;
	float3a_internal* b 	= (float3a_internal*)inB;
	float3a_internal* res	= (float3a_internal*)resF;

	res->store(vecMax(a->load(), b->load()));
}

FORCEINLINE void __cdecl vecILMathMax4(float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inA, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT inB, float4_t89D9A294E7A79BD81BFBDD18654508532958555E* RESTRICT resF)
{
	float4_internal* a 		= (float4_internal*)inA;
	float4_internal* b 		= (float4_internal*)inB;
	float4_internal* res	= (float4_internal*)resF;

	res->store(vecMax(a->load(), b->load()));
}

} // extern "C"
