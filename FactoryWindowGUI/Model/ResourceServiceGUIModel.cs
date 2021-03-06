﻿// ==================================================
// 文件名：ResourceServiceGUIModel.cs
// 创建时间：2020/01/04 16:25
// 上海芸浦信息技术有限公司
// copyright@yumpoo
// ==================================================
// 最后修改于：2020/05/11 16:25
// 修改人：jians
// ==================================================

using ProcessControlService.Contracts;

namespace FactoryWindowGUI.Model
{
    public class ResourceServiceGuiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ResourceServiceModel ServiceModel { get; set; }
    }
}