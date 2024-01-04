// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Minor Code Smell", "S6608:Prefer indexing instead of \"Enumerable\" methods on types implementing \"IList\"", Justification = "<보류 중>", Scope = "module")]
[assembly: SuppressMessage("Minor Code Smell", "S3241:Methods should not return values that are never used", Justification = "<보류 중>", Scope = "member", Target = "~M:UpdateInstaller.Program.Main")]
[assembly: SuppressMessage("Major Code Smell", "S4070:Non-flags enums should not be marked with \"FlagsAttribute\"", Justification = "<보류 중>", Scope = "type", Target = "~T:UpdateInstaller.RestartHelper.ShutdownReason")]
[assembly: SuppressMessage("Critical Code Smell", "S2346:Flags enumerations zero-value members should be named \"None\"", Justification = "<보류 중>", Scope = "type", Target = "~T:UpdateInstaller.RestartHelper.ShutdownReason")]
[assembly: SuppressMessage("Major Code Smell", "S112:General or reserved exceptions should never be thrown", Justification = "<보류 중>", Scope = "member", Target = "~M:UpdateInstaller.RestartHelper.Restart")]
[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "<보류 중>", Scope = "type", Target = "~T:UpdateInstaller.RestartHelper")]
