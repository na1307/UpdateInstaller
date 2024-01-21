namespace UpdateInstaller;

[Flags]
public enum CpuArch {
    x86 = 1,
    x64 = 2,
    All = x86 | x64,
}
