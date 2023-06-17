using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyTitle("RuCollection.API")]
[assembly: AssemblyFileVersion("1.0.0")]
[assembly: AssemblyCompany(".grey#9120")]
[assembly: AssemblyProduct("RuCollection.API")]
[assembly: AssemblyTrademark(".grey#9120")]
[assembly: InternalsVisibleTo("Scp035")]
[assembly: InternalsVisibleTo("ScpSwap")]
[assembly: InternalsVisibleTo("LevelSystem")]
[assembly: InternalsVisibleTo("MiscPlugins")]
[assembly: InternalsVisibleTo("RoundScenarious")]
[assembly: Guid("d1f73c4e-2ad4-4ec9-ac25-cba6290e711f")]
[assembly: AssemblyDescription("Plugin for SCP:SL.")]
[assembly: AssemblyCopyright("Copyright © .grey#9120 2023")]
[assembly: AssemblyMetadata("RepositoryURL", "https://github.com/NotAloneAgain/RuCollection.API")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#elif TRACE
[assembly: AssemblyConfiguration("Production")]
#endif
