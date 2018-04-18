using Shine.Comman;
using Shine.Comman.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Shine.WebApi.Utils
{

    /// <summary>
    /// 保存上传文件
    /// </summary>
    public static class SaveFiles
    {
        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="Request">标识HTTP请求信息</param>
        /// <param name="Id">一般是信息主键，用来标识文件存储名称</param>
        /// <param name="saveFileType">保存类型，详细参见<see cref="SaveFileType"/></param>
        /// <returns></returns>
        public static async Task<FileInfo> SaveUploadFile(this HttpRequestMessage Request, string Id, SaveFileType saveFileType = SaveFileType.HeadIcon)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            //指定要将文件存入的服务器物理位置
            string dirTempPath = string.Empty;
            switch (saveFileType)
            {
                case SaveFileType.HeadIcon:
                    dirTempPath = HttpContext.Current.Server.MapPath("~/HeadIconFiles");
                    break;
                case SaveFileType.OrganizeLogo:
                    dirTempPath = HttpContext.Current.Server.MapPath("~/OrganizeLogos");
                    break;
                case SaveFileType.HostBrushBag:
                    dirTempPath = HttpContext.Current.Server.MapPath("~/HostBrushBags");
                    break;
                case SaveFileType.SubBrushBag:
                    dirTempPath = HttpContext.Current.Server.MapPath("~/SubBrushBags");
                    break;
            }

            //文件夹不存在则创建
            if (!Directory.Exists(dirTempPath))
            {
                Directory.CreateDirectory(dirTempPath);
            }


            // 设置上传目录 
            var provider = new RenamingMultipartFormDataStreamProvider(dirTempPath);

            // 接受数据并保存文件
            return await Request.Content.ReadAsMultipartAsync(provider).
                 ContinueWith(NewSaveIocnMethod(Id, saveFileType, dirTempPath, provider));
        }

        /// <summary>
        /// 文件
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="saveFileType"></param>
        /// <param name="dirTempPath"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        private static Func<Task<RenamingMultipartFormDataStreamProvider>, FileInfo> NewSaveIocnMethod(string Id, SaveFileType saveFileType, string dirTempPath, RenamingMultipartFormDataStreamProvider provider)
        {
            return o =>
            {
                var file = provider.FileData[0];
                file.CheckNotNull("file");
                string orfilename = provider.GetLocalFileName(file.Headers);
                FileInfo fileinfo = new FileInfo(file.LocalFileName);
                string fileExt = fileinfo.Extension;
                string imageFileTypes = ".gif,.jpg,.jpeg,.png,.bmp";
                string binFileTypes = ".shine";

                try
                {
                    //文件长度判断
                    switch (fileinfo.Length)
                    {
                        case long i when (i <= 0):
                            fileinfo.Delete();
                            throw new Exception("id:请选择上传文件。");
                        case long i when ((saveFileType == SaveFileType.HeadIcon || saveFileType == SaveFileType.OrganizeLogo) && (i >= 4194304)):
                            //图片文件不能大于4MB
                            fileinfo.Delete();
                            throw new Exception("id:上传的图片文件大小不能超过4MB。");
                        case long i when ((saveFileType == SaveFileType.HostBrushBag || saveFileType == SaveFileType.SubBrushBag) && (i >= 31457280)):
                            //刷机包文件不能大于30MB
                            fileinfo.Delete();
                            throw new Exception("id:上传的刷机包文件大小不能超过30MB。");
                    }
                }
                catch
                {
                    throw new Exception("id:获取上传文件失败...");
                }


                //文件扩展
                switch (fileExt)
                {
                    case string ext when (string.IsNullOrEmpty(ext)):
                        fileinfo.Delete();
                        throw new Exception("id:判断文件类型失败！");
                    case string ext when (!imageFileTypes.Contains(ext) && (saveFileType == SaveFileType.HeadIcon || saveFileType == SaveFileType.OrganizeLogo)):
                        fileinfo.Delete();
                        throw new Exception("id:上传的文件扩展名是不允许的图片扩展名。");
                    case string ext when (!binFileTypes.Contains(ext) && (saveFileType == SaveFileType.HostBrushBag || saveFileType == SaveFileType.SubBrushBag)):
                        fileinfo.Delete();
                        throw new Exception("id:上传的文件扩展名是不允许的刷机包扩展名。");
                    case string ext when (!imageFileTypes.Contains(ext) && !binFileTypes.Contains(ext)):
                        fileinfo.Delete();
                        throw new Exception("id:上传的文件扩展名是不允许的扩展名。");
                }

                FileInfo info = fileinfo.CopyTo(Path.Combine(dirTempPath, Id.ToString() + fileExt), true);
                fileinfo.Delete();
                return info;
            };
        }
    }

    /// <summary>
    /// 文件信息保存枚举
    /// </summary>
    public enum SaveFileType
    {
        /// <summary>
        /// 用户头像图片
        /// </summary>
        HeadIcon,
        /// <summary>
        /// 组织Logo图片
        /// </summary>
        OrganizeLogo,
        /// <summary>
        /// 主机程序
        /// </summary>
        HostBrushBag,

        /// <summary>
        /// 分控刷机包
        /// </summary>
        SubBrushBag
    }
}