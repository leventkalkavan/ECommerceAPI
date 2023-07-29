namespace ECommerceAPI.Infrastructure.Services;

public class FileService
{
    // private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
    // {
    //     var newFileName = await Task.Run(async () =>
    //     {
    //         var extension = Path.GetExtension(fileName);
    //         var newFileName = string.Empty;
    //         if (first)
    //         {
    //             var oldName = Path.GetFileNameWithoutExtension(fileName);
    //             newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
    //         }
    //         else
    //         {
    //             newFileName = fileName;
    //             var indexNo1 = newFileName.IndexOf("-");
    //             if (indexNo1 == -1)
    //             {
    //                 newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
    //             }
    //             else
    //             {
    //                 var lastIndex = 0;
    //                 while (true)
    //                 {
    //                     lastIndex = indexNo1;
    //                     indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
    //                     if (indexNo1 == -1)
    //                     {
    //                         indexNo1 = lastIndex;
    //                         break;
    //                     }
    //                 }
    //
    //                 var indexNo2 = newFileName.IndexOf(".");
    //                 var fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);
    //
    //                 if (int.TryParse(fileNo, out var _fileNo))
    //                 {
    //                     _fileNo++;
    //                     newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
    //                         .Insert(indexNo1 + 1, _fileNo.ToString());
    //                 }
    //                 else
    //                 {
    //                     newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
    //                 }
    //             }
    //         }
    //
    //         if (File.Exists($"{path}//{newFileName}"))
    //             return await FileRenameAsync(path, newFileName, false);
    //         return newFileName;
    //     });
    //
    //     return newFileName;
    // }
}