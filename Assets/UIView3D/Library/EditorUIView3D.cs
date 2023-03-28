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

using UnityEngine;

public class EditorUIView3D : UIView3DRenderer.UIView3D
{
    #region ctor
    public EditorUIView3D()
    {
    }
    #endregion

    #region dtor
    ~EditorUIView3D()
    {
        Destroy();
    }
    #endregion

    #region UIView3D overrides
    public override void Create(UIView3DRenderer.UIViewProvider provider, int width, int height)
    {
    }

    public override void Destroy()
    {
    }

    public override void SetScale(float scale)
    {
    }

    public override void SetTileCount(int x, int y)
    {
    }

    public override void Update()
    {
    }

    public override void SetScroll(float x, float y)
    {
    }

    public override void Redraw(Texture2D texture)
    {
    }

    public override void WaitForRender()
    {
    }

    public override int GetContentWidth()
    {
        return 0;
    }

    public override int GetContentHeight()
    {
        return 0;
    }

    public override int GetContentScrollWidth()
    {
        return 0;
    }

    public override int GetContentScrollHeight()
    {
        return 0;
    }
    #endregion
}
