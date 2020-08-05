using System.IO;
using System.Text;

namespace Centrifuge.Distance.Helpers
{
    public class ShutUpWwise : TextWriter
    {
        private TextWriter OriginalOut { get; }

        public override Encoding Encoding => Encoding.UTF8;

        public ShutUpWwise(TextWriter originalOut)
        {
            OriginalOut = originalOut;
        }

        public override void Write(string value)
        {
            if (value.StartsWith("MaxFrames:") ||
                value.StartsWith("uValidFrames:") ||
                value.StartsWith("eState:"))
                return;

            OriginalOut.Write(value);
        }

        public override void WriteLine(string value)
        {
            if (value.StartsWith("MaxFrames:") ||
                value.StartsWith("uValidFrames:") ||
                value.StartsWith("eState:"))
                return;

            OriginalOut.WriteLine(value);
        }
    }
}
