using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyTitle("ScpSwap")]
[assembly: AssemblyFileVersion("1.0.0")]
[assembly: AssemblyCompany(".grey#9120")]
[assembly: AssemblyProduct("ScpSwap")]
[assembly: AssemblyTrademark(".grey#9120")]
[assembly: InternalsVisibleTo("PluginAPI")]
[assembly: Guid("011f317d-bef8-4074-b4c8-68c1765c1afa")]
[assembly: AssemblyDescription("Plugin for SCP:SL.")]
[assembly: AssemblyCopyright("Copyright © .grey#9120 2023")]
[assembly: AssemblyMetadata("RepositoryURL", "https://github.com/NotAloneAgain/ScpSwap")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#elif TRACE
[assembly: AssemblyConfiguration("Production")]
#endif
