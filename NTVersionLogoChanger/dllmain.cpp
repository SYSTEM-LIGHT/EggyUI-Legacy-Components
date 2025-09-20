// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "pch.h"
#include "NTVersionLogoChanger.h"

// 全局变量定义
HMODULE g_hModule = NULL;
HBITMAP g_hLogoSmall = NULL;
HBITMAP g_hLogoMedium = NULL;
HBITMAP g_hLogoLarge = NULL;
//HHOOK g_hHook = NULL;
//HOOKPROC g_OldProc = NULL;

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        g_hModule = hModule;
        // 初始化临界区
        InitializeCriticalSection(&g_csLogoResources);
        // 延迟加载logo，避免在系统进程初始化时加载失败
        break;
    case DLL_THREAD_ATTACH:
        break;
    case DLL_THREAD_DETACH:
        break;
    case DLL_PROCESS_DETACH:
        // 首先卸载钩子
        UninstallHooks();
        
        // 等待钩子完全卸载（简单的延迟）
        Sleep(100);
        
        // 然后删除位图对象
        EnterCriticalSection(&g_csLogoResources);
        if (g_hLogoSmall) DeleteObject(g_hLogoSmall);
        if (g_hLogoMedium) DeleteObject(g_hLogoMedium);
        if (g_hLogoLarge) DeleteObject(g_hLogoLarge);
        LeaveCriticalSection(&g_csLogoResources);
        
        // 删除临界区
        DeleteCriticalSection(&g_csLogoResources);
        break;
    }
    return TRUE;
}

// 导出函数用于手动安装钩子
extern "C" NTVERSIONLOGOCHANGER_API BOOL __stdcall InstallLogoChanger()
{
    if (!LoadLogoImages()) {
        LogError(_T("LoadLogoImages (InstallLogoChanger)"));
        return FALSE;
    }
    
    if (!InstallHooks())
    {
        LogError(_T("InstallHooks (InstallLogoChanger)"));
        return FALSE;
    }
    
    return TRUE;
}

// DllRegisterServer函数实现 - 增强错误处理
extern "C" NTVERSIONLOGOCHANGER_API HRESULT __stdcall DllRegisterServer()
{
    // 获取DLL路径
    WCHAR szDllPath[MAX_PATH] = { 0 };
    if (GetModuleFileName(g_hModule, szDllPath, MAX_PATH) == 0)
    {
        LogError(_T("GetModuleFileName (DllRegisterServer)"));
        return E_FAIL;
    }

    // 在注册表中添加AppInit_DLLs条目
    HKEY hKey = NULL;
    LONG lResult = RegCreateKeyEx(HKEY_LOCAL_MACHINE, 
        L"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Windows",
        0, NULL, REG_OPTION_NON_VOLATILE, KEY_SET_VALUE | KEY_WOW64_64KEY, NULL, &hKey, NULL);
    
    if (lResult != ERROR_SUCCESS)
    {
        LogError(_T("RegCreateKeyEx (64-bit)"), lResult);
        return HRESULT_FROM_WIN32(lResult);
    }

    // 存储DLL路径到AppInit_DLLs
    lResult = RegSetValueEx(hKey, L"AppInit_DLLs", 0, REG_SZ, (LPBYTE)szDllPath, 
        (lstrlen(szDllPath) + 1) * sizeof(WCHAR));
    
    if (lResult != ERROR_SUCCESS)
    {
        LogError(_T("RegSetValueEx (AppInit_DLLs)"), lResult);
        RegCloseKey(hKey);
        return HRESULT_FROM_WIN32(lResult);
    }
    
    // 确保LoadAppInit_DLLs设置为1
    DWORD dwLoadAppInit_DLLs = 1;
    lResult = RegSetValueEx(hKey, L"LoadAppInit_DLLs", 0, REG_DWORD, 
        (LPBYTE)&dwLoadAppInit_DLLs, sizeof(dwLoadAppInit_DLLs));
    
    if (lResult != ERROR_SUCCESS)
    {
        LogError(_T("RegSetValueEx (LoadAppInit_DLLs)"), lResult);
        RegCloseKey(hKey);
        return HRESULT_FROM_WIN32(lResult);
    }
    
    // 设置RequireSignedAppInit_DLLs为0（允许未签名DLL）
    DWORD dwRequireSigned = 0;
    lResult = RegSetValueEx(hKey, L"RequireSignedAppInit_DLLs", 0, REG_DWORD, 
        (LPBYTE)&dwRequireSigned, sizeof(dwRequireSigned));
    
    RegCloseKey(hKey);
    
    if (lResult != ERROR_SUCCESS)
    {
        LogError(_T("RegSetValueEx (RequireSignedAppInit_DLLs)"), lResult);
        return HRESULT_FROM_WIN32(lResult);
    }
    
    // 同时注册到Wow6432Node（64位系统上的32位应用）
    lResult = RegCreateKeyEx(HKEY_LOCAL_MACHINE, 
        L"SOFTWARE\\Wow6432Node\\Microsoft\\Windows NT\\CurrentVersion\\Windows",
        0, NULL, REG_OPTION_NON_VOLATILE, KEY_SET_VALUE, NULL, &hKey, NULL);
    
    if (lResult == ERROR_SUCCESS) 
    {
        lResult = RegSetValueEx(hKey, L"AppInit_DLLs", 0, REG_SZ, (LPBYTE)szDllPath, 
            (lstrlen(szDllPath) + 1) * sizeof(WCHAR));
        
        if (lResult == ERROR_SUCCESS)
        {
            lResult = RegSetValueEx(hKey, L"LoadAppInit_DLLs", 0, REG_DWORD, 
                (LPBYTE)&dwLoadAppInit_DLLs, sizeof(dwLoadAppInit_DLLs));
        }
        
        if (lResult == ERROR_SUCCESS)
        {
            lResult = RegSetValueEx(hKey, L"RequireSignedAppInit_DLLs", 0, REG_DWORD, 
                (LPBYTE)&dwRequireSigned, sizeof(dwRequireSigned));
        }
        
        if (lResult != ERROR_SUCCESS)
        {
            LogError(_T("RegSetValueEx (Wow6432Node)"), lResult);
        }
        
        RegCloseKey(hKey);
    }
    else
    {
        LogError(_T("RegCreateKeyEx (Wow6432Node)"), lResult);
        // 注意：Wow6432Node注册失败不应阻止主功能
    }
    
    // 显示成功消息
    MessageBox(NULL, L"NTVersionLogoChanger 注册成功！\n请重启系统以应用更改。", L"注册成功", MB_ICONINFORMATION);
    
    return S_OK;
}

// DllUnregisterServer函数实现 - 增强错误处理
extern "C" NTVERSIONLOGOCHANGER_API HRESULT __stdcall DllUnregisterServer()
{
    BOOL bMainKeySuccess = FALSE;
    
    // 处理主注册表项
    HKEY hKey = NULL;
    LONG lResult = RegOpenKeyEx(HKEY_LOCAL_MACHINE, 
        L"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Windows",
        0, KEY_SET_VALUE | KEY_WOW64_64KEY, &hKey);
    
    if (lResult == ERROR_SUCCESS)
    {
        // 清空AppInit_DLLs值
        lResult = RegSetValueEx(hKey, L"AppInit_DLLs", 0, REG_SZ, NULL, 0);
        
        if (lResult == ERROR_SUCCESS)
        {
            // 恢复默认设置
            DWORD dwLoadAppInit_DLLs = 0;
            lResult = RegSetValueEx(hKey, L"LoadAppInit_DLLs", 0, REG_DWORD, 
                (LPBYTE)&dwLoadAppInit_DLLs, sizeof(dwLoadAppInit_DLLs));
        }
        
        if (lResult == ERROR_SUCCESS)
        {
            DWORD dwRequireSigned = 1;
            lResult = RegSetValueEx(hKey, L"RequireSignedAppInit_DLLs", 0, REG_DWORD, 
                (LPBYTE)&dwRequireSigned, sizeof(dwRequireSigned));
        }
        
        RegCloseKey(hKey);
        bMainKeySuccess = (lResult == ERROR_SUCCESS);
        
        if (!bMainKeySuccess)
        {
            LogError(_T("RegSetValueEx (Main Key Unregister)"), lResult);
        }
    }
    else
    {
        LogError(_T("RegOpenKeyEx (Main Key Unregister)"), lResult);
    }
    
    // 同时清理Wow6432Node
    BOOL bWow64KeySuccess = FALSE;
    lResult = RegOpenKeyEx(HKEY_LOCAL_MACHINE, 
        L"SOFTWARE\\Wow6432Node\\Microsoft\\Windows NT\\CurrentVersion\\Windows",
        0, KEY_SET_VALUE, &hKey);
    
    if (lResult == ERROR_SUCCESS)
    {
        lResult = RegSetValueEx(hKey, L"AppInit_DLLs", 0, REG_SZ, NULL, 0);
        
        if (lResult == ERROR_SUCCESS)
        {
            DWORD dwLoadAppInit_DLLs = 0;
            lResult = RegSetValueEx(hKey, L"LoadAppInit_DLLs", 0, REG_DWORD, 
                (LPBYTE)&dwLoadAppInit_DLLs, sizeof(dwLoadAppInit_DLLs));
        }
        
        RegCloseKey(hKey);
        bWow64KeySuccess = (lResult == ERROR_SUCCESS);
        
        if (!bWow64KeySuccess)
        {
            LogError(_T("RegSetValueEx (Wow6432Node Unregister)"), lResult);
        }
    }
    else
    {
        LogError(_T("RegOpenKeyEx (Wow6432Node Unregister)"), lResult);
    }
    
    // 通知外壳刷新
    SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, NULL, NULL);
    
    // 显示成功消息
    MessageBox(NULL, L"NTVersionLogoChanger 注销成功！\n请重启系统以移除更改。", L"注销成功", MB_ICONINFORMATION);
    
    // 如果两个注册表操作都失败，才返回错误
    if (!bMainKeySuccess && !bWow64KeySuccess)
    {
        return E_FAIL;
    }
    
    return S_OK;
}