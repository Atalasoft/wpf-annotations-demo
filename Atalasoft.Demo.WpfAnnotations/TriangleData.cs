// ------------------------------------------------------------------------------------
// <copyright file="TriangleData.cs" company="Atalasoft">
//     (c) 2000-2015 Atalasoft, a Kofax Company. All rights reserved. Use is subject to license terms.
// </copyright>
// ------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.Windows;
using Atalasoft.Annotate;
using Atalasoft.Annotate.Wpf;

namespace WpfAnnotations
{
    /// <summary>
    /// This data class is used only for serialization in WPF.
    /// </summary>
    [Serializable]
    public class TriangleData : PointBaseData
    {
        private AnnotationBrush _fill = WpfAnnotationUI.DefaultFill;
        private AnnotationPen _outline = WpfAnnotationUI.DefaultOutline;

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleData"/> class.
        /// </summary>
        public TriangleData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleData"/> class.
        /// </summary>
        /// <param name="points">The triangle points.</param>
        /// <param name="fill">The annotation fill.</param>
        /// <param name="outline">The annotation outline.</param>
        public TriangleData(Point[] points, AnnotationBrush fill, AnnotationPen outline)
            : base(new PointFCollection(WpfObjectConverter.ConvertPointF(points)))
        {
            _fill = fill;
            _outline = outline;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleData"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public TriangleData(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _fill = (AnnotationBrush)info.GetValue("Fill", typeof(AnnotationBrush));
            _outline = (AnnotationPen)info.GetValue("Outline", typeof(AnnotationPen));
        }

        /// <summary>
        /// Gets or sets the annotation fill.
        /// </summary>
        public AnnotationBrush Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }

        /// <summary>
        /// Gets or sets the annotation outline.
        /// </summary>
        public AnnotationPen Outline
        {
            get { return _outline; }
            set { _outline = value; }
        }

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Fill", _fill);
            info.AddValue("Outline", _outline);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Clone of this object.</returns>
        public override object Clone()
        {
            TriangleData data = new TriangleData();
            CloneBaseData(data);

            if (_fill != null) data._fill = _fill.Clone();
            if (_outline != null) data._outline = _outline.Clone();

            return data;
        }
    }
}