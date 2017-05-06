#include "stdafx.h"
#include "Util.h"

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

StatsApp::Util util;

extern "C" DLLEXPORT double Mean(int* items, const size_t length)
{
    return util.Mean(items, length);
}
extern "C" DLLEXPORT double Median(int* items, const size_t length)
{
    double median = RETURN_DOUBLE_ERROR;
    try
    {
        median = util.Median(items, length);
    } catch (std::runtime_error &e)
    {
        LOG_ERROR(e.what());
    }
    return median;
}
extern "C" DLLEXPORT double Mode(int* items, const size_t length)
{
    double mode = RETURN_DOUBLE_ERROR;
    try 
    {
        mode = util.Mode(items, length);
    } catch (std::runtime_error &e)
    {
        LOG_ERROR(e.what());
    }
    return mode;
}
extern "C" DLLEXPORT double Range(int* items, const size_t length)
{
    double range = RETURN_DOUBLE_ERROR;
    try 
    {
        range = util.Range(items, length);
    } catch (std::runtime_error &e)
    {
        LOG_ERROR(e.what());
    }
    return range;
}
extern "C" DLLEXPORT double UpperQuartile(int* items, const size_t length)
{
    double uq = RETURN_DOUBLE_ERROR;
    try
    {   
        uq = util.UpperQuartile(items, length);
    } catch (std::runtime_error &e)
    {
        LOG_ERROR(e.what());
    }
    return uq;
}
extern "C" DLLEXPORT double LowerQuartile(int* items, const size_t length)
{
    double lq = RETURN_DOUBLE_ERROR;
    try
    {
        lq = util.LowerQuartile(items, length);
    } catch (std::runtime_error &e)
    {
        LOG_ERROR(e.what());
    }
    return lq;
}