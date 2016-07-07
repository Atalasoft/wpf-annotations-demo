// ------------------------------------------------------------------------------------
// <copyright file="About.xaml.cs" company="Atalasoft">
//     (c) 2000-2015 Atalasoft, a Kofax Company. All rights reserved. Use is subject to license terms.
// </copyright>
// ------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace Atalasoft.Demo.WpfAnnotations
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="About"/> class.
        /// </summary>
        /// <param name="dialogLabel">The dialog title.</param>
        /// <param name="demoName">The demo name.</param>
        /// <remarks>This constructor tries to mirror the one used in the WinformsDemos</remarks>
        public About(string dialogLabel, string demoName)
        {
            InitializeComponent();

            // This is the window Title
            Title = dialogLabel;

            // This is the name of the demo at the top of the About Page
            DemoNameField.Content = demoName;

            // This is the Demo Gallery Link Label
            SetDemoGalleryLinkLabel(demoName);
        }
        
        #region Alt Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="About"/> class.
        /// </summary>
        /// <param name="demoName">Name of the demo.</param>
        /// <remarks>Simplified constructor for when you just want mostly defaults</remarks>
        public About(string demoName)
        {
            InitializeComponent();

            // This is the window Title
            Title = "About " + demoName;

            // This is the name of the demo at the top of the About Page
            DemoNameField.Content = demoName;

            // This is the Demo Gallery Link Label
            SetDemoGalleryLinkLabel(demoName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="About"/> class.
        /// </summary>
        /// <param name="demoName">Name of the Demo</param>
        /// <param name="demoLink">Link to the demo's home page</param>
        /// <param name="demoDesc">Full Description of the demo (separate graphs with two crlf</param>
        /// <remarks> An all-in-one version of the constructor for when you want to set everything</remarks>
        public About(string demoName, string demoLink, string demoDesc) : this("About " + demoName, demoName, demoLink, demoDesc)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="About"/> class.
        /// </summary>
        /// <param name="dialogDesc">The title for the about window</param>
        /// <param name="demoName">Name of the Demo</param>
        /// <param name="demoLink">Link to the demo's home page</param>
        /// <param name="demoDesc">Full Description of the demo (separate graphs with two crlf</param>
        /// <remarks>Full-monte - when you want to set all the params up front in the constructor</remarks>
        public About(string dialogDesc, string demoName, string demoLink, string demoDesc)
        {
            InitializeComponent();

            // This is the window Title
            Title = dialogDesc;

            // This is the name of the demo at the top of the About Page
            DemoNameField.Content = demoName;

            // This is the Demo Gallery Link Label
            SetDemoGalleryLinkLabel(demoName);

            // set the hyperlink
            SetDemoGalleryLink(demoLink);

            // set the description
            SetDescription(demoDesc);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="About"/> class.
        /// </summary>
        public About()
        {
            InitializeComponent();
        }

        #endregion Alt Constructors
        
        #region Properties

        /// <summary>
        /// Gets or sets the demo gallery link.
        /// </summary>
        public string Link
        {
            get { return this.demoGalleryLink.NavigateUri.ToString(); }
            set { SetDemoGalleryLink(value); }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get { return this.DemoDescription.Text; }
            set { SetDescription(value); } 
        }
        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Sets the demo gallery link.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public void SetDemoGalleryLink(string uri)
        {
            if (!uri.Contains("http://") && !uri.Contains("https://"))
            {
                demoGalleryLink.NavigateUri = new Uri("http://" + uri);
            }
            else
            {
                demoGalleryLink.NavigateUri = new Uri(uri);
            }
        }

        /// <summary>
        /// Sets the description.
        /// </summary>
        /// <param name="desc">The desc.</param>
        public void SetDescription(string desc)
        {
            this.DemoDescription.Text = desc;
        }

        #endregion Public Methods
        
        #region Private Methods
        private void SetDemoGalleryLinkLabel(string demoName)
        {
            // This is the Demo Gallery Link Label
            if (demoName.Contains("Demo"))
            {
                demoGalleryLinkLabel.Text = demoName + " Home";
            }
            else
            {
                demoGalleryLinkLabel.Text = demoName + " Demo Home";
            }
        }
        #endregion Private Methods
        
        #region Event Handlers
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Uri.OriginalString))
            {
                Process.Start(e.Uri.OriginalString);
            }
        }
        #endregion Event Handlers
    }   
}
