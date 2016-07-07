// ------------------------------------------------------------------------------------
// <copyright file="TriangleAnnotation.cs" company="Atalasoft">
//     (c) 2000-2015 Atalasoft, a Kofax Company. All rights reserved. Use is subject to license terms.
// </copyright>
// ------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Media;
using Atalasoft.Annotate;
using Atalasoft.Annotate.Wpf;
using Atalasoft.Annotate.Wpf.Renderer;
using WpfAnnotations;

namespace Atalasoft.Demo.WpfAnnotations
{
    /// <summary>
    /// This class is the actual annotation that will be used in WPF.
    /// </summary>
    /// <remarks>
    /// Don't forget to add a UI factory for this annotation. An example would be:
    /// this.AnnotationViewer.Annotations.Factories.Add(new WpfAnnotationUIFactory&lt;TriangleAnnotation, TriangleData&gt;());
    /// </remarks>
    public class TriangleAnnotation : WpfPointBaseAnnotation
    {
        /// <summary>
        /// Initializes static members of the <see cref="TriangleAnnotation"/> class.
        /// </summary>
        static TriangleAnnotation()
        {
            WpfAnnotationRenderers.Add(typeof(TriangleAnnotation), new TriangleAnnotationRenderingEngine());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleAnnotation"/> class.
        /// </summary>
        public TriangleAnnotation()
            : this(null, DefaultFill, DefaultOutline)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleAnnotation"/> class.
        /// </summary>
        /// <param name="points">The triangle points.</param>
        /// <param name="fill">The annotation fill.</param>
        /// <param name="outline">The annotation outline.</param>
        public TriangleAnnotation(Point[] points, AnnotationBrush fill, AnnotationPen outline)
            : base(3, points)
        {
            // WpfAnnotationUI already has dependency properties for fill and outline.
            this.SetValue(FillProperty, fill);
            this.SetValue(OutlineProperty, outline);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleAnnotation"/> class.
        /// </summary>
        /// <param name="data">The annotation data.</param>
        public TriangleAnnotation(TriangleData data)
            : base(3, data)
        {
            this.SetValue(FillProperty, data.Fill);
            this.SetValue(OutlineProperty, data.Outline);
        }

        /// <summary>
        /// Gets or sets the annotation fill.
        /// </summary>
        public AnnotationBrush Fill
        {
            get { return (AnnotationBrush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Gets or sets the annotation outline.
        /// </summary>
        public AnnotationPen Outline
        {
            get { return (AnnotationPen)GetValue(OutlineProperty); }
            set { SetValue(OutlineProperty, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the annotation supports being created from multiple mouse clicks.
        /// </summary>
        /// <value>
        /// Indicates whether the annotation supports being created from multiple mouse clicks.
        /// </value>
        public override bool SupportsMultiClickCreation
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a Geometry representing the annotation.
        /// </summary>
        /// <value>
        /// A Geometry representing the annotation.
        /// </value>
        /// <remarks>
        /// This is used for rendering and hit testing.
        /// The Geometry is presented in annotation space. Derived classes should override this property if the geometry of the annotation is not rectangular.
        /// </remarks>
        public override Geometry Geometry
        {
            get
            {
                return GeometryFromPoints(this.Points.ToArray(), WpfFreehandLineType.Straight, true, (this.Fill != null));
            }
        }

        /// <summary>
        /// Derived classes must override this method and return a copy of itself.
        /// </summary>
        /// <param name="clone">The new clone to add additional properties values.</param>
        /// <returns>
        /// A copy of the annotation.
        /// </returns>
        /// <remarks>
        /// This method is called from Clone to allow additional properties to be set. The WpfAnnotationUI properties of the clone will be filled before this method is called.
        /// </remarks>
        protected override WpfAnnotationUI CloneOverride(WpfAnnotationUI clone)
        {
            TriangleAnnotation ann = clone as TriangleAnnotation;
            if (ann == null) return null;

            AnnotationBrush fill = this.Fill;
            ann.Fill = fill == null ? null : fill.Clone();

            AnnotationPen outline = this.Outline;
            ann.Outline = outline == null ? null : outline.Clone();

            if (this.Points.Count > 0)
                ann.Points.AddRange(this.Points.ToArray());

            ann.GripMode = this.GripMode;

            return ann;
        }

        /// <summary>
        /// This method is used to serialize the annotation.
        /// </summary>
        /// <returns>
        /// An AnnotationData object with the current annotation settings.
        /// </returns>
        /// <remarks>This is used when serializing the annotation to XMP.</remarks>
        protected override AnnotationData CreateDataSnapshotOverride()
        {
            Point[] points = this.Points.ToArray();
            AnnotationBrush fill = this.Fill;
            AnnotationPen outline = this.Outline;

            if (fill != null) fill = fill.Clone();
            if (outline != null) outline = outline.Clone();

            return new TriangleData(points, fill, outline);
        }

        /// <summary>
        /// Rendering engine implementation for triangle annotation.
        /// </summary>
        public class TriangleAnnotationRenderingEngine : WpfAnnotationRenderingEngine<TriangleAnnotation>
        {
            /// <summary>
            /// Called when rendering annotation.
            /// </summary>
            /// <param name="annotation">The annotation.</param>
            /// <param name="environment">The environment.</param>
            protected override void OnRenderAnnotation(TriangleAnnotation annotation, WpfRenderEnvironment environment)
            {
                Pen pen = WpfObjectConverter.ConvertAnnotationPen(annotation.Outline);
                Brush brush = WpfObjectConverter.ConvertAnnotationBrush(annotation.Fill);
                environment.DrawingContext.DrawGeometry(brush, pen, annotation.Geometry);
            }
        }
    }
}
