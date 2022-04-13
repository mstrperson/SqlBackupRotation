// See https://aka.ms/new-console-template for more information

using System.IO;

Dictionary<DateTime, List<string>> filesByDate = new Dictionary<DateTime, List<string>>();
foreach (string fileName in Directory.GetFiles("C:\\Databases"))
{
    if(!fileName.EndsWith(".BAK")) 
        continue;
    DateTime createdDate = File.GetCreationTime(fileName).Date;
    if(!filesByDate.ContainsKey(createdDate))
        filesByDate.Add(createdDate, new List<string>());
    filesByDate[createdDate].Add(fileName);
}

foreach (DateTime date in filesByDate.Keys)
{
    if(date < DateTime.Today.AddMonths(-6))
        DeleteAllFiles(filesByDate[date]);
    else if(date < DateTime.Today.AddMonths(-1) && date.Day != 1)
        DeleteAllFiles(filesByDate[date]);
    else if(date < DateTime.Today.AddDays(-14) && date.DayOfWeek != DayOfWeek.Monday)
        DeleteAllFiles(filesByDate[date]);
}

void DeleteAllFiles(List<string> fileNames)
{
    foreach(string fileName in fileNames)
        File.Delete(fileName);
}