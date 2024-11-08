#include "Clock.h"
#include <ctime>
#include <string>
#pragma warning(disable : 4996).


Clock::Clock()
{
    //Get timestamp
    timestamp = time(nullptr);
    //Get current localtime and date
    timeInfo = localtime(&timestamp);
}

string Clock::GetCurrentDate()
{
    //String to hold informtion
    string dateString;

    int year = timeInfo->tm_year + 1900;
    int month = timeInfo->tm_mon + 1;
    int day = timeInfo->tm_mday;

    //Add a zero if it is less than 10 (Change 1 to 01)
    dateString += to_string(year);
    if (month < 10)
    {
        dateString += "0";
    }
    dateString += to_string(month);

    //Add a zero if it is less than 10 (Change 1 to 01)
    if (day < 10)
    {
        dateString += "0";
    }
    dateString += to_string(day);

    return dateString;
}
string Clock::GetCurrentTime()
{
    //Holds string for time
    string timeString;

    //Get hour and Minute of the current time.
    int hour = timeInfo->tm_hour;
    int min = timeInfo->tm_min;
    
    //Add hour and min to output string
    if (hour < 10)
    {
        timeString += "0";
    }
    timeString += to_string(hour);
    
    timeString += ":";
    
    if (min < 10)
    {
        timeString += "0";
    }
    timeString += to_string(min);

    //return string
    return timeString;
}
