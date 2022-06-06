using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjetoTCC.Interface
{
    public interface IResizeImage
    {
        byte[] Resize(byte[] imageData, int width, int height, bool inCamera);
    }
}
