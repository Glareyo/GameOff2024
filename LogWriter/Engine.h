#pragma once

#include <string>
#include <iomanip>
#include <queue>
#include "Log.h"
#include "Clock.h"
#include <iostream>

using namespace std;

class Engine
{
public:
	Engine(string masterLogLoc);

	string DisplayLogList();
	void UpdateLog(int index, string input, string userName);
	void AddLog(string logName, string logLoc);
	int SelectFromList(string input);
	bool RunEngine();
	void GetUsername();
private:
	Clock clock;
	Log MasterLog;
	queue<Log> Logs;
	string userName;
	string GetLogName(int index);
	void UpdateMasterLog(string input, string userName, string currentDate, string currentTime);
	void ClearScreen();
};