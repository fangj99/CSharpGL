﻿using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace CSharpGL
{
    public partial class Scene
    {
        private const string strScene = "Scene";

        /// <summary>
        /// camera of the scene.
        /// </summary>
        [Category(strScene)]
        [Description("camera of the scene.")]
        [Editor(typeof(PropertyGridEditor), typeof(UITypeEditor))]
        public ICamera FirstCamera { get; set; }

        /// <summary>
        /// background color in this view port's area.
        /// </summary>
        [Category(strScene)]
        [Description("background color.")]
        public Color ClearColor { get; set; }

        /// <summary>
        /// Canvas that this scene binds to.
        /// </summary>
        [Category(strScene)]
        [Description("Canvas that this scene binds to.")]
        [Editor(typeof(PropertyGridEditor), typeof(UITypeEditor))]
        public ICanvas Canvas { get; private set; }

        ///// <summary>
        ///// Gets root view port's background color./Sets all viewports' background color.
        ///// </summary>
        //[Category(strScene)]
        //[Description("Gets root view port's background color./Sets all viewports' background color.")]
        //public Color ClearColor
        //{
        //    get
        //    {
        //        return this.rootViewPort.ClearColor;
        //    }
        //    set
        //    {
        //        foreach (ViewPort item in this.rootViewPort.Traverse(TraverseOrder.Pre))
        //        {
        //            item.ClearColor = value;
        //        }
        //    }
        //}

        ///// <summary>
        ///// OpenGL UI for cursor.
        ///// </summary>
        //public UICursor Cursor { get; set; }

        /// <summary>
        /// Root object of all objects to be rendered in the scene.
        /// </summary>
        [Category(strScene)]
        [Description("Root object of all objects to be rendered in the scene.")]
        [Editor(typeof(PropertyGridEditor), typeof(UITypeEditor))]
        public SceneRootObject RootObject { get { return this.rootObject; } }

        /// <summary>
        /// hosts all UI renderers.
        /// </summary>
        [Category(strScene)]
        [Description("hosts all UI renderers.")]
        [Editor(typeof(PropertyGridEditor), typeof(UITypeEditor))]
        public SceneRootUI RootUI { get { return this.rootUI; } }
    }
}