//=============================================================================
//
// MIT License
//
// Copyright (c) 2017-2018 ALTIMIT SYSTEMS LTD
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
//=============================================================================

using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class AndroidUIView3D : UIView3DRenderer.UIView3D
{
    #region Native bindings
    [DllImport("uiview3d")]
    private static extern IntPtr alt_unity_uiview_create(IntPtr nativeView, int width, int height);
    [DllImport("uiview3d")]
    private static extern void alt_unity_uiview_destroy(IntPtr uiView);
    [DllImport("uiview3d")]
    private static extern void alt_unity_uiview_wait(IntPtr uiViewTask);
    [DllImport("uiview3d")]
    private static extern void alt_unity_uiview_set_scale(IntPtr uiView, float scale);
    [DllImport("uiview3d")]
    private static extern void alt_unity_uiview_set_tile(IntPtr uiView, int x, int y);
    [DllImport("uiview3d")]
    private static extern void alt_unity_uiview_update(IntPtr uiView);
    [DllImport("uiview3d")]
    private static extern void alt_unity_uiview_scroll_to(IntPtr uiView, int x, int y);
    [DllImport("uiview3d")]
    private static extern IntPtr alt_unity_uiview_redraw(IntPtr uiView, IntPtr nativeTexture, int width, int height);
    [DllImport("uiview3d")]
    private static extern int alt_unity_uiview_get_content_width(IntPtr uiView);
    [DllImport("uiview3d")]
    private static extern int alt_unity_uiview_get_content_height(IntPtr uiView);
    [DllImport("uiview3d")]
    private static extern int alt_unity_uiview_get_content_scroll_width(IntPtr uiView);
    [DllImport("uiview3d")]
    private static extern int alt_unity_uiview_get_content_scroll_height(IntPtr uiView);
    #endregion

    #region Private members
    UIView3DRenderer.AndroidViewProvider viewProvider = null;
    IntPtr viewHandle = IntPtr.Zero;
    IntPtr renderTask = IntPtr.Zero;
    float invScaledDensity = 1.0f;
    #endregion

    #region ctor
    public AndroidUIView3D()
    {
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        invScaledDensity = 1.0f / unityActivity.Call<AndroidJavaObject>("getResources").Call<AndroidJavaObject>("getDisplayMetrics").Get<float>("scaledDensity");
    }
    #endregion

    #region dtor
    ~AndroidUIView3D()
    {
        Destroy();
    }
    #endregion

    #region UIView3D overrides
    public override void Create(UIView3DRenderer.UIViewProvider provider, int width, int height)
    {
        viewProvider = provider as UIView3DRenderer.AndroidViewProvider;
        if (viewHandle != IntPtr.Zero)
        {
            alt_unity_uiview_destroy(viewHandle);
        }
        IntPtr viewProviderNative = viewProvider.GetNativeObject();
        if (viewProviderNative != IntPtr.Zero)
        {
            viewHandle = alt_unity_uiview_create(viewProviderNative, width, height);
        }
    }

    public override void Destroy()
    {
        if (renderTask != IntPtr.Zero)
        {
            alt_unity_uiview_wait(renderTask);
            renderTask = IntPtr.Zero;
        }
        if (viewHandle != IntPtr.Zero)
        {
            alt_unity_uiview_destroy(viewHandle);
            viewHandle = IntPtr.Zero;
        }
    }

    public override void SetScale(float scale)
    {
        alt_unity_uiview_set_scale(viewHandle, scale * invScaledDensity);
    }

    public override void SetTileCount(int x, int y)
    {
        alt_unity_uiview_set_tile(viewHandle, x, y);
    }

    public override void Update()
    {
        alt_unity_uiview_update(viewHandle);
    }

    public override void SetScroll(float x, float y)
    {
        alt_unity_uiview_scroll_to(viewHandle, (int)x, (int)y);
    }

    public override void Redraw(Texture2D texture)
    {
        if (renderTask == IntPtr.Zero)
        {
            renderTask = alt_unity_uiview_redraw(viewHandle, texture.GetNativeTexturePtr(), texture.width, texture.height);
        }
    }

    public override void WaitForRender()
    {
        if (renderTask != IntPtr.Zero)
        {
            alt_unity_uiview_wait(renderTask);
            renderTask = IntPtr.Zero;
        }
    }

    public override int GetContentWidth()
    {
        return alt_unity_uiview_get_content_width(viewHandle);
    }

    public override int GetContentHeight()
    {
        return alt_unity_uiview_get_content_height(viewHandle);
    }

    public override int GetContentScrollWidth()
    {
        return alt_unity_uiview_get_content_scroll_width(viewHandle);
    }

    public override int GetContentScrollHeight()
    {
        return alt_unity_uiview_get_content_scroll_height(viewHandle);
    }
    #endregion

}
