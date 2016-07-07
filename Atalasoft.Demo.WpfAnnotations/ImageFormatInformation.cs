// ------------------------------------------------------------------------------------
// <copyright file="ImageFormatInformation.cs" company="Atalasoft">
//     (c) 2000-2015 Atalasoft, a Kofax Company. All rights reserved. Use is subject to license terms.
// </copyright>
// ------------------------------------------------------------------------------------

using Atalasoft.Imaging.Codec;

namespace Atalasoft.Demo.WpfAnnotations
{
    /// <summary>
    /// Represents image format information.
    /// </summary>
    public struct ImageFormatInformation
    {
        public string Filter;
        public string Description;
        public ImageEncoder Encoder;
        public ImageDecoder Decoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFormatInformation"/> struct.
        /// </summary>
        /// <param name="encoder">The encoder.</param>
        /// <param name="description">The format description.</param>
        /// <param name="filter">The file dialog filter.</param>
        public ImageFormatInformation(ImageEncoder encoder, string description, string filter)
        {
            this.Encoder = encoder;
            this.Decoder = null;
            this.Description = description;
            this.Filter = filter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFormatInformation"/> struct.
        /// </summary>
        /// <param name="decoder">The decoder.</param>
        /// <param name="description">The format description.</param>
        /// <param name="filter">The file dialog filter.</param>
        public ImageFormatInformation(ImageDecoder decoder, string description, string filter)
        {
            this.Decoder = decoder;
            this.Encoder = null;
            this.Description = description;
            this.Filter = filter;
        }
    }
}