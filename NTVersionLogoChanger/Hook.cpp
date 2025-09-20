// 完全替换Hook.cpp文件内容
#include "pch.h"
#include "NTVersionLogoChanger.h"

// SC_ABOUTBOX常量定义
#ifndef SC_ABOUTBOX
#define SC_ABOUTBOX 0xF100
#endif

// 全局变量定义
HOOKPROC g_OldProc = NULL;
HHOOK g_hHook = NULL;
CRITICAL_SECTION g_csLogoResources;

// 日志函数实现
void LogError(LPCTSTR szFunction, DWORD dwError)
{
    if (dwError == 0)
        dwError = GetLastError();
        
    TCHAR szError[1024] = {0};
    _stprintf_s(szError, _countof(szError), _T("%s failed with error %lu\n"), szFunction, dwError);
    OutputDebugString(szError);
}

// 改进的BitmapToIcon函数 - 修复资源泄漏风险
HICON BitmapToIcon(HBITMAP hBitmap)
{
    if (hBitmap == nullptr) {
        return nullptr;
    }
    
    // 获取位图信息
    BITMAP bmp = {0};
    if (GetObject(hBitmap, sizeof(BITMAP), &bmp) == 0)
    {
        LogError(_T("GetObject"));
        return nullptr;
    }
    
    // 创建兼容DC
    HDC hDC = GetDC(NULL);
    if (hDC == nullptr)
    {
        LogError(_T("GetDC"));
        return nullptr;
    }
    
    HDC hMemDC = CreateCompatibleDC(hDC);
    if (hMemDC == nullptr)
    {
        LogError(_T("CreateCompatibleDC"));
        ReleaseDC(NULL, hDC);
        return nullptr;
    }
    
    ReleaseDC(NULL, hDC);
    
    // 创建掩码位图
    HBITMAP hMask = CreateBitmap(bmp.bmWidth, bmp.bmHeight, 1, 1, NULL);
    if (hMask == nullptr)
    {
        LogError(_T("CreateBitmap"));
        DeleteDC(hMemDC);
        return nullptr;
    }
    
    // 设置图标信息
    ICONINFO iconInfo = {0};
    iconInfo.fIcon = TRUE;
    iconInfo.hbmColor = hBitmap;
    iconInfo.hbmMask = hMask;
    
    // 创建图标
    HICON hIcon = CreateIconIndirect(&iconInfo);
    
    // 清理资源 - 无论图标创建是否成功，都要释放资源
    DeleteObject(hMask);
    DeleteDC(hMemDC);
    
    if (hIcon == nullptr)
    {
        LogError(_T("CreateIconIndirect"));
    }
    
    return hIcon;
}

// 钩子过程 - 扩展图标替换逻辑并确保资源安全
LRESULT CALLBACK LogoChangerHookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
    if (nCode >= 0)
    {
        // 检查是否是WH_CALLWNDPROC钩子
        if (nCode == HC_ACTION)
        {
            CWPSTRUCT* pCwp = (CWPSTRUCT*)lParam;
            
            // 检查是否是WM_SYSCOMMAND消息，可能与About对话框相关
            if (pCwp->message == WM_SYSCOMMAND && (pCwp->wParam & 0xFFF0) == SC_ABOUTBOX)
            {
                // 延迟加载logo（如果尚未加载）
                EnterCriticalSection(&g_csLogoResources);
                if (g_hLogoLarge == nullptr) {
                    LeaveCriticalSection(&g_csLogoResources);
                    LoadLogoImages();
                    EnterCriticalSection(&g_csLogoResources);
                }
                LeaveCriticalSection(&g_csLogoResources);
            }
            
            // 检查窗口是否是About对话框
            HWND hWnd = pCwp->hwnd;
            WCHAR szClassName[256] = {0};
            if (GetClassName(hWnd, szClassName, ARRAYSIZE(szClassName)) > 0)
            {
                // 典型的About对话框类名通常包含"#32770"（对话框类）
                if (wcscmp(szClassName, L"#32770") == 0)
                {
                    WCHAR szTitle[256] = {0};
                    if (GetWindowText(hWnd, szTitle, ARRAYSIZE(szTitle)) > 0)
                    {
                        // 扩展检测逻辑：检查窗口标题是否包含"关于"、"About"或其他常见关键词
                        if (wcsstr(szTitle, L"关于") != nullptr || 
                            wcsstr(szTitle, L"About") != nullptr ||
                            wcsstr(szTitle, L"关于") != nullptr)
                        {
                            // 尝试替换对话框中的图标
                            EnterCriticalSection(&g_csLogoResources);
                            HBITMAP hLogoLarge = g_hLogoLarge; // 临时保存指针以减少临界区占用时间
                            LeaveCriticalSection(&g_csLogoResources);
                            
                            if (hLogoLarge != nullptr)
                            {
                                // 查找对话框中的图标控件
                                HWND hIconWnd = FindWindowEx(hWnd, NULL, L"Static", NULL);
                                while (hIconWnd != NULL)
                                {
                                    // 检查控件是否是图标
                                    LONG_PTR style = GetWindowLongPtr(hIconWnd, GWL_STYLE);
                                    if ((style & SS_ICON) == SS_ICON)
                                    {
                                        // 创建自定义图标并设置
                                        HICON hCustomIcon = BitmapToIcon(hLogoLarge);
                                        if (hCustomIcon)
                                        {
                                            // 获取旧图标并释放
                                            HICON hOldIcon = (HICON)SendMessage(hIconWnd, STM_GETICON, 0, 0);
                                            SendMessage(hIconWnd, STM_SETICON, (WPARAM)hCustomIcon, 0);
                                            if (hOldIcon)
                                                DestroyIcon(hOldIcon);
                                        }
                                        break;
                                    }
                                    hIconWnd = FindWindowEx(hWnd, hIconWnd, L"Static", NULL);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    
    // 调用下一个钩子过程
    return CallNextHookEx(g_hHook, nCode, wParam, lParam);
}

// 安装钩子
bool InstallHooks()
{
    // 安装全局WH_CALLWNDPROC钩子
    g_hHook = SetWindowsHookEx(
        WH_CALLWNDPROC,
        LogoChangerHookProc,
        g_hModule,
        0); // 0表示钩子将安装到所有线程
    
    if (g_hHook == NULL)
    {
        LogError(_T("SetWindowsHookEx"));
    }
    
    return (g_hHook != NULL);
}

// 卸载钩子
void UninstallHooks()
{
    if (g_hHook != NULL)
    {
        if (!UnhookWindowsHookEx(g_hHook))
        {
            LogError(_T("UnhookWindowsHookEx"));
        }
        g_hHook = NULL;
    }
}