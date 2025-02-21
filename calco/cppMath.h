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

template<typename T>
inline T* il2cpp_span_get_item(T* refPtrValue, int index, int length)
{
    return &refPtrValue[index];
}

// there is no other way to disable Spans bound checking in IL2CPP atm
// so obscure the original il2cpp_span_get_item function by changing type of its first argument to redirect all calls to the above one
// the 'refPtrValue' is uniquely used only at that place among the entire codebase (it might change in future Unity versions)
#define refPtrValue **refPtrValue

#endif
