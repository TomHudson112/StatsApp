#ifndef EXCEPTION
#define EXCEPTION

#include "stdafx.h"

class StatsAppBadMathsException : public std::runtime_error
{
    /* This exception is thrown when something goes wrong when calculating a value. */
public:
    StatsAppBadMathsException(const char* msg)
        : std::runtime_error(msg), m_msg(msg)
    {}

    virtual const char* what() const throw()
    {
        return m_msg;
    }

private:
    const char* m_msg;
};


#endif  // EXCEPTION