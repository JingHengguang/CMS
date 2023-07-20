
using Cms.Backend.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Backend.Api.Model
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var currentData = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //初始化用户角色种子数据
            modelBuilder.Entity<UserRoles>().HasData(
                /// <summary>
                /// 超级管理员只能有一个
                /// 
                /// 管理员和超管可以进入后台，普通用户不可以
                /// </summary>
                /// <value></value>
                new UserRoles
                {
                    Id = 1,
                    RoleName = "超级管理员",
                    IsActived = true,
                    IsDeleted = false,
                    CreatedTime = currentData,
                    UpdatedTime = currentData,
                    Remarks = "种子数据"
                },
                 new UserRoles
                 {
                     Id = 2,
                     RoleName = "管理员",
                     IsActived = true,
                     IsDeleted = false,
                     CreatedTime = currentData,
                     UpdatedTime = currentData,
                     Remarks = "种子数据"
                 },
                new UserRoles
                {
                    Id = 3,
                    RoleName = "普通用户",
                    IsActived = true,
                    IsDeleted = false,
                    CreatedTime = currentData,
                    UpdatedTime = currentData,
                    Remarks = "种子数据"
                }
            );
            //初始化用户种子数据
            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = 1,
                    Username = "admin",
                    Password = "123",
                    IsActived = true,
                    IsDeleted = false,
                    CreatedTime = currentData,
                    UpdatedTime = currentData,
                    UserRoleId = 1,
                    Remarks = "种子数据",
                    AvatarUrl = "UploadFiles\\DefaultImage\\default.jpg",

                }
            );

            //初始化类型数据
            // modelBuilder.Entity<Types>().HasData(
            //     new Types
            //     {
            //         Id = 1,
            //         Type = "科技",
            //         IsActived = true,
            //         IsDeleted = false,
            //         CreatedTime = currentData,
            //         UpdatedTime = currentData,
            //         Remarks = "种子数据"
            //     },
            //     new Types
            //     {
            //         Id = 2,
            //         Type = "财经",
            //         IsActived = true,
            //         IsDeleted = false,
            //         CreatedTime = currentData,
            //         UpdatedTime = currentData,
            //         Remarks = "种子数据"
            //     },
            //     new Types
            //     {
            //         Id = 3,
            //         Type = "娱乐",
            //         IsActived = true,
            //         IsDeleted = false,
            //         CreatedTime = currentData,
            //         UpdatedTime = currentData,
            //         Remarks = "种子数据"
            //     },
            //     new Types
            //     {
            //         Id = 4,
            //         Type = "体育",
            //         IsActived = true,
            //         IsDeleted = false,
            //         CreatedTime = currentData,
            //         UpdatedTime = currentData,
            //         Remarks = "种子数据"
            //     },
            //     new Types
            //     {
            //         Id = 5,
            //         Type = "美食",
            //         IsActived = true,
            //         IsDeleted = false,
            //         CreatedTime = currentData,
            //         UpdatedTime = currentData,
            //         Remarks = "种子数据"
            //     },
            //     new Types
            //     {
            //         Id = 6,
            //         Type = "游戏",
            //         IsActived = true,
            //         IsDeleted = false,
            //         CreatedTime = currentData,
            //         UpdatedTime = currentData,
            //         Remarks = "种子数据"
            //     },
            //     new Types
            //     {
            //         Id = 7,
            //         Type = "历史",
            //         IsActived = true,
            //         IsDeleted = false,
            //         CreatedTime = currentData,
            //         UpdatedTime = currentData,
            //         Remarks = "种子数据"
            //     },
            //     new Types
            //     {
            //         Id = 8,
            //         Type = "其他",
            //         IsActived = true,
            //         IsDeleted = false,
            //         CreatedTime = currentData,
            //         UpdatedTime = currentData,
            //         Remarks = "种子数据"
            //     }
            // );
            

            /// <summary>
            /// 初始化轮播图
            /// </summary>
            /// <param name="Broadcast_map("></param>
            /// <typeparam name="Broadcast_map"></typeparam>
            /// <returns></returns>
            modelBuilder.Entity<Broadcast_map>().HasData(
                new Broadcast_map()
                {
                    Id = 1,
                    Picture = "UploadFiles\\DefaultImage\\bj001.jpg",
                    IsActived = true,
                    IsDeleted = false,
                    CreatedTime = currentData,
                    UpdatedTime = currentData,
                    Remarks = "种子数据"
                }
            );
        }
    }
}