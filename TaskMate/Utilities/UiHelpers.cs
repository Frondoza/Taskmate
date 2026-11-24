using System.Data;

namespace TaskMate.Utilities;

internal static class UiHelpers
{
    public static void StyleGrid(DataGridView grid)
    {
        grid.Dock = DockStyle.Fill;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.RowHeadersVisible = false;
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        grid.DefaultCellStyle.SelectionBackColor = Color.LightGray;
        grid.DefaultCellStyle.SelectionForeColor = Color.Black;
        grid.Font = new Font("Segoe UI", 9);
    }

    public static Button Button(string text)
    {
        return new Button { Text = text, AutoSize = true, Padding = new Padding(8, 4, 8, 4), BackColor = SystemColors.Control, ForeColor = Color.Black, FlatStyle = FlatStyle.Standard };
    }

    public static Label Heading(string text) =>
        new() { Text = text, AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.Black, Margin = new Padding(0, 0, 0, 12) };

    public static bool EditRecord(string title, string initialTitle, string initialDescription,
        out string newTitle, out string newDescription)
    {
        using var form = new Form { Text = title, Width = 440, Height = 300, StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(14), ColumnCount = 1, RowCount = 5 };
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        var titleBox = new TextBox { Dock = DockStyle.Fill, Text = initialTitle };
        var descriptionBox = new TextBox { Dock = DockStyle.Fill, Multiline = true, Text = initialDescription, ScrollBars = ScrollBars.Vertical };
        layout.Controls.Add(new Label { Text = "Title", AutoSize = true }, 0, 0);
        layout.Controls.Add(titleBox, 0, 1);
        layout.Controls.Add(new Label { Text = "Description / message", AutoSize = true, Padding = new Padding(0, 8, 0, 0) }, 0, 2);
        layout.Controls.Add(descriptionBox, 0, 3);
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, AutoSize = true };
        var save = Button("Save");
        var cancel = Button("Cancel");
        save.DialogResult = DialogResult.OK;
        cancel.DialogResult = DialogResult.Cancel;
        buttons.Controls.Add(save);
        buttons.Controls.Add(cancel);
        layout.Controls.Add(buttons, 0, 4);
        form.Controls.Add(layout);
        form.AcceptButton = save;
        form.CancelButton = cancel;
        bool ok = form.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(titleBox.Text);
        newTitle = titleBox.Text.Trim();
        newDescription = descriptionBox.Text.Trim();
        return ok;
    }
}
