using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyTitle("LevelSystem")]
[assembly: AssemblyFileVersion("1.0.0")]
[assembly: AssemblyCompany(".grey#9120")]
[assembly: AssemblyProduct("LevelSystem")]
[assembly: AssemblyTrademark(".grey#9120")]
[assembly: InternalsVisibleTo("PluginAPI")]
[assembly: Guid("f31c6470-63af-418c-9636-0b3830a5dabb")]
[assembly: AssemblyDescription("Plugin for SCP:SL.")]
[assembly: AssemblyCopyright("Copyright © .grey#9120 2023")]
[assembly: AssemblyMetadata("RepositoryURL", "https://github.com/NotAloneAgain/LevelSystem")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#elif TRACE
[assembly: AssemblyConfiguration("Production")]
#endif
