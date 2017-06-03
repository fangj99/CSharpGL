﻿using System.Drawing;
using System.IO;

namespace CSharpGL.Demos
{
    /// <summary>
    ///
    /// </summary>
    [DemoRenderer]
    internal class EmitNormalLineRenderer : PickableRenderer
    {
        public static EmitNormalLineRenderer Create(IBufferable model, string position, string normal, vec3 lengths)
        {
            var shaderCodes = new ShaderCode[3];
            shaderCodes[0] = new ShaderCode(File.ReadAllText(@"shaders\EmitNormalLineRenderer\EmitNormalLine.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(File.ReadAllText(@"shaders\EmitNormalLineRenderer\EmitNormalLine.geom"), ShaderType.GeometryShader);
            shaderCodes[2] = new ShaderCode(File.ReadAllText(@"shaders\EmitNormalLineRenderer\EmitNormalLine.frag"), ShaderType.FragmentShader);
            var provider = new ShaderCodeArray(shaderCodes);
            var map = new AttributeMap();
            map.Add("in_Position", position);
            map.Add("in_Normal", normal);
            var renderer = new EmitNormalLineRenderer(model, provider, map, position);
            renderer.ModelSize = lengths;

            return renderer;
        }

        private EmitNormalLineRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
            AttributeMap attributeMap, string positionNameInIBufferable, params GLState[] switches)
            : base(model, shaderProgramProvider, attributeMap, positionNameInIBufferable, switches)
        {
        }

        protected override void DoInitialize()
        {
            base.DoInitialize();

            this.SetUniform("normalLength", 0.5f);
            this.SetUniform("showModel", true);
            this.SetUniform("showNormal", true);
        }

        private long modelTicks;

        protected override void DoRender(RenderEventArgs arg)
        {
            mat4 projection = arg.Camera.GetProjectionMatrix();
            mat4 view = arg.Camera.GetViewMatrix();
            this.SetUniform("projectionMatrix", projection);
            this.SetUniform("viewMatrix", view);
            MarkableStruct<mat4> model = this.GetModelMatrix();
            if (this.modelTicks != model.UpdateTicks)
            {
                this.SetUniform("modelMatrix", model.Value);
                this.modelTicks = model.UpdateTicks;
            }

            base.DoRender(arg);
        }
    }
}