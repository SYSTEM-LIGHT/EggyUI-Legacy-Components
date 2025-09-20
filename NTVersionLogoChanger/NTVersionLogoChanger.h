// 修改头文件，移除不需要的声明
#pragma once

#ifdef NTVERSIONLOGOCHANGER_EXPORTS
#define NTVERSIONLOGOCHANGER_API __declspec(dllexport)
#else
#define NTVERSIONLOGOCHANGER_API __declspec(dllimport)
#endif

#include <windows.h>
#include <tchar.h>

// 导出DllRegisterServer和DllUnregisterServer函数以支持Regsvr32注册
extern "C" NTVERSIONLOGOCHANGER_API HRESULT __stdcall DllRegisterServer();
extern "C" NTVERSIONLOGOCHANGER_API HRESULT __stdcall DllUnregisterServer();

// 导出手动安装函数
extern "C" NTVERSIONLOGOCHANGER_API BOOL __stdcall InstallLogoChanger();

// 钩子相关函数声明
bool InstallHooks();
void UninstallHooks();
HICON BitmapToIcon(HBITMAP hBitmap);

// 图像加载和处理函数
bool LoadLogoImages();
HBITMAP ResizeBitmap(HBITMAP hOriginal, int width, int height);

// 工具函数声明
void LogError(LPCTSTR szFunction, DWORD dwError = 0);

// 全局变量
extern HMODULE g_hModule;
extern HBITMAP g_hLogoSmall;
extern HBITMAP g_hLogoMedium;
extern HBITMAP g_hLogoLarge;
extern HHOOK g_hHook;
extern HOOKPROC g_OldProc;
extern CRITICAL_SECTION g_csLogoResources;