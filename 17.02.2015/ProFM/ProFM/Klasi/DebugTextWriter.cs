using System;
using System.Text;
using System.Windows.Forms;

namespace ProFM
{
    class DebugTextWriter : System.IO.TextWriter
    {
        private TextBox _logTextBox;

        public DebugTextWriter(TextBox log)
        {
            _logTextBox = log;
        }

        public override void Write(char[] buffer, int index, int count)
        {
            _logTextBox.AppendText(string.Concat(new String(buffer, index, count), "\n"));
        }

        public override void Write(string value)
        {
            _logTextBox.AppendText(value);
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }
    }
}
