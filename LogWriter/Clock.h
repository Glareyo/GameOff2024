#pragma once
#pragma warning(disable : 4996).

#include <string>
#include <ctime>

using namespace std;

class Clock
{
public:
	Clock();
	string GetCurrentDate();
	string GetCurrentTime();
private:
	time_t timestamp;
	tm* timeInfo;

	int year;
	int month;
	int day;
};