using Shell32;
using System.IO; //not common enough.
namespace MediaHelpers.WPFCoreLibrary.MediaControls;
public static class LengthModule
{
    private static Shell? _shl;
    public static int Length(string tempPath)
    {
        if (File.Exists(tempPath) == true)
        {
            FileInfo fileInf = new(tempPath);
            if (_shl == null)
            {
                _shl = new Shell();
            }
            Folder folder = _shl.NameSpace(fileInf.DirectoryName);
            FolderItem item = folder.ParseName(fileInf.Name);
            string lenthStr = string.Empty;
            for (int index = 0; index <= 150; index++)
            {
                var dtlDesc = folder.GetDetailsOf(null, index);
                var dtlVal = folder.GetDetailsOf(item, index);
                if (dtlDesc.Equals("Length"))
                {
                    lenthStr = dtlVal;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(lenthStr))
            {
                return Convert.ToInt32(TimeSpan.Parse(lenthStr).TotalSeconds);
            }
        }
        return -1;
    }
}