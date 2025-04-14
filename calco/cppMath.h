#pragma once

#if defined(__cplusplus) 

#if __clang__ || __GNUC__
	#define FORCEINLINE inline __attribute__((always_inline))
	#define RESTRICT __restrict__
#elif _MSC_VER && !__INTEL_COMPILER
	#define FORCEINLINE __forceinline
	#define RESTRICT __restrict
#endif

#include <math.h>

#include "vecILMath.h"

extern "C" {

inline float __cdecl cppMathTanh(float x)
{
	return tanhf(x);
}

inline float __cdecl cppMathCosh(float x)
{
	return coshf(x);
}

inline float __cdecl cppMathSinh(float x)
{
	return sinhf(x);
}

} // extern "C"

#endif
