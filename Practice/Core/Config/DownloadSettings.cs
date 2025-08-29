using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config;
public class DownloadSettings
{
    public string? Folder { get; set; }
    public string? FileName { get; set; }

    public string FullPath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Folder, FileName);
}
