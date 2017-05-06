// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently.

#ifndef STDAFX_H
#define STDAFX_H

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
#include <windows.h>
#include <iostream>
#include <assert.h>
#include <vector>
#include <algorithm>
#include <math.h>
#include <map>
#include <math.h>
#include <exception>
#include <stdexcept>

#define DLLEXPORT	_declspec(dllexport)

#ifndef NDEBUG
    #define DEBUG
#endif

#ifdef DEBUG
    #define LOG_MESSAGE(msg) (std::cout << msg << std::endl)
    #define LOG_ERROR(msg) (std::cout << "ERROR: " << msg << ". Line: " << __LINE__ << ", file: " << __FILE__ << std::endl)
    #define LOG_WARNING(msg) (std::cout << "Warning: " << msg << ". Line: " << __LINE__ << ", file: " << __FILE__ << std::endl )
#else
    #define LOG_MESSAGE(msg)
    #define LOG_ERROR(msg)
    #define LOG_WARNING(msg)
#endif

const int    RETURN_INT_ERROR    = -111111;  // return this when the function is returning after an error, or when needing a default value
const double RETURN_DOUBLE_ERROR = -1.1111;  // return this when the function is returning after an error, or when needing a default value

#endif  // STDAFX_H