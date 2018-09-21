using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HebrewToUtf8
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                MessageBox.Show("Hebrew encoding to UTF-8 converter\nDeveloped by Oron Feinerman (OronDF343)\nDrag files on to the executable to convert them\n\n\u202bהמרת קידוד בעברית ל-UTF-8\n\u202bפותח על ידי אורון פיינרמן (OronDF343)\n\u202bגרור קבצים על קובץ ההרצה בכדי להמיר אותם",
                                "HebrewToUtf8", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            foreach (var f in args)
            {
                try
                {
                    var s = File.ReadAllText(f, Encoding.GetEncoding(1255));
                    var arr = Encoding.UTF8.GetBytes(s);
                    using (var o = File.Create(f))
                    {
                        if (arr[0] != 0xEF || arr[1] != 0xBB || arr[2] != 0xBF)
                        {
                            o.WriteByte(0xEF);
                            o.WriteByte(0xBB);
                            o.WriteByte(0xBF);
                        }
                        o.Write(arr, 0, arr.Length);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Error | שגיאה\n{e.Message}\n{f}", "HebrewToUtf8", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            MessageBox.Show("Done | הסתיים", "HebrewToUtf8", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
