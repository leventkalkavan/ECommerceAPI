using ECommerceAPI.Infrastructure.Operations;

namespace ECommerceAPI.Infrastructure.Services.Storage;

public class Storage
{
    protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod,
        bool first = true)
    {
        var newFileName = await Task.Run(async () =>
        {
            var extension = Path.GetExtension(fileName);
            var newFileName = string.Empty;
            if (first)
            {
                var oldName = Path.GetFileNameWithoutExtension(fileName);
                newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
            }
            else
            {
                newFileName = fileName;
                var indexNo1 = newFileName.IndexOf("-", StringComparison.Ordinal);
                if (indexNo1 == -1)
                {
                    newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                }
                else
                {
                    var lastIndex = 0;
                    while (true)
                    {
                        lastIndex = indexNo1;
                        indexNo1 = newFileName.IndexOf("-", indexNo1 + 1, StringComparison.Ordinal);
                        if (indexNo1 == -1)
                        {
                            indexNo1 = lastIndex;
                            break;
                        }
                    }

                    var indexNo2 = newFileName.IndexOf(".", StringComparison.Ordinal);
                    var fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

                    if (int.TryParse(fileNo, out var _fileNo))
                    {
                        _fileNo++;
                        newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
                            .Insert(indexNo1 + 1, _fileNo.ToString());
                    }
                    else
                    {
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    }
                }
            }

            //if (File.Exists($"{path}//{newFileName}"))
            if (hasFileMethod(pathOrContainerName, newFileName))
                return await FileRenameAsync(pathOrContainerName, newFileName, hasFileMethod, false);
            return newFileName;
        });

        return newFileName;
    }

    protected delegate bool HasFile(string pathOrContainerName, string fileName);
}