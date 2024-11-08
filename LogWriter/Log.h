#pragma once
#include <string>
#include <iostream>
#include <fstream>
#include <iomanip>

class Log
{
public:
	Log();
	Log(std::string logName, std::string logLoc);
	void AddToLog(std::string input, std::string userName, std::string currentDate, std::string currentTime);
	std::string GetLogName();
private:
	std::string LogName;
	std::string LogLocation; //Log Name
	std::string LastUpdatedString; //String of when the log was last updated.
	
	//Get the data from the log.
	std::string GetDataFromLog();
};