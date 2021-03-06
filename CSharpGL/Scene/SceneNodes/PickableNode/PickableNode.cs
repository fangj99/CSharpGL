﻿using System.ComponentModel;
namespace CSharpGL
{
    /// <summary>
    /// Rendering something using GLSL shader and VBO(VAO).
    /// </summary>
    public abstract partial class PickableNode : SceneNodeBase, IRenderable, IPickable
    {
        // data structure for rendering.
        private readonly RenderUnitBuilder[] builders;
        private readonly IPickableRenderUnitBuilder pickingRenderUnitBuilder;
        private readonly IBufferSource model;
        /// <summary>
        /// 支持"拾取"的渲染器
        /// </summary>
        /// <param name="model">vertex shader种描述顶点位置信息的in变量的名字</param>
        ///<param name="positionNameInIBufferSource"></param>
        ///<param name="builders"></param>
        public PickableNode(IBufferSource model, string positionNameInIBufferSource, params RenderUnitBuilder[] builders)
        {
            this.model = model;

            var pickProgramProvider = PickingShaderHelper.GetPickingShaderProgramProvider();
            this.pickingRenderUnitBuilder = new IPickableRenderUnitBuilder(pickProgramProvider, positionNameInIBufferSource);

            this.builders = builders;
        }
    }
}