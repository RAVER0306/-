using Shine.Comman;
using Shine.Comman.Data;
using Shine.Comman.Extensions;
using Shine.Comman.Secutiry;
using Shine.Core.Caching;
using Shine.Core.Caching.Models;
using Shine.Core.Data;
using Shine.Core.Data.Extensions;
using Shine.Core.Mapping;
using Shine.DataProcessingLogic.Contracts;
using Shine.DataProcessingLogic.Dtos;
using Shine.DataProcessingLogic.Dtos.OrganzieManager;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using Shine.DataProcessingLogic.Models.UserManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shine.DataProcessingLogic.Services
{
    public class OrganizeService: IOrganizeContract
    {
        #region 仓储对象
        /// <summary>
        /// 获取或设置 <see cref="Organize"/>的信息仓储对象
        /// </summary>
        public IRepository<Organize, Guid> OrganizeRepository { set; get; }
        /// <summary>
        /// 获取或设置 <see cref="UserOrganizeMap"/>的信息仓储对象
        /// </summary>
        public IRepository<UserOrganizeMap, Guid> UserOrganizeMapRepository { set; get; }
        
        #endregion

        #region 实现接口方法

        #region 获取组织机构logo
        /// <summary>
        /// 获取组织机构logo
        /// </summary>
        /// <param name="id">组织机构主键id</param>
        /// <returns></returns>
        public byte[] GetOrganizeLogo(Guid id)
        {
            if (!OrganizeRepository.CheckExists(m => m.Id == id))
            {
                throw new Exception("id:查询信息不存在");
            }
            else
            {
                var result = OrganizeRepository.TrackEntities.First(m => m.Id == id);
                if (string.IsNullOrEmpty(result.OrganizeLogoPath) && (result.OrganizeLogo?.Length <= 0 || result.OrganizeLogo == null))
                {
                    return Properties.Resources.OrganizeLog.ToBytes();
                }
                else
                {
                    var imgPath = HttpContext.Current.Server.MapPath($"{result.OrganizeLogoPath}");
                    if (File.Exists(imgPath))
                    {
                        return File.ReadAllBytes(imgPath);
                    }
                    else
                    {
                        imgPath = HttpContext.Current.Server.MapPath($"~/OrganizeLogos\\{result.Id.ToString()}");
                        string str = result.OrganizeLogo.CreateImageFromBytes(imgPath);
                        result.OrganizeLogoPath = $"~/OrganizeLogos\\{str.Substring(str.LastIndexOf('\\') + 1)}";
                        OrganizeRepository.Update(result);
                        return result.OrganizeLogo;
                    }
                }

            }
        }
        #endregion

        #region 设置更新组织机构logo
        /// <summary>
        /// 设置更新组织机构logo
        /// </summary>
        /// <param name="id">组织机构主键id</param>
        /// <param name="imageFile">准备要更新的组织机构logo文件信息</param>
        /// <returns></returns>
        public async Task<OperationResult> SetOrganizeLogo(Guid id, Task<FileInfo> imageFile)
        {
            if (!OrganizeRepository.CheckExists(m => m.Id == id))
            {
                throw new Exception("id:组织机构信息不存在");
            }
            else
            {
                var file = await imageFile;
                using (Bitmap bitmap = new Bitmap(file.FullName))
                {
                    byte[] imageBits = bitmap.ToBytes();
                    var result = OrganizeRepository.TrackEntities.First(m => m.Id == id);
                    result.OrganizeLogo = imageBits;
                    result.OrganizeLogoPath = Path.Combine("~/OrganizeLogos", file.Name);
                    if (OrganizeRepository.Update(result) > 0)
                    {
                        return new OperationResult(OperationResultType.Success, "操作成功");
                    }
                    else
                    {
                        throw new Exception("id:操作失败");
                    }
                }
            }
        }
        #endregion

        #region 获取组织机构查询数据集
        /// <summary>
        /// 获取<see cref="Organize"/>的数据源查询
        /// </summary>
        public IQueryable<Organize> IQOrganizes => OrganizeRepository.Entities;
        #endregion

        #region 获取 UserOrganizeMap 的数据源查询
        /// <summary>
        /// 获取<see cref="UserOrganizeMap"/>的数据源查询
        /// </summary>
        public IQueryable<UserOrganizeMap> IQUserOrganizeMap => UserOrganizeMapRepository.Entities;
        #endregion

        #region 添加新的组织机构
        /// <summary>
        /// 添加新的组织机构
        /// </summary>
        /// <param name="cache">当前操作的缓存用户</param>
        /// <param name="inputDto">信息输入模型</param>
        /// <returns></returns>
        public async Task<OperationResult> AddOrganizes(CacheUser cache,params OrganizeIn[] inputDto)
        {
            try
            {
                List<string> names = new List<string>();
                OrganizeRepository.UnitOfWork.BeginTransaction();
                foreach (var dto in inputDto)
                {
                    var input = dto.MapTo<Organize>();                  
                    input.CreatedTime = DateTime.Now;
                    input.CreatorUserId = cache.UserName;
                    input.SortCode = (int)DateTime.Now.GetTimeStamp();
                    await OrganizeRepository.InsertAsync(input);
                    if (!cache.IsAdministrator)
                    {
                       int count =  UserOrganizeMapRepository.CheckExists(m => m.Organize_Id == input.Id && m.UserLogin_Id == cache.Id)? 0 : UserOrganizeMapRepository.Insert(new UserOrganizeMap { Organize_Id=input.Id, UserLogin_Id=cache.Id });
                    }
                    names.Add(input.FullName);
                }
                OrganizeRepository.UnitOfWork.Commit();
                if (names.Count > 0)
                {
                    return new OperationResult(OperationResultType.Success, "添加组织机构:{0}成功".FormatWith(names.ExpandAndToString()));
                }
                else
                {
                    return new OperationResult(OperationResultType.NoChanged, "操作失败，添加数据请求已取消");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"id:{ ex.InnerException.Message }");
            }
        }
        #endregion

        #region 修改组织机构信息
        /// <summary>
        /// 修改组织机构信息
        /// </summary>
        /// <param name="cache">当前操作的缓存用户</param>
        /// <param name="inputDto">信息输入模型</param>
        /// <returns></returns>
        public async Task<OperationResult> EditOrganizes(CacheUser cache, params OrganizeInputDto[] inputDto)
        {
            ICache iCache = CacheManager.GetCacher("CacheOrganizeId");
            var cacheIds = iCache.Get(cache.UserName.AESEncrypt128()) as IEnumerable<Guid>;
            List<string> names = new List<string>();
            OrganizeRepository.UnitOfWork.BeginTransaction();
            foreach (var i in inputDto)
            {
                if (!cache.IsAdministrator&&!cacheIds.Contains(i.Id))
                {
                    throw new Exception("id:当前操作不在权限范围内");
                }
                var value = OrganizeRepository.Entities.FirstOrDefault(m => m.Id == i.Id);
                if (value == null)
                {
                    throw new Exception($"id:主键为:{i.Id}的组织机构信息不存在");
                }
                Organize dto = i.MapTo<Organize>();
                dto.CreatedTime = value.CreatedTime;
                dto.CreatorUserId = value.CreatorUserId;
                dto.LastUpdatedTime = DateTime.Now;
                dto.LastUpdatorUserId = cache.UserName;
                dto.SortCode = value.SortCode;
                await OrganizeRepository.UpdateAsync(dto);
                names.Add(dto.FullName);
            }
            OrganizeRepository.UnitOfWork.Commit();
            if (names.Count > 0)
            {
                return new OperationResult(OperationResultType.Success, "修改组织机构:{0}成功".FormatWith(names.ExpandAndToString()));
            }
            else
            {
                return new OperationResult(OperationResultType.NoChanged, "操作失败，修改数据请求已取消");
            }
        }
        #endregion

        #region 根据主键删除指定的组织机构
        /// <summary>
        /// 通过主键删除指定的组织机构
        /// </summary>
        /// <param name="Ids">组织机构主键集合</param>
        /// <returns></returns>
        public async Task<OperationResult> DeleteOrganizeById(params Guid[] Ids)
        {
            return await Task.Run(() =>
            {
                List<string> names = new List<string>();
                List<Guid> waitDeleteId = new List<Guid>();
                OrganizeRepository.UnitOfWork.BeginTransaction();
                foreach (var i in Ids)
                {
                    if (!waitDeleteId.Contains(i))
                    {
                        SqlParameter[] para = new SqlParameter[]
                        {
                    new SqlParameter("@Id",i),
                    new SqlParameter("@SPAction",true)
                        };
                        var treeResult = OrganizeRepository.SqlQuery(sql: "Tree_Organize @Id,@SPAction", trackEnabled: true, parameters: para).ToList();
                        if (treeResult == null || treeResult.Count == 0)
                        {
                            throw new Exception($"id:未找到{i}&{treeResult.Find(m => m.Id == i).FullName}的机构信息，提交请求已取消");
                        }
                        waitDeleteId.AddRange(treeResult.Select(m => m.Id));
                        waitDeleteId = waitDeleteId.Distinct().ToList();
                        for (int max = treeResult.Count - 1; max >= 0; max--)
                        {
                            OrganizeRepository.DeleteDirect(treeResult[max].Id);
                            names.Add(treeResult[max].FullName);
                        }
                    } 
                }
                OrganizeRepository.UnitOfWork.Commit();
                if (names.Count > 0)
                {
                    return new OperationResult( OperationResultType.Success, "删除组织机构:{0}成功".FormatWith(names.ExpandAndToString()));
                }
                else
                {
                    return new OperationResult(OperationResultType.NoChanged, "操作失败，删除数据请求已取消");
                }
            });     
        }
        #endregion
        #endregion
    }
}
