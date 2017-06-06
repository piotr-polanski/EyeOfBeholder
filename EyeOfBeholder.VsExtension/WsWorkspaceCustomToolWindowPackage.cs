//------------------------------------------------------------------------------
// <copyright file="WsWorkspaceCustomToolWindowPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using EyeOfBeholder.Uml;
using EyeOfBeholder.Uml.UmlStringGenerators;
using EyeOfBeholder.Uml.UmlType;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

namespace EyeOfBeholder.VsExtension
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(WsWorkspaceCustomToolWindow))]
    [Guid(WsWorkspaceCustomToolWindowPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class WsWorkspaceCustomToolWindowPackage : Package
    {
        /// <summary>
        /// WsWorkspaceCustomToolWindowPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "909045be-c5b8-4fa5-8b65-c2c8cb73bd63";

        private VisualStudioWorkspace workspace;
        /// <summary>
        /// Initializes a new instance of the <see cref="WsWorkspaceCustomToolWindow"/> class.
        /// </summary>
        public WsWorkspaceCustomToolWindowPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            var componentModel = (IComponentModel) this.GetService(typeof(SComponentModel));
            workspace = componentModel.GetService<VisualStudioWorkspace>();

            var plantUmlGenerator = new PlantUmlStringGenerator();
            var diagramGenerator = new DiagramGenerator(plantUmlGenerator);
            var umlEntitesExtractor = new UmlEntitiesExtractor();
            var umlContainers = umlEntitesExtractor.GetFrom(workspace.CurrentSolution.Projects, new List<string>() {"B2BPlatform.Services"});
            var umlStrign = diagramGenerator.GenerateUmlString(umlContainers.SelectMany(c => c.UmlClasses));

            File.WriteAllText(@"PlantUml.txt", umlStrign);

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(@"java -jar plantuml.jar -tsvg PlantUml.txt");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            //Console.WriteLine(cmd.StandardOutput.ReadToEnd());

            WsWorkspaceCustomToolWindowCommand.Initialize(this);
            base.Initialize();
        }

        #endregion
    }
}
