using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Shine.WebApi.Utils
{
    /// <summary>
    /// 继承<see cref="MultipartFormDataStreamProvider"/>重写
    /// </summary>
    public class RenamingMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        /// <summary>
        /// 内容写入到的根路径
        /// </summary>
        public string Root { get; set; }

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="root">多部分正文部分的内容写入到的根路径</param>
        public RenamingMultipartFormDataStreamProvider(string root)
            : base(root)
        {
            Root = root;
        }

        /// <summary>
        /// 获取本地文件名，该文件名将与用于创建存储当前 MIME 正文部分内容的绝对文件名的根路径组合在一起。
        /// </summary>
        /// <param name="headers">当前 MIME 正文部分的标头。</param>
        /// <returns>不包含路径部分的相对文件名</returns>
        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            string filePath = headers.ContentDisposition.FileName;

            if (filePath.StartsWith(@"""") && filePath.EndsWith(@""""))
            {
                filePath = filePath.Substring(1, filePath.Length - 2);
            }

            var filename = Path.GetFileName(filePath);
            var extension = Path.GetExtension(filePath);
            var contentType = headers.ContentType.MediaType;

            return filename;
        }

    }
}