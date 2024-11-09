// LogWriter.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "Log.h"
#include "Clock.h"
#include "Engine.h"
#include <iostream>
#include <fstream>
#include <string>
#include <iomanip>
#include <ctime>

#pragma warning(disable : 4996).

using namespace std;

int main()
{
   Clock c;
   string input;
   int index;
   bool running = true;

   //UPDATED / CHANGE CODE HERE
   // ***********************************
   // ***********************************
   // ***********************************
   // ADD WHERE MASTER FOLDER IS
   // EX: Engine e("../Test Folder Data/log_master.txt");
   Engine e("logs_master.txt");
   // ***********************************
   // ***********************************
   // ***********************************
   // ADD TARGET LOGS HERE
   // EX: e.AddLog("name", "location.txt");
   e.AddLog("Art Assets Log", "Art Assets/logs_artAssets.txt");
   e.AddLog("Documentation Log", "Documentation/logs_documentation.txt");
   e.AddLog("Builds Log", "Prototype/logs_builds.txt");
   e.AddLog("SFX Assets", "SFX Assets/logs_SFX.txt");
   // ***********************************
   // ***********************************
   // ***********************************

   e.GetUsername();
   while (running)
   {
	   Clock clock;
	   running = e.RunEngine();
   }
}
