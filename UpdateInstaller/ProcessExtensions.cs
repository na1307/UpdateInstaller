namespace UpdateInstaller;

public static class ProcessExtensions {
    public static Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default) {
        if (process.HasExited) {
            return Task.FromResult((object?)null);
        }

        TaskCompletionSource<object?> tcs = new();
        process.EnableRaisingEvents = true;
        process.Exited += (_, _) => tcs.TrySetResult(null);

        if (cancellationToken != default) {
            cancellationToken.Register(() => tcs.TrySetCanceled());
        }

        return !process.HasExited ? tcs.Task : Task.FromResult((object?)null);
    }
}
