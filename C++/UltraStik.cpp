// UltraStik.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "windows.h"
#include "UltraStik.h"

int main(int argc, char* argv[])
{
	int deviceCount = UltraStik_Initialize();

	for(int i=0; i<deviceCount; i++)
		UltraStik_LoadMap(i, "mouse");

	UltraStik_Shutdown();

	return 0;
}
