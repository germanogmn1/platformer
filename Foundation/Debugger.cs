namespace Foundation
{
    public static class Debugger
    {
        private static DebuggerForm form;
        private static DebuggerForm Form
        {
            get
            {
                if (form == null)
                    form = new DebuggerForm();
                return form;
            }
        }

        public static bool ShowWindow
        {
            set
            {
                if (value)
                    Form.Show();
                else
                    Form.Hide();
            }
            get { return Form.Visible; }
        }

        public static void Debug(string key, object value)
        {
            Form.Debug(key, value.ToString());
        }
    }
}
