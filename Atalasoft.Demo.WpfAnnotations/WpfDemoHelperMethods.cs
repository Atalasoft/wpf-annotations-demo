// ------------------------------------------------------------------------------------
// <copyright file="WpfDemoHelperMethods.cs" company="Atalasoft">
//     (c) 2000-2015 Atalasoft, a Kofax Company. All rights reserved. Use is subject to license terms.
// </copyright>
// ------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using Atalasoft.Imaging;
using Atalasoft.Imaging.Codec;
using Atalasoft.Imaging.Codec.CadCam;
using Atalasoft.Imaging.Codec.Dicom;
using Atalasoft.Imaging.Codec.Jbig2;
using Atalasoft.Imaging.Codec.Jpeg2000;
using Atalasoft.Imaging.Codec.Office;
using Atalasoft.Imaging.Codec.Pdf;
using Microsoft.Win32;

namespace Atalasoft.Demo.WpfAnnotations
{
    /// <summary>
    /// A collection of static methods.
    /// </summary>
    public sealed class WpfDemoHelperMethods
    {
        private static readonly ArrayList _decoderImageFormats = new ArrayList();
        private static readonly ArrayList _encoderImageFormats = new ArrayList();

        static WpfDemoHelperMethods()
        {
            // Decoders
            _decoderImageFormats.Add(new ImageFormatInformation(new JpegDecoder(), "Joint Photographic Experts Group (*.jpg)", "*.jpg"));
            _decoderImageFormats.Add(new ImageFormatInformation(new PngDecoder(), "Portable Network Graphic (*.png)", "*.png"));
            _decoderImageFormats.Add(new ImageFormatInformation(new TiffDecoder(), "Tagged Image File (*.tif, *.tiff)", "*.tif;*.tiff"));
            _decoderImageFormats.Add(new ImageFormatInformation(new PcxDecoder(), "ZSoft PaintBrush (*.pcx)", "*.pcx"));
            _decoderImageFormats.Add(new ImageFormatInformation(new TgaDecoder(), "Truevision Targa (*.tga)", "*.tga"));
            _decoderImageFormats.Add(new ImageFormatInformation(new BmpDecoder(), "Windows Bitmap (*.bmp)", "*.bmp"));
            _decoderImageFormats.Add(new ImageFormatInformation(new WmfDecoder(), "Windows Meta File (*.wmf)", "*.wmf"));
            _decoderImageFormats.Add(new ImageFormatInformation(new EmfDecoder(), "Enhanced Windows Meta File (*.emf)", "*.emf"));
            _decoderImageFormats.Add(new ImageFormatInformation(new PsdDecoder(), "Adobe (tm) Photoshop format (*.psd)", "*.psd"));
            _decoderImageFormats.Add(new ImageFormatInformation(new WbmpDecoder(), "Wireless Bitmap (*.wbmp)", "*.wbmp"));
            _decoderImageFormats.Add(new ImageFormatInformation(new GifDecoder(), "Graphics Interchange Format (*.gif)", "*.gif"));
            _decoderImageFormats.Add(new ImageFormatInformation(new TlaDecoder(), "Smaller Animals TLA (*.tla)", "*.tla"));
            _decoderImageFormats.Add(new ImageFormatInformation(new PcdDecoder(), "Kodak (tm) PhotoCD (*.pcd)", "*.pcd"));
            _decoderImageFormats.Add(new ImageFormatInformation(new RawDecoder(), "RAW Images", "*.dcr;*.dng;*.eff;*.mrw;*.nef;*.orf;*.pef;*.raf;*.srf;*.x3f;*.crw;*.cr2;*.tif;*.ppm"));

            try
            {
                _decoderImageFormats.Add(new ImageFormatInformation(new DicomDecoder(), "Dicom (*.dcm *.dce)", "*.dcm;*.dce"));
            }
            catch (AtalasoftLicenseException e)
            {
                Console.WriteLine("DicomDecoder license error {0}", e);
            }

            try
            {
                _decoderImageFormats.Add(new ImageFormatInformation(new DwgDecoder(), "Cad/Cam (*.dwg *.dxf)",
                    "*.dwg;*.dxf"));
            }
            catch (AtalasoftLicenseException e)
            {
                Console.WriteLine("DwgDecoder license error {0}", e);
            }

            try
            {
                _decoderImageFormats.Add(new ImageFormatInformation(new Jb2Decoder(), "JBIG2 (*.jb2)", "*.jb2"));
            }
            catch (AtalasoftLicenseException e)
            {
                Console.WriteLine("Jb2Decoder(JBIG2) license error {0}", e);
            }

            try
            {
                _decoderImageFormats.Add(new ImageFormatInformation(new Jp2Decoder(), "JPEG2000 (*.jpf *.jp2, *.jpc *.j2c *.j2k)", "*.jpf;*.jp2;*.jpc;*.j2c;*.j2k"));
            }
            catch (AtalasoftLicenseException e)
            {
                Console.WriteLine("Jp2Decoder(JPEG2000) license error {0}", e);
            }

            try
            {
                _decoderImageFormats.Add(new ImageFormatInformation(new PdfDecoder() { Resolution = 200, RenderSettings = new RenderSettings() { AnnotationSettings = AnnotationRenderSettings.None } }, "PDF (*.pdf)", "*.pdf"));
            }
            catch (AtalasoftLicenseException e)
            {
                Console.WriteLine("PdfDecoder license error {0}", e);
            }

            // If you're getting errors from DotImage < 10.4 due to missing Word Decoder dll, comment this next item out (both lines)
            try
            {
                _decoderImageFormats.Add(new ImageFormatInformation(new OfficeAdapterDecoder(), "MS Office Documents (*.doc *.docx *.xls *.xlsx *.ppt *.pptx)", "*.doc;*.docx*.xls;*.xlsx;*.ppt;*.pptx"));
            }
            catch (AtalasoftLicenseException e)
            {
                Console.WriteLine("OfficeAdapterDecoder license error {0}", e);
            }

            // Encoders
            _encoderImageFormats.Add(new ImageFormatInformation(new JpegEncoder(), "Joint Photographic Experts Group (*.jpg)", "*.jpg"));
            _encoderImageFormats.Add(new ImageFormatInformation(new PngEncoder(), "Portable Network Graphic (*.png)", "*.png"));
            _encoderImageFormats.Add(new ImageFormatInformation(new TiffEncoder(), "Tagged Image File (*.tif, *.tiff)", "*.tif;*.tiff"));
            _encoderImageFormats.Add(new ImageFormatInformation(new PcxEncoder(), "ZSoft PaintBrush (*.pcx)", "*.pcx"));
            _encoderImageFormats.Add(new ImageFormatInformation(new TgaEncoder(), "Truevision Targa (*.tga)", "*.tga"));
            _encoderImageFormats.Add(new ImageFormatInformation(new BmpEncoder(), "Windows Bitmap (*.bmp)", "*.bmp"));
            _encoderImageFormats.Add(new ImageFormatInformation(new WmfEncoder(), "Windows Meta File (*.wmf)", "*.wmf"));
            _encoderImageFormats.Add(new ImageFormatInformation(new EmfEncoder(), "Enhanced Windows Meta File (*.emf)", "*.emf"));
            _encoderImageFormats.Add(new ImageFormatInformation(new PsdEncoder(), "Adobe (tm) Photoshop format (*.psd)", "*.psd"));
            _encoderImageFormats.Add(new ImageFormatInformation(new WbmpEncoder(), "Wireless Bitmap (*.wbmp)", "*.wbmp"));
            _encoderImageFormats.Add(new ImageFormatInformation(new GifEncoder(), "Graphics Interchange Format (*.gif)", "*.gif"));
            _encoderImageFormats.Add(new ImageFormatInformation(new TlaEncoder(), "Smaller Animals TLA (*.tla)", "*.tla"));

            try
            {
                _encoderImageFormats.Add(new ImageFormatInformation(new Jb2Encoder(), "JBIG2 (*.jb2)", "*.jb2"));
            }
            catch (AtalasoftLicenseException e)
            {
                Console.WriteLine("Jb2Encoder license error {0}", e);
            }

            try
            {
                _encoderImageFormats.Add(new ImageFormatInformation(new Jp2Encoder(), "JPEG2000 (*.jpf *.jp2, *.jpc *.j2c *.j2k)", "*.jpf;*.jp2;*.jpc;*.j2c;*.j2k"));
            }
            catch (AtalasoftLicenseException e)
            {
                Console.WriteLine("Jp2Encoder license error {0}", e);
            }

            try
            {
                _encoderImageFormats.Add(new ImageFormatInformation(new PdfEncoder(), "PDF (*.pdf)", "*.pdf"));
            }
            catch (AtalasoftLicenseException e)
            {
                Console.WriteLine("PdfEncoder license error {0}", e);
            }
        }

        /// <summary>
        /// Creates the filter string for open and save dialogs.
        /// </summary>
        /// <param name="isOpenDialog">Set to true if this filter is for an open dialog.</param>
        /// <returns>The filter string for an open or save dialog.</returns>
        public static string CreateDialogFilter(bool isOpenDialog)
        {
            StringBuilder filter = new StringBuilder();

            if (isOpenDialog)
            {
                // All supported formats
                filter.Append("All Supported Images|");
                foreach (ImageFormatInformation info in _decoderImageFormats)
                    filter.Append(info.Filter + ";");

                // Individual format filter
                foreach (ImageFormatInformation info in _decoderImageFormats)
                    filter.Append("|" + info.Description + "|" + info.Filter);

                // Add all files filter, this will cover, e.g. Dicom files without extension
                filter.Append("|All files (*.*)|*.*");
            }
            else
            {
                foreach (ImageFormatInformation info in _encoderImageFormats)
                    filter.Append("|" + info.Description + "|" + info.Filter);

                filter.Append("|Animated GIF (*.gif)|*.gif");
                filter.Remove(0, 1); // Remove leading "|"
            }

            return filter.ToString();
        }

        /// <summary>
        /// Based on licensed decoders, this will generate a search pattern string that may be used to search a directory for supported image formats
        /// </summary>
        /// <returns>search pattern string of supported extensions</returns>
        public static string GenerateDecoderSearchPattern()
        {
            StringBuilder pattern = new StringBuilder();
            foreach (ImageFormatInformation info in _decoderImageFormats)
                pattern.Append(info.Filter + ";");

            pattern.Remove(pattern.Length - 1, 1); // remove trailing semicolon

            return pattern.ToString();
        }

        /// <summary>
        /// Populates the decoders.
        /// </summary>
        /// <param name="col">The decoders collection to populate.</param>
        public static void PopulateDecoders(DecoderCollection col)
        {
            foreach (ImageFormatInformation info in _decoderImageFormats)
                if (!col.Contains(info.Decoder))
                    col.Add(info.Decoder);
        }

        /// <summary>
        /// Gets the image encoder.
        /// </summary>
        /// <param name="filterIndex">Index of the filter.</param>
        /// <returns>The image decoder.</returns>
        public static ImageEncoder GetImageEncoder(int filterIndex)
        {
            // FilterIndex is 1-indexed so decrement it before using
            filterIndex--;
            if (filterIndex < 0 || filterIndex >= _encoderImageFormats.Count) return null;

            ImageFormatInformation info = (ImageFormatInformation)_encoderImageFormats[filterIndex];
            return info.Encoder;
        }

        /// <summary>
        /// Fills the command arrays with method types.
        /// </summary>
        /// <param name="channelCommand">The channel command.</param>
        /// <param name="effectCommand">The effect command.</param>
        /// <param name="filterCommand">The filter command.</param>
        /// <param name="transformCommand">The transform command.</param>
        /// <exception cref="System.ArgumentException">Unable to load the Atalasoft.Imaging assembly.</exception>
        public static void GetReflectionMethods(out Type[] channelCommand, out Type[] effectCommand, out Type[] filterCommand, out Type[] transformCommand)
        {
            // Just to make the compiler happy.
            channelCommand = null;
            effectCommand = null;
            filterCommand = null;
            transformCommand = null;

            // Load the assumebly.
            Assembly myAssembly = Assembly.Load("Atalasoft.Imaging");
            if (myAssembly == null)
                throw new ArgumentException("Unable to load the Atalasoft.Imaging assembly.");

            // Get all of the assembly types.
            Type[] myTypes = myAssembly.GetExportedTypes();

            // Create temporary storage for the types.
            // 100 elements each should be enough.
            Type[] channels = new Type[100];
            Type[] effects = new Type[100];
            Type[] filters = new Type[100];
            Type[] transforms = new Type[100];

            int channelCount = 0;
            int effectCount = 0;
            int filterCount = 0;
            int transformCount = 0;

            // Loop through all of the types and fill out the arrays.
            foreach (Type type in myTypes)
            {
                if (type.IsClass && type.IsPublic)
                {
                    switch (type.Namespace)
                    {
                        case "Atalasoft.Imaging.Imaging.Channels":
                            channels[channelCount] = type;
                            channelCount++;
                            break;
                        case "Atalasoft.Imaging.Imaging.Effects":
                            effects[effectCount] = type;
                            effectCount++;
                            break;
                        case "Atalasoft.Imaging.Imaging.Filters":
                            filters[filterCount] = type;
                            filterCount++;
                            break;
                        case "Atalasoft.Imaging.Imaging.Transforms":
                            transforms[transformCount] = type;
                            transformCount++;
                            break;
                    }
                }
            }

            // Copy the data to the arrays which were passed in.
            if (channelCount > 0)
            {
                channelCommand = new Type[channelCount];
                Array.Copy(channels, 0, channelCommand, 0, channelCount);
            }

            if (effectCount > 0)
            {
                effectCommand = new Type[effectCount];
                Array.Copy(effects, 0, effectCommand, 0, effectCount);
            }

            if (filterCount > 0)
            {
                filterCommand = new Type[filterCount];
                Array.Copy(filters, 0, filterCommand, 0, filterCount);
            }

            if (transformCount > 0)
            {
                transformCommand = new Type[transformCount];
                Array.Copy(transforms, 0, transformCommand, 0, transformCount);
            }
        }

        /// <summary>
        /// Break apart the command name to make it more readable.
        /// </summary>
        /// <param name="commandName">Command name to separate.</param>
        /// <returns>Formated command name.</returns>
        public static string SeparateCommandName(string commandName)
        {
            string nice = string.Empty;
            int lastPos = 0;

            for (int i = 1; i < commandName.Length; i++)
            {
                var letter = commandName[i].ToString();
                if (letter.ToUpper() == letter)
                {
                    nice += commandName.Substring(lastPos, i - lastPos) + " ";
                    lastPos = i;
                }
            }

            nice.Trim();
            return nice;
        }

        /// <summary>
        /// Convenience method to put up message boxes and start the activation wizard to request licenses
        /// </summary>
        /// <param name="message">The message shown as part of (before) the yes/no prompt to request licenses</param>
        private static void LicenseCheckFailure(string message)
        {
            if (MessageBox.Show(null, message + "\r\n\r\nWould you like to request an evaluation license?", "License Required", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                // Locate the activation utility.
                string path = string.Empty;
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Atalasoft\dotImage\6.0");
                if (key != null)
                {
                    path = Convert.ToString(key.GetValue("AssemblyBasePath"));
                    if (path != null && path.Length > 5)
                        path = path.Substring(0, path.Length - 3) + "AtalasoftToolkitActivation.exe";
                    else
                        path = Path.GetFullPath(@"..\..\..\..\..\AtalasoftToolkitActivation.exe");

                    key.Close();
                }

                if (File.Exists(path))
                    Process.Start(path);
                else
                    MessageBox.Show(null, "We were unable to location the DotImage activation utility.\r\nPlease run it from the Start menu shortcut.", "File Not Found");
            }
        }
    }
}