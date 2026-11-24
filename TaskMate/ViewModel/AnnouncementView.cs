using TaskMate.Utilities;

namespace TaskMate.ViewModel;

public partial class Announcement : UserControl
{
    private readonly DataGridView grid = new();
    private readonly TextBox title = new() { Width = 240 };
    private readonly TextBox message = new() { Multiline = true, Width = 420, Height = 75, ScrollBars = ScrollBars.Vertical };

    public Announcement()
    {
        InitializeComponent();
        BuildUi();
        LoadAnnouncements();
    }

    private void BuildUi()
    {
        Controls.Clear(); BackColor = Color.White;
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), RowCount = 3 };
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.Controls.Add(UiHelpers.Heading("Announcements"), 0, 0);
        var editor = new TableLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, ColumnCount = 2, RowCount = 3, Padding = new Padding(0, 0, 0, 12) };
        editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90)); editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        editor.Controls.Add(new Label { Text = "Title", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0); editor.Controls.Add(title, 1, 0);
        editor.Controls.Add(new Label { Text = "Message", AutoSize = true, Anchor = AnchorStyles.Top }, 0, 1); editor.Controls.Add(message, 1, 1);
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.LeftToRight };
        var publish = UiHelpers.Button("Publish"); var delete = UiHelpers.Button("Delete Selected");
        buttons.Controls.Add(publish); buttons.Controls.Add(delete); editor.Controls.Add(buttons, 1, 2);
        root.Controls.Add(editor, 0, 1);
        UiHelpers.StyleGrid(grid); root.Controls.Add(grid, 0, 2); Controls.Add(root);
        publish.Click += (_, _) => Publish(); delete.Click += (_, _) => DeleteSelected();
        grid.SelectionChanged += (_, _) => FillEditor();
    }

    private void LoadAnnouncements()
    {
        grid.DataSource = Database.Query("SELECT announcement_id,title AS Title,message AS Message,created_at AS Published FROM announcements WHERE user_id=1 ORDER BY created_at DESC");
        if (grid.Columns["announcement_id"] != null) grid.Columns["announcement_id"].Visible = false;
    }

    private void Publish()
    {
        if (string.IsNullOrWhiteSpace(title.Text) || string.IsNullOrWhiteSpace(message.Text))
        { MessageBox.Show("Enter both a title and a message."); return; }
        Database.Execute("INSERT INTO announcements(user_id,title,message) VALUES(1,@t,@m)",
            Database.P("@t", title.Text.Trim()), Database.P("@m", message.Text.Trim()));
        title.Clear(); message.Clear(); LoadAnnouncements();
    }

    private void FillEditor()
    {
        if (grid.CurrentRow == null) return;
        title.Text = grid.CurrentRow.Cells["Title"]?.Value?.ToString() ?? "";
        message.Text = grid.CurrentRow.Cells["Message"]?.Value?.ToString() ?? "";
    }

    private void DeleteSelected()
    {
        if (grid.CurrentRow?.Cells["announcement_id"].Value is null) { MessageBox.Show("Select an announcement."); return; }
        int id = Convert.ToInt32(grid.CurrentRow.Cells["announcement_id"].Value);
        if (MessageBox.Show("Delete this announcement?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        Database.Execute("DELETE FROM announcements WHERE announcement_id=@id AND user_id=1", Database.P("@id", id));
        title.Clear(); message.Clear(); LoadAnnouncements();
    }
}
