using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyTitle("MiscPlugins")]
[assembly: AssemblyFileVersion("1.0.0")]
[assembly: AssemblyCompany(".grey#9120")]
[assembly: AssemblyProduct("MiscPlugins")]
[assembly: AssemblyTrademark(".grey#9120")]
[assembly: InternalsVisibleTo("PluginAPI")]
[assembly: Guid("d9e12874-168b-40ed-a0f4-069dd8be8de9")]
[assembly: AssemblyDescription("Plugin for SCP:SL.")]
[assembly: AssemblyCopyright("Copyright © .grey#9120 2023")]
[assembly: AssemblyMetadata("RepositoryURL", "https://github.com/NotAloneAgain/MiscPlugins")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#elif TRACE
[assembly: AssemblyConfiguration("Production")]
#endif
