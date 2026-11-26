using TaskMate.Utilities;

namespace TaskMate.ViewModel;

public partial class ScheduleView : UserControl
{
    private readonly DataGridView grid = new();
    public ScheduleView()
    {
        InitializeComponent();
        BuildUi();
        LoadSchedules();
    }

    private void Cancel_Click(object? sender, EventArgs e) => Visible = false;
    private void CloseBtn_Click(object? sender, EventArgs e) => Visible = false;
    private void ScheduleView_Load(object? sender, EventArgs e) { }

    private void BuildUi()
    {
        Controls.Clear(); BackColor = Color.White;
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), RowCount = 3 };
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.Controls.Add(UiHelpers.Heading("Schedules"), 0, 0);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, Padding = new Padding(0, 0, 0, 10) };
        var add = UiHelpers.Button("Add Schedule"); var edit = UiHelpers.Button("Edit"); var delete = UiHelpers.Button("Delete");
        actions.Controls.AddRange(new Control[] { add, edit, delete });
        root.Controls.Add(actions, 0, 1);
        UiHelpers.StyleGrid(grid); root.Controls.Add(grid, 0, 2); Controls.Add(root);
        add.Click += (_, _) => EditSchedule(null); edit.Click += (_, _) => EditSchedule(SelectedId()); delete.Click += (_, _) => DeleteSchedule();
        grid.CellDoubleClick += (_, _) => EditSchedule(SelectedId());
    }

    private int? SelectedId() => grid.CurrentRow?.Cells["schedule_id"].Value is null ? null : Convert.ToInt32(grid.CurrentRow.Cells["schedule_id"].Value);
    private void LoadSchedules()
    {
        grid.DataSource = Database.Query("""
            SELECT schedule_id,title AS Title,description AS Description,start_time AS Start,end_time AS End
            FROM schedules WHERE user_id=1 ORDER BY start_time
            """);
        grid.Columns["schedule_id"].Visible = false;
    }

    private void EditSchedule(int? id)
    {
        string title = "", description = ""; DateTime start = DateTime.Now, end = DateTime.Now.AddHours(1);
        if (id.HasValue)
        {
            var row = Database.Query("SELECT * FROM schedules WHERE schedule_id=@id AND user_id=1", Database.P("@id", id)).Rows;
            if (row.Count == 0) return;
            title = row[0]["title"].ToString()!; description = row[0]["description"] is DBNull ? "" : row[0]["description"].ToString()!;
            start = Convert.ToDateTime(row[0]["start_time"]); end = Convert.ToDateTime(row[0]["end_time"]);
        }
        using var form = new ScheduleEditorForm(title, description, start, end);
        if (form.ShowDialog(FindForm()) != DialogResult.OK) return;
        if (id.HasValue)
            Database.Execute("UPDATE schedules SET title=@t,description=@d,start_time=@s,end_time=@e WHERE schedule_id=@id AND user_id=1",
                Database.P("@t", form.TitleValue), Database.P("@d", form.DescriptionValue), Database.P("@s", form.StartValue), Database.P("@e", form.EndValue), Database.P("@id", id));
        else
            Database.Execute("INSERT INTO schedules(user_id,title,description,start_time,end_time) VALUES(1,@t,@d,@s,@e)",
                Database.P("@t", form.TitleValue), Database.P("@d", form.DescriptionValue), Database.P("@s", form.StartValue), Database.P("@e", form.EndValue));
        LoadSchedules();
    }

    private void DeleteSchedule()
    {
        if (SelectedId() is not int id) { MessageBox.Show("Select a schedule."); return; }
        if (MessageBox.Show("Delete this schedule?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        Database.Execute("DELETE FROM schedules WHERE schedule_id=@id AND user_id=1", Database.P("@id", id)); LoadSchedules();
    }
}

internal sealed class ScheduleEditorForm : Form
{
    private readonly TextBox title = new() { Dock = DockStyle.Fill };
    private readonly TextBox description = new() { Dock = DockStyle.Fill, Multiline = true, ScrollBars = ScrollBars.Vertical };
    private readonly DateTimePicker start = new() { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Custom, CustomFormat = "yyyy-MM-dd HH:mm" };
    private readonly DateTimePicker end = new() { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Custom, CustomFormat = "yyyy-MM-dd HH:mm" };
    public string TitleValue => title.Text.Trim();
    public string DescriptionValue => description.Text.Trim();
    public DateTime StartValue => start.Value;
    public DateTime EndValue => end.Value;

    public ScheduleEditorForm(string t, string d, DateTime s, DateTime e)
    {
        Text = "Schedule"; Width = 520; Height = 390; StartPosition = FormStartPosition.CenterParent; FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false;
        title.Text = t; description.Text = d; start.Value = s; end.Value = e;
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 2, RowCount = 5 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105)); layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        AddRow(layout, 0, "Title", title, 36); AddRow(layout, 1, "Description", description, 90); AddRow(layout, 2, "Start", start, 36); AddRow(layout, 3, "End", end, 36);
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
        var save = UiHelpers.Button("Save"); var cancel = UiHelpers.Button("Cancel");
        save.Click += (_, _) => { if (string.IsNullOrWhiteSpace(title.Text)) { MessageBox.Show("Enter a title."); return; } if (end.Value <= start.Value) { MessageBox.Show("End must be after start."); return; } DialogResult = DialogResult.OK; Close(); };
        cancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
        buttons.Controls.Add(save); buttons.Controls.Add(cancel); layout.Controls.Add(buttons, 1, 4); Controls.Add(layout);
    }
    private static void AddRow(TableLayoutPanel layout, int row, string label, Control c, int height)
    {
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
        layout.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, 0, row); layout.Controls.Add(c, 1, row);
    }
}
