﻿using System;

namespace CSharpGL
{
    // 用glDrawElements()执行一个索引buffer的渲染操作。
    /// <summary>
    /// Wraps glDrawElements(uint mode, int count, uint type, IntPtr indices).
    /// </summary>
    public sealed partial class OneIndexBuffer : IndexBuffer
    {
        /// <summary>
        /// Wraps glDrawElements(uint mode, int count, uint type, IntPtr indices).
        /// </summary>
        /// <param name="bufferId">用glGenBuffers()得到的VBO的Id。<para>Id got from glGenBuffers();</para></param>
        /// <param name="mode">用哪种方式渲染各个顶点？（GL.GL_TRIANGLES etc.）</param>
        /// <param name="elementType">type in glDrawElements(uint mode, int count, uint type, IntPtr indices);
        /// <para>表示第3个参数，表示索引元素的类型。</para></param>
        /// <param name="length">此VBO含有多个个元素？<para>How many elements?</para></param>
        /// <param name="byteLength">此VBO中的数据在内存中占用多少个字节？<para>How many bytes in this buffer?</para></param>
        /// <param name="primCount">primCount in instanced rendering.</param>
        internal OneIndexBuffer(uint bufferId, DrawMode mode,
            IndexBufferElementType elementType, int length, int byteLength, int primCount = 1)
            : base(mode, bufferId, length, byteLength, primCount)
        {
            this.Target = BufferTarget.ElementArrayBuffer;

            this.ElementCount = length;
            //this.OriginalElementCount = length;
            this.ElementType = elementType;
        }

        /// <summary>
        /// 要渲染的第一个索引的位置。
        /// <para>First index to be rendered.</para>
        /// </summary>
        public int FirstIndex { get; set; }

        /// <summary>
        /// 要渲染多少个索引。
        /// <para>How many indexes to be rendered?</para>
        /// </summary>
        public int ElementCount { get; set; }

        ///// <summary>
        ///// 实际上一共有多少个索引？
        ///// <para>How many indexes exists?</para>
        ///// </summary>
        //public int OriginalElementCount { get; set; }

        /// <summary>
        /// type in GL.DrawElements(uint mode, int count, uint type, IntPtr indices);
        /// </summary>
        public IndexBufferElementType ElementType { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public override void Draw()
        {
            int primCount = this.PrimCount;
            if (primCount < 1) { throw new Exception("error: primCount is less than 1."); }

            uint mode = (uint)this.Mode;
            IntPtr offset = GetOffset(this.ElementType, this.FirstIndex);

            glBindBuffer(GL.GL_ELEMENT_ARRAY_BUFFER, this.BufferId);
            if (primCount == 1)
            {
                GL.Instance.DrawElements(mode, this.ElementCount, (uint)this.ElementType, offset);
            }
            else
            {
                glDrawElementsInstanced(mode, this.ElementCount, (uint)this.ElementType, offset, primCount);
            }
            glBindBuffer(GL.GL_ELEMENT_ARRAY_BUFFER, 0);
        }

        private IntPtr GetOffset(IndexBufferElementType elementType, int firstIndex)
        {
            IntPtr offset;
            switch (elementType)
            {
                case IndexBufferElementType.UByte:
                    offset = new IntPtr(firstIndex * sizeof(byte));
                    break;

                case IndexBufferElementType.UShort:
                    offset = new IntPtr(firstIndex * sizeof(ushort));
                    break;

                case IndexBufferElementType.UInt:
                    offset = new IntPtr(firstIndex * sizeof(uint));
                    break;

                default:
                    throw new Exception("Unexpected IndexBufferElementType!");
            }
            return offset;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string type = string.Empty;
            switch (this.ElementType)
            {
                case IndexBufferElementType.UByte:
                    type = "byte";
                    break;

                case IndexBufferElementType.UShort:
                    type = "ushort";
                    break;

                case IndexBufferElementType.UInt:
                    type = "uint";
                    break;

                default:
                    throw new Exception("Unexpected IndexBufferElementType!");
            }
            int primCount = this.PrimCount;
            if (primCount < 1)
            {
                return string.Format("error: primCount is less than 1.");
            }
            else if (primCount == 1)
            {
                return string.Format("glDrawElements({0}, {1}, {2}, new IntPtr({3} * sizeof({4}))",
                    this.Mode, this.ElementCount, this.ElementType, this.FirstIndex, type);
            }
            else
            {
                return string.Format("glDrawElementsInstanced({0}, {1}, {2}, new IntPtr({3} * sizeof({4}), {5})",
                    this.Mode, this.ElementCount, this.ElementType, this.FirstIndex, type, primCount);
            }
        }
    }
}