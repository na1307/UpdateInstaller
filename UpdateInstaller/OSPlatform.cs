namespace UpdateInstaller;

[Flags]
public enum OSPlatform {
    Client = 1,
    Server = 2,
    Both = Client | Server,
}
