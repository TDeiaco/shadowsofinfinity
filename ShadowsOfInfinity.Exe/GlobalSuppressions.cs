// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This application is not cross platform.  It must be run on Windows.", Scope = "member", Target = "~M:ShadowsOfInfinity.BaseRenderer.SaveBmp")]
[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This application is not cross platform.  It must be run on Windows.", Scope = "member", Target = "~M:ShadowsOfInfinity.BaseRenderer.BlitPixel(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)")]
[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This application is not cross platform.  It must be run on Windows.", Scope = "member", Target = "~M:ShadowsOfInfinity.BaseRenderer.InitBitmap(System.Int32,System.Int32,System.Drawing.Color)")]
[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "This application is not cross platform.  It must be run on Windows.", Scope = "member", Target = "~F:ShadowsOfInfinity.BaseRenderer._imageFormat")]
