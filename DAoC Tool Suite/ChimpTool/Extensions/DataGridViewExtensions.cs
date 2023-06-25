//using DAoCToolSuite.ChimpTool.Settings;
using Logger;
using Newtonsoft.Json;

namespace DAoCToolSuite.ChimpTool.Extensions
{
    public static class DataGridViewExtensions
    {
        internal static LogManager Logger => LogManager.Instance;
        public static void FormatTable(this DataGridView dataGridView, TextProgressBar? progressBar = null)
        {
            List<string> visibleColumns = JsonConvert.DeserializeObject<ColumnNames>(Properties.Settings.Default.DisplayedDatabaseColumnNames)?.Names ?? new() { "Name", "Realm", "Class", "Server", "RealmRank", "TotalRealmPoints", "TotalSoloKills", "TotalDeathBlows", "TotalKills", "TotalDeaths", "IRS", "RPNextRank", "RPLastUpdate", "BountyPoints" };
            List<string> visibleColumnHeaderNames = JsonConvert.DeserializeObject<HeaderNames>(Properties.Settings.Default.DisplayedDataGridViewHeaderNames)?.Names ?? new() { "Name", "Realm", "Class", "Server", "Realm\nRank", "Realm\nPoints", "Solo\nKills", "Death\nBlows", "Kills", "Deaths", "IRS", "RP Next\nRank", "RP Last\nUpdate", "BountyPoints" };
            int rowCount = dataGridView.Rows.Count;
            int columnCount = dataGridView.Columns.Count;
            int nonVisibleIndex = visibleColumns.Count;
            if (progressBar is not null)
            {
                progressBar.MarqueeAnimationSpeed = 0;
                progressBar.Maximum = columnCount + rowCount;
                progressBar.Minimum = 0;
                progressBar.Value = 0;
                progressBar.CustomText = "Formatting Table";
                progressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
                progressBar.Visible = true;
                progressBar.Refresh();
            }

            //Sets the column names, order, and what data from the DB to display.
            //Values pulled from the config file.

            if (columnCount > 0)
            {
                for (int index = 0; index < columnCount; index++)
                {
                    if (progressBar is not null)
                    {
                        progressBar.Value++;
                    }

                    DataGridViewColumn column = dataGridView.Columns[index];
                    if (!visibleColumns.Contains(column.Name))
                    {
                        column.Visible = false;
                        column.DisplayIndex = nonVisibleIndex;
                        nonVisibleIndex++;
                    }
                    else
                    {
                        column.DisplayIndex = visibleColumns.IndexOf(column.Name);
                        column.HeaderText = visibleColumnHeaderNames[visibleColumns.IndexOf(column.Name)];
                        column.ValueType = typeof(string);
                        column.AutoSizeMode = column.Name switch
                        {
                            "MasterLevel_Name" => DataGridViewAutoSizeColumnMode.Fill,
                            _ => DataGridViewAutoSizeColumnMode.AllCells,
                        };
                    }

                }
            }

            ////Formats the row color by Realm
            if (rowCount > 0)
            {
                for (int index = 0; index < rowCount; index++)
                {
                    if (progressBar is not null)
                    {
                        progressBar.Value++;
                    }

                    DataGridViewRow? row = dataGridView.Rows[index];
                    string? realm = row?.Cells["Realm"]?.Value?.ToString();
                    if (row is null || realm is null)
                    {
                        continue;
                    }

                    switch (realm)
                    {
                        case "Albion":
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.DarkRed;
                            row.DefaultCellStyle.SelectionBackColor = Color.White;
                            row.DefaultCellStyle.SelectionForeColor = Color.DarkRed;
                            break;
                        case "Hibernia":
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.DarkGreen;
                            row.DefaultCellStyle.SelectionBackColor = Color.White;
                            row.DefaultCellStyle.SelectionForeColor = Color.DarkGreen;
                            break;
                        case "Midgard":
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.DarkBlue;
                            row.DefaultCellStyle.SelectionBackColor = Color.White;
                            row.DefaultCellStyle.SelectionForeColor = Color.DarkBlue;
                            break;
                        default:
                            break;
                    }
                }
            }
            if (progressBar is not null)
            {
                progressBar.Visible = false;
                progressBar.CustomText = "";
                progressBar.VisualMode = ProgressBarDisplayMode.Percentage;
                progressBar.Value = 0;
                progressBar.Refresh();
            }
        }
    }
    public class HeaderNames
    {
        [JsonProperty]
        public List<string>? Names { get; set; }
    }

    public class ColumnNames
    {
        [JsonProperty]
        public List<string>? Names { get; set; }
    }
}
