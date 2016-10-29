using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public interface IResource
    {
        //
        // 备注: 
        //     Only valid for resources that can be obtained through relative paths
        string FileBasePath { get; }

        // 摘要: 
        //     Returns an instance of Castle.Core.Resource.IResource created according to
        //     the relativePath using itself as the root.
        //
        // 参数: 
        //   relativePath:
        IResource CreateRelative(string relativePath);
        //
        // 摘要: 
        //     Returns a reader for the stream
        //
        // 备注: 
        //     It's up to the caller to dispose the reader.
        TextReader GetStreamReader();
        //
        // 摘要: 
        //     Returns a reader for the stream
        //
        // 参数: 
        //   encoding:
        //
        // 备注: 
        //     It's up to the caller to dispose the reader.
        TextReader GetStreamReader(Encoding encoding);
    }
}
