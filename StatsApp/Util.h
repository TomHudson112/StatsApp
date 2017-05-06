#ifndef UTIL_H
#define UTIL_H

namespace StatsApp
{
	class Util
	{
	public:
        double Mean(int* items, const size_t length) const;
        double Median(int* items, const size_t length) const;

        /* Calculate the mode using std::map. This function should be used when the amount of numbers 
           in the items array is not excessively large, as every unique number is stored in memeory. 
           To calculate the mode of a large data set, use IterationMode, which calculates the mode using
           an iterative approach. */
        double Mode(int* items, const size_t length) const;             
        double IterationMode(int* items, size_t length) const;
        double Range(int* items, const size_t length) const;
        double UpperQuartile(int* items, const size_t length) const;
        double LowerQuartile(int* items, const size_t length) const;
	};
}

#endif // UTIL_H