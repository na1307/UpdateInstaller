﻿using System.Runtime.InteropServices;
using UpdateInstaller.Properties;
using static UpdateInstaller.ConfigJsonFileHelper;

namespace UpdateInstaller;

internal static class Program {
    private enum SW {
        Hide,
        Normal,
        ShowMinimized,
        ShowMaximized,
        ShowNoActivate,
        Show,
        Minimize,
        ShowMinNoActive,
        ShowNa,
        Restore,
        ShowDefault,
        ForceMinimize
    }

    [STAThread]
    private static void Main() {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        try {
            // 구성 파일이 없으면 예외 던지기
            if (!File.Exists(ConfigFileName)) throw new UpdateInstallerException("구성 파일을 찾을 수 없습니다.");

            // 구성 파일이 스키마와 일치하지 않음
            if (!IsConfigJsonValid) throw new UpdateInstallerException("구성 파일이 올바르지 않습니다.");
#if !DEBUG

            // 커널이나 서비스 팩 버전이 다름
            if (new Version(OSVersion.ToString("F1")) != new Version(Environment.OSVersion.Version.Major, Environment.OSVersion.Version.Minor) || SPVersion != Winver.SPLevel) throw new UpdateInstallerException("패키지와 현재 운영 체제가 호환되지 않습니다.");
#endif

            if (OSVersion == Vista && Settings.Default.UpdateWorker != WorkerType.PkgMgr) Settings.Default.UpdateWorker = WorkerType.PkgMgr;

            using Mutex uiMutex = new(true, "Global\\eadb0d97-ce09-49e5-a17f-11acdb02323a", out var isCreated);

            if (isCreated) {
                Application.Run(MainForm.Instance);
            } else {
                IntPtr firstInstance = FindWindowW(null, MainForm.Instance.Text);
                ShowWindow(firstInstance, SW.Normal);
                SetForegroundWindow(firstInstance);

                [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
                static extern IntPtr FindWindowW([MarshalAs(UnmanagedType.LPWStr)] string? lpClassName, [MarshalAs(UnmanagedType.LPWStr)] string? lpWindowName);

                [DllImport("user32.dll", ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                static extern bool ShowWindow(IntPtr hWnd, SW nCmdShow);

                [DllImport("user32.dll", ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                static extern bool SetForegroundWindow(IntPtr hWnd);
            }
        } catch (UpdateInstallerException e) {
            ErrMsg(e.Message);
        }
    }
}
