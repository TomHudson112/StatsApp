#include "stdafx.h"
#include "Util.h"
#include "Exception.h"

namespace StatsApp
{
    double Util::Mean(int* items, const size_t length) const
    {
        /* Calculate the mean of the items array. */
        int sum = 0;
        for (size_t i = 0; i < length; i++)
            sum += items[i];
        return (double)sum / (double)length;
    }

    double Util::Median(int* items, const size_t length) const
    {
        /* Return the median of the items array. */
        std::vector<int> v(items, items + length);
        if (v.empty())
            throw StatsAppBadMathsException("Items vector is empty when calculating the median.");

        size_t mid = length / 2;
        std::nth_element(v.begin(), v.begin() + mid, v.end());

        if (length % 2 == 0)
        {
            double ub = v[mid];
            std::nth_element(v.begin(), v.begin() + --mid, v.end());
            return (ub + (double)v[mid - 1]) / 2;
        }
        else
            return v[mid];
    }

    double Util::Mode(int* items, const size_t length) const
    {
        /* Return the mode of the items array using std::map. */
        std::vector<int> v(items, items + length);
        if (v.empty())
            throw StatsAppBadMathsException("Items vector is empty when calculating the mode.");

        std::map<int, int> count;  // <number, number of times it occurs>

        // calculate the number of occurences of each number
        for (size_t i = 0; i < v.size(); i++)
        {
            if (count.find(v[i]) == count.end())
                count.insert(std::make_pair(v[i], 1));
            else
            {
                count[v[i]] += 1;
            }
        }

        // calculate the mode
        std::map<int, int>::iterator it = count.begin();
        std::pair<int, int> mode = std::make_pair(it->first, it->second);
        for (++it; it != count.end(); ++it)
        {
            if (it->second > mode.second)
            {
                mode.first = it->first;
                mode.second = it->second;
            }
        }

        return mode.first;
    }

    double Util::IterationMode(int* items, size_t length) const
    {
        // TODO
        return RETURN_DOUBLE_ERROR;
    }

    double Util::Range(int* items, const size_t length) const
    {
        /* Return the largest number - the smallest number in the items array. */
        std::vector<int> v(items, items + length);
        if (v.empty())
            throw StatsAppBadMathsException("Items vector is empty when calculating the range");
        std::sort(v.begin(), v.end());
        return (double)v.back() - (double)v.front();
    }

    double Util::UpperQuartile(int* items, const size_t length) const
    {
        std::vector<int> v(items, items + length);
        if (v.empty())
            throw StatsAppBadMathsException("Items vector is empty when calculating the upper quartile.");
        std::sort(v.begin(), v.end());
        std::cout << length * 0.75 << std::endl;
        return (double)v[(int)ceil(length * 0.75) - 1];  // -1 to account for 0 based indexing
    }

    double Util::LowerQuartile(int* items, const size_t length) const
    {
        std::vector<int> v(items, items + length);
        if (v.empty())
            throw StatsAppBadMathsException("Items vector is empty when calculating the lower quartile.");
        std::sort(v.begin(), v.end());
        std::cout << length * 0.25 << std::endl;
        return (double)v[(int)ceil(length * 0.25)];
    }
}