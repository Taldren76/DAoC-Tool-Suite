namespace DAoCToolSuite.ChimpTool
{
    public enum CursorTypes
    {
        Default,
        Wait
    }
    internal class WaitCursor
    {

        private readonly List<CursorTypes> History = new() { CursorTypes.Default };

        internal void Push()
        {
            if (Cursor.Current is null)
            {
                return;
            }

            History.Add(CursorTypes.Wait);
            if (Cursor.Current == Cursors.Default)
            {
                Cursor.Current = Cursors.WaitCursor;
            }
        }
        internal void PopAll()
        {
            History.Clear();
            History.Add(CursorTypes.Default);
            Cursor.Current = Cursors.Default;
        }
        internal void Pop()
        {
            if (Cursor.Current is null)
            {
                return;
            }

            if (History.Last() != CursorTypes.Default)
            {
                _ = History.Remove(History.Last());
            }
            else if (Cursor.Current != Cursors.Default)
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
