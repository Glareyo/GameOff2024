#include "Log.h"
#include <iostream>
#include <fstream>
#include <string>
#include <iomanip>

using namespace std;

Log::Log()
{

}
Log::Log(string logName, string logLoc)
{
	LogName = logName;
	LogLocation = logLoc;
	GetDataFromLog();
}

string Log::GetLogName()
{
	return LogName;
}

void Log::AddToLog(string input, string userName,string currentDate, string currentTime)
{
	//Redo the last Updated string
	string LocalLastUpdatedString = currentDate;

	//Add last updated string to text
	string text = "Last Updated: \n";
	//Add last updated date.
	text += currentDate + "\n";
	//Get the rest of the data
	text += GetDataFromLog();

	//Check to see if the date needs to be added.
	if (LastUpdatedString != LocalLastUpdatedString)
	{
		text += "\n";
		text += currentDate;
		text += "\n";
		LastUpdatedString = LocalLastUpdatedString;
	}
	text += currentTime + "|" + userName + " - ";
	text += input;
	text += "\n";
	
	ofstream data(LogLocation);

	data << text;

	data.close();
}

string Log::GetDataFromLog()
{
	//Read data from data.txt
	ifstream data(LogLocation);
	string s;

	//String variable that will store all strings.
	string text;
	//Checks for the Last Updated Date
	bool SkippedUpdated = false;
	bool LastUpdatedDateObtained = false;

	//Run through each line in text file
	while (getline(data, s)) {
		if (!SkippedUpdated)
		{
			SkippedUpdated = true;
		}
		else if (!LastUpdatedDateObtained)
		{
			LastUpdatedDateObtained = true;
			LastUpdatedString = s;
		}
		else
		{
			text += s;
			text += "\n";
		}
	}

	data.close();
	return text;
}
