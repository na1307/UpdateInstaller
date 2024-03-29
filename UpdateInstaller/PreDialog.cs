﻿using System.Runtime.CompilerServices;
using static UpdateInstaller.ConfigJsonFileHelper;

namespace UpdateInstaller;

public sealed partial class PreDialog {
    public PreDialog() {
        InitializeComponent();
        Opacity = 0;
        HideOKButton();
        setDescription(1, preEntry1);
        setDescription(2, preEntry2);
        setDescription(3, preEntry3);
        setDescription(4, preEntry4);
        setDescription(5, preEntry5);

        static void setDescription(int index, PreEntry entry) {
            var preUpdate = getPreUpdate(index);

            if (preUpdate is not null) {
                entry.Text = preUpdate.Description;
            } else {
                entry.Visible = false;
            }
        }
    }

    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);

        if (getPreUpdate(1) == null) {
            BeginInvoke(Close);
            MessageBox.Show("사전 업데이트가 없습니다.", "사전 업데이트 설치", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } else {
            Opacity = 1;
        }
    }

    private void entry_Click(object sender, EventArgs e) {
        OK_Button_Click(sender, e);

        new Progress((((PreEntry)sender).Name switch {
            nameof(preEntry1) => getPreUpdate(1)!,
            nameof(preEntry2) => getPreUpdate(2)!,
            nameof(preEntry3) => getPreUpdate(3)!,
            nameof(preEntry4) => getPreUpdate(4)!,
            nameof(preEntry5) => getPreUpdate(5)!,
            _ => throw new InvalidOperationException(),
        }).Updates).Show();
    }

    [MethodImpl(AggressiveInlining)]
    private static PreUpdateItem? getPreUpdate(int index) => PreUpdates?.ElementAtOrDefault(index - 1);
}
