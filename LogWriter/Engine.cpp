#include "Engine.h"
#include "Log.h"
#include "Clock.h"
#include <string>
#include <iomanip>
#include <queue>
#include <iostream>

using namespace std;

Engine::Engine(string masterLogLoc)
{
	Log temp("Master Log",masterLogLoc);
	MasterLog = temp;
}

string Engine::DisplayLogList()
{
	int originalQueueSize = Logs.size();
	string list;
	queue<Log> tempLogs;

	for (int i = 1; i <= originalQueueSize; i++)
	{
		list += to_string(i) + ". " + Logs.front().GetLogName() + "\n";

		//Place log into temp queue
		tempLogs.push(Logs.front());
		//Pop log to go to next item.
		Logs.pop();
	}

	//Replace Logs with tempLogs.
	Logs.swap(tempLogs);

	return list;
}
void Engine::GetUsername()
{
	cout << "Please Enter Your UserName: ";
	getline(cin, userName);
	ClearScreen();
}

bool Engine::RunEngine()
{
	string input;
	int index;

	cout << "Selected an Option: \n";
	cout << "0. Exit\n";
	cout << DisplayLogList();

	cout << "Input: ";
	getline(cin, input);

	ClearScreen();

	try
	{
		//Validate and get input.
		index = SelectFromList(input);

		cout << "\n";

		if (index == -1)
		{//Invalidate input
			cout << "Invalid Input...\n";
		}
		else if (index == 0)
		{//Exit
			return false;
		}
		else
		{
			cout << "Adding Log: " << GetLogName(index);
			//Get Log text from user
			cout << "\nType Logs Here : ";
			getline(cin, input);

			UpdateLog(index, input, userName);
			ClearScreen();
		}
	}
	catch (const std::exception& e)
	{
		cout << "Invalid Input...\n";
	}

	return true;
}

int Engine::SelectFromList(string inputFromUser)
{
	int originalQueueSize = Logs.size();
	int input = stoi(inputFromUser);
	Log targetLog;
	queue<Log> tempLogs;

	if (input < 0 || input > originalQueueSize)
	{
		return -1;
	}

	return input;
}

void Engine::UpdateLog(int index, string input, string userName)
{
	int originalQueueSize = Logs.size();
	Log targetLog;
	queue<Log> tempLogs;
	
	Clock c;

	clock = c;

	string currentDate = clock.GetCurrentDate();
	string currentTime = clock.GetCurrentTime();

	for (int i = 1; i <= originalQueueSize; i++)
	{
		if (index == i)
		{
			//If log is the target log, grab it as a variable.
			targetLog = Logs.front();
		}
		//Place log into temp queue
		tempLogs.push(Logs.front());
		//Pop log to go to next item.
		Logs.pop();
	}

	//Replace Logs with tempLogs.
	Logs.swap(tempLogs);

	targetLog.AddToLog(input, userName, currentDate, currentTime);
	UpdateMasterLog(input, userName, currentDate, currentTime);
}
void Engine::AddLog(string logName, string logLoc)
{
	Log log(logName, logLoc);
	Logs.push(log);
}

void Engine::UpdateMasterLog(string input, string userName, string currentDate, string currentTime)
{
	MasterLog.AddToLog(input, userName, currentDate, currentTime);
}

string Engine::GetLogName(int index)
{
	int originalQueueSize = Logs.size();
	Log targetLog;
	queue<Log> tempLogs;

	for (int i = 1; i <= originalQueueSize; i++)
	{
		if (index == i)
		{
			//If log is the target log, grab it as a variable.
			targetLog = Logs.front();
		}
		//Place log into temp queue
		tempLogs.push(Logs.front());
		//Pop log to go to next item.
		Logs.pop();
	}

	//Replace Logs with tempLogs.
	Logs.swap(tempLogs);

	return targetLog.GetLogName();
}

void Engine::ClearScreen()
{
	std::cout << "\033[2J\033[1;1H";
}