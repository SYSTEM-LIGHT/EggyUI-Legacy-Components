#include "pch.h"
#include "NTVersionLogoChanger.h"

// 加载Logo图像 - 增强错误处理和路径构建
bool LoadLogoImages()
{
    // 检查资源是否已经加载
    EnterCriticalSection(&g_csLogoResources);
    if (g_hLogoSmall && g_hLogoMedium && g_hLogoLarge)
    {
        LeaveCriticalSection(&g_csLogoResources);
        return true;
    }
    LeaveCriticalSection(&g_csLogoResources);
    
    // 先释放已有的资源
    EnterCriticalSection(&g_csLogoResources);
    if (g_hLogoSmall) { DeleteObject(g_hLogoSmall); g_hLogoSmall = NULL; }
    if (g_hLogoMedium) { DeleteObject(g_hLogoMedium); g_hLogoMedium = NULL; }
    if (g_hLogoLarge) { DeleteObject(g_hLogoLarge); g_hLogoLarge = NULL; }
    LeaveCriticalSection(&g_csLogoResources);
    
    // 获取DLL路径
    WCHAR szDllPath[MAX_PATH] = { 0 };
    if (GetModuleFileName(g_hModule, szDllPath, MAX_PATH) == 0)
    {
        LogError(_T("GetModuleFileName"));
        return false;
    }

    // 构建图像文件路径
    WCHAR szImagePath[MAX_PATH] = { 0 };
    if (!PathRemoveFileSpec(szDllPath))
    {
        LogError(_T("PathRemoveFileSpec"));
        return false;
    }
    
    // 加载小图标
    if (!PathCombine(szImagePath, szDllPath, L"images\\logo_small.bmp"))
    {
        LogError(_T("PathCombine (small logo)"));
        return false;
    }
    
    HBITMAP hSmall = (HBITMAP)LoadImage(NULL, szImagePath, IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
    if (hSmall == NULL)
    {
        LogError(_T("LoadImage (small logo)"));
        return false;
    }
    
    // 加载中图标
    if (!PathCombine(szImagePath, szDllPath, L"images\\logo_medium.bmp"))
    {
        LogError(_T("PathCombine (medium logo)"));
        DeleteObject(hSmall);
        return false;
    }
    
    HBITMAP hMedium = (HBITMAP)LoadImage(NULL, szImagePath, IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
    if (hMedium == NULL)
    {
        LogError(_T("LoadImage (medium logo)"));
        DeleteObject(hSmall);
        return false;
    }
    
    // 加载大图标
    if (!PathCombine(szImagePath, szDllPath, L"images\\logo_large.bmp"))
    {
        LogError(_T("PathCombine (large logo)"));
        DeleteObject(hSmall);
        DeleteObject(hMedium);
        return false;
    }
    
    HBITMAP hLarge = (HBITMAP)LoadImage(NULL, szImagePath, IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
    if (hLarge == NULL)
    {
        LogError(_T("LoadImage (large logo)"));
        DeleteObject(hSmall);
        DeleteObject(hMedium);
        return false;
    }
    
    // 所有图像加载成功，更新全局变量
    EnterCriticalSection(&g_csLogoResources);
    g_hLogoSmall = hSmall;
    g_hLogoMedium = hMedium;
    g_hLogoLarge = hLarge;
    LeaveCriticalSection(&g_csLogoResources);
    
    return true;
}

// 调整位图大小（实现自动分辨率适配）
HBITMAP ResizeBitmap(HBITMAP hOriginal, int width, int height)
{
    if (hOriginal == nullptr || width <= 0 || height <= 0)
        return nullptr;
    
    // 获取原始位图信息
    BITMAP bm = { 0 };
    if (GetObject(hOriginal, sizeof(bm), &bm) == 0)
    {
        LogError(_T("GetObject (ResizeBitmap)"));
        return nullptr;
    }

    // 创建新的位图
    HDC hdcScreen = GetDC(NULL);
    if (hdcScreen == nullptr)
    {
        LogError(_T("GetDC (ResizeBitmap)"));
        return nullptr;
    }
    
    HDC hdcSrc = CreateCompatibleDC(hdcScreen);
    if (hdcSrc == nullptr)
    {
        LogError(_T("CreateCompatibleDC (src)"));
        ReleaseDC(NULL, hdcScreen);
        return nullptr;
    }
    
    HDC hdcDest = CreateCompatibleDC(hdcScreen);
    if (hdcDest == nullptr)
    {
        LogError(_T("CreateCompatibleDC (dest)"));
        DeleteDC(hdcSrc);
        ReleaseDC(NULL, hdcScreen);
        return nullptr;
    }

    HBITMAP hNewBitmap = CreateCompatibleBitmap(hdcScreen, width, height);
    if (hNewBitmap == nullptr)
    {
        LogError(_T("CreateCompatibleBitmap"));
        DeleteDC(hdcSrc);
        DeleteDC(hdcDest);
        ReleaseDC(NULL, hdcScreen);
        return nullptr;
    }
    
    HBITMAP hOldSrcBitmap = (HBITMAP)SelectObject(hdcSrc, hOriginal);
    HBITMAP hOldDestBitmap = (HBITMAP)SelectObject(hdcDest, hNewBitmap);

    // 调整图像大小
    SetStretchBltMode(hdcDest, HALFTONE);
    if (!StretchBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, bm.bmWidth, bm.bmHeight, SRCCOPY))
    {
        LogError(_T("StretchBlt"));
        DeleteObject(hNewBitmap);
        hNewBitmap = nullptr;
    }

    // 清理资源
    SelectObject(hdcSrc, hOldSrcBitmap);
    SelectObject(hdcDest, hOldDestBitmap);
    DeleteDC(hdcSrc);
    DeleteDC(hdcDest);
    ReleaseDC(NULL, hdcScreen);

    return hNewBitmap;
}